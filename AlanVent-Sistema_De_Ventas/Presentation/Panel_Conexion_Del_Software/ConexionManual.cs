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

namespace AlanVent_Sistema_De_Ventas.Panel_Conexion_Del_Software
{
    public partial class ConexionManual : Form
    {
        public ConexionManual()
        {
            InitializeComponent();
        }
        private DataAccess.AES aes = new DataAccess.AES();
        
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
        string dbcnString;
        public void ReadfromXML()
        {

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("ConnectionString.xml");
                XmlElement root = doc.DocumentElement;
                dbcnString = root.Attributes[0].Value;
                txtCnString.Text = (aes.Decrypt(dbcnString, DataAccess.Desencryptacion.appPwdUnique, int.Parse("256")));

            }
            catch (System.Security.Cryptography.CryptographicException ex)
            {

            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            mostrar();

        }
        private void mostrar()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("select * from Usuarios", con);

                da.Fill(dt);
                datalistado.DataSource = dt;
                con.Close();
                string conexion = "server = localhost; database=AlanVent_SistemaDeVentas; Integrated Security = true";

                SavetoXML(aes.Encrypt(conexion, DataAccess.Desencryptacion.appPwdUnique, int.Parse("256")));



                MessageBox.Show("Conexion realizada correctamente", "Conexion:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Application.Exit();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Conexion fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //MessageBox.Show("Sin conexion a la Base de datos", "Conexion fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void ConexionManual_Load(object sender, EventArgs e)
        {
            ReadfromXML();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            mostrar();
        }
    }
}
