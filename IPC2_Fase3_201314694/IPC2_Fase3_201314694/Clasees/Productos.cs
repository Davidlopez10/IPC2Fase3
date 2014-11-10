using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPC2_Fase3_201314694
{
    public class Productos
    {
        private string nombre = "";

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private string codigoProducto = "";

        public string CodigoProducto
        {
            get { return codigoProducto; }
            set { codigoProducto = value; }
        }
         private string categoria = "";

        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }
        string precio = "";

        public string Precio
        {
            get { return precio; }
            set { precio = value; }
        }
        string codigoLista = "";

        public string CodigoLista
        {
            get { return codigoLista; }
            set { codigoLista = value; }
        }
        private string Cantidad = "";

        public string Cantidad1
        {
            get { return Cantidad; }
            set { Cantidad = value; }
        }

        private string PrecioTotal = "";

        public string PrecioTotal1
        {
            get { return PrecioTotal; }
            set { PrecioTotal = value; }
        }
        private string CodigoOrden = "";

        public string CodigoOrden1
        {
            get { return CodigoOrden; }
            set { CodigoOrden = value; }
        }
    }
}