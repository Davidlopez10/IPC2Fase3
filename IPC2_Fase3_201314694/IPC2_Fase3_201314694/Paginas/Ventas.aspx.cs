using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using IPC2_Fase3_201314694.Clasees;

namespace IPC2_Fase3_201314694.Paginas
{
    public partial class Ventas : System.Web.UI.Page
    {/*
      Activar la sesion porque estan desacctivadas, por el motivo de las purebas,
      hay que activarlas tanto para esta pagina como para la masteer
      */
        DataTable tabla;
        DataSet ds;
        UsuarioEmpleado usuario= new UsuarioEmpleado();
        ConexionSQL conexion = new ConexionSQL();
     
        protected void Page_Load(object sender, EventArgs e)
        {
            usuario = (UsuarioEmpleado)Session["Usuario"];
            DateTime today = DateTime.Today;
            Label27.Text = today.ToString("dd/MM/yyyy");
            Label29.Text = today.ToString("dd/MM/yyyy");
            Label30.Text = today.ToString("dd/MM/yyyy");
            //Label11.Text = Convert.ToString(conexion.TotalDetalleOrden(TextBox2.Text));
            if (!IsPostBack) {
                if (usuario != null) {
                    if (usuario.Puesto.Equals("Gerente"))
                    {

                    }
                    else if (usuario.Puesto.Equals("Supervisor"))
                    {
                       // LinkButton4.Visible = false;
                        LinkedList<string> listaorden = conexion.ListaOrdenes(usuario.Nit);
                        foreach (string x in listaorden)
                        {
                            DropDownList6.Items.Add(x);
                            DropDownList7.Items.Add(x);
                        }

                    }
                    else if (usuario.Puesto.Equals("Vendedor"))
                    {
                        LinkButton3.Visible = false;
                       // LinkButton4.Visible = false;
                        LinkButton5.Visible = false;
                    }                
                
                }              
                else {
                    Response.Redirect("/Login.aspx");                
                }
                //columnas de las tablas
            /* ds = new DataSet();
             tabla = new DataTable();
             tabla.Columns.Add("Cantidad",typeof(string));
             tabla.Columns.Add("Producto",typeof(string));
             tabla.Columns.Add("Valor",typeof(string));
             for (int i = 0; i < 1; i++)
             {
                 DataRow row = tabla.NewRow();
                 row["Cantidad"] = "";
                 row["Producto"] = "";
                 row["Valor"] = "";
                 tabla.Rows.Add(row);
                 ds.Tables.Add(tabla);
                 GridView1.DataSource = ds.Tables[0];
                 GridView1.DataBind();
             }
            */
            }//fin del if // LinkButton1.Visible = false;
            Button7.Visible = false;        
        }

