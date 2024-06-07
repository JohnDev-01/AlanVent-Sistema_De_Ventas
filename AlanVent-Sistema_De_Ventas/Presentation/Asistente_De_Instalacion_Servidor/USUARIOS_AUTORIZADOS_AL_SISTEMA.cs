using AlanVent_Sistema_De_Ventas.Datos;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.Asistente_De_Instalacion_Servidor
{
    public partial class USUARIOS_AUTORIZADOS_AL_SISTEMA : Form
    {
        public USUARIOS_AUTORIZADOS_AL_SISTEMA()
        {
            InitializeComponent();
        }
        string lblIDSERIAL;
        private void Insertar_licencia_de_prueba_30_dias()
        {
            DateTime today = DateTime.Now;
            DateTime fechaFinal = today.AddDays(30);
            txtfechaFinalOK.Text = Convert.ToString(fechaFinal);
            string SERIALpC;
            SERIALpC = lblIDSERIAL;
            
            string FECHA_FINAL;
            FECHA_FINAL = Bases.Encriptar(this.txtfechaFinalOK.Text.Trim());
            string estado;
            estado = Bases.Encriptar("?ACTIVO?");
            string fecha_activacion;
            fecha_activacion = Bases.Encriptar(this.txtfechaInicio.Text.Trim());


            try
            {


                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("Insertar_Marcan", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@s", SERIALpC);
                cmd.Parameters.AddWithValue("@f", FECHA_FINAL);
                cmd.Parameters.AddWithValue("@e", estado);
                cmd.Parameters.AddWithValue("@fa", fecha_activacion);
                cmd.ExecuteNonQuery();
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void insertar_cliente_standar() 
        {
            try
            {


                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("insertar_clientes", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", "GENERICO");
                cmd.Parameters.AddWithValue("@Direccion", 0);
                cmd.Parameters.AddWithValue("@Celular", 0);
                cmd.Parameters.AddWithValue("@Estado", 0);
                cmd.Parameters.AddWithValue("@Saldo", 0);
                cmd.Parameters.AddWithValue("@TipoIdentificacion", "-");
                cmd.Parameters.AddWithValue("@cedula", "-");
                cmd.Parameters.AddWithValue("@rnc", "-");
                cmd.ExecuteNonQuery();
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void insertar_grupo_por_defecto()
        {
            try
            {


                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("Insertar_grupo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@grupo", "GENERAL");
                cmd.Parameters.AddWithValue("@por_defecto", "Si");

                cmd.ExecuteNonQuery();
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void insertar_proveedor_standar()
        {
            var funcion = new Dproveedores();
            var parametros = new Lproveedores();
            parametros.Nombre = "GENERICO";
            parametros.Direccion = "0";
            parametros.Celular = "0";
            parametros.Estado = "0";
            parametros.Saldo = 0;
            parametros.TipoIdentificacion = "-";
            parametros.Cedula = "-";
            parametros.Rnc = "-";
            parametros.Prov_Informal = "SI";
            funcion.insertar_Proveedores(parametros);
        }
        private void insertar_inicio_De_sesion()
        {
            try
            {
                string serialPC;
                serialPC = lblIDSERIAL;

                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("insertar_inicio_seccion", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_serial_pc", serialPC);

                cmd.ExecuteNonQuery();
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error desde Insertar inicio sesion: "+ex.Message);
            }
        }
        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (txtnombre.Text != "" && TXTCONTRASEÑA.Text != "" && TXTUSUARIO.Text != "")
            {
                if (TXTCONTRASEÑA.Text == txtconfirmarcontraseña.Text)
                {
                    string contraseña_encryptada;
                    contraseña_encryptada = Bases.Encriptar(this.TXTCONTRASEÑA.Text.Trim());
                    try
                    {
                        SqlConnection con = new SqlConnection();
                        con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                        con.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd = new SqlCommand("CrearUsuarios", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombreYApellido", txtnombre.Text);
                        cmd.Parameters.AddWithValue("@login", TXTUSUARIO.Text);
                        cmd.Parameters.AddWithValue("@contra", contraseña_encryptada);

                        cmd.Parameters.AddWithValue("@Correo", Presentation.Asistente_De_Instalacion_Servidor.Registro_de_Empresa.Correo);
                        cmd.Parameters.AddWithValue("@Rol", "Administrador (Control total)");
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        PictureBox2.Image.Save(ms, PictureBox2.Image.RawFormat);


                        cmd.Parameters.AddWithValue("@Icono", ms.GetBuffer());
                        cmd.Parameters.AddWithValue("@NombreIcono", "AlanVent");
                        cmd.ExecuteNonQuery();
                        con.Close();

                        
                        Insertar_licencia_de_prueba_30_dias();
                        insertar_cliente_standar();
                        insertar_grupo_por_defecto();
                        insertar_proveedor_standar();
                        insertar_inicio_De_sesion();


                        MessageBox.Show("!LISTO! RECUERDA que para Iniciar Sesión tu Usuario es: " + TXTUSUARIO.Text + " y tu Contraseña es: " + TXTCONTRASEÑA.Text, "Registro Exitoso", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                        Dispose();
                        frmInicioSeccion frm = new frmInicioSeccion();
                        frm.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Las contraseñas no Coinciden", "Contraseñas Incompatibles", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                }
            }
            else
            {
                MessageBox.Show("Falta ingresar Datos", "Datos incompletos", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
        }

        private void USUARIOS_AUTORIZADOS_AL_SISTEMA_Load(object sender, EventArgs e)
        {
            try
            {
                Panel2.Location = new Point((Width - Panel2.Width) / 2, (Height - Panel2.Height) / 2);

                Logic.Bases.Obtener_serialPc(ref lblIDSERIAL);
            }
            catch (Exception)
            {


            }
        }

        private void TXTCONTRASEÑA_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
