using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPC2_Fase3_201314694.Clasees
{
    public class Moneda
    {
        private string NameMoneda = "";

        public string NameMoneda1
        {
            get { return NameMoneda; }
            set { NameMoneda = value; }
        }
        private string idMoneda = "";

        public string IdMoneda
        {
            get { return idMoneda; }
            set { idMoneda = value; }
        }
        private string simbolo = "";

        public string Simbolo
        {
            get { return simbolo; }
            set { simbolo = value; }
        }
        private string valor = "";

        public string Valor
        {
            get { return valor; }
            set { valor = value; }
        }
    }
}