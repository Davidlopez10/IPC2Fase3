using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPC2_Fase3_201314694.Paginas
{
    public partial class Vendedor : System.Web.UI.Page
    {
        UsuarioEmpleado usuario = new UsuarioEmpleado();
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
           Reportes reporte = new Reportes();           
           reporte.CrearReporte_VentaMetaVendedor(usuario,DropDownList1.SelectedValue);
           Response.ContentType = "application/pdf";
           Response.AddHeader("content-disposition", "attachment; filename= Reporte_" + usuario.Nombre + ".pdf");
           System.Web.HttpContext.Current.Response.Write(reporte.retornarDocumento());
           Response.Flush();
           Response.End();
          
       }

       protected void LinkButton2_Click(object sender, EventArgs e)
       {
           MultiView1.ActiveViewIndex = 0;
       }

       protected void Button2_Click(object sender, EventArgs e)
       {
           Reportes reporte = new Reportes();
           reporte.CrearReporteVenta_MetaXCategoria(usuario, DropDownList2.SelectedValue);
           Response.ContentType = "application/pdf";
           Response.AddHeader("content-disposition", "attachment; filename= reporte_meta" + usuario.Nombre + ".pdf");
           System.Web.HttpContext.Current.Response.Write(reporte.retornarDocumento());
           Response.Flush();
           Response.End();

       }
    }
}