        private void BloquearView1() {
            DropDownList1.Enabled = false;
            TextBox2.Enabled = false;
            TextBox1.Enabled = false;
        }
        private void DesbloquearView1() {
            DropDownList1.Enabled = true;
            TextBox2.Enabled = true;
            TextBox2.Text = "";
            //DropDownList1.SelectedIndex = 0;
            DropDownList3.Items.Clear();
            Label8.Text = "0";
            Label9.Text = "0";
            Label11.Text = "0";
            TextBox1.Enabled = true;
            TextBox1.Text = "";
            DropDownList2.Items.Clear();
            GridView1.DataBind();
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            DesbloquearView1();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {//boton de agregar
            if (TextBox2.Text.Equals(""))
            {
                Label35.Text = "Ingrese un Numero de orden";
                Label35.Visible = true;
                TextBox1.Enabled = true;
            }
            else
            {

                if (TextBox1.Text.Equals(""))
                {
                    Label35.Text = "Ingrese Cantidad";
                    Label35.Visible = true;
                    TextBox1.Enabled = true;
                }
                else
                {
                    BloquearView1();
                    string fecha = Label27.Text;
                    TextBox1.Enabled = true;
                    Productos Tproducto = conexion.ConsultaDatosProductos(DropDownList2.SelectedValue, fecha);
                    string cantidad = TextBox1.Text;
                    string NoOrden = TextBox2.Text;
                    //Datos Generales del cliente
                    DatosCliente CLIENTE = conexion.ConsultaDatosCliente(DropDownList1.SelectedValue);
                    decimal credito = Convert.ToDecimal(CLIENTE.LimiteCredito1);

                    if ((credito > 0))//verifico si tene Credito
                    {
                        //se ingresa la orden por primera ves
                        string tabla = "ORDEN(CODIGOORDEN,TOTALPAGAR,FECHACREACION,DIASFALTANTE,SALDOFALTA,ESTADOAPROBACION,NITEMPLEADOVENDE,NITCLIENTE)";
                        string datos = "'" + NoOrden + "'," + Label11.Text.Replace(",", ".") + ",'" + fecha + "'," + 20 + "," + Label11.Text.Replace(",", ".") + ",'Procesando','" + usuario.Nit + "','" + DropDownList1.SelectedValue + "'";
                        conexion.Insertar(tabla, datos);

                        //se ingresan los detalles
                        tabla = "DETALLEPRODUCTOORDEN(NOMBREPRODUCTO,CANTIDAD,CODIGOORDEN, CODIGOPRODUCTO, VALOR)";
                        datos = "'" + DropDownList2.SelectedValue + "'," + cantidad + ",'" + NoOrden + "','" + Tproducto.CodigoProducto + "'," + Tproducto.Precio.Replace(",", ".") + "";
                        conexion.Insertar(tabla, datos);

                        decimal total = conexion.TotalDetalleOrden(TextBox2.Text);
                        Decimal Resultado = credito - total;

                        if (Resultado > 0)
                        {
                            GridView1.DataBind();//actualiza tabla
                            //Visualizar el total de la orden
                            string totalPon = Convert.ToString(total);//total para colocarlo en el comando siguente
                            conexion.ActualizarOrden(NoOrden, totalPon);//me actualiza el total y el saldo que se tiene, esto en la base de datos
                            Label11.Text = totalPon.Replace(",", ".");//muestra el total EN EL GRAFICO                    
                            //conexion.ActualizarClienteCredito(DropDownList1.SelectedValue, Convert.ToString(Resultado));
                            //Response.Write("<script language='javascript'> { window.alert('Todavia tiene Credito') } </script> ");
                            Label8.Text = Convert.ToString(Resultado).Replace(",", ".");
                            Button10.Enabled = true;
                            Label35.Visible = false;
                        }
                        else
                        {
                            //no se puede agregar porque ya no tiene ccredito
                            //Response.Write("<script language='javascript'> { window.alert('Ya no tiene Credito') } </script> ");
                            Label35.Text = "No Suficiente Tiene Credito";
                            Label35.Visible = true;
                            conexion.BorrarFilaOrden(cantidad, Tproducto.CodigoProducto, NoOrden);
                            //Label8.Text = Convert.ToString(Resultado);
                            //GridView1.RowDeleted();
                        }
                    }
                    else
                    {
                        //Response.Write("<script language='javascript'> { window.alert('No tiene Credito') } </script> ");
                        Label35.Text = "No Tiene Suficiente Credito";
                        Label35.Visible = true;
                    }
                }
            }                
        }       

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            
            if (usuario.Puesto.Equals("Vendedor")) {
                LinkedList<string> ordenes = conexion.OrdenCerrarEmpleado(usuario.Nit);//listas del propio empleado
                    DropDownList6.Items.Clear();
                foreach (string jk in ordenes) {
                    DropDownList6.Items.Add(jk);              
                }
            }
            else if (usuario.Puesto.Equals("Supervisor"))
            {
                LinkedList<string> milistaempleado = conexion.ListaOrdenes(usuario.Nit);//lita de empeados
                DropDownList6.Items.Clear();
                foreach (string x in milistaempleado)
                {
                    DropDownList6.Items.Add(x);
                }
                LinkedList<string> misListapropia = conexion.OrdenCerrarEmpleado(usuario.Nit);//mis propias listas
                foreach (string l in misListapropia) {
                    DropDownList6.Items.Add(l);                
                }
            }else if(usuario.Puesto.Equals("Gerente")){  
                DropDownList6.Items.Clear();
                LinkedList<string> Listasger=conexion.OrdenesGerente();
                foreach(string xd in Listasger){
                DropDownList6.Items.Add(xd);
                }
            }
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            Label22.Text = usuario.Nombre;
            Label24.Text = usuario.Puesto;
            if (usuario.Puesto.Equals("Supervisor")) {
                LinkedList<string> listaorden = conexion.OrdenAprobarSupervisor(usuario.Nit);
                DropDownList7.Items.Clear();
                foreach (string gh in listaorden) {
                    DropDownList7.Items.Add(gh);
                }
            }
            else if (usuario.Puesto.Equals("Gerente")) {
                LinkedList<string> listager = conexion.OrdenAprobarGerente();
                DropDownList7.Items.Clear();
                foreach (string fj in listager)
                {
                    DropDownList7.Items.Add(fj);
                }            
            }
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            
            MultiView1.ActiveViewIndex = 3;
            if (usuario.Puesto.Equals("Gerente")) {
                LinkedList<string> Anulacion = conexion.AnularOrdenGerente();
                DropDownList14.Items.Clear();
                foreach (string lk in Anulacion) {
                    DropDownList14.Items.Add(lk);
                }
            }
            else if (usuario.Puesto.Equals("Supervisor")) {
                LinkedList<string> anulacionsuper = conexion.AnularOrdenSupervisor(usuario.Nit);
                DropDownList14.Items.Clear();
                foreach (string lk in anulacionsuper)
                {
                    DropDownList14.Items.Add(lk);
                }
            }
            else if (usuario.Puesto.Equals("Vendedor"))
            {
                LinkedList<string> anulacionvend = conexion.AnularOrdenEmpleado(usuario.Nit);
                DropDownList14.Items.Clear();
                foreach (string lk in anulacionvend)
                {
                    DropDownList14.Items.Add(lk);
                }            
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/AnularVenta.aspx");
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatosCliente cliente = conexion.ConsultaDatosCliente(DropDownList1.SelectedValue);
            Label8.Text = cliente.LimiteCredito1;
            Label9.Text = cliente.CantidadOrdenVenciadas1;
            DropDownList3.Enabled = true;
            TextBox2.Enabled = true;
            DropDownList3.Items.Clear();
            Button11.Enabled = true;
            Button12.Visible = false;
            //lleno la lista que tiene el cliente a su disposicion
            LinkedList<string> listaPrecios=conexion.NombreListaPreciosCliente(DropDownList1.SelectedValue);
            foreach( string xd in listaPrecios){
                DropDownList3.Items.Add(xd);
            }
            if (listaPrecios.Count == 0) {
                Button12.Visible = true;
                Button11.Enabled = false;
            }
        }

       protected void SqlDataSource3_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            if (TextBox2.Text.Equals(""))
            {
               // Label8.Text = "0";
            }
            else
            {
                int filas = GridView1.Rows.Count;

                decimal total = conexion.TotalDetalleOrden(TextBox2.Text);
                string totalTex = Convert.ToString(total);
                Label11.Text = totalTex;
                conexion.ActualizarOrden(TextBox2.Text, totalTex);//me actualiza el total y el saldo que se tiene, esto en la base de datos
                DatosCliente CLIENTE = conexion.ConsultaDatosCliente(DropDownList1.SelectedValue);
                try
                {
                    decimal credito = Convert.ToDecimal(CLIENTE.LimiteCredito1);
                    decimal Resultado = credito - total;
                    Label8.Text = Convert.ToString(Resultado).Replace(",",".");
                }
                catch (Exception ex)
                {

                }
            }
        }

