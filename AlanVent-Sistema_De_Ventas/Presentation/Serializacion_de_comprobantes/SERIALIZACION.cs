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

namespace AlanVent_Sistema_De_Ventas.Presentation.Serializacion_de_comprobantes
{
    public partial class Serializacion : Form
    {
        public Serializacion()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Serializacion_Load(object sender, EventArgs e)
        {
            Mostrar_SerializacionNCF();
        }
        private void Mostrar_SerializacionNCF()
        {
            
            txtCreditoFiscal.Text = Obtener_datos.MOSTRAR_SERIALIZACION_NCF_01();
            txtConsumo.Text = Obtener_datos.MOSTRAR_SERIALIZACION_NCF_02();
            txtCompras.Text = Obtener_datos.MOSTRAR_SERIALIZACION_NCF_11();
            txtGastosMenores.Text = Obtener_datos.MOSTRAR_SERIALIZACION_NCF_13();
        }

        private void txtCreditoFiscal_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
        private void ValidarVaciosACeros(ref MaskedTextBox txtMask)
        {
            if (txtMask.Text =="")
            {
                txtMask.Text = "0";
            }
        }
        private void txtCreditoFiscal_TextChanged(object sender, EventArgs e)
        {
            ValidarVaciosACeros(ref txtCreditoFiscal);
        }

        private void txtConsumo_TextChanged(object sender, EventArgs e)
        {
            ValidarVaciosACeros(ref txtConsumo);
        }

        private void txtCompras_TextChanged(object sender, EventArgs e)
        {
            ValidarVaciosACeros(ref txtCompras);
        }

        private void txtGastosMenores_TextChanged(object sender, EventArgs e)
        {
            ValidarVaciosACeros(ref txtGastosMenores);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Editar_datos.EditarSerializacionNCF(txtCreditoFiscal.Text,txtConsumo.Text,
                                                txtCompras.Text,txtGastosMenores.Text);
            this.Dispose();
        }
    }
}
