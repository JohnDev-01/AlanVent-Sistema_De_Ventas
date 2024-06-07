using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;

namespace AlanVent_Sistema_De_Ventas.Presentation.Apertura_de_credito
{
    public partial class Por_Cobrar : Form
    {
        public Por_Cobrar()
        {
            InitializeComponent();
        }
        int idcliente;
        Panel p = new Panel();
        //Crud------
        private void insertarCreditos()
        {
            LcreditoPorCobrar parametros = new LcreditoPorCobrar();
            Insertar_datos funcion = new Insertar_datos();
            parametros.Descripcion = txtDetalle.Text;
            parametros.Fecha_registro = txtFregistro.Value;
            parametros.Fecha_vencimiento = txtFvenci.Value;
            parametros.Total = Convert.ToDouble(txtSaldo.Text);
            parametros.Saldo = Convert.ToDouble(txtSaldo.Text);
            
            parametros.Id_cliente = idcliente;
            if (funcion.insertar_CreditoPorCobrar(parametros) == true)
            {
               


                MessageBox.Show("Registrado","Proceso:",MessageBoxButtons.OK,MessageBoxIcon.Information);
                limpiar();
                buscar_clientes();

            }

        }
        
        private void buscar_clientes()
        {
            DataTable dt = new DataTable();
            Obtener_datos.buscar_clientes(ref dt, txtCliente.Text);
            datalistado.DataSource = dt;
            datalistado.Columns[1].Visible = false;
            datalistado.Columns[3].Visible = false;
            datalistado.Columns[4].Visible = false;
            datalistado.Columns[5].Visible = false;
            datalistado.Columns[6].Visible = false;
            datalistado.Columns[7].Visible = false;
            dibujarPanel();
           
        }
        //----------
        private void dibujarPanel()
        {
            datalistado.Dock = DockStyle.Fill;
            datalistado.Visible = true;
            p.Controls.Add(datalistado);
            p.Location = new Point(panelcorrdenadas.Location.X, panelcorrdenadas.Location.Y + panelproveedor.Location.Y);
            p.Size = new System.Drawing.Size(477, 303);
            Controls.Add(p);
            p.BringToFront();
        }
        private void limpiar()
        {
            txtSaldo.Clear();
            txtDetalle.Clear();
            
            idcliente = 0;
            txtCliente.Clear();
        }

        private void Por_Cobrar_Load(object sender, EventArgs e)
        {
            buscar_clientes();
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            buscar_clientes();
            txtSaldo.Clear();
            txtDetalle.Clear();
            txtCliente.Clear();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSaldo.Text))
            {
                rellenarCamposVacios();
                insertarCreditos();
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

        private void btnagregar_Click(object sender, EventArgs e)
        {
            Clientes_Proveedores.Clientes frm = new Clientes_Proveedores.Clientes();
            frm.ShowDialog();
        }

        private void datalistado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idcliente = Convert.ToInt32(datalistado.SelectedCells[1].Value);
            
            txtCliente.Text = datalistado.SelectedCells[2].Value.ToString();
            Controls.Remove(p);
        }

        private void txtCliente_Click(object sender, EventArgs e)
        {
            txtCliente.SelectAll();
        }

        private void txtSaldo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtSaldo, e);
        }
    }
}
