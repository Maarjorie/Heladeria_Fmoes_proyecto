DROP PROCEDURE IF EXISTS p_obtener_usuario_por_correo;
DROP PROCEDURE IF EXISTS p_actualizar_contrasena;

DELIMITER //

CREATE PROCEDURE p_obtener_usuario_por_correo(IN p_correo VARCHAR(150))
BEGIN
    SELECT
        id_usuario,
        id_rol,
        nombre,
        usuario,
        contraseña_hash,
        contraseña_salt,
        correo,
        activo
    FROM tbl_usuarios
    WHERE correo = p_correo
    LIMIT 1;
END //

CREATE PROCEDURE p_actualizar_contrasena(IN p_id_usuario INT, IN p_hash VARCHAR(255), IN p_salt VARCHAR(255))
BEGIN
    UPDATE tbl_usuarios
    SET contraseña_hash = p_hash,
        contraseña_salt = p_salt
    WHERE id_usuario = p_id_usuario;
END //

DELIMITER ;
