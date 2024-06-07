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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.CopiasBd
{
    public partial class CrearCopiaBd : Form
    {
        public CrearCopiaBd()
        {
            InitializeComponent();
        }
        string txtsoftware = "AlanVent";
        string Base_De_datos = "AlanVent_SistemaDeVentas";
        private Thread Hilo;
        private bool acaba = false;
        int contador = 0;
        private void CrearCopiaBd_Load(object sender, EventArgs e)
        {
            Mostrar_empresa();
        }
        private void Mostrar_empresa()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrar_EMPRESA(ref dt);
            foreach (DataRow item in dt.Rows)
            {
                txtRuta.Text = item["Carpeta_para_copias_de_seguridad"].ToString();
                //lblfecha.Text = item["Ultima_Fecha_copia_seguridad"].ToString();
                //lblfrecuencia.Text = item["Frecuencia_de_copia"].ToString();
                //lbldirectorio.Text = "Copia de seguridad guardada en: " + txtRuta.Text + "AlanVent_SistemaDeVentas.bak";

            }

        }
        private void ObtenerRuta()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            ObtenerRuta();
        }

        private void ToolStripButton22_Click(object sender, EventArgs e)
        {
            ObtenerRuta();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            GenerarCopia();
        }
        private void GenerarCopia()
        {
            if (!string.IsNullOrEmpty(txtRuta.Text))
            {
                Hilo = new Thread(new ThreadStart(ejecucion));
                Pcargando.Visible = true;
                Hilo.Start();
                acaba = false;
                timer1.Start();

            }
            else
            {
                MessageBox.Show("Selecciona una Ruta donde Guardar las Copias de Seguridad", "Seleccione Ruta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRuta.Focus();

            }
        }
        private void ejecucion()
        {
            try
            {
                string miCarpeta = "Copias_de_Seguridad_de_" + txtsoftware;
                if (System.IO.Directory.Exists(txtRuta.Text + miCarpeta))
                {

                }
                else
                {
                    System.IO.Directory.CreateDirectory(txtRuta.Text + miCarpeta);
                }
                string ruta_completa = txtRuta.Text + miCarpeta;
                string SubCarpeta = ruta_completa + @"\Respaldo_al_" + DateTime.Now.Day + "_" + (DateTime.Now.Month) + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute;
                try
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.Combine(ruta_completa, SubCarpeta));

                }
                catch (Exception)
                {


                }
                try
                {
                    string v_nombre_respaldo = Base_De_datos + ".bak";
                    DataAccess.ConexionMaestra.abrir();
                    SqlCommand cmd = new SqlCommand("BACKUP DATABASE " + Base_De_datos + " TO DISK = '" + SubCarpeta + @"\" + v_nombre_respaldo + "'", DataAccess.ConexionMaestra.conectar);
                    cmd.ExecuteNonQuery();
                    acaba = true;
                    ConexionMaestra.cerrar();
                }
                catch (Exception ex)
                {
                    acaba = false;
                    ConexionMaestra.cerrar();
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se ha podido realizar el respaldo de seguridad, verifica el correcto funcionamiento de el disco de destino",
                   "Valida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (acaba == true)
            {
                timer1.Stop();
                Pcargando.Visible = false;
                lblDirectorio.Visible = true;
                lblDirectorio.Text = "Copia Guardada en: " + txtRuta.Text + @"\" + "AlanVent_SistemaDeVentas.bak";
                timerTiempoCarga.Enabled = true;
                timerTiempoCarga.Start();
                editarRespaldos();

            }
        }
        private void editarRespaldos()
        {
            Lempresa parametros = new Lempresa();
            Editar_datos funcion = new Editar_datos();
            parametros.Carpeta_para_copias_de_seguridad = txtRuta.Text;
            parametros.Ultima_fecha_de_copia_de_seguridad = DateTime.Now.ToString();
            parametros.Ultima_fecha_de_copia_date = DateTime.Now;
            parametros.Frecuencia_de_copias = 1;
            if (funcion.editarRespaldos(parametros) == true)
            {
                MessageBox.Show("Copia de Base de datos Generada.", "Generacion de Copia de BD", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pbBuscarRuta_Click(object sender, EventArgs e)
        {
            ObtenerRuta();
        }

        private void timerTiempoCarga_Tick(object sender, EventArgs e)
        {
     
           
                contador += 1;
                if (contador == 7)
                {

                    timerTiempoCarga.Stop(); 
                    lblDirectorio.Visible = false;
                }
           
        }
    }
}
