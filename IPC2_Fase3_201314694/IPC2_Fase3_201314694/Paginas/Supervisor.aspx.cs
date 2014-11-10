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
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Ventas.aspx"); 
        }
    }
}