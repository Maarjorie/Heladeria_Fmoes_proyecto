-- ============================================================================
-- Heladería FMO — Corrige p_cambiar_estado_vendedor.
-- La columna tbl_vendedores.estado es enum('activo','inactivo'); el proc hacía
-- SET estado = p_activo, por lo que un 0 dejaba la cadena vacía en vez de
-- 'inactivo'. Ahora mapea el bit a la etiqueta correcta.
-- ============================================================================
USE db_heladeria;

DELIMITER $$

DROP PROCEDURE IF EXISTS p_cambiar_estado_vendedor $$
CREATE PROCEDURE p_cambiar_estado_vendedor(IN p_id_vendedor INT, IN p_activo TINYINT(1))
BEGIN
    UPDATE tbl_vendedores
    SET estado = IF(p_activo = 1, 'activo', 'inactivo')
    WHERE id_vendedor = p_id_vendedor;
END $$

DELIMITER ;
