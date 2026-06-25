-- Datos necesarios para enviar por correo el comprobante de retiro (con QR)
-- a un cliente mayorista cuando su pedido queda confirmado/listo.
DROP PROCEDURE IF EXISTS p_obtener_pedido_comprobante;
DELIMITER $$
CREATE PROCEDURE p_obtener_pedido_comprobante(IN p_id_pedido INT)
BEGIN
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
DELIMITER ;
