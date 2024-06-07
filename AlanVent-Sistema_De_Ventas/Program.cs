using AlanVent_Sistema_De_Ventas.Presentation.Asistente_De_Instalacion_Servidor;
using AlanVent_Sistema_De_Ventas.Presentation.Clientes_Proveedores;
using AlanVent_Sistema_De_Ventas.Presentation.EMPRESA_CONFIG;
using AlanVent_Sistema_De_Ventas.Presentation.Membrecias;
using AlanVent_Sistema_De_Ventas.Presentation.Usuarios_Y_Permisos;
using AlanVent_Sistema_De_Ventas.Presentation.Ventas_Menu_Principal;
using AlanVent_Sistema_De_Ventas.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlanVent_Sistema_De_Ventas.Presentation.Reportes.Impresion_de_comprobantes.Factura;
using AlanVent_Sistema_De_Ventas.Presentation.Inventarios_Kardex;


namespace AlanVent_Sistema_De_Ventas
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //var frm = new Presentation.Cobros.CobrosForm();
            var frm = new frmInicioSeccion();
            frm.FormClosed += Frm_FormClosed;
            frm.ShowDialog();
            Application.Run();

        }

        private static void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
            Application.Exit();
        }
    }
}
