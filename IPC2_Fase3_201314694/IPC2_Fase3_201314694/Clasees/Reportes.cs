using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.awt.geom;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using IPC2_Fase3_201314694.Clasees;

namespace IPC2_Fase3_201314694
{
    public class Reportes
    {
        Document Doc;
        ConexionSQL conexion = new ConexionSQL(); 
        DateTime today = DateTime.Today;
        public void RealizarPdf(string cadenaFinal)
        {
            Document PDFD = new Document(PageSize.A4, 40, 40, 40, 40);
            try
            {
                PdfWriter.GetInstance(PDFD, System.Web.HttpContext.Current.Response.OutputStream);

                PDFD.Open();
                string strContent = cadenaFinal;
                var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(strContent), null);
                PDFD.AddCreator("DavidLopez_201314694");
                foreach (var htmlElement in parsedHtmlElements)
                    PDFD.Add(htmlElement as IElement);

                PDFD.Close();
                Doc = PDFD;
            }
            catch (Exception ex)
            {
                //Response.Write(ex.ToString());
            }        
        }
        /***********SIRVE PARA EL BOTON DE APROBACION***************/
        public void CrearReporteAprobar(string codigoOrden,UsuarioEmpleado User) {
                
            DatosCliente cliente = conexion.ObtenerDatosGenerales(codigoOrden);
            Orden1 ordenes = conexion.obtenerDatosOrdenGeneral(codigoOrden);
            Lista listaprecio = conexion.obtenerDatosLista(cliente.Nit,ordenes.FechaCreacion1);
            LinkedList<Productos> detalleProducto = conexion.DetalleOrden(codigoOrden);
           string CodigoPDF = "";
            //ENCABEZADO
            const string comilla = "\"";
            CodigoPDF += "<h1> ORDEN APROBADA: </h1><br/>";
            CodigoPDF += "<FONT FACE= " + comilla + "courier new" + comilla + "SIZE=4 COLOR=" + comilla + "Black" + comilla + "><p> <b> ID ORDEN:  </b>"+codigoOrden+"</p><br/>";
            //CLIENTE
            CodigoPDF += "<h3> <b>DATOS CLIENTE </b> </h3></br>";
            CodigoPDF += "<h4> <b>NIT CLIENTE: </b>"+cliente.Nit+" "+cliente.Apellido+"</h4></br>";
            CodigoPDF += "<h4> <b>NOMBRE: </b>"+cliente.Nombre+"</h4></br>";
            CodigoPDF += "<h4> <b>DIRECCION: </b>"+cliente.Direccion1+"</h4><br/>";
            //EMPLEADO
            CodigoPDF += "<h3> <b>DATOS EMPLEADO </b> </h3></br>";
            CodigoPDF += "<h4> <b>NIT EMPLEADO: </b>"+User.Nit+"</h4></br>";
            CodigoPDF += "<h4> <b>NOMBRE: </b>"+User.Nombre+"</h4></br>";
            CodigoPDF += "<h4> <b>PUESTO: </b>"+User.Puesto+"</h4><br/>";
            //ORDEN
            CodigoPDF += "<h3> <b>DATOS ORDEN </b> </h3></br>";
            CodigoPDF += "<h4> <b>FECHA DE CREACION: </b>"+ordenes.FechaCreacion1+" </h4></br>";
            CodigoPDF += "<h4> <b>FECHA DE CIERRE: </b>"+ordenes.FechaCerrada1+"</h4></br>";
            CodigoPDF += "<h4> <b>FECHA APROBADA: </b>"+ordenes.FechaAprobacion1+"</h4></br>";
            CodigoPDF += "<h4> <b>APROBADA POR: </b>"+ordenes.EmpleadoAprobo1+"</h4><br/>";
            CodigoPDF += "<h4> <b>PUESTO: </b>"+ordenes.EmpeadoPuesto1+"</h4><br/>";
            //LISTA
            CodigoPDF += "<h4> <b> CODIGO LISTA: </b>"+listaprecio.ListaPrecioCodigo1+"</h4></br>";
            CodigoPDF += "<h4> <b> NOMBRE LISTA: </b>"+listaprecio.ListaPrecioNombre1+"</h4><br/><br/></font>";
            //MONTO TOTAL
            CodigoPDF += "<h3> <b>MONTO TOTAL A PAGAR: </b>"+ordenes.ValorTotal1+"</h3><br/><br/>";
           
            CodigoPDF += "<h2> <b> DETALLE ORDEN: </b></h2><br/>";
            //DESCRIPCION
            CodigoPDF += "<TABLE BORDER='0.1'>";
            CodigoPDF += "<TR><TD> <b>ID Producto</b> </TD><TD> <b>Nombre Producto</b> </TD> <TD> <b>Cantidad</b> </TD> <TD> <b>Precio Unitario</b></TD> <TD><b> Precio Total</b></TD> </TR>";
            //PRODUCTOS
           foreach(  Productos xx in detalleProducto){
               CodigoPDF += "<TR><TD>"+xx.CodigoProducto+"</TD><TD>"+xx.Nombre+"</TD><TD>"+xx.Cantidad1+"</TD><TD>"+xx.Precio+"</TD><TD>"+xx.PrecioTotal1+"</TD></TR>";
           }

            CodigoPDF += "</TABLE><br/>";           
          
            CodigoPDF += "<h6> <i>PRODUCTOS MAGNIFICOS </i> </h6></br>";
            RealizarPdf(CodigoPDF);      
        }
        /**************Reporte venta vs meta(Vendedor)********************/
        public void CrearReporte_VentaMetaVendedor(UsuarioEmpleado usr,string mes) {
            String Pdf = "";
            string anio = today.ToString("yyyy");
            decimal total = conexion.TotalMetaEmpleadoVendedor(mes+"/"+anio,usr.Nit);//meta del mes del vendedor
            string ordcerrada = conexion.TotalOrdenesCerradas(usr.Nit,mes+"/"+anio);//cantidad de ordenes cerradas durante el mes
            string OrdenesPagadas = conexion.TotalOrdenesPagadas(usr.Nit,mes+"/"+anio);//cantidad de ordens pagadas durente el mes
            const string comilla = "\"";
            Pdf += "<h1> REPORTE VENTA -META: </h1><br/>";
            Pdf += "<FONT FACE= " + comilla + "courier new" + comilla + "SIZE=4 COLOR=" + comilla + "Black" + comilla + "><p> <b>  NIT VENDEDOR:  </b>" + usr.Nit + "</p><p> <b> MES A REPORTAR:  </b>" +mes + "</p><br/>";
            //DATOS GENERALES DE EMPLEADO
            Pdf += "<h3><b>DATOS DE EMPLEADO:</b> </h3></br/>";
            Pdf += "<h4><b>NIT DE EMPLEADO: </b>"+usr.Nit+"</h4></br>";
            Pdf += "<h4><b>NOMBRE DE EMPLEADO: </b>" + usr.Nombre + "</h4></br>";
            //FECHAS 
            Pdf += "<h3><b>FECHA </b></h3></br>";           
            Pdf += "<h4><b>FECHA Y HORA: </b>" + today.ToString("G") + "</h4></br>";
            Pdf += "<h4><b>META DEL MES: </b>" +Convert.ToString(total).Replace(",",".")+ "</h4></br>";
            Pdf += "<h4><b>ORDENES CERRADA: </b>" + ordcerrada + "</h4></br>";
            Pdf += "<h4><b>ORDENES PAGADAS: </b>" + OrdenesPagadas + "</h4></br>";
            decimal porcentaje;
            try {
                porcentaje = (total / Convert.ToDecimal(OrdenesPagadas)) * 100;
                porcentaje = decimal.Round(porcentaje, 4);
            }catch(Exception ex){
                porcentaje=0;
            }
            
            Pdf += "<h4><b>PORCENTAJES CUMPLIDOS: </b>" +porcentaje+ "</h4></br>";
            Pdf += "<h6> <i>PRODUCTOS MAGNIFICOS </i> </h6></br>";
            RealizarPdf(Pdf);
        }

