USE master;

IF NOT EXISTS(SELECT name FROM sysdatabases WHERE name = 'TP_CUATRIMESTRAL_DB')
BEGIN
    CREATE DATABASE TP_CUATRIMESTRAL_DB;
END

USE TP_CUATRIMESTRAL_DB;

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Cliente')
BEGIN
    CREATE TABLE Cliente (
        id_cliente INT IDENTITY(1,1) PRIMARY KEY,
        nombre VARCHAR(90) NOT NULL,
        apellido VARCHAR(90) NOT NULL,
        email VARCHAR(250) NOT NULL UNIQUE,
        telefono VARCHAR(20)
    );
END
ELSE
BEGIN
    IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Cliente' AND COLUMN_NAME = 'admin')
    BEGIN
        ALTER TABLE Cliente ADD admin TINYINT NOT NULL DEFAULT 0;
    END
END

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Marca')
BEGIN
    CREATE TABLE Marca (
        id_marca INT IDENTITY(1,1) PRIMARY KEY,
        nombre VARCHAR(90) NOT NULL
    );
END

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Categoria')
BEGIN
    CREATE TABLE Categoria (
        id_categoria INT IDENTITY(1,1) PRIMARY KEY,
        nombre VARCHAR(90) NOT NULL
    );
END

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Producto')
BEGIN
    CREATE TABLE Producto (
        id_producto INT IDENTITY(1,1) PRIMARY KEY,
        nombre VARCHAR(155) NOT NULL,
        descripcion VARCHAR(250),
        precio DECIMAL(10,2) NOT NULL,
        id_marca INT FOREIGN KEY REFERENCES Marca(id_marca),
        id_categoria INT FOREIGN KEY REFERENCES Categoria(id_categoria),
        porcentaje_descuento TINYINT DEFAULT 0,
        stock INT NOT NULL DEFAULT 0
    );
END

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Home_banner')
BEGIN
    CREATE TABLE Home_banner (
        id_banner INT IDENTITY(1,1) PRIMARY KEY,
        url_banner TEXT NOT NULL,
        activo TINYINT NOT NULL DEFAULT 0,
        id_cuenta INT NOT NULL,
        fecha DATETIME,
        orden INT
    );
END

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Usuario')
BEGIN
	CREATE TABLE Usuario(
	id_usuario INT IDENTITY(1,1) PRIMARY KEY,
	nombre_usuario VARCHAR(90) NOT NULL UNIQUE,
    contrasena VARBINARY(64) NOT NULL
);
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_EncriptarContrasena')
BEGIN
    EXEC('
    CREATE PROCEDURE sp_EncriptarContrasena
        @contrasena NVARCHAR(4000),
        @contrasenaEncriptada VARBINARY(64) OUTPUT
    AS
    BEGIN
        SET @contrasenaEncriptada = HASHBYTES(''SHA2_256'', @contrasena);
    END
    ');
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InsertarUsuario')
BEGIN
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
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_VerificarLogin')
BEGIN
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
END
