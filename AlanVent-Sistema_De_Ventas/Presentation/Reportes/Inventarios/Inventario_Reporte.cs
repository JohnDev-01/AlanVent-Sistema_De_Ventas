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

namespace AlanVent_Sistema_De_Ventas.Presentation.Reportes.Inventarios
{
    public partial class Inventario_Reporte : Form
    {
        public Inventario_Reporte()
        {
            InitializeComponent();
        }
        public string busqueda = "";
        private void MostrarInventarios(ref DataTable dt)
        {
            try
            {
                ConexionMaestra.abrir();
                var da = new SqlDataAdapter("mostrar_inventarios_todos", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", busqueda);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
            }
            if (dt.Rows.Count == 0)
            {
                Obtener_datos.MostrarNombreEmpresa(ref dt);
            }
        }
        private void CargarReporte()
        {
            DataTable dt = new DataTable(); 
            MostrarInventarios(ref dt);
            var rpt = new rptInventarios();
            rpt.DataSource = dt;
            rpt.table1.DataSource = dt;
            reportViewer1.Report = rpt;
            reportViewer1.RefreshReport();
        }
        private void Inventario_Reporte_Load(object sender, EventArgs e)
        {
            CargarReporte();
        }
    }
}
