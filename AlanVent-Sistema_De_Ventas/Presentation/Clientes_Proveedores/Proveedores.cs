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

namespace AlanVent_Sistema_De_Ventas.Presentation.Clientes_Proveedores
{
    public partial class Proveedores : Form
    {
        public Proveedores()
        {
            InitializeComponent();
        }
        int IdProveedor;
        string estado;
        string TipoDeIdentificacion;
        string _ProveedorInformal;

        private void Nuevo()
        {

            Limpiar();
            btnGuardar.Visible = true;
            btnGuardarCambios.Visible = false;
            txtnombre.Focus();
            panelRegistrofondo.Visible = true;
            panelRegistrofondo.Dock = DockStyle.Fill;
            Panelregistro.Location = new Point((panelRegistrofondo.Width - Panelregistro.Width) / 2, 28);
            ValidarPanelesChecked();
        }
        private void Limpiar()
        {
            txtnombre.Clear();
            txtcelular.Clear();
            txtdireccion.Clear();
            txtCedula.Clear();
            txtRNC.Clear();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ValidarProveedorInformal();
            ValidarPanelesChecked();
            if (!string.IsNullOrEmpty(txtnombre.Text))
            {
                if (TipoDeIdentificacion == "CEDULA" && txtCedula.Text == "")
                {
                    MessageBox.Show("Ingrese un numero de cedula valido", "Datos Incompletos:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    if (ValidarNumeroDeDocumento() == true)
                    {
                        Rellenar_campos_vacios();
                        Insertar();
                    }
                }

            }
            else
            {
                MessageBox.Show("Ingrese un nombre", "Datos Incompletos:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
        private void Insertar()
        {
            Logic.Lproveedores models = new Logic.Lproveedores();
            DataAccess.Insertar_datos I = new DataAccess.Insertar_datos();
            models.Nombre = txtnombre.Text;
            models.Celular = txtcelular.Text;
            models.Direccion = txtdireccion.Text;
            models.TipoIdentificacion = TipoDeIdentificacion;
            models.Cedula = txtCedula.Text;
            models.Rnc = txtRNC.Text;
            models.Prov_Informal = _ProveedorInformal;
            if (I.Insertar_proveedores(models) == true)
            {
                mostrar();
            }
        }
        private void mostrar()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrar_proveedores(ref dt);
            datalistado.DataSource = dt;
            panelRegistrofondo.Visible = false;
            datalistado.Columns[10].Visible = false;
            PintarDt();
        }
        private void Rellenar_campos_vacios()
        {
            if (!string.IsNullOrEmpty(txtcelular.Text))
            {
                txtcelular.Text = "-";
            }
            if (!string.IsNullOrEmpty(txtdireccion.Text))
            {
                txtdireccion.Text = "-";
            }
            
        }

        private void datalistado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == datalistado.Columns["Eliminar"].Index)
            {
                try
                {
                    obtenerIdEstado();
                    if (estado == "ACTIVO")
                    {
                        DialogResult dr = MessageBox.Show("Realmente deseas borrar este registro?", "Confirma:", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr == DialogResult.Yes)
                        {
                            _Eliminar();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (e.ColumnIndex == datalistado.Columns["Editar"].Index)
            {
                obtenerIdEstado();
                obtener_datos_editar();
            }
        }
        private void _Eliminar()
        {
            try
            {
                Lproveedores p = new Lproveedores();
                Eliminar_datos d = new Eliminar_datos();
                p.IdProveedor = IdProveedor;
                if (d.Eliminar_proveedores(p) == true)
                {
                    mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void obtenerIdEstado()
        {
            IdProveedor = Convert.ToInt32(datalistado.SelectedCells[2].Value.ToString());
            estado = datalistado.SelectedCells[9].Value.ToString();
        }
        private void restaurar()
        {
            try
            {
                IdProveedor = Convert.ToInt32(datalistado.SelectedCells[2].Value.ToString());
                Lproveedores p = new Lproveedores();
                Editar_datos e = new Editar_datos();
                p.IdProveedor = IdProveedor;
                if (e.restaurar_proveedores(p) == true)
                {
                    mostrar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void obtener_datos_editar()
        {
            try
            {
                IdProveedor = Convert.ToInt32(datalistado.SelectedCells[2].Value.ToString());
                txtnombre.Text = datalistado.SelectedCells[3].Value.ToString();
                txtdireccion.Text = datalistado.SelectedCells[4].Value.ToString();
                txtCedula.Text = datalistado.SelectedCells[5].Value.ToString();
                txtRNC.Text = datalistado.SelectedCells[6].Value.ToString();
                TipoDeIdentificacion = datalistado.SelectedCells[7].Value.ToString();
                txtcelular.Text = datalistado.SelectedCells[8].Value.ToString();
                estado = datalistado.SelectedCells[9].Value.ToString();
                var proveedorInformal = Obtener_datos.IdentificarProveedorInformal(IdProveedor);
                if (proveedorInformal == true)
                {
                    checkProveedorInformal.Checked = true;
                }
                else
                {
                    checkProveedorInformal.Checked = false;
                }
                if (estado == "ELIMINADO")
                {
                    DialogResult r = MessageBox.Show("Este proveedor se ha eliminado, ¿Deseas restaurarlo?", "Restaurando...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (r == DialogResult.Yes)
                    {
                        restaurar();
                        if (TipoDeIdentificacion == "CEDULA")
                        {
                            rbCedula.Checked = true;
                        }
                        if (TipoDeIdentificacion == "RNC")
                        {
                            rbRnc.Checked = true;
                        }
                        panelRegistrofondo.Visible = true;
                        panelRegistrofondo.Dock = DockStyle.Fill;

                        Panelregistro.Location = new Point((panelRegistrofondo.Width - Panelregistro.Width) / 2, 28);
                        btnGuardar.Visible = false;
                        btnGuardarCambios.Visible = true;
                    }

                }
                else
                {
                    if (TipoDeIdentificacion == "CEDULA")
                    {
                        rbCedula.Checked = true;
                    }
                    if (TipoDeIdentificacion == "RNC")
                    {
                        rbRnc.Checked = true;
                    }
                    panelRegistrofondo.Visible = true;
                    panelRegistrofondo.Dock = DockStyle.Fill;

                    Panelregistro.Location = new Point((panelRegistrofondo.Width - Panelregistro.Width) / 2, 28);
                    btnGuardar.Visible = false;
                    btnGuardarCambios.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Proveedores_Load(object sender, EventArgs e)
        {
            mostrar();
        }

        private void insertar_editado()
        {
            ValidarProveedorInformal();
            ValidarPanelesChecked();
            Logic.Lproveedores models = new Logic.Lproveedores();
            DataAccess.Insertar_datos I = new DataAccess.Insertar_datos();
            models.IdProveedor = IdProveedor;
            models.Nombre = txtnombre.Text;
            models.Celular = txtcelular.Text;
            models.TipoIdentificacion = TipoDeIdentificacion;
            models.Cedula = txtCedula.Text;
            models.Rnc = txtRNC.Text;
            models.Direccion = txtdireccion.Text;
            models.Prov_Informal = _ProveedorInformal;
            if (I.editar_Proveedores(models) == true)
            {
                mostrar();
            }
        }
        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtnombre.Text))
            {
                Rellenar_campos_vacios();
                insertar_editado();
            }
            else
            {
                MessageBox.Show("Ingrese un nombre", "Datos Incompletos:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnVolver_Click(object sender, EventArgs e)
        {
            panelRegistrofondo.Visible = false;
        }
        private void buscar()
        {
            DataTable dt = new DataTable();
            Obtener_datos.buscar_proveedores(ref dt, txtbusca.Text);
            datalistado.DataSource = dt;
            PintarDt();
        }

        private void PintarDt()
        {
            Bases.Multilinea(ref datalistado);
            datalistado.Columns[2].Visible = false;
            foreach (DataGridViewRow row in datalistado.Rows)
            {
                string estado = row.Cells["Estado"].Value.ToString();
                if (estado == "ELIMINADO")
                {
                    row.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Strikeout | FontStyle.Bold);
                    row.DefaultCellStyle.ForeColor = Color.Red;
                }
            }
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtbusca.Clear();
            txtbusca.Focus();
        }

        private void lblBuscarEtiqueta_Click(object sender, EventArgs e)
        {
            txtbusca.Focus();
        }

        private void btnNuevoProveedor_Click(object sender, EventArgs e)
        {
            Nuevo();
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
        private void rbCedula_CheckedChanged(object sender, EventArgs e)
        {
            ValidarPanelesChecked();
        }

        private void rbRnc_CheckedChanged(object sender, EventArgs e)
        {
            ValidarPanelesChecked();
        }
        private void ValidarProveedorInformal()
        {
            if (checkProveedorInformal.Checked == true)
            {
                _ProveedorInformal = "SI";
            }
            else
            {
                _ProveedorInformal = "NO";
            }
        }
        private void checkProveedorInformal_CheckedChanged(object sender, EventArgs e)
        {
            ValidarProveedorInformal();
        }
    }
}
