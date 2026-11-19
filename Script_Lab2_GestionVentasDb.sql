-- =====================================================================
-- LABORATORIO 2 - Base de datos de Gestion de Ventas (GestionVentasDb)

CREATE DATABASE GestionVentasDb;
GO
USE GestionVentasDb;
GO

-- ---------------------------------------------------------------------
-- CREACION DE TABLAS
-- ---------------------------------------------------------------------

-- TABLA CLIENTES
CREATE TABLE Clientes (
    id_cliente  INT IDENTITY(1,1) PRIMARY KEY,
    nombres     VARCHAR(50)  NOT NULL,
    apellidos   VARCHAR(50)  NOT NULL,
    dui         CHAR(10)     NULL,
    telefono    VARCHAR(15)  NULL,
    correo      VARCHAR(100) NULL,
    direccion   VARCHAR(150) NULL,
    CONSTRAINT UQ_Clientes_DUI    UNIQUE (dui),
    CONSTRAINT UQ_Clientes_Correo UNIQUE (correo)
);
GO

-- TABLA EMPLEADOS
CREATE TABLE Empleados (
    id_empleado        INT IDENTITY(1,1) PRIMARY KEY,
    nombres            VARCHAR(50)  NOT NULL,
    apellidos          VARCHAR(50)  NOT NULL,
    cargo              VARCHAR(50)  NOT NULL,
    telefono           VARCHAR(15)  NULL,
    correo             VARCHAR(100) NULL,
    fecha_contratacion DATE         NOT NULL,
    CONSTRAINT UQ_Empleados_Correo UNIQUE (correo)
);
GO

-- TABLA PRODUCTOS
CREATE TABLE Productos (
    id_producto INT IDENTITY(1,1) PRIMARY KEY,
    nombre      VARCHAR(100)  NOT NULL,
    marca       VARCHAR(50)   NOT NULL,
    descripcion VARCHAR(200)  NULL,
    precio      DECIMAL(10,2) NOT NULL,
    stock       INT           NOT NULL CONSTRAINT DF_Productos_Stock DEFAULT 0,
    CONSTRAINT CK_Productos_Precio CHECK (precio > 0),
    CONSTRAINT CK_Productos_Stock  CHECK (stock >= 0)
);
GO

-- TABLA VENTAS (cabecera) - el total NO se guarda, se calcula
CREATE TABLE Ventas (
    id_venta    INT IDENTITY(1,1) PRIMARY KEY,
    fecha_venta DATETIME NOT NULL CONSTRAINT DF_Ventas_Fecha DEFAULT GETDATE(),
    id_cliente  INT NOT NULL,
    id_empleado INT NOT NULL,
    CONSTRAINT FK_Ventas_Clientes  FOREIGN KEY (id_cliente)  REFERENCES Clientes(id_cliente),
    CONSTRAINT FK_Ventas_Empleados FOREIGN KEY (id_empleado) REFERENCES Empleados(id_empleado)
);
GO

-- 5. DETALLEVENTA (resuelve la relacion N:M entre Ventas y Productos)
CREATE TABLE DetalleVenta (
    id_detalle      INT IDENTITY(1,1) PRIMARY KEY,
    id_venta        INT           NOT NULL,
    id_producto     INT           NOT NULL,
    cantidad        INT           NOT NULL,
    precio_unitario DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_Detalle_Ventas    FOREIGN KEY (id_venta)    REFERENCES Ventas(id_venta),
    CONSTRAINT FK_Detalle_Productos FOREIGN KEY (id_producto) REFERENCES Productos(id_producto),
    CONSTRAINT CK_Detalle_Cantidad  CHECK (cantidad > 0),
    CONSTRAINT CK_Detalle_Precio    CHECK (precio_unitario > 0),
    CONSTRAINT UQ_Detalle_VentaProducto UNIQUE (id_venta, id_producto)
);
GO

