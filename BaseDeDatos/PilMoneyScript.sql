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
	Clave VARCHAR(100) NOT NULL,
	FotoPerfil VARBINARY(MAX) NULL,
	FotoDNI VARCHAR(50) NULL,
	CONSTRAINT PK_Usuario PRIMARY KEY (Id),
	CONSTRAINT UQ_DNI UNIQUE (DNI),
	CONSTRAINT UQ_Email UNIQUE (Email),
	CONSTRAINT UQ_NombreUsuario UNIQUE (NombreUsuario)
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
	CuentaDestino VARCHAR(50) NOT NULL,
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
@Clave VARCHAR(100),
@FotoPerfil VARBINARY(MAX),
@FotoDNI VARCHAR(255),
@TipoCuenta INT,
@Usuario INT,
@TipoMoneda INT,
@CVU VARCHAR(100),
@FechaAlta DATETIME,
@Alias VARCHAR(15),
@Saldo DECIMAL(18,2)
AS
IF @DNI != '' AND @Nombre != '' AND @Apellido != '' AND @Email != '' AND @NombreUsuario != '' AND @Clave != '' 
BEGIN
	INSERT INTO [dbo].[Usuario] (DNI, Nombre, Apellido, Email, NombreUsuario, Clave, FotoPerfil, FotoDNI)
	VALUES (@DNI, @Nombre, @Apellido, @Email, @NombreUsuario, @Clave, @FotoPerfil, @FotoDNI);
	SET @Usuario = SCOPE_IDENTITY();

	IF @TipoCuenta > -1 AND @Usuario > -1 AND @TipoMoneda > -1 AND @CVU != '' AND @FechaAlta != '' AND @Alias != '' AND @Saldo = 0.0
	BEGIN
		INSERT INTO [dbo].[Cuenta] (TipoCuenta, Usuario, TipoMoneda, CVU, FechaAlta, Alias, Saldo)
		VALUES (@TipoCuenta, @Usuario, @TipoMoneda, @CVU, @FechaAlta, @Alias, @Saldo);
	END
END
GO

CREATE PROCEDURE [dbo].[PilMoney_Api_AgregarCuenta]
@TipoCuenta INT,
@Usuario INT,
@TipoMoneda INT,
@CVU VARCHAR(100),
@FechaAlta DATETIME,
@Alias VARCHAR(15),
@Saldo DECIMAL(18,2)
AS
IF @TipoCuenta != '' AND @Usuario != '' AND @TipoMoneda != '' AND @CVU != '' AND @FechaAlta != '' AND @Alias != '' AND @Saldo != ''
BEGIN
	INSERT INTO [dbo].[Cuenta] (TipoCuenta, Usuario, TipoMoneda, CVU, FechaAlta, Alias, Saldo)
	VALUES (@TipoCuenta, @Usuario, @TipoMoneda, @CVU, @FechaAlta, @Alias, @Saldo);
END
GO

CREATE PROCEDURE [dbo].[PilMoney_Api_DetalleDeUsuario]
@id INT
AS
SELECT Id, DNI, Nombre, Apellido, Email, NombreUsuario, Clave, CAST(FotoPerfil AS VARCHAR(MAX)) AS FotoPerfil, FotoDNI
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
SELECT Id, DNI, Nombre, Apellido, Email, NombreUsuario, Clave, CAST(FotoPerfil AS VARCHAR(MAX)) AS FotoPerfil, FotoDNI
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
@FotoPerfil VARBINARY(MAX),
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
@Password VARCHAR(100)
AS
SELECT u.Id , CONCAT(u.Nombre, ' ', u.Apellido) AS NombreApellido  
FROM Usuario u
WHERE u.NombreUsuario = @Usuario AND u.Clave = @Password;
GO

CREATE PROCEDURE [dbo].[PilMoney_Api_DatosCuentaPeso]
@Id INT
AS
SELECT tc.TipoCuenta, tm.TipoMoneda, CONCAT(u.Nombre, ' ', u.Apellido) AS NombreApellido, c.CVU, c.Alias, c.Saldo, c.FechaAlta
FROM Cuenta c
INNER JOIN TipoCuenta tc ON c.TipoCuenta = tc.Id
INNER JOIN TipoMoneda tm ON c.TipoMoneda = tm.Id
INNER JOIN Usuario u ON c.Usuario = u.Id
WHERE c.Id = @Id;
GO

CREATE PROCEDURE [dbo].[PilMoney_Api_UltimosMovimiento]
@Id INT
AS
SELECT tp.TipoServicio, S.Periodo, s.CVUServicio, FORMAT(S.FechaVencimiento, 'dd/MM/yyyy') AS Fecha, S.Monto
FROM Servicio S
INNER JOIN TipoServicio tp ON s.TipoServicio = tp.Id
INNER JOIN PagoServicios ps ON ps.Servicio = s.Id
WHERE ps.CuentaOrigen = @Id
GO

CREATE PROCEDURE [dbo].[PilMoney_Api_ListadoDeServicios]
AS 
SELECT TipoServicio 
FROM [dbo].[TipoServicio]
ORDER BY TipoServicio;
GO

