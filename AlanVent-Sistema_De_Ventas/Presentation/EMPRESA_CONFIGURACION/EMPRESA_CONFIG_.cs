using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AlanVent_Sistema_De_Ventas.DataAccess;

namespace AlanVent_Sistema_De_Ventas.Presentation.EMPRESA_CONFIG
{
    public partial class EMPRESA_CONFIG_ : Form
    {
        public EMPRESA_CONFIG_()
        {
            InitializeComponent();
        }

        private void EMPRESA_CONFIG__Load(object sender, EventArgs e)
        {
            Mostrar();
            Obtener_Datos();
        }
        string vendes_con_impuesto;
        string tipo_de_busqueda;
        string tipoidentificador;

        private void Obtener_Datos()
        {
            txtnombreEmpresa.Text = datalistado.SelectedCells[2].Value.ToString();
            ImagenEmpresa.BackgroundImage = null;
            byte[] b = (byte[])datalistado.SelectedCells[1].Value;
            MemoryStream ms = new MemoryStream(b);
            ImagenEmpresa.Image = Image.FromStream(ms);
            TXTPAIS.Text = datalistado.SelectedCells[13].Value.ToString();
            txtmoneda.Text = datalistado.SelectedCells[4].Value.ToString();
            vendes_con_impuesto = datalistado.SelectedCells[10].Value.ToString();
            if (vendes_con_impuesto == "NO")
            {
                si.Checked = false;
                no.Checked = true;
                PanelImpuestos.Visible = false;
            }
            if (vendes_con_impuesto == "SI")
            {
                si.Checked = true;
                no.Checked = false;
                PanelImpuestos.Visible = true;
            }
            txtporcentaje.Text = datalistado.SelectedCells[6].Value.ToString();
            txtimpuesto.Text = datalistado.SelectedCells[7].Value.ToString();
            tipo_de_busqueda = datalistado.SelectedCells[8].Value.ToString();
            if (tipo_de_busqueda == "LECTORA")
            {
                TXTCON_LECTORA.Checked = true;
                txtteclado.Checked = false;
            }
            if (tipo_de_busqueda == "TECLADO")
            {
                TXTCON_LECTORA.Checked = false;
                txtteclado.Checked = true;
            }
            txtRuta.Text = datalistado.SelectedCells[12].Value.ToString();
            txtcorreo.Text = datalistado.SelectedCells[11].Value.ToString();
            txtDireccion.Text = datalistado.SelectedCells[14].Value.ToString();
            txtContacto.Text = datalistado.SelectedCells[15].Value.ToString();
            txtrnc.Text = datalistado.SelectedCells[16].Value.ToString();
            var valueTipoidentificador = datalistado.SelectedCells[17].Value.ToString();
            if (valueTipoidentificador == "CEDULA")
            {
                checkIdentificador.Checked = true;
            }
            else
            {
                checkIdentificador.Checked = false;
            }
        }
        private void Mostrar()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();
                da = new SqlDataAdapter("mostrar_Empresa", cn);
                da.Fill(dt);
                datalistado.DataSource = dt;
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool Validar_Mail(string sMail)
        {
            return Regex.IsMatch(sMail, @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$");
        }

        private void TSIGUIENTE_Y_GUARDAR__Click(object sender, EventArgs e)
        {
            ValidarTipoDeIdentificador();
            string correo = txtcorreo.Text;
            if (txtcorreo.Text == "")
            {
                DialogResult r = MessageBox.Show("No registrar un Correo hara que pierdas funcionalidades importantes como envios de reportes de cierre de cajas y algunos seguimientos de tu empresa, ¿Deseas Continuar Asi?", "Confirmación:", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r == DialogResult.Yes)
                {
                    correo = "SIN REGISTRO";
                }
            }
            if (correo != "SIN REGISTRO")
            {
                if (Validar_Mail(txtcorreo.Text) == false)
                {
                    MessageBox.Show("Dirección de correo electronico no valida, el correo debe tener un formato: nombre@dominio.com, por favor ingrese uno valido", "Dirección Invalida:", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtcorreo.Focus();
                    txtcorreo.SelectAll();
                    return;
                }
            }
            if (txtnombreEmpresa.Text != "")
            {
                try
                {
                    if (no.Checked == true)
                    {
                        vendes_con_impuesto = "NO";

                    }
                    else
                    {
                        vendes_con_impuesto = "SI";
                    }

                    bool estadornc = true;
                    if(vendes_con_impuesto == "NO")
                    {
                        txtrnc.Text = "-";
                        estadornc = true;
                    }
                    else
                    {
                        estadornc = ValidarCaracteresIdentificador();
                    }

                    if ( estadornc == true)
                    {
                        SqlConnection con = new SqlConnection();
                        con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                        con.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd = new SqlCommand("editar_Empresa", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre_Empresa", txtnombreEmpresa.Text);
                        cmd.Parameters.AddWithValue("@Impuesto", txtimpuesto.Text);
                        cmd.Parameters.AddWithValue("@Porcentaje_impuesto", Convert.ToDouble(txtporcentaje.Text));
                        cmd.Parameters.AddWithValue("@Moneda", txtmoneda.Text);
                        cmd.Parameters.AddWithValue("@Trabajas_con_impuestos", vendes_con_impuesto);

                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        ImagenEmpresa.Image.Save(ms, ImagenEmpresa.Image.RawFormat);
                        cmd.Parameters.AddWithValue("@logo", ms.GetBuffer());

                        //-----------------------------------------------------------------
                        if (TXTCON_LECTORA.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Modo_de_busqueda", "LECTORA");
                        }
                        if (txtteclado.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Modo_de_busqueda", "TECLADO");

                        }
                        cmd.Parameters.AddWithValue("@Carpeta_para_copias_de_seguridad", txtRuta.Text);
                        cmd.Parameters.AddWithValue("@Correo_para_envio_de_reportes", correo);
                        cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                        cmd.Parameters.AddWithValue("@Telefono", txtContacto.Text);
                        cmd.Parameters.AddWithValue("@Rnc", txtrnc.Text);
                        cmd.Parameters.AddWithValue("@tipoidentificador",tipoidentificador);
                        int r = cmd.ExecuteNonQuery();
                        con.Close();

                        Editar_datos.AplicarConfiguracionImpuestosProductos();
                        MessageBox.Show("Cambios guardados correctamente.", "Guardando Cambios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Dispose();
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }

        }
        private bool ValidarCaracteresIdentificador()
        {
            ValidarTipoDeIdentificador();
            var caracteres = txtrnc.Text.Length;

            //Validate Type of Cedula 
            if (tipoidentificador == "CEDULA")
            {
                if (caracteres == 11)
                    return true;
                else
                {
                    MessageBox.Show("Idenficador de empresa invalido");
                    return false;
                }

            }
            //Validate type of RNC
            else
            {
                if (caracteres == 9)
                    return true;
                else
                {
                    MessageBox.Show("Idenficador de empresa invalido");
                    return false;
                }
            }
        }
        private void si_CheckedChanged(object sender, EventArgs e)
        {
            if (si.Checked == true)
            {
                PanelImpuestos.Visible = true;
                no.Checked = false;
                txtrnc.Clear();
            }
        }

        private void no_CheckedChanged(object sender, EventArgs e)
        {
            if (no.Checked == true)
            {
                PanelImpuestos.Visible = false;
                si.Checked = false;
                txtrnc.Text = "-";
            }
        }

        private void TXTCON_LECTORA_CheckedChanged(object sender, EventArgs e)
        {
            if (TXTCON_LECTORA.Checked == true)
            {
                txtteclado.Checked = false;
            }
            else
            {
                txtteclado.Checked = true;
            }
        }

        private void txtteclado_CheckedChanged(object sender, EventArgs e)
        {
            if (txtteclado.Checked == true)
            {
                TXTCON_LECTORA.Checked = false;
            }
            else
            {
                TXTCON_LECTORA.Checked = true;
            }
        }

        private void TXTPAIS_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtmoneda.SelectedIndex = TXTPAIS.SelectedIndex;
        }

        private void txtmoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            TXTPAIS.SelectedIndex = txtmoneda.SelectedIndex;
        }
        private void Obtener_ruta()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {

                string ruta = folderBrowserDialog1.SelectedPath;
                if (ruta.Contains(@"C:\"))
                {
                    MessageBox.Show("Seleccione un disco diferente al C", "Ruta Invalida", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    txtRuta.Text = folderBrowserDialog1.SelectedPath;
                }
            }
        }
        private void btnEditarLogo_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "";
            dlg.Filter = "Imagenes|*.jpg;*.png";
            dlg.FilterIndex = 2;
            dlg.Title = "Selecciona el logo de tu empresa:";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ImagenEmpresa.BackgroundImage = null;
                ImagenEmpresa.Image = new Bitmap(dlg.FileName);
                ImagenEmpresa.SizeMode = PictureBoxSizeMode.Zoom;

            }
        }

        private void ToolStripButton22_Click(object sender, EventArgs e)
        {
            Obtener_ruta();
        }

        private void Label9_Click(object sender, EventArgs e)
        {
            Obtener_ruta();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void txtnombreEmpresa_TextChanged(object sender, EventArgs e)
        {
            if (txtnombreEmpresa.Text == "")
            {
                lblNombreEmpresa.Visible = false;
                lblNombretxt.Visible = true;
            }
            else
            {
                lblNombreEmpresa.Visible = true;
                lblNombretxt.Visible = false;
            }
        }

        private void lblNombretxt_Click(object sender, EventArgs e)
        {
            txtnombreEmpresa.Focus();
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            txtDireccion.Focus();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            txtContacto.Focus();
        }

        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {
            if (txtDireccion.Text == "")
            {
                label6.Visible = true;
            }
            else
            {
                label6.Visible = false;
            }
        }

        private void txtContacto_TextChanged(object sender, EventArgs e)
        {
            if (txtContacto.Text == "")
            {
                label7.Visible = true;
            }
            else
            {
                label7.Visible = false;
            }
        }

        private void txtrnc_TextChanged(object sender, EventArgs e)
        {
            if (txtrnc.Text == "")
            {
                lblRnc.Visible = true;
            }
            else
            {
                lblRnc.Visible = false;
            }
        }

        private void PanelImpuestos_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblRnc_Click(object sender, EventArgs e)
        {
            txtrnc.Focus();
        }
        private void ValidarTipoDeIdentificador()
        {
            if (checkIdentificador.Checked == true)
            {
                tipoidentificador = "CEDULA";
            }
            else
            {
                tipoidentificador = "RNC";
            }
        }
        private void checkIdentificador_CheckedChanged(object sender, EventArgs e)
        {
            ValidarTipoDeIdentificador();
        }

        private void txtrnc_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