-- ---------------------------------------------------------------------
-- INSERCION DE DATOS
-- ---------------------------------------------------------------------
INSERT INTO Clientes (nombres, apellidos, dui, telefono, correo, direccion) VALUES
('Carlos Alberto', 'Hernández Ruiz',  '01234567-8', '7123-4567', 'carlos.hernandez@mail.com', 'Col. Escalón, San Salvador'),
('María José',     'López Castro',    '02345678-9', '7234-5678', 'maria.lopez@mail.com',      'Barrio El Centro, San Miguel'),
('José Antonio',   'Martínez Flores', '03456789-0', '7345-6789', 'jose.martinez@mail.com',    'Col. Ciudad Jardín, San Miguel'),
('Ana Gabriela',   'Gómez Portillo',  '04567890-1', '7456-7890', 'ana.gomez@mail.com',        'Usulután, Usulután'),
('Luis Fernando',  'Ramírez Díaz',    '05678901-2', '7567-8901', 'luis.ramirez@mail.com',     'Santa Ana, Santa Ana');

INSERT INTO Empleados (nombres, apellidos, cargo, telefono, correo, fecha_contratacion) VALUES
('Rafael',    'Cruz',          'Vendedor',  '7611-2233', 'rafael.cruz@tienda.com',       '2024-02-15'),
('Meybeline', 'Corvera Rivas', 'Vendedora', '7622-3344', 'meybeline.corvera@tienda.com', '2023-08-01'),
('Fernando',  'Cruz Aguilar',  'Cajero',    '7633-4455', 'fernando.cruz@tienda.com',     '2025-01-10');

INSERT INTO Productos (nombre, marca, descripcion, precio, stock) VALUES
('Laptop IdeaPad 3',        'Lenovo',   'Laptop 15.6", 8GB RAM, 512GB SSD', 549.99, 10),
('Smartphone Galaxy A55',   'Samsung',  'Celular 128GB, 8GB RAM',           399.00, 15),
('Audífonos WH-CH520',      'Sony',     'Audífonos inalámbricos Bluetooth',  59.99, 25),
('Monitor 24" 24MH400',     'LG',       'Monitor IPS Full HD 100Hz',        129.50, 12),
('Mouse inalámbrico M185',  'Logitech', 'Mouse inalámbrico USB',             19.99, 40),
('Teclado mecánico Humara', 'Redragon', 'Teclado mecánico retroiluminado',   45.00, 20),
('Smart TV 50" 4K',         'TCL',      'Televisor 4K UHD Google TV',       349.00,  8),
('Tablet Redmi Pad SE',     'Xiaomi',   'Tablet 11", 128GB',                189.99, 10);

INSERT INTO Ventas (fecha_venta, id_cliente, id_empleado) VALUES
('2026-09-01 10:15', 1, 1),
('2026-09-03 14:30', 2, 2),
('2026-09-05 11:00', 1, 3),
('2026-09-10 16:45', 3, 1),
('2026-09-12 09:20', 4, 2),
('2026-09-15 13:10', 2, 1),
('2026-09-20 17:05', 5, 3),
('2026-09-25 15:40', 3, 2);

INSERT INTO DetalleVenta (id_venta, id_producto, cantidad, precio_unitario) VALUES
(1, 1, 1, 549.99), (1, 5, 1, 19.99),
(2, 2, 1, 399.00), (2, 3, 2, 59.99),
(3, 7, 1, 349.00),
(4, 4, 2, 129.50), (4, 6, 1, 45.00),
(5, 8, 1, 189.99),
(6, 3, 1, 59.99),  (6, 5, 2, 19.99),
(7, 6, 1, 45.00),  (7, 5, 1, 19.99),
(8, 2, 1, 399.00);
GO

-- ---------------------------------------------------------------------
-- CONSULTAS SELECT / UPDATE
-- ---------------------------------------------------------------------
SELECT * FROM Clientes;
SELECT * FROM Empleados;
SELECT * FROM Productos;
SELECT * FROM Ventas;
SELECT * FROM DetalleVenta;
GO

