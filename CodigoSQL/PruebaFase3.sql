create database FASE3
use FASE3

CREATE TABLE PUESTO(
IDPUESTO INT IDENTITY (1,1),
CODPUESTO VARCHAR(50) NOT NULL PRIMARY KEY,
NOMBREPUESTO VARCHAR(MAX)
);

CREATE TABLE EMPLEADO(
 IDEMPLEADO INT IDENTITY (1,1),
 NITEMPLEADO VARCHAR(50) NOT NULL PRIMARY KEY,
 NOMBRES VARCHAR(25) NOT NULL,
 APELLIDOS VARCHAR(25)NOT NULL,
 FECHANACIMIENTO VARCHAR(25) NOT NULL,
 DIRECCION VARCHAR(MAX) NOT NULL,
 TELEFONODOMICILIO VARCHAR(MAX) NOT NULL,
 CELULAR VARCHAR(50) NULL,
 EMAIL VARCHAR(25)  NULL,
 CONTRASENIA VARCHAR(MAX) NULL,
 CODIGOPUESTO VARCHAR(50) FOREIGN KEY REFERENCES PUESTO(CODPUESTO)
);
ALTER TABLE EMPLEADO
ADD JEFEEMPLEADO VARCHAR(50) FOREIGN KEY REFERENCES EMPLEADO(NITEMPLEADO)

CREATE TABLE CATEGORIA(
IDCATEGORIA INT IDENTITY (1,1),
CODCATEGORIA VARCHAR(50) NOT NULL PRIMARY KEY,
NOMBRECATEGORIA VARCHAR(50)
);

CREATE TABLE PRODUCTO(
IDPRODUCTO INT IDENTITY (1,1),
CODIGOPRODUCTO VARCHAR(50) PRIMARY KEY NOT NULL,
NOMBREPRODUCTO VARCHAR (MAX) NOT NULL,
DESCRIPCION VARCHAR (MAX) ,
CATEGORIA VARCHAR(50) FOREIGN KEY REFERENCES CATEGORIA(CODCATEGORIA)
);

--LISTA
CREATE TABLE LISTAPRECIOS(
IDLISTA INT IDENTITY (1,1),
CODIGO VARCHAR(50) NOT NULL PRIMARY KEY,
NOMBRE VARCHAR(25),
FECHAINICIO VARCHAR(50),
FECHAFIN VARCHAR(50),
);

CREATE TABLE DETALLELISTAPRECIOS(
IDDETALLELISTA INT IDENTITY (1,1),
VALOR float(12),
FKCODIGOPRODUC VARCHAR(50) FOREIGN KEY REFERENCES PRODUCTO(CODIGOPRODUCTO),
FKCODIGOLIST VARCHAR(50) FOREIGN KEY REFERENCES LISTAPRECIOS(CODIGO) 
);

CREATE TABLE DEPARTAMENTO(
CODIGODEPARTAMENTO VARCHAR(50) NOT NULL PRIMARY KEY,
NOMBRE VARCHAR(25),
);

CREATE TABLE CIUDAD(
CODIGOCIUDAD VARCHAR(50) NOT NULL PRIMARY KEY,
NOMBRE VARCHAR(MAX),
CODDEPARTAMENTO VARCHAR(50) FOREIGN KEY REFERENCES DEPARTAMENTO(CODIGODEPARTAMENTO)
);

CREATE TABLE CLIENTE(
IDCLIENTE INT IDENTITY (1,1),
NITCLIENTE VARCHAR(50) PRIMARY KEY NOT NULL,
NOMBRECLIENTE VARCHAR (MAX) NOT NULL,
APELLIDOSCLIENTE VARCHAR (MAX)  NULL,
FECHANACIMIENTOCLIENTE VARCHAR(MAX)  NULL,
DIRECCIONDOMICILIO VARCHAR (MAX) NOT NULL,
TELEFONO VARCHAR(50) ,
CELULAR VARCHAR(50),
EMAIL VARCHAR (MAX) ,
CANTIDADORDENES INT,
LIMITECREDITO float(50),
DIASCREDITO INT,
CODEPARTAMENTO VARCHAR(50) FOREIGN KEY REFERENCES DEPARTAMENTO(CODIGODEPARTAMENTO),
CODCIUDAD VARCHAR(50) FOREIGN KEY REFERENCES CIUDAD(CODIGOCIUDAD) 
);

CREATE TABLE LISTACLIENTE(
IDLISTACLIENTE INT IDENTITY (1,1),
FECHAINICIO VARCHAR(MAX),
FECHAFIN VARCHAR(MAX),
CODIGOLISTAPRECIOS VARCHAR(50) FOREIGN KEY REFERENCES LISTAPRECIOS(CODIGO),
NITCLIENTE VARCHAR(50) FOREIGN KEY REFERENCES CLIENTE(NITCLIENTE)
);
----CODIGO VARCHAR(50) NOT NULL PRIMARY KEY,

CREATE TABLE METAS(
IDMETA INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
FECHA VARCHAR(25)  NULL,
TOTALMES float(50)  NULL,
CODIGOEMPELADO VARCHAR(50) FOREIGN KEY REFERENCES EMPLEADO(NITEMPLEADO)
);

