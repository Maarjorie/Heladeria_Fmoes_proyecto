-- ============================================================================
-- Heladería FMO — Procedimiento de apoyo para la detección automática de
-- notificaciones: indica si ya existe una notificación del mismo tipo y
-- referencia dentro de una ventana reciente (para no duplicar/spamear).
-- ============================================================================
USE db_heladeria;

DELIMITER $$

DROP PROCEDURE IF EXISTS p_existe_notificacion_reciente $$
CREATE PROCEDURE p_existe_notificacion_reciente(
    IN p_tipo VARCHAR(30),
    IN p_referencia_id INT,
    IN p_horas INT)
BEGIN
    SELECT COUNT(*) AS total
    FROM tbl_notificacion
    WHERE tipo = p_tipo
      AND referencia_id = p_referencia_id
      AND fecha_registro >= (NOW() - INTERVAL p_horas HOUR);
END $$

DELIMITER ;
