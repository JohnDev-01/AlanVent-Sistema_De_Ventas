using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Management;
using AlanVent_Sistema_De_Ventas.Logic;

namespace AlanVent_Sistema_De_Ventas.Presentation.Caja
{
    public partial class frm_Apertura_de_caja : Form
    {
        public frm_Apertura_de_caja()
        {
            InitializeComponent();
        }
        int txtidcaja;
      
         
        private void frm_Apertura_de_caja_Load(object sender, EventArgs e)
        {

            panelDineroEnCaja.Location = new Point((this.Width - panelDineroEnCaja.Width) / 2, (this.Height - panelDineroEnCaja.Height) / 2);
            Bases.Cambiar_idioma_regional();
            DataAccess.Obtener_datos.Obtener_id_caja_PorSerial(ref txtidcaja);
           


        }

        private void iniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            bool estado = DataAccess.Editar_datos.editar_dinero_caja_inicial(txtidcaja, Convert.ToDouble(txtmonto.Text));
            if(estado == true)
            {
                 Dispose();
                Ventas_Menu_Principal.frm_VENTAS_MENU_PRINCIPAL ven = new Ventas_Menu_Principal.frm_VENTAS_MENU_PRINCIPAL();
                ven.ShowDialog();
               
            }
           
            
        }

        private void omitirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool estado = DataAccess.Editar_datos.editar_dinero_caja_inicial(txtidcaja,0);
            if (estado == true)
            {
                 Dispose();
                Ventas_Menu_Principal.frm_VENTAS_MENU_PRINCIPAL ven = new Ventas_Menu_Principal.frm_VENTAS_MENU_PRINCIPAL();
                ven.ShowDialog();
                
            }
        }

        private void txtmonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtmonto, e);
        }
        private void ValidateNumberToString()
        {
            try
            {
                double Number = Convert.ToDouble(txtmonto.Text);
                txtmonto.Text = Bases.AsignarComa(Number);
                txtmonto.Focus();
                txtmonto.SelectionStart = txtmonto.Text.Length;
            }
            catch (Exception ex )
            {
                if (txtmonto.Text.Length > 0)
                txtmonto.Text = txtmonto.Text.Remove(txtmonto.Text.Length - 1);
            }
        }
        private void txtmonto_TextChanged(object sender, EventArgs e)
        {
            ValidateNumberToString();
        }
    }
}
