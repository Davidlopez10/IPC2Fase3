using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IPC2_Fase3_201314694.Clasees;

namespace IPC2_Fase3_201314694
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        ConexionSQL CONEXION = new ConexionSQL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null) {
                Response.Redirect("");
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text.Equals("Admi") && txtpassword.Text.Equals("administrador")) {
                //es administrador
                Session["Usuario"] = txtUsuario.Text;
                Response.Redirect("/Paginas/Administrador.aspx");//sirve para redireccionarnos a administrador
            }
            else if (txtUsuario.Text.Equals("") && txtpassword.Text.Equals(""))
            {
                lblError.Text = "Falta ingresar datos";
                lblError.Visible = true;
            }
            else {
                if (CONEXION.Login(txtUsuario.Text, txtpassword.Text).Equals(txtUsuario.Text))
                {
                    Session["Usuario"] = CONEXION.Login2(txtUsuario.Text);
                    UsuarioEmpleado usuarioNuevo = new UsuarioEmpleado();
                    usuarioNuevo = CONEXION.Login2(txtUsuario.Text);
                    if (usuarioNuevo.Puesto.Equals("Gerente"))
                    {
                        Response.Redirect("/Paginas/Gerente.aspx");
                    }
                    else if (usuarioNuevo.Puesto.Equals("Supervisor")) {
                        Response.Redirect("/Paginas/Supervisor.aspx");
                    }
                    else if (usuarioNuevo.Puesto.Equals("Vendedor"))
                    {
                        Response.Redirect("/Paginas/Vendedor.aspx");
                    }
                    else {
                        lblError.Text = "No existe usuario";
                        lblError.Visible = true;
                    }
                    // Response.Redirect("/principal.aspx");//sirve para redireccionarnos
                }
                else {
                    lblError.Text = "Usuario o Password incorrectos";
                    lblError.Visible = true;                   
                }           
            }
        }
    }
}