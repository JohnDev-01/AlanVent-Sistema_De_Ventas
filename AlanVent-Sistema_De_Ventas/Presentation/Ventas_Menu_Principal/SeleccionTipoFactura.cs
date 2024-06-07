using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.Ventas_Menu_Principal
{
    public partial class SeleccionTipoFactura : Form
    {
        public SeleccionTipoFactura()
        {
            InitializeComponent();
        }
        public string TipoComprobante;
        private void SeleccionTipoFactura_Load(object sender, EventArgs e)
        {
            TipoComprobante = "-";

        }

        private void rbCreditoFiscal_CheckedChanged(object sender, EventArgs e)
        {
            ValidarTipoFactura();
        }

        private void rbConsumidorFinal_CheckedChanged(object sender, EventArgs e)
        {
            ValidarTipoFactura();
        }
        private void ValidarTipoFactura()
        {
            if (rbConsumidorFinal.Checked == true)
            {
                TipoComprobante = "CONSUMIDORFINAL";
            }
            if (rbCreditoFiscal.Checked == true)
            {
                TipoComprobante = "CREDITOFISCAL";

            }   
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            TipoComprobante = "-";
            this.Dispose();
        }
    }
}
