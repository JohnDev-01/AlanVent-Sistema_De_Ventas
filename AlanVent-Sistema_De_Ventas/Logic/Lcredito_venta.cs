using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanVent_Sistema_De_Ventas.Logic
{
    public class Lcredito_venta
    {
        public int IdCliente { get; set; }
        public string No_Venta { get; set; }
        public DateTime Fecha { get; set; }
        public double Efectivo { get; set; }
        public double Tarjeta { get; set; }
        public double Credito { get; set; }
        public double Abono { get; set; }
        public double Resta { get; set; }
        public string Estado_pago { get; set; }
    }
}
