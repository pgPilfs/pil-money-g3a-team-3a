/* Configuracion del script de la base de datos */
/* DDL */
USE [master];
if EXISTS(SELECT * FROM sys.databases WHERE name='PilMoney')
DROP DATABASE PilMoney
CREATE DATABASE PilMoney
GO
USE PilMoney
set dateformat dmy
GO
/* Creacion de las tablas */

CREATE TABLE TipoCuenta 
(
	Id INT IDENTITY(1,1) NOT NULL,
	TipoCuenta varchar(50) NOT NULL,
	CONSTRAINT PK_TipoCuenta PRIMARY KEY (Id)
)
GO
CREATE TABLE TipoMoneda 
(
	Id INT IDENTITY(1,1) NOT NULL,
	TipoMoneda varchar(50) NOT NULL,
	CONSTRAINT PK_TipoMoneda PRIMARY KEY (Id)
)
GO
CREATE TABLE TipoServicio
(
	Id INT IDENTITY(1,1) NOT NULL,
	TipoServicio varchar(50) NOT NULL,
	CONSTRAINT PK_TipoServicio PRIMARY KEY (Id)
)
GO
CREATE TABLE Usuario
(
	Id INT IDENTITY(1,1) NOT NULL,
	DNI VARCHAR(8) NOT NULL,
	Nombre VARCHAR(20) NOT NULL,
	Apellido VARCHAR(20) NOT NULL,
	Email VARCHAR(40) NOT NULL,
	NombreUsuario VARCHAR(20) NOT NULL,
	Clave VARCHAR(20) NOT NULL,
	FotoPerfil VARCHAR(50) NULL,
	FotoDNI VARCHAR(50) NULL,
	CONSTRAINT PK_Usuario PRIMARY KEY (Id),
	CONSTRAINT UQ_DNI UNIQUE (DNI),
	CONSTRAINT UQ_Email UNIQUE (Email),
	CONSTRAINT UQ_NombreUsuario UNIQUE (NombreUsuario)
)
GO
CREATE TABLE Autenticacion(
	Id INT IDENTITY(1,1) NOT NULL,
	IdUsuario INT NOT NULL,
	Token VARCHAR(255) NOT NULL,
	Fecha DATETIME NOT NULL,
	Estado BIT NOT NULL,
	CONSTRAINT UQ_Token UNIQUE (Token),
	CONSTRAINT PK_Autenticacion PRIMARY KEY (Id),
	CONSTRAINT FK_Autenticacion_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(Id)
)
GO
CREATE TABLE Cuenta
(
	Id INT IDENTITY(1,1) NOT NULL,
	TipoCuenta INT NOT NULL,
	Usuario INT NOT NULL,
	TipoMoneda INT NOT NULL,
	CVU VARCHAR(22) NOT NULL,
	FechaAlta DATETIME NOT NULL,
	Alias VARCHAR(15) NOT NULL,
	Saldo DECIMAL(18,2) NOT NULL,
	CONSTRAINT PK_Cuenta PRIMARY KEY (Id),
	CONSTRAINT UQ_Alias UNIQUE (Alias),
	CONSTRAINT UQ_CVU UNIQUE (CVU),
	CONSTRAINT FK_Cuenta_TipoCuenta FOREIGN KEY (TipoCuenta) REFERENCES TipoCuenta(Id), 
	CONSTRAINT FK_Cuenta_Usuario FOREIGN KEY (Usuario) REFERENCES Usuario(Id),
	CONSTRAINT FK_Cuenta_TipoMoneda FOREIGN KEY (TipoMoneda) REFERENCES TipoMoneda(Id) 
)
GO
CREATE TABLE Servicio
(
	Id INT IDENTITY(1,1) NOT NULL,
	TipoServicio INT NOT NULL,
	FechaVencimiento DATETIME NOT NULL,
	Periodo VARCHAR(10) NOT NULL,
	CVUServicio VARCHAR(22) NOT NULL,
	Monto DECIMAL(18,2) NOT NULL,
	CONSTRAINT PK_Servicio PRIMARY KEY (Id),
	CONSTRAINT UQ_CVUServicio UNIQUE (CVUServicio),
	CONSTRAINT FK_Servicio_TipoServicio FOREIGN KEY (TipoServicio) REFERENCES TipoServicio(Id)
)
GO
CREATE TABLE TipoTransacciones
(
	Id INT IDENTITY(1,1) NOT NULL,
	TipoTransaccion varchar(100) NOT NULL,
	CONSTRAINT PK_TipoTransacciones PRIMARY KEY (Id),
)
GO
CREATE TABLE Transacciones
(
	Id INT IDENTITY(1,1) NOT NULL,
	TipoTransaccion INT NOT NULL,
	CuentaOrigen INT NOT NULL,
	CuentaDestino INT NOT NULL,
	FechaTransaccion DATETIME NOT NULL,
	Monto DECIMAL(18,2) NOT NULL,
	CONSTRAINT PK_Transacciones PRIMARY KEY (Id),
	CONSTRAINT FK_Transacciones_Tipo FOREIGN KEY (TipoTransaccion) REFERENCES TipoTransacciones(Id), 
	CONSTRAINT FK_Transacciones_Cuenta FOREIGN KEY (CuentaOrigen) REFERENCES Cuenta(Id)
)
GO

