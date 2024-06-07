using AlanVent_Sistema_De_Ventas.DataAccess;
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
using System.Xml;

namespace AlanVent_Sistema_De_Ventas.Presentation.Conexion_Remota
{
    public partial class Conexion_secundaria : Form
    {
        public Conexion_secundaria()
        {
            InitializeComponent();
        }
        string cadena_de_conexion;
        int id;
        string indicador_de_conexion;
        private AES aes = new AES();
        int idcaja = 0;
        string serialPC;
        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIp.Text))
            {
                conectar_manualmente();
            }
            else
            {
                MessageBox.Show("Ingrese la IP", "Conexion", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void obtenerIdCaja()
        {
            try
            {
                Logic.Bases.Obtener_serialPc(ref serialPC);
                SqlConnection conexionExpress = new SqlConnection(cadena_de_conexion);
                conexionExpress.Open();
                SqlCommand com = new SqlCommand("mostrar_cajas_por_Serial_de_DiscoDuro", conexionExpress);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Serial", serialPC);
                idcaja = Convert.ToInt32(com.ExecuteScalar());
                conexionExpress.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        private void conectar_manualmente()
        {
            string IP = txtIp.Text;
            cadena_de_conexion = "Data Source =" + IP + ";Initial Catalog=AlanVent_SistemaDeVentas;Integrated Security=False;User Id=CambiaremosMundo;Password=alanventexitoso";
           
            comprobar_conexion();
            if (indicador_de_conexion == "HAY CONEXION")
            {
                SavetoXML(aes.Encrypt(cadena_de_conexion, Desencryptacion.appPwdUnique, int.Parse("256")));
                obtenerIdCaja();
                if (idcaja > 0)
                {
                   MessageBox.Show("Conexion Correcta. Vuelve a Abrir el Sistema", "Conexion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    Dispose();
                    Application.Exit();
                    Application.ExitThread();
                }
                else
                {
                    Caja_Secundaria.lblconexion = cadena_de_conexion;
                    Dispose();
                    Caja_Secundaria frm = new Caja_Secundaria();
                    frm.ShowDialog();
                }
            }
            
        }
        public void SavetoXML(object dbcnString)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("ConnectionString.xml");
            XmlElement root = doc.DocumentElement;
            root.Attributes[0].Value = Convert.ToString(dbcnString);
            XmlTextWriter writer = new XmlTextWriter("ConnectionString.xml", null);
            writer.Formatting = Formatting.Indented;
            doc.Save(writer);
            writer.Close();
        }
        private void comprobar_conexion()
        {
            try
            {
                SqlConnection conexionManual = new SqlConnection(cadena_de_conexion);
                conexionManual.Open();
                SqlCommand da = new SqlCommand("select ID from Usuarios", conexionManual);
                id = Convert.ToInt32(da.ExecuteScalar());
                indicador_de_conexion = "HAY CONEXION";
            }
            catch (Exception ex )
            {
                indicador_de_conexion = "NO HAY CONEXION";
                MessageBox.Show(ex.Message);
            }
        }

        private void Conexion_secundaria_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
