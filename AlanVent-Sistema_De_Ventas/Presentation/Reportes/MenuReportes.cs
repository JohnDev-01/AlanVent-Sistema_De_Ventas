using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.Reportes
{
    public partial class MenuReportes : Form
    {
        public MenuReportes()
        {
            InitializeComponent();
        }
        int idusuario;
        string NombreEmpresa = "";
        private void MenuReportes_Load(object sender, EventArgs e)
        {
            PanelBienvenida.Visible = true;
            PanelBienvenida.Dock = DockStyle.Fill;
            Ocultarpanelesb();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            panelVentas.Visible = true;
            panelVentas.Dock = DockStyle.Fill;
            PanelBienvenida.Visible = false;
            PanelProductos.Visible = false;
            panelCuentasPagar.Visible = false;
            PanelPorCobrar.Visible = false;
            panelcompras.Visible = false;

            //--------------Paneles
            panelArriba.Enabled = false;
            PanelEmpleado.Visible = false;
            //Botones
            Ocultarpanelesb();
            btnVentas.ForeColor = Color.Gold;
            PC4.Visible = true;
            //Controles internos
            chekFiltros.Checked = false;
            PanelFiltros.Visible = false;
            btnResumenVentas_Click(sender, e);
        }

        private void btnResumenVentas_Click(object sender, EventArgs e)
        {
            panelArriba.Enabled = true;
            btnResumenVentas.ForeColor = Color.FromArgb(0, 192, 0);
            btnEmpleado.ForeColor = Color.DimGray;
            PResumenVentas.Visible = true;
            PVentasPorempleado.Visible = false;
            btnHoy.ForeColor = Color.FromArgb(0, 192, 0);
            PanelEmpleado.Visible = false;
            chekFiltros.Checked = false;
            PanelFiltros.Visible = false;
            chekFiltros.ForeColor = Color.DimGray;
            panelArriba.Height = 75;

            ReporteResumenVentasHoy();
        }
        private void ReporteResumenVentasHoy()
        {


            DataTable dt = new DataTable();
            Obtener_datos.BuscarVentasTodaReporte(ref dt);
            
            ReportesVentas.ResumenVentas rpt = new ReportesVentas.ResumenVentas();
            rpt.DataSource = dt;
            rpt.table1.DataSource = dt;
            rpt.DataSource = dt;
            reportViewer1.Report = rpt;
            reportViewer1.RefreshReport();
        }
       
        private void btnHoy_Click(object sender, EventArgs e)
        {
            if (PResumenVentas.Visible == true)
            {
                ReporteResumenVentasHoy();
            }
            if (PVentasPorempleado.Visible == true)
            {
                ReporteResumenVentasHoyEmpleado();

            }
            btnHoy.ForeColor = Color.FromArgb(0, 192, 0);
            chekFiltros.Checked = false;
            PanelFiltros.Visible = false;
            chekFiltros.ForeColor = Color.DimGray;
        }

        private void chekFiltros_Click(object sender, EventArgs e)
        {
            if (chekFiltros.Checked == true)
            {
                if (PResumenVentas.Visible == true)
                {
                    ReporteResumenVentasFechas();
                }
                if (PVentasPorempleado.Visible == true)
                {
                    ReporteResumenVentasEmpleadoFechas();
                }
                btnHoy.ForeColor = Color.DimGray;
                PanelFiltros.Visible = true;
                TXTFI.Value = DateTime.Today;
                TXTFF.Value = DateTime.Today;
                chekFiltros.ForeColor = Color.FromArgb(0, 192, 0);
            }
            else
            {
                if (PResumenVentas.Visible == true)
                {
                    ReporteResumenVentasHoy();
                }
                if (PVentasPorempleado.Visible == true)
                {
                    ReporteResumenVentasHoyEmpleado();
                }
                btnHoy.ForeColor = Color.FromArgb(0, 192, 0);
                PanelFiltros.Visible = false;
                chekFiltros.ForeColor = Color.DimGray;

            }
        }

        private void ReporteResumenVentasFechas()
        {
            DataTable dt = new DataTable();
            
            Obtener_datos.BuscarVentasPorFechasConsultadas(ref dt, TXTFI.Value.ToString("dd/MM/yyyy"), TXTFF.Value.ToString("dd/MM/yyyy"));
            
            ReportesVentas.Ventas_Desde_Hasta rpt = new ReportesVentas.Ventas_Desde_Hasta();
            rpt.table1.DataSource = dt;
            rpt.DataSource = dt;
            reportViewer1.Report = rpt;
            reportViewer1.RefreshReport();

        }

        private void btnEmpleado_Click(object sender, EventArgs e)
        {
            panelArriba.Enabled = true;
            btnResumenVentas.ForeColor = Color.DimGray;
            btnEmpleado.ForeColor = Color.FromArgb(0, 192, 0);
            PResumenVentas.Visible = false;
            PVentasPorempleado.Visible = true;
            btnHoy.ForeColor = Color.FromArgb(0, 192, 0);

            chekFiltros.Checked = false;
            PanelFiltros.Visible = false;
            chekFiltros.ForeColor = Color.DimGray;
            PanelEmpleado.Visible = true;
            panelArriba.Height = 126;
            mostrarUsuarios();
            ReporteResumenVentasHoyEmpleado();
        }
        private void ReporteResumenVentasHoyEmpleado()
        {

            DataTable dt = new DataTable();
            

            Obtener_datos.BuscarVentasPorEmpleados(ref dt, idusuario);
          
            ReportesVentas.VentasEmpleado rpt = new ReportesVentas.VentasEmpleado();
            rpt.table1.DataSource = dt;
            rpt.DataSource = dt;
            reportViewer1.Report = rpt;
            reportViewer1.RefreshReport();
        }
        private void ReporteResumenVentasEmpleadoFechas()
        {
            DataTable dt = new DataTable();


            Obtener_datos.MostrarReporteVentasFechasEmpleados(ref dt, idusuario, TXTFI.Value.ToString("dd/MM/yyyy"), TXTFF.Value.ToString("dd/MM/yyyy"));
            var rpt = new ReportesVentas.Ventas_Empleados_Fechas();
            rpt.table1.DataSource = dt;
            
            rpt.DataSource = dt;
            reportViewer1.Report = rpt;
            reportViewer1.RefreshReport();
        }
        private void mostrarUsuarios()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrarUsuarios(ref dt);
            txtEmpleado.DisplayMember = "NombreYApellido";
            txtEmpleado.ValueMember = "ID";
            txtEmpleado.DataSource = dt;

        }

        private void txtEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            idusuario = Convert.ToInt32(txtEmpleado.SelectedValue);
            if (chekFiltros.Checked == true)
            {
                ReporteResumenVentasEmpleadoFechas();
            }
            else

            {
                ReporteResumenVentasHoyEmpleado();
            }
        }

        private void TXTFF_ValueChanged(object sender, EventArgs e)
        {
            validarFiltros();
        }

        private void TXTFI_ValueChanged(object sender, EventArgs e)
        {
            validarFiltros();
        }
        private void validarFiltros()
        {
            if (chekFiltros.Checked == true)
            {
                if (PResumenVentas.Visible == true)
                {
                    ReporteResumenVentasFechas();
                }
                if (PVentasPorempleado.Visible == true)
                {
                    ReporteResumenVentasEmpleadoFechas();
                }
            }
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            panelVentas.Visible = false;
            PanelBienvenida.Visible = false;
            PanelProductos.Visible = false;
            panelGastos.Visible = false;
            panelIngresos.Visible = false;
            panelCuentasPagar.Visible = false;
            PanelPorCobrar.Visible = true;
            PanelPorCobrar.Dock = DockStyle.Fill;
            PanelPorCobrar.BringToFront();
            panelcompras.Visible = false;
            //Botones
            Ocultarpanelesb();
            btnCobrar.ForeColor = Color.Gold;
            PC2.Visible = true;
            ReporteCuestasPorCobrar();
        }
        private void ReporteCuestasPorCobrar()
        {
            DataTable dt = new DataTable();
            Obtener_datos.ReporteCuestasPorCobrar(ref dt);
            ReportePorCobrar.ReporteCobrar rpt = new ReportePorCobrar.ReporteCobrar();
            rpt.Table1.DataSource = dt;
            rpt.DataSource = dt;
            this.reportViewer2.Report = rpt;
            this.reportViewer2.RefreshReport();

        }

        private void BtnPagar_Click(object sender, EventArgs e)
        {
            panelGastos.Visible = false;
            panelIngresos.Visible = false;
            panelVentas.Visible = false;
            PanelBienvenida.Visible = false;
            PanelProductos.Visible = false;
            PanelPorCobrar.Visible = false;
            panelcompras.Visible = false;
            panelCuentasPagar.Visible = true;
            panelCuentasPagar.Dock = DockStyle.Fill;
            panelCuentasPagar.BringToFront();
            //Botones
            Ocultarpanelesb();
            BtnPagar.ForeColor = Color.Gold;
            PC1.Visible = true;
            ReporteCuentasPorPagar();
        }
        private void Ocultarpanelesb()
        {
            //paneles de colores
            PC1.Visible = false;
            PC2.Visible = false;
            PC3.Visible = false;
            PC4.Visible = false;
            PC5.Visible = false;
            pIngresos.Visible = false;
            pGastos.Visible = false;

            //Botones cambio de color
            BtnPagar.ForeColor = Color.FromArgb(23, 23, 23);
            btnCobrar.ForeColor = Color.FromArgb(23, 23, 23);
            BtnProductos.ForeColor = Color.FromArgb(23, 23, 23);
            btnVentas.ForeColor = Color.FromArgb(23, 23, 23);
            btnCompras.ForeColor = Color.FromArgb(23, 23, 23);
            btnIngresos.ForeColor = Color.FromArgb(23, 23, 23);
            btnGastos.ForeColor = Color.FromArgb(23, 23, 23);
        }


        private void ReporteCuentasPorPagar()
        {

            DataTable dt = new DataTable();
            Obtener_datos.ReporteCuestasPorPagar(ref dt);
            var rpt = new ReportePorPagar.ReportePagar();
            rpt.table1.DataSource = dt;
            rpt.DataSource = dt;
            reportViewer5.ReportSource = rpt;
            reportViewer5.RefreshReport();

        }
        private void ReportePagosRealizados()
        {
            int idproveedor = Convert.ToInt32(cbListaProveedores.SelectedValue);

            DataTable dt = new DataTable();
            Obtener_datos.PagosRealizadorProveedor(ref dt, idproveedor);
            var rpt = new ReportePorPagar.PagosPorProveedor();
            rpt.table1.DataSource = dt;
            rpt.DataSource = dt;
            reportViewer5.ReportSource = rpt;
            reportViewer5.RefreshReport();

        }

        private void BtnProductos_Click(object sender, EventArgs e)
        {
            panelVentas.Visible = false;
            PanelBienvenida.Visible = false;
            PanelProductos.Visible = true;
            PanelProductos.Dock = DockStyle.Fill;
            PanelPorCobrar.Visible = false;
            panelcompras.Visible = false;
            panelCuentasPagar.Visible = false;
            panelGastos.Visible = false;
            panelIngresos.Visible = false;
            //Botones
            Ocultarpanelesb();
            BtnProductos.ForeColor = Color.Gold;
            PC3.Visible = true;
            //Paneles
            PInventarios.Visible = false;
            Pvencidos.Visible = false;
            PStockBajo.Visible = false;
            ReportViewer3.Visible = false;
        }

        private void btnInventarios_Click(object sender, EventArgs e)
        {
            PInventarios.Visible = true;
            PStockBajo.Visible = false;
            Pvencidos.Visible = false;
            ReportViewer3.Visible = true;
            imprimir_inventarios_todos();
        }
        private void imprimir_inventarios_todos()
        {
            DataTable dt = new DataTable();
            Obtener_datos.imprimir_inventarios_todos(ref dt);
            Reportes_de_kardex.Reporte_de_inventarios.Reporte_inventarios_todos rpt = new Reportes_de_kardex.Reporte_de_inventarios.Reporte_inventarios_todos();
            rpt.table1.DataSource = dt;
            rpt.DataSource = dt;
            ReportViewer3.Report = rpt;
            ReportViewer3.RefreshReport();
        }

        private void btnPvencidos_Click(object sender, EventArgs e)
        {
            PInventarios.Visible = false;
            PStockBajo.Visible = false;
            Pvencidos.Visible = true;
            ReportViewer3.Visible = true;
            mostrar_productos_vencidos();
        }
        private void mostrar_productos_vencidos()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrar_productos_vencidos(ref dt);
            Reportes_de_kardex.Reporte_de_inventarios.ReportePVencidos rpt = new Reportes_de_kardex.Reporte_de_inventarios.ReportePVencidos();
            rpt.table1.DataSource = dt;
            rpt.DataSource = dt;
            ReportViewer3.Report = rpt;
            ReportViewer3.RefreshReport();
        }

        private void btnStockBajo_Click(object sender, EventArgs e)
        {
            PInventarios.Visible = false;
            PStockBajo.Visible = true;
            Pvencidos.Visible = false;
            ReportViewer3.Visible = true;
            mostrarProdctosMinimo();
        }

        private void mostrarProdctosMinimo()
        {
            DataTable dt = new DataTable();
            string NombreEmpresa = "";
            Obtener_datos.MOSTRAR_Inventarios_bajo_minimo(ref dt, ref NombreEmpresa);
            var dataNameEmpresa = new DataTable();
            dataNameEmpresa.Columns.Add("Nombre_Empresa");
            dataNameEmpresa.Rows.Add(NombreEmpresa);
            Reportes_de_kardex.Reporte_de_inventarios.ReportePbajomin rpt = new Reportes_de_kardex.Reporte_de_inventarios.ReportePbajomin();
            rpt.table1.DataSource = dt;
            rpt.DataSource = dt;
            rpt.DataSource = dataNameEmpresa;
            ReportViewer3.Report = rpt;
            ReportViewer3.RefreshReport();

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TFILTROS_Click(object sender, EventArgs e)
        {

        }

        private void chekFiltros_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
            panelVentas.Visible = false;
            panelcompras.Visible = true;
            panelcompras.Dock = DockStyle.Fill;
            PanelBienvenida.Visible = false;
            PanelProductos.Visible = false;
            panelCuentasPagar.Visible = false;
            PanelPorCobrar.Visible = false;
            panelGastos.Visible = false;
            panelIngresos.Visible = false;
            //Botones 
            Ocultarpanelesb();
            btnCompras.ForeColor = Color.Gold;
            PC5.Visible = true;
            //Controles Internos del panel
            btnActualidadCompras_Click(sender, e);
            checkFiltrosCompras.Enabled = true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            MostrarReporteComprasFechas();

        }

        private void btnActualidadCompras_Click(object sender, EventArgs e)
        {
            btnActualidadCompras.ForeColor = Color.FromArgb(0, 192, 0);
            checkFiltrosCompras.ForeColor = Color.DimGray;
            checkFiltrosCompras.Checked = false;
            panelFechaCompras.Visible = false;
            MostrarComprasActualidad();
        }
        private void MostrarComprasActualidad()
        {
            var dt = new DataTable();
            var dtSuma = new DataTable();
            Obtener_datos.ReporteComprasRealizadas(ref dt);
            SumarCamposReporteCOMPRAS(ref dt, ref dtSuma);
            var rpt = new ReporteCompras.ComprasActualidad();
            rpt.DataSource = dt;
            rpt.table1.DataSource = dt;
            rpt.table2.DataSource = dtSuma;
            reportViewer4.Report = rpt;
            reportViewer4.RefreshReport();
        }
        private void SumarCamposReporteCOMPRAS(ref DataTable dtBase, ref DataTable dtSumas)
        {
            double TProductos = 0;
            double TINVERTIDO = 0;
            foreach (DataRow Element in dtBase.Rows)
            {
                try
                {
                    TProductos += Convert.ToDouble(Element["Cant"].ToString());
                    TINVERTIDO += Convert.ToDouble(Element["Invertido"].ToString());
                }
                catch (Exception ex)
                {
                }

            }
            dtSumas.Columns.Add("Suma_invertido");
            dtSumas.Columns.Add("Suma_pr");
            DataRow row = dtSumas.NewRow();
            row["Suma_pr"] = Bases.AsignarComa(TProductos);
            row["Suma_invertido"] = Bases.AsignarComa(TINVERTIDO);
            dtSumas.Rows.Add(row);
        }
        private void dtDesdeCompras_ValueChanged(object sender, EventArgs e)
        {
            MostrarReporteComprasFechas();
        }
        private void MostrarReporteComprasFechas()
        {

            var dt = new DataTable();
            var dtSuma = new DataTable();
            Obtener_datos.ReporteComprasRealizadasFechas(ref dt, dtDesdeCompras.Value.ToString("dd/MM/yyyy"), dtHastaCompras.Value.ToString("dd/MM/yyyy"));
            SumarCamposReporteCOMPRAS(ref dt, ref dtSuma);
            var rpt = new ReporteCompras.ComprasFecha();
            rpt.DataSource = dt;
            rpt.table1.DataSource = dt;
            rpt.table2.DataSource = dtSuma;
            reportViewer4.Report = rpt;
            reportViewer4.RefreshReport();
        }
        private void checkFiltrosCompras_CheckedChanged(object sender, EventArgs e)
        {
            if (checkFiltrosCompras.Checked == true)
            {
                btnActualidadCompras.ForeColor = Color.DimGray;
                checkFiltrosCompras.ForeColor = Color.FromArgb(0, 192, 0);
                panelFechaCompras.Visible = true;
                dtDesdeCompras.Value = DateTime.Now;
                dtHastaCompras.Value = DateTime.Now;
                MostrarReporteComprasFechas();
            }
            else
            {
                btnActualidadCompras_Click(sender, e);

            }
        }

        private void btnIngresos_Click(object sender, EventArgs e)
        {
            panelVentas.Visible = false;
            panelcompras.Visible = false;
            panelGastos.Visible = false;
            panelIngresos.Visible = true;
            panelIngresos.Dock = DockStyle.Fill;
            PanelBienvenida.Visible = false;
            PanelProductos.Visible = false;
            panelCuentasPagar.Visible = false;
            PanelPorCobrar.Visible = false;

            //Botones 
            Ocultarpanelesb();
            btnIngresos.ForeColor = Color.Gold;
            pIngresos.Visible = true;
            //Controles Internos del panel
            MostrarReporteIngresos();
        }

        private void btnGastos_Click(object sender, EventArgs e)
        {
            OrganizarPanelesGastos();
            MostrarGastosTodos();

        }
        private void OrganizarPanelesGastos()
        {
            panelGastos.Visible = true;
            panelGastos.Dock = DockStyle.Fill;
            panelVentas.Visible = false;
            panelcompras.Visible = false;
            panelIngresos.Visible = false;
            PanelBienvenida.Visible = false;
            panelCuentasPagar.Visible = false;
            PanelProductos.Visible = false;
            PanelPorCobrar.Visible = false;

            //Botones 
            Ocultarpanelesb();
            btnGastos.ForeColor = Color.Gold;
            pGastos.Visible = true;
            //Controles Internos del panel
        }
        private void OrganizarControlsIngresosNormal()
        {
            panelDateTime.Visible = false;
            btnTodos.ForeColor = Color.Gold;
            dtFIingresos.Value = DateTime.Now;
            dtffIngresos.Value = DateTime.Now;
        }
        private void MostrarReporteIngresos()
        {
            OrganizarControlsIngresosNormal();
            var dt = new DataTable();
            Obtener_datos.ReporteIngresos(ref dt);
            var rpt = new ReporteIngresos.rptIngresos();
            rpt.DataSource = dt;
            rpt.table1.DataSource = dt;
            reportviewIngreso.ReportSource = rpt;
            reportviewIngreso.RefreshReport();
        }
        private void MostrarReporteIngresosFecha()
        {
            var dt = new DataTable();
            Obtener_datos.ReporteIngresosFecha(ref dt, dtFIingresos.Value, dtffIngresos.Value);
            var rpt = new ReporteIngresos.rptIngresosFecha();
            rpt.DataSource = dt;
            rpt.table1.DataSource = dt;
            reportviewIngreso.ReportSource = rpt;
            reportviewIngreso.RefreshReport();
        }
        private void btnTodos_Click(object sender, EventArgs e)
        {
            MostrarReporteIngresos();
        }

        private void checkFechaIngresos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkFechaIngresos.Checked == true)
            {
                MostrarReporteIngresosFecha();
                panelDateTime.Visible = true;
                btnTodos.ForeColor = Color.DimGray;
            }
            else
            {
                MostrarReporteIngresos();
                panelDateTime.Visible = false;
                btnTodos.ForeColor = Color.Gold;
            }
        }

        private void dtFIingresos_ValueChanged(object sender, EventArgs e)
        {
            MostrarReporteIngresosFecha();
        }

        private void dtffIngresos_ValueChanged(object sender, EventArgs e)
        {
            MostrarReporteIngresosFecha();
        }
        private void MostrarGastosTodos()
        {
            var dt = new DataTable();
            Obtener_datos.ReporteGastos(ref dt);
            var rpt = new Reportes.ReporteGastos.rptGastos();
            rpt.DataSource = dt;
            rpt.table1.DataSource = dt;
            reportGastos.ReportSource = rpt;
            reportGastos.RefreshReport();

            btnActualidadGastos.ForeColor = Color.Gold;
            checkFiltrosGastos.ForeColor = Color.FromArgb(65, 65, 65);
            panelFechasGastos.Visible = false;

        }
        private void btnActualidadGastos_Click(object sender, EventArgs e)
        {
            MostrarGastosTodos();

        }
        private void MostrarReporteGastosFecha()
        {
            var dt = new DataTable();
            Obtener_datos.ReporteGastosFecha(ref dt, dtFIgastos.Value, dtFFgastos.Value);
            var rpt = new Reportes.ReporteGastos.rptGastosFecha();
            rpt.DataSource = dt;
            rpt.table1.DataSource = dt;
            reportGastos.ReportSource = rpt;
            reportGastos.RefreshReport();

            btnActualidadGastos.ForeColor = Color.FromArgb(65, 65, 65);
            checkFiltrosGastos.ForeColor = Color.Gold;
            panelFechasGastos.Visible = true;
        }
        private void checkFiltrosGastos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkFiltrosGastos.Checked == true)
            {
                panelFechasGastos.Visible = true;
                btnActualidadGastos.ForeColor = Color.FromArgb(65, 65, 65);
                checkFiltrosGastos.ForeColor = Color.Gold;
                dtFIgastos.Value = DateTime.Now;
                dtFFgastos.Value = DateTime.Now;
                MostrarReporteGastosFecha();
            }
            else
            {
                panelFechasGastos.Visible = false;
                btnActualidadGastos.ForeColor = Color.Gold;
                checkFiltrosGastos.ForeColor = Color.Black;
                MostrarGastosTodos();
            }
        }

        private void dtFIgastos_ValueChanged(object sender, EventArgs e)
        {
            MostrarReporteGastosFecha();
        }

        private void dtFFgastos_ValueChanged(object sender, EventArgs e)
        {
            MostrarReporteGastosFecha();
        }

        private void btnCuentasPagarTodas_Click(object sender, EventArgs e)
        {
            ReporteCuentasPorPagar();
            checkPorProveedor.Checked = false;
        }
        private void ObtenerListadoProveedores()
        {
            var dt = new DataTable();
            Obtener_datos.MostrarProveedoresCombo(ref dt);
            cbListaProveedores.DisplayMember = "Nombre";
            cbListaProveedores.ValueMember = "IdProveedor";
            cbListaProveedores.DataSource = dt;
        }
        private void checkPorProveedor_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPorProveedor.Checked == true)
            {
                ObtenerListadoProveedores();
                panelListaProveedores.Enabled = true;
               
            }
            else
            { 

                ReporteCuentasPorPagar();
                panelListaProveedores.Enabled = false;
            }
        }

        private void cbListaProveedores_SelectedValueChanged(object sender, EventArgs e)
        {
            ReportePagosRealizados();
        }

        private void cbListaProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