CREATE TABLE DETALLEMETA(
IDDETALLEMETA INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
VENTAMETA float(30),
CODIGOPRODUC VARCHAR(50) FOREIGN KEY REFERENCES PRODUCTO(CODIGOPRODUCTO),
CODIGOMETA INT FOREIGN KEY REFERENCES METAS(IDMETA)
);

CREATE TABLE MONEDA(
IDMONEDA INT IDENTITY (1,1),
NOMBRE VARCHAR(50)PRIMARY KEY NOT NULL,
SIMBOLO VARCHAR(50) ,
VALOR float(30),
);

CREATE TABLE ORDEN(
IDORDEN INT IDENTITY (1,1),
CODIGOORDEN VARCHAR(50) PRIMARY KEY NOT NULL ,
TOTALPAGAR float(50)   NULL,
FECHACREACION VARCHAR(MAX) NULL,
FECHACERRADA VARCHAR(MAX),
DIASFALTANTE INT  NULL,
PAGOABONO VARCHAR(53),
SALDOFALTA float(50),
ESTADOAPROBACION VARCHAR(MAX),
FECHAAPROBACION VARCHAR(MAX),
DIASFALTANTEAPROBACION VARCHAR(MAX),
EMPLEADOAPROBACION VARCHAR(50) FOREIGN KEY REFERENCES EMPLEADO(NITEMPLEADO),
NITEMPLEADOVENDE VARCHAR(50) FOREIGN KEY REFERENCES EMPLEADO(NITEMPLEADO),
NITCLIENTE  VARCHAR(50) FOREIGN KEY REFERENCES CLIENTE (NITCLIENTE)
);
-------usada solo para orden ----- y esta es la que se muestra
CREATE TABLE DETALLEPRODUCTOORDEN(
IDDETALLE INT IDENTITY (1,1) PRIMARY KEY,
NOMBREPRODUCTO VARCHAR(MAX) NULL,
CANTIDAD INT NOT NULL,
VALOR float(30),
CODIGOORDEN  VARCHAR(50) FOREIGN KEY REFERENCES ORDEN(CODIGOORDEN),
CODIGOPRODUCTO VARCHAR(50) FOREIGN KEY REFERENCES PRODUCTO(CODIGOPRODUCTO) 
);

------------------empieza lo forma de pago--------------------
CREATE TABLE PAGO(
CODIGOPAGO INT PRIMARY KEY NOT NULL,
TIPOMONEDA VARCHAR(50) FOREIGN KEY REFERENCES MONEDA(NOMBRE),
TIPO VARCHAR(MAX),
VALORPAGO FLOAT(12),
NOORDEN VARCHAR(50) FOREIGN KEY REFERENCES ORDEN(CODIGOORDEN)
);

CREATE TABLE CHEQUE(
CODIGOCHEQUE INT PRIMARY KEY NOT NULL,
CODIGOPAGO INT FOREIGN KEY REFERENCES PAGO(CODIGOPAGO) NOT NULL,
CUENTABANCARIA VARCHAR(MAX) NOT NULL,
INFORMACIONBANCO VARCHAR(MAX)
);

CREATE TABLE TARJETA(
NUMEROAUTORIZACION INT PRIMARY KEY NOT NULL,
CDPAGO INT FOREIGN KEY REFERENCES PAGO(CODIGOPAGO),
EMISOR VARCHAR(50)
);

CREATE TABLE EFECTIVO(
IDEFECTIVO INT PRIMARY KEY IDENTITY(1,1),
CDPAGO INT FOREIGN KEY REFERENCES PAGO(CODIGOPAGO)
);

CREATE TABLE FACTURA(
SERIE INT PRIMARY KEY NOT NULL,
FECHAEMISION VARCHAR(30) NOT NULL,
CDPAGO INT FOREIGN KEY REFERENCES PAGO(CODIGOPAGO),
CDEMPLEADO VARCHAR(50) FOREIGN KEY REFERENCES EMPLEADO(NITEMPLEADO),
CDCLIENTE VARCHAR(50) FOREIGN KEY REFERENCES CLIENTE(NITCLIENTE),
CDORDEN VARCHAR(50) FOREIGN KEY REFERENCES ORDEN(CODIGOORDEN)
);

CREATE TABLE RECIBO(
CODIGORECIBO INT PRIMARY KEY NOT NULL,
CDPAGO INT FOREIGN KEY REFERENCES PAGO(CODIGOPAGO),
CDCLIENTE VARCHAR(50) FOREIGN KEY REFERENCES CLIENTE(NITCLIENTE),
CDORDEN VARCHAR(50) FOREIGN KEY REFERENCES ORDEN(CODIGOORDEN),
CDTARJETA INT FOREIGN KEY REFERENCES TARJETA(NUMEROAUTORIZACION),
CDCHEQUE INT FOREIGN KEY REFERENCES CHEQUE(CODIGOCHEQUE),
CDEMPELADO VARCHAR(50) FOREIGN KEY REFERENCES EMPLEADO(NITEMPLEADO)
);







