-- ============================================================================
--  Migracion de brechas - Helados La FMO
--  Solo aditiva: agrega columnas con defaults seguros y procedimientos nuevos.
--  No modifica firmas de procedimientos existentes (eso se hace junto con su
--  DAO en cada fase) ni elimina datos.
-- ============================================================================

-- ---- Columnas nuevas -------------------------------------------------------

-- Descuento fijo por cliente mayorista (% aplicado a sus pedidos).
ALTER TABLE tbl_clientes_mayoristas
    ADD COLUMN IF NOT EXISTS descuento_porcentaje DECIMAL(5,2) NOT NULL DEFAULT 0.00;

-- Fecha de entrega programada del pedido mayorista (opcional).
ALTER TABLE tbl_pedidos_mayoristas
    ADD COLUMN IF NOT EXISTS fecha_entrega_programada DATETIME NULL;

-- Usuario (supervisor) que autorizo un descuento en venta de sucursal.
ALTER TABLE tbl_ventas_sucursal
    ADD COLUMN IF NOT EXISTS id_autorizado_descuento INT NULL;

-- Estado de aprobacion del movimiento de inventario (solo los 'ajuste' usan
-- 'pendiente'; el resto se inserta directo como 'aplicado').
ALTER TABLE tbl_movimientos_inventario
    ADD COLUMN IF NOT EXISTS estado ENUM('aplicado','pendiente','rechazado') NOT NULL DEFAULT 'aplicado',
    ADD COLUMN IF NOT EXISTS id_aprobado_por INT NULL;

-- ---- Procedimientos nuevos -------------------------------------------------

DROP PROCEDURE IF EXISTS p_listar_pedidos_mayoristas_cliente;
DROP PROCEDURE IF EXISTS p_listar_movimientos_inventario_pendientes;
DROP PROCEDURE IF EXISTS p_solicitar_ajuste_inventario;
DROP PROCEDURE IF EXISTS p_aprobar_ajuste_inventario;
DROP PROCEDURE IF EXISTS p_rechazar_ajuste_inventario;
DROP PROCEDURE IF EXISTS p_registrar_descuento_venta;
DROP PROCEDURE IF EXISTS p_reporte_rentabilidad_ruta;
DROP PROCEDURE IF EXISTS p_reporte_ranking_vendedores;
DROP PROCEDURE IF EXISTS p_kpi_utilidad_dia;
DROP PROCEDURE IF EXISTS p_kpi_comisiones_dia;
DROP PROCEDURE IF EXISTS p_kpi_egresos_caja_dia;
DROP PROCEDURE IF EXISTS p_kpi_ventas_mayoristas_dia;
DROP PROCEDURE IF EXISTS p_kpi_ventas_por_ruta_dia;
DROP PROCEDURE IF EXISTS p_kpi_producto_top_dia;

DELIMITER //

-- Historial de pedidos de un cliente mayorista especifico.
CREATE PROCEDURE p_listar_pedidos_mayoristas_cliente(IN p_id_cliente INT)
BEGIN
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
END //

-- Ajustes de inventario pendientes de aprobacion.
CREATE PROCEDURE p_listar_movimientos_inventario_pendientes()
BEGIN
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
END //

-- Solicita un ajuste de inventario (queda pendiente, NO toca el stock).
-- p_cantidad puede ser negativa (merma) o positiva (sobrante).
CREATE PROCEDURE p_solicitar_ajuste_inventario(
    IN p_id_producto INT,
    IN p_cantidad INT,
    IN p_observacion TEXT,
    IN p_id_usuario INT,
    OUT p_id_movimiento INT)
BEGIN
    INSERT INTO tbl_movimientos_inventario(
        id_producto, id_usuario, tipo, cantidad, observacion, estado)
    VALUES(
        p_id_producto, p_id_usuario, 'ajuste', p_cantidad, p_observacion, 'pendiente');

    SET p_id_movimiento = LAST_INSERT_ID();
END //

-- Aprueba un ajuste pendiente: lo marca 'aplicado' y aplica el delta al stock.
CREATE PROCEDURE p_aprobar_ajuste_inventario(
    IN p_id_movimiento INT,
    IN p_id_aprobado_por INT)
BEGIN
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
END //

