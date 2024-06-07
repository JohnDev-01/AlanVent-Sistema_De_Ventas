using AlanVent_Sistema_De_Ventas.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.Visor_Precios
{
    public partial class BuscadorPrecios : Form
    {
        public BuscadorPrecios()
        {
            InitializeComponent();
        }

        private void txtCodigoProducto_TextChanged(object sender, EventArgs e)
        {
            TimerBUSCADORcodigodebarras.Start();
        }

        private void TimerBUSCADORcodigodebarras_Tick(object sender, EventArgs e)
        {
            TimerBUSCADORcodigodebarras.Stop();
            Obtener_datos.MostrarInformacionVisorPrecios(ref lblNombreProducto, ref lblPrecio, txtCodigoProducto.Text);
            txtCodigoProducto.SelectAll();
        }

        private void BuscadorPrecios_Load(object sender, EventArgs e)
        {
            MostrarInformacionEmpresa();
            txtCodigoProducto.Focus();
        }
        private void MostrarInformacionEmpresa()
        {
            Obtener_datos.MostrarNombreLogoEmpresa(ref pbIconoEmpresa, ref lblNombreEmpresa);
        }
    }
}
