-- ============================================================================
-- Heladería FMO — Autorizaciones: arqueos (estado de autorización) y rechazo de
-- pedidos mayoristas.
-- ============================================================================
USE db_heladeria;

-- Columnas de autorización para los arqueos (MariaDB soporta IF NOT EXISTS).
ALTER TABLE tbl_arqueos_caja
    ADD COLUMN IF NOT EXISTS autorizado TINYINT(1) NOT NULL DEFAULT 0,
    ADD COLUMN IF NOT EXISTS id_autorizado_por INT NULL;

DELIMITER $$

-- Arqueos pendientes de autorización (con el nombre de quien lo realizó).
DROP PROCEDURE IF EXISTS p_listar_arqueos_pendientes $$
CREATE PROCEDURE p_listar_arqueos_pendientes()
BEGIN
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
END $$

-- Autoriza un arqueo.
DROP PROCEDURE IF EXISTS p_autorizar_arqueo $$
CREATE PROCEDURE p_autorizar_arqueo(IN p_id_arqueo INT, IN p_id_autorizado_por INT)
BEGIN
    UPDATE tbl_arqueos_caja
    SET autorizado = 1, id_autorizado_por = p_id_autorizado_por
    WHERE id_arqueo = p_id_arqueo;
END $$

-- Rechaza (cancela) un pedido mayorista pendiente.
DROP PROCEDURE IF EXISTS p_cancelar_pedido_mayorista $$
CREATE PROCEDURE p_cancelar_pedido_mayorista(IN p_id_pedido INT)
BEGIN
    UPDATE tbl_pedidos_mayoristas
    SET estado = 'cancelado'
    WHERE id_pedido = p_id_pedido AND estado = 'pendiente';
END $$

DELIMITER ;
