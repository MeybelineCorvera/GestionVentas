-- =====================================================================
-- LABORATORIO 3 - Modificaciones a la base GestionVentasDb
-- Se ejecuta DESPUES del script del Laboratorio 2 (Script_Lab2_GestionVentasDb.sql).
-- NO borra datos: solo agrega a Empleados las columnas del LOGIN (usuario y clave cifrada).
-- Ejecutar en SQL Server Management Studio (SSMS), completo o por partes.
-- =====================================================================
USE GestionVentasDb;
GO

-- 1) Columnas nuevas (primero permiten NULL para no romper las filas que ya existen)
ALTER TABLE Empleados ADD usuario VARCHAR(30) NULL,
                          clave   CHAR(64)    NULL;
GO

-- 2) Usuario y clave de cada empleado. La clave se guarda como hash SHA-256 (nunca en texto plano)
--      Rafael    -> rcruz     / rafael123
--      Meybeline -> mcorvera  / meybeline123
--      Fernando  -> fcruz     / fernando123
UPDATE Empleados SET usuario = 'rcruz',    clave = CONVERT(CHAR(64), HASHBYTES('SHA2_256', 'rafael123'),    2) WHERE id_empleado = 1;
UPDATE Empleados SET usuario = 'mcorvera', clave = CONVERT(CHAR(64), HASHBYTES('SHA2_256', 'meybeline123'), 2) WHERE id_empleado = 2;
UPDATE Empleados SET usuario = 'fcruz',    clave = CONVERT(CHAR(64), HASHBYTES('SHA2_256', 'fernando123'), 2) WHERE id_empleado = 3;
GO

-- 3) Ahora que todas las filas tienen valor, las columnas pasan a ser obligatorias y el usuario unico
ALTER TABLE Empleados ALTER COLUMN usuario VARCHAR(30) NOT NULL;
ALTER TABLE Empleados ALTER COLUMN clave   CHAR(64)    NOT NULL;
ALTER TABLE Empleados ADD CONSTRAINT UQ_Empleados_Usuario UNIQUE (usuario);
GO

-- 4) (OPCIONAL) Dejar algunos productos con stock bajo para ver el SEMAFORO del bonus
--    rojo = 5 o menos, ambar = de 6 a 10
UPDATE Productos SET stock = 4 WHERE id_producto = 7;   -- Smart TV -> rojo
UPDATE Productos SET stock = 5 WHERE id_producto = 8;   -- Tablet   -> rojo
UPDATE Productos SET stock = 9 WHERE id_producto = 1;   -- Laptop   -> ambar
GO

-- Verificacion: cada empleado debe tener su usuario y una clave de 64 caracteres
SELECT id_empleado, nombres, usuario, clave, LEN(clave) AS largo_clave FROM Empleados;
GO