-- Ventas con cliente, empleado y TOTAL calculado en tiempo de consulta
SELECT v.id_venta,
       v.fecha_venta,
       c.nombres + ' ' + c.apellidos AS cliente,
       e.nombres + ' ' + e.apellidos AS empleado,
       SUM(d.cantidad * d.precio_unitario) AS total_venta
FROM Ventas v
INNER JOIN Clientes c      ON v.id_cliente  = c.id_cliente
INNER JOIN Empleados e     ON v.id_empleado = e.id_empleado
INNER JOIN DetalleVenta d  ON v.id_venta    = d.id_venta
GROUP BY v.id_venta, v.fecha_venta, c.nombres, c.apellidos, e.nombres, e.apellidos
ORDER BY v.id_venta;

-- Detalle de una venta especifica (productos, cantidad, subtotal)
SELECT d.id_venta,
       p.nombre AS producto,
       p.marca,
       d.cantidad,
       d.precio_unitario,
       d.cantidad * d.precio_unitario AS subtotal
FROM DetalleVenta d
INNER JOIN Productos p ON d.id_producto = p.id_producto
WHERE d.id_venta = 2;

-- Ventas registradas por cada empleado
SELECT e.nombres + ' ' + e.apellidos AS empleado,
       COUNT(v.id_venta) AS ventas_registradas
FROM Empleados e
LEFT JOIN Ventas v ON e.id_empleado = v.id_empleado
GROUP BY e.id_empleado, e.nombres, e.apellidos
ORDER BY ventas_registradas DESC;

-- UPDATE: modificar precio y stock de un producto
UPDATE Productos SET precio = 529.99, stock = stock - 1 WHERE id_producto = 1;
SELECT * FROM Productos WHERE id_producto = 1;
GO

-- ---------------------------------------------------------------------
-- VERIFICACION: ventas que NO tienen ningun producto (debe devolver 0 filas)
-- ---------------------------------------------------------------------
SELECT v.id_venta, v.fecha_venta
FROM Ventas v
LEFT JOIN DetalleVenta d ON v.id_venta = d.id_venta
WHERE d.id_detalle IS NULL;
GO

-- ---------------------------------------------------------------------
-- BONUS: los 3 clientes que mas dinero han gastado en la tienda
-- ---------------------------------------------------------------------
SELECT TOP 3
       c.nombres + ' ' + c.apellidos          AS nombre_completo,
       COUNT(DISTINCT v.id_venta)             AS cantidad_ventas,
       SUM(d.cantidad * d.precio_unitario)    AS total_gastado
FROM Clientes c
INNER JOIN Ventas v        ON c.id_cliente = v.id_cliente
INNER JOIN DetalleVenta d  ON v.id_venta   = d.id_venta
GROUP BY c.id_cliente, c.nombres, c.apellidos
ORDER BY total_gastado DESC;
GO

-- ---------------------------------------------------------------------
-- PRUEBAS DE REGLAS DE NEGOCIO (cada una DEBE dar ERROR; es lo esperado)
-- ---------------------------------------------------------------------
-- CHECK: cantidad debe ser > 0 -> error CK_Detalle_Cantidad
INSERT INTO DetalleVenta (id_venta, id_producto, cantidad, precio_unitario) VALUES (1, 2, 0, 399.00);

-- CHECK: precio_unitario debe ser > 0 -> error CK_Detalle_Precio
INSERT INTO DetalleVenta (id_venta, id_producto, cantidad, precio_unitario) VALUES (1, 3, 1, -5.00);

-- FK: no existe el cliente 99 -> error FK_Ventas_Clientes
INSERT INTO Ventas (fecha_venta, id_cliente, id_empleado) VALUES ('2026-10-01', 99, 1);

-- FK: no existe el producto 50 -> error FK_Detalle_Productos
INSERT INTO DetalleVenta (id_venta, id_producto, cantidad, precio_unitario) VALUES (1, 50, 1, 10.00);

-- NOT NULL: venta sin empleado -> error
INSERT INTO Ventas (fecha_venta, id_cliente, id_empleado) VALUES ('2026-10-01', 1, NULL);
GO