CREATE TABLE PagoServicios
(
	Id INT IDENTITY(1,1) NOT NULL,
	Servicio INT NOT NULL,
	CuentaOrigen INT NOT NULL,
	CVUServicio varchar(22) NOT NULL,
	FechaPago DATETIME NOT NULL,
	Monto DECIMAL(18,2) NOT NULL,
	CONSTRAINT PK_PagoServicio PRIMARY KEY (Id),
	CONSTRAINT FK_Pago_CVUServicio FOREIGN KEY(CVUServicio) REFERENCES Servicio(CVUServicio),
	CONSTRAINT FK_PagoServicio_Cuenta FOREIGN KEY (CuentaOrigen) REFERENCES Cuenta(Id)
)
GO

CREATE PROCEDURE [dbo].[PilMoney_Api_AgregarUsuario]
@DNI VARCHAR(8),
@Nombre VARCHAR(20),
@Apellido VARCHAR(20),
@Email VARCHAR(40),
@NombreUsuario VARCHAR(20),
@Clave VARCHAR(20),
@FotoPerfil VARCHAR(255),
@FotoDNI VARCHAR(255),
@IdUsuario INT,
@Token VARCHAR(255),
@FechaToken DATETIME,
@Estado BIT
AS
IF @DNI != '' AND @Nombre != '' AND @Apellido != '' AND @Email != '' AND @NombreUsuario != '' AND @Clave != '' AND @Token != '' AND @FechaToken != '' AND @Estado >= 0
BEGIN
	INSERT INTO [dbo].[Usuario] (DNI, Nombre, Apellido, Email, NombreUsuario, Clave, FotoPerfil, FotoDNI)
	VALUES (@DNI, @Nombre, @Apellido, @Email, @NombreUsuario, @Clave, @FotoPerfil, @FotoDNI);

	SET @IdUsuario = SCOPE_IDENTITY();

	INSERT INTO [dbo].[Autenticacion] (IdUsuario, Token, Fecha, Estado) 
	VALUES(@idUsuario, @Token, @FechaToken, @Estado);
END
GO

CREATE PROCEDURE [dbo].[PilMoney_Api_DetalleDeUsuario]
@id INT
AS
SELECT Id, DNI, Nombre, Apellido, Email, NombreUsuario, Clave, FotoPerfil, FotoDNI
FROM [dbo].[Usuario]
WHERE Id = @id
GO

CREATE PROCEDURE [dbo].[PilMoney_Api_EliminarUsuario]
@Id INT
AS
IF @Id != ''
BEGIN
	DELETE FROM [dbo].[Usuario] 
	WHERE Id = @Id
END
GO

CREATE PROCEDURE [dbo].[PilMoney_Api_ListadoDeUsuarios]
AS
SELECT Id, DNI, Nombre, Apellido, Email, NombreUsuario, Clave, FotoPerfil, FotoDNI
FROM [dbo].[Usuario]
ORDER BY [Id] DESC
GO

CREATE PROCEDURE [dbo].[PilMoney_Api_ModificarUsuario]
@Id INT,
@DNI VARCHAR(8),
@Nombre VARCHAR(20),
@Apellido VARCHAR(20),
@Email VARCHAR(40),
@NombreUsuario VARCHAR(20),
@Clave VARCHAR(20),
@FotoPerfil VARCHAR(255),
@FotoDNI VARCHAR(255)
AS
IF @DNI != '' AND @Nombre != '' AND @Apellido != '' AND @Email != '' AND @NombreUsuario != '' AND @Clave != '' 
BEGIN
	UPDATE [dbo].[Usuario] 
	SET DNI=@DNI, Nombre=@Nombre, Apellido=@Apellido, Email=@Email, 
	NombreUsuario=@NombreUsuario, Clave=@Clave, FotoPerfil=@FotoPerfil, FotoDNI=@FotoDNI
	WHERE Id = @Id
