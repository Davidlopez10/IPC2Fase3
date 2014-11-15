using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using IPC2_Fase3_201314694.Clasees;

namespace IPC2_Fase3_201314694
{
    public class ConexionSQL
    {
        
       // string cadenaconexion = @"Data Source=HOME\SQLEXPRESS2012;Initial Catalog=PROJECTO_FASE3;Integrated Security=True";
      //string cadenaconexion = @"Data Source=HOME\SQLEXPRESS2012;Initial Catalog=PROJECTO_FASE2;Integrated Security=True"; 
        string cadenaconexion = @"Data Source=HOME\SQLEXPRESS2012;Initial Catalog=FASE3;Integrated Security=True";
        /**Administrador*/
        public string IdMeta(string fecha, string cdempleado) {
            string consulta = "select IDMETA from METAS where FECHA='"+fecha+"' and CODIGOEMPELADO='"+cdempleado+"';";
            string codigo = "";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();
               
                try
                { SqlDataReader read = comando.ExecuteReader();
                    while (read.Read())
                    {
                        codigo = read.GetValue(0).ToString();
                    }
                    read.Close();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                   
                    conexion.Close();
                }
            }
            return codigo;    
        
        }

        public string ActualizarEmpleado(string jefe, string nit)
        {
            string mensaje = "";
            using (SqlConnection cone = new SqlConnection(cadenaconexion))
            {
                cone.Open();
                string actualizar = "UPDATE EMPLEADO SET EMPLEADO.JEFEEMPLEADO ='" + jefe + "'  WHERE NITEMPLEADO='" + nit + "'";
                SqlCommand cmd = new SqlCommand(actualizar, cone);
                cmd.ExecuteScalar();
                cone.Close();
                mensaje = "Se actualizo con exito";
            }
            return mensaje;
        }
      /**************************************Para Crear Orden***********************/
        public string Insertar(string Tabla,string valores) {
            string Consulta = "INSERT INTO "+Tabla+" VALUES ("+valores+");";
            string error = "";
            using (SqlConnection coneccion = new SqlConnection(cadenaconexion)){
            SqlCommand comando = new SqlCommand(Consulta,coneccion);
            try
            {   coneccion.Open();
                comando.ExecuteNonQuery();
                error = "Se realizo con exito la conexion";
            }
            catch (Exception e)
            {
                error = "Ese es un error de la base de datos"+e;
            }
            finally {
                coneccion.Close();               
            }
            return error; 
            }
        }

        public string Login(string usuario,string contrasenia) {
            string error = "";
            string consult = "SELECT EMPLEADO.NITEMPLEADO,EMPLEADO.CONTRASENIA,PUESTO.NOMBREPUESTO,EMPLEADO.NOMBRES FROM EMPLEADO,PUESTO WHERE EMPLEADO.CODIGOPUESTO=PUESTO.CODPUESTO";

            using (SqlConnection conetions = new SqlConnection(cadenaconexion)) {
                conetions.Open();
                SqlCommand cmd = new SqlCommand(consult, conetions);
                SqlDataReader dr=cmd.ExecuteReader();
                    
               try {
                   while (dr.Read()) {
                      // System.Console.WriteLine("Estoy en la conexion  "+dr.GetValue(0).ToString());                  
                       if (usuario.Equals(dr.GetValue(0).ToString()) && contrasenia.Equals(dr.GetValue(1).ToString()))
                       {
                           error = usuario;
                           break;
                       }
                       else {
                           error = "";
                       }
                       //error =error+dr.GetValue(0).ToString() + " " + dr.GetValue(1).ToString() + " " + dr.GetValue(2).ToString()+"</br>";
                      }

                }
                catch (Exception e)
                {
                    error = "No entro a ser la conexion";
                }
                finally
                {
                    dr.Close();
                    conetions.Close();
                }
            
            }
            return error;
        }

        public UsuarioEmpleado Login2(string User) {
            UsuarioEmpleado usuario = new UsuarioEmpleado();
            string consult = "SELECT EMPLEADO.NITEMPLEADO,PUESTO.NOMBREPUESTO,EMPLEADO.NOMBRES,EMPLEADO.JEFEEMPLEADO FROM EMPLEADO,PUESTO WHERE EMPLEADO.CODIGOPUESTO=PUESTO.CODPUESTO";

            using (SqlConnection conetions = new SqlConnection(cadenaconexion))
            {
                conetions.Open();
                SqlCommand cmd = new SqlCommand(consult, conetions);
                SqlDataReader dr = cmd.ExecuteReader();

                try
                {
                    while (dr.Read())
                    {
                        // System.Console.WriteLine("Estoy en la conexion  "+dr.GetValue(0).ToString());                  
                        if (User.Equals(dr.GetValue(0).ToString()))
                        {
                            usuario.Nit = dr.GetValue(0).ToString();
                            usuario.Puesto = dr.GetValue(1).ToString();
                            usuario.Nombre = dr.GetValue(2).ToString();
                            usuario.Jefe = dr.GetValue(3).ToString();                            
                            break;
                        }
                        else
                        {
                           //error
                        }
                        //error =error+dr.GetValue(0).ToString() + " " + dr.GetValue(1).ToString() + " " + dr.GetValue(2).ToString()+"</br>";
                    }

                }
                catch (Exception e)
                {
                    // = "No entro a ser la conexion";
                }
                finally
                {
                    dr.Close();
                    conetions.Close();
                }
            }
            
            return usuario;
        }
               
        public DatosCliente ConsultaDatosCliente(String nit) {
            DatosCliente datos= new DatosCliente();
            using (SqlConnection con = new SqlConnection(cadenaconexion)) {
                con.Open();
                string consulta = "SELECT NITCLIENTE,NOMBRECLIENTE,APELLIDOSCLIENTE,CANTIDADORDENES,LIMITECREDITO,DIASCREDITO,CLIENTE.DIRECCIONDOMICILIO FROM CLIENTE WHERE NITCLIENTE='" + nit + "';";
                SqlCommand com = new SqlCommand(consulta,con);
                SqlDataReader leer= com.ExecuteReader();
                try
                {
                   while (leer.Read())
                    {
                        if (nit.Equals(String.Format("{0}",leer[0])))
                        {
                            datos.Nit = leer.GetValue(0).ToString();
                            datos.Nombre = leer.GetValue(1).ToString();
                            datos.Apellido = leer.GetValue(2).ToString();
                            datos.CantidadOrden=leer.GetValue(3).ToString();
                            datos.LimiteCredito1=leer.GetValue(4).ToString();
                            datos.DiasCredito1=leer.GetValue(5).ToString();
                            datos.Direccion1 = leer.GetValue(6).ToString();
                            datos.CantidadOrdenVenciadas1 = string.Format("{0}", ConsultasOrdneVencidas(nit));
                            break;
                        }
                        else
                        {                          
                            //error
                        }
                   }
                }
                catch (Exception ex)
                {
                   //error
                }
                finally
                {
                    leer.Close();
                    con.Close();
                }            
            }
            return datos;
        }