CREATE PROCEDURE [dbo].[PilMoney_Api_IngresarDinero]
@TipoTrans INT,
@CuentaOrigen INT,
@CuentaDestino VARCHAR(50),
@Fecha DATETIME,
@Monto DECIMAL(18,2)
AS
IF @TipoTrans != '' AND @CuentaOrigen != '' AND @CuentaDestino != '' AND @Fecha != '' AND @Monto >= 0-0
BEGIN
	INSERT INTO [dbo].[Transacciones] (TipoTransaccion, CuentaOrigen, CuentaDestino, FechaTransaccion, Monto)
	VALUES (@TipoTrans, @CuentaOrigen, @CuentaDestino, @Fecha, @Monto);

	DECLARE @saldoActualizado DECIMAL(18,2);
	DECLARE @saldoActual DECIMAL(18,2);
	SET @saldoActual = (SELECT Saldo FROM Cuenta WHERE Id = @CuentaOrigen);
	SET @saldoActualizado = @Monto + @saldoActual

	UPDATE [dbo].[Cuenta] SET Saldo = @saldoActualizado WHERE Id = @CuentaOrigen;
END
GO

CREATE PROCEDURE [dbo].[PilMoney_Api_ListadoDeTransacciones]
@cuentaPropia INT
AS
SELECT t.CuentaDestino,  tt.TipoTransaccion, t.FechaTransaccion, t.Monto
FROM Transacciones t
INNER JOIN TipoTransacciones tt ON t.TipoTransaccion = tt.Id
WHERE t.CuentaOrigen = @cuentaPropia
ORDER BY t.FechaTransaccion
GO

CREATE PROCEDURE [dbo].[PilMoney_Api_TransferirDinero]
@TipoTrans INT,
@CuentaOrigen INT,
@CuentaDestino VARCHAR(50),
@Fecha DATETIME,
@Monto DECIMAL(18,2)
AS
IF @TipoTrans != '' AND @CuentaOrigen != '' AND @CuentaDestino != '' AND @Fecha != '' AND @Monto >= 0-0
BEGIN
	INSERT INTO [dbo].[Transacciones] (TipoTransaccion, CuentaOrigen, CuentaDestino, FechaTransaccion, Monto)
	VALUES (@TipoTrans, @CuentaOrigen, @CuentaDestino, @Fecha, @Monto);

	DECLARE @saldoActualizado DECIMAL(18,2);
	DECLARE @saldoActual DECIMAL(18,2);
	SET @saldoActual = (SELECT Saldo FROM Cuenta WHERE Id = @CuentaOrigen);
	SET @saldoActualizado = @saldoActual - @Monto 

	UPDATE [dbo].[Cuenta] SET Saldo = @saldoActualizado WHERE Id = @CuentaOrigen;
END
GO

CREATE PROCEDURE [dbo].[PilMoney_Api_PagoServicio]
@Servicio INT,
@CuentaOrigen INT,
@CVUServicio VARCHAR(20),
@FechaPago DATETIME,
@Monto DECIMAL(18,2)
AS
IF @Servicio != '' AND @CuentaOrigen != '' AND @CVUServicio != '' AND @FechaPago != '' AND @Monto >= 0-0
BEGIN
	INSERT INTO [dbo].[PagoServicios] (Servicio, CuentaOrigen, CVUServicio, FechaPago, Monto)
	VALUES (@Servicio, @CuentaOrigen, @CVUServicio, @FechaPago, @Monto);

	DECLARE @saldoActualizado DECIMAL(18,2);
	DECLARE @saldoActual DECIMAL(18,2);
	SET @saldoActual = (SELECT Saldo FROM Cuenta WHERE Id = @CuentaOrigen);
	SET @saldoActualizado = @saldoActual - @Monto 

	UPDATE [dbo].[Cuenta] SET Saldo = @saldoActualizado WHERE Id = @CuentaOrigen;
END
GO


/* DML */

/* Insertar Datos */
INSERT INTO TipoMoneda (TipoMoneda) VALUES ('Pesos');
INSERT INTO TipoMoneda (TipoMoneda) VALUES ('Dolares'),('BitCoin');
INSERT INTO TipoTransacciones(Tipotransaccion) VALUES ('Transferencia'),('IngresoDinero');
INSERT INTO TipoCuenta (TipoCuenta) VALUES ('Caja de ahorro en Pesos'), ('Caja de ahorro en Dolares'), ('Cuenta de Criptomonedas');
INSERT INTO TipoServicio (TipoServicio) VALUES ('Luz'), ('Agua'),('Gas'), ('Internet'), ('Telefono'), ('Cable');
INSERT INTO Usuario VALUES ('39735440', 'Juan Cruz', 'Pomares', 'juancruz.1600@gmail.com', 'JUANC1997', '846C18C02F7B07211100AEDF84ED26A4325851D34318D55AF240F0A3155027F4', NULL, NULL);
INSERT INTO Usuario VALUES ('39229650', 'Lucia','Grosso', 'lugrosso.96@gmail.com', 'LUGROSSO96', '1A385F1837EE226328FE8E40C8EFCBC18AAF50DBE94B3C1A1B32619BEB95A719', NULL, NULL);
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
