using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.Reportes.Impresion_de_comprobantes.Factura
{
    public partial class FacturaForm : Form
    {
        public FacturaForm()
        {
            InitializeComponent();
        }

        private void FacturaForm_Load(object sender, EventArgs e)
        {
            CargarReport();
        }
        private void CargarReport()
        {
            Facturarpt f = new Facturarpt();
            reportViewer1.Report = f;
            reportViewer1.RefreshReport();

        }
    }
}
