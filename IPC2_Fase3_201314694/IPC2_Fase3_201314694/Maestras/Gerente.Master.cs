using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IPC2_Fase3_201314694.Clasees;

namespace IPC2_Fase3_201314694.Maestras
{
    public partial class Gerente : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioEmpleado usuario = (UsuarioEmpleado)Session["Usuario"];
            if (usuario != null)
            {
                nombreUsuario.Text = usuario.Nombre;
            }
            else
            {
                Response.Redirect("/Login.aspx");
            }
        }

        protected void lkbtlogout_Click(object sender, EventArgs e)
        {
            Session.Remove("Usuario");
            Response.Redirect("/Login.aspx");  
        }
    }
}