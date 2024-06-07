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
    public partial class FormMovimientoBuscar : Form
    {
        public FormMovimientoBuscar()
        {
            InitializeComponent();
        }
        Reportes.Reportes_de_kardex.Reporte_de_inventarios.ReportMovimientosBuscar_ rpt = new ReportMovimientosBuscar_();
        private void MostrarReporte()
        {
            try
            {
                DataTable dta = new DataTable();
                SqlConnection Cn = new SqlConnection();
                Cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                Cn.Open();

                SqlDataAdapter da = new SqlDataAdapter("Buscar_Movimientos_De_Kardex", Cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idProducto", Inventarios_menu.IdProducto);
                da.Fill(dta);

                rpt = new ReportMovimientosBuscar_();
                rpt.DataSource = dta;
                reportViewer1.Report = rpt;
                reportViewer1.RefreshReport();
                Cn.Close();
            }
            catch (Exception)
            {

             
            }
        }
        private void FormMovimientoBuscar_Load(object sender, EventArgs e)
        {
            MostrarReporte();
        }
    }
}
