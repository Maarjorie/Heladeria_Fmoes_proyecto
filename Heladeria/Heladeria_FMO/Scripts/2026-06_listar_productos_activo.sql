-- ============================================================================
-- Heladería FMO — Agrega la columna 'activo' al listado de productos para poder
-- mostrar el estado, reactivar productos y filtrar los inactivos en el Punto de
-- Venta. (Antes el listado no devolvía 'activo'.)
-- ============================================================================
USE db_heladeria;

DELIMITER $$

DROP PROCEDURE IF EXISTS p_listar_productos $$
CREATE PROCEDURE p_listar_productos()
BEGIN
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
END $$

DELIMITER ;
