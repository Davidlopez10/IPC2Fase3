USE FASE3

delete from pago
delete from cheque
delete from tarjeta

select *from PAGO
select*from MONEDA

insert into PAGO(CODIGOPAGO,TIPOMONEDA,TIPO,VALORPAGO,NOORDEN) VALUES (1,'Dolar','cheque',4561.23,'1')

select*from cheque

insert into	CHEQUE(CODIGOCHEQUE, CODIGOPAGO, CUENTABANCARIA,INFORMACIONBANCO) values 
select*from tarjeta
insert into TARJETA(NUMEROAUTORIZACION,CDPAGO,EMISOR) values(4545,1,'visa')

SELECT SALDOFALTA FROM ORDEN WHERE ORDEN.CODIGOORDEN='1'
select*from ORDEN

select VALORPAGO from pago where pago.NOORDEN='10' 
select *from PAGO
select*from tarjeta
select*from cheque
select*from efectivo

select CODIGOCHEQUE FROM CHEQUE INNER JOIN PAGO ON CHEQUE.CODIGOPAGO=PAGO.CODIGOPAGO WHERE PAGO.NOORDEN='10'
SELECT NUMEROAUTORIZACION FROM TARJETA INNER JOIN PAGO ON TARJETA.CDPAGO=PAGO.CODIGOPAGO WHERE PAGO.NOORDEN='10'
SELECT IDEFECTIVO FROM EFECTIVO INNER JOIN PAGO ON EFECTIVO.CDPAGO=PAGO.CODIGOPAGO WHERE PAGO.NOORDEN='10'

select*from recibo

insert into RECIBO(CODIGORECIBO,CDPAGO,CDCLIENTE,CDORDEN,)


SELECT ORDEN.NITEMPLEADOVENDE,EMPLEADO.NOMBRES,EMPLEADO.APELLIDOS,PUESTO.NOMBREPUESTO FROM ORDEN INNER JOIN EMPLEADO ON ORDEN.NITEMPLEADOVENDE=EMPLEADO.NITEMPLEADO INNER JOIN PUESTO ON EMPLEADO.CODIGOPUESTO=PUESTO.CODPUESTO WHERE ORDEN.CODIGOORDEN='1'

select ORDEN.CODIGOORDEN from ORDEN where ORDEN.ESTADOAPROBACION in('Cerrada','Aprobado')
select *from ORDEN
SELECT*FROM EMPLEADO

SELECT CODIGOORDEN FROM ORDEN WHERE ESTADOAPROBACION='Aprobado' AND PAGOABONO in('Pendiente de Pago','Pagando') AND SALDOFALTA>0;
SELECT CODIGOORDEN FROM ORDEN WHERE ESTADOAPROBACION='Aprobado' AND PAGOABONO in('Pendiente de Pago','Pagando') AND SALDOFALTA>0 AND ORDEN.NITEMPLEADOVENDE='444000-4';
SELECT CODIGOORDEN FROM ORDEN INNER JOIN EMPLEADO ON ORDEN.NITEMPLEADOVENDE=EMPLEADO.NITEMPLEADO WHERE (ESTADOAPROBACION='Aprobado') AND PAGOABONO in('Pendiente de Pago','Pagando') AND (SALDOFALTA>0) AND (EMPLEADO.JEFEEMPLEADO='333000-3' OR ORDEN.NITEMPLEADOVENDE='333000-3');

UPDATE ORDEN SET  WHERE CODIGOORDEN='1'
UPDATE ORDEN SET ORDEN.SALDOFALTA=9900,ORDEN.PAGOABONO='NULL' WHERE ORDEN.CODIGOORDEN='1';

select*from MONEDA
SELECT MONEDA.IDMONEDA, MONEDA.NOMBRE,MONEDA.SIMBOLO,MONEDA.VALOR FROM MONEDA WHERE MONEDA.NOMBRE='Dolar'

SELECT ORDEN.CODIGOORDEN FROM ORDEN INNER JOIN EMPLEADO ON ORDEN.NITEMPLEADOVENDE=EMPLEADO.NITEMPLEADO WHERE (ORDEN.PAGOABONO='Pagando') AND (SALDOFALTA>0) AND (EMPLEADO.JEFEEMPLEADO='333000-3')

UPDATE ORDEN SET ORDEN.ESTADOAPROBACION='ANULADO',ORDEN.PAGOABONO='ANULADO' WHERE ORDEN.CODIGOORDEN=''

