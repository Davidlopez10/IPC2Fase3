using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IPC2_Fase3_201314694.Clasees;

namespace IPC2_Fase3_201314694.Paginas
{
    public partial class Gerente : System.Web.UI.Page
    {
        UsuarioEmpleado usuario;
        Reportes reporte = new Reportes();
        protected void Page_Load(object sender, EventArgs e)
        {
            usuario = (UsuarioEmpleado)Session["Usuario"];
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Ventas.aspx");            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (DropDownList1.Text.Equals(""))
            {

            }
            else {
                reporte.CrearReporteMetasxClientes(DropDownList1.SelectedValue);
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment; filename= VentaxCliente.pdf");
                System.Web.HttpContext.Current.Response.Write(reporte.retornarDocumento());
                Response.Flush();
                Response.End();            
            }            
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            reporte.CrearReporteVentaXProducto();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename= Reporte_VentaXProducto.pdf");
            System.Web.HttpContext.Current.Response.Write(reporte.retornarDocumento());
            Response.Flush();
            Response.End();
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            reporte.CrearReporteVetaXCategoria();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename= Reporte_VentaXcategoria.pdf");
            System.Web.HttpContext.Current.Response.Write(reporte.retornarDocumento());
            Response.Flush();
            Response.End();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }
    }
}