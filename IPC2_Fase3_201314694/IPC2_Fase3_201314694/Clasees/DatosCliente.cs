using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPC2_Fase3_201314694
{
    public class DatosCliente
    {
        string nit = "";

        public string Nit
        {
            get { return nit; }
            set { nit = value; }
        }

        string nombre = "";

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        string apellido = "";

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }
        string cantidadOrden = "";

        public string CantidadOrden
        {
            get { return cantidadOrden; }
            set { cantidadOrden = value; }
        }
        private string CantidadOrdenVenciadas = "";

        public string CantidadOrdenVenciadas1
        {
            get { return CantidadOrdenVenciadas; }
            set { CantidadOrdenVenciadas = value; }
        }

        string DiasCredito = "";

        public string DiasCredito1
        {
            get { return DiasCredito; }
            set { DiasCredito = value; }
        }

        string LimiteCredito = "";

        public string LimiteCredito1
        {
            get { return LimiteCredito; }
            set { LimiteCredito = value; }
        }
        string Direccion = "";

        public string Direccion1
        {
            get { return Direccion; }
            set { Direccion = value; }
        }
        private string Telefono = "";

        public string Telefono1
        {
            get { return Telefono; }
            set { Telefono = value; }
        }

    }
}