SELECT EMPLEADO.NOMBRES,EMPLEADO.NITEMPLEADO,PUESTO.NOMBREPUESTO FROM EMPLEADO INNER JOIN PUESTO ON EMPLEADO.CODIGOPUESTO=PUESTO.CODPUESTO
INNER JOIN ORDEN ON ORDEN.NITEMPLEADOVENDE=EMPLEADO.NITEMPLEADO 


SELECT ORDEN.CODIGOORDEN,EMPLEADO.NOMBRES,EMPLEADO.NITEMPLEADO,PUESTO.NOMBREPUESTO,DETALLEPRODUCTOORDEN.NOMBREPRODUCTO,DETALLEPRODUCTOORDEN.CANTIDAD,DETALLEPRODUCTOORDEN.VALOR FROM ORDEN INNER JOIN CLIENTE ON ORDEN.NITCLIENTE=CLIENTE.NITCLIENTE INNER JOIN EMPLEADO ON ORDEN.NITEMPLEADOVENDE=EMPLEADO.NITEMPLEADO INNER JOIN PUESTO ON EMPLEADO.CODIGOPUESTO=PUESTO.CODPUESTO INNER JOIN DETALLEPRODUCTOORDEN ON ORDEN.CODIGOORDEN=DETALLEPRODUCTOORDEN.CODIGOORDEN WHERE CLIENTE.NITCLIENTE='222222-2'
 
 SELECT ORDEN.CODIGOORDEN FROM ORDEN INNER JOIN CLIENTE ON ORDEN.NITCLIENTE=CLIENTE.NITCLIENTE WHERE CLIENTE.NITCLIENTE='222222-2' and ORDEN.TOTALPAGAR>0

 select*from ORDEN
 SELECT *FROM CLIENTE
 select *from PAGO
 select*from cheque
 select*from TARJETA
 select*from RECIBO

 SELECT ORDEN.CODIGOORDEN, PAGO.TIPOMONEDA, MONEDA.VALOR , PAGO.VALORPAGO FROM PAGO INNER JOIN MONEDA ON PAGO.TIPOMONEDA=MONEDA.NOMBRE INNER JOIN ORDEN ON PAGO.NOORDEN=ORDEN.CODIGOORDEN WHERE ORDEN.CODIGOORDEN='4'

 select DETALLEPRODUCTOORDEN.CODIGOORDEN,NOMBREPRODUCTO,VALOR,CANTIDAD from DETALLEPRODUCTOORDEN inner join ORDEN on ORDEN.CODIGOORDEN=DETALLEPRODUCTOORDEN.CODIGOORDEN where ORDEN.TOTALPAGAR>0 and( CODIGOPRODUCTO='70707') order by NOMBREPRODUCTO

 select ORDEN.CODIGOORDEN from ORDEN order by ORDEN.
  select*from ORDEN
 
select *from DETALLEPRODUCTOORDEN
 select *from CATEGORIA
 select*from PRODUCTO
 select CATEGORIA.CODCATEGORIA from CATEGORIA

 DELETE FROM ORDEN WHERE CODIGOORDEN='2' AND TOTALPAGAR=0 AND ESTADOAPROBACION='Procesando'

 select Count(PRODUCTO.NOMBREPRODUCTO),CATEGORIA.NOMBRECATEGORIA from CATEGORIA inner join PRODUCTO on PRODUCTO.CATEGORIA=CATEGORIA.CODCATEGORIA
  select PRODUCTO.CODIGOPRODUCTO from PRODUCTO

  select  PRODUCTO.NOMBREPRODUCTO, DETALLEPRODUCTOORDEN.CANTIDAD,DETALLEPRODUCTOORDEN.CODIGOORDEN,DETALLEPRODUCTOORDEN.VALOR from CATEGORIA inner join PRODUCTO on PRODUCTO.CATEGORIA=CATEGORIA.CODCATEGORIA inner join DETALLEPRODUCTOORDEN on PRODUCTO.CODIGOPRODUCTO=DETALLEPRODUCTOORDEN.CODIGOPRODUCTO where CATEGORIA.CODCATEGORIA='11111'

  select sum(CANTIDAD) from DETALLEPRODUCTOORDEN where CODIGOORDEN='8'

  select count(CODIGOORDEN)from DETALLEPRODUCTOORDEN inner join PRODUCTO on DETALLEPRODUCTOORDEN.CODIGOPRODUCTO=PRODUCTO.CODIGOPRODUCTO where PRODUCTO.CATEGORIA=''

  Delete from ORDEN where ORDEN.CODIGOORDEN='5'
  select* from ORDEN
  select*from DETALLEPRODUCTOORDEN
  select*from EMPLEADO
  delete from DETALLEPRODUCTOORDEN where CODIGOORDEN=''


  select*from LISTACLIENTE
  insert into LISTACLIENTE values('','','','');

  delete from ORDEN where CODIGOORDEN=' '

