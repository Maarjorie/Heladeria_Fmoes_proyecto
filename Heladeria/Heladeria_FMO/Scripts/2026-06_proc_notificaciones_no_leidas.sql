-- ============================================================================
-- Heladería FMO — Listado de notificaciones NO LEÍDAS (bandeja del supervisor).
-- Independiente del envío por correo (enviado): aquí importa "leido".
-- ============================================================================
USE db_heladeria;

DELIMITER $$

DROP PROCEDURE IF EXISTS p_listar_notificaciones_no_leidas $$
CREATE PROCEDURE p_listar_notificaciones_no_leidas()
BEGIN
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
END $$

DELIMITER ;