        public void CrearReporteVenta_MetaXCategoria(UsuarioEmpleado user,string mes) {
            LinkedList<string> categorias = conexion.CodigosCategorias();
            string Pdf = "";
            string anio=today.ToString("yyyy");
            const string comilla = "\"";
            Pdf += "<h1> REPORTE VENTA META X CATEGORIA: </h1><br/>";
            Pdf += "<FONT FACE= " + comilla + "courier new" + comilla + "SIZE=4 COLOR=" + comilla + "Black" + comilla + "><p> <b> MES A REPORTAR:  </b>" + mes + "</p><br/>";
            //DATOS GENERALES DE EMPLEADO
            Pdf += "<h3><b>DATOS DE EMPLEADO:</b> </h3></br/>";
            Pdf += "<h4><b>NIT DE EMPLEADO: </b>" + user.Nit + "</h4></br>";
            Pdf += "<h4><b>NOMBRE DE EMPLEADO: </b>" + user.Nombre + "</h4></br>";
            //fecha que se reporta
            Pdf += "<h3><b>FECHA </b></h3></br>";
            Pdf += "<h4><b>FECHA Y HORA: </b>" + today.ToString("G") + "</h4></br>";
            foreach (string x in categorias) {
                Pdf += "<h3><b>CATEGORIA:</b>"+x+"</h3></br>";
                Metas met = conexion.TotalMetaMesVendedor(user.Nit,mes+"/"+anio,x);//metas del vendedor
                Pdf += "<h2><b>CODIGO DE META_MES: </b>"+met.Id+"</h2></br>";
                Pdf += "<h4><b> VALOR DEL MES</b>"+met.SumaTotal+"</h4></br>";
                string ordenCerrada = conexion.TotalOrdencesCerradasXCategoria(user.Nit,mes+"/"+anio,x);//cantidad de ordenes cerradas
                Pdf += "<h4><b> ORDENES CERRADAS </b>" + ordenCerrada + "</h4></br>";
                string ordensPagadas = conexion.TotalOrdenesPagadasXCategoria(user.Nit, mes + "/" + anio, x);//cantidad de ordenes pagadas
                Pdf += "<h4><b> ORDENES PAGADAS </b>" + ordensPagadas + "</h4></br>";
                decimal porcentaje;
                try
                {
                    porcentaje = (Convert.ToDecimal(ordensPagadas) / Convert.ToDecimal(met.SumaTotal)) * 100;//porcentaje
                    porcentaje = decimal.Round(porcentaje, 4);
                }
                catch (Exception ex) {
                    porcentaje = 0;
                }
                
                Pdf += "<h4><b> Porcentaje Cumplido </b>" + porcentaje + "%</h4></br>";
            }
            RealizarPdf(Pdf);
        }
        
       