        public Productos ConsultaDatosProductos(string nombre,string fecha) {
            Productos producto = new Productos();
            string codigolista = CodigoListaFecha(fecha);
            string consulta = "SELECT VALOR,NOMBREPRODUCTO,FKCODIGOLIST,PRODUCTO.CODIGOPRODUCTO FROM DETALLELISTAPRECIOS INNER JOIN PRODUCTO ON DETALLELISTAPRECIOS.FKCODIGOPRODUC=PRODUCTO.CODIGOPRODUCTO WHERE NOMBREPRODUCTO='" + nombre + "' AND DETALLELISTAPRECIOS.FKCODIGOLIST='" + codigolista + "'";

            using (SqlConnection conexion = new SqlConnection(cadenaconexion)) {
                SqlCommand comando = new SqlCommand(consulta,conexion);
                conexion.Open();
                SqlDataReader read = comando.ExecuteReader();
                try {
                    while (read.Read())
                    {
                        producto.Precio = read.GetValue(0).ToString();
                        producto.Nombre = read.GetValue(1).ToString();
                        producto.CodigoLista = read.GetValue(2).ToString();
                        producto.CodigoProducto = read.GetValue(3).ToString();
                    }

                }catch(Exception ex){

                }finally{
                    read.Close();
                    conexion.Close();
                }            
            }
            return producto;
        }

        private string CodigoListaFecha(string fecha) {
            string consulta = "SELECT CODIGO,LISTAPRECIOS.FECHAINICIO FROM LISTAPRECIOS WHERE FECHAFIN>'" + fecha + "' AND FECHAINICIO<'" + fecha + "'";
            string codigo="";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {                
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();
                SqlDataReader read = comando.ExecuteReader();
                try
                {
                    while (read.Read())
                    {
                        codigo = read.GetValue(0).ToString();
                    }

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    read.Close();
                    conexion.Close();
                }
            }
            return codigo;      
        }
    
       private int ConsultasOrdneVencidas(string nitcliente) {
           int cantidad = 0;
           string consulta = "SELECT *FROM ORDEN WHERE (SALDOFALTA>0) AND (NITCLIENTE='"+nitcliente+"')";
           using (SqlConnection conexion = new SqlConnection(cadenaconexion))
           {
               SqlCommand comando = new SqlCommand(consulta, conexion);
               conexion.Open();              
               try
               { SqlDataReader read = comando.ExecuteReader();
                   while (read.Read())
                   {
                       cantidad++;
                   }
                    read.Close();
               }
               catch (Exception ex)
               {
               }
               finally
               {                  
                   conexion.Close();
               }
           }
           return cantidad;
       }

