using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanVent_Sistema_De_Ventas.Presentation.Cotizaciones.Models
{
    public class Mcotizacion
    {
        public int Idcotizacion { get; set; }
        public int Idcliente { get; set; }
        public string Fecha { get; set; }
        public double Impuestos { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }
        public string SerialPc { get; set; }
        public double Descuento { get; set; }

    }
}
