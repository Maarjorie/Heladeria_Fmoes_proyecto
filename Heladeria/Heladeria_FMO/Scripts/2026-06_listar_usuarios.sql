-- ============================================================================
-- Heladería FMO — Listado de TODOS los usuarios (activos e inactivos) con el
-- nombre de su rol, para la gestión de usuarios (incluida la reactivación).
-- (p_listar_usuarios_activos solo devuelve los activos.)
-- ============================================================================
USE db_heladeria;

DELIMITER $$

DROP PROCEDURE IF EXISTS p_listar_usuarios $$
CREATE PROCEDURE p_listar_usuarios()
BEGIN
    SELECT
        u.id_usuario,
        u.usuario,
        u.nombre,
        u.correo,
        r.nombre AS rol,
        u.id_rol,
        u.activo
    FROM tbl_usuarios u
    INNER JOIN tbl_roles r ON u.id_rol = r.id_rol
    ORDER BY u.nombre;
END $$

DELIMITER ;
