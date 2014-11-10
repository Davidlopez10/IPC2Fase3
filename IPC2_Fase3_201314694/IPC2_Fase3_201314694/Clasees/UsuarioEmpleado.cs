using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPC2_Fase3_201314694
{
    public class UsuarioEmpleado
    {
        string nombre = "";

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        string puesto = "";

        public string Puesto
        {
            get { return puesto; }
            set { puesto = value; }
        }
        string nit = "";

        public string Nit
        {
            get { return nit; }
            set { nit = value; }
        }
        string jefe = "";

        public string Jefe
        {
            get { return jefe; }
            set { jefe = value; }
        }
    }
}