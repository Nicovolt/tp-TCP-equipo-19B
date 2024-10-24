USE master;

IF NOT EXISTS(SELECT name FROM sysdatabases WHERE name = 'TP_CUATRIMESTRAL_DB')
BEGIN
    CREATE DATABASE TP_CUATRIMESTRAL_DB;
END

USE TP_CUATRIMESTRAL_DB;

-- Eliminar restricciones de clave foránea de Producto
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE TABLE_NAME = 'Producto' AND CONSTRAINT_TYPE = 'FOREIGN KEY')
BEGIN
    DECLARE @sql NVARCHAR(MAX) = '';
    SELECT @sql += 'ALTER TABLE Producto DROP CONSTRAINT ' + QUOTENAME(CONSTRAINT_NAME) + '; '
    FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
    WHERE TABLE_NAME = 'Producto' AND CONSTRAINT_TYPE = 'FOREIGN KEY';
    EXEC sp_executesql @sql;
END

-- Eliminar restricciones de clave foránea de Imagen
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE TABLE_NAME = 'Imagen' AND CONSTRAINT_TYPE = 'FOREIGN KEY')
BEGIN
    DECLARE @query NVARCHAR(MAX) = '';
    SELECT @query += 'ALTER TABLE Imagen DROP CONSTRAINT ' + QUOTENAME(CONSTRAINT_NAME) + '; '
    FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
    WHERE TABLE_NAME = 'Imagen' AND CONSTRAINT_TYPE = 'FOREIGN KEY';
    EXEC sp_executesql @query;
END

-- Eliminar tabla Producto primero debido a las restricciones de clave foránea
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Producto')
BEGIN
    DROP TABLE Producto;
END

-- Eliminar y crear tabla Cliente
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Cliente')
BEGIN
    DROP TABLE Cliente;
END
CREATE TABLE Cliente (
    id_cliente INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(90) NOT NULL,
    apellido VARCHAR(90) NOT NULL,
    email VARCHAR(250) NOT NULL UNIQUE,
    telefono VARCHAR(20),
    admin tinyint not null default 0
);

-- Eliminar y crear tabla Marca
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Marca')
BEGIN
    DROP TABLE Marca;
END
CREATE TABLE Marca (
    id_marca INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(90) NOT NULL
);

-- Eliminar y crear tabla Categoria
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Categoria')
BEGIN
    DROP TABLE Categoria;
END
CREATE TABLE Categoria (
    id_categoria INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(90) NOT NULL
);

-- Crear tabla Producto después de Marca y Categoria
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

-- Eliminar y crear tabla Home_banner
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Home_banner')
BEGIN
    DROP TABLE Home_banner;
END
CREATE TABLE Home_banner (
    id_banner INT IDENTITY(1,1) PRIMARY KEY,
    url_banner TEXT NOT NULL,
    activo TINYINT NOT NULL DEFAULT 0,
    id_cuenta INT NOT NULL,
    fecha DATETIME,
    orden INT
);

-- Eliminar y crear tabla Usuario
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Usuario')
BEGIN
    DROP TABLE Usuario;
END
CREATE TABLE Usuario(
    id_usuario INT IDENTITY(1,1) PRIMARY KEY,
    nombre_usuario VARCHAR(90) NOT NULL UNIQUE,
    contrasena VARBINARY(64) NOT NULL
);

-- Procedimiento almacenado sp_EncriptarContrasena
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_EncriptarContrasena')
BEGIN
    DROP PROCEDURE sp_EncriptarContrasena;
