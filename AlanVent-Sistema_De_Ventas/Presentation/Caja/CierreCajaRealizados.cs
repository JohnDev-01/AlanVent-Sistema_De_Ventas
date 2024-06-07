using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;
using AlanVent_Sistema_De_Ventas.Presentation.Reportes.CierreCaja;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.Caja
{
    public partial class CierreCajaRealizados : Form
    {
        public CierreCajaRealizados()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            txtBuscar.Focus();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                lblAnuncio.Visible = true;
            }
            else
            {
                lblAnuncio.Visible = false;
            }
            BuscarCierreCaja();
            reportViewer1.ReportSource = null;
            reportViewer1.RefreshReport();
        }
        private void BuscarCierreCaja()
        {
            var dt = new DataTable();
            Obtener_datos.MostrarCierreCajaRealizados(ref dt, txtBuscar.Text) ;
            dgCierreCaja.DataSource = dt;
            dgCierreCaja.Columns[0].Visible = false;
            Bases.Multilinea(ref dgCierreCaja);
        }
        private void MostrarInforme()
        {
            var dt = new DataTable();
            int idcierreseleccionado = Convert.ToInt32(dgCierreCaja.SelectedCells[0].Value);
            Obtener_datos.ImprimirCierreCajaRealizado(ref dt, idcierreseleccionado);

            var report = new CierreInforme();
            report.DataSource = dt;
            reportViewer1.ReportSource = report;
            reportViewer1.RefreshReport();
        }
        private void dgCierreCaja_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MostrarInforme();
        }

        private void CierreCajaRealizados_Load(object sender, EventArgs e)
        {
            BuscarCierreCaja();
            reportViewer1.ReportSource = null;
            reportViewer1.RefreshReport();
        }
    }
}
