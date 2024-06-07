using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanVent_Sistema_De_Ventas.Logic
{
    public class Lcaja
    {
        public int Id_Caja { get; set; }
        public string Descripcion { get; set; }
        public string Tema { get; set; }
        public string Serial_PC { get; set; }
        public string Impresora_Ticket { get; set; }
        public string Impresora_A4 { get; set; }
        public string Estado { get; set; }
        public string Tipo { get; set; }
        public string PuertoBalanza { get; set; }
        public string EstadoBalanza { get; set; }
    }
}