END
EXEC('
CREATE PROCEDURE sp_EncriptarContrasena
    @contrasena NVARCHAR(4000),
    @contrasenaEncriptada VARBINARY(64) OUTPUT
AS
BEGIN
    SET @contrasenaEncriptada = HASHBYTES(''SHA2_256'', @contrasena);
END
');

-- Procedimiento almacenado sp_InsertarUsuario
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InsertarUsuario')
BEGIN
    DROP PROCEDURE sp_InsertarUsuario;
END
EXEC('
CREATE PROCEDURE sp_InsertarUsuario
    @nombre_usuario NVARCHAR(90),
    @contrasena NVARCHAR(4000)
AS
BEGIN
    DECLARE @contrasenaEncriptada VARBINARY(64);
    EXEC sp_EncriptarContrasena @contrasena, @contrasenaEncriptada OUTPUT;

    INSERT INTO Usuario (nombre_usuario, contrasena)
    VALUES (@nombre_usuario, @contrasenaEncriptada);
END
');

-- Procedimiento almacenado sp_VerificarLogin
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_VerificarLogin')
BEGIN
    DROP PROCEDURE sp_VerificarLogin;
END
EXEC('
CREATE PROCEDURE sp_VerificarLogin
    @nombre_usuario NVARCHAR(90),
    @contrasena NVARCHAR(4000),
    @loginExitoso BIT OUTPUT
AS
BEGIN
    DECLARE @contrasenaEncriptada VARBINARY(64);
    EXEC sp_EncriptarContrasena @contrasena, @contrasenaEncriptada OUTPUT;

    IF EXISTS (
        SELECT 1
        FROM Usuario
        WHERE nombre_usuario = @nombre_usuario
        AND contrasena = @contrasenaEncriptada
    )
    BEGIN
        SET @loginExitoso = 1;
    END
    ELSE
    BEGIN
        SET @loginExitoso = 0;
    END
END
');

-- Eliminar y crear tabla Imagen
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Imagen')
BEGIN
    DROP TABLE Imagen;
END
CREATE TABLE Imagen (
    Id int IDENTITY(1,1) NOT NULL,
    IdProducto int FOREIGN KEY REFERENCES Producto(id_producto),
    ImagenUrl varchar(1000) COLLATE Modern_Spanish_CI_AS NOT NULL,
    CONSTRAINT PK_IMAGENES PRIMARY KEY (Id),
    activo TINYINT NOT NULL DEFAULT 0
);

-- Insert de datos

-- Categoria
INSERT INTO TP_CUATRIMESTRAL_DB.dbo.Categoria (nombre) VALUES
	 (N'Teclados'),
	 (N'Monitores'),
	 (N'Procesadores'),
	 (N'Motherboards'),
	 (N'Memorias Ram'),
	 (N'Notebooks'),
	 (N'Placas de Video'),
	 (N'Fuentes'),
	 (N'Almacenamiento'),
	 (N'Auriculares');
	
-- Marca
INSERT INTO TP_CUATRIMESTRAL_DB.dbo.Marca (nombre) VALUES
	 (N'Asus'),
	 (N'Wesdar'),
	 (N'Redragon'),
	 (N'Zotac'),
	 (N'Viewsonic'),
	 (N'Razer'),
	 (N'XFX'),
	 (N'Team'),
	 (N'AMD'),
	 (N'Antec');

-- Clientes
INSERT INTO TP_CUATRIMESTRAL_DB.dbo.Cliente (nombre,apellido,email,telefono,admin) VALUES
	 (N'Lucio',N'Garcia',N'luciog@gmail.com',N'1166112254',1),
	 (N'elba',N'lazo',N'elba@gmail.com',N'2626266559',0),
	 (N'Nicolas',N'Perez',N'nicop@hotmail.com',N'1548785968',1),
	 (N'Luka',N'Gallo',N'luka@yahoo.com',N'1458795841',1);
	
-- Producto
INSERT INTO TP_CUATRIMESTRAL_DB.dbo.Producto (nombre,descripcion,precio,id_marca,id_categoria,porcentaje_descuento,stock) VALUES
	 (N'Teclado ASUS ROG Strix M602 Falchion Ace',N'Teclado Mecanico RGB Switch Brown',96280.00,1,1,0,50),
	 (N'Teclado Wesdar MK10',N'Teclado Gaming Retroiluminado',11120.00,2,1,0,50),
	 (N'Teclado Redragon K585 Diti',N'Teclado Mecanico RGB Diti Switch Brown ',36360.00,3,1,0,50),
	 (N'Placa de Video Zotac GeForce RTX 4060 Ti',N'Placa de Video GeForce 16GB GDDR6 AMP',632930.00,4,7,0,15),
	 (N'Placa de Video XFX Radeon RX 6650 XT',N'Placa de Video XFX Radeon 8GB GDDR6 Speedster SWFT 210',362150.00,7,7,0,30),
	 (N'Monitor Curvo ViewSonic VX3218C-2K 32"',N'Monitor Gamer Curvo 1500R QHD 1440p 165Hz VA 1ms MPRT FreeSync Premium',538200.00,5,2,0,15),
	 (N'Auriculares Razer BLACKSHARK V2 PRO',N'Auriculares Wireless USB-C White PC/PS5/XBOX',322990.00,6,10,0,15),
	 (N'Memoria Team DDR5 32GB (2x16GB)',N'Memoria 32GB (2x16GB) 6400MHz T-Force Delta RGB Black CL40 Intel XMP 3.0 ',143580.00,8,5,0,100),
	 (N'Procesador AMD RYZEN 5 3600',N'Procesador 4.2GHz Turbo AM4 Wraith Stealth Cooler ',110000.00,9,3,0,70),
	 (N'Fuente Antec 550W',N'Fuente 80 Plus Bronze CSK550',68650.00,10,8,0,50);

-- Imagenes
INSERT INTO TP_CUATRIMESTRAL_DB.dbo.Imagen (IdProducto,ImagenUrl,activo) VALUES
	 (10,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_38277_Fuente_Antec_550W_80_Plus_Bronze_CSK550_67f87193-grn.jpg',0),
	 (9,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_16749_Procesador_AMD_RYZEN_5_3600_4.2GHz_Turbo_AM4_Wraith_Stealth_Cooler_f8ab4915-grn.jpg',0),
	 (8,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_29474_Memoria_Team_DDR5_32GB__2x16GB__6400MHz_T-Force_Delta_RGB_Black_CL40_Intel_XMP_3.0_884828e7-grn.jpg',0),
	 (8,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_29475_Memoria_Team_DDR5_32GB__2x16GB__6400MHz_T-Force_Delta_RGB_Black_CL40_Intel_XMP_3.0_3e9ff9b6-grn.jpg',0),
	 (5,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_33401_Placa_de_Video_XFX_Radeon_RX_6650_XT_8GB_GDDR6_Speedster_SWFT_210_589f396b-grn.jpg',0),
	 (5,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_33402_Placa_de_Video_XFX_Radeon_RX_6650_XT_8GB_GDDR6_Speedster_SWFT_210_59981144-grn.jpg',1),
	 (5,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_33403_Placa_de_Video_XFX_Radeon_RX_6650_XT_8GB_GDDR6_Speedster_SWFT_210_de9bc156-grn.jpg',0),
	 (6,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_41549_Monitor_Gamer_Curvo_ViewSonic_VX3218C-2K_32__1500R_QHD_1440p_165Hz_VA_1ms_MPRT_FreeSync_Premium_e66d7f5f-grn.jpg',0),
	 (6,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_41551_Monitor_Gamer_Curvo_ViewSonic_VX3218C-2K_32__1500R_QHD_1440p_165Hz_VA_1ms_MPRT_FreeSync_Premium_76a0d126-grn.jpg',0),
	 (6,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_41550_Monitor_Gamer_Curvo_ViewSonic_VX3218C-2K_32__1500R_QHD_1440p_165Hz_VA_1ms_MPRT_FreeSync_Premium_e7f78e55-grn.jpg',0);
INSERT INTO TP_CUATRIMESTRAL_DB.dbo.Imagen (IdProducto,ImagenUrl,activo) VALUES
	 (4,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_41330_Placa_de_Video_Zotac_GeForce_RTX_4060_Ti_16GB_GDDR6_AMP_b55acf2f-grn.jpg',0),
	 (4,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_41331_Placa_de_Video_Zotac_GeForce_RTX_4060_Ti_16GB_GDDR6_AMP_0d8d4ec7-grn.jpg',1),
	 (4,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_41333_Placa_de_Video_Zotac_GeForce_RTX_4060_Ti_16GB_GDDR6_AMP_ed981310-grn.jpg',0),
	 (4,N'https://imagenes.compragamer.com/productos/compragamer_Imganen_general_41334_Placa_de_Video_Zotac_GeForce_RTX_4060_Ti_16GB_GDDR6_AMP_d93bcd40-grn.jpg',0);
