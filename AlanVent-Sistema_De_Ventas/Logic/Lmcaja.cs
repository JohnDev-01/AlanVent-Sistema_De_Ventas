﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanVent_Sistema_De_Ventas.Logic
{
    public class Lmcaja
    {
        public int idcierrecaja { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafin { get; set; }
        public DateTime fechacierre { get; set; }
        public double ingresos { get; set; }
        public double egresos { get; set; }
        public double Saldo_queda_en_caja { get; set; }
        public int Id_usuario { get; set; }
        public double Total_calculado { get; set; }
        public double Total_real { get; set; }
        public string Estado { get; set; }
        public double Diferencia { get; set; }
        public int Id_caja { get; set; }
    }
}
