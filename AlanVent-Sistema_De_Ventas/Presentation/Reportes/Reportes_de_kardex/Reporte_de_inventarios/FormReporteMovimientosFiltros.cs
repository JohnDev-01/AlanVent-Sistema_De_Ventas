using AlanVent_Sistema_De_Ventas.Presentation.Inventarios_Kardex;
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
    public partial class FormReporteMovimientosFiltros : Form
    {
        public FormReporteMovimientosFiltros()
        {
            InitializeComponent();
        }
        Reportes.Reportes_de_kardex.Reporte_de_inventarios.Reporte_Movimientos_Filtro rpt = new Reporte_Movimientos_Filtro();
        private void MostrarReporte()
        {
            try
            {
                DataTable dta = new DataTable();
                SqlConnection Cn = new SqlConnection();
                Cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                Cn.Open();

                SqlDataAdapter da = new SqlDataAdapter("buscar_MOVIMIENTOS_DE_KARDEX_filtros", Cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fecha", Inventarios_menu.Fecha);
                da.SelectCommand.Parameters.AddWithValue("@tipo", Inventarios_menu.TipoMovimiento);
                da.SelectCommand.Parameters.AddWithValue("@Id_usuario", Inventarios_menu.Id_Usuario);
                da.Fill(dta);

                rpt = new Reporte_Movimientos_Filtro();
                rpt.DataSource = dta;
                rpt.table1.DataSource = dta;
                reportViewer1.Report = rpt;
                reportViewer1.RefreshReport();
                Cn.Close();
            }
            catch (Exception)
            {


            }
        }
        private void FormReporteMovimientosFiltros_Load(object sender, EventArgs e)
        {
            MostrarReporte();
        }
    }
}