       protected void Button10_Click(object sender, EventArgs e)
       {
           //Cambiar el credito del cliente, y la cantidad de ordees
           DatosCliente cliente = conexion.ConsultaDatosCliente(DropDownList1.SelectedValue);
           int cantidadorden;
           if (cliente.CantidadOrden.Equals(""))
           {
               cantidadorden=1;
           }
           else if (cliente.CantidadOrden.Equals("NULL"))
           {
               cantidadorden = 1;
           }
           else
           {
               cantidadorden = Convert.ToInt32(cliente.CantidadOrden);
               cantidadorden = cantidadorden + 1;
           }
           string numeroFilas = this.GridView1.Rows.Count.ToString();
           string limite=Label8.Text;
           conexion.ActualizarClienteCredito(DropDownList1.SelectedValue,Convert.ToString(cantidadorden),limite);
           DesbloquearView1();       
       }

       protected void Button4_Click(object sender, EventArgs e)
       {
           if (DropDownList6.SelectedValue.Equals(""))
           {
               Label36.Text = "No hay Ordenes para Cerrar";
               Label36.Visible = true;
           }
           else {
               Label36.Visible = false;
               Label31.Text = conexion.ActualizarOrdenCerrar(DropDownList6.SelectedValue, Label29.Text);
               DropDownList6.ClearSelection();
               Label14.Text = "";
               Label16.Text = "";           
           }          
       }

