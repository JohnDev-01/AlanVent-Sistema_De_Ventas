using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.Asistente_De_Instalacion_Servidor
{
    public partial class Registro_de_Empresa : Form
    {
        public Registro_de_Empresa()
        {
            InitializeComponent();
        }
        public static string Correo;
        string serial_pc;
        bool estadoImagen = false;
        string correo;
        bool estadornc = true;
        string tipoidentificador;
        private void Registro_de_Empresa_Load(object sender, EventArgs e)
        {
            panel2.Location = new Point((Width - panel2.Width) / 2, (Height - panel2.Height) / 2);
            Bases.Obtener_serialPc(ref serial_pc);
            TXTCON_LECTORA.Checked = true;
            txtteclado.Checked = false;
            no.Checked = true;
            Panel11.Visible = false;
            Panel9.Visible = false;

            TSIGUIENTE.Visible = false;
            TSIGUIENTE_Y_GUARDAR.Visible = true;
        }

        private void txtnombreEmpresa_Click(object sender, EventArgs e)
        {
            txtnombreEmpresa.SelectAll();
        }
        private void Insertar_3_Comprobantes_por_defecto()
        {

            try
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();

                SqlCommand com = new SqlCommand("insertar_Serializacion", cn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Serie", "T");
                com.Parameters.AddWithValue("@numeroinicio", 6);
                com.Parameters.AddWithValue("@numerofin", 0);
                com.Parameters.AddWithValue("@Destino", "VENTAS");
                com.Parameters.AddWithValue("@tipodoc", "TICKET");
                com.Parameters.AddWithValue("@Por_defecto", "SI");
                com.ExecuteNonQuery();
                cn.Close();

                cn.Open();
                com = new SqlCommand("insertar_Serializacion", cn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Serie", "B");
                com.Parameters.AddWithValue("@numeroinicio", 6);
                com.Parameters.AddWithValue("@numerofin", 0);
                com.Parameters.AddWithValue("@Destino", "VENTAS");
                com.Parameters.AddWithValue("@tipodoc", "BOLETA");
                com.Parameters.AddWithValue("@Por_defecto", "-");
                com.ExecuteNonQuery();
                cn.Close();

                cn.Open();
                com = new SqlCommand("insertar_Serializacion", cn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Serie", "F");
                com.Parameters.AddWithValue("@numeroinicio", 6);
                com.Parameters.AddWithValue("@numerofin", 0);
                com.Parameters.AddWithValue("@Destino", "VENTAS");
                com.Parameters.AddWithValue("@tipodoc", "FACTURA");
                com.Parameters.AddWithValue("@Por_defecto", "-");
                com.ExecuteNonQuery();
                cn.Close();

                cn.Open();
                com = new SqlCommand("insertar_Serializacion", cn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Serie", "I");
                com.Parameters.AddWithValue("@numeroinicio", 6);
                com.Parameters.AddWithValue("@numerofin", 0);
                com.Parameters.AddWithValue("@Destino", "INGRESO DE COBROS");
                com.Parameters.AddWithValue("@tipodoc", "INGRESO");
                com.Parameters.AddWithValue("@Por_defecto", "-");
                com.ExecuteNonQuery();
                cn.Close();

                cn.Open();
                com = new SqlCommand("insertar_Serializacion", cn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Serie", "I");
                com.Parameters.AddWithValue("@numeroinicio", 6);
                com.Parameters.AddWithValue("@numerofin", 0);
                com.Parameters.AddWithValue("@Destino", "EGRESO DE PAGOS");
                com.Parameters.AddWithValue("@tipodoc", "EGRESO");
                com.Parameters.AddWithValue("@Por_defecto", "-");
                com.ExecuteNonQuery();
                cn.Close();
                //Insertamos el comprobante de Ticket de compra
                cn.Open();
                com = new SqlCommand("insertar_Serializacion", cn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Serie", "TC");
                com.Parameters.AddWithValue("@numeroinicio", 6);
                com.Parameters.AddWithValue("@numerofin", 0);
                com.Parameters.AddWithValue("@Destino", "TICKETC");
                com.Parameters.AddWithValue("@tipodoc", "COMPRAS");
                com.Parameters.AddWithValue("@Por_defecto", "-");
                com.ExecuteNonQuery();
                cn.Close();

                //Serializacion de NCF (dgii), estos incluye todos los ncf correspondientes
                cn.Open();
                com = new SqlCommand("InsertarSerializacionNCF", cn);
                com.CommandType = CommandType.StoredProcedure;
                com.ExecuteNonQuery();
                cn.Close();

                cn.Open();
                com = new SqlCommand("Insertar_FORMATO_TICKET", cn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@Identificador_fiscal", "RUC Identificador Fiscal de la Empresa");
                com.Parameters.AddWithValue("@Direccion", "Calle, Nro, avenida");
                com.Parameters.AddWithValue("@Provincia_Departamento_Pais", "Provincia - Departamento - Pais");
                com.Parameters.AddWithValue("@Nombre_de_Moneda", "Nombre de Moneda");
                com.Parameters.AddWithValue("@Agradecimiento", "Agradecimiento");
                com.Parameters.AddWithValue("@pagina_Web_Facebook", "pagina Web ó Facebook");
                com.Parameters.AddWithValue("@Anuncio", "Anuncio");
                com.Parameters.AddWithValue("@Datos_fiscales_de_autorizacion", "Datos Fiscales - Numero de Autorizacion, Resolucion...");
                com.Parameters.AddWithValue("@Por_defecto", "Ticket No Fiscal");
                com.Parameters.AddWithValue("@LinkRed", "LINK DE RED SOCIAL PARA CODIGO QR");
                com.ExecuteNonQuery();
                cn.Close(); 
                
                
                cn.Open();
                com = new SqlCommand("InsertarFormatoFactura", cn);
                com.CommandType = CommandType.StoredProcedure;
                com.ExecuteNonQuery();
                cn.Close();

                cn.Open();
                com = new SqlCommand("insertarCorreoBase", cn);
                com.CommandType = CommandType.StoredProcedure;
                string correo;
                string pass;
                string estado;
                correo = Bases.Encriptar("alanvent2107@gmail.com");
                pass = Bases.Encriptar("twugguemnkptugxm");
                estado = "Sin confirmar";
                com.Parameters.AddWithValue("@Correo", correo);
                com.Parameters.AddWithValue("@Password", pass);
                com.Parameters.AddWithValue("@Estado_De_envio", estado);
                com.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Ingresar_Caja()
        {

            try
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();

                SqlCommand com = new SqlCommand("Insertar_caja", cn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@descripcion", txtcaja.Text);
                com.Parameters.AddWithValue("@Tema", "Redentor");
                com.Parameters.AddWithValue("@Serial_PC", serial_pc);
                com.Parameters.AddWithValue("@Impresora_Ticket", "Ninguna");
                com.Parameters.AddWithValue("@Impresora_A4", "Ninguna");
                com.Parameters.AddWithValue("@Tipo", "PRINCIPAL");
                com.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception)
            {

            }

        }
        private void Insertar_Empresa()
        {
            
            try
            {



                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();

                SqlCommand com = new SqlCommand("insertar_Empresa", cn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Nombre_Empresa", txtnombreEmpresa.Text);

                //Segunda Opcion Por si no tiene logo
                if (estadoImagen == true)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    ImagenEmpresa.Image.Save(ms, ImagenEmpresa.Image.RawFormat);
                    com.Parameters.AddWithValue("@logo", ms.GetBuffer());
                }
                else
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    pIconoSegundaOpcion.Image.Save(ms, pIconoSegundaOpcion.Image.RawFormat);
                    com.Parameters.AddWithValue("@logo", ms.GetBuffer());
                }
                //-----------------------------------------------------------------
                com.Parameters.AddWithValue("@Impuesto", txtimpuesto.Text);
                com.Parameters.AddWithValue("@Porcentaje_impuesto", txtporcentaje.Text);
                com.Parameters.AddWithValue("@Moneda", txtmoneda.Text);
                com.Parameters.AddWithValue("@Trabajas_con_impuestos", TXTTRABAJASCONIMPUESOS.Text);
                com.Parameters.AddWithValue("@Carpeta_para_copias_de_seguridad", txtRuta.Text);
                com.Parameters.AddWithValue("@Correo_para_envio_de_reportes", correo);
                com.Parameters.AddWithValue("@Ultima_fecha_de_copia_de_seguridad", "NINGUNA");
                com.Parameters.AddWithValue("@Ultima_fecha_de_copia_date", txtfecha.Value);
                com.Parameters.AddWithValue("@Frecuencia_de_copias", 1);
                com.Parameters.AddWithValue("@Estado", "PENDIENTE");
                com.Parameters.AddWithValue("@Tipo_de_empresa", "GENERAL");
                com.Parameters.AddWithValue("@Pais", TXTPAIS.Text);
                com.Parameters.AddWithValue("@Redondeo_de_total", "NO");

                if (TXTCON_LECTORA.Checked == true)
                {
                    com.Parameters.AddWithValue("@Modo_de_busqueda", "LECTORA");
                }
                if (txtteclado.Checked == true)
                {
                    com.Parameters.AddWithValue("@Modo_de_busqueda", "TECLADO");
                }
                com.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                com.Parameters.AddWithValue("@Telefono", txtContacto.Text);
                com.Parameters.AddWithValue("@Rnc", txtrnc.Text);
                com.Parameters.AddWithValue("@tipoIdentificador", tipoidentificador);
                com.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
            }

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
        private void TSIGUIENTE_Y_GUARDAR__Click(object sender, EventArgs e)
        {
            #region Validation
            if (txtnombreEmpresa.Text == "" | txtnombreEmpresa.Text == "NOMBRE DE TU EMPRESA")
            {
                MessageBox.Show("Ingrese un nombre de empresa, por favor completa el campo.", "Campos Vacios:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtRuta.Text == "")
            {
                MessageBox.Show("Por favor seleccione una ruta para guardar las copias de seguridad.", "Campos Vacios:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtDireccion.Text == "")
            {
                MessageBox.Show("Por favor proporciona una direccion de tu negocio", "Campos Vacios:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtContacto.Text == "")
            {
                MessageBox.Show("Por favor proporciona un contacto de tu negocio", "Campos Vacios:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            correo = txtcorreo.Text;
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
                if (validar_Mail(txtcorreo.Text) == false)
                {
                    MessageBox.Show("Dirección de correo electronico no valida, el correo debe tener un formato: nombre@dominio.com, por favor ingrese uno valido", "Dirección Invalida:", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtcorreo.Focus();
                    txtcorreo.SelectAll();
                    return;
                }
            }
            if (no.Checked == true)
            {
                TXTTRABAJASCONIMPUESOS.Text = "NO";
            }
            if (si.Checked == true)
            {
                TXTTRABAJASCONIMPUESOS.Text = "SI";
            }

            
            if (TXTTRABAJASCONIMPUESOS.Text == "NO")
            {
                txtrnc.Text = "-";
                estadornc = true;
            }
            else
            {
                estadornc = ValidarCaracteresIdentificador();
            }
            
            //Insertar Datos 


            if (estadornc == true)
            {
                ValidarTipoDeIdentificador();
                Insertar_Empresa();
                Ingresar_Caja();
                Insertar_3_Comprobantes_por_defecto();
                InsertarCodigoProductoPorDefecto();
                CrearConceptosGastosDefecto();
                Editar_datos.AplicarConfiguracionImpuestosProductos();
                Editar_datos.ModificarEstadoImprimirCierreCaja("SI");
                Correo = txtcorreo.Text;
                this.Hide();
                Presentation.Asistente_De_Instalacion_Servidor.USUARIOS_AUTORIZADOS_AL_SISTEMA frm = new USUARIOS_AUTORIZADOS_AL_SISTEMA();
                frm.ShowDialog();
                this.Dispose();
            }
            

        }
        private void CrearConceptosGastosDefecto()
        {
            Insertar_datos.InsertarConceptosPorDefecto();
        }
        private void InsertarCodigoProductoPorDefecto()
        {
            DataAccess.Insertar_datos.InsertarCodigoProductoPorDefecto();
        }
        private void TXTCON_LECTORA_CheckedChanged(object sender, EventArgs e)
        {
            if (TXTCON_LECTORA.Checked == true)
            {
                txtteclado.Checked = false;
                TXTCON_LECTORA.Checked = true;
            }
        }

        private void txtteclado_CheckedChanged(object sender, EventArgs e)
        {
            if (txtteclado.Checked == true)
            {
                TXTCON_LECTORA.Checked = false;
                txtteclado.Checked = true;
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

        private void btnEditarLogo_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "";
            dlg.Filter = "Imagenes|*.jpg;*.png";
            dlg.FilterIndex = 2;
            dlg.Title = "Cargador de imagenes AlanVent";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ImagenEmpresa.BackgroundImage = null;
                ImagenEmpresa.Image = new Bitmap(dlg.FileName);
                ImagenEmpresa.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void Label9_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = fd.SelectedPath;
                string ruta = txtRuta.Text;
                if (ruta.Contains(@"C:\"))
                {
                    MessageBox.Show("Selecciona un disco diferente al Disco C:", "Ruta Invalida:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRuta.Text = "";

                }
                else
                {
                    txtRuta.Text = fd.SelectedPath;
                }
            }
        }
        public bool validar_Mail(string sMail)
        {
            return Regex.IsMatch(sMail, @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$");

        }
        private void ToolStripButton22_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = fd.SelectedPath;
                string ruta = txtRuta.Text;
                if (ruta.Contains(@"C:\"))
                {
                    MessageBox.Show("Selecciona un disco diferente al Disco C:", "Ruta Invalida:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRuta.Text = "";

                }
                else
                {
                    txtRuta.Text = fd.SelectedPath;
                }
            }
        }

        private void si_CheckedChanged(object sender, EventArgs e)
        {
            if (si.Checked == true)
            {
                Panel11.Visible = true;
            }
        }

        private void no_CheckedChanged(object sender, EventArgs e)
        {
            if (no.Checked == true)
            {
                Panel11.Visible = false;
            }
        }

        private void txtcorreo_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblNombretxt_Click(object sender, EventArgs e)
        {
            txtnombreEmpresa.Focus();
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

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void Label9_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = fd.SelectedPath;
                string ruta = txtRuta.Text;
                if (ruta.Contains(@"C:\"))
                {
                    MessageBox.Show("Selecciona un disco diferente al Disco C:", "Ruta Invalida:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRuta.Text = "";

                }
                else
                {
                    txtRuta.Text = fd.SelectedPath;
                }
            }
        }

        private void ToolStripButton22_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = fd.SelectedPath;
                string ruta = txtRuta.Text;
                if (ruta.Contains(@"C:\"))
                {
                    MessageBox.Show("Selecciona un disco diferente al Disco C:", "Ruta Invalida:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRuta.Text = "";

                }
                else
                {
                    txtRuta.Text = fd.SelectedPath;
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            txtDireccion.Focus();
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

        private void label7_Click(object sender, EventArgs e)
        {
            txtContacto.Focus();
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

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtrnc_TextChanged(object sender, EventArgs e)
        {
            if (txtrnc.Text == "")
            {
                txtrnc.Text = "-";
                lblRnc.Visible = true;
            }
            else
            {
                lblRnc.Visible = false;
            }
        }

        private void lblRnc_Click(object sender, EventArgs e)
        {
            txtrnc.Focus();
        }

        private void checkIdentificador_CheckedChanged(object sender, EventArgs e)
        {
            ValidarTipoDeIdentificador();
        }
    }
}
