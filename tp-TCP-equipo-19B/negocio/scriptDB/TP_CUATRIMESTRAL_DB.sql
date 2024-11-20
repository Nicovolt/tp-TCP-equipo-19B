USE master;
GO

-- Eliminar la base de datos si existe
IF EXISTS(SELECT name FROM sys.databases WHERE name = 'TP_CUATRIMESTRAL_DB')
BEGIN
    ALTER DATABASE TP_CUATRIMESTRAL_DB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE TP_CUATRIMESTRAL_DB;
END
GO

CREATE DATABASE TP_CUATRIMESTRAL_DB;
GO

USE TP_CUATRIMESTRAL_DB;
GO

-- Crear las tablas base (sin dependencias)
CREATE TABLE Cliente (
    id_cliente INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(90) NOT NULL,
    apellido VARCHAR(90) NOT NULL,
    email VARCHAR(250) NOT NULL UNIQUE,
    telefono VARCHAR(20)
);

CREATE TABLE Marca (
    id_marca INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(90) NOT NULL
);

CREATE TABLE Categoria (
    id_categoria INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(90) NOT NULL
);

CREATE TABLE envio_tipo (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(90) NOT NULL,
    url_imagen VARCHAR(255),
    costo DECIMAL(10,2) NOT NULL DEFAULT 0,
    activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE presupuesto_estado (
    id SMALLINT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    descripcion VARCHAR(150),
    final BIT NOT NULL DEFAULT 0,
    cancelado BIT NOT NULL DEFAULT 0,
    vencido BIT NOT NULL DEFAULT 0,
    orden SMALLINT NOT NULL DEFAULT 0
);

CREATE TABLE presupuesto_forma_pago (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    descripcion VARCHAR(150),
    activo BIT NOT NULL DEFAULT 1
);

-- Crear tablas con dependencias nivel 1
CREATE TABLE Usuario (
    id_usuario INT IDENTITY(1,1) PRIMARY KEY,
    id_cliente INT FOREIGN KEY REFERENCES Cliente(id_cliente),
    contrasena VARBINARY(64) NOT NULL,
    admin bit NOT NULL DEFAULT 0
);

CREATE TABLE cliente_dom_envio (
    id INT IDENTITY(1,1) PRIMARY KEY,
    id_cliente INT NOT NULL FOREIGN KEY REFERENCES Cliente(id_cliente),
    calle VARCHAR(150) NOT NULL,
    entre_calles VARCHAR(150),
    altura INT NOT NULL,
    piso INT,
    departamento VARCHAR(20),
    localidad VARCHAR(150) NOT NULL,
    provincia VARCHAR(150) NOT NULL,
    cp VARCHAR(10) NOT NULL,
    observaciones VARCHAR(255),
    activo BIT NOT NULL DEFAULT 1,
    fecha_creacion DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Producto (
    id_producto INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(155) NOT NULL,
    descripcion VARCHAR(250),
    precio DECIMAL(10,2) NOT NULL,
    id_marca INT FOREIGN KEY REFERENCES Marca(id_marca),
    id_categoria INT FOREIGN KEY REFERENCES Categoria(id_categoria),
    porcentaje_descuento TINYINT DEFAULT 0,
    stock INT NOT NULL DEFAULT 0,
    activo TINYINT NOT NULL DEFAULT 0
);

-- Crear tablas con dependencias nivel 2
CREATE TABLE presupuesto (
    id INT IDENTITY(1,1) PRIMARY KEY,
    id_cliente INT NOT NULL FOREIGN KEY REFERENCES Cliente(id_cliente),
    id_metodo_envio INT NOT NULL FOREIGN KEY REFERENCES envio_tipo(id),
    id_estado SMALLINT NOT NULL FOREIGN KEY REFERENCES presupuesto_estado(id),
    id_forma_pago INT NOT NULL FOREIGN KEY REFERENCES presupuesto_forma_pago(id),
    fecha_creacion DATETIME NOT NULL DEFAULT GETDATE(),
    fecha_validez DATETIME NOT NULL,
    id_cliente_envio INT NOT NULL FOREIGN KEY REFERENCES cliente_dom_envio(id),
    costo_envio DECIMAL(10,2) NOT NULL DEFAULT 0,
    total DECIMAL(10,2) NOT NULL DEFAULT 0,
    ultima_actualizacion DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Imagen (
    Id int IDENTITY(1,1) PRIMARY KEY,
    IdProducto int FOREIGN KEY REFERENCES Producto(id_producto),
    ImagenUrl varchar(1000) COLLATE Modern_Spanish_CI_AS NOT NULL,
    activo TINYINT NOT NULL DEFAULT 0
);

-- Crear tablas con dependencias nivel 3
CREATE TABLE presupuesto_detalle (
    id INT IDENTITY(1,1) PRIMARY KEY,
    id_presupuesto INT NOT NULL FOREIGN KEY REFERENCES presupuesto(id),
    id_producto INT NOT NULL FOREIGN KEY REFERENCES Producto(id_producto),
    precio_unitario DECIMAL(10,2) NOT NULL,
    cantidad INT NOT NULL,
    subtotal DECIMAL(10,2) NOT NULL,
    fecha_agregado DATETIME NOT NULL DEFAULT GETDATE(),
    agregado_id_usuario INT NOT NULL FOREIGN KEY REFERENCES Usuario(id_usuario)
);

-- Crear stored procedures para el manejo de usuarios
CREATE PROCEDURE sp_EncriptarContrasena
    @contrasena NVARCHAR(4000),
    @contrasenaEncriptada VARBINARY(64) OUTPUT
AS
BEGIN
    SET @contrasenaEncriptada = HASHBYTES('SHA2_256', @contrasena);
END;
GO

CREATE PROCEDURE sp_InsertarUsuario
    @idCliente int,
    @contrasena NVARCHAR(4000)
AS
BEGIN
    DECLARE @contrasenaEncriptada VARBINARY(64);
    EXEC sp_EncriptarContrasena @contrasena, @contrasenaEncriptada OUTPUT;

    INSERT INTO Usuario (id_cliente, contrasena)
    VALUES (@idCliente, @contrasenaEncriptada);
END;
GO

CREATE PROCEDURE sp_VerificarLogin
    @id_cliente INT,
    @contrasena NVARCHAR(4000),
    @loginExitoso BIT OUTPUT
AS
BEGIN
    DECLARE @contrasenaEncriptada VARBINARY(64);
    EXEC sp_EncriptarContrasena @contrasena, @contrasenaEncriptada OUTPUT;

    IF EXISTS (
        SELECT 1
        FROM Usuario
        WHERE id_cliente = @id_cliente
        AND contrasena = @contrasenaEncriptada
    )
    BEGIN
        SET @loginExitoso = 1;
    END
    ELSE
    BEGIN
        SET @loginExitoso = 0;
    END
END;
GO

-- Crear índices
CREATE INDEX IX_presupuesto_fechas ON presupuesto(fecha_creacion, fecha_validez);
CREATE INDEX IX_presupuesto_cliente ON presupuesto(id_cliente);
CREATE INDEX IX_presupuesto_estado ON presupuesto(id_estado);
CREATE INDEX IX_domicilio_cliente ON cliente_dom_envio(id_cliente);

-- Insertar datos iniciales
-- Categoria
INSERT INTO Categoria (nombre) VALUES
    ('Teclados'),
    ('Monitores'),
    ('Procesadores'),
    ('Motherboards'),
    ('Memorias Ram'),
    ('Notebooks'),
    ('Placas de Video'),
    ('Fuentes'),
    ('Almacenamiento'),
    ('Auriculares');

-- Marca
INSERT INTO Marca (nombre) VALUES
    ('Asus'),
    ('Wesdar'),
    ('Redragon'),
    ('Zotac'),
    ('Viewsonic'),
    ('Razer'),
    ('XFX'),
    ('Team'),
    ('AMD'),
    ('Antec');

-- Clientes
INSERT INTO Cliente (nombre, apellido, email, telefono) VALUES
    ('Lucio', 'Garcia', 'luciog@gmail.com', '1166112254'),
    ('elba', 'lazo', 'elba@gmail.com', '2626266559'),
    ('Nicolas', 'Perez', 'nicop@hotmail.com', '1548785968'),
    ('Luka', 'Gallo', 'luka@yahoo.com', '1458795841');

   SET IDENTITY_INSERT presupuesto_estado ON;
INSERT INTO presupuesto_estado (id, nombre, descripcion, final, cancelado, vencido, orden) VALUES
    (1, 'Creado', 'Presupuesto creado', 0, 0, 0, 1),
    (2, 'Pagado', 'Presupuesto pagado por el cliente', 0, 0, 0, 2),
    (3, 'Vencido', 'Presupuesto vencido por falta de pago', 1, 0, 1, 3),
    (4, 'Cancelado', 'Presupuesto cancelado', 1, 1, 0, 4),
    (5, 'Armado', 'El pedido se encuentra armado en nuestras instalaciones', 0, 0, 0, 5),
    (6, 'Embalado', 'El pedido se encuentra embalado en nuestras instalaciones', 0, 0, 0, 6),
    (7, 'Despachado', 'El pedido se encuentra despachado', 1, 0, 0, 7),
    (8, 'Entregado', 'El pedido fue entregado al cliente', 1, 0, 0, 8);
SET IDENTITY_INSERT presupuesto_estado OFF;

-- Insertar formas de pago
SET IDENTITY_INSERT presupuesto_forma_pago ON;
INSERT INTO presupuesto_forma_pago (id, nombre, descripcion, activo) VALUES
    (1, 'Efectivo', 'Se abono en efectivo en la sucursal', 1),
    (2, 'Transferencia bancaria', 'Se abono mediante el metodo de pago transferencia bancaria', 1),
    (3, 'Deposito', 'Se abono mediante el metodo de pago deposito', 1),
    (4, 'MercadoPago', 'Se abono mediante la plataforma Mercado Pago', 1);
SET IDENTITY_INSERT presupuesto_forma_pago OFF;

-- Insertar tipos de envío
SET IDENTITY_INSERT envio_tipo ON;
INSERT INTO envio_tipo (id, nombre, url_imagen, costo, activo) VALUES
    (1, 'OCA', 'https://pbs.twimg.com/profile_images/1561766269333999616/eKl7zcmD_400x400.jpg', 1500.00, 1),
    (2, 'Andreani', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSNgTsD8HwijtWC8UrLCFPyUBy0ZO1LcWbaBQ&s', 2000.00, 1),
    (3, 'MercadoEnvios', 'https://zonaflex.com.ar/wp-content/uploads/2021/08/Envios-flex-moto-mensajerias.jpg', 1000.00, 1);
SET IDENTITY_INSERT envio_tipo OFF;

-- Insertar productos
INSERT INTO Producto (nombre, descripcion, precio, id_marca, id_categoria, porcentaje_descuento, stock) VALUES
    ('Teclado ASUS ROG Strix M602 Falchion Ace', 'Teclado Mecanico RGB Switch Brown', 96280.00, 1, 1, 0, 50),
    ('Teclado Wesdar MK10', 'Teclado Gaming Retroiluminado', 11120.00, 2, 1, 0, 50),
    ('Teclado Redragon K585 Diti', 'Teclado Mecanico RGB Diti Switch Brown', 36360.00, 3, 1, 0, 50),
    ('Placa de Video Zotac GeForce RTX 4060 Ti', 'Placa de Video GeForce 16GB GDDR6 AMP', 632930.00, 4, 7, 0, 15),
    ('Placa de Video XFX Radeon RX 6650 XT', 'Placa de Video XFX Radeon 8GB GDDR6 Speedster SWFT 210', 362150.00, 7, 7, 0, 30),
    ('Monitor Curvo ViewSonic VX3218C-2K 32"', 'Monitor Gamer Curvo 1500R QHD 1440p 165Hz VA 1ms MPRT FreeSync Premium', 538200.00, 5, 2, 0, 15),
    ('Auriculares Razer BLACKSHARK V2 PRO', 'Auriculares Wireless USB-C White PC/PS5/XBOX', 322990.00, 6, 10, 0, 15),
    ('Memoria Team DDR5 32GB (2x16GB)', 'Memoria 32GB (2x16GB) 6400MHz T-Force Delta RGB Black CL40 Intel XMP 3.0', 143580.00, 8, 5, 0, 100),
    ('Procesador AMD RYZEN 5 3600', 'Procesador 4.2GHz Turbo AM4 Wraith Stealth Cooler', 110000.00, 9, 3, 0, 70),
    ('Fuente Antec 550W', 'Fuente 80 Plus Bronze CSK550', 68650.00, 10, 8, 0, 50);

-- Insertar usuarios (con contraseñas encriptadas)
INSERT INTO Usuario (id_cliente, contrasena, admin) VALUES
    (1, 0xBFE28E20AA89715A67BDBC4BB5DBE45D878D05E21ADFBEBBC91E437446F015EA, 1),
    (2, 0xCC842563132899AC0068C315748E77030C8DE04B1AEB4B1BF19F313F5AB0E3E7, 0),
    (3, 0x22B69DE3D8798A782DB25A601EEAFF0E9C0433A90830C06B6201CC726A61F057, 1),
    (4, 0x347E23E66EDBCC4163682EDF410031121E7EF19557E018A7E7FC16C9407E8F5A, 1);

-- Imagenes
INSERT INTO TP_CUATRIMESTRAL_DB.dbo.Imagen (IdProducto,ImagenUrl,activo) VALUES
	 (10,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_38277_Fuente_Antec_550W_80_Plus_Bronze_CSK550_67f87193-grn.jpg',1),
	 (9,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_16749_Procesador_AMD_RYZEN_5_3600_4.2GHz_Turbo_AM4_Wraith_Stealth_Cooler_f8ab4915-grn.jpg',1),
	 (8,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_29474_Memoria_Team_DDR5_32GB__2x16GB__6400MHz_T-Force_Delta_RGB_Black_CL40_Intel_XMP_3.0_884828e7-grn.jpg',1),
	 (8,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_29475_Memoria_Team_DDR5_32GB__2x16GB__6400MHz_T-Force_Delta_RGB_Black_CL40_Intel_XMP_3.0_3e9ff9b6-grn.jpg',1),
	 (5,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_33401_Placa_de_Video_XFX_Radeon_RX_6650_XT_8GB_GDDR6_Speedster_SWFT_210_589f396b-grn.jpg',1),
	 (5,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_33402_Placa_de_Video_XFX_Radeon_RX_6650_XT_8GB_GDDR6_Speedster_SWFT_210_59981144-grn.jpg',1),
	 (5,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_33403_Placa_de_Video_XFX_Radeon_RX_6650_XT_8GB_GDDR6_Speedster_SWFT_210_de9bc156-grn.jpg',1),
	 (6,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_41549_Monitor_Gamer_Curvo_ViewSonic_VX3218C-2K_32__1500R_QHD_1440p_165Hz_VA_1ms_MPRT_FreeSync_Premium_e66d7f5f-grn.jpg',1),
	 (6,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_41551_Monitor_Gamer_Curvo_ViewSonic_VX3218C-2K_32__1500R_QHD_1440p_165Hz_VA_1ms_MPRT_FreeSync_Premium_76a0d126-grn.jpg',1),
	 (6,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_41550_Monitor_Gamer_Curvo_ViewSonic_VX3218C-2K_32__1500R_QHD_1440p_165Hz_VA_1ms_MPRT_FreeSync_Premium_e7f78e55-grn.jpg',1);
INSERT INTO TP_CUATRIMESTRAL_DB.dbo.Imagen (IdProducto,ImagenUrl,activo) VALUES
	 (4,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_41330_Placa_de_Video_Zotac_GeForce_RTX_4060_Ti_16GB_GDDR6_AMP_b55acf2f-grn.jpg',1),
	 (4,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_41331_Placa_de_Video_Zotac_GeForce_RTX_4060_Ti_16GB_GDDR6_AMP_0d8d4ec7-grn.jpg',1),
	 (4,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_41333_Placa_de_Video_Zotac_GeForce_RTX_4060_Ti_16GB_GDDR6_AMP_ed981310-grn.jpg',1),
	 (4,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_41334_Placa_de_Video_Zotac_GeForce_RTX_4060_Ti_16GB_GDDR6_AMP_d93bcd40-grn.jpg',1),
	 (1,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_42536_Teclado_Mecanico_ASUS_ROG_Strix_M602_Falchion_Ace_RGB_Switch_Red__db773696-grn.jpg',1),
	 (1,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_42537_Teclado_Mecanico_ASUS_ROG_Strix_M602_Falchion_Ace_RGB_Switch_Red__08ddd666-grn.jpg',1),
	 (1,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_42538_Teclado_Mecanico_ASUS_ROG_Strix_M602_Falchion_Ace_RGB_Switch_Red__d986ca57-grn.jpg',1),
	 (2,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_34677_Teclado_Gaming_Retroiluminado_Wesdar_MK10_0ac28ff2-grn.jpg',1),
	 (2,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_34678_Teclado_Gaming_Retroiluminado_Wesdar_MK10_edd2ef82-grn.jpg',1),
	 (7,N'https://lezamapc.com.ar/50123-large_default/auricular-razer-blackshark-v2-pro-wireless-playstation-licensed-white.jpg',1),
	 (7,N'https://http2.mlstatic.com/D_NQ_NP_628847-MLU71479173112_092023-O.webp',1),
	 (3,N'https://redragon.es/content/uploads/2021/04/DITI.png',1);