        /***********************************Usado Por pagos*****************************************/
        public void CrearReciboPago(string noOrden,string codigoRecibo) {
            string PDF = "";
            //ENCABEZADO
            DatosCliente CLIENTE = conexion.ObtenerDatosGenerales(noOrden);
            string SALDO = conexion.ObtenerSaldoOrden(noOrden);
            string pagoValor = conexion.ObtenerValorPago(noOrden);
            UsuarioEmpleado usuario = conexion.obtenerDatosGeneralesEmpleado(noOrden);
            LinkedList<string> cheques = conexion.ObtenerDetallePagoCHEQUE(noOrden);
            LinkedList<string> tarjetas = conexion.ObtenerDetallePagoTARJETA(noOrden);
            LinkedList<string> efectivo = conexion.ObtenerDetallePagoEFECTIVO(noOrden);

            const string comilla = "\"";
            PDF += "<h1> RECIBO DE PAGO: </h1><br/>";
            PDF += "<FONT FACE= " + comilla + "courier new" + comilla + "SIZE=4 COLOR=" + comilla + "Black" + comilla + "><p> <b> ID DEL RECIBO:  </b>"  + codigoRecibo+"</p><br/>";
            PDF += "<h4><b>NO DE ORDEN: </b>" + noOrden + "</h4></br>";
            //CLIENTE
            PDF += "<h3><b>DATOS DE CLIENTE:</b> </h3></br/>";
            PDF += "<h4><b>NIT DE CLIENTE: </b>" + CLIENTE.Nit + "</h4></br>";
            PDF += "<h4><b>NOMBRE DE CLIENTE: </b>" + CLIENTE.Nombre+" "+CLIENTE.Apellido + "</h4></br>";
            PDF += "<h4><b>DIRECCION DE CLIENTE: </b>" + CLIENTE.Direccion1 + "</h4></br>";
            //EMPLEADO
            PDF += "<h3><b>DATOS DE EMPLEADO:</b> </h3></br/>";
            PDF += "<h4><b>NIT DE EMPLEADO: </b>" + usuario.Nit + "</h4></br>";
            PDF += "<h4><b>NOMBRE DE EMPLEADO: </b>" + usuario.Nombre + "</h4></br>";
            PDF += "<h4><b>PUESTO: </b>" + usuario.Puesto + "</h4></br>";
           //

            PDF += "<h4><b>FECHA DE CREACION: </b>" + today + "</h4></br>";
            PDF += "<h4><b>VALOR: </b>" +pagoValor+  "</h4></br>";
            PDF += "<h4><b>SALDO PENDIENTE: </b>" + SALDO + "</h4></br>";
            PDF += "<h3><b>DETALLE DE PAGOS:</b> </h3></br/>";
            foreach (string x in cheques) { 
             PDF += "<h4><b>codigo de Cheque: </b>" + x + "</h4></br>";
            }
            foreach (string c in tarjetas) {
                PDF += "<h4><b>NO DE AUTORIZACIONES: </b>" + c+ "</h4></br>";            
            }
            foreach (String k in efectivo) {
                PDF += "<h4><b>efectivos: </b>" + k + "</h4></br>"; 
            }
            RealizarPdf(PDF);           
        }