       protected void Button5_Click(object sender, EventArgs e)
       {
           Label14.Text = "";
           Label16.Text = "";
           //DropDownList6.SelectedIndex = 0;
           MultiView1.ActiveViewIndex = -1;
       }

      protected void Button6_Click(object sender, EventArgs e)
       {
           if (DropDownList7.SelectedValue.Equals(""))
           {
               Label37.Text = "No hay Ordenes Para aprobar";
               Label37.Visible = true;
           }
           else
           {
               Label37.Visible = false;
               conexion.ActualizarOrdenAprobar(Label30.Text, usuario.Nit, DropDownList7.SelectedValue);
               Button7.Visible = true;
               Label34.Text = DropDownList7.SelectedValue;
               Label33.Visible = true;
               Label34.Visible = true;
           }
           //DropDownList7.ClearSelection();
       }

      protected void Button7_Click(object sender, EventArgs e)
      {
          Reportes reportes = new Reportes();
          reportes.CrearReporteAprobar(Label34.Text,usuario);
          Response.ContentType = "application/pdf";
          Response.AddHeader("content-disposition", "attachment; filename= Orden"+Label34.Text+".pdf");
          System.Web.HttpContext.Current.Response.Write(reportes.retornarDocumento());
          Response.Flush();
          Response.End();
         
      }

      protected void Button8_Click(object sender, EventArgs e)
      {
          MultiView1.ActiveViewIndex = -1;
      }

      protected void Button9_Click(object sender, EventArgs e)
      {
          if (DropDownList14.SelectedValue.Equals(""))
          {
              Label38.Text = "No hay ordens para Anular";
              Label38.Visible = true;
          }
          else {
              Label38.Visible = false;
              Label32.Text= conexion.ActualizarOrdenAnulada(DropDownList14.SelectedValue);
          }
         
      }

     protected void DropDownList14_SelectedIndexChanged(object sender, EventArgs e)
      {
          Button9.Enabled = true;
      }

      protected void Button2_Click(object sender, EventArgs e)
      {
          if (DropDownList6.SelectedValue.Equals(""))
          {
              Label36.Text = "No Hay Ordenes para cerrar";
              Label36.Visible = true;
          }
          else {
              Label36.Visible = false;
              string TOTAL = conexion.ObtenerTotalOrden(DropDownList6.SelectedValue);
              string Limite = conexion.ObtenerLimiteCredito(DropDownList6.SelectedValue);
              Label14.Text = TOTAL;
              //decimal tot = Convert.ToDecimal(TOTAL);
              //decimal limiteclien=decimal.Parse(Limite,System.Globalization.CultureInfo.InvariantCulture);
              Label16.Text = Limite;
              Button4.Enabled = true;        
          }         
      }

      protected void Button3_Click(object sender, EventArgs e)
      {
          if (DropDownList7.SelectedValue.Equals(""))
          {
              Label37.Text = "No hay Ordenes para Aprobar";
              Label37.Visible = true;
          }
          else {
              Label37.Visible = false;
              Button6.Enabled = true;
          }
      }

