using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using IPC2_Fase3_201314694.Clasees;

namespace IPC2_Fase3_201314694.Paginas
{
    public partial class Administrador : System.Web.UI.Page
    {
        ConexionSQL CONEXION = new ConexionSQL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

            int LINEA = 0;
            if ((cargarArchivo.PostedFile != null) && (cargarArchivo.PostedFile.ContentLength > 0))
            {
                XmlDocument xdoc = new XmlDocument();
                try
                {
                    xdoc.Load(cargarArchivo.PostedFile.InputStream);
                    XmlNodeList definicion = (xdoc.GetElementsByTagName("definicion"));
                    foreach (XmlElement nodoInicial in definicion)
                    {
                        //recorre categoria
                        XmlNodeList categoria = ((XmlElement)nodoInicial).GetElementsByTagName("categoria");
                        foreach (XmlElement nodo in categoria)
                        {
                            XmlNodeList codigo = nodo.GetElementsByTagName("codigo");
                            XmlNodeList nombre = nodo.GetElementsByTagName("nombre");

                            if ((nombre.Count > 0) && (codigo.Count > 0))
                            {
                                // contenido.Text += nombre[0].InnerText + "" + codigo[0].InnerText + "</br>";
                                string dat = "'" + codigo[0].InnerText.Trim() + "','" + nombre[0].InnerText.Trim() + "'";
                                CONEXION.Insertar("CATEGORIA(CODCATEGORIA,NOMBRECATEGORIA)", dat);
                            }
                        }
                        //recorre producto
                        XmlNodeList producto = ((XmlElement)nodoInicial).GetElementsByTagName("producto");
                        foreach (XmlElement nodo in producto)
                        {
                            XmlNodeList codigo = nodo.GetElementsByTagName("codigo");
                            XmlNodeList nombre = nodo.GetElementsByTagName("nombre");
                            XmlNodeList categorias = nodo.GetElementsByTagName("categoria");

                            if ((categorias.Count > 0) && (nombre.Count > 0) && (codigo.Count > 0))
                            {
                                //   contenido.Text += categorias[0].InnerText + "</br>";
                                string DATOS = "'" + codigo[0].InnerText.Trim() + "','" + nombre[0].InnerText.Trim() + "','" + categorias[0].InnerText.Trim() + "'";
                                string tabla = "PRODUCTO(CODIGOPRODUCTO,NOMBREPRODUCTO,CATEGORIA)";
                                CONEXION.Insertar(tabla, DATOS);
                            }

                        }

                        //lista PRECIOS
                        XmlNodeList listas = ((XmlElement)nodoInicial).GetElementsByTagName("lista");
                        foreach (XmlElement nodo in listas)
                        {
                            XmlNodeList codigo = nodo.GetElementsByTagName("codigo");
                            XmlNodeList nombre = nodo.GetElementsByTagName("nombre");
                            XmlNodeList vigenciainicio = nodo.GetElementsByTagName("vigencia_inicio");//nuevo
                            XmlNodeList vigenciafin = nodo.GetElementsByTagName("vigencia_final");//nuevo
                            XmlNodeList detalle = nodo.GetElementsByTagName("detalle");
                            if ((nombre.Count > 0) && (codigo.Count > 0)&& (vigenciainicio.Count>0)&&(vigenciafin.Count>0))
                            {
                                //contenido.Text += nombre[0].InnerText+ codigo[0].InnerText + "</br>";
                                string tabla = "LISTAPRECIOS(CODIGO,NOMBRE,FECHAINICIO,FECHAFIN)";
                                string datos = "'" + codigo[0].InnerText.Trim() + "','" + nombre[0].InnerText.Trim() + "','"+vigenciainicio[0].InnerText+"','"+vigenciafin[0].InnerText+"'";
                                CONEXION.Insertar(tabla, datos);
                            }
                            //DETALLE DE LISTA DE PRECIOS
                            if (detalle.Count > 0)
                            {
                                XmlNodeList item = ((XmlElement)detalle[0]).GetElementsByTagName("item");
                                foreach (XmlElement node in item)
                                {
                                    XmlNodeList codigoproducto = node.GetElementsByTagName("codigo_producto");
                                    XmlNodeList valor = node.GetElementsByTagName("valor");
                                    if ((valor.Count > 0) && (codigoproducto.Count > 0))
                                    {
                                        string tabla = "DETALLELISTAPRECIOS(VALOR,FKCODIGOPRODUC,FKCODIGOLIST)";
                                        string dat = "" + valor[0].InnerText.Trim() + ",'" + codigoproducto[0].InnerText.Trim() + "','" + codigo[0].InnerText.Trim() + "'";
                                        //  contenido.Text += valor[0].InnerText  +codigoproducto[0].InnerText+ "</br>";
                                        CONEXION.Insertar(tabla, dat);
                                        //CONEXION.ActualizarProducto(valor[0].InnerText, codigoproducto[0].InnerText);
                                    }

                                }

                            }

                        }
                        //nodo departamento
                        XmlNodeList depto = ((XmlElement)nodoInicial).GetElementsByTagName("depto");
                        foreach (XmlElement nodo in depto)
                        {
                            XmlNodeList codigo = nodo.GetElementsByTagName("codigo");
                            XmlNodeList nombre = nodo.GetElementsByTagName("nombre");
                            // XmlNodeList codigocuidad = nodo.GetElementsByTagName("codigo_ciudad"); nuevo

                            if ((nombre.Count > 0) && (codigo.Count > 0))
                            {
                                // contenido.Text += codigocuidad[0].InnerText + "</br>";
                                string datos = "'" + codigo[0].InnerText.Trim() + "','" + nombre[0].InnerText.Trim() + "'";
                                string tabla = "DEPARTAMENTO(CODIGODEPARTAMENTO,NOMBRE)";
                                CONEXION.Insertar(tabla, datos);
                            }
                        }

                        //nodo ciudad
                        XmlNodeList ciudad = ((XmlElement)nodoInicial).GetElementsByTagName("ciudad");
                        foreach (XmlElement nodo in ciudad)
                        {
                            XmlNodeList codigo = nodo.GetElementsByTagName("codigo");
                            XmlNodeList nombre = nodo.GetElementsByTagName("nombre");
                            XmlNodeList codigoDeparta = nodo.GetElementsByTagName("codigo_depto");//nuevo
                            if ((nombre.Count > 0) && (codigo.Count > 0)&&(codigoDeparta.Count>0))
                            {
                                // contenido.Text += nombre[0].InnerText + "</br>";
                                string datos = "'" + codigo[0].InnerText.Trim() + "','" + nombre[0].InnerText.Trim() + "','"+codigoDeparta[0].InnerText+"'";
                                string tabla = "CIUDAD(CODIGOCIUDAD,NOMBRE,CODDEPARTAMENTO)";
                                CONEXION.Insertar(tabla, datos);
                            }
                        }

                       //nodo de cliente
                        XmlNodeList cliente = ((XmlElement)nodoInicial).GetElementsByTagName("cliente");
                        foreach (XmlElement nodo in cliente)
                        {
                            XmlNodeList nit = nodo.GetElementsByTagName("NIT");
                            XmlNodeList nombre = nodo.GetElementsByTagName("nombres");
                            XmlNodeList apellido = nodo.GetElementsByTagName("apellidos");
                            XmlNodeList nacimiento = nodo.GetElementsByTagName("nacimiento");
                            XmlNodeList direccion = nodo.GetElementsByTagName("direccion");
                            XmlNodeList telefono = nodo.GetElementsByTagName("telefono");
                            XmlNodeList celular = nodo.GetElementsByTagName("celular");
                            XmlNodeList email = nodo.GetElementsByTagName("email");
                            XmlNodeList cuidad = nodo.GetElementsByTagName("ciudad");
                            XmlNodeList dept = nodo.GetElementsByTagName("depto");
                            XmlNodeList limit = nodo.GetElementsByTagName("limite_credito");
                            XmlNodeList diascredito = nodo.GetElementsByTagName("dias_credito");
                            //XmlNodeList codigolis = nodo.GetElementsByTagName("codigo_lista");


                            if ((nit.Count > 0) && (nombre.Count > 0) && (apellido.Count > 0) && (nacimiento.Count > 0) && (direccion.Count > 0) && (telefono.Count > 0) && (celular.Count > 0) &&
                                (email.Count > 0) && (cuidad.Count > 0) && (dept.Count > 0) && (diascredito[0] != null) && (limit[0] != null))
                            {
                                // Label2.Text = "" + nit[0].InnerText + ",'" + nombre[0].InnerText + "','" + apellido[0].InnerText + "','" + nacimiento[0].InnerText + "','" + direccion[0].InnerText + "',"
                                //    + telefono[0].InnerText + "," + celular[0].InnerText + ",'" + email[0].InnerText + "'," + dept[0].InnerText + "," + cuidad[0].InnerText + "</br>";

                                string tabla = "CLIENTE(NITCLIENTE,NOMBRECLIENTE,APELLIDOSCLIENTE,FECHANACIMIENTOCLIENTE,DIRECCIONDOMICILIO,TELEFONO,CELULAR,EMAIL,LIMITECREDITO,DIASCREDITO,CODEPARTAMENTO,CODCIUDAD)";
                                //         (15946,'Juan','Gonzales','4/12/2000','Villa Canales, Zona 15',54891523,7518962,'juan@hotmail.com',1000,45,300,4)
                                string datos = "'" + nit[0].InnerText + "','" + nombre[0].InnerText + "','" + apellido[0].InnerText + "','" + nacimiento[0].InnerText + "','" + direccion[0].InnerText + "','" + telefono[0].InnerText + "','" + celular[0].InnerText + "','" + email[0].InnerText + "'," + limit[0].InnerText + "," + diascredito[0].InnerText + ",'" + dept[0].InnerText + "','" + cuidad[0].InnerText + "'";
                                //Label1.Text =datos;
                                CONEXION.Insertar(tabla, datos);
                                /*if (codigolis.Count > 0) NUEVO
                                {
                                    // Label2.Text = codigolis[0].InnerText;
                                    CONEXION.ActualizarLista(nit[0].InnerText, codigolis[0].InnerText);
                                }*/
                            }
                        }
                        //lista del cliente 
                        XmlNodeList listaxcliente = ((XmlElement)nodoInicial).GetElementsByTagName("lstXCliente");
                        foreach (XmlElement clint in listaxcliente)
                        {
                            XmlNodeList clientes = clint.GetElementsByTagName("cliente");
                            XmlNodeList codigoLista = clint.GetElementsByTagName("codigo_lista");
                            XmlNodeList vigenciaInicio = clint.GetElementsByTagName("vigencia_inicio");
                            XmlNodeList vigenciafin = clint.GetElementsByTagName("vigencia_final");
                            if (clientes.Count > 0 && codigoLista.Count > 0 && vigenciaInicio.Count > 0 && vigenciafin.Count > 0) {
                                string Tabla = "LISTACLIENTE(FECHAINICIO,FECHAFIN,CODIGOLISTAPRECIOS,NITCLIENTE)";
                                string datos = "'"+vigenciaInicio[0].InnerText+"','"+vigenciafin[0].InnerText+"','"+codigoLista[0].InnerText+"','"+clientes[0].InnerText+"'";
                                CONEXION.Insertar(Tabla, datos);
                            }
                        }

                        //nodo de puesto
                        XmlNodeList puesto = ((XmlElement)nodoInicial).GetElementsByTagName("puesto");

                        foreach (XmlElement nodo in puesto)
                        {
                            XmlNodeList codigo = nodo.GetElementsByTagName("codigo");
                            XmlNodeList nombre = nodo.GetElementsByTagName("nombre");
                            if (codigo.Count > 0 && nombre.Count > 0)
                            {
                                // contenido.Text += codigo[0].InnerText+nombre[0].InnerText;
                                string datos = "'" + codigo[0].InnerText.Trim() + "','" + nombre[0].InnerText.Trim() + "'";
                                string tabla = "PUESTO(CODPUESTO,NOMBREPUESTO)";
                                CONEXION.Insertar(tabla, datos);
                            }
                        }
                        
                        //empleados
                        XmlNodeList empleado = ((XmlElement)nodoInicial).GetElementsByTagName("empleado");
                        foreach (XmlElement nodo in empleado)
                        {
                            XmlNodeList nit = nodo.GetElementsByTagName("NIT");
                            XmlNodeList nombre = nodo.GetElementsByTagName("nombres");
                            XmlNodeList apellido = nodo.GetElementsByTagName("apellidos");
                            XmlNodeList nacimiento = nodo.GetElementsByTagName("nacimiento");
                            XmlNodeList direccion = nodo.GetElementsByTagName("direccion");
                            XmlNodeList telefono = nodo.GetElementsByTagName("telefono");
                            XmlNodeList celular = nodo.GetElementsByTagName("celular");
                            XmlNodeList email = nodo.GetElementsByTagName("email");
                            XmlNodeList codigopuesto = nodo.GetElementsByTagName("codigo_puesto");
                            XmlNodeList codigojefe = nodo.GetElementsByTagName("codigo_jefe");

                            if ((nit.Count > 0) && (nombre.Count > 0) && (apellido.Count > 0) && (nacimiento.Count > 0) && (direccion.Count > 0) && (telefono.Count > 0)
                                && (celular.Count > 0) && (email.Count > 0) && (codigopuesto.Count > 0))
                            {
                                string tabla = "EMPLEADO(NITEMPLEADO,NOMBRES,APELLIDOS,FECHANACIMIENTO,DIRECCION,TELEFONODOMICILIO,CELULAR,EMAIL,CODIGOPUESTO)";
                                string datos = "'" + nit[0].InnerText.Trim() + "','" + nombre[0].InnerText + "','" + apellido[0].InnerText + "','" + nacimiento[0].InnerText + "','" + direccion[0].InnerText + "','" + telefono[0].InnerText + "','" + celular[0].InnerText + "','" + email[0].InnerText + "','" + codigopuesto[0].InnerText.Trim() + "'";
                                CONEXION.Insertar(tabla, datos);
                                if (codigojefe[0] != null)
                                {
                                    CONEXION.ActualizarEmpleado(codigojefe[0].InnerText, nit[0].InnerText.Trim());
                                }
                            }
                        }

                        //lista 2 meta
                        XmlNodeList meta = ((XmlElement)nodoInicial).GetElementsByTagName("meta");
                        foreach (XmlElement nodo in meta)
                        {
                            XmlNodeList nit = nodo.GetElementsByTagName("NIT_empleado");
                            XmlNodeList mesmeta = nodo.GetElementsByTagName("mes_meta");
                            XmlNodeList detalles = nodo.GetElementsByTagName("detalle");

                            if ((nit.Count > 0) && (mesmeta.Count > 0))
                            {
                                string datos = "'" + mesmeta[0].InnerText + "','" + nit[0].InnerText.Trim() + "'";
                                string tablasme = "METAS(FECHA,CODIGOEMPELADO)";
                                CONEXION.Insertar(tablasme, datos);
                            }

                            if (detalles.Count > 0)
                            {
                                XmlNodeList item = ((XmlElement)detalles[0]).GetElementsByTagName("item");
                                foreach (XmlElement node in item)
                                {
                                    XmlNodeList codigoproducto = node.GetElementsByTagName("codigo_producto");
                                    XmlNodeList metaventa = node.GetElementsByTagName("valor");
                                    if ((metaventa.Count > 0) && (codigoproducto.Count > 0))
                                    {
                                        string tablas = "DETALLEMETA(VENTAMETA,CODIGOPRODUC,CODIGOMETA)";
                                        string dta = "" + metaventa[0].InnerText + ",'" + codigoproducto[0].InnerText.Trim() + "',"+CONEXION.IdMeta(mesmeta[0].InnerText,nit[0].InnerText)+"";
                                        //contenido.Text += metaventa[0].InnerText + codigoproducto[0].InnerText +"</br>";
                                        CONEXION.Insertar(tablas, dta);
                                        //CONEXION.ActualizarProducto(metaventa[0].InnerText, codigoproducto[0].InnerText);
                                    }
                                }
                            }
                        }

                        XmlNodeList moneda = ((XmlElement)nodoInicial).GetElementsByTagName("moneda");
                        foreach (XmlElement money in moneda)
                        {
                            XmlNodeList nombre = money.GetElementsByTagName("nombre");
                            XmlNodeList simbolo = money.GetElementsByTagName("simbolo");
                            XmlNodeList tasa = money.GetElementsByTagName("tasa");
                            if ((nombre.Count > 0) && (simbolo.Count > 0) && (tasa.Count > 0))
                            {
                                string tabla = "MONEDA(NOMBRE,SIMBOLO,VALOR)";
                                string datos = "'" + nombre[0].InnerText + "','" + simbolo[0].InnerText + "'," + tasa[0].InnerText + "";
                                CONEXION.Insertar(tabla, datos);
                            }
                        }
                        LINEA++;
                    }
                }
                catch (Exception ex)
                {
                    Label1.Visible = true;
                    Label1.Text = "Error en la linea " + LINEA + "  " + ex;
                }
                Label1.Visible=true;
                Label1.Text = "Se cargo con exito el Archivo. :)  ";
            }
            else
            {
                Label1.Visible=true;
                Label1.Text = "Error al cargar el archivo profavor cargar uno";
            }

        }
    }
}