using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.Apertura_de_credito
{
    public partial class PorPagar : Form
    {
        public PorPagar()
        {
            InitializeComponent();
        }
        int idproveedor;
        Panel p = new Panel();
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSaldo.Text))
            {
                rellenarCamposVacios();
                Insertar_Creditos();
            }
            else
            {
                MessageBox.Show("Ingrese un saldo");
            }
            
        }
        private void rellenarCamposVacios()
        {
            if (string.IsNullOrEmpty(txtDetalle.Text)) { txtDetalle.Text = "-"; }
        }
        private void Insertar_Creditos()
        {
            LCreditosPorPagar parametros = new LCreditosPorPagar();
            Insertar_datos fn = new Insertar_datos();
            parametros.Descripcion = txtDetalle.Text;
            parametros.Fecha_registro = txtFregistro.Value;
            parametros.Fecha_vencimiento = txtFvenci.Value;
            parametros.Total = Convert.ToDouble(txtSaldo.Text);
            parametros.Saldo = Convert.ToDouble(txtSaldo.Text);
            parametros.Id_Proveedor = idproveedor;
            if (fn.insertar_CreditoPorPagar(parametros) == true)
            {
                MessageBox.Show("Registrado", "Proceso:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiar();
                buscar();
            }
        }

        private void txtProveedor_TextChanged(object sender, EventArgs e)
        {
            buscar();
        }
        private void limpiar()
        {
            txtSaldo.Clear();
            txtDetalle.Clear();
            idproveedor = 0;
            txtProveedor.Clear();
        }
        private void buscar()
        {
            DataTable dt = new DataTable();
            Obtener_datos.buscar_proveedores(ref dt, txtProveedor.Text);
            datalistado.DataSource = dt;
            datalistado.Columns[1].Visible = false;
            datalistado.Columns[3].Visible = false;
            datalistado.Columns[4].Visible = false;
            datalistado.Columns[5].Visible = false;
            datalistado.Columns[6].Visible = false;
            datalistado.Columns[7].Visible = false;
            dibujarPanel();
           
        }
        private void dibujarPanel()
        {
            datalistado.Dock = DockStyle.Fill;
            datalistado.Visible = true;
            p.Controls.Add(datalistado);
            p.Location = new Point(panelcoordenadas.Location.X, panelcoordenadas.Location.Y + panelproveedor.Location.Y);
            p.Size = new System.Drawing.Size(477, 303);
            Controls.Add(p);
            p.BringToFront();
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            Clientes_Proveedores.Proveedores frm = new Clientes_Proveedores.Proveedores();
            frm.ShowDialog();
        }

        private void PorPagar_Load(object sender, EventArgs e)
        {
            buscar();
        }

        private void txtProveedor_Click(object sender, EventArgs e)
        {
            txtProveedor.SelectAll();
        }

        private void datalistado_CellBorderStyleChanged(object sender, EventArgs e)
        {

        }

        private void datalistado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idproveedor = Convert.ToInt32(datalistado.SelectedCells[1].Value);
            txtProveedor.Text = datalistado.SelectedCells[2].Value.ToString();
            Controls.Remove(p);

        }

        private void txtSaldo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Logic.Bases.Separador_de_Numeros(txtSaldo, e);
        }
    }
}
