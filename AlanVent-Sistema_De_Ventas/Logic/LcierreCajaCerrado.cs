using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanVent_Sistema_De_Ventas.Logic
{
    public class LcierreCajaCerrado
    {
        public string fechainicio { get; set; }
        public DateTime fechacierre { get; set; }
        public double SaldoIncial { get; set; }
        public double VentasEfvo { get; set; }
        public double VentasTarjeta { get; set; }
        public double VentasCredito { get; set; }
        public double VentasTotales { get; set; }
        public double IngresosVarios { get; set; }
        public double CobrosEfvo { get; set; }
        public double CobrosTarjeta { get; set; }
        public double ImpuestosVentas { get; set; }
        public double CreditoPagar { get; set; }
        public double CreditoCobrar { get; set; }
        public double GastosVarios { get; set; }
        public double GananciasVentas { get; set; }
        public double EfvoEsperado { get; set; }
        public double Diferencia { get; set; }
        public double PagosEfectivo { get; set; }
        public double PagosTarjeta { get; set; }
        public int Idcajero { get; set; }
        public int  Idcaja { get; set; }
    }
}
