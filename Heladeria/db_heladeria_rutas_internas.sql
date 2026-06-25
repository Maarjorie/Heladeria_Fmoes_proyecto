-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 25-06-2026 a las 22:01:24
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `db_heladeria`
--

DELIMITER $$
--
-- Procedimientos
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `p_abrir_caja` (IN `p_id_cajero` INT, IN `p_fondo_inicial` DECIMAL(10,2), OUT `p_id_caja` INT)   BEGIN
    INSERT INTO tbl_cajas(
        id_cajero,
        fondo_inicial,
        estado
    )
    VALUES(
        p_id_cajero,
        p_fondo_inicial,
        'abierta'
    );
    SET p_id_caja = LAST_INSERT_ID();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_actualizar_contrasena` (IN `p_id_usuario` INT, IN `p_hash` VARCHAR(255), IN `p_salt` VARCHAR(255))   BEGIN
    UPDATE tbl_usuarios
    SET `contraseña_hash`        = p_hash,
        `contraseña_salt`        = p_salt,
        `contraseña_temp_hash`   = NULL,
        `contraseña_temp_salt`   = NULL,
        `contraseña_temp_expira` = NULL
    WHERE id_usuario = p_id_usuario;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_actualizar_totales_pedido_mayorista` (IN `p_id_pedido` INT, IN `p_subtotal` DECIMAL(10,2), IN `p_descuento` DECIMAL(10,2), IN `p_total` DECIMAL(10,2))   BEGIN
    UPDATE tbl_pedidos_mayoristas
    SET subtotal = p_subtotal,
        descuento = p_descuento,
        total = p_total
    WHERE id_pedido = p_id_pedido;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_agregar_detalle_liquidacion` (IN `p_id_liquidacion` INT, IN `p_id_producto` INT, IN `p_cantidad_vendida` INT, IN `p_cantidad_devuelta` INT, IN `p_cantidad_daniada` INT)   BEGIN
    DECLARE v_id_salida INT;
    DECLARE v_cantidad_carga INT;
    DECLARE v_precio_venta DECIMAL(10,2);
    DECLARE v_total_vendido DECIMAL(10,2);
    DECLARE v_comision DECIMAL(5,2);

    SELECT id_salida
    INTO v_id_salida
    FROM tbl_liquidaciones_ruta
    WHERE id_liquidacion = p_id_liquidacion;

    SELECT cantidad_carga, precio_venta
    INTO v_cantidad_carga, v_precio_venta
    FROM tbl_detalles_salida_ruta
    WHERE id_salida = v_id_salida
    AND id_producto = p_id_producto;

    IF v_cantidad_carga <> (p_cantidad_vendida + p_cantidad_devuelta + p_cantidad_daniada) THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Las cantidades no coinciden con la carga inicial';
    ELSE

        INSERT INTO tbl_detalles_liquidacion(
            id_liquidacion,
            id_producto,
            cantidad_carga,
            cantidad_vendida,
            cantidad_devuelta,
            cantidad_dañada,
            precio_venta
        )
        VALUES(
            p_id_liquidacion,
            p_id_producto,
            v_cantidad_carga,
            p_cantidad_vendida,
            p_cantidad_devuelta,
            p_cantidad_daniada,
            v_precio_venta
        );

        UPDATE tbl_productos
        SET stock_actual = stock_actual + p_cantidad_devuelta
        WHERE id_producto = p_id_producto;

        SELECT IFNULL(SUM(subtotal),0)
        INTO v_total_vendido
        FROM tbl_detalles_liquidacion
        WHERE id_liquidacion = p_id_liquidacion;

        SELECT s.comision
        INTO v_comision
        FROM tbl_salidas_ruta s
        INNER JOIN tbl_liquidaciones_ruta l
            ON s.id_salida = l.id_salida
        WHERE l.id_liquidacion = p_id_liquidacion;

        UPDATE tbl_liquidaciones_ruta
        SET
            total_vendido = v_total_vendido,
            comision_generada = v_total_vendido * (v_comision / 100)
        WHERE id_liquidacion = p_id_liquidacion;

    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_agregar_detalle_pedido_mayorista` (IN `p_id_pedido` INT, IN `p_id_producto` INT, IN `p_cantidad` INT)   BEGIN

    DECLARE v_precio DECIMAL(10,2);

    SELECT precio_venta
    INTO v_precio
    FROM tbl_productos
    WHERE id_producto = p_id_producto;

    INSERT INTO tbl_detalles_pedido_mayorista(
        id_pedido,
        id_producto,
        cantidad,
        precio_unitario
    )
    VALUES(
        p_id_pedido,
        p_id_producto,
        p_cantidad,
        v_precio
    );

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_agregar_detalle_venta_sucursal` (IN `p_id_venta` INT, IN `p_id_producto` INT, IN `p_cantidad` INT, IN `p_descuento` DECIMAL(10,2), IN `p_id_usuario` INT)   BEGIN
    DECLARE v_precio DECIMAL(10,2);
    DECLARE v_stock INT;
    DECLARE v_subtotal DECIMAL(10,2);

    SELECT precio_venta, stock_actual
    INTO v_precio, v_stock
    FROM tbl_productos
    WHERE id_producto = p_id_producto;

    IF v_stock < p_cantidad THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Stock insuficiente para realizar la venta';
    ELSE

        INSERT INTO tbl_detalles_venta_sucursal(
            id_venta,
            id_producto,
            cantidad,
            precio_unitario,
            descuento
        )
        VALUES(
            p_id_venta,
            p_id_producto,
            p_cantidad,
            v_precio,
            p_descuento
        );

        UPDATE tbl_productos
        SET stock_actual = stock_actual - p_cantidad
        WHERE id_producto = p_id_producto;

        INSERT INTO tbl_movimientos_inventario(
            id_producto,
            id_usuario,
            tipo,
            cantidad,
            referencia,
            observacion
        )
        VALUES(
            p_id_producto,
            p_id_usuario,
            'salida',
            p_cantidad,
            CONCAT('Venta sucursal #', p_id_venta),
            'Salida por venta en sucursal'
        );

        SELECT IFNULL(SUM(subtotal),0)
        INTO v_subtotal
        FROM tbl_detalles_venta_sucursal
        WHERE id_venta = p_id_venta;

        UPDATE tbl_ventas_sucursal
        SET
            subtotal = v_subtotal,
            total = v_subtotal - descuento
        WHERE id_venta = p_id_venta;

    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_anular_venta_sucursal` (IN `p_id_venta` INT, IN `p_id_usuario` INT, IN `p_motivo` VARCHAR(255))   BEGIN
    UPDATE tbl_productos p
    INNER JOIN tbl_detalles_venta_sucursal d
        ON p.id_producto = d.id_producto
    SET p.stock_actual = p.stock_actual + d.cantidad
    WHERE d.id_venta = p_id_venta;

    INSERT INTO tbl_movimientos_inventario(
        id_producto,
        id_usuario,
        tipo,
        cantidad,
        referencia,
        observacion
    )
    SELECT
        id_producto,
        p_id_usuario,
        'devolucion',
        cantidad,
        CONCAT('Anulacion venta #', p_id_venta),
        p_motivo
    FROM tbl_detalles_venta_sucursal
    WHERE id_venta = p_id_venta;

    UPDATE tbl_ventas_sucursal
    SET activo = 0
    WHERE id_venta = p_id_venta;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_aprobar_ajuste_inventario` (IN `p_id_movimiento` INT, IN `p_id_aprobado_por` INT)   BEGIN
    DECLARE v_cantidad INT;
    DECLARE v_producto INT;
    DECLARE v_estado VARCHAR(20);

    SELECT cantidad, id_producto, estado
        INTO v_cantidad, v_producto, v_estado
    FROM tbl_movimientos_inventario
    WHERE id_movimiento = p_id_movimiento;

    IF v_estado = 'pendiente' THEN
        UPDATE tbl_movimientos_inventario
        SET estado = 'aplicado', id_aprobado_por = p_id_aprobado_por
        WHERE id_movimiento = p_id_movimiento;

        UPDATE tbl_productos
        SET stock_actual = stock_actual + v_cantidad
        WHERE id_producto = v_producto;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_aprobar_promocion` (IN `p_id_promocion` INT, IN `p_id_aprobado_por` INT)   BEGIN
    UPDATE tbl_promociones
    SET estado = 'aprobada', id_aprobado_por = p_id_aprobado_por, activo = 1
    WHERE id_promocion = p_id_promocion;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_autorizar_arqueo` (IN `p_id_arqueo` INT, IN `p_id_autorizado_por` INT)   BEGIN
    UPDATE tbl_arqueos_caja
    SET autorizado = 1, id_autorizado_por = p_id_autorizado_por
    WHERE id_arqueo = p_id_arqueo;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_buscar_rutas` (IN `p_busqueda` VARCHAR(100))   BEGIN
    SELECT
        id_ruta,
        nombre,
        zona,
        id_responsable,
        horario_inicio,
        horario_fin,
        activo
    FROM tbl_rutas
    WHERE nombre LIKE CONCAT('%', p_busqueda, '%')
       OR zona LIKE CONCAT('%', p_busqueda, '%')
    ORDER BY nombre;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_cambiar_estado_cliente_mayorista` (IN `p_id_cliente` INT, IN `p_activo` TINYINT(1))   BEGIN
    UPDATE tbl_clientes_mayoristas
    SET activo = p_activo
    WHERE id_cliente = p_id_cliente;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_cambiar_estado_producto` (IN `p_id_producto` INT, IN `p_activo` TINYINT(1))   BEGIN
    UPDATE tbl_productos
    SET activo = p_activo
    WHERE id_producto = p_id_producto;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_cambiar_estado_promocion` (IN `p_id_promocion` INT, IN `p_activo` TINYINT(1))   BEGIN
    UPDATE tbl_promociones
    SET activo = p_activo
    WHERE id_promocion = p_id_promocion;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_cambiar_estado_ruta` (IN `p_id_ruta` INT, IN `p_activo` TINYINT(1))   BEGIN
    UPDATE tbl_rutas
    SET activo = p_activo
    WHERE id_ruta = p_id_ruta;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_cambiar_estado_usuario` (IN `p_id_usuario` INT, IN `p_activo` TINYINT(1))   BEGIN
    UPDATE tbl_usuarios
    SET activo = p_activo
    WHERE id_usuario = p_id_usuario;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_cambiar_estado_vendedor` (IN `p_id_vendedor` INT, IN `p_activo` TINYINT(1))   BEGIN
    UPDATE tbl_vendedores
    SET estado = IF(p_activo = 1, 'activo', 'inactivo')
    WHERE id_vendedor = p_id_vendedor;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_cancelar_pedido_mayorista` (IN `p_id_pedido` INT)   BEGIN
    UPDATE tbl_pedidos_mayoristas
    SET estado = 'cancelado'
    WHERE id_pedido = p_id_pedido AND estado = 'pendiente';
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_cerrar_caja` (IN `p_id_caja` INT, IN `p_monto_contado` DECIMAL(10,2))   BEGIN
    DECLARE v_total_ventas DECIMAL(10,2);

    SELECT IFNULL(SUM(total),0)
    INTO v_total_ventas
    FROM tbl_ventas_sucursal
    WHERE id_caja = p_id_caja
    AND activo = 1;

    UPDATE tbl_cajas
    SET
        fecha_cierre = NOW(),
        total_ventas = v_total_ventas,
        monto_contado = p_monto_contado,
        diferencia = p_monto_contado - v_total_ventas,
        estado = 'cerrada'
    WHERE id_caja = p_id_caja;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_confirmar_pedido_mayorista` (IN `p_id_pedido` INT)   BEGIN

    UPDATE tbl_pedidos_mayoristas
    SET
        estado = 'confirmado',
        codigo_retiro = CONCAT(
            'HTX',
            FLOOR(100000 + RAND() * 900000)
        )
    WHERE id_pedido = p_id_pedido;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_crear_pedido_mayorista` (IN `p_id_cliente` INT, IN `p_id_atendido_por` INT, IN `p_fecha_entrega_programada` DATETIME, OUT `p_id_pedido` INT)   BEGIN
    INSERT INTO tbl_pedidos_mayoristas(
        id_cliente, id_atendido_por, codigo_pedido, estado, fecha_pedido,
        fecha_entrega_programada)
    VALUES(
        p_id_cliente,
        p_id_atendido_por,
        CONCAT('PM', LPAD(FLOOR(RAND() * 1000000), 6, '0')),
        'pendiente',
        NOW(),
        p_fecha_entrega_programada);

    SET p_id_pedido = LAST_INSERT_ID();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_editar_cliente_mayorista` (IN `p_id_cliente` INT, IN `p_nombre_comercial` VARCHAR(150), IN `p_nit` VARCHAR(25), IN `p_encargado` VARCHAR(150), IN `p_direccion` VARCHAR(255), IN `p_telefono` VARCHAR(20), IN `p_correo` VARCHAR(100), IN `p_descuento_porcentaje` DECIMAL(5,2))   BEGIN
    UPDATE tbl_clientes_mayoristas
    SET nombre_comercial = p_nombre_comercial,
        nit = p_nit,
        encargado = p_encargado,
        direccion = p_direccion,
        telefono = p_telefono,
        correo = p_correo,
        descuento_porcentaje = p_descuento_porcentaje
    WHERE id_cliente = p_id_cliente;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_editar_producto` (IN `p_id_producto` INT, IN `p_codigo` VARCHAR(50), IN `p_codigo_barras` VARCHAR(100), IN `p_nombre` VARCHAR(100), IN `p_id_categoria` INT, IN `p_presentacion` VARCHAR(100), IN `p_precio_compra` DECIMAL(10,2), IN `p_precio_venta` DECIMAL(10,2), IN `p_stock_minimo` INT, IN `p_fecha_vencimiento` DATE, IN `p_imagen_ruta` VARCHAR(255))   BEGIN
    UPDATE tbl_productos
    SET
        codigo = p_codigo, codigo_barras = p_codigo_barras,
        nombre = p_nombre, id_categoria = p_id_categoria,
        presentacion = p_presentacion, precio_compra = p_precio_compra,
        precio_venta = p_precio_venta, stock_minimo = p_stock_minimo,
        fecha_vencimiento = p_fecha_vencimiento, imagen_ruta = p_imagen_ruta
    WHERE id_producto = p_id_producto;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_editar_promocion` (IN `p_id_promocion` INT, IN `p_id_producto` INT, IN `p_id_categoria` INT, IN `p_nombre` VARCHAR(150), IN `p_descripcion` TEXT, IN `p_tipo_descuento` VARCHAR(20), IN `p_valor_descuento` DECIMAL(10,2), IN `p_fecha_inicio` DATE, IN `p_fecha_fin` DATE)   BEGIN
    UPDATE tbl_promociones
    SET
        id_producto = p_id_producto,
        id_categoria = p_id_categoria,
        nombre = p_nombre,
        descripcion = p_descripcion,
        tipo_descuento = p_tipo_descuento,
        valor_descuento = p_valor_descuento,
        fecha_inicio = p_fecha_inicio,
        fecha_fin = p_fecha_fin
    WHERE id_promocion = p_id_promocion;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_editar_ruta` (IN `p_id_ruta` INT, IN `p_nombre` VARCHAR(100), IN `p_zona` VARCHAR(100), IN `p_id_responsable` INT, IN `p_horario_inicio` TIME, IN `p_horario_fin` TIME)   BEGIN
    UPDATE tbl_rutas
    SET
        nombre = p_nombre,
        zona = p_zona,
        id_responsable = p_id_responsable,
        horario_inicio = p_horario_inicio,
        horario_fin = p_horario_fin
    WHERE id_ruta = p_id_ruta;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_editar_usuario` (IN `p_id_usuario` INT, IN `p_nombre` VARCHAR(150), IN `p_correo` VARCHAR(150), IN `p_id_rol` INT)   BEGIN
    UPDATE tbl_usuarios
    SET
        nombre = p_nombre,
        correo = p_correo,
        id_rol = p_id_rol
    WHERE id_usuario = p_id_usuario;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_editar_vendedor` (IN `p_id_vendedor` INT, IN `p_codigo_empleado` VARCHAR(20), IN `p_nombre` VARCHAR(150), IN `p_fotografia` VARCHAR(200), IN `p_dui` VARCHAR(15), IN `p_telefono` VARCHAR(20), IN `p_direccion` VARCHAR(255))   BEGIN
    UPDATE tbl_vendedores
    SET
        codigo_empleado = p_codigo_empleado,
        nombre = p_nombre,
        fotografia = p_fotografia,
        dui = p_dui,
        telefono = p_telefono,
        direccion = p_direccion
    WHERE id_vendedor = p_id_vendedor;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_entregar_pedido_mayorista` (IN `p_id_pedido` INT)   BEGIN

    UPDATE tbl_pedidos_mayoristas
    SET
        estado = 'entregado',
        fecha_entrega = NOW()
    WHERE id_pedido = p_id_pedido;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_existe_notificacion_reciente` (IN `p_tipo` VARCHAR(30), IN `p_referencia_id` INT, IN `p_horas` INT)   BEGIN
    SELECT COUNT(*) AS total
    FROM tbl_notificacion
    WHERE tipo = p_tipo
      AND referencia_id = p_referencia_id
      AND fecha_registro >= (NOW() - INTERVAL p_horas HOUR);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_guardar_contrasena_temporal` (IN `p_id_usuario` INT, IN `p_hash` VARCHAR(255), IN `p_salt` VARCHAR(255), IN `p_expira` DATETIME)   BEGIN
    UPDATE tbl_usuarios
    SET `contraseña_temp_hash`   = p_hash,
        `contraseña_temp_salt`   = p_salt,
        `contraseña_temp_expira` = p_expira
    WHERE id_usuario = p_id_usuario;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_insertar_arqueo_caja` (IN `p_id_caja` INT, IN `p_id_realizado_por` INT, IN `p_monto_esperado` DECIMAL(10,2), IN `p_monto_contado` DECIMAL(10,2))   BEGIN
    INSERT INTO tbl_arqueos_caja(
        id_caja,
        id_realizado_por,
        monto_esperado,
        monto_contado
    )
    VALUES(
        p_id_caja,
        p_id_realizado_por,
        p_monto_esperado,
        p_monto_contado
    );
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_insertar_cliente_mayorista` (IN `p_nombre_comercial` VARCHAR(150), IN `p_nit` VARCHAR(25), IN `p_encargado` VARCHAR(150), IN `p_direccion` VARCHAR(255), IN `p_telefono` VARCHAR(20), IN `p_correo` VARCHAR(100), IN `p_descuento_porcentaje` DECIMAL(5,2))   BEGIN
    INSERT INTO tbl_clientes_mayoristas(
        nombre_comercial, nit, encargado, direccion, telefono, correo,
        descuento_porcentaje, activo)
    VALUES(
        p_nombre_comercial, p_nit, p_encargado, p_direccion, p_telefono, p_correo,
        p_descuento_porcentaje, 1);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_insertar_movimiento_caja` (IN `p_id_caja` INT, IN `p_id_usuario` INT, IN `p_tipo` VARCHAR(10), IN `p_concepto` VARCHAR(200), IN `p_monto` DECIMAL(10,2))   BEGIN
    INSERT INTO tbl_movimientos_caja(
        id_caja,
        id_usuario,
        tipo,
        concepto,
        monto
    )
    VALUES(
        p_id_caja,
        p_id_usuario,
        p_tipo,
        p_concepto,
        p_monto
    );
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_insertar_notificacion` (IN `p_tipo` VARCHAR(30), IN `p_referencia_id` INT, IN `p_mensaje` TEXT)   BEGIN
    INSERT INTO tbl_notificacion(
        tipo,
        referencia_id,
        mensaje
    )
    VALUES(
        p_tipo,
        p_referencia_id,
        p_mensaje
    );
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_insertar_producto` (IN `p_codigo` VARCHAR(50), IN `p_codigo_barras` VARCHAR(100), IN `p_nombre` VARCHAR(100), IN `p_id_categoria` INT, IN `p_presentacion` VARCHAR(100), IN `p_precio_compra` DECIMAL(10,2), IN `p_precio_venta` DECIMAL(10,2), IN `p_stock_actual` INT, IN `p_stock_minimo` INT, IN `p_fecha_ingreso` DATE, IN `p_fecha_vencimiento` DATE, IN `p_imagen_ruta` VARCHAR(255))   BEGIN
    INSERT INTO tbl_productos(
        codigo, codigo_barras, nombre, id_categoria, presentacion,
        precio_compra, precio_venta, stock_actual, stock_minimo,
        fecha_ingreso, fecha_vencimiento, imagen_ruta
    )
    VALUES(
        p_codigo, p_codigo_barras, p_nombre, p_id_categoria, p_presentacion,
        p_precio_compra, p_precio_venta, p_stock_actual, p_stock_minimo,
        p_fecha_ingreso, p_fecha_vencimiento, p_imagen_ruta
    );
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_insertar_promocion` (IN `p_id_producto` INT, IN `p_id_categoria` INT, IN `p_nombre` VARCHAR(150), IN `p_descripcion` TEXT, IN `p_tipo_descuento` VARCHAR(20), IN `p_valor_descuento` DECIMAL(10,2), IN `p_fecha_inicio` DATE, IN `p_fecha_fin` DATE)   BEGIN
    INSERT INTO tbl_promociones(
        id_producto, id_categoria, nombre, descripcion,
        tipo_descuento, valor_descuento, fecha_inicio, fecha_fin
    )
    VALUES(
        p_id_producto, p_id_categoria, p_nombre, p_descripcion,
        p_tipo_descuento, p_valor_descuento, p_fecha_inicio, p_fecha_fin
    );
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_insertar_ruta` (IN `p_nombre` VARCHAR(100), IN `p_zona` VARCHAR(100), IN `p_id_responsable` INT, IN `p_horario_inicio` TIME, IN `p_horario_fin` TIME)   BEGIN
    INSERT INTO tbl_rutas(
        nombre,
        zona,
        id_responsable,
        horario_inicio,
        horario_fin
    )
    VALUES(
        p_nombre,
        p_zona,
        p_id_responsable,
        p_horario_inicio,
        p_horario_fin
    );
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_insertar_usuario` (IN `p_id_rol` INT, IN `p_nombre` VARCHAR(150), IN `p_usuario` VARCHAR(60), IN `p_contrasena_hash` VARCHAR(255), IN `p_contrasena_salt` VARCHAR(255), IN `p_correo` VARCHAR(150))   BEGIN
    INSERT INTO tbl_usuarios (
        id_rol,
        nombre,
        usuario,
        contraseña_hash,
        contraseña_salt,
        correo
    )
    VALUES (
        p_id_rol,
        p_nombre,
        p_usuario,
        p_contrasena_hash,
        p_contrasena_salt,
        p_correo
    );
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_insertar_vendedor` (IN `p_codigo_empleado` VARCHAR(20), IN `p_nombre` VARCHAR(150), IN `p_fotografia` VARCHAR(200), IN `p_dui` VARCHAR(15), IN `p_telefono` VARCHAR(20), IN `p_direccion` VARCHAR(255))   BEGIN
    INSERT INTO tbl_vendedores(
        codigo_empleado,
        nombre,
        fotografia,
        dui,
        telefono,
        direccion,
        estado
    )
    VALUES(
        p_codigo_empleado,
        p_nombre,
        p_fotografia,
        p_dui,
        p_telefono,
        p_direccion,
        1
    );
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_kpi_comisiones_dia` (IN `p_fecha` DATE)   BEGIN
    SELECT COALESCE(SUM(lr.comision_generada), 0) AS comisiones
    FROM tbl_liquidaciones_ruta lr
    INNER JOIN tbl_salidas_ruta sr ON lr.id_salida = sr.id_salida
    WHERE sr.fecha = p_fecha;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_kpi_egresos_caja_dia` (IN `p_fecha` DATE)   BEGIN
    SELECT COALESCE(SUM(monto), 0) AS egresos
    FROM tbl_movimientos_caja
    WHERE tipo = 'egreso' AND DATE(fecha_registro) = p_fecha;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_kpi_producto_top_dia` (IN `p_fecha` DATE)   BEGIN
    SELECT pr.nombre AS producto, SUM(d.cantidad) AS unidades
    FROM tbl_detalles_venta_sucursal d
    INNER JOIN tbl_ventas_sucursal vs ON d.id_venta = vs.id_venta
    INNER JOIN tbl_productos pr ON d.id_producto = pr.id_producto
    WHERE DATE(vs.fecha_registro) = p_fecha AND vs.activo = 1
    GROUP BY pr.id_producto, pr.nombre
    ORDER BY unidades DESC
    LIMIT 1;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_kpi_utilidad_dia` (IN `p_fecha` DATE)   BEGIN
    SELECT COALESCE(SUM((d.precio_unitario - pr.precio_compra) * d.cantidad - d.descuento), 0) AS utilidad
    FROM tbl_ventas_sucursal vs
    INNER JOIN tbl_detalles_venta_sucursal d ON vs.id_venta = d.id_venta
    INNER JOIN tbl_productos pr ON d.id_producto = pr.id_producto
    WHERE DATE(vs.fecha_registro) = p_fecha AND vs.activo = 1;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_kpi_ventas_mayoristas_dia` (IN `p_fecha` DATE)   BEGIN
    SELECT COALESCE(SUM(total), 0) AS ventas_mayoristas
    FROM tbl_pedidos_mayoristas
    WHERE DATE(fecha_pedido) = p_fecha AND estado <> 'cancelado';
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_kpi_ventas_por_ruta_dia` (IN `p_fecha` DATE)   BEGIN
    SELECT COALESCE(SUM(lr.total_vendido), 0) AS ventas_ruta
    FROM tbl_liquidaciones_ruta lr
    INNER JOIN tbl_salidas_ruta sr ON lr.id_salida = sr.id_salida
    WHERE sr.fecha = p_fecha;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_liquidar_ruta` (IN `p_id_salida` INT, IN `p_hora_regreso` TIME, IN `p_dinero_entregado` DECIMAL(10,2), IN `p_observacion` TEXT, OUT `p_id_liquidacion` INT)   BEGIN
    INSERT INTO tbl_liquidaciones_ruta(
        id_salida,
        hora_regreso,
        total_vendido,
        comision_generada,
        dinero_entregado,
        observacion
    )
    VALUES(
        p_id_salida,
        p_hora_regreso,
        0,
        0,
        p_dinero_entregado,
        p_observacion
    );

    SET p_id_liquidacion = LAST_INSERT_ID();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_listar_arqueos_caja` (IN `p_id_caja` INT)   BEGIN
    SELECT
        id_arqueo,
        id_caja,
        id_realizado_por,
        monto_esperado,
        monto_contado,
        diferencia,
        fecha_registro
    FROM tbl_arqueos_caja
    WHERE id_caja = p_id_caja
    ORDER BY fecha_registro DESC;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_listar_arqueos_pendientes` ()   BEGIN
    SELECT
        a.id_arqueo,
        a.id_caja,
        u.nombre AS realizado_por,
        a.monto_esperado,
        a.monto_contado,
        a.diferencia,
        a.fecha_registro
    FROM tbl_arqueos_caja a
    LEFT JOIN tbl_usuarios u ON a.id_realizado_por = u.id_usuario
    WHERE a.autorizado = 0
    ORDER BY a.fecha_registro DESC;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_listar_categorias` ()   BEGIN
    SELECT id_categoria, nombre, descripcion, activo
    FROM tbl_categorias
    WHERE activo = 1
    ORDER BY nombre;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_listar_clientes_mayoristas` ()   BEGIN
    SELECT
        id_cliente,
        nombre_comercial,
        nit,
        encargado,
        direccion,
        telefono,
        correo,
        descuento_porcentaje,
        activo
    FROM tbl_clientes_mayoristas
    ORDER BY nombre_comercial;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_listar_liquidaciones_pendientes` ()   BEGIN
    SELECT
        l.id_liquidacion,
        l.id_salida,
        v.nombre            AS vendedor,
        r.nombre            AS ruta,
        l.total_vendido,
        l.comision_generada,
        l.dinero_entregado,
        l.diferencia,
        l.fecha_registro
    FROM tbl_liquidaciones_ruta l
    INNER JOIN tbl_salidas_ruta s ON l.id_salida   = s.id_salida
    INNER JOIN tbl_vendedores   v ON s.id_vendedor = v.id_vendedor
    INNER JOIN tbl_rutas        r ON s.id_ruta     = r.id_ruta
    WHERE l.id_validado_por IS NULL
    ORDER BY l.fecha_registro DESC;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_listar_movimientos_caja` (IN `p_id_caja` INT)   BEGIN
    SELECT
        id_movimiento_caja,
        id_caja,
        id_usuario,
        tipo,
        concepto,
        monto,
        fecha_registro
    FROM tbl_movimientos_caja
    WHERE id_caja = p_id_caja
    ORDER BY fecha_registro DESC;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_listar_movimientos_inventario_pendientes` ()   BEGIN
    SELECT
        m.id_movimiento,
        pr.nombre AS producto,
        m.cantidad,
        m.observacion,
        u.nombre AS solicitado_por,
        m.fecha_registro
    FROM tbl_movimientos_inventario m
    INNER JOIN tbl_productos pr ON m.id_producto = pr.id_producto
    LEFT JOIN tbl_usuarios u ON m.id_usuario = u.id_usuario
    WHERE m.estado = 'pendiente' AND m.tipo = 'ajuste'
    ORDER BY m.fecha_registro ASC;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_listar_notificaciones_no_leidas` ()   BEGIN
    SELECT
        id_notificacion,
        tipo,
        referencia_id,
        mensaje,
        enviado,
        leido,
        fecha_registro
    FROM tbl_notificacion
    WHERE leido = 0
    ORDER BY fecha_registro DESC;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_listar_notificaciones_pendientes` ()   BEGIN
    SELECT
        id_notificacion,
        tipo,
        referencia_id,
        mensaje,
        enviado,
        leido,
        fecha_registro
    FROM tbl_notificacion
    WHERE enviado = 0
    ORDER BY fecha_registro;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_listar_pedidos_mayoristas` ()   BEGIN

    SELECT
        p.id_pedido,
        c.nombre_comercial,
        p.fecha_pedido,
        p.codigo_retiro,
        p.estado
    FROM tbl_pedidos_mayoristas p
    INNER JOIN tbl_clientes_mayoristas c
        ON p.id_cliente = c.id_cliente
    ORDER BY p.fecha_pedido DESC;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_listar_pedidos_mayoristas_cliente` (IN `p_id_cliente` INT)   BEGIN
    SELECT
        p.id_pedido,
        c.nombre_comercial,
        p.fecha_pedido,
        p.codigo_retiro,
        p.estado,
        p.total
    FROM tbl_pedidos_mayoristas p
    INNER JOIN tbl_clientes_mayoristas c ON p.id_cliente = c.id_cliente
    WHERE p.id_cliente = p_id_cliente
    ORDER BY p.fecha_pedido DESC;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_listar_productos` ()   BEGIN
    SELECT
        p.id_producto,
        p.codigo,
        p.codigo_barras,
        p.nombre,
        c.nombre AS nombre_categoria,
        p.presentacion,
        p.precio_compra,
        p.precio_venta,
        p.stock_actual,
        p.stock_minimo,
        p.fecha_vencimiento,
        p.imagen_ruta,
        p.activo
    FROM tbl_productos p
    INNER JOIN tbl_categorias c ON p.id_categoria = c.id_categoria
    ORDER BY p.nombre;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_listar_promociones` ()   BEGIN
    SELECT
        p.id_promocion,
        p.id_producto,
        p.id_categoria,
        p.nombre,
        p.descripcion,
        p.tipo_descuento,
        p.valor_descuento,
        p.fecha_inicio,
        p.fecha_fin,
        p.activo,
        p.fecha_registro,
        p.estado,
        pr.nombre AS producto,
        c.nombre  AS categoria
    FROM tbl_promociones p
    LEFT JOIN tbl_productos  pr ON p.id_producto  = pr.id_producto
    LEFT JOIN tbl_categorias c  ON p.id_categoria = c.id_categoria
    ORDER BY p.fecha_registro DESC;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_listar_promociones_pendientes` ()   BEGIN
    SELECT
        p.id_promocion,
        p.nombre,
        COALESCE(pr.nombre, c.nombre, 'ÔÇö') AS objetivo,
        p.tipo_descuento,
        p.valor_descuento,
        p.fecha_inicio,
        p.fecha_fin
    FROM tbl_promociones p
    LEFT JOIN tbl_productos  pr ON p.id_producto  = pr.id_producto
    LEFT JOIN tbl_categorias c  ON p.id_categoria = c.id_categoria
    WHERE p.estado = 'pendiente'
    ORDER BY p.fecha_registro DESC;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_listar_roles` ()   BEGIN
	SELECT id_rol, nombre, descripcion, activo
    FROM tbl_roles
    WHERE activo = 1
    ORDER BY nombre;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_listar_rutas` ()   BEGIN
    SELECT
        id_ruta,
        nombre,
        zona,
        id_responsable,
        horario_inicio,
        horario_fin,
        activo
    FROM tbl_rutas
    ORDER BY nombre;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_listar_salidas_ruta` ()   BEGIN
    SELECT
        s.id_salida,
        v.nombre        AS vendedor,
        r.nombre        AS ruta,
        s.fecha,
        s.hora_salida,
        s.vehiculo,
        s.comision,
        s.estado
    FROM tbl_salidas_ruta s
    INNER JOIN tbl_vendedores v ON s.id_vendedor = v.id_vendedor
    INNER JOIN tbl_rutas      r ON s.id_ruta     = r.id_ruta
    ORDER BY s.fecha DESC, s.hora_salida DESC;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_listar_usuarios` ()   BEGIN
    SELECT
        u.id_usuario,
        u.usuario,
        u.nombre,
        u.correo,
        r.nombre AS rol,
        u.id_rol,
        u.activo
    FROM tbl_usuarios u
    INNER JOIN tbl_roles r ON u.id_rol = r.id_rol
    ORDER BY u.nombre;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_listar_usuarios_activos` ()   BEGIN
    SELECT
        u.id_usuario,
        u.usuario,
        u.nombre,
        u.correo,
        r.nombre AS nombre_rol
    FROM tbl_usuarios u
    INNER JOIN tbl_roles r
        ON u.id_rol = r.id_rol
    WHERE u.activo = 1;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_listar_vendedores` ()   BEGIN
    SELECT
        v.id_vendedor,
        v.codigo_empleado,
        v.nombre,
        v.dui,
        v.telefono,
        v.estado
    FROM tbl_vendedores v
    ORDER BY v.nombre;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_listar_ventas_sucursal` ()   BEGIN
    SELECT
        v.id_venta,
        v.fecha_registro,
        u.nombre AS cajero,
        v.subtotal,
        v.descuento,
        v.total,
        v.activo
    FROM tbl_ventas_sucursal v
    LEFT JOIN tbl_usuarios u
        ON v.id_cajero = u.id_usuario
    ORDER BY v.fecha_registro DESC;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_login_usuario` (IN `p_usuario` VARCHAR(60))   BEGIN
    SELECT
        id_usuario,
        id_rol,
        nombre,
        usuario,
        `contraseña_hash`,
        `contraseña_salt`,
        correo,
        activo,
        `contraseña_temp_hash`,
        `contraseña_temp_salt`,
        `contraseña_temp_expira`
    FROM tbl_usuarios
    WHERE usuario = p_usuario;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_marcar_notificacion_enviada` (IN `p_id_notificacion` INT)   BEGIN
    UPDATE tbl_notificacion
    SET enviado = 1
    WHERE id_notificacion = p_id_notificacion;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_marcar_notificacion_leida` (IN `p_id_notificacion` INT)   BEGIN
    UPDATE tbl_notificacion
    SET leido = 1
    WHERE id_notificacion = p_id_notificacion;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_obtener_pedido_comprobante` (IN `p_id_pedido` INT)   BEGIN
    SELECT
        p.codigo_pedido,
        p.codigo_retiro,
        p.total,
        c.nombre_comercial,
        c.correo,
        p.fecha_pedido
    FROM tbl_pedidos_mayoristas p
    INNER JOIN tbl_clientes_mayoristas c
        ON p.id_cliente = c.id_cliente
    WHERE p.id_pedido = p_id_pedido;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_obtener_usuario_por_correo` (IN `p_correo` VARCHAR(150))   BEGIN
    SELECT
        id_usuario,
        id_rol,
        nombre,
        usuario,
        `contraseña_hash`,
        `contraseña_salt`,
        correo,
        activo
    FROM tbl_usuarios
    WHERE correo = p_correo
    LIMIT 1;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_productos_bajo_stock` ()   BEGIN
    SELECT
        id_producto,
        id_categoria,
        codigo,
        codigo_barras,
        nombre,
        presentacion,
        precio_compra,
        precio_venta,
        stock_actual,
        stock_minimo,
        fecha_ingreso,
        fecha_vencimiento,
        nivel_alerta_vencimiento,
        activo,
        fecha_registro
    FROM tbl_productos
    WHERE stock_actual <= stock_minimo;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_productos_por_vencer` (IN `p_dias` INT)   BEGIN
    SELECT
        id_producto,
        id_categoria,
        codigo,
        codigo_barras,
        nombre,
        presentacion,
        precio_compra,
        precio_venta,
        stock_actual,
        stock_minimo,
        fecha_ingreso,
        fecha_vencimiento,
        nivel_alerta_vencimiento,
        activo,
        fecha_registro
    FROM tbl_productos
    WHERE fecha_vencimiento
          BETWEEN CURDATE()
          AND DATE_ADD(CURDATE(), INTERVAL p_dias DAY);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_rechazar_ajuste_inventario` (IN `p_id_movimiento` INT, IN `p_id_aprobado_por` INT)   BEGIN
    UPDATE tbl_movimientos_inventario
    SET estado = 'rechazado', id_aprobado_por = p_id_aprobado_por
    WHERE id_movimiento = p_id_movimiento AND estado = 'pendiente';
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_rechazar_promocion` (IN `p_id_promocion` INT, IN `p_id_aprobado_por` INT)   BEGIN
    UPDATE tbl_promociones
    SET estado = 'rechazada', id_aprobado_por = p_id_aprobado_por, activo = 0
    WHERE id_promocion = p_id_promocion;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_registrar_carga_producto_ruta` (IN `p_id_salida` INT, IN `p_id_producto` INT, IN `p_cantidad_carga` INT, IN `p_id_usuario` INT)   BEGIN
    DECLARE v_stock INT;
    DECLARE v_precio_venta DECIMAL(10,2);

    SELECT stock_actual, precio_venta
    INTO v_stock, v_precio_venta
    FROM tbl_productos
    WHERE id_producto = p_id_producto;

    IF v_stock < p_cantidad_carga THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Stock insuficiente para cargar producto a la ruta';
    ELSE

        INSERT INTO tbl_detalles_salida_ruta(
            id_salida,
            id_producto,
            cantidad_carga,
            precio_venta
        )
        VALUES(
            p_id_salida,
            p_id_producto,
            p_cantidad_carga,
            v_precio_venta
        );

        UPDATE tbl_productos
        SET stock_actual = stock_actual - p_cantidad_carga
        WHERE id_producto = p_id_producto;

        INSERT INTO tbl_movimientos_inventario(
            id_producto,
            id_usuario,
            tipo,
            cantidad,
            referencia,
            observacion
        )
        VALUES(
            p_id_producto,
            p_id_usuario,
            'salida',
            p_cantidad_carga,
            CONCAT('Salida a ruta #', p_id_salida),
            'Carga entregada a vendedor ambulante'
        );

    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_registrar_descuento_venta` (IN `p_id_venta` INT, IN `p_descuento` DECIMAL(10,2), IN `p_id_autorizado_por` INT)   BEGIN
    UPDATE tbl_ventas_sucursal
    SET descuento = p_descuento,
        total = subtotal - p_descuento,
        id_autorizado_descuento = p_id_autorizado_por
    WHERE id_venta = p_id_venta;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_registrar_movimiento_inventario` (IN `p_id_producto` INT, IN `p_tipo` VARCHAR(20), IN `p_cantidad` INT, IN `p_observacion` VARCHAR(255))   BEGIN

    INSERT INTO tbl_movimientos_inventario(
        id_producto,
        tipo,
        cantidad,
        observacion
    )
    VALUES(
        p_id_producto,
        p_tipo,
        p_cantidad,
        p_observacion
    );

    IF p_tipo = 'entrada' THEN

        UPDATE tbl_productos
        SET stock_actual = stock_actual + p_cantidad
        WHERE id_producto = p_id_producto;

    ELSE

        UPDATE tbl_productos
        SET stock_actual = stock_actual - p_cantidad
        WHERE id_producto = p_id_producto;

    END IF;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_registrar_salida_ruta` (IN `p_id_vendedor` INT, IN `p_id_ruta` INT, IN `p_id_usuario` INT, IN `p_fecha` DATE, IN `p_hora_salida` TIME, IN `p_vehiculo` VARCHAR(100), IN `p_comision` DECIMAL(5,2), OUT `p_id_salida` INT)   BEGIN
    INSERT INTO tbl_salidas_ruta(
        id_vendedor,
        id_ruta,
        id_usuario,
        fecha,
        hora_salida,
        vehiculo,
        comision,
        estado
    )
    VALUES(
        p_id_vendedor,
        p_id_ruta,
        p_id_usuario,
        p_fecha,
        p_hora_salida,
        p_vehiculo,
        p_comision,
        'abierta'
    );

    SET p_id_salida = LAST_INSERT_ID();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_registrar_venta_sucursal` (IN `p_id_caja` INT, IN `p_id_cajero` INT, IN `p_descuento` DECIMAL(10,2), OUT `p_id_venta` INT)   BEGIN
    INSERT INTO tbl_ventas_sucursal(
        id_caja,
        id_cajero,
        subtotal,
        descuento,
        total,
        activo
    )
    VALUES(
        p_id_caja,
        p_id_cajero,
        0,
        p_descuento,
        0,
        1
    );

    SET p_id_venta = LAST_INSERT_ID();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_reporte_caja` (IN `p_fecha_inicio` DATE, IN `p_fecha_fin` DATE)   BEGIN
    SELECT
        c.id_caja,
        u.nombre AS cajero,
        c.fecha_apertura,
        c.fecha_cierre,
        c.fondo_inicial,
        c.total_ventas,
        c.monto_contado,
        c.diferencia,
        c.estado
    FROM tbl_cajas c
    INNER JOIN tbl_usuarios u
        ON c.id_cajero = u.id_usuario
    WHERE DATE(c.fecha_apertura) BETWEEN p_fecha_inicio AND p_fecha_fin
    ORDER BY c.fecha_apertura DESC;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_reporte_comisiones_vendedores` (IN `p_fecha_inicio` DATE, IN `p_fecha_fin` DATE)   BEGIN
    SELECT
        v.id_vendedor,
        v.nombre AS vendedor,
        COUNT(lr.id_liquidacion) AS total_liquidaciones,
        SUM(lr.total_vendido) AS total_vendido,
        SUM(lr.comision_generada) AS total_comision
    FROM tbl_liquidaciones_ruta lr
    INNER JOIN tbl_salidas_ruta sr
        ON lr.id_salida = sr.id_salida
    INNER JOIN tbl_vendedores v
        ON sr.id_vendedor = v.id_vendedor
    WHERE sr.fecha BETWEEN p_fecha_inicio AND p_fecha_fin
    GROUP BY v.id_vendedor, v.nombre
    ORDER BY total_comision DESC;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_reporte_productos_danados` (IN `p_fecha_inicio` DATE, IN `p_fecha_fin` DATE)   BEGIN
    SELECT
        p.nombre AS producto,
        SUM(m.cantidad) AS cantidad_danada,
        m.observacion
    FROM tbl_movimientos_inventario m
    INNER JOIN tbl_productos p
        ON m.id_producto = p.id_producto
    WHERE m.tipo = 'dañado'
      AND DATE(m.fecha_registro) BETWEEN p_fecha_inicio AND p_fecha_fin
    GROUP BY p.nombre, m.observacion
    ORDER BY cantidad_danada DESC;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_reporte_productos_mas_vendidos` (IN `p_fecha_inicio` DATE, IN `p_fecha_fin` DATE)   BEGIN
    SELECT
        producto,
        SUM(cantidad_vendida) AS cantidad_total,
        SUM(total_vendido) AS monto_total
    FROM
    (
        SELECT
            p.nombre AS producto,
            SUM(d.cantidad) AS cantidad_vendida,
            SUM(d.subtotal) AS total_vendido
        FROM tbl_detalles_venta_sucursal d
        INNER JOIN tbl_ventas_sucursal v
            ON d.id_venta = v.id_venta
        INNER JOIN tbl_productos p
            ON d.id_producto = p.id_producto
        WHERE DATE(v.fecha_registro) BETWEEN p_fecha_inicio AND p_fecha_fin
          AND v.activo = 1
        GROUP BY p.nombre

        UNION ALL

        SELECT
            p.nombre AS producto,
            SUM(dl.cantidad_vendida) AS cantidad_vendida,
            SUM(dl.subtotal) AS total_vendido
        FROM tbl_detalles_liquidacion dl
        INNER JOIN tbl_liquidaciones_ruta lr
            ON dl.id_liquidacion = lr.id_liquidacion
        INNER JOIN tbl_salidas_ruta sr
            ON lr.id_salida = sr.id_salida
        INNER JOIN tbl_productos p
            ON dl.id_producto = p.id_producto
        WHERE sr.fecha BETWEEN p_fecha_inicio AND p_fecha_fin
        GROUP BY p.nombre
    ) AS ventas
    GROUP BY producto
    ORDER BY cantidad_total DESC;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_reporte_ranking_vendedores` (IN `p_fecha_inicio` DATE, IN `p_fecha_fin` DATE)   BEGIN
    SELECT
        v.nombre AS vendedor,
        SUM(lr.total_vendido) AS total_vendido,
        COALESCE(u.unidades, 0) AS unidades_vendidas,
        SUM(lr.comision_generada) AS comision_generada
    FROM tbl_liquidaciones_ruta lr
    INNER JOIN tbl_salidas_ruta sr ON lr.id_salida = sr.id_salida
    INNER JOIN tbl_vendedores v ON sr.id_vendedor = v.id_vendedor
    LEFT JOIN (
        SELECT sr2.id_vendedor, SUM(dl.cantidad_vendida) AS unidades
        FROM tbl_detalles_liquidacion dl
        INNER JOIN tbl_liquidaciones_ruta lr2 ON dl.id_liquidacion = lr2.id_liquidacion
        INNER JOIN tbl_salidas_ruta sr2 ON lr2.id_salida = sr2.id_salida
        WHERE sr2.fecha BETWEEN p_fecha_inicio AND p_fecha_fin
        GROUP BY sr2.id_vendedor
    ) u ON u.id_vendedor = v.id_vendedor
    WHERE sr.fecha BETWEEN p_fecha_inicio AND p_fecha_fin
    GROUP BY v.id_vendedor, v.nombre, u.unidades
    ORDER BY total_vendido DESC;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_reporte_rentabilidad_ruta` (IN `p_fecha_inicio` DATE, IN `p_fecha_fin` DATE)   BEGIN
    SELECT
        r.nombre AS nombre_ruta,
        r.zona,
        SUM(lr.total_vendido) AS total_vendido,
        COALESCE(co.costo_total, 0) AS costo_total,
        SUM(lr.comision_generada) AS comision_generada,
        (SUM(lr.total_vendido) - COALESCE(co.costo_total, 0) - SUM(lr.comision_generada)) AS rentabilidad
    FROM tbl_liquidaciones_ruta lr
    INNER JOIN tbl_salidas_ruta sr ON lr.id_salida = sr.id_salida
    INNER JOIN tbl_rutas r ON sr.id_ruta = r.id_ruta
    LEFT JOIN (
        SELECT sr2.id_ruta, SUM(dl.cantidad_vendida * pr.precio_compra) AS costo_total
        FROM tbl_detalles_liquidacion dl
        INNER JOIN tbl_liquidaciones_ruta lr2 ON dl.id_liquidacion = lr2.id_liquidacion
        INNER JOIN tbl_salidas_ruta sr2 ON lr2.id_salida = sr2.id_salida
        INNER JOIN tbl_productos pr ON dl.id_producto = pr.id_producto
        WHERE sr2.fecha BETWEEN p_fecha_inicio AND p_fecha_fin
        GROUP BY sr2.id_ruta
    ) co ON co.id_ruta = r.id_ruta
    WHERE sr.fecha BETWEEN p_fecha_inicio AND p_fecha_fin
    GROUP BY r.id_ruta, r.nombre, r.zona, co.costo_total
    ORDER BY rentabilidad DESC;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_reporte_ventas_dia` (IN `p_fecha` DATE)   BEGIN
    SELECT
        v.id_venta,
        v.fecha_registro,
        u.nombre AS cajero,
        v.subtotal,
        v.descuento,
        v.total,
        v.activo
    FROM tbl_ventas_sucursal v
    INNER JOIN tbl_usuarios u
        ON v.id_cajero = u.id_usuario
    WHERE DATE(v.fecha_registro) = p_fecha
    ORDER BY v.fecha_registro DESC;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_reporte_ventas_por_ruta` (IN `p_fecha_inicio` DATE, IN `p_fecha_fin` DATE)   BEGIN
    SELECT
        r.nombre AS nombre_ruta,
        r.zona,
        v.nombre AS vendedor,
        sr.fecha,
        lr.total_vendido,
        lr.comision_generada,
        lr.dinero_entregado
    FROM tbl_liquidaciones_ruta lr
    INNER JOIN tbl_salidas_ruta sr
        ON lr.id_salida = sr.id_salida
    INNER JOIN tbl_rutas r
        ON sr.id_ruta = r.id_ruta
    INNER JOIN tbl_vendedores v
        ON sr.id_vendedor = v.id_vendedor
    WHERE sr.fecha BETWEEN p_fecha_inicio AND p_fecha_fin
    ORDER BY sr.fecha DESC;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_solicitar_ajuste_inventario` (IN `p_id_producto` INT, IN `p_cantidad` INT, IN `p_observacion` TEXT, IN `p_id_usuario` INT, OUT `p_id_movimiento` INT)   BEGIN
    INSERT INTO tbl_movimientos_inventario(
        id_producto, id_usuario, tipo, cantidad, observacion, estado)
    VALUES(
        p_id_producto, p_id_usuario, 'ajuste', p_cantidad, p_observacion, 'pendiente');

    SET p_id_movimiento = LAST_INSERT_ID();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `p_validar_liquidacion` (IN `p_id_liquidacion` INT, IN `p_id_validado_por` INT)   BEGIN
    UPDATE tbl_liquidaciones_ruta
    SET id_validado_por = p_id_validado_por
    WHERE id_liquidacion = p_id_liquidacion;

    UPDATE tbl_salidas_ruta sr
    INNER JOIN tbl_liquidaciones_ruta lr
        ON sr.id_salida = lr.id_salida
    SET sr.estado = 'liquidada'
    WHERE lr.id_liquidacion = p_id_liquidacion;
END$$

--
-- Funciones
--
CREATE DEFINER=`root`@`localhost` FUNCTION `fn_calcular_comision` (`p_total_vendido` DECIMAL(10,2), `p_porcentaje` DECIMAL(5,2)) RETURNS DECIMAL(10,2) DETERMINISTIC BEGIN
    RETURN p_total_vendido * (p_porcentaje / 100);
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `fn_dias_para_vencer` (`p_fecha_vencimiento` DATE) RETURNS INT(11) DETERMINISTIC BEGIN
    RETURN DATEDIFF(p_fecha_vencimiento, CURDATE());
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tbl_arqueos_caja`
--