        public void CrearFacturaPago(string NoOrden) {
            DatosCliente CLIENTE = conexion.ObtenerDatosGenerales(NoOrden);
            UsuarioEmpleado empleado = conexion.obtenerDatosGeneralesEmpleado(NoOrden);
            LinkedList<Productos> detalleProducto = conexion.DetalleOrden(NoOrden);
            string total = conexion.ObtenerTotalOrden(NoOrden);
            string PDF = "";
            //ENCABEZADO
                      const string comilla = "\"";
            PDF += "<h1> FACTURA DE PAGO: </h1><br/>";
            PDF += "<FONT FACE= " + comilla + "courier new" + comilla + "SIZE=4 COLOR=" + comilla + "Black" + comilla + "><br/>";
           
            //EMPRESA
            PDF += "<h3><b>EMPRESA:</b>Productos Magnificos S.A. </h3></br/>";
            PDF += "<h4><b>NIT: </b>489456-4</h4></br>";
            PDF += "<h4><b>TELEFONO: </b>2488-888</h4></br>";
            PDF += "<h4><b>DIRECCION: </b>4ta Av. Zona 12 Calzada Aguilar Batres</h4></br>";
            //CLIENTE
            PDF += "<h3><b>DATOS DE CLIENTE:</b> </h3></br/>";
            PDF += "<h4><b>NIT: </b>" + CLIENTE.Nit + "</h4></br>";
            PDF += "<h4><b>NOMBRE: </b>" + CLIENTE.Nombre + " " + CLIENTE.Apellido + "</h4></br>";
            PDF += "<h4><b>DIRECCION: </b>" + CLIENTE.Direccion1 + "</h4></br>";
            PDF += "<h4><b>Telefono: </b>" + CLIENTE.Telefono1 + "</h4></br>";
            //EMPLEADO
            PDF += "<h3><b>DATOS DE EMPLEADO:</b> </h3></br/>";
            PDF += "<h4><b>NIT DE EMPLEADO: </b>" + empleado.Nit + "</h4></br>";
            PDF += "<h4><b>NOMBRE DE EMPLEADO: </b>" + empleado.Nombre + "</h4></br>";
            PDF += "<h4><b>PUESTO: </b>" + empleado.Puesto + "</h4></br>";
            //Monto total de factura
            PDF += "<h3><b>TOTAL:</b>"+total+" </h3></br/>";
            PDF += "<h4><b>FECHA: </b>" + today + "</h4></br>";
            PDF += "<h2> <b> DETALLE ORDEN: </b></h2><br/>";
            //DESCRIPCION
            PDF += "<TABLE BORDER='0.1'>";
            PDF += "<TR><TD> <b>ID Producto</b> </TD><TD> <b>Nombre Producto</b> </TD> <TD> <b>Cantidad</b> </TD> <TD> <b>Precio Unitario</b></TD> <TD><b> Precio Total</b></TD> </TR>";
            //PRODUCTOS
            foreach (Productos xx in detalleProducto)
            {
                PDF += "<TR><TD>" + xx.CodigoProducto + "</TD><TD>" + xx.Nombre + "</TD><TD>" + xx.Cantidad1 + "</TD><TD>" + xx.Precio + "</TD><TD>" + xx.PrecioTotal1 + "</TD></TR>";
            }

            PDF += "</TABLE><br/>";
            RealizarPdf(PDF);
        }
        
