using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IPC2_Fase3_201314694.Clasees;
using System.Data;

namespace IPC2_Fase3_201314694.Paginas
{
    public partial class AnularVenta : System.Web.UI.Page
    {
        UsuarioEmpleado usuario = new UsuarioEmpleado();
        ConexionSQL conexion = new ConexionSQL();
        DateTime today = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            usuario = (UsuarioEmpleado)Session["Usuario"];
            Label3.Text = today.ToString();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Ventas.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (usuario.Puesto.Equals("Gerente"))
            {
                LinkedList<string> ordenes = conexion.AnulacionVentaOrdenConPago();
                DropDownList1.Items.Clear();
                foreach (string x in ordenes) {
                    DropDownList1.Items.Add(x);               
                }
            }
            else if (usuario.Puesto.Equals("Supervisor"))
            {
                LinkedList<string> ordens = conexion.AnulacionVentaOrdenSupervisor(usuario.Nit);
                DropDownList1.Items.Clear();
                foreach (string x in ordens)
                {
                    DropDownList1.Items.Add(x);
                }
            } 
            Button2.Visible = false;
            Button3.Visible = false;
            Label4.Visible = false;
            MultiView1.ActiveViewIndex = 0;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue.Equals(""))
            {
                Label4.Text = "No hay Ventas para Cancelar";
                Label4.Visible = true;
            }
            else {
                DropDownList1.Enabled = false;
                Button2.Visible = true;
                Button3.Visible = true;
                Label4.Text = conexion.ActualizarOrdeVentaAnulada(DropDownList1.SelectedValue);            
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text.Equals(""))
            {
                Label4.Text = "Falta La Razon por La Cual se Quiere Anular la Venta";
            }
            else {
                Label4.Text = "";
                Reportes reporte = new Reportes();
                reporte.CrearRecibo(TextBox1.Text, DropDownList1.SelectedValue);
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment; filename=ReciboAnulacion_" + DropDownList1.SelectedValue + ".pdf");
                System.Web.HttpContext.Current.Response.Write(reporte.retornarDocumento());
                Response.Flush();
                Response.End();
            }
            
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Reportes reporte = new Reportes();
            reporte.CrearFactura(DropDownList1.SelectedValue);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename= Factura_" +DropDownList1.SelectedValue+ ".pdf");
            System.Web.HttpContext.Current.Response.Write(reporte.retornarDocumento());
            Response.Flush();
            Response.End();
        }
    }
}