select CODIGOPRODUCTO,NOMBREPRODUCTO,VALOR,CANTIDAD from DETALLEPRODUCTOORDEN where CODIGOORDEN='6' order by NOMBREPRODUCTO;

select*from PRODUCTO

SELECT DISTINCT CLIENTE.NITCLIENTE FROM CLIENTE INNER JOIN ORDEN ON ORDEN.NITCLIENTE = CLIENTE.NITCLIENTE WHERE (CLIENTE.CANTIDADORDENES > 0) AND (ORDEN.TOTALPAGAR > 0)

select SUM(VENTAMETA) from DETALLEMETA inner join METAS on detallemeta.codigometa=METAS.IDMETA where (CONVERT(date,METAS.FECHA,103) BETWEEN CONVERT(date,'1/11/2014',103) AND CONVERT(date,'30/11/2014',103)) and METAS.CODIGOEMPELADO='666000-6'
select *from ORDEN
select COUNT(ESTADOAPROBACION) from ORDEN where ESTADOAPROBACION in('Cerrada','Aprobado') and ORDEN.NITEMPLEADOVENDE='666000-6' and  (CONVERT(date,ORDEN.FECHACERRADA,103) BETWEEN CONVERT(date,'1/11/2014',103) AND CONVERT(date,'30/11/2014',103))

select COUNT(CODIGOORDEN) from ORDEN where ESTADOAPROBACION in('Cerrada','Aprobado') and ORDEN.PAGOABONO='Pagando' and ORDEN.NITEMPLEADOVENDE='666000-6' and  (CONVERT(date,ORDEN.FECHACERRADA,103) BETWEEN CONVERT(date,'1/11/2014',103) AND CONVERT(date,'30/11/2014',103))

select*from METAS
select*from DETALLEMETA
select*from PRODUCTO
select*from CATEGORIA

select IDMETA, SUM(VENTAMETA) from METAS inner join DETALLEMETA on METAS.IDMETA=DETALLEMETA.CODIGOMETA inner join PRODUCTO on DETALLEMETA.CODIGOPRODUC=PRODUCTO.CODIGOPRODUCTO inner join CATEGORIA on PRODUCTO.CATEGORIA=CATEGORIA.CODCATEGORIA where (CONVERT(date,METAS.FECHA,103) BETWEEN CONVERT(date,'1/09/2014',103) AND CONVERT(date,'30/09/2014',103)) and (METAS.CODIGOEMPELADO='444000-4') and CATEGORIA.CODCATEGORIA='33333'  group by IDMETA

select*from ORDEN
select*from	 DETALLEPRODUCTOORDEN

select COUNT(ESTADOAPROBACION) from ORDEN inner join DETALLEPRODUCTOORDEN on ORDEN.CODIGOORDEN=DETALLEPRODUCTOORDEN.CODIGOORDEN inner join PRODUCTO on DETALLEPRODUCTOORDEN.CODIGOPRODUCTO=PRODUCTO.CODIGOPRODUCTO inner join CATEGORIA on PRODUCTO.CATEGORIA=CATEGORIA.CODCATEGORIA  where ESTADOAPROBACION in('Cerrada','Aprobado') and ORDEN.NITEMPLEADOVENDE='666000-6' and  (CONVERT(date,ORDEN.FECHACERRADA,103) BETWEEN CONVERT(date,'1/11/2014',103) AND CONVERT(date,'30/11/2014',103)) and CATEGORIA.CODCATEGORIA='11111'

select COUNT(ORDEN.CODIGOORDEN) from ORDEN inner join DETALLEPRODUCTOORDEN on ORDEN.CODIGOORDEN=DETALLEPRODUCTOORDEN.CODIGOORDEN inner join PRODUCTO on DETALLEPRODUCTOORDEN.CODIGOPRODUCTO=PRODUCTO.CODIGOPRODUCTO inner join CATEGORIA on PRODUCTO.CATEGORIA=CATEGORIA.CODCATEGORIA where ESTADOAPROBACION in('Cerrada','Aprobado') and ORDEN.PAGOABONO='Pagando' and ORDEN.NITEMPLEADOVENDE='666000-6' and  (CONVERT(date,ORDEN.FECHACERRADA,103) BETWEEN CONVERT(date,'1/11/2014',103) AND CONVERT(date,'30/11/2014',103)) and CATEGORIA.CODCATEGORIA='33333'














