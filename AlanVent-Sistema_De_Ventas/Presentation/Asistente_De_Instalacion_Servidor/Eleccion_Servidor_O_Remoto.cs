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

namespace AlanVent_Sistema_De_Ventas.Presentation.Asistente_De_Instalacion_Servidor
{
    public partial class Eleccion_Servidor_O_Remoto : Form
    {
        public Eleccion_Servidor_O_Remoto()
        {
            InitializeComponent();
        }
        string Estado_Connexion;
        private void listar()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();
                var Query = "select * from Usuarios";
                SqlDataAdapter data = new SqlDataAdapter(Query, cn);
                data.Fill(dt);
                datalistado.DataSource = dt;
                cn.Close();
                Estado_Connexion = "CONECTADO";
            }
            catch (Exception Ex)
            {
                Estado_Connexion = "-";
               
            }
        }
        private void Eleccion_Servidor_O_Remoto_Load(object sender, EventArgs e)
        {
            PanelEleccion.Location = new Point((Width - PanelEleccion.Width) / 2, (Height - PanelEleccion.Height) / 2);
            listar();
            if (Estado_Connexion == "CONECTADO")
            {
                //Dispose();
                Registro_de_Empresa fr = new Registro_de_Empresa();
                fr.ShowDialog();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            Dispose();
            Instalaccion_Del_Servidor_SQL frm = new Instalaccion_Del_Servidor_SQL();
            frm.ShowDialog();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Dispose();
            Conexion_Remota.Conexion_secundaria frm = new Conexion_Remota.Conexion_secundaria();
            frm.ShowDialog();
        }
    }
}