CREATE TABLE `tbl_arqueos_caja` (
  `id_arqueo` int(11) NOT NULL,
  `id_caja` int(11) NOT NULL,
  `id_realizado_por` int(11) DEFAULT NULL,
  `monto_esperado` decimal(10,2) NOT NULL,
  `monto_contado` decimal(10,2) NOT NULL,
  `diferencia` decimal(10,2) GENERATED ALWAYS AS (`monto_contado` - `monto_esperado`) STORED,
  `fecha_registro` datetime NOT NULL DEFAULT current_timestamp(),
  `autorizado` tinyint(1) NOT NULL DEFAULT 0,
  `id_autorizado_por` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tbl_arqueos_caja`
--

INSERT INTO `tbl_arqueos_caja` (`id_arqueo`, `id_caja`, `id_realizado_por`, `monto_esperado`, `monto_contado`, `fecha_registro`, `autorizado`, `id_autorizado_por`) VALUES
(1, 3, 13, 100.00, 2.00, '2026-06-23 20:33:43', 1, 13),
(2, 3, 13, 100.00, 100.00, '2026-06-23 20:40:37', 1, 13);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tbl_cajas`
--

CREATE TABLE `tbl_cajas` (
  `id_caja` int(11) NOT NULL,
  `id_cajero` int(11) NOT NULL,
  `fecha_apertura` datetime NOT NULL DEFAULT current_timestamp(),
  `fondo_inicial` decimal(10,2) NOT NULL DEFAULT 0.00,
  `fecha_cierre` datetime DEFAULT NULL,
  `total_ventas` decimal(10,2) DEFAULT NULL,
  `monto_contado` decimal(10,2) DEFAULT NULL,
  `diferencia` decimal(10,2) DEFAULT NULL,
  `estado` enum('abierta','cerrada') NOT NULL DEFAULT 'abierta'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tbl_cajas`
--

INSERT INTO `tbl_cajas` (`id_caja`, `id_cajero`, `fecha_apertura`, `fondo_inicial`, `fecha_cierre`, `total_ventas`, `monto_contado`, `diferencia`, `estado`) VALUES
(1, 13, '2026-06-23 12:16:42', 50.00, '2026-06-23 12:17:32', 0.00, 25.00, 25.00, 'cerrada'),
(2, 13, '2026-06-23 20:32:41', 100.00, '2026-06-23 20:33:23', 19.20, 21.70, 2.50, 'cerrada'),
(3, 13, '2026-06-23 20:33:35', 100.00, '2026-06-23 20:40:39', 0.00, 100.00, 100.00, 'cerrada'),
(4, 13, '2026-06-24 18:27:08', 100.00, '2026-06-24 18:28:01', 0.00, 100.00, 100.00, 'cerrada');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tbl_categorias`
--

CREATE TABLE `tbl_categorias` (
  `id_categoria` int(11) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `descripcion` varchar(255) DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tbl_categorias`
--

INSERT INTO `tbl_categorias` (`id_categoria`, `nombre`, `descripcion`, `activo`) VALUES
(1, 'Helados de paleta', NULL, 1),
(2, 'Helados de vaso', NULL, 1),
(3, 'Conos', NULL, 1),
(4, 'Nieves', NULL, 1),
(5, 'Postres congelados', NULL, 1),
(6, 'Bebidas frias', NULL, 1),
(7, 'Litros familiares', NULL, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tbl_clientes_mayoristas`
--

CREATE TABLE `tbl_clientes_mayoristas` (
  `id_cliente` int(11) NOT NULL,
  `nombre_comercial` varchar(150) NOT NULL,
  `nit` varchar(20) DEFAULT NULL,
  `encargado` varchar(150) DEFAULT NULL,
  `direccion` varchar(255) DEFAULT NULL,
  `telefono` varchar(20) DEFAULT NULL,
  `correo` varchar(150) DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT 1,
  `fecha_registro` datetime NOT NULL DEFAULT current_timestamp(),
  `descuento_porcentaje` decimal(5,2) NOT NULL DEFAULT 0.00
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tbl_detalles_liquidacion`
--

CREATE TABLE `tbl_detalles_liquidacion` (
  `id_detalle_liquidacion` int(11) NOT NULL,
  `id_liquidacion` int(11) NOT NULL,
  `id_producto` int(11) NOT NULL,
  `cantidad_carga` int(11) NOT NULL,
  `cantidad_vendida` int(11) NOT NULL DEFAULT 0,
  `cantidad_devuelta` int(11) NOT NULL DEFAULT 0,
  `cantidad_dañada` int(11) NOT NULL DEFAULT 0,
  `precio_venta` decimal(10,2) NOT NULL,
  `subtotal` decimal(10,2) GENERATED ALWAYS AS (`cantidad_vendida` * `precio_venta`) STORED
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tbl_detalles_pedido_mayorista`
--

CREATE TABLE `tbl_detalles_pedido_mayorista` (
  `id_detalle_pedido` int(11) NOT NULL,
  `id_pedido` int(11) NOT NULL,
  `id_producto` int(11) NOT NULL,
  `cantidad` int(11) NOT NULL,
  `precio_unitario` decimal(10,2) NOT NULL,
  `descuento` decimal(10,2) NOT NULL DEFAULT 0.00,
  `subtotal` decimal(10,2) GENERATED ALWAYS AS (`cantidad` * `precio_unitario` - `descuento`) STORED
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tbl_detalles_salida_ruta`
--

CREATE TABLE `tbl_detalles_salida_ruta` (
  `id_detalle_salida` int(11) NOT NULL,
  `id_salida` int(11) NOT NULL,
  `id_producto` int(11) NOT NULL,
  `cantidad_carga` int(11) NOT NULL,
  `precio_venta` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tbl_detalles_venta_sucursal`
--

CREATE TABLE `tbl_detalles_venta_sucursal` (
  `id_detalle_venta` int(11) NOT NULL,
  `id_venta` int(11) NOT NULL,
  `id_producto` int(11) NOT NULL,
  `cantidad` int(11) NOT NULL,
  `precio_unitario` decimal(10,2) NOT NULL,
  `descuento` decimal(10,2) NOT NULL DEFAULT 0.00,
  `subtotal` decimal(10,2) GENERATED ALWAYS AS (`cantidad` * `precio_unitario` - `descuento`) STORED
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tbl_liquidaciones_ruta`
--

CREATE TABLE `tbl_liquidaciones_ruta` (
  `id_liquidacion` int(11) NOT NULL,
  `id_salida` int(11) NOT NULL,
  `id_validado_por` int(11) DEFAULT NULL,
  `hora_regreso` time DEFAULT NULL,
  `total_vendido` decimal(10,2) NOT NULL DEFAULT 0.00,
  `comision_generada` decimal(10,2) NOT NULL DEFAULT 0.00,
  `dinero_entregado` decimal(10,2) NOT NULL DEFAULT 0.00,
  `diferencia` decimal(10,2) GENERATED ALWAYS AS (`dinero_entregado` - (`total_vendido` - `comision_generada`)) STORED,
  `observacion` text DEFAULT NULL,
  `fecha_registro` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tbl_movimientos_caja`
--

CREATE TABLE `tbl_movimientos_caja` (
  `id_movimiento_caja` int(11) NOT NULL,
  `id_caja` int(11) NOT NULL,
  `id_usuario` int(11) DEFAULT NULL,
  `tipo` enum('ingreso','egreso') NOT NULL,
  `concepto` varchar(200) NOT NULL,
  `monto` decimal(10,2) NOT NULL,
  `fecha_registro` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tbl_movimientos_caja`
--

INSERT INTO `tbl_movimientos_caja` (`id_movimiento_caja`, `id_caja`, `id_usuario`, `tipo`, `concepto`, `monto`, `fecha_registro`) VALUES
(1, 4, 13, 'ingreso', 'venta de paquete de 12 conos', 3.50, '2026-06-24 18:27:51');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tbl_movimientos_inventario`
--

CREATE TABLE `tbl_movimientos_inventario` (
  `id_movimiento` int(11) NOT NULL,
  `id_producto` int(11) NOT NULL,
  `id_usuario` int(11) DEFAULT NULL,
  `tipo` enum('entrada','salida','ajuste','devolucion','dañado','vencido') NOT NULL,
  `cantidad` int(11) NOT NULL,
  `costo_unitario` decimal(10,2) DEFAULT NULL,
  `referencia` varchar(100) DEFAULT NULL,
  `observacion` text DEFAULT NULL,
  `fecha_registro` datetime NOT NULL DEFAULT current_timestamp(),
  `estado` enum('aplicado','pendiente','rechazado') NOT NULL DEFAULT 'aplicado',
  `id_aprobado_por` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tbl_notificacion`
--

CREATE TABLE `tbl_notificacion` (
  `id_notificacion` int(11) NOT NULL,
  `tipo` enum('bajo_stock','prox_vencer','diferencia_liq','pedido_listo','arqueo_inconsistente') NOT NULL,
  `referencia_id` int(11) DEFAULT NULL,
  `mensaje` text NOT NULL,
  `enviado` tinyint(1) NOT NULL DEFAULT 0,
  `leido` tinyint(1) NOT NULL DEFAULT 0,
  `fecha_registro` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tbl_notificacion`
--

INSERT INTO `tbl_notificacion` (`id_notificacion`, `tipo`, `referencia_id`, `mensaje`, `enviado`, `leido`, `fecha_registro`) VALUES
(1, 'arqueo_inconsistente', 3, 'El arqueo de la caja #3 presenta una diferencia de -98.00.', 1, 1, '2026-06-23 20:33:43'),
(2, 'prox_vencer', 2, 'El producto \"ASDADASDASD\" vence el 2026-06-24.', 1, 1, '2026-06-24 08:06:30');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tbl_pedidos_mayoristas`
--

CREATE TABLE `tbl_pedidos_mayoristas` (
  `id_pedido` int(11) NOT NULL,
  `id_cliente` int(11) NOT NULL,
  `id_atendido_por` int(11) DEFAULT NULL,
  `id_entregado_por` int(11) DEFAULT NULL,
  `codigo_pedido` varchar(30) NOT NULL,
  `codigo_retiro` varchar(20) DEFAULT NULL,
  `estado` enum('pendiente','confirmado','pagado','listo','entregado','cancelado') NOT NULL DEFAULT 'pendiente',
  `subtotal` decimal(10,2) NOT NULL DEFAULT 0.00,
  `descuento` decimal(10,2) NOT NULL DEFAULT 0.00,
  `total` decimal(10,2) NOT NULL DEFAULT 0.00,
  `fecha_pedido` datetime NOT NULL DEFAULT current_timestamp(),
  `fecha_confirmacion` datetime DEFAULT NULL,
  `fecha_entrega` datetime DEFAULT NULL,
  `fecha_entrega_programada` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tbl_productos`
--

CREATE TABLE `tbl_productos` (
  `id_producto` int(11) NOT NULL,
  `id_categoria` int(11) NOT NULL,
  `codigo` varchar(30) NOT NULL,
  `codigo_barras` varchar(50) DEFAULT NULL,
  `nombre` varchar(150) NOT NULL,
  `presentacion` varchar(80) DEFAULT NULL,
  `precio_compra` decimal(10,2) NOT NULL DEFAULT 0.00,
  `precio_venta` decimal(10,2) NOT NULL DEFAULT 0.00,
  `stock_actual` int(11) NOT NULL DEFAULT 0,
  `stock_minimo` int(11) NOT NULL DEFAULT 0,
  `fecha_ingreso` date DEFAULT NULL,
  `fecha_vencimiento` date DEFAULT NULL,
  `nivel_alerta_vencimiento` tinyint(4) NOT NULL DEFAULT 0,
  `activo` tinyint(1) NOT NULL DEFAULT 1,
  `fecha_registro` datetime NOT NULL DEFAULT current_timestamp(),
  `imagen_ruta` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tbl_productos`
--

INSERT INTO `tbl_productos` (`id_producto`, `id_categoria`, `codigo`, `codigo_barras`, `nombre`, `presentacion`, `precio_compra`, `precio_venta`, `stock_actual`, `stock_minimo`, `fecha_ingreso`, `fecha_vencimiento`, `nivel_alerta_vencimiento`, `activo`, `fecha_registro`, `imagen_ruta`) VALUES
(3, 6, 'P001', '2260625123648', 'Te frio limon', 'unidad', 1.00, 1.50, 300, 75, '2026-06-25', '2026-12-25', 0, 1, '2026-06-25 12:36:48', 'Imagenes/Productos/descarga.jpg'),
(4, 6, 'P002', '2260625123947', 'Cafe frio', 'unidad', 1.50, 2.00, 240, 70, '2026-06-25', '2026-09-01', 0, 1, '2026-06-25 12:39:47', 'Imagenes/Productos/descarga (1).jpg'),
(5, 3, 'P003', '2260625124156', 'Cono vainilla', 'unidad', 1.00, 1.50, 750, 180, '2026-06-25', '2026-08-01', 0, 1, '2026-06-25 12:41:56', 'Imagenes/Productos/descarga (2).jpg'),
(6, 3, 'P004', '2260625124631', 'Cono chocolate', 'unidad', 1.00, 1.50, 680, 165, '2026-06-25', '2026-08-01', 0, 1, '2026-06-25 12:46:31', 'Imagenes/Productos/descarga.png'),
(7, 3, 'P005', '2260625130652', 'Cono mixto', 'unidad', 0.85, 1.30, 420, 135, '2026-06-25', '2026-08-01', 0, 1, '2026-06-25 13:06:52', 'Imagenes/Productos/descarga (3).jpg'),
(8, 3, 'P006', '2260625131007', 'Cono fresa', 'unidad', 0.75, 1.25, 420, 135, '2026-06-25', '2026-08-01', 0, 1, '2026-06-25 13:10:07', 'Imagenes/Productos/descarga (1).png'),
(9, 1, 'P007', '2260625131414', 'Paleta fresa', 'unidad', 0.60, 1.20, 900, 325, '2026-06-25', '2026-11-01', 0, 1, '2026-06-25 13:14:14', 'Imagenes/Productos/descarga (2).png'),
(10, 1, 'P008', '2260625131722', 'Paleta mango', 'unidad', 0.60, 1.20, 800, 210, '2026-06-25', '2026-11-01', 0, 1, '2026-06-25 13:17:22', 'Imagenes/Productos/descarga (4).jpg'),
(11, 1, 'P009', '2260625132036', 'Paleta chocolate', 'unidad', 0.75, 1.25, 750, 180, '2026-06-25', '2026-11-01', 0, 1, '2026-06-25 13:20:36', 'Imagenes/Productos/images.jpg'),
(12, 1, 'P010', '2260625132228', 'Paleta coco', 'unidad', 0.75, 1.25, 600, 150, '2026-06-25', '2026-11-01', 0, 1, '2026-06-25 13:22:28', 'Imagenes/Productos/paleta coco.jpg'),
(13, 1, 'P011', '2260625132508', 'Paleta limon', 'unidad', 0.50, 0.80, 900, 225, '2026-06-25', '2026-11-25', 0, 1, '2026-06-25 13:25:08', 'Imagenes/Productos/paleta limom.png'),
(14, 7, 'P012', '2260625132720', 'Litro vainilla', 'litro', 5.50, 6.50, 240, 60, '2026-06-25', '2026-11-01', 0, 1, '2026-06-25 13:27:20', 'Imagenes/Productos/litro de vainilla.jpg'),
(15, 7, 'P013', '2260625132918', 'Litro chocolate', 'litro', 6.00, 7.00, 210, 70, '2026-06-25', '2026-10-25', 0, 1, '2026-06-25 13:29:18', 'Imagenes/Productos/litro de chocolate.jpg'),
(16, 7, 'P014', '2260625133204', 'Litro fresa', 'litro', 5.75, 6.75, 210, 70, '2026-06-25', '2026-11-01', 0, 1, '2026-06-25 13:32:04', 'Imagenes/Productos/litro fresa.jpg'),
(17, 7, 'P015', '2260625133318', 'Litro napolitano', '', 6.50, 7.50, 180, 70, '2026-06-25', '2026-11-01', 0, 1, '2026-06-25 13:33:18', 'Imagenes/Productos/litro de napolitano.png'),
(18, 4, 'P016', '2260625133731', 'Nieve limon', 'unidad', 1.25, 1.75, 450, 120, '2026-06-25', '2026-08-25', 0, 1, '2026-06-25 13:37:31', 'Imagenes/Productos/nieve de limon.jpg'),
(19, 4, 'P017', '2260625133914', 'Nieve naranja', 'unidad', 1.25, 1.75, 410, 120, '2026-06-25', '2026-08-25', 0, 1, '2026-06-25 13:39:14', 'Imagenes/Productos/nieve de naranja.jpg'),
(20, 4, 'P018', '2260625134104', 'Nieve tamarindo', 'unidad', 1.50, 2.00, 420, 105, '2026-06-25', '2026-08-25', 0, 1, '2026-06-25 13:41:04', 'Imagenes/Productos/nieve de tamarindo.jpg'),
(21, 4, 'P019', '2260625134240', 'Nieve piña', 'unidad', 1.50, 2.00, 440, 105, '2026-06-25', '2026-08-25', 0, 1, '2026-06-25 13:42:40', 'Imagenes/Productos/nieve de piña.jpg'),
(22, 5, 'P020', '2260625134357', 'Sandwich de helado', 'unidad', 1.25, 2.00, 500, 130, '2026-06-25', '2026-12-25', 0, 1, '2026-06-25 13:43:57', 'Imagenes/Productos/sandwist de elado.jpg'),
(23, 5, 'P021', '2260625134850', 'Pastel helado', 'unidad', 8.00, 10.00, 120, 30, '2026-06-25', '2026-12-25', 0, 1, '2026-06-25 13:48:50', 'Imagenes/Productos/pastel helado.jpeg'),
(24, 5, 'P022', '2260625135037', 'Brownie', 'unidad', 2.00, 3.00, 300, 75, '2026-06-25', '2026-12-25', 0, 1, '2026-06-25 13:50:37', 'Imagenes/Productos/brownie congelado.jpg'),
(25, 5, 'P023', '2260625135214', 'Cheesecake', 'unidad', 2.50, 3.50, 100, 60, '2026-06-25', '2026-07-25', 0, 1, '2026-06-25 13:52:14', 'Imagenes/Productos/chesecake.png'),
(26, 2, 'P024', '2260625135326', 'Vaso vainilla', 'unidad', 1.00, 1.50, 600, 180, '2026-06-25', '2026-09-25', 0, 1, '2026-06-25 13:53:26', 'Imagenes/Productos/vaso de vainilla.jpg'),
(27, 2, 'P025', '2260625135430', 'Vaso chocolate', 'unidad', 1.00, 1.50, 600, 180, '2026-06-25', '2026-09-25', 0, 1, '2026-06-25 13:54:30', 'Imagenes/Productos/vaso de chocolate.jpg'),
(28, 2, 'P026', '2260625135533', 'Vaso fresa', 'unidad', 1.00, 1.50, 600, 180, '2026-06-25', '2026-09-25', 0, 1, '2026-06-25 13:55:33', 'Imagenes/Productos/vaso de fresa.jpg'),
(29, 2, 'P027', '2260625135704', 'Vaso napolitano', 'unidad', 1.25, 1.75, 680, 130, '2026-06-25', '2026-09-25', 0, 1, '2026-06-25 13:57:04', 'Imagenes/Productos/vaso napolitano.jpg'),
(30, 2, 'P028', '2260625135816', 'Cookies and cream', 'unidad', 1.50, 2.25, 450, 90, '2026-06-25', '2026-09-25', 0, 1, '2026-06-25 13:58:16', 'Imagenes/Productos/cookies.jpg'),
(31, 2, 'P029', '2260625135935', 'Dulce de leche', 'unidad', 1.50, 2.00, 450, 90, '2026-06-25', '2026-09-25', 0, 1, '2026-06-25 13:59:35', 'Imagenes/Productos/vaso Dulce de leche.png');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tbl_promociones`
--

CREATE TABLE `tbl_promociones` (
  `id_promocion` int(11) NOT NULL,
  `id_producto` int(11) DEFAULT NULL,
  `id_categoria` int(11) DEFAULT NULL,
  `nombre` varchar(150) NOT NULL,
  `descripcion` text DEFAULT NULL,
  `tipo_descuento` enum('porcentaje','monto_fijo') NOT NULL DEFAULT 'porcentaje',
  `valor_descuento` decimal(10,2) NOT NULL DEFAULT 0.00,
  `fecha_inicio` date NOT NULL,
  `fecha_fin` date NOT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT 1,
  `fecha_registro` datetime NOT NULL DEFAULT current_timestamp(),
  `estado` enum('pendiente','aprobada','rechazada') NOT NULL DEFAULT 'pendiente',
  `id_aprobado_por` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tbl_roles`
--

CREATE TABLE `tbl_roles` (
  `id_rol` int(11) NOT NULL,
  `nombre` varchar(60) NOT NULL,
  `descripcion` varchar(200) DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tbl_roles`
--

INSERT INTO `tbl_roles` (`id_rol`, `nombre`, `descripcion`, `activo`) VALUES
(1, 'Administrador', 'Control total del sistema', 1),
(2, 'Supervisor', 'Autorizaciones y validaciones comerciales', 1),
(3, 'Empleado de Caja', 'Venta en sucursal y entregas mayoristas', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tbl_rutas`
--

CREATE TABLE `tbl_rutas` (
  `id_ruta` int(11) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `zona` varchar(100) DEFAULT NULL,
  `id_responsable` int(11) DEFAULT NULL,
  `horario_inicio` time DEFAULT NULL,
  `horario_fin` time DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tbl_rutas`
--

INSERT INTO `tbl_rutas` (`id_ruta`, `nombre`, `zona`, `id_responsable`, `horario_inicio`, `horario_fin`, `activo`) VALUES
(1, 'R001', 'San Miguel Centro', 31, '07:00:00', '15:00:00', 1),
(2, 'R002', 'San Miguel Norte', 28, '08:00:00', '16:00:00', 1),
(3, 'R003', 'San Miguel Oeste', 28, '09:00:00', '17:00:00', 1),
(4, 'R004', 'Usulutan', 29, '06:00:00', '14:00:00', 1),
(5, 'R005', 'La Union Sur', 29, '10:00:00', '18:00:00', 1),
(6, 'R006', 'Morazan Norte', 27, '07:30:00', '15:30:00', 1),
(7, 'R007', 'Morazan Sur', 27, '08:30:00', '16:30:00', 1),
(8, 'R008', 'Morazan Centro', 31, '11:00:00', '19:00:00', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tbl_salidas_ruta`
--

CREATE TABLE `tbl_salidas_ruta` (
  `id_salida` int(11) NOT NULL,
  `id_vendedor` int(11) NOT NULL,
  `id_ruta` int(11) NOT NULL,
  `id_usuario` int(11) DEFAULT NULL,
  `fecha` date NOT NULL,
  `hora_salida` time NOT NULL,
  `vehiculo` varchar(100) DEFAULT NULL,
  `comision` decimal(5,2) NOT NULL,
  `estado` enum('abierta','liquidada','cancelada') NOT NULL DEFAULT 'abierta',
  `fecha_registro` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tbl_usuarios`
--

CREATE TABLE `tbl_usuarios` (
  `id_usuario` int(11) NOT NULL,
  `id_rol` int(11) NOT NULL,
  `nombre` varchar(150) NOT NULL,
  `usuario` varchar(60) NOT NULL,
  `contraseña_hash` varchar(255) NOT NULL,
  `contraseña_salt` varchar(255) NOT NULL,
  `correo` varchar(150) DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT 1,
  `fecha_registro` datetime NOT NULL DEFAULT current_timestamp(),
  `contraseña_temp_hash` varchar(255) DEFAULT NULL,
  `contraseña_temp_salt` varchar(255) DEFAULT NULL,
  `contraseña_temp_expira` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tbl_usuarios`
--

INSERT INTO `tbl_usuarios` (`id_usuario`, `id_rol`, `nombre`, `usuario`, `contraseña_hash`, `contraseña_salt`, `correo`, `activo`, `fecha_registro`, `contraseña_temp_hash`, `contraseña_temp_salt`, `contraseña_temp_expira`) VALUES
(13, 1, 'Administrador', 'admin', 'irP+98zNxP7E2vKR/mnG9FzsskRJlhzsNinvjdt+koY=', 'YJ8Z8KqnPccitPexut6cHUYHku9+j8lZ116c3ZPAuJ0=', 'heladeriafmo@gmail.com', 1, '2026-06-22 06:45:15', NULL, NULL, NULL),
(15, 3, 'Fausto Melendez', 'fausto', 'kIX17NlWyKoxPitPCIEgW2TjkHoQNqk9KFcdyi8/F3s=', '/LqG23SOG4qsT9Nm3A5v+P554KkaRacj5aFJ4zEZkUE=', 'fausto.melendez.@heladeriafmo.com', 1, '2026-06-25 09:12:06', NULL, NULL, NULL),
(16, 3, 'Enrique Garcia', 'enrique', '5CwSlogPOzLsCxS3mMQbfWt8rhaYW0TTgiRVBrMok1I=', 'Od6g0WZ7wd/wHEGmBR0TDg4LekiLXf49ET9TmoDhiTA=', 'enrique.garcia.@heladeriafmo.com', 1, '2026-06-25 09:12:53', NULL, NULL, NULL),
(17, 3, 'Elena Marquez', 'elena', 'ZptCXVcJm1vyupevahObfC887G/FpFPwd/SuC4bopYc=', 'SpYCkRVVFg50yM4bPcWaHVoyzn5cos5dlvN2BbFbHlI=', 'elena.marquez.@heladeriafmo.com', 1, '2026-06-25 09:15:23', NULL, NULL, NULL),
(18, 3, 'Maria Hernandez', 'maria', 'fUQsTd9ZGmoNNtNAuED0uZAfw7m89tScounFGNj2n+Y=', 'yauHK+NbH4hweVZSFheegPhZaonjQPBEXHPWDB67EHA=', 'maria.hernandez.@heladeriafmo.com', 1, '2026-06-25 09:16:01', NULL, NULL, NULL),
(27, 2, 'Carlos Martinez', 'carlos', 'rUtzG/mvUiWqq/c0ojo4y4Z+B3GDMmmkmuBVWRbNXoQ=', 'EltnZe3HZHIxw45CB86XBV7vuLXMckp+mvgipXWNWgI=', 'mc24012@ues.edu.sv', 1, '2026-06-25 09:27:14', NULL, NULL, NULL),
(28, 2, 'Danis Portillo', 'danis', 'qyIZlKZGPsfHH3P3cNIxdiLoKn93JWLcB7SPr3fkft0=', 'iHoYdsjfZaNCK3CPH8v2edtY1KjNwuYYj7NfZBdEUGc=', 'pa22067@ues.edu.sv', 1, '2026-06-25 09:30:31', NULL, NULL, NULL),
(29, 2, 'Karen Beltrami', 'karen', '97XyDmLHFKMG/e40Pwkv+v1MXxxNUawMYngvpSyyJyw=', 'cENKxQRNcn5x2twjg7gWSq7dhA3fZJwZGg+5W6blQBI=', 'ba24004@ues.edu.sv', 1, '2026-06-25 09:36:09', NULL, NULL, NULL),
(30, 1, 'Cesar Rodriguez', 'franco', 'F8cmh2pWVIZcxW6tcEsBRERWXJs+zt4UqKIQxuaIE+0=', 't0ZodWKYCq2Fb4TBv6yI6BQQRt6UJnPXUl5Cv/TaOBA=', 'cesar.rodriguez@ues.edu.sv', 1, '2026-06-25 09:39:52', NULL, NULL, NULL),
(31, 2, 'Francisco Perez', 'francisco', 'uYnAMCNlF/O3JQntX2sP2FcMpxbYfuI1nryhSTfTJ2o=', 'KtP95qA7xxOOd+IejTbiOlKbrhfQEVjX8FpzEVw5YO8=', 'po24009@ues.edu.sv', 1, '2026-06-25 09:41:50', NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tbl_vendedores`
--

CREATE TABLE `tbl_vendedores` (
  `id_vendedor` int(11) NOT NULL,
  `codigo_empleado` varchar(20) NOT NULL,
  `nombre` varchar(150) NOT NULL,
  `fotografia` varchar(200) DEFAULT NULL,
  `dui` varchar(15) NOT NULL,
  `telefono` varchar(20) DEFAULT NULL,
  `direccion` varchar(255) DEFAULT NULL,
  `estado` enum('activo','inactivo') NOT NULL DEFAULT 'activo',
  `fecha_registro` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tbl_vendedores`
--

INSERT INTO `tbl_vendedores` (`id_vendedor`, `codigo_empleado`, `nombre`, `fotografia`, `dui`, `telefono`, `direccion`, `estado`, `fecha_registro`) VALUES
(2, 'E001', 'Mateo Ramirez', 'Imagenes/Empleados/0b05b51867894e0c9a7220fa2e459af9.png', '111111111', '11111111', 'San Miguel', 'activo', '2026-06-25 10:17:08'),
(3, 'E002', 'Sofia Castillo', 'Imagenes/Empleados/ded38ac2d1e64f46b29e9e2e690c8997.jpg', '222222222', '22222222', 'San Miguel', 'activo', '2026-06-25 10:18:03'),
(4, 'E003', 'Daniel Herrera', 'Imagenes/Empleados/71a84f8b56b04b0cbea7655428c7c17d.png', '333333333', '33333333', 'San Miguel', 'activo', '2026-06-25 10:19:01'),
(5, 'E004', 'Valeria Morales', 'Imagenes/Empleados/2305de6ff8594f8fbe0d918e445edf8d.jpg', '444444444', '44444444', 'San Miguel', 'activo', '2026-06-25 10:21:39'),
(6, 'E005', 'Sebastian Cruz', 'Imagenes/Empleados/a151a2542f474eb7b70443725944fc08.png', '555555555', '55555555', 'San Miguel', 'activo', '2026-06-25 10:22:16'),
(7, 'E006', 'Camila flores', 'Imagenes/Empleados/d460594b5f8c4fb9bfa7179d63b6fa68.jpg', '666666666', '66666666', 'San Miguel', 'activo', '2026-06-25 10:23:01'),
(8, 'E007', 'Andres Mejia', 'Imagenes/Empleados/25d7b94058704470b2cc8902ccb77733.png', '777777777', '77777777', 'San Miguel', 'activo', '2026-06-25 10:24:05'),
(9, 'E008', 'Isabella Rivas', 'Imagenes/Empleados/6b91d7e6b2d24a69ac145c33cef90995.jpg', '888888888', '88888888', 'San Miguel', 'activo', '2026-06-25 10:24:39');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tbl_ventas_sucursal`
--

CREATE TABLE `tbl_ventas_sucursal` (
  `id_venta` int(11) NOT NULL,
  `id_caja` int(11) DEFAULT NULL,
  `id_cajero` int(11) DEFAULT NULL,
  `subtotal` decimal(10,2) NOT NULL DEFAULT 0.00,
  `descuento` decimal(10,2) NOT NULL DEFAULT 0.00,
  `total` decimal(10,2) NOT NULL DEFAULT 0.00,
  `activo` tinyint(1) NOT NULL DEFAULT 1,
  `fecha_registro` datetime NOT NULL DEFAULT current_timestamp(),
  `id_autorizado_descuento` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tbl_ventas_sucursal`
--

INSERT INTO `tbl_ventas_sucursal` (`id_venta`, `id_caja`, `id_cajero`, `subtotal`, `descuento`, `total`, `activo`, `fecha_registro`, `id_autorizado_descuento`) VALUES
(1, 2, 13, 19.20, 0.00, 19.20, 1, '2026-06-23 20:32:58', NULL);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `tbl_arqueos_caja`
--
ALTER TABLE `tbl_arqueos_caja`
  ADD PRIMARY KEY (`id_arqueo`),
  ADD KEY `id_caja` (`id_caja`),
  ADD KEY `id_realizado_por` (`id_realizado_por`);

--
-- Indices de la tabla `tbl_cajas`
--
ALTER TABLE `tbl_cajas`
  ADD PRIMARY KEY (`id_caja`),
  ADD KEY `id_cajero` (`id_cajero`);

--
-- Indices de la tabla `tbl_categorias`
--
ALTER TABLE `tbl_categorias`
  ADD PRIMARY KEY (`id_categoria`);

--
-- Indices de la tabla `tbl_clientes_mayoristas`
--
ALTER TABLE `tbl_clientes_mayoristas`
  ADD PRIMARY KEY (`id_cliente`);

--
-- Indices de la tabla `tbl_detalles_liquidacion`
--
ALTER TABLE `tbl_detalles_liquidacion`
  ADD PRIMARY KEY (`id_detalle_liquidacion`),
  ADD KEY `id_liquidacion` (`id_liquidacion`),
  ADD KEY `id_producto` (`id_producto`);

--
-- Indices de la tabla `tbl_detalles_pedido_mayorista`
--
ALTER TABLE `tbl_detalles_pedido_mayorista`
  ADD PRIMARY KEY (`id_detalle_pedido`),
  ADD KEY `id_pedido` (`id_pedido`),
  ADD KEY `id_producto` (`id_producto`);

--
-- Indices de la tabla `tbl_detalles_salida_ruta`
--
ALTER TABLE `tbl_detalles_salida_ruta`
  ADD PRIMARY KEY (`id_detalle_salida`),
  ADD KEY `id_salida` (`id_salida`),
  ADD KEY `id_producto` (`id_producto`);

--
-- Indices de la tabla `tbl_detalles_venta_sucursal`
--
ALTER TABLE `tbl_detalles_venta_sucursal`
  ADD PRIMARY KEY (`id_detalle_venta`),
  ADD KEY `id_venta` (`id_venta`),
  ADD KEY `id_producto` (`id_producto`);

--
-- Indices de la tabla `tbl_liquidaciones_ruta`
--
ALTER TABLE `tbl_liquidaciones_ruta`
  ADD PRIMARY KEY (`id_liquidacion`),
  ADD UNIQUE KEY `id_salida` (`id_salida`),
  ADD KEY `id_validado_por` (`id_validado_por`);

--
-- Indices de la tabla `tbl_movimientos_caja`
--
ALTER TABLE `tbl_movimientos_caja`
  ADD PRIMARY KEY (`id_movimiento_caja`),
  ADD KEY `id_caja` (`id_caja`),
  ADD KEY `id_usuario` (`id_usuario`);

--
-- Indices de la tabla `tbl_movimientos_inventario`
--
ALTER TABLE `tbl_movimientos_inventario`
  ADD PRIMARY KEY (`id_movimiento`),
  ADD KEY `id_producto` (`id_producto`),
  ADD KEY `id_usuario` (`id_usuario`);

--
-- Indices de la tabla `tbl_notificacion`
--
ALTER TABLE `tbl_notificacion`
  ADD PRIMARY KEY (`id_notificacion`);

--
-- Indices de la tabla `tbl_pedidos_mayoristas`
--
ALTER TABLE `tbl_pedidos_mayoristas`
  ADD PRIMARY KEY (`id_pedido`),
  ADD UNIQUE KEY `codigo_pedido` (`codigo_pedido`),
  ADD UNIQUE KEY `codigo_retiro` (`codigo_retiro`),
  ADD KEY `id_cliente` (`id_cliente`),
  ADD KEY `id_atendido_por` (`id_atendido_por`),
  ADD KEY `id_entregado_por` (`id_entregado_por`);

--
-- Indices de la tabla `tbl_productos`
--
ALTER TABLE `tbl_productos`
  ADD PRIMARY KEY (`id_producto`),
  ADD UNIQUE KEY `codigo` (`codigo`),
  ADD UNIQUE KEY `codigo_barras` (`codigo_barras`),
  ADD KEY `id_categoria` (`id_categoria`);

--
-- Indices de la tabla `tbl_promociones`
--
ALTER TABLE `tbl_promociones`
  ADD PRIMARY KEY (`id_promocion`),
  ADD KEY `id_producto` (`id_producto`),
  ADD KEY `id_categoria` (`id_categoria`);

--
-- Indices de la tabla `tbl_roles`
--
ALTER TABLE `tbl_roles`
  ADD PRIMARY KEY (`id_rol`),
  ADD UNIQUE KEY `nombre` (`nombre`);

--
-- Indices de la tabla `tbl_rutas`
--
ALTER TABLE `tbl_rutas`
  ADD PRIMARY KEY (`id_ruta`),
  ADD KEY `id_responsable` (`id_responsable`);

--
-- Indices de la tabla `tbl_salidas_ruta`
--
ALTER TABLE `tbl_salidas_ruta`
  ADD PRIMARY KEY (`id_salida`),
  ADD KEY `id_vendedor` (`id_vendedor`),
  ADD KEY `id_ruta` (`id_ruta`),
  ADD KEY `id_usuario` (`id_usuario`);

--
-- Indices de la tabla `tbl_usuarios`
--
ALTER TABLE `tbl_usuarios`
  ADD PRIMARY KEY (`id_usuario`),
  ADD UNIQUE KEY `usuario` (`usuario`),
  ADD KEY `id_rol` (`id_rol`);

--
-- Indices de la tabla `tbl_vendedores`
--
ALTER TABLE `tbl_vendedores`
  ADD PRIMARY KEY (`id_vendedor`),
  ADD UNIQUE KEY `codigo_empleado` (`codigo_empleado`),
  ADD UNIQUE KEY `dui` (`dui`);

--
-- Indices de la tabla `tbl_ventas_sucursal`
--
ALTER TABLE `tbl_ventas_sucursal`
  ADD PRIMARY KEY (`id_venta`),
  ADD KEY `id_caja` (`id_caja`),
  ADD KEY `id_cajero` (`id_cajero`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `tbl_arqueos_caja`
--
ALTER TABLE `tbl_arqueos_caja`
  MODIFY `id_arqueo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `tbl_cajas`
--
ALTER TABLE `tbl_cajas`
  MODIFY `id_caja` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `tbl_categorias`
--
ALTER TABLE `tbl_categorias`
  MODIFY `id_categoria` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de la tabla `tbl_clientes_mayoristas`
--
ALTER TABLE `tbl_clientes_mayoristas`
  MODIFY `id_cliente` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `tbl_detalles_liquidacion`
--
ALTER TABLE `tbl_detalles_liquidacion`
  MODIFY `id_detalle_liquidacion` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `tbl_detalles_pedido_mayorista`
--
ALTER TABLE `tbl_detalles_pedido_mayorista`
  MODIFY `id_detalle_pedido` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `tbl_detalles_salida_ruta`
--
ALTER TABLE `tbl_detalles_salida_ruta`
  MODIFY `id_detalle_salida` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `tbl_detalles_venta_sucursal`
--
ALTER TABLE `tbl_detalles_venta_sucursal`
  MODIFY `id_detalle_venta` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `tbl_liquidaciones_ruta`
--
ALTER TABLE `tbl_liquidaciones_ruta`
  MODIFY `id_liquidacion` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `tbl_movimientos_caja`
--
ALTER TABLE `tbl_movimientos_caja`
  MODIFY `id_movimiento_caja` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `tbl_movimientos_inventario`
--
ALTER TABLE `tbl_movimientos_inventario`
  MODIFY `id_movimiento` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `tbl_notificacion`
--
ALTER TABLE `tbl_notificacion`
  MODIFY `id_notificacion` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `tbl_pedidos_mayoristas`
--
ALTER TABLE `tbl_pedidos_mayoristas`
  MODIFY `id_pedido` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `tbl_productos`
--
ALTER TABLE `tbl_productos`
  MODIFY `id_producto` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;

--
-- AUTO_INCREMENT de la tabla `tbl_promociones`
--
ALTER TABLE `tbl_promociones`
  MODIFY `id_promocion` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `tbl_roles`
--
ALTER TABLE `tbl_roles`
  MODIFY `id_rol` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `tbl_rutas`
--
ALTER TABLE `tbl_rutas`
  MODIFY `id_ruta` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT de la tabla `tbl_salidas_ruta`
--
ALTER TABLE `tbl_salidas_ruta`
  MODIFY `id_salida` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `tbl_usuarios`
--
ALTER TABLE `tbl_usuarios`
  MODIFY `id_usuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;

--
-- AUTO_INCREMENT de la tabla `tbl_vendedores`
--
ALTER TABLE `tbl_vendedores`
  MODIFY `id_vendedor` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT de la tabla `tbl_ventas_sucursal`
--
ALTER TABLE `tbl_ventas_sucursal`
  MODIFY `id_venta` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `tbl_arqueos_caja`
--
ALTER TABLE `tbl_arqueos_caja`
  ADD CONSTRAINT `tbl_arqueos_caja_ibfk_1` FOREIGN KEY (`id_caja`) REFERENCES `tbl_cajas` (`id_caja`),
  ADD CONSTRAINT `tbl_arqueos_caja_ibfk_2` FOREIGN KEY (`id_realizado_por`) REFERENCES `tbl_usuarios` (`id_usuario`);

--
-- Filtros para la tabla `tbl_cajas`
--
ALTER TABLE `tbl_cajas`
  ADD CONSTRAINT `tbl_cajas_ibfk_1` FOREIGN KEY (`id_cajero`) REFERENCES `tbl_usuarios` (`id_usuario`);

--
-- Filtros para la tabla `tbl_detalles_liquidacion`
--
ALTER TABLE `tbl_detalles_liquidacion`
  ADD CONSTRAINT `tbl_detalles_liquidacion_ibfk_1` FOREIGN KEY (`id_liquidacion`) REFERENCES `tbl_liquidaciones_ruta` (`id_liquidacion`),
  ADD CONSTRAINT `tbl_detalles_liquidacion_ibfk_2` FOREIGN KEY (`id_producto`) REFERENCES `tbl_productos` (`id_producto`);

--
-- Filtros para la tabla `tbl_detalles_pedido_mayorista`
--
ALTER TABLE `tbl_detalles_pedido_mayorista`
  ADD CONSTRAINT `tbl_detalles_pedido_mayorista_ibfk_1` FOREIGN KEY (`id_pedido`) REFERENCES `tbl_pedidos_mayoristas` (`id_pedido`),
  ADD CONSTRAINT `tbl_detalles_pedido_mayorista_ibfk_2` FOREIGN KEY (`id_producto`) REFERENCES `tbl_productos` (`id_producto`);

--
-- Filtros para la tabla `tbl_detalles_salida_ruta`
--
ALTER TABLE `tbl_detalles_salida_ruta`
  ADD CONSTRAINT `tbl_detalles_salida_ruta_ibfk_1` FOREIGN KEY (`id_salida`) REFERENCES `tbl_salidas_ruta` (`id_salida`),
  ADD CONSTRAINT `tbl_detalles_salida_ruta_ibfk_2` FOREIGN KEY (`id_producto`) REFERENCES `tbl_productos` (`id_producto`);

--
-- Filtros para la tabla `tbl_detalles_venta_sucursal`
--
ALTER TABLE `tbl_detalles_venta_sucursal`
  ADD CONSTRAINT `tbl_detalles_venta_sucursal_ibfk_1` FOREIGN KEY (`id_venta`) REFERENCES `tbl_ventas_sucursal` (`id_venta`),
  ADD CONSTRAINT `tbl_detalles_venta_sucursal_ibfk_2` FOREIGN KEY (`id_producto`) REFERENCES `tbl_productos` (`id_producto`);

--
-- Filtros para la tabla `tbl_liquidaciones_ruta`
--
ALTER TABLE `tbl_liquidaciones_ruta`
  ADD CONSTRAINT `tbl_liquidaciones_ruta_ibfk_1` FOREIGN KEY (`id_salida`) REFERENCES `tbl_salidas_ruta` (`id_salida`),
  ADD CONSTRAINT `tbl_liquidaciones_ruta_ibfk_2` FOREIGN KEY (`id_validado_por`) REFERENCES `tbl_usuarios` (`id_usuario`);

--
-- Filtros para la tabla `tbl_movimientos_caja`
--
ALTER TABLE `tbl_movimientos_caja`
  ADD CONSTRAINT `tbl_movimientos_caja_ibfk_1` FOREIGN KEY (`id_caja`) REFERENCES `tbl_cajas` (`id_caja`),
  ADD CONSTRAINT `tbl_movimientos_caja_ibfk_2` FOREIGN KEY (`id_usuario`) REFERENCES `tbl_usuarios` (`id_usuario`);

--
-- Filtros para la tabla `tbl_movimientos_inventario`
--
ALTER TABLE `tbl_movimientos_inventario`
  ADD CONSTRAINT `tbl_movimientos_inventario_ibfk_1` FOREIGN KEY (`id_producto`) REFERENCES `tbl_productos` (`id_producto`),
  ADD CONSTRAINT `tbl_movimientos_inventario_ibfk_2` FOREIGN KEY (`id_usuario`) REFERENCES `tbl_usuarios` (`id_usuario`);

--
-- Filtros para la tabla `tbl_pedidos_mayoristas`
--
ALTER TABLE `tbl_pedidos_mayoristas`
  ADD CONSTRAINT `tbl_pedidos_mayoristas_ibfk_1` FOREIGN KEY (`id_cliente`) REFERENCES `tbl_clientes_mayoristas` (`id_cliente`),
  ADD CONSTRAINT `tbl_pedidos_mayoristas_ibfk_2` FOREIGN KEY (`id_atendido_por`) REFERENCES `tbl_usuarios` (`id_usuario`),
  ADD CONSTRAINT `tbl_pedidos_mayoristas_ibfk_3` FOREIGN KEY (`id_entregado_por`) REFERENCES `tbl_usuarios` (`id_usuario`);

--
-- Filtros para la tabla `tbl_productos`
--
ALTER TABLE `tbl_productos`
  ADD CONSTRAINT `tbl_productos_ibfk_1` FOREIGN KEY (`id_categoria`) REFERENCES `tbl_categorias` (`id_categoria`);

--
-- Filtros para la tabla `tbl_promociones`
--
ALTER TABLE `tbl_promociones`
  ADD CONSTRAINT `tbl_promociones_ibfk_1` FOREIGN KEY (`id_producto`) REFERENCES `tbl_productos` (`id_producto`),
  ADD CONSTRAINT `tbl_promociones_ibfk_2` FOREIGN KEY (`id_categoria`) REFERENCES `tbl_categorias` (`id_categoria`);

--
-- Filtros para la tabla `tbl_rutas`
--
ALTER TABLE `tbl_rutas`
  ADD CONSTRAINT `tbl_rutas_ibfk_1` FOREIGN KEY (`id_responsable`) REFERENCES `tbl_usuarios` (`id_usuario`);

--
-- Filtros para la tabla `tbl_salidas_ruta`
--
ALTER TABLE `tbl_salidas_ruta`
  ADD CONSTRAINT `tbl_salidas_ruta_ibfk_1` FOREIGN KEY (`id_vendedor`) REFERENCES `tbl_vendedores` (`id_vendedor`),
  ADD CONSTRAINT `tbl_salidas_ruta_ibfk_2` FOREIGN KEY (`id_ruta`) REFERENCES `tbl_rutas` (`id_ruta`),
  ADD CONSTRAINT `tbl_salidas_ruta_ibfk_3` FOREIGN KEY (`id_usuario`) REFERENCES `tbl_usuarios` (`id_usuario`);

--
-- Filtros para la tabla `tbl_usuarios`
--
ALTER TABLE `tbl_usuarios`
  ADD CONSTRAINT `tbl_usuarios_ibfk_1` FOREIGN KEY (`id_rol`) REFERENCES `tbl_roles` (`id_rol`);

--
-- Filtros para la tabla `tbl_ventas_sucursal`
--
ALTER TABLE `tbl_ventas_sucursal`
  ADD CONSTRAINT `tbl_ventas_sucursal_ibfk_1` FOREIGN KEY (`id_caja`) REFERENCES `tbl_cajas` (`id_caja`),
  ADD CONSTRAINT `tbl_ventas_sucursal_ibfk_2` FOREIGN KEY (`id_cajero`) REFERENCES `tbl_usuarios` (`id_usuario`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
