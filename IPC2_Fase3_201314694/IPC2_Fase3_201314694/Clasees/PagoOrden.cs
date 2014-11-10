using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPC2_Fase3_201314694.Clasees
{
    public class PagoOrden
    {
        private string NoOrden = "";

        public string NoOrden1
        {
            get { return NoOrden; }
            set { NoOrden = value; }
        }
        private string MonedaPago = "";

        public string MonedaPago1
        {
            get { return MonedaPago; }
            set { MonedaPago = value; }
        }
        private string Monedavalor = "";

        public string Monedavalor1
        {
            get { return Monedavalor; }
            set { Monedavalor = value; }
        }
        private string ValorPago = "";

        public string ValorPago1
        {
            get { return ValorPago; }
            set { ValorPago = value; }
        }
    }
}