-- ============================================================================
--  Fase E - Mayoristas: descuento por cliente, fecha de entrega programada,
--  totales del pedido e historial. Alinea las firmas de los procedimientos
--  con sus DAOs y corrige referencias a columnas inexistentes.
-- ============================================================================

DROP PROCEDURE IF EXISTS p_insertar_cliente_mayorista;
DROP PROCEDURE IF EXISTS p_editar_cliente_mayorista;
DROP PROCEDURE IF EXISTS p_listar_clientes_mayoristas;
DROP PROCEDURE IF EXISTS p_crear_pedido_mayorista;
DROP PROCEDURE IF EXISTS p_actualizar_totales_pedido_mayorista;

DELIMITER //

-- Inserta cliente mayorista (incluye descuento %). Usa la columna real 'activo'.
CREATE PROCEDURE p_insertar_cliente_mayorista(
    IN p_nombre_comercial VARCHAR(150),
    IN p_nit VARCHAR(25),
    IN p_encargado VARCHAR(150),
    IN p_direccion VARCHAR(255),
    IN p_telefono VARCHAR(20),
    IN p_correo VARCHAR(100),
    IN p_descuento_porcentaje DECIMAL(5,2))
BEGIN
    INSERT INTO tbl_clientes_mayoristas(
        nombre_comercial, nit, encargado, direccion, telefono, correo,
        descuento_porcentaje, activo)
    VALUES(
        p_nombre_comercial, p_nit, p_encargado, p_direccion, p_telefono, p_correo,
        p_descuento_porcentaje, 1);
END //

-- Edita cliente mayorista (incluye descuento %).
CREATE PROCEDURE p_editar_cliente_mayorista(
    IN p_id_cliente INT,
    IN p_nombre_comercial VARCHAR(150),
    IN p_nit VARCHAR(25),
    IN p_encargado VARCHAR(150),
    IN p_direccion VARCHAR(255),
    IN p_telefono VARCHAR(20),
    IN p_correo VARCHAR(100),
    IN p_descuento_porcentaje DECIMAL(5,2))
BEGIN
    UPDATE tbl_clientes_mayoristas
    SET nombre_comercial = p_nombre_comercial,
        nit = p_nit,
        encargado = p_encargado,
        direccion = p_direccion,
        telefono = p_telefono,
        correo = p_correo,
        descuento_porcentaje = p_descuento_porcentaje
    WHERE id_cliente = p_id_cliente;
END //

-- Listado de clientes (incluye descuento % y dirección).
CREATE PROCEDURE p_listar_clientes_mayoristas()
BEGIN
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
END //

-- Crea un pedido mayorista (alineado con el DAO: cliente, atendido_por,
-- fecha de entrega programada opcional) y devuelve el id.
CREATE PROCEDURE p_crear_pedido_mayorista(
    IN p_id_cliente INT,
    IN p_id_atendido_por INT,
    IN p_fecha_entrega_programada DATETIME,
    OUT p_id_pedido INT)
BEGIN
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
END //

-- Fija subtotal/descuento/total del pedido (se calcula en la app tras agregar
-- el detalle, aplicando el descuento del cliente).
CREATE PROCEDURE p_actualizar_totales_pedido_mayorista(
    IN p_id_pedido INT,
    IN p_subtotal DECIMAL(10,2),
    IN p_descuento DECIMAL(10,2),
    IN p_total DECIMAL(10,2))
BEGIN
    UPDATE tbl_pedidos_mayoristas
    SET subtotal = p_subtotal,
        descuento = p_descuento,
        total = p_total
    WHERE id_pedido = p_id_pedido;
END //

DELIMITER ;
