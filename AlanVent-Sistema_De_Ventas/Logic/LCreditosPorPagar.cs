using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanVent_Sistema_De_Ventas.Logic
{
    public class LCreditosPorPagar
    {
        public int Id_credito { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha_registro { get; set; }
        public DateTime Fecha_vencimiento { get; set; }
        public double Total { get; set; }
        public double Saldo { get; set; }
        public string Estado { get; set; }
        public int Id_caja { get; set; }
        public int Id_Proveedor { get; set; }
    }
}