        /***************************************Anular Ventas*************************************/
        public void CrearRecibo(string Razon,string noOrden) {
            string pago = conexion.ObtenerValorPago(noOrden);
            LinkedList<PagoOrden> pagos = conexion.NumeroPagosOrden(noOrden);
            string Pdf="";
            const string comilla = "\"";
            Pdf += "<h1> RECIBO ANULACION: </h1><br/>";
            Pdf += "<FONT FACE= " + comilla + "courier new" + comilla + "SIZE=4 COLOR=" + comilla + "Black" + comilla + "><p> <b>  Recibo  </b></p><br/>";
            Pdf += "<h4>NOTA DE CREDITO POR ANULACION:</b>"+Razon+"</h4><br/>";
            Pdf += "<h4><b>MONTO: </b>"+pago+"</h4></br>";
            //obtengo el detalle de orden
            Pdf += "<TABLE BORDER='0.1'>";
            Pdf += "<TR><TD> <b>Codigo Orden</b></TD> <TD> <b>Moneda de Pago</b> </TD> <TD> <b>Tasa de Cambio</b></TD> <TD><b> Monto($)</b></TD> </TR>";
            foreach (PagoOrden d in pagos)
            {
                Pdf += "<TR><TD>" + d.NoOrden1 + "</TD><TD>" + d.MonedaPago1 + "</TD><TD>" + d.Monedavalor1 + "</TD><TD>" + d.ValorPago1 + "</TD></TR>";
            }
            Pdf += "</TABLE><br/>";
            RealizarPdf(Pdf);       
        }
        
        public void CrearFactura(string noOrden) {
            string pago = conexion.ObtenerValorPago(noOrden);
            LinkedList<PagoOrden> pagos = conexion.NumeroPagosOrden(noOrden);
            string Pdf = "";
            const string comilla = "\"";
            Pdf += "<h1> FACTURA POR ANULACION: </h1><br/>";
            Pdf += "<FONT FACE= " + comilla + "courier new" + comilla + "SIZE=4 COLOR=" + comilla + "Black" + comilla + "><p> <b>  FACTURA  </b></p><br/>";
            Pdf += "<h4>MONTO PAGADO:</b>" + pago + "</h4><br/>";
            //EMPIEZA LOS DETALLE DE PAGOS
            Pdf += "<h2>DETALLE DE PAGO</b></h2><br/>";
            //obtengo el detalle de orden
            Pdf += "<TABLE BORDER='0.1'>";
            Pdf += "<TR><TD> <b>Codigo Orden</b></TD> <TD> <b>Moneda de Pago</b> </TD> <TD> <b>Tasa de Cambio</b></TD> <TD><b> Monto($)</b></TD> </TR>";
            foreach (PagoOrden d in pagos)
            {
             Pdf += "<TR><TD>" + d.NoOrden1 + "</TD><TD>" + d.MonedaPago1 + "</TD><TD>" + d.Monedavalor1 + "</TD><TD>" + d.ValorPago1 + "</TD></TR>";
            }
            Pdf += "</TABLE><br/>";
            RealizarPdf(Pdf);
        }