-- Rechaza un ajuste pendiente (no toca el stock).
CREATE PROCEDURE p_rechazar_ajuste_inventario(
    IN p_id_movimiento INT,
    IN p_id_aprobado_por INT)
BEGIN
    UPDATE tbl_movimientos_inventario
    SET estado = 'rechazado', id_aprobado_por = p_id_aprobado_por
    WHERE id_movimiento = p_id_movimiento AND estado = 'pendiente';
END //

-- Registra un descuento autorizado sobre una venta de sucursal.
CREATE PROCEDURE p_registrar_descuento_venta(
    IN p_id_venta INT,
    IN p_descuento DECIMAL(10,2),
    IN p_id_autorizado_por INT)
BEGIN
    UPDATE tbl_ventas_sucursal
    SET descuento = p_descuento,
        total = subtotal - p_descuento,
        id_autorizado_descuento = p_id_autorizado_por
    WHERE id_venta = p_id_venta;
END //

-- Reporte de rentabilidad por ruta (ingresos vs costos y comisiones).
CREATE PROCEDURE p_reporte_rentabilidad_ruta(
    IN p_fecha_inicio DATE,
    IN p_fecha_fin DATE)
BEGIN
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
END //

-- Ranking de vendedores por monto vendido y unidades.
CREATE PROCEDURE p_reporte_ranking_vendedores(
    IN p_fecha_inicio DATE,
    IN p_fecha_fin DATE)
BEGIN
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
END //

-- ---- KPIs del dashboard (una fila, por fecha) ------------------------------

CREATE PROCEDURE p_kpi_utilidad_dia(IN p_fecha DATE)
BEGIN
    SELECT COALESCE(SUM((d.precio_unitario - pr.precio_compra) * d.cantidad - d.descuento), 0) AS utilidad
    FROM tbl_ventas_sucursal vs
    INNER JOIN tbl_detalles_venta_sucursal d ON vs.id_venta = d.id_venta
    INNER JOIN tbl_productos pr ON d.id_producto = pr.id_producto
    WHERE DATE(vs.fecha_registro) = p_fecha AND vs.activo = 1;
END //

CREATE PROCEDURE p_kpi_comisiones_dia(IN p_fecha DATE)
BEGIN
    SELECT COALESCE(SUM(lr.comision_generada), 0) AS comisiones
    FROM tbl_liquidaciones_ruta lr
    INNER JOIN tbl_salidas_ruta sr ON lr.id_salida = sr.id_salida
    WHERE sr.fecha = p_fecha;
END //

CREATE PROCEDURE p_kpi_egresos_caja_dia(IN p_fecha DATE)
BEGIN
    SELECT COALESCE(SUM(monto), 0) AS egresos
    FROM tbl_movimientos_caja
    WHERE tipo = 'egreso' AND DATE(fecha_registro) = p_fecha;
END //

CREATE PROCEDURE p_kpi_ventas_mayoristas_dia(IN p_fecha DATE)
BEGIN
    SELECT COALESCE(SUM(total), 0) AS ventas_mayoristas
    FROM tbl_pedidos_mayoristas
    WHERE DATE(fecha_pedido) = p_fecha AND estado <> 'cancelado';
END //

CREATE PROCEDURE p_kpi_ventas_por_ruta_dia(IN p_fecha DATE)
BEGIN
    SELECT COALESCE(SUM(lr.total_vendido), 0) AS ventas_ruta
    FROM tbl_liquidaciones_ruta lr
    INNER JOIN tbl_salidas_ruta sr ON lr.id_salida = sr.id_salida
    WHERE sr.fecha = p_fecha;
END //

CREATE PROCEDURE p_kpi_producto_top_dia(IN p_fecha DATE)
BEGIN
    SELECT pr.nombre AS producto, SUM(d.cantidad) AS unidades
    FROM tbl_detalles_venta_sucursal d
    INNER JOIN tbl_ventas_sucursal vs ON d.id_venta = vs.id_venta
    INNER JOIN tbl_productos pr ON d.id_producto = pr.id_producto
    WHERE DATE(vs.fecha_registro) = p_fecha AND vs.activo = 1
    GROUP BY pr.id_producto, pr.nombre
    ORDER BY unidades DESC
    LIMIT 1;
END //

DELIMITER ;