       public string getFechaInicioLista(string nombre) {
           string fecha = "";
            string consulta = "SELECT LISTAPRECIOS.FECHAINICIO FROM LISTAPRECIOS  WHERE LISTAPRECIOS.NOMBRE='"+nombre+"';";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(consulta, conexion);

                try
                {
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        fecha= read.GetValue(0).ToString();
                    }
                    read.Close();
                }
                catch (Exception ex)
                {
                    fecha = "";
                }
                finally
                {
                    conexion.Close();
                }
            }
           return fecha;
       }

       public string getFechaFinLista(string nombre) {
           string fecha = "";
           string consulta = "SELECT LISTAPRECIOS.FECHAFIN FROM LISTAPRECIOS WHERE LISTAPRECIOS.NOMBRE='" + nombre + "';";
           using (SqlConnection conexion = new SqlConnection(cadenaconexion))
           {
               conexion.Open();
               SqlCommand cmd = new SqlCommand(consulta, conexion);

               try
               {
                   SqlDataReader read = cmd.ExecuteReader();
                   while (read.Read())
                   {
                       fecha = read.GetValue(0).ToString();
                   }
                   read.Close();
               }
               catch (Exception ex)
               {
                   fecha = "";
               }
               finally
               {
                   conexion.Close();
               }
           }
           return fecha;
       
       
       
       }

       public LinkedList<string> NombreListaPreciosCliente(string nitcliente){
           LinkedList<string> Listas = new LinkedList<string> { };
           string consulta="SELECT LISTAPRECIOS.NOMBRE FROM LISTACLIENTE INNER JOIN LISTAPRECIOS ON (LISTACLIENTE.CODIGOLISTAPRECIOS=LISTAPRECIOS.CODIGO) WHERE LISTACLIENTE.NITCLIENTE='"+nitcliente+"';";
          // string consulta = "SELECT LISTAPRECIOS.FECHAINICIO FROM LISTAPRECIOS  WHERE LISTAPRECIOS.NOMBRE='" + nombre + "';";
           using (SqlConnection conexion = new SqlConnection(cadenaconexion))
           {
               conexion.Open();
               SqlCommand cmd = new SqlCommand(consulta, conexion);

               try
               {
                   SqlDataReader read = cmd.ExecuteReader();
                   while (read.Read())
                   {
                       Listas.AddLast(read.GetValue(0).ToString());
                   }
                   read.Close();
               }
               catch (Exception ex)
               {
                   
               }
               finally
               {
                   conexion.Close();
               }
           }
           return Listas;     
       }

       public LinkedList<string> NombreProductosLista(string NombreLista) {
           LinkedList<string> Productos = new LinkedList<string> { };
           string consulta = "SELECT PRODUCTO.NOMBREPRODUCTO FROM PRODUCTO INNER JOIN DETALLELISTAPRECIOS ON PRODUCTO.CODIGOPRODUCTO=DETALLELISTAPRECIOS.FKCODIGOPRODUC INNER JOIN LISTAPRECIOS ON DETALLELISTAPRECIOS.FKCODIGOLIST=LISTAPRECIOS.CODIGO WHERE LISTAPRECIOS.NOMBRE='"+NombreLista+"';";
           using (SqlConnection conexion = new SqlConnection(cadenaconexion))
           {
               conexion.Open();
               SqlCommand cmd = new SqlCommand(consulta, conexion);
               try
               {
                   SqlDataReader read = cmd.ExecuteReader();
                   while (read.Read())
                   {
                       Productos.AddFirst(read.GetValue(0).ToString());
                   }
                   read.Close();
               }
               catch (Exception ex)
               {

               }
               finally
               {
                   conexion.Close();
               }
           }
           return Productos;
       }

       public Decimal TotalDetalleOrden(string NoOrden,string NitCliente)
       {
           decimal total = 0;
           using (SqlConnection cone = new SqlConnection(cadenaconexion))
           {
               cone.Open();
               string cosulta = "SELECT VALOR,CANTIDAD FROM DETALLEPRODUCTOORDEN INNER JOIN ORDEN ON DETALLEPRODUCTOORDEN.CODIGOORDEN=ORDEN.CODIGOORDEN WHERE ORDEN.CODIGOORDEN='"+NoOrden+"' AND ORDEN.NITCLIENTE='"+NitCliente+"';";
               try
               {
                   SqlCommand comd = new SqlCommand(cosulta, cone);
                   SqlDataReader read = comd.ExecuteReader();
                   while (read.Read())
                   {
                       total = total + ((Convert.ToDecimal(read.GetValue(0).ToString())) * Convert.ToDecimal(read.GetValue(1).ToString()));
                  //     Console.WriteLine(total);
                       
                   }
                  // Console.ReadKey();
                   read.Close();
                   //total = total / 100;     
               }
               catch (Exception ex)
               {
                   total = -1;
               }
               finally
               {
                   cone.Close();
               }
           }
           return total;
       }

       public string ActualizarOrden(string NoOrden,string total) {
           string mensaje = "";
           using (SqlConnection conexion = new SqlConnection(cadenaconexion))
           {
               conexion.Open();
               string consulta = "UPDATE ORDEN SET ORDEN.TOTALPAGAR =" + total.Replace(",",".") + ",ORDEN.SALDOFALTA=" +total.Replace(",",".") + " WHERE ORDEN.CODIGOORDEN='" + NoOrden + "';";
               SqlCommand com = new SqlCommand(consulta, conexion);
               try {  
                   com.ExecuteScalar();
                   conexion.Close();
                   mensaje = "Save Exitoso";
               }catch(Exception e){
                   mensaje = "Error";
               }              
           }
           return mensaje;
       }

       public void BorrarFilaOrden(string cantidad, string codigoProducto,string codigoOrden) {
           string mensajeOrden;
           using (SqlConnection conexion = new SqlConnection(cadenaconexion))
           {
               conexion.Open();
               string consulta = "DELETE DETALLEPRODUCTOORDEN WHERE CODIGOPRODUCTO='"+codigoProducto+"' AND CODIGOORDEN='"+codigoOrden+"' AND CANTIDAD="+cantidad+";";
               SqlCommand com = new SqlCommand(consulta, conexion);
               try
               {
                   com.ExecuteScalar();
                   conexion.Close();
                   mensajeOrden = "Delete Exitoso";
               }
               catch (Exception e)
               {
                   mensajeOrden = "Error";
               }
           }     
       }

       public void BorrarOrden(string nOrden) {
           using (SqlConnection conexion = new SqlConnection(cadenaconexion))
           {
               conexion.Open();
               string consulta = "DELETE FROM ORDEN WHERE CODIGOORDEN='"+nOrden+"' AND TOTALPAGAR=0 AND ESTADOAPROBACION='Procesando';";
               SqlCommand com = new SqlCommand(consulta, conexion);
               try
               {
                   com.ExecuteScalar();
                   conexion.Close();
                  
               }
               catch (Exception e)
               {
                  
               }
           }           
       }

       public string ActualizarClienteCredito(string NitCliente,string cantidadOrden,string limiteCredito) {
           string mensajeCliente = "";
           using (SqlConnection conexion = new SqlConnection(cadenaconexion))
           {
               conexion.Open();
               string consulta = "UPDATE CLIENTE SET CLIENTE.LIMITECREDITO ="+limiteCredito.Replace(",",".")+",CLIENTE.CANTIDADORDENES=" + cantidadOrden + " WHERE NITCLIENTE='" + NitCliente + "'";
               SqlCommand com = new SqlCommand(consulta, conexion);
               try
               {
                   com.ExecuteScalar();
                   conexion.Close();
                   mensajeCliente = "Save Exitoso";
               }
               catch (Exception e)
               {
                   mensajeCliente = "Error";
               }
           }
           return mensajeCliente;          
       }

        //*****************es para Cerrar orden*********************************
       public LinkedList<string> OrdenCerrarEmpleado(string nitEmpleado) {
           LinkedList<string> Ordenes = new LinkedList<string> { };
             string consulta = "SELECT CODIGOORDEN FROM ORDEN WHERE NITEMPLEADOVENDE='"+nitEmpleado+"'  AND ESTADOAPROBACION='Procesando' AND SALDOFALTA>0 ;";
           using (SqlConnection conexion = new SqlConnection(cadenaconexion))
           {
               conexion.Open();
               SqlCommand cmd = new SqlCommand(consulta, conexion);
               try
               {
                   SqlDataReader read = cmd.ExecuteReader();
                   while (read.Read())
                   {
                       Ordenes.AddLast(read.GetValue(0).ToString());
                   }
                   read.Close();
               }
               catch (Exception ex)
               {

               }
               finally
               {
                   conexion.Close();
               }
           }



       return Ordenes;
       }

       public string ObtenerTotalOrden(string NoOrden) {
           string Total="";
           string consulta = "SELECT TOTALPAGAR FROM ORDEN WHERE ORDEN.CODIGOORDEN='"+NoOrden+"';";
           using (SqlConnection conexion = new SqlConnection(cadenaconexion))
           {
               conexion.Open();
               SqlCommand cmd = new SqlCommand(consulta, conexion);
               try
               {
                   SqlDataReader read = cmd.ExecuteReader();
                   while (read.Read())
                   {
                       Total = read.GetValue(0).ToString();
                   }
                   read.Close();
               }
               catch (Exception ex)
               {
                   Total = "eRROR";
               }
               finally
               {
                   conexion.Close();
               }
           }
           return Total;
       }

       public string ObtenerLimiteCredito(string Noorden) {
           string limite = "";
           string consulta = "SELECT CLIENTE.LIMITECREDITO FROM CLIENTE INNER JOIN ORDEN ON ORDEN.NITCLIENTE=CLIENTE.NITCLIENTE WHERE ORDEN.CODIGOORDEN='"+Noorden+"';";
           using (SqlConnection conexion = new SqlConnection(cadenaconexion))
           {
               conexion.Open();
               SqlCommand cmd = new SqlCommand(consulta, conexion);
               try
               {
                   SqlDataReader read = cmd.ExecuteReader();
                   while (read.Read())
                   {
                       limite = read.GetValue(0).ToString();
                   }
                   read.Close();
               }
               catch (Exception ex)
               {
                   limite = "eRROR";
               }
               finally
               {
                   conexion.Close();
               }
           }
           return limite;
       }

       public string ActualizarOrdenCerrar(string noOrden,string fecha) {
           string mensaje = "";
           using (SqlConnection conexion = new SqlConnection(cadenaconexion))
           {
               conexion.Open();
               string consulta = "UPDATE ORDEN SET FECHACERRADA='" + fecha + "',ESTADOAPROBACION='Cerrada',PAGOABONO='Pendiente de Pago' WHERE CODIGOORDEN='" + noOrden + "';";
               SqlCommand com = new SqlCommand(consulta, conexion);
               try
               {
                   com.ExecuteScalar();
                   conexion.Close();
                   mensaje = "Save Exitoso";
               }
               catch (Exception e)
               {
                   mensaje = "Error";
               }
           }
           return mensaje;
       }

       public LinkedList<string> ListaOrdenes(string nitjefe)
       {
           LinkedList<string> empleados = new LinkedList<string> { };
           string consulta = "SELECT CODIGOORDEN FROM ORDEN INNER JOIN EMPLEADO ON NITEMPLEADOVENDE=EMPLEADO.NITEMPLEADO WHERE JEFEEMPLEADO='" + nitjefe + "' AND ORDEN.ESTADOAPROBACION='Procesando' AND ORDEN.SALDOFALTA>0;";
           using (SqlConnection conexion = new SqlConnection(cadenaconexion))
           {
               SqlCommand comando = new SqlCommand(consulta, conexion);
               conexion.Open();

               try
               {
                   SqlDataReader read = comando.ExecuteReader();
                   while (read.Read())
                   {
                       empleados.AddFirst(read.GetValue(0).ToString());
                   }
                   read.Close();
               }
               catch (Exception ex)
               {

               }
               finally
               {
                   conexion.Close();
               }
           }
           return empleados;
       }

        public LinkedList<string> OrdenesGerente(){
            LinkedList<string> gerente = new LinkedList<string> { };
            string consulta = "SELECT CODIGOORDEN FROM ORDEN WHERE ESTADOAPROBACION='Procesando' AND SALDOFALTA>0";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();

                try
                {
                    SqlDataReader read = comando.ExecuteReader();
                    while (read.Read())
                    {
                        gerente.AddFirst(read.GetValue(0).ToString());
                    }
                    read.Close();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    conexion.Close();
                }
            }
            return gerente;        
        }
        /************************para Aprobar Orden******************************/
        public string ActualizarOrdenAprobar(string fecha,string empleado,string NoOrden) {
            string mensaje = "";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                conexion.Open();
                string consulta = "UPDATE ORDEN SET ESTADOAPROBACION='Aprobado',FECHAAPROBACION='"+fecha+"',DIASFALTANTEAPROBACION=1 ,EMPLEADOAPROBACION='"+empleado+"' WHERE CODIGOORDEN='"+NoOrden+"';";
                SqlCommand com = new SqlCommand(consulta, conexion);
                try
                {
                    com.ExecuteScalar();
                    conexion.Close();
                    mensaje = "Actualizacion Exitosa";
                }
                catch (Exception e)
                {
                    mensaje = "Error";
                }
            }
            return mensaje;      
        }

        public LinkedList<string> OrdenAprobarSupervisor(string empleado) {
            LinkedList<string> orden = new LinkedList<string> { };
            string consulta = "SELECT CODIGOORDEN FROM ORDEN INNER JOIN EMPLEADO ON NITEMPLEADOVENDE=EMPLEADO.NITEMPLEADO WHERE (EMPLEADO.JEFEEMPLEADO='"+empleado+"'OR ORDEN.NITEMPLEADOVENDE='"+empleado+"') AND ORDEN.ESTADOAPROBACION='Cerrada' AND ORDEN.SALDOFALTA>0 ;";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                try
                {
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        orden.AddLast(read.GetValue(0).ToString());
                    }
                    read.Close();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    conexion.Close();
                }
            }

            return orden;
        }

        public LinkedList<string> OrdenAprobarGerente() {
            LinkedList<string> gerente = new LinkedList<string> { };
            string consulta = "SELECT CODIGOORDEN FROM ORDEN WHERE ESTADOAPROBACION='Cerrada' AND SALDOFALTA>0";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();

                try
                {
                    SqlDataReader read = comando.ExecuteReader();
                    while (read.Read())
                    {
                        gerente.AddFirst(read.GetValue(0).ToString());
                    }
                    read.Close();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    conexion.Close();
                }
            }
            return gerente;       
        }

        /***************************Anular Orden**********************************/
        public LinkedList<string> AnularOrdenGerente() {
            LinkedList<string> Anulgerent = new LinkedList<string> { };
            string consulta = "SELECT CODIGOORDEN FROM ORDEN WHERE ORDEN.PAGOABONO='Pendiente de Pago';";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();
                try
                {
                    SqlDataReader read = comando.ExecuteReader();
                    while (read.Read())
                    {
                        Anulgerent.AddFirst(read.GetValue(0).ToString());
                    }
                    read.Close();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    conexion.Close();
                }
            }
            return Anulgerent;       
        }

        public LinkedList<string> AnularOrdenEmpleado(string nitVendedor) {
            LinkedList<string> Anulvende = new LinkedList<string> { };
            string consulta = "SELECT CODIGOORDEN FROM ORDEN WHERE ORDEN.PAGOABONO='Pendiente de Pago' and ORDEN.NITEMPLEADOVENDE='"+nitVendedor+"' ;";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();
                try
                {
                    SqlDataReader read = comando.ExecuteReader();
                    while (read.Read())
                    {
                        Anulvende.AddFirst(read.GetValue(0).ToString());
                    }
                    read.Close();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    conexion.Close();
                }
            }
            return Anulvende;      
        
        }

        public LinkedList<string> AnularOrdenSupervisor(string nitJefe) {
            LinkedList<string> Anulvende = new LinkedList<string> { };
            string consulta = "SELECT CODIGOORDEN FROM ORDEN INNER JOIN EMPLEADO ON ORDEN.NITEMPLEADOVENDE=EMPLEADO.NITEMPLEADO WHERE (EMPLEADO.JEFEEMPLEADO='"+nitJefe+"' OR ORDEN.NITEMPLEADOVENDE='"+nitJefe+"') AND ORDEN.PAGOABONO='Pendiente de Pago';";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();
                try
                {
                    SqlDataReader read = comando.ExecuteReader();
                    while (read.Read())
                    {
                        Anulvende.AddFirst(read.GetValue(0).ToString());
                    }
                    read.Close();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    conexion.Close();
                }
            }
            return Anulvende;
        
        
        }

        public string ActualizarOrdenAnulada(string NoOrden) {
            string mensaje = "";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                conexion.Open();
                string consulta = "UPDATE ORDEN SET ESTADOAPROBACION='Anulada',ORDEN.PAGOABONO='Anulada' WHERE CODIGOORDEN='" + NoOrden + "';";
                SqlCommand com = new SqlCommand(consulta, conexion);
                try
                {
                    com.ExecuteScalar();
                    conexion.Close();
                    mensaje = "Se Anulo con Exito";
                }
                catch (Exception e)
                {
                    mensaje = "Error, No se pudo Anular";
                }
            }
            return mensaje;      
        
        
        }

        /******************************Pagos*****************************************/
        public LinkedList<string> OrdenesPagos() {
            LinkedList<string> orden = new LinkedList<string> { };
            string consulta = "SELECT CODIGOORDEN FROM ORDEN WHERE ESTADOAPROBACION='Aprobado' AND PAGOABONO in('Pendiente de Pago','Pagando') AND SALDOFALTA>0; ";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();
                try
                {
                    SqlDataReader read = comando.ExecuteReader();
                    while (read.Read())
                    {
                        orden.AddFirst(read.GetValue(0).ToString());
                    }
                    read.Close();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    conexion.Close();
                }
            }
            return orden;       
        }

        public LinkedList<string> OrdenesPagoEmpleado(string NIT) {
            LinkedList<string> orden = new LinkedList<string> ();
            string consulta = "SELECT CODIGOORDEN FROM ORDEN WHERE ESTADOAPROBACION='Aprobado' AND PAGOABONO in('Pendiente de Pago','Pagando') AND SALDOFALTA>0 AND ORDEN.NITEMPLEADOVENDE='"+NIT+"';";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();
                try
                {
                    SqlDataReader read = comando.ExecuteReader();
                    while (read.Read())
                    {
                        orden.AddFirst(read.GetValue(0).ToString());
                    }
                    read.Close();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    conexion.Close();
                }
            }
            return orden;       
        }

        public LinkedList<string> OrdenesPagoSupervisor(string NITSUP) {
            LinkedList<string> orden = new LinkedList<string>();
            string consulta = "SELECT CODIGOORDEN FROM ORDEN INNER JOIN EMPLEADO ON ORDEN.NITEMPLEADOVENDE=EMPLEADO.NITEMPLEADO WHERE (ESTADOAPROBACION='Aprobado') AND PAGOABONO in('Pendiente de Pago','Pagando') AND (SALDOFALTA>0) AND (EMPLEADO.JEFEEMPLEADO='"+NITSUP+"' OR ORDEN.NITEMPLEADOVENDE='"+NITSUP+"');";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();
                try
                {
                    SqlDataReader read = comando.ExecuteReader();
                    while (read.Read())
                    {
                        orden.AddFirst(read.GetValue(0).ToString());
                    }
                    read.Close();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    conexion.Close();
                }
            }
            return orden;        
        }

        public string ActualizarSaldoOrden(string orden, string saldo) {
            string mensaje = "";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                conexion.Open();
                string consulta = "UPDATE ORDEN SET ORDEN.SALDOFALTA=" + saldo.Replace(",", ".") + ",ORDEN.PAGOABONO='Pagando' WHERE ORDEN.CODIGOORDEN='" + orden + "';";
                SqlCommand com = new SqlCommand(consulta, conexion);
                try
                {
                    com.ExecuteScalar();
                    conexion.Close();
                    mensaje = "Save Exitoso";
                }
                catch (Exception e)
                {
                    mensaje = "Error";
                }
            }
            return mensaje;
        }

        public string ObtenerSaldoOrden(string NoOrden) {
            string saldo = "";
            string consulta = "SELECT SALDOFALTA FROM ORDEN WHERE ORDEN.CODIGOORDEN='" + NoOrden + "';";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                try
                {
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        saldo = read.GetValue(0).ToString();
                    }
                    read.Close();
                }
                catch (Exception ex)
                {
                    saldo = "eRROR";
                }
                finally
                {
                    conexion.Close();
                }
            }
            return saldo;       
        }

        public string ActualizarLimiteCreditoCliente(string NitCliente,string limite) {
            string mensajeCliente = "";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                conexion.Open();
                string consulta = "UPDATE CLIENTE SET CLIENTE.LIMITECREDITO = " + limite.Replace(",",".") + " WHERE NITCLIENTE='" +NitCliente + "'";
                SqlCommand com = new SqlCommand(consulta, conexion);
                try
                {
                    com.ExecuteScalar();
                    conexion.Close();
                    mensajeCliente = "Save Exitoso";
                }
                catch (Exception e)
                {
                    mensajeCliente = "Error";
                }
            }
            return mensajeCliente;    
       
        
        
        
        }

        public Moneda ObtenerMoneda(string nombre) {
            Moneda money = new Moneda();
            string consulta = "SELECT MONEDA.IDMONEDA, MONEDA.NOMBRE,MONEDA.SIMBOLO,MONEDA.VALOR FROM MONEDA WHERE MONEDA.NOMBRE='"+nombre+"';";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                try
                {
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        money.IdMoneda = read.GetValue(0).ToString();
                        money.NameMoneda1 = read.GetValue(1).ToString();
                        money.Simbolo = read.GetValue(2).ToString();
                        money.Valor = read.GetValue(3).ToString();
                    }
                    read.Close();
                }
                catch (Exception ex)
                {
                    //saldo = "eRROR";
                }
                finally
                {
                    conexion.Close();
                }
            }
            return money;              
        }

        /*REPORTES DE PAGOS*/
        public string ObtenerValorPago(string NoOrden) {
            string saldo = "";
            string consulta = "select VALORPAGO from pago where pago.NOORDEN='"+NoOrden+"';";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                try
                {
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        saldo = read.GetValue(0).ToString();
                    }
                    read.Close();
                }
                catch (Exception ex)
                {
                    saldo = "eRROR";
                }
                finally
                {
                    conexion.Close();
                }
            }
            return saldo;           
        }

        public LinkedList<string> ObtenerDetallePagoCHEQUE(string NoOrden) {
            LinkedList<string> cheque = new LinkedList<string>();
            string consulta = "SELECT CODIGOCHEQUE FROM CHEQUE INNER JOIN PAGO ON CHEQUE.CODIGOPAGO=PAGO.CODIGOPAGO WHERE PAGO.NOORDEN='"+NoOrden+"';";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                try
                {
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                       cheque.AddFirst(read.GetValue(0).ToString());
                    }
                    read.Close();
                }
                catch (Exception ex)
                {
                   // saldo = "eRROR";
                }
                finally
                {
                    conexion.Close();
                }
            }
            return cheque;           
        }

        public LinkedList<string> ObtenerDetallePagoTARJETA(string NoOrden)
        {
            LinkedList<string> cheque = new LinkedList<string>();
            string consulta = "SELECT NUMEROAUTORIZACION FROM TARJETA INNER JOIN PAGO ON TARJETA.CDPAGO=PAGO.CODIGOPAGO WHERE PAGO.NOORDEN='" + NoOrden + "';";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                try
                {
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        cheque.AddFirst(read.GetValue(0).ToString());
                    }
                    read.Close();
                }
                catch (Exception ex)
                {
                    // saldo = "eRROR";
                }
                finally
                {
                    conexion.Close();
                }
            }
            return cheque;
        }

        public LinkedList<string> ObtenerDetallePagoEFECTIVO(string NoOrden)
        {
            LinkedList<string> cheque = new LinkedList<string>();
            string consulta = "SELECT IDEFECTIVO FROM EFECTIVO INNER JOIN PAGO ON EFECTIVO.CDPAGO=PAGO.CODIGOPAGO WHERE PAGO.NOORDEN='" + NoOrden + "';";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                try
                {
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        cheque.AddFirst(read.GetValue(0).ToString());
                    }
                    read.Close();
                }
                catch (Exception ex)
                {
                    // saldo = "eRROR";
                }
                finally
                {
                    conexion.Close();
                }
            }
            return cheque;
        }

        public UsuarioEmpleado obtenerDatosGeneralesEmpleado(string NoOrden) {
            UsuarioEmpleado usuario = new UsuarioEmpleado();
            string consulta = "SELECT ORDEN.NITEMPLEADOVENDE,EMPLEADO.NOMBRES,PUESTO.NOMBREPUESTO FROM ORDEN INNER JOIN EMPLEADO ON ORDEN.NITEMPLEADOVENDE=EMPLEADO.NITEMPLEADO INNER JOIN PUESTO ON EMPLEADO.CODIGOPUESTO=PUESTO.CODPUESTO WHERE ORDEN.CODIGOORDEN='" + NoOrden + "';";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                try
                {
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        usuario.Nit = read.GetValue(0).ToString();
                        usuario.Nombre = read.GetValue(1).ToString();
                        usuario.Puesto = read.GetValue(2).ToString();
                    }
                    read.Close();
                }
                catch (Exception ex)
                {
                    //saldo = "eRROR";
                }
                finally
                {
                    conexion.Close();
                }
            }
            return usuario;          
        }

        /***********************************Reportes******************************************/
        /**reprote de aprobacion*/
        public DatosCliente ObtenerDatosGenerales(String orden) {
            DatosCliente datos = new DatosCliente();
            using (SqlConnection con = new SqlConnection(cadenaconexion))
            {
                con.Open();
                string consulta = "SELECT ORDEN.NITCLIENTE,CLIENTE.NOMBRECLIENTE,CLIENTE.APELLIDOSCLIENTE,CLIENTE.DIRECCIONDOMICILIO,CLIENTE.TELEFONO FROM ORDEN INNER JOIN CLIENTE ON CLIENTE.NITCLIENTE=ORDEN.NITCLIENTE WHERE ORDEN.CODIGOORDEN='" + orden + "';";
                SqlCommand com = new SqlCommand(consulta, con);
                SqlDataReader leer = com.ExecuteReader();
                try
                {
                    while (leer.Read())
                    {
                            datos.Nit = leer.GetValue(0).ToString();
                            datos.Nombre = leer.GetValue(1).ToString();
                            datos.Apellido = leer.GetValue(2).ToString();
                            datos.Direccion1 = leer.GetValue(3).ToString();
                            datos.Telefono1 = leer.GetValue(4).ToString();
                    }
                }
                catch (Exception ex)
                {
                    //error
                }
                finally
                {
                    leer.Close();
                    con.Close();
                }
            }
            return datos;   
        }

        public Orden1 obtenerDatosOrdenGeneral(string orden) {
            Orden1 Dtorden = new Orden1();
            using (SqlConnection con = new SqlConnection(cadenaconexion))
            {
                con.Open();
                string consulta = "SELECT ORDEN.TOTALPAGAR,ORDEN.FECHACREACION,ORDEN.FECHACERRADA,ORDEN.FECHAAPROBACION,EMPLEADO.NOMBRES,PUESTO.NOMBREPUESTO FROM ORDEN INNER JOIN EMPLEADO ON ORDEN.EMPLEADOAPROBACION=EMPLEADO.NITEMPLEADO INNER JOIN PUESTO ON EMPLEADO.CODIGOPUESTO=PUESTO.CODPUESTO WHERE ORDEN.CODIGOORDEN='" + orden + "';";
                SqlCommand com = new SqlCommand(consulta, con);
                
                try
                {SqlDataReader leer = com.ExecuteReader();
                    while (leer.Read())
                    {
                        Dtorden.ValorTotal1 = leer.GetValue(0).ToString();
                        Dtorden.FechaCreacion1 = leer.GetValue(1).ToString();
                        Dtorden.FechaCerrada1 = leer.GetValue(2).ToString();
                        Dtorden.FechaAprobacion1 = leer.GetValue(3).ToString();
                        Dtorden.EmpleadoAprobo1 = leer.GetValue(4).ToString();
                        Dtorden.EmpeadoPuesto1 = leer.GetValue(5).ToString();                       
                    }
                    leer.Close();
                }
                catch (Exception ex)
                {
                    //error
                }
                finally
                {                   
                    con.Close();
                }
            }
            return Dtorden;
        }

        public Lista obtenerDatosLista(string nitcliente,string fechainicio) {
            Lista dtlista = new Lista();
            using (SqlConnection con = new SqlConnection(cadenaconexion))
            {
                con.Open();
                string consulta = "SELECT LISTACLIENTE.CODIGOLISTAPRECIOS,LISTAPRECIOS.NOMBRE FROM LISTACLIENTE INNER JOIN LISTAPRECIOS ON LISTACLIENTE.CODIGOLISTAPRECIOS=LISTAPRECIOS.CODIGO WHERE LISTACLIENTE.NITCLIENTE='"+nitcliente+"'  AND CAST( LISTACLIENTE.FECHAINICIO AS datetime2)<= CAST( '"+fechainicio+"' as datetime2);";
                SqlCommand com = new SqlCommand(consulta, con);
                SqlDataReader leer = com.ExecuteReader();
                try
                {
                    while (leer.Read())
                    {
                        dtlista.ListaPrecioCodigo1 = leer.GetValue(0).ToString();
                        dtlista.ListaPrecioNombre1 = leer.GetValue(1).ToString();
                    }
                }
                catch (Exception ex)
                {
                    //error
                }
                finally
                {
                    leer.Close();
                    con.Close();
                }
            }
            return dtlista;
        }

        public LinkedList<Productos> DetalleOrden(string NoOrden) {
            LinkedList<Productos> detalleproductos = new LinkedList<Productos> ();            
            using (SqlConnection con = new SqlConnection(cadenaconexion))
            {
                con.Open();
                string consulta = " select CODIGOPRODUCTO,NOMBREPRODUCTO,VALOR,CANTIDAD from DETALLEPRODUCTOORDEN where CODIGOORDEN='"+NoOrden+"' order by NOMBREPRODUCTO;";
                SqlCommand com = new SqlCommand(consulta, con);
                SqlDataReader leer = com.ExecuteReader();
                try
                {
                    while (leer.Read())
                    {   
                        Productos product = new Productos();
                        product.CodigoProducto = leer.GetValue(0).ToString();
                        product.Nombre = leer.GetValue(1).ToString();
                        product.Precio = leer.GetValue(2).ToString();
                        product.Cantidad1 = leer.GetValue(3).ToString();
                        decimal total = Convert.ToDecimal(leer.GetValue(2).ToString()) * Convert.ToDecimal(leer.GetValue(3).ToString());
                        product.PrecioTotal1 =Convert.ToString(total);
                        detalleproductos.AddFirst(product);
                    }
                }
                catch (Exception ex)
                {
                    //error
                }
                finally
                {
                    leer.Close();
                    con.Close();
                }
            }
            return detalleproductos;      
        } 
        /**Reporte empleados meta***/
        //Reporte de metas de vencdedor
        public string TotalOrdenesCerradas(string nitEmpledo,string fecha) {
            string numerototal = "";            
            using (SqlConnection con = new SqlConnection(cadenaconexion))
            {
                con.Open();
                string consulta = "select COUNT(ESTADOAPROBACION) from ORDEN where ESTADOAPROBACION in('Cerrada','Aprobado') and ORDEN.NITEMPLEADOVENDE='"+nitEmpledo+"' and  (CONVERT(date,ORDEN.FECHACERRADA,103) BETWEEN CONVERT(date,'1/"+fecha+"',103) AND CONVERT(date,'30/"+fecha+"',103));";
                SqlCommand com = new SqlCommand(consulta, con);
                try
                {
                    SqlDataReader leer = com.ExecuteReader();
                    while (leer.Read())
                    {
                        numerototal = leer.GetValue(0).ToString();
                    }
                    leer.Close();
                }
                catch (Exception ex)
                {
                    //error
                }
                finally
                {
                    con.Close();
                }
            }
            return numerototal;      
        }

        public string TotalOrdenesPagadas(string nitusuario,string fecha) {
            string numerototal = "";
            using (SqlConnection con = new SqlConnection(cadenaconexion))
            {
                con.Open();
                string consulta = "select COUNT(CODIGOORDEN) from ORDEN where ESTADOAPROBACION in('Cerrada','Aprobado') and ORDEN.PAGOABONO='Pagando' and ORDEN.NITEMPLEADOVENDE='" + nitusuario + "' and  (CONVERT(date,ORDEN.FECHACERRADA,103) BETWEEN CONVERT(date,'1/" + fecha + "',103) AND CONVERT(date,'30/" + fecha + "',103));";
                SqlCommand com = new SqlCommand(consulta, con);
                try
                {
                    SqlDataReader leer = com.ExecuteReader();
                    while (leer.Read())
                    {
                        numerototal = leer.GetValue(0).ToString();
                    }
                    leer.Close();
                }
                catch (Exception ex)
                {
                    //error
                }
                finally
                {
                    con.Close();
                }
            }
            return numerototal;              
        }

        public decimal TotalMetaEmpleadoVendedor(string fecha, string nitempleado)
        {
            decimal total = 0;
            using (SqlConnection con = new SqlConnection(cadenaconexion))
            {
                con.Open();
                string consulta = "select SUM(VENTAMETA) from DETALLEMETA inner join METAS on detallemeta.codigometa=METAS.IDMETA where (CONVERT(date,METAS.FECHA,103) BETWEEN CONVERT(date,'1/"+fecha+"',103) AND CONVERT(date,'30/"+fecha+"',103)) and METAS.CODIGOEMPELADO='"+nitempleado+"';";
                SqlCommand com = new SqlCommand(consulta, con);
                try
                {
                    SqlDataReader leer = com.ExecuteReader();
                    while (leer.Read())
                    {
                        total = Convert.ToDecimal(leer.GetValue(0).ToString());
                    }
                    leer.Close();
                }
                catch (Exception ex)
                {
                    //error
                }
                finally
                {
                    con.Close();
                }
            }
            return total;
        }
        //reporte de metas de VEntas metsXcategoria
        public Metas TotalMetaMesVendedor(string nit,string fecha,string categoria) {
            Metas meta = new Metas();
            using (SqlConnection con = new SqlConnection(cadenaconexion))
            {
                con.Open();
                string consulta = "select IDMETA, SUM(VENTAMETA) from METAS inner join DETALLEMETA on METAS.IDMETA=DETALLEMETA.CODIGOMETA inner join PRODUCTO on DETALLEMETA.CODIGOPRODUC=PRODUCTO.CODIGOPRODUCTO inner join CATEGORIA on PRODUCTO.CATEGORIA=CATEGORIA.CODCATEGORIA where (CONVERT(date,METAS.FECHA,103) BETWEEN CONVERT(date,'1/"+fecha+"',103) AND CONVERT(date,'30/"+fecha+"',103)) and (METAS.CODIGOEMPELADO='"+nit+"') and CATEGORIA.CODCATEGORIA='"+categoria+"'  group by IDMETA;";
                SqlCommand com = new SqlCommand(consulta, con);
                try
                {
                    SqlDataReader leer = com.ExecuteReader();
                    while (leer.Read())
                    {
                        meta.Id = leer.GetValue(0).ToString();
                        meta.SumaTotal = leer.GetValue(1).ToString();
                    }
                    leer.Close();
                }
                catch (Exception ex)
                {
                    //error
                }
                finally
                {
                    con.Close();
                }
            }
            return meta;      
        }

        public string TotalOrdencesCerradasXCategoria(string nit,string fecha,string categoria) {
            string numerototal = "";
            using (SqlConnection con = new SqlConnection(cadenaconexion))
            {
                con.Open();
                string consulta = "select COUNT(ESTADOAPROBACION) from ORDEN inner join DETALLEPRODUCTOORDEN on ORDEN.CODIGOORDEN=DETALLEPRODUCTOORDEN.CODIGOORDEN inner join PRODUCTO on DETALLEPRODUCTOORDEN.CODIGOPRODUCTO=PRODUCTO.CODIGOPRODUCTO inner join CATEGORIA on PRODUCTO.CATEGORIA=CATEGORIA.CODCATEGORIA  where ESTADOAPROBACION in('Cerrada','Aprobado') and ORDEN.NITEMPLEADOVENDE='"+nit+"' and  (CONVERT(date,ORDEN.FECHACERRADA,103) BETWEEN CONVERT(date,'1/"+fecha+"',103) AND CONVERT(date,'30/"+fecha+"',103)) and CATEGORIA.CODCATEGORIA='"+categoria+"';";
                SqlCommand com = new SqlCommand(consulta, con);
                try
                {
                    SqlDataReader leer = com.ExecuteReader();
                    while (leer.Read())
                    {
                        numerototal = leer.GetValue(0).ToString();
                    }
                    leer.Close();
                }
                catch (Exception ex)
                {
                    //error
                }
                finally
                {
                    con.Close();
                }
            }
            return numerototal;             
        }

        public string TotalOrdenesPagadasXCategoria(string nit, string fecha, string categoria) {
            string numerototal = "";
            using (SqlConnection con = new SqlConnection(cadenaconexion))
            {
                con.Open();
                string consulta = "select COUNT(ORDEN.CODIGOORDEN) from ORDEN inner join DETALLEPRODUCTOORDEN on ORDEN.CODIGOORDEN=DETALLEPRODUCTOORDEN.CODIGOORDEN inner join PRODUCTO on DETALLEPRODUCTOORDEN.CODIGOPRODUCTO=PRODUCTO.CODIGOPRODUCTO inner join CATEGORIA on PRODUCTO.CATEGORIA=CATEGORIA.CODCATEGORIA where ESTADOAPROBACION in('Cerrada','Aprobado') and ORDEN.PAGOABONO='Pagando' and ORDEN.NITEMPLEADOVENDE='"+nit+"' and  (CONVERT(date,ORDEN.FECHACERRADA,103) BETWEEN CONVERT(date,'1/"+fecha+"',103) AND CONVERT(date,'30/+"+fecha+"',103)) and CATEGORIA.CODCATEGORIA='"+categoria+"';";
                SqlCommand com = new SqlCommand(consulta, con);
                try
                {
                    SqlDataReader leer = com.ExecuteReader();
                    while (leer.Read())
                    {
                        numerototal = leer.GetValue(0).ToString();
                    }
                    leer.Close();
                }
                catch (Exception ex)
                {
                    //error
                }
                finally
                {
                    con.Close();
                }
            }
            return numerototal;          
        }

        /* Reporte meta*Producto **********************/
       public LinkedList<string> CodigosDeProductos() {
            LinkedList<string> codigos = new LinkedList<string>();
            using (SqlConnection con = new SqlConnection(cadenaconexion))
            {
                con.Open();
                string consulta = "select PRODUCTO.CODIGOPRODUCTO from PRODUCTO;";
                SqlCommand com = new SqlCommand(consulta, con);
                SqlDataReader leer = com.ExecuteReader();
                try
                {
                    while (leer.Read())
                    {
                        codigos.AddFirst(leer.GetValue(0).ToString());
                    }
                }
                catch (Exception ex)
                {
                    //error
                }
                finally
                {
                    leer.Close();
                    con.Close();
                }
            }
            return codigos; 
        }

        public LinkedList<Productos> DetalleVentasProducto(string CodigoProducto) {
            LinkedList<Productos> detalleproductos = new LinkedList<Productos> { };
            
            using (SqlConnection con = new SqlConnection(cadenaconexion))
            {
                con.Open();
                string consulta = "select DETALLEPRODUCTOORDEN.CODIGOORDEN,NOMBREPRODUCTO,VALOR,CANTIDAD from DETALLEPRODUCTOORDEN inner join ORDEN on ORDEN.CODIGOORDEN=DETALLEPRODUCTOORDEN.CODIGOORDEN where ORDEN.TOTALPAGAR>0 and( CODIGOPRODUCTO='"+CodigoProducto+"') order by NOMBREPRODUCTO;";
                SqlCommand com = new SqlCommand(consulta, con);
                SqlDataReader leer = com.ExecuteReader();
                try
                {
                    while (leer.Read())
                    {   
                        Productos product = new Productos();
                        product.CodigoOrden1 = leer.GetValue(0).ToString();
                        product.Nombre = leer.GetValue(1).ToString();
                        product.Precio = leer.GetValue(2).ToString();
                        product.Cantidad1 = leer.GetValue(3).ToString();
                        decimal total = Convert.ToDecimal(leer.GetValue(2).ToString()) * Convert.ToDecimal(leer.GetValue(3).ToString());
                        product.PrecioTotal1 = Convert.ToString(total);
                        detalleproductos.AddFirst(product);
                    }
                }
                catch (Exception ex)
                {
                    //error
                }
                finally
                {
                    leer.Close();
                    con.Close();
                }
            }
            return detalleproductos;          
        }
        /**Reporte meta*categoria*/
        public LinkedList<string> CodigosCategorias() {
            LinkedList<string> codigos = new LinkedList<string>();
            using (SqlConnection con = new SqlConnection(cadenaconexion))
            {
                con.Open();
                string consulta = " select CATEGORIA.CODCATEGORIA from CATEGORIA;";
                SqlCommand com = new SqlCommand(consulta, con);
                SqlDataReader leer = com.ExecuteReader();
                try
                {
                    while (leer.Read())
                    {
                        codigos.AddFirst(leer.GetValue(0).ToString());
                    }
                }
                catch (Exception ex)
                {
                    //error
                }
                finally
                {
                    leer.Close();
                    con.Close();
                }
            }
            return codigos;       
        }

        public LinkedList<Productos> DetalleVentasCategoria(string CodigoCategoria) {
            LinkedList<Productos> detalleproductos = new LinkedList<Productos> { };           
            using (SqlConnection con = new SqlConnection(cadenaconexion))
            {
                con.Open();
                string consulta = "select  PRODUCTO.NOMBREPRODUCTO,DETALLEPRODUCTOORDEN.CODIGOORDEN,DETALLEPRODUCTOORDEN.VALOR,DETALLEPRODUCTOORDEN.CANTIDAD from CATEGORIA inner join PRODUCTO on PRODUCTO.CATEGORIA=CATEGORIA.CODCATEGORIA inner join DETALLEPRODUCTOORDEN on PRODUCTO.CODIGOPRODUCTO=DETALLEPRODUCTOORDEN.CODIGOPRODUCTO where CATEGORIA.CODCATEGORIA='" + CodigoCategoria + "';";
                SqlCommand com = new SqlCommand(consulta, con);
                SqlDataReader leer = com.ExecuteReader();
                try
                {
                    while (leer.Read())
                    {
                        Productos product = new Productos();
                        product.Nombre = leer.GetValue(0).ToString();
                        product.CodigoOrden1 = leer.GetValue(1).ToString();
                        product.Precio = leer.GetValue(2).ToString();
                        product.Cantidad1 = leer.GetValue(3).ToString();
                        decimal total = Convert.ToDecimal(leer.GetValue(2).ToString()) * Convert.ToDecimal(leer.GetValue(3).ToString());
                        product.PrecioTotal1 = Convert.ToString(total);
                        detalleproductos.AddFirst(product);
                    }
                }
                catch (Exception ex)
                {
                    //error
                }
                finally
                {
                    leer.Close();
                    con.Close();
                }
            }
            return detalleproductos;       
        }

        public string TotalDeOrdenesCategoria(string CodigoCategoria) {//me duevuelve el total de ordenes en dodne se encuentra la categoria
            string numerototal = "";
            using (SqlConnection con = new SqlConnection(cadenaconexion))
            {
                con.Open();
                string consulta = "  select count(CODIGOORDEN)from DETALLEPRODUCTOORDEN inner join PRODUCTO on DETALLEPRODUCTOORDEN.CODIGOPRODUCTO=PRODUCTO.CODIGOPRODUCTO where PRODUCTO.CATEGORIA='"+CodigoCategoria+"';";
                SqlCommand com = new SqlCommand(consulta, con);
                try
                {
                    SqlDataReader leer = com.ExecuteReader();
                    while (leer.Read())
                    {
                        numerototal = leer.GetValue(0).ToString();
                    }
                    leer.Close();
                }
                catch (Exception ex)
                {
                    //error
                }
                finally
                {
                    con.Close();
                }
            }
            return numerototal;  
        
        
        
        }
        /********REPORTE METAS X CLIENTE **/
        public LinkedList<string> NumerosOrdenesCliente(string nitcliente) {
            LinkedList<string> ordenes = new LinkedList<string>();
            string consulta = " SELECT ORDEN.CODIGOORDEN FROM ORDEN INNER JOIN CLIENTE ON ORDEN.NITCLIENTE=CLIENTE.NITCLIENTE WHERE CLIENTE.NITCLIENTE='" + nitcliente + "'  and ORDEN.TOTALPAGAR>0;";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                try
                {
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        ordenes.AddFirst(read.GetValue(0).ToString());
                    }
                    read.Close();
                }
                catch (Exception ex)
                {
                    // saldo = "eRROR";
                }
                finally
                {
                    conexion.Close();
                }
            }
            return ordenes;       
        }

        public LinkedList<PagoOrden> NumeroPagosOrden(string NoOrden) {
            LinkedList<PagoOrden> pago = new LinkedList<PagoOrden>();
            
            string consulta = "SELECT ORDEN.CODIGOORDEN, PAGO.TIPOMONEDA, MONEDA.VALOR , PAGO.VALORPAGO FROM PAGO INNER JOIN MONEDA ON PAGO.TIPOMONEDA=MONEDA.NOMBRE INNER JOIN ORDEN ON PAGO.NOORDEN=ORDEN.CODIGOORDEN WHERE ORDEN.CODIGOORDEN='"+NoOrden+"';";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                try
                {
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {   
                        PagoOrden pag = new PagoOrden();
                        pag.NoOrden1=read.GetValue(0).ToString();
                        pag.MonedaPago1 = read.GetValue(1).ToString();
                        pag.Monedavalor1 = read.GetValue(2).ToString();
                        pag.ValorPago1 = read.GetValue(3).ToString();
                        pago.AddFirst(pag);
                    }
                    read.Close();
                }
                catch (Exception ex)
                {
                    // saldo = "eRROR";
                }
                finally
                {
                    conexion.Close();
                }
            }
            return pago;       
        }

        public LinkedList<PagoOrden> NumerosOrdenAnuladosPagos() {
            LinkedList<PagoOrden> pago = new LinkedList<PagoOrden>();

            string consulta = "select CODIGOORDEN,PAGO.VALORPAGO,pago.TIPO from ORDEN inner join PAGO on ORDEN.CODIGOORDEN=PAGO.NOORDEN where ORDEN.ESTADOAPROBACION='ANULADO' and ORDEN.PAGOABONO='ANULADO';";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                try
                {
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        PagoOrden pag = new PagoOrden();
                        pag.NoOrden1 = read.GetValue(0).ToString();
                        pag.MonedaPago1 = read.GetValue(1).ToString();
                        pag.TipoPago1 = read.GetValue(2).ToString();
                        pago.AddFirst(pag);
                    }
                    read.Close();
                }
                catch (Exception ex)
                {
                    // saldo = "eRROR";
                }
                finally
                {
                    conexion.Close();
                }
            }
            return pago;               
        }

        /*****************************Anular Venta*******************************************/

        public LinkedList<string> AnulacionVentaOrdenConPago() {
            LinkedList<string> ordenes = new LinkedList<string>();
            string consulta = "SELECT ORDEN.CODIGOORDEN FROM ORDEN WHERE (ORDEN.PAGOABONO='Pagando') AND (SALDOFALTA>0);";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                try
                {
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        ordenes.AddFirst(read.GetValue(0).ToString());
                    }
                    read.Close();
                }
                catch (Exception ex)
                {
                    // saldo = "eRROR";
                }
                finally
                {
                    conexion.Close();
                }
            }
            return ordenes;
        }

        public LinkedList<string> AnulacionVentaOrdenSupervisor(string nitempleado) {
            LinkedList<string> ordenes = new LinkedList<string>();
            string consulta = "SELECT ORDEN.CODIGOORDEN FROM ORDEN INNER JOIN EMPLEADO ON ORDEN.NITEMPLEADOVENDE=EMPLEADO.NITEMPLEADO WHERE (ORDEN.PAGOABONO='Pagando') AND (SALDOFALTA>0) AND (EMPLEADO.JEFEEMPLEADO='"+nitempleado+"');";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                try
                {
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        ordenes.AddFirst(read.GetValue(0).ToString());
                    }
                    read.Close();
                }
                catch (Exception ex)
                {
                    // saldo = "eRROR";
                }
                finally
                {
                    conexion.Close();
                }
            }
            return ordenes;      
        }

        public string ActualizarOrdeVentaAnulada(string NoOrden) {
            string mensaje = "";
            using (SqlConnection conexion = new SqlConnection(cadenaconexion))
            {
                conexion.Open();
                string consulta = "UPDATE ORDEN SET ORDEN.ESTADOAPROBACION='ANULADO',ORDEN.PAGOABONO='ANULADO' WHERE ORDEN.CODIGOORDEN='" + NoOrden + "';";
                SqlCommand com = new SqlCommand(consulta, conexion);
                try
                {
                    com.ExecuteScalar();
                    conexion.Close();
                    mensaje = "Se actualizo con exito!!!";
                }
                catch (Exception e)
                {
                    mensaje = "Error en la actualizacion";
                }
            }
            return mensaje;      
        }
        /**
         * Condiciones para ver si existen ordenes*/
        public Boolean numeroOrdenExiste(string CodigoOrden) {
            Boolean RES = false;
            string consulta = "SELECT CODIGOORDEN FROM ORDEN ;";
            using(SqlConnection conexion=new SqlConnection(cadenaconexion)){
                conexion.Open();
                SqlCommand comand = new SqlCommand(consulta, conexion);
                SqlDataReader read = comand.ExecuteReader();
                try {
                    while(read.Read()){

                        if (CodigoOrden.Equals(read.GetValue(0).ToString()))
                        {
                            RES = true;
                            break;
                        }
                        else {
                            RES = false;
                        }
                    }
                }catch(Exception ex){
                
                }finally{
                read.Close();
                conexion.Close();
                }
            }
            return RES;
        }
         
    }
}