        /*************Gerente**************/
        public void CrearReporteMetasxClientes(string nitCliente) {
            DatosCliente cliente = conexion.ConsultaDatosCliente(nitCliente);
            //obtengo los numeros de ordenes del cliente
            LinkedList<string> ordenes = conexion.NumerosOrdenesCliente(nitCliente);
            string Pdf = "";
            const string comilla = "\"";
            Pdf += "<h1> REPORTE METAS X CLIENTE: </h1><br/>";
            Pdf += "<FONT FACE= " + comilla + "courier new" + comilla + "SIZE=4 COLOR=" + comilla + "Black" + comilla + "><p> <b> </b></p><br/>";
            Pdf += "<h4>FECHA INICIO:</b>" +   "</h4><br/>";
            Pdf += "<h4>FECHA FIN:</b>" + "</h4><br/>";

            Pdf += "<h2>DATOS DE CLIENTE</b>" + "</h2><br/>";
            Pdf += "<h4>NIT:</b>" +cliente.Nit+ "</h4><br/>";
            Pdf += "<h4>NOMBRE:</b>" +cliente.Nombre+ "</h4><br/>";
            Pdf += "<h4>DIRECCION:</b>" + cliente.Direccion1 + "</h4><br/>";
            //empiezan los detalles
            Pdf += "<h2>DETALLE DE VENTA</b></h2><br/>";
            decimal sumas = 0;
            foreach (string d in ordenes)
            {
                //obtengo los empleados que lo despacharon
                UsuarioEmpleado usuario= conexion.obtenerDatosGeneralesEmpleado(d);
                Pdf += "<h3> <b>DATOS EMPLEADO </b> </h3></br>";
                Pdf += "<h4>NIT:</b>" + usuario.Nit + "</h4><br/>";
                Pdf += "<h4>NOMBRE:</b>" + usuario.Nombre+ "</h4><br/>";
                Pdf += "<h4>PUESTO:</b>" + usuario.Puesto + "</h4><br/>";
                Pdf += "<h4>No Orden:</b>" + d + "</h4><br/>";
                
                LinkedList<Productos> detalle = conexion.DetalleOrden(d);
                if (detalle.Count > 0) {
                    //obtengo el detalle de orden
                    Pdf += "<TABLE BORDER='0.1'>";
                    Pdf += "<TR><TD> <b>Nombre Producto</b> </TD> <TD> <b>Cantidad</b> </TD> <TD> <b>Precio Unitario($)</b></TD> <TD><b> Precio Total($)</b></TD> </TR>";
                    //DETALLE DE PRODUCTOS EN LA ORDEN
                    foreach (Productos xx in detalle)
                    {
                        Pdf += "<TR><TD>" + xx.Nombre + "</TD><TD>" + xx.Cantidad1 + "</TD><TD>" + xx.Precio + "</TD><TD>" + xx.PrecioTotal1 + "</TD></TR>";
                        sumas += Convert.ToDecimal(xx.PrecioTotal1);
                    }
                    Pdf += "</TABLE><br/>";
                    Pdf += "<h4>Total($) :</b>" + sumas + "</h4><br/><br/><br/>";
                    sumas = 0;                
                }
                LinkedList<PagoOrden> pagos = conexion.NumeroPagosOrden(d);
                if (pagos.Count > 0) {

                    Pdf += "<h2>Detalle de pago</b></h2><br/>";
                    Pdf += "<TABLE BORDER='0.1'>";
                    Pdf += "<TR><TD> <b>CodigoOrden</b></TD> <TD> <b>Moneda de Pago</b> </TD> <TD> <b>Tasa de Cambio</b></TD> <TD><b> Monto($)</b></TD> </TR>";
                    decimal suma = 0;
                    foreach (PagoOrden h in pagos)
                    {
                        Pdf += "<TR><TD>" + h.NoOrden1 + "</TD><TD>" + h.MonedaPago1 + "</TD><TD>" + h.Monedavalor1 + "</TD><TD>" + h.ValorPago1 + "</TD></TR>";
                        suma = suma + Convert.ToDecimal(h.ValorPago1);    
                    }
                    Pdf += "</TABLE><br/>";
                    Pdf += "<h2>Sumatoria </b>" + suma + "</h2><br/>";
                }
            }
            LinkedList<PagoOrden> PagoAnulado = conexion.NumerosOrdenAnuladosPagos();
            if (PagoAnulado.Count > 0)
            {

                Pdf += "<h2>Detalle de pago Anulado</b></h2><br/>";
                Pdf += "<TABLE BORDER='0.1'>";
                Pdf += "<TR><TD> <b>CodigoOrden</b></TD> <TD> <b>Valor de Pago</b> </TD> <TD> <b>Tipo de pago</b></TD> </TR>";
                decimal suma = 0;
                foreach (PagoOrden h in PagoAnulado)
                {
                    Pdf += "<TR><TD>" + h.NoOrden1 + "</TD><TD>" + h.MonedaPago1 + "</TD><TD>" + h.TipoPago1 + "</TD></TR>";
                    suma = suma + Convert.ToDecimal(h.MonedaPago1);
                }
                Pdf += "</TABLE><br/>";
                Pdf += "<h2>Sumatoria </b>" + suma + "</h2><br/>";
            }
            RealizarPdf(Pdf);
        }

