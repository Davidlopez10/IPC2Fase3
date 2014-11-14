using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IPC2_Fase3_201314694.Clasees;

namespace IPC2_Fase3_201314694.Paginas
{
    public partial class Supervisor : System.Web.UI.Page
    {
        UsuarioEmpleado usuario;
        Reportes reporte = new Reportes();
        protected void Page_Load(object sender, EventArgs e)
        {
            usuario = (UsuarioEmpleado)Session["Usuario"];
        }
        
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Ventas.aspx"); 
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            reporte.CrearReporte_VentaMetaVendedor(usuario, DropDownList1.Text);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename= MetaVendedor" + usuario.Nombre + ".pdf");
            System.Web.HttpContext.Current.Response.Write(reporte.retornarDocumento());
            Response.Flush();
            Response.End();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            reporte.CrearReporteVenta_MetaXCategoria(usuario, DropDownList2.SelectedValue);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename= MetaCategoria" + usuario.Nombre + ".pdf");
            System.Web.HttpContext.Current.Response.Write(reporte.retornarDocumento());
            Response.Flush();
            Response.End();
        }
    }
}