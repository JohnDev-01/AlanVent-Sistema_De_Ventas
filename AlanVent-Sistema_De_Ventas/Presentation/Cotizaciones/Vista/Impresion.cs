using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Presentation.Cotizaciones.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.Reporting.Processing;

namespace AlanVent_Sistema_De_Ventas.Presentation.Cotizaciones.Vista
{
    public partial class Impresion : Form
    {
        public Impresion()
        {
            InitializeComponent();
        }
        private string tipoImpresion;
        public int Idcliente;
        public int Idcotizacion;
        private void rbDirecta_CheckedChanged(object sender, EventArgs e)
        {
            ValidarVistaReporte();
            if (tipoImpresion == "FACTURA")
            {
                MostrarFactura();
            }
            if (tipoImpresion == "TICKET")
            {
                MostrarTicket();
            }
        }

        private void rbVistaPrevia_CheckedChanged(object sender, EventArgs e)
        {
            ValidarVistaReporte();
            if (tipoImpresion == "FACTURA")
            {
                MostrarFactura();
            }
            if (tipoImpresion == "TICKET")
            {
                MostrarTicket();
            }
        }
        private void ValidarVistaReporte()
        {
            VistaReporte.Dock = DockStyle.Fill;
            if (rbDirecta.Checked == true)
            {
                panelDirecto.Visible = true;
                VistaReporte.Visible = false;
            }
            else
            {
                panelDirecto.Visible = false ;
                VistaReporte.Visible = true;
                
            }

            if (rbVistaPrevia.Checked == true)
            {
                panelDirecto.Visible = false ;
                VistaReporte.Visible = true;

            }
            else
            {
                panelDirecto.Visible = true;
                VistaReporte.Visible = false;
            }
        }

        private void btnFactura_Click(object sender, EventArgs e)
        {
            tipoImpresion = "FACTURA";
            HabilitarVista();
        }
        private void HabilitarVista()
        {
            panel3.Enabled = true;
            rbDirecta.Checked = true;
            rbVistaPrevia.Checked = false;
        }
        private void Impresion_Load(object sender, EventArgs e)
        {
            panel3.Enabled = false;
            VistaReporte.Visible = false;
            panelDirecto.Visible = false;
            rbDirecta.Checked = false;
            rbVistaPrevia.Checked = false;
            Cargar_impresoras_del_equipo();
            Obtener_datos.Mostrar_impresora_Predeterminada(ref txtImpresora);
        }
        private void Cargar_impresoras_del_equipo()
        {
            txtImpresora.Items.Clear();
            for (var I = 0; I < PrinterSettings.InstalledPrinters.Count; I++)
            {
                txtImpresora.Items.Add(PrinterSettings.InstalledPrinters[I]);
            }
        }
        
        private void btnTicket_Click(object sender, EventArgs e)
        {
            tipoImpresion = "TICKET";
            HabilitarVista();

        }

        private void VistaReporte_Load(object sender, EventArgs e)
        {

        }
        private void MostrarFactura()
        {
            var datatable = new DataTable();
            Obtener_datos.mostrar_EMPRESA(ref datatable);

            var dtCliente = new DataTable();
            ObtenerDatos.MostrarInformacionCliente(ref dtCliente, Idcliente);

            var dtProductos = new DataTable();
            ObtenerDatos.MostrarCotizacionReporte(ref dtProductos, Idcotizacion);

            var dtBalances = new DataTable();
            ObtenerDatos.MostrarBalanceCotizacion(ref dtBalances, Idcotizacion);

            string nombreEmpresa = "";
            string Direccion = "";
            string Telefono = "";

            foreach (DataRow item in datatable.Rows)
            {
                nombreEmpresa = item["Nombre_Empresa"].ToString();
                Direccion = item["Direccion"].ToString();
                Telefono = item["Telefono"].ToString();
            }

            var pageReport = new Report.rptCotizacionFactura();
            pageReport.tableClientes.DataSource = dtCliente;
            pageReport.tableProductos.DataSource = dtProductos;
            pageReport.tableBalances.DataSource = dtBalances;
            pageReport.txtNombre.Value = nombreEmpresa; 
            pageReport.txtDireccion.Value = Direccion; 
            pageReport.txtTelefono.Value = Telefono;

            VistaReporte.ReportSource = pageReport;
            VistaReporte.RefreshReport();
        }
        private void MostrarTicket()
        {
            var datatable = new DataTable();
            Obtener_datos.mostrar_EMPRESA(ref datatable);

            var dtCliente = new DataTable();
            ObtenerDatos.MostrarInformacionCliente(ref dtCliente, Idcliente);

            var dtProductos = new DataTable();
            ObtenerDatos.MostrarCotizacionReporte(ref dtProductos, Idcotizacion);

            var dtBalances = new DataTable();
            ObtenerDatos.MostrarBalanceCotizacion(ref dtBalances, Idcotizacion);

            string nombreEmpresa = "";
            string Direccion = "";
            string Telefono = "";

            foreach (DataRow item in datatable.Rows)
            {
                nombreEmpresa = item["Nombre_Empresa"].ToString();
                Direccion = item["Direccion"].ToString();
                Telefono = item["Telefono"].ToString();
            }

            var pageReport = new Report.Ticket();
            pageReport.tableClientes.DataSource = dtCliente;
            pageReport.tableProductos.DataSource = dtProductos;
            pageReport.tableBalances.DataSource = dtBalances;
            pageReport.txtNombre.Value = nombreEmpresa; 
            pageReport.txtDireccion.Value = Direccion; 
            pageReport.txtTelefono.Value = Telefono;

            VistaReporte.ReportSource = pageReport;
            VistaReporte.RefreshReport();
        }
        private void EnviarImprimirDirecto()
        {
            try
            {
                var printDocument = new PrintDocument();
                printDocument.PrinterSettings.PrinterName = txtImpresora.Text;
                if (printDocument.PrinterSettings.IsValid)
                {
                    PrinterSettings parametrosPrinter = new PrinterSettings();
                    parametrosPrinter.PrinterName = txtImpresora.Text;
                    var procesadorReport = new ReportProcessor();
                    procesadorReport.PrintReport(VistaReporte.ReportSource, parametrosPrinter);
                }
                Dispose();
            }
            catch (Exception ex )
            {                
            }
        }
        private void btnImprimirDirecto_Click(object sender, EventArgs e)
        {
            EnviarImprimirDirecto();
        }
       
        private void txtImpresora_SelectedValueChanged(object sender, EventArgs e)
        {
            int Idcaja = 0;
            Obtener_datos.Obtener_id_caja_PorSerial(ref Idcaja);
            Obtener_datos.Editar_eleccion_de_impresora(Idcaja,txtImpresora.Text);
        }
    }
}
