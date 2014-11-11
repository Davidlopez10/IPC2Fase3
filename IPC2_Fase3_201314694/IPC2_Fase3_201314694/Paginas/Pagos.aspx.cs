using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IPC2_Fase3_201314694.Clasees;

namespace IPC2_Fase3_201314694.Paginas
{
    public partial class Pagos : System.Web.UI.Page
    {
        ConexionSQL conexion = new ConexionSQL();
        UsuarioEmpleado usuario = new UsuarioEmpleado();
        Reportes reporte = new Reportes();
        protected void Page_Load(object sender, EventArgs e)
        {
            usuario = (UsuarioEmpleado)Session["Usuario"];
            if(!IsPostBack){                
                
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            if (usuario.Puesto.Equals("Gerente"))
            {
                LinkedList<string> ordenes = conexion.OrdenesPagos();
                DropDownList1.Items.Clear();
                foreach (string fj in ordenes)
                {
                    DropDownList1.Items.Add(fj);
                }
                Button2.Visible = false;
                Button3.Visible = false;
            }
            else if (usuario.Puesto.Equals("Supervisor"))
            {
                LinkedList<string> ordenpag = conexion.OrdenesPagoSupervisor(usuario.Nit);
                DropDownList1.Items.Clear();
                foreach (string fj in ordenpag)
                {
                    DropDownList1.Items.Add(fj);
                }
                Button2.Visible = false;
                Button3.Visible = false;            
            }
            else if (usuario.Puesto.Equals("Vendedor"))
            {
                LinkedList<string> ordPago = conexion.OrdenesPagoEmpleado(usuario.Nit);
                DropDownList1.Items.Clear();
                foreach (string fj in ordPago)
                {
                    DropDownList1.Items.Add(fj);
                }
                Button2.Visible = false;
                Button3.Visible = false;
            }
            else {           
            }
            TextBox7.Text = "";
            Label16.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox1.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";



        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = -1;
            Response.Redirect("/Paginas/Ventas.aspx");
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {/*Solo hago visible los botones cuando se elige la opcion correcta, aparecen las casillas que debe de aparecer y desaparecen
          las demas*/
            if (DropDownList2.SelectedValue.Equals("Cheque"))
            {
                Label4.Visible = true;
                Label5.Visible = true;
                Label6.Visible = true;
                TextBox2.Visible = true;
                TextBox3.Visible = true;
                TextBox4.Visible = true;
                Label7.Visible = false;
                Label8.Visible = false;
                TextBox5.Visible = false;
                TextBox6.Visible = false;
            }
            else if (DropDownList2.SelectedValue.Equals("Tarjeta de Credito")) 
            {
                Label7.Visible = true;
                Label8.Visible = true;
                TextBox5.Visible = true;
                TextBox6.Visible = true;
                Label4.Visible = false;
                Label5.Visible = false;
                Label6.Visible = false;
                TextBox2.Visible = false;
                TextBox3.Visible = false;
                TextBox4.Visible = false;
            
            }
            else if (DropDownList2.SelectedValue.Equals("Efectivo"))
            {
                Label4.Visible = false;
                Label5.Visible = false;
                Label6.Visible = false;
                TextBox2.Visible = false;
                TextBox3.Visible = false;
                TextBox4.Visible = false;
                Label7.Visible = false;
                Label8.Visible = false;
                TextBox5.Visible = false;
                TextBox6.Visible = false;
            }
            else {
                Label4.Visible = false;
                Label5.Visible = false;
                Label6.Visible = false;
                TextBox2.Visible = false;
                TextBox3.Visible = false;
                TextBox4.Visible = false;
                Label7.Visible = false;
                Label8.Visible = false;
                TextBox5.Visible = false;
                TextBox6.Visible = false;           
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if ((TextBox1.Text.Equals("") || TextBox7.Text.Equals("")))
            {
                Label14.Text = "Faltan Datos";
                Label14.Visible = true;
            }else{
                string tabla;
                string datos;
                decimal saldo = Convert.ToDecimal(conexion.ObtenerSaldoOrden(DropDownList1.SelectedValue));//saldo que tiene la orden
                decimal pagos = Convert.ToDecimal(TextBox1.Text);
                Moneda moneda = conexion.ObtenerMoneda(DropDownList3.SelectedValue);
                /*Se conveirte la moneda a dolar*/
                if (!moneda.NameMoneda1.Equals("Dolar"))
                {
                    decimal valorvd = pagos * Convert.ToDecimal(moneda.Valor);//E, quetazales
                    pagos = valorvd * Convert.ToDecimal("1");//convertido a $
                }

                /*SE verifica que el pago no se mayor al saldo de la orden*/
                if (pagos > saldo)
                {
                    Label14.Text = "El pago es mayor al Saldo de La orden Elejida!!!!!!!!!!";
                    Label14.Visible = true;
                    Button2.Visible = false;
                }
                else
                {


                    if (DropDownList2.SelectedValue.Equals("Cheque"))
                    {

                        if (TextBox3.Text.Equals("") || TextBox2.Text.Equals("") || TextBox4.Text.Equals(""))
                        {
                            Label14.Text = "Faltan Datos de Cheque";
                            Label14.Visible = true;
                        }
                        else
                        {
                            //Inserto en tabla pago
                            DatosCliente cliente = conexion.ObtenerDatosGenerales(DropDownList1.SelectedValue);
                            tabla = "PAGO(CODIGOPAGO,TIPOMONEDA,TIPO,VALORPAGO,NOORDEN)";
                            datos = "" + TextBox7.Text + ",'" + DropDownList3.SelectedValue + "','Cheque'," + Convert.ToString(pagos).Replace(",", ".") + ",'" + DropDownList1.SelectedValue + "'";
                            conexion.Insertar(tabla, datos);
                            //inserto en tabla cheque
                            tabla = "CHEQUE(CODIGOCHEQUE, CODIGOPAGO, CUENTABANCARIA,INFORMACIONBANCO)";
                            datos = "" + TextBox4.Text + "," + TextBox7.Text + ",'" + TextBox3.Text + "','" + TextBox2.Text + "'";
                            conexion.Insertar(tabla, datos);
                            //actualizaciones de tablas de orden y limite de credito del cliente
                            decimal total = saldo - pagos;
                            conexion.ActualizarSaldoOrden(DropDownList1.SelectedValue, Convert.ToString(total));//actualizo el nuevo saldo
                            decimal limite = Convert.ToDecimal(conexion.ObtenerLimiteCredito(DropDownList1.SelectedValue));
                            total = limite + pagos;
                            conexion.ActualizarLimiteCreditoCliente(cliente.Nit, Convert.ToString(total));
                            Label14.Visible = false;
                            Button2.Visible = true;
                        }
                    }
                    else if (DropDownList2.SelectedValue.Equals("Tarjeta de Credito"))
                    {
                        if (TextBox5.Text.Equals("") || TextBox6.Text.Equals(""))
                        {
                            Label14.Text = "FAltan Los datos de la Tarjeta de Credito";
                            Label14.Visible = true;
                        }
                        else
                        {
                            DatosCliente cliente = conexion.ObtenerDatosGenerales(DropDownList1.SelectedValue);
                            //guardo en tabla de pagos primero 
                            tabla = "PAGO(CODIGOPAGO,TIPOMONEDA,TIPO,VALORPAGO,NOORDEN)";
                            datos = "" + TextBox7.Text + ",'" + DropDownList3.SelectedValue + "','Tarjeta de Credito'," + Convert.ToString(pagos).Replace(",", ".") + ",'" + DropDownList1.SelectedValue + "'";
                            conexion.Insertar(tabla, datos);
                            //luego ingreso en la tabla de tarjeta
                            tabla = "TARJETA(NUMEROAUTORIZACION,CDPAGO,EMISOR)";
                            datos = "" + TextBox6.Text + "," + TextBox7.Text + ",'" + TextBox5.Text + "'";
                            conexion.Insertar(tabla, datos);
                            //proceso de actualizaciones
                            decimal total = saldo - pagos;
                            conexion.ActualizarSaldoOrden(DropDownList1.SelectedValue, Convert.ToString(total));//actualizo el nuevo saldo
                            decimal limite = Convert.ToDecimal(conexion.ObtenerLimiteCredito(DropDownList1.SelectedValue));
                            total = limite + pagos;
                            conexion.ActualizarLimiteCreditoCliente(cliente.Nit, Convert.ToString(total));
                            Label14.Visible = false;
                            Button2.Visible = true;
                        }
                    }
                    else if (DropDownList2.SelectedValue.Equals("Efectivo"))
                    {
                        DatosCliente cliente = conexion.ObtenerDatosGenerales(DropDownList1.SelectedValue);
                        tabla = "PAGO(CODIGOPAGO,TIPOMONEDA,TIPO,VALORPAGO,NOORDEN)";
                        datos = "" + TextBox7.Text + ",'" + DropDownList3.SelectedValue + "','Efectivo'," + Convert.ToString(pagos).Replace(",", ".") + ",'" + DropDownList1.SelectedValue + "'";
                        conexion.Insertar(tabla, datos);
                        //actualizacion de datos
                        decimal total = saldo - pagos;
                        conexion.ActualizarSaldoOrden(DropDownList1.SelectedValue, Convert.ToString(total));//actualizo el nuevo saldo
                        decimal limite = Convert.ToDecimal(conexion.ObtenerLimiteCredito(DropDownList1.SelectedValue));
                        total = limite + pagos;
                        conexion.ActualizarLimiteCreditoCliente(cliente.Nit, Convert.ToString(total));
                        Label14.Visible = false;
                        Button2.Visible = true;
                    }
                    else
                    {
                        Label14.Text = "Elija una forma de Pago";
                        Label14.Visible = true;
                        Button2.Visible = false;
                    }

                }
                if (pagos == saldo)
                {
                    Button3.Visible = true;
                }
                else
                {
                    Button3.Visible = false;
                }
            }            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Label13.Text = DropDownList1.SelectedValue;
            TextBox8.Text = "";
            Label17.Visible = false;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (TextBox8.Text.Equals(""))
            {
                Label17.Visible = true;
                Label17.Text = "Ingrese El codigo del Recibo";
            }
            else {
                Label17.Visible = false;                
                reporte.CrearReciboPago(Label13.Text,TextBox8.Text);
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment; filename= ReciboCDPago_" +TextBox7.Text + ".pdf");
                System.Web.HttpContext.Current.Response.Write(reporte.retornarDocumento());
                Response.Flush();
                Response.End();           
            }          
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            reporte.CrearFacturaPago(DropDownList1.SelectedValue);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename= Factura_" + DropDownList1.SelectedValue + ".pdf");
            System.Web.HttpContext.Current.Response.Write(reporte.retornarDocumento());
            Response.Flush();
            Response.End();
        }

       
        protected void Button5_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue.Equals(""))
            {
                Label14.Text = "No hay Ordnes para Pagos";
                Label14.Visible = true;
            }
            else {
                Label14.Visible = false;
                Label16.Text = conexion.ObtenerSaldoOrden(DropDownList1.SelectedValue);
            }
            
        }
    }
}