        public void CrearReporteVentaXProducto() {
            LinkedList<string> codigoProducto = conexion.CodigosDeProductos();
            string Pdf = "";
            const string comilla = "\"";
            Pdf += "<h1> REPORTE VENTA X PRODUCTO: </h1><br/>";
            Pdf += "<FONT FACE= " + comilla + "courier new" + comilla + "SIZE=4 COLOR=" + comilla + "Black" + comilla + "><p> <b> </b></p><br/>";
            Pdf += "<h4>FECHA: </b>" +today.ToString("dd/MM/yy")+ "</h4><br/>";
           
            Pdf += "<h2>Detalles</h2><br>";
            decimal totales = 0;
            decimal Cantidad = 0;
            foreach(string a in codigoProducto){
           
                LinkedList<Productos> detalle = conexion.DetalleVentasProducto(a);
                if (detalle.Count > 0) {
                    Pdf += "<h2>Codigo Producto:</b>"+a+"</h2><br/>";
                    Pdf += "<TABLE BORDER='0.1'>";
                    Pdf += "<TR><TD> <b>Codigo Orden</b> </TD> <TD> <b>Cantidad</b> </TD> <TD> <b>Precio Unitario($)</b></TD> <TD><b> Precio Total($)</b></TD> </TR>";
                    foreach (Productos p in detalle)
                    {
                        Pdf += "<TR><TD>" + p.CodigoOrden1 + "</TD><TD>" + p.Cantidad1 + "</TD><TD>" + p.Precio + "</TD><TD>" + p.PrecioTotal1 + "</TD></TR>";
                        totales = totales + Convert.ToDecimal(p.PrecioTotal1);
                        Cantidad = Cantidad + Convert.ToDecimal(p.Cantidad1);
                    }
                    Pdf += "</TABLE><br/>";
                    Pdf += "<h4>TOTAL:</b>" + totales + "</h4><br/>";
                    Pdf += "<h4>CANTIDAD TOTAL:</b>" + Cantidad + "</h4><br/><br/>";
                    totales = 0;
                    Cantidad = 0;
                }               
            }
            RealizarPdf(Pdf);
        }

        public void CrearReporteVetaXCategoria() {
            LinkedList<string> categorias = conexion.CodigosCategorias();
            string Pdf = "";
            const string comilla = "\"";
            Pdf += "<h1> REPORTE VENTA X PRODUCTO: </h1><br/>";
            Pdf += "<FONT FACE= " + comilla + "courier new" + comilla + "SIZE=4 COLOR=" + comilla + "Black" + comilla + "><p> <b> </b></p><br/>";
            Pdf += "<h4>FECHA :</b>" + today.ToString("dd/MM/yy") + "</h4><br/>";
            Pdf += "<h2>DETALLE DE CATEGORIAS</b></h2><br/>";            
            //taba de detalles
            decimal cantidad = 0;
            String ordens = "";
            decimal promedio=0;
            foreach (string d in categorias) {
                LinkedList<Productos> productos = conexion.DetalleVentasCategoria(d);
                if (productos.Count > 0) {
                    Pdf += "<h3>Codigo de Categoria:</b>" + d + "</h3><br/>";
                    Pdf += "<TABLE BORDER='0.1'>";
                    Pdf += "<TR><TD> <b>Producto</b></TD> <TD> <b>Cantidad </b> </TD> <TD><b>Total($) </b></TD></TR>";
                    foreach (Productos h in productos)
                    {
                        Pdf += "<TR><TD>" + h.Nombre + "</TD><TD>" + h.Cantidad1 + "</TD><TD>" + h.PrecioTotal1 + "</TD></TR>";
                        cantidad = cantidad = Convert.ToDecimal(h.Cantidad1);
                        promedio = promedio + Convert.ToDecimal(h.PrecioTotal1);
                    }
                    Pdf += "</TABLE><br/>";
                    ordens = conexion.TotalDeOrdenesCategoria(d);
                    Pdf += "<h4>Cantidad Total</b>" + cantidad + "</h4><br/>";
                    Pdf += "<h4>Cantidad Ordenes</b>" + ordens + "</h4><br/>";
                    Pdf += "<h4>Promedio de Ordenes</b>" + Convert.ToInt32(ordens) / 2 + "</h4><br/>";
                    Pdf += "<h4>Promedio($)</b>" + promedio / 2 + "</h4><br/>";
                    cantidad = 0;
                    promedio = 0;
                    ordens = "";                
                }                
            }                  
        RealizarPdf(Pdf);  
        }

        /**************ME RETORNA EL DOCUMENTO EN DONDE LO ESTE LLAMANDO*********/
        public Document retornarDocumento() {
            return Doc;
        }
    }
}