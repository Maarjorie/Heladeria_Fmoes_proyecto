-- ============================================================================
-- Heladería FMO — Procedimientos de listado faltantes para el módulo de
-- Vendedores ambulantes (fichas de salida y liquidaciones).
-- Solo agregan SELECT/JOIN de lectura; no modifican datos ni esquema.
-- Aplicar sobre la base db_heladeria.
-- ============================================================================
USE db_heladeria;

DELIMITER $$

-- ---- Listado de vendedores (para combos y administración) ------------------
DROP PROCEDURE IF EXISTS p_listar_vendedores $$
CREATE PROCEDURE p_listar_vendedores()
BEGIN
    SELECT
        v.id_vendedor,
        v.codigo_empleado,
        v.nombre,
        v.dui,
        v.telefono,
        v.estado
    FROM tbl_vendedores v
    ORDER BY v.nombre;
END $$

-- ---- Listado de fichas de salida (con vendedor y ruta) ---------------------
DROP PROCEDURE IF EXISTS p_listar_salidas_ruta $$
CREATE PROCEDURE p_listar_salidas_ruta()
BEGIN
    SELECT
        s.id_salida,
        v.nombre        AS vendedor,
        r.nombre        AS ruta,
        s.fecha,
        s.hora_salida,
        s.vehiculo,
        s.comision,
        s.estado
    FROM tbl_salidas_ruta s
    INNER JOIN tbl_vendedores v ON s.id_vendedor = v.id_vendedor
    INNER JOIN tbl_rutas      r ON s.id_ruta     = r.id_ruta
    ORDER BY s.fecha DESC, s.hora_salida DESC;
END $$

-- ---- Liquidaciones pendientes de validación --------------------------------
DROP PROCEDURE IF EXISTS p_listar_liquidaciones_pendientes $$
CREATE PROCEDURE p_listar_liquidaciones_pendientes()
BEGIN
    SELECT
        l.id_liquidacion,
        l.id_salida,
        v.nombre            AS vendedor,
        r.nombre            AS ruta,
        l.total_vendido,
        l.comision_generada,
        l.dinero_entregado,
        l.diferencia,
        l.fecha_registro
    FROM tbl_liquidaciones_ruta l
    INNER JOIN tbl_salidas_ruta s ON l.id_salida   = s.id_salida
    INNER JOIN tbl_vendedores   v ON s.id_vendedor = v.id_vendedor
    INNER JOIN tbl_rutas        r ON s.id_ruta     = r.id_ruta
    WHERE l.id_validado_por IS NULL
    ORDER BY l.fecha_registro DESC;
END $$

DELIMITER ;