      protected void Button11_Click(object sender, EventArgs e)
      {
          if (TextBox2.Text.Equals(""))
          {
              Label35.Text = "Ingrese un Numero de Orden";
              Label35.Visible = true;
          }
          else {
              if (conexion.numeroOrdenExiste(TextBox2.Text))
              {
                  //Response.Write("<script language='javascript'> { window.alert('No Orden Ya existe') } </script> ");
                  Label35.Text = "Numero de Orden Ya existe";
                  Label35.Visible = true;
                  DropDownList2.Items.Clear();
              }
              else
              {
                  DropDownList3.Enabled = true;
                  string fechaIni = conexion.getFechaInicioLista(DropDownList3.SelectedValue);
                  string fechaFin = conexion.getFechaFinLista(DropDownList3.SelectedValue);

                  DateTime time1 = Convert.ToDateTime(fechaIni);
                  DateTime time2 = Convert.ToDateTime(Label27.Text);
                  DateTime time3 = Convert.ToDateTime(fechaFin);
                  int result1 = DateTime.Compare(time1, time2);//comparacion de fecha de inilista con fecha actual
                  int result2 = DateTime.Compare(time2, time3);//comparacion de fecha finlista con fecha actula

                  if ((result1 < 0) && (result2 < 0))
                  {
                      //fecha inicio es menor que la fecha actual, lleno de nombre de productos si se cumple con la fecha
                      DropDownList2.Items.Clear();
                      foreach (string jk in conexion.NombreProductosLista(DropDownList3.SelectedValue))
                      {
                          DropDownList2.Items.Add(jk);
                      }
                      //activar funciones
                      DropDownList2.Enabled = true;
                      TextBox1.Enabled = true;
                      Button1.Enabled = true;
                      Label35.Visible = false;
                  }
                  else if (result1 == 0)
                  {//las fechas son iguales al inicio
                      DropDownList2.Items.Clear();
                      foreach (string jk in conexion.NombreProductosLista(DropDownList3.SelectedValue))
                      {
                          DropDownList2.Items.Add(jk);
                      }
                      DropDownList2.Enabled = true;
                      TextBox1.Enabled = true;
                      Button1.Enabled = true;
                      Label35.Visible = false;
                  }
                  else if (result2 == 0)
                  {
                      //es igual al fin
                      DropDownList2.Items.Clear();
                      foreach (string jk in conexion.NombreProductosLista(DropDownList3.SelectedValue))
                      {
                          DropDownList2.Items.Add(jk);
                      }
                      DropDownList2.Enabled = true;
                      TextBox1.Enabled = true;
                      Button1.Enabled = true;
                      Label35.Visible = false;
                  }
                  else if ((result1 > 0) && (result2 > 0))
                  {
                      //fecha de inicio no ha empezado
                      //Response.Write("<script language='javascript'> { window.alert('Error No existe Lista de Preecios') } </script> ");
                      Label35.Text = "Error!! La Lista de Precios No Existe";
                      Label35.Visible = true;
                      DropDownList2.Items.Clear();
                  }
                  else
                  {
                      //Response.Write("<script language='javascript'> { window.alert('Lista de Precios ya expiro') } </script> ");
                      Label35.Text = "La Lista de Precios ya Expiro";
                      Label35.Visible = true;
                      DropDownList2.Items.Clear();
                  }
              }
          }                   
      }

      protected void LinkButton6_Click(object sender, EventArgs e)
      {
          Response.Redirect("/Paginas/Pagos.aspx");
      }

      protected void Button12_Click(object sender, EventArgs e)
      {//PARA CREAR UNA LISTA NUEVA
          MultiView1.ActiveViewIndex = 4;
          Label43.Text = DropDownList1.SelectedValue;
          DateTime dia = DateTime.Today;
          Label44.Text = dia.ToString("dd/MM/yyyy");
          Label45.Text = dia.ToString("dd/MM/yyyy");
      }

      protected void LinkButton7_Click(object sender, EventArgs e)
      {
          if (usuario.Puesto.Equals("Gerente"))
          {
              Response.Redirect("/Paginas/Gerente.aspx");
          }
          else if (usuario.Puesto.Equals("Supervisor"))
          {
              Response.Redirect("/Paginas/Supervisor.aspx");
          }
          else if (usuario.Puesto.Equals("Vendedor"))
          {
              Response.Redirect("/Paginas/Vendedor.aspx");
          }
      }

      protected void Button13_Click(object sender, EventArgs e)
      {
          string tabla = "LISTACLIENTE";
          string datos = "'"+Label44.Text+"','"+Label45.Text+"','"+DropDownList15.SelectedValue+"','"+Label43.Text+"'";
          conexion.Insertar(tabla, datos);
          MultiView1.ActiveViewIndex = 0;
      }            
    }
}