END
GO

CREATE PROCEDURE [dbo].[PilMoney_Api_Login]
@Usuario VARCHAR(20),
@Password VARCHAR(20)
AS
SELECT a.Token, CONCAT(u.Nombre, ' ', u.Apellido) AS NombreApellido  
FROM Usuario u
INNER JOIN Autenticacion a ON u.Id = a.Id
WHERE u.NombreUsuario = @Usuario AND u.Clave = @Password;
GO

CREATE PROCEDURE [dbo].[PilMoney_Api_Validar_Token]
@Token VARCHAR(255)
AS
SELECT a.Token 
FROM   Autenticacion a
WHERE a.Token = @Token;
GO

CREATE PROCEDURE PilMoney_Api_ListadoDeServicios
AS 
SELECT TipoServicio 
FROM [dbo].[TipoServicio]
ORDER BY TipoServicio;
GO


/* DML */

/* Insertar Datos */
INSERT INTO TipoMoneda (TipoMoneda) VALUES ('Pesos');
INSERT INTO TipoMoneda (TipoMoneda) VALUES ('Dolares'),('BitCoin');
INSERT INTO TipoTransacciones(Tipotransaccion) VALUES ('Transferencia'),('IngresoDinero');
INSERT INTO TipoCuenta (TipoCuenta) VALUES ('Caja de ahorro en Pesos'), ('Caja de ahorro en Dolares'), ('Cuenta de Criptomonedas');
INSERT INTO TipoServicio (TipoServicio) VALUES ('Luz'), ('Agua'),('Gas'), ('Internet'), ('Telefono'), ('Cable');
INSERT INTO Usuario VALUES ('39735440', 'Juan Cruz', 'Pomares', 'juancruz.1600@gmail.com', 'JUANC1997', 'unodostres4', null, null);
INSERT INTO Usuario VALUES ('39229650', 'Lucia','Grosso', 'lugrosso.96@gmail.com', 'LUGROSSO96', 'lamandamas', null, null);
INSERT INTO Autenticacion VALUES(1,'93c102c9-7734-47eb-b011-620fbe8d4814', GETDATE(), 1);
INSERT INTO Autenticacion VALUES(2,'ffb2bc6a-a83f-462c-9727-58e58ab7898e', GETDATE(), 1);
INSERT INTO Cuenta VALUES (1, 1, 1, '0000125478558896325145', '23/08/2021', 'JUAN.C.POMARES', 15000);
INSERT INTO Cuenta VALUES (1, 2, 1, '0000525696548552215485', '23/08/2021', 'LU.CIA.GROSSO', 100000);
INSERT INTO Servicio VALUES (1, '03/09/2021', 'Septiembre', '0232521487999632502500', 4500);
INSERT INTO Servicio VALUES (2, '05/09/2021', 'Septiembre', '2525252525254567896215', 699);
INSERT INTO Servicio VALUES (3, '13/09/2021', 'Septiembre', '0202123456752585453152', 3200);
INSERT INTO PagoServicios VALUES (1, 1, '0232521487999632502500', '23/08/2021', 4500);
INSERT INTO PagoServicios VALUES (2, 2, '2525252525254567896215', '23/08/2021', 699);
INSERT INTO Transacciones VALUES (1, 1, 2, '23/08/2021', 5000);
INSERT INTO Transacciones VALUES (1, 2, 1, '23/08/2021', 15000);
INSERT INTO Transacciones VALUES (2, 1, 1, '23/08/2021', 5000);

/* Cosultar resultado */
SELECT Id, TipoMoneda FROM TipoMoneda;

/* Modificar Datos */
UPDATE TipoMoneda SET TipoMoneda = 'Pesos' WHERE Id = 1 ;
UPDATE TipoMoneda SET TipoMoneda = 'Dolares' WHERE Id = 2 ;
UPDATE TipoMoneda SET TipoMoneda = 'BitCoin' WHERE Id = 3 ;

/* Borrar Datos */
