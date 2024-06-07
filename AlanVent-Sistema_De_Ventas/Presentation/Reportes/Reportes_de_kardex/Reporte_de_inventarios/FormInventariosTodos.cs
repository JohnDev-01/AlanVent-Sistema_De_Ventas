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

namespace AlanVent_Sistema_De_Ventas.Presentation.Reportes.Reportes_de_kardex.Reporte_de_inventarios
{
    public partial class FormInventariosTodos : Form
    {
        public FormInventariosTodos()
        {
            InitializeComponent();
        }
        Reporte_inventarios_todos ReporteInventarioTods = new Reporte_inventarios_todos();
        private void mostrar()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("imprimir_inventarios_todos", con);
                da.Fill(dt);
                con.Close();
                ReporteInventarioTods = new Reporte_inventarios_todos();
                ReporteInventarioTods.DataSource = dt;
                ReporteInventarioTods.table1.DataSource = dt;
                reportViewer1.Report = ReporteInventarioTods;
                reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            //Bases.Multilinea(ref datalistado);

        }
        private void FormInventariosTodos_Load(object sender, EventArgs e)
        {
            mostrar();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            mostrar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Reporte_inventarios_todos ReporteInventarioTods = new Reporte_inventarios_todos();
            reportViewer1.Report= ReporteInventarioTods;
            reportViewer1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }
    }
}
