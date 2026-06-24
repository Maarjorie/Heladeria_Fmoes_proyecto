-- ============================================================================
-- Heladería FMO — Flujo de aprobación de promociones.
-- Agrega estado (pendiente/aprobada/rechazada) y quién la aprobó, y procedimientos
-- para listar (con nombres y estado), listar pendientes, aprobar y rechazar.
-- ============================================================================
USE db_heladeria;

ALTER TABLE tbl_promociones
    ADD COLUMN IF NOT EXISTS estado ENUM('pendiente','aprobada','rechazada') NOT NULL DEFAULT 'pendiente',
    ADD COLUMN IF NOT EXISTS id_aprobado_por INT NULL;

DELIMITER $$

-- Listado de promociones (con nombre del producto/categoría y estado).
DROP PROCEDURE IF EXISTS p_listar_promociones $$
CREATE PROCEDURE p_listar_promociones()
BEGIN
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
END $$

-- Promociones pendientes de aprobación.
DROP PROCEDURE IF EXISTS p_listar_promociones_pendientes $$
CREATE PROCEDURE p_listar_promociones_pendientes()
BEGIN
    SELECT
        p.id_promocion,
        p.nombre,
        COALESCE(pr.nombre, c.nombre, '—') AS objetivo,
        p.tipo_descuento,
        p.valor_descuento,
        p.fecha_inicio,
        p.fecha_fin
    FROM tbl_promociones p
    LEFT JOIN tbl_productos  pr ON p.id_producto  = pr.id_producto
    LEFT JOIN tbl_categorias c  ON p.id_categoria = c.id_categoria
    WHERE p.estado = 'pendiente'
    ORDER BY p.fecha_registro DESC;
END $$

-- Aprueba una promoción (queda activa).
DROP PROCEDURE IF EXISTS p_aprobar_promocion $$
CREATE PROCEDURE p_aprobar_promocion(IN p_id_promocion INT, IN p_id_aprobado_por INT)
BEGIN
    UPDATE tbl_promociones
    SET estado = 'aprobada', id_aprobado_por = p_id_aprobado_por, activo = 1
    WHERE id_promocion = p_id_promocion;
END $$

-- Rechaza una promoción (queda inactiva).
DROP PROCEDURE IF EXISTS p_rechazar_promocion $$
CREATE PROCEDURE p_rechazar_promocion(IN p_id_promocion INT, IN p_id_aprobado_por INT)
BEGIN
    UPDATE tbl_promociones
    SET estado = 'rechazada', id_aprobado_por = p_id_aprobado_por, activo = 0
    WHERE id_promocion = p_id_promocion;
END $$

DELIMITER ;
