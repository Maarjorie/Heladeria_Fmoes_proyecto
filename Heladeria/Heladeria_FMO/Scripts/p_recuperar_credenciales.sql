-- Recuperacion de credenciales NO destructiva:
-- la contrasena temporal se guarda en columnas aparte con expiracion,
-- de modo que la contrasena real sigue siendo valida hasta que el
-- usuario realmente use la temporal (y entonces fija una nueva).

ALTER TABLE tbl_usuarios
    ADD COLUMN IF NOT EXISTS `contraseña_temp_hash`   VARCHAR(255) NULL,
    ADD COLUMN IF NOT EXISTS `contraseña_temp_salt`   VARCHAR(255) NULL,
    ADD COLUMN IF NOT EXISTS `contraseña_temp_expira` DATETIME     NULL;

DROP PROCEDURE IF EXISTS p_login_usuario;
DROP PROCEDURE IF EXISTS p_obtener_usuario_por_correo;
DROP PROCEDURE IF EXISTS p_actualizar_contrasena;
DROP PROCEDURE IF EXISTS p_guardar_contrasena_temporal;

DELIMITER //

-- Login: ahora devuelve tambien las columnas de contrasena temporal.
CREATE PROCEDURE p_login_usuario(IN `p_usuario` VARCHAR(60))
BEGIN
    SELECT
        id_usuario,
        id_rol,
        nombre,
        usuario,
        `contraseña_hash`,
        `contraseña_salt`,
        correo,
        activo,
        `contraseña_temp_hash`,
        `contraseña_temp_salt`,
        `contraseña_temp_expira`
    FROM tbl_usuarios
    WHERE usuario = p_usuario;
END //

CREATE PROCEDURE p_obtener_usuario_por_correo(IN p_correo VARCHAR(150))
BEGIN
    SELECT
        id_usuario,
        id_rol,
        nombre,
        usuario,
        `contraseña_hash`,
        `contraseña_salt`,
        correo,
        activo
    FROM tbl_usuarios
    WHERE correo = p_correo
    LIMIT 1;
END //

-- Guarda la contrasena temporal SIN tocar la real.
CREATE PROCEDURE p_guardar_contrasena_temporal(
    IN p_id_usuario INT,
    IN p_hash VARCHAR(255),
    IN p_salt VARCHAR(255),
    IN p_expira DATETIME)
BEGIN
    UPDATE tbl_usuarios
    SET `contraseña_temp_hash`   = p_hash,
        `contraseña_temp_salt`   = p_salt,
        `contraseña_temp_expira` = p_expira
    WHERE id_usuario = p_id_usuario;
END //

-- Fija una nueva contrasena permanente y limpia cualquier temporal pendiente.
CREATE PROCEDURE p_actualizar_contrasena(
    IN p_id_usuario INT,
    IN p_hash VARCHAR(255),
    IN p_salt VARCHAR(255))
BEGIN
    UPDATE tbl_usuarios
    SET `contraseña_hash`        = p_hash,
        `contraseña_salt`        = p_salt,
        `contraseña_temp_hash`   = NULL,
        `contraseña_temp_salt`   = NULL,
        `contraseña_temp_expira` = NULL
    WHERE id_usuario = p_id_usuario;
END //

DELIMITER ;
