using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AlanVent_Sistema_De_Ventas.Presentation.Cotizaciones.Vista
{
    public partial class Clientes : Form
    {
        public Clientes()
        {
            InitializeComponent();
        }
        public int Idcliente;
        string estado;
        string TipoDeIdentificacion;
        private void Clientes_Load(object sender, EventArgs e)
        {
            mostrar();
        }
        private void mostrar()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrar_cliente(ref dt);
            datalistado.DataSource = dt;
            datalistado.Columns[9].Visible = false;
            panelRegistrofondo.Visible = false;
            pintarDatalistado();

        }
        private void editar()
        {
            Lclientes parametros = new Lclientes();
            Editar_datos funcion = new Editar_datos();
            parametros.IdCliente = Idcliente;
            parametros.Nombre = txtnombre.Text;
            parametros.Celular = txtcelular.Text;
            parametros.Direccion = txtdireccion.Text;
            parametros.Tipoidentificacion = TipoDeIdentificacion;
            parametros.Cedula = txtCedula.Text;
            parametros.Rnc = txtRNC.Text;
            if (funcion.editar_clientes(parametros) == true)
            {
                mostrar();
            }
        }
        private void eliminar()
        {
            try
            {
                Lclientes parametros = new Lclientes();
                Eliminar_datos funcion = new Eliminar_datos();
                parametros.IdCliente = Idcliente;
                if (funcion.eliminar_clientes(parametros) == true)
                {
                    mostrar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }
        private void restaurar()
        {
            Lclientes parametros = new Lclientes();
            Editar_datos funcion = new Editar_datos();
            parametros.IdCliente = Idcliente;
            if (funcion.restaurar_clientes(parametros) == true)
            {
                mostrar();
            }

        }
        private void buscar()
        {
            DataTable dt = new DataTable();
            Obtener_datos.buscar_clientes(ref dt, txtbusca.Text);
            datalistado.DataSource = dt;
            datalistado.Columns[9].Visible = false;
            pintarDatalistado();
        }
        public static void Multilinea(ref DataGridView List)
        {
            List.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            List.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            List.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            List.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            List.EnableHeadersVisualStyles = false;
            List.BackgroundColor = Color.White;
            List.RowTemplate.DefaultCellStyle.BackColor = Color.White;
            List.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;
            List.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.Gainsboro;
            List.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.Black;



            DataGridViewCellStyle styCabeceras = new DataGridViewCellStyle();
            styCabeceras.BackColor = System.Drawing.Color.White;
            styCabeceras.ForeColor = System.Drawing.Color.Black;
            styCabeceras.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            List.ColumnHeadersDefaultCellStyle = styCabeceras;
        }
        private void insertar()
        {
            Lclientes parametros = new Lclientes();
            Insertar_datos funcion = new Insertar_datos();
            parametros.Nombre = txtnombre.Text;
            parametros.Celular = txtcelular.Text;
            parametros.Direccion = txtdireccion.Text;
            parametros.Tipoidentificacion = TipoDeIdentificacion;
            parametros.Cedula = txtCedula.Text;
            parametros.Rnc = txtRNC.Text;
            if (funcion.Insertar_clientes(parametros) == true)
            {
                mostrar();
            }

        }
        private void pintarDatalistado()
        {
           Multilinea(ref datalistado);
            datalistado.Columns[3].Visible = false;
            datalistado.Columns[0].Width = 50;
            datalistado.Columns[1].Width = 50;
            datalistado.Columns[2].Width = 100;

            foreach (DataGridViewRow row in datalistado.Rows)
            {
                string estado = Convert.ToString(row.Cells["Estado"].Value);
                if (estado == "ELIMINADO")
                {
                    row.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Strikeout | FontStyle.Bold);
                    row.DefaultCellStyle.ForeColor = Color.Red;
                }

            }
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            Nuevo();
        }
        private void Nuevo()
        {
            panelRegistrofondo.Visible = true;
            Panelregistro.Visible = true;
            limpiar();
            btnGuardar.Visible = true;
            btnGuardarCambios.Visible = false;
            txtnombre.Focus();
            panelRegistrofondo.Dock = DockStyle.Fill;
            Panelregistro.Location = new Point((panelRegistrofondo.Width - Panelregistro.Width) / 2, 28);
            ValidarPanelesChecked();
        }
        private void limpiar()
        {
            txtnombre.Clear();
            txtcelular.Clear();
            txtdireccion.Clear();
            txtCedula.Clear();
            txtRNC.Clear();
        }
       
        private void datalistado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == datalistado.Columns["Editar"].Index)
            {
                obtenerDatos();
            }
            if (e.ColumnIndex == datalistado.Columns["Eliminarg"].Index)
            {
                obtenerId_estado();
                if (estado == "ACTIVO")
                {
                    DialogResult result = MessageBox.Show("¿Realmente desea eliminar este Registro?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        eliminar();
                    }
                }
            }
            if (e.ColumnIndex == datalistado.Columns["seleccionar"].Index)
            {
                obtenerId_estado();
                this.Hide();
            }
        }
        private void obtenerId_estado()
        {
            try
            {
                Idcliente = Convert.ToInt32(datalistado.SelectedCells[3].Value);
                estado = datalistado.SelectedCells[8].Value.ToString();

            }
            catch (Exception)
            {

            }
        }
        private void obtenerDatos()
        {
            try
            {
                ValidarPanelesChecked();
                limpiar();

                Idcliente = Convert.ToInt32(datalistado.SelectedCells[2].Value);
                txtnombre.Text = datalistado.SelectedCells[3].Value.ToString();
                txtdireccion.Text = datalistado.SelectedCells[4].Value.ToString();
                txtcelular.Text = datalistado.SelectedCells[5].Value.ToString();
                estado = datalistado.SelectedCells[6].Value.ToString();
                var tipoIdentificacion = datalistado.SelectedCells[9].Value.ToString();
                if (tipoIdentificacion == "CEDULA")
                {
                    rbCedula.Checked = true;
                    txtCedula.Text = datalistado.SelectedCells[8].Value.ToString();
                }
                else
                {
                    rbRnc.Checked = true;
                    txtRNC.Text = datalistado.SelectedCells[8].Value.ToString();
                }
                if (estado == "ELIMINADO")
                {
                    DialogResult result = MessageBox.Show("Este cliente se Elimino. ¿Desea Volver a Habilitarlo?", "Restaurando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        restaurar();
                        prepararEdicion();
                    }

                }
                else
                {
                    prepararEdicion();
                }
            }
            catch (Exception)
            {

            }
        }
       
        private void prepararEdicion()
        {
            panelRegistrofondo.Visible = true;
            panelRegistrofondo.Dock = DockStyle.Fill;
            Panelregistro.Location = new Point((panelRegistrofondo.Width - Panelregistro.Width) / 2, 22);
            
            btnGuardar.Visible = false;
            btnGuardarCambios.Visible = true;
        }
       
        private void txtbusca_TextChanged(object sender, EventArgs e)
        {
            if (txtbusca.Text == "")
            {
                lblBuscarEtiqueta.Visible = true;
            }
            else
            {
                lblBuscarEtiqueta.Visible = false;
            }
            buscar();
        }
        private bool ValidarNumeroDeDocumento()
        {
            bool estado = false;

            int numValidation = 2;
            if (rbCedula.Checked == true && (txtCedula.Text.Length) - 2 != 11)
            {
                MessageBox.Show("Digita un numero de cedula valido", "Valida:",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                numValidation++;
            }
            else
            {
                numValidation--;
            }
            if (rbRnc.Checked == true && (txtRNC.Text.Length) != 9)
            {
                MessageBox.Show("Digita un RNC valido", "Valida:",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                numValidation++;
            }
            else
            {
                numValidation--;
            }

            if (numValidation == 0)
                estado = true;
            else
                estado = false;

            return estado;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtnombre.Text))
            {
                rellenarCamposVacios();
                if (ValidarNumeroDeDocumento() == true)
                {
                    insertar();
                }
               
            }
            else
            {
                MessageBox.Show("Ingrese un nombre", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void rellenarCamposVacios()
        {
            if (string.IsNullOrEmpty(txtcelular.Text)) { txtcelular.Text = "-"; };
            if (string.IsNullOrEmpty(txtdireccion.Text)) { txtdireccion.Text = "-"; };
           

        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtnombre.Text))
            {
                rellenarCamposVacios();
                if (ValidarNumeroDeDocumento() == true)
                {
                    editar();
                }
            }
            else
            {
                MessageBox.Show("Ingrese un nombre", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void ValidarPanelesChecked()
        {
            if (rbCedula.Checked == true)
            {
                panelCedula.Visible = true;
                panelrnc.Visible = false;
                TipoDeIdentificacion = "CEDULA";
            }
            else
            {
                panelrnc.Visible = true;
                panelCedula.Visible = false;
                TipoDeIdentificacion = "RNC";
            }
        }
        private void rbRnc_CheckedChanged(object sender, EventArgs e)
        {
            ValidarPanelesChecked();

        }

        private void rbCedula_CheckedChanged(object sender, EventArgs e)
        {
            ValidarPanelesChecked();
        }
    }
}
