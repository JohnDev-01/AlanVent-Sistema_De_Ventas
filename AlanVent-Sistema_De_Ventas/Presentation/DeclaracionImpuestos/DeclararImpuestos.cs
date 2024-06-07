using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.DeclaracionImpuestos
{
    public partial class DeclararImpuestos : Form
    {
        private string TipoFormulario;
        DateTime fechainicial;
        DateTime fechafinal;
        string rutaDestino;
        string rutaDestinoCompleta;
        bool GuardarConFormulario;
        public DeclararImpuestos()
        {
            InitializeComponent();
        }

        private void btnGenerar606_Click(object sender, EventArgs e)
        {

        }
        private void ProcesarInformacionExcel606(System.Data.DataTable dtCompras,
            System.Data.DataTable dtEgresos)
        {
            int totalFilas = dtCompras.Rows.Count + dtEgresos.Rows.Count;
            var ruta606 = "";
            LiberarFilas606(totalFilas, ref ruta606);

            var excelApplication = new Microsoft.Office.Interop.Excel.Application();
            var modeloexcel = excelApplication.Workbooks.Open(ruta606);

            var Hojadecalculo = excelApplication.ActiveSheet
                      as Microsoft.Office.Interop.Excel.Worksheet;

            //Procesamiento de datos en datatable 
            int IndiceFilas = 12;

            foreach (DataRow item in dtCompras.Rows)
            {

                Hojadecalculo.Cells[IndiceFilas, 2] = item["Documento"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 3] = item["tipoId"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 4] = item["TipoBien"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 5] = item["NCF"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 6] = item["NCFModificado"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 7] = item["FechaComprobante"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 8] = item["Diacomprobante"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 9] = item["FechaPago"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 10] = item["DiaPago"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 12] = item["MontoBienes"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 13] = item["MontoBienes"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 14] = item["ImpuestosFact"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 15] = item["ImpRetenido"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 18] = item["ImpuestosFact"].ToString();

                Hojadecalculo.Cells[IndiceFilas, 26] = item["ModoPago"].ToString();
                IndiceFilas++;
            }
            foreach (DataRow item in dtEgresos.Rows)
            {

                Hojadecalculo.Cells[IndiceFilas, 2] = item["Documento"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 3] = item["tipoId"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 4] = item["TipoBien"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 5] = item["NCF"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 6] = item["NCFModificado"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 7] = item["FechaComprobante"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 8] = item["Diacomprobante"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 9] = item["FechaPago"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 10] = item["DiaPago"].ToString();


                Hojadecalculo.Cells[IndiceFilas, 11] = item["ImporteServicios"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 12] = item["ImporteBienes"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 13] = item["TotalImporte"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 26] = item["ModoPago"].ToString();
                IndiceFilas++;
            }


            modeloexcel.Close(true);
            excelApplication.Quit();


            //EjecutarMacro(ruta606, "Formato606.validacion_global");
            EjecutarMacro(ruta606, "GenerarArchivo");
            IdentificarSiEliminoArchivoExcel();
        }
        private void ProcesarInformacionExcel607(System.Data.DataTable dtVentas)
        {
            int totalFilas = dtVentas.Rows.Count;
            var ruta607 = "";
            LiberarFilas607(totalFilas, ref ruta607);

            var excelApplication = new Microsoft.Office.Interop.Excel.Application();
            var modeloexcel = excelApplication.Workbooks.Open(ruta607);

            var Hojadecalculo = excelApplication.ActiveSheet
                      as Microsoft.Office.Interop.Excel.Worksheet;

            //Procesamiento de datos en datatable 
            int IndiceFilas = 12;
            foreach (DataRow item in dtVentas.Rows)
            {

                Hojadecalculo.Cells[IndiceFilas, 2] = item["Documento"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 3] = item["TipoIdentificacion"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 4] = item["Serializacion"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 5] = item["SerializacionModificada"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 6] = item["TipoIngreso"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 7] = item["FechaComprobante"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 9] = item["MontoFacturado"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 10] = item["ImpuestoFacturado"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 18] = item["Efectivo"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 20] = item["Tarjeta"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 21] = item["Credito"].ToString();
                IndiceFilas++;
            }

            modeloexcel.Close(SaveChanges: true, Type.Missing, Type.Missing);
            excelApplication.Quit();


            EjecutarMacro(ruta607, "validacionglobal");
            EjecutarMacro(ruta607, "GenerarArchivo");
            IdentificarSiEliminoArchivoExcel();
        }
        private void ProcesarInformacionExcel608(System.Data.DataTable dtVentas)
        {
            int totalFilas = dtVentas.Rows.Count;
            var ruta608 = "";
            LiberarFilas608(totalFilas, ref ruta608);

            var excelApplication = new Microsoft.Office.Interop.Excel.Application();
            var modeloexcel = excelApplication.Workbooks.Open(ruta608);

            var Hojadecalculo = excelApplication.ActiveSheet
                      as Microsoft.Office.Interop.Excel.Worksheet;

            //Procesamiento de datos en datatable 
            int IndiceFilas = 12;
            foreach (DataRow item in dtVentas.Rows)
            {

                Hojadecalculo.Cells[IndiceFilas, 2] = item["NumeroComprobante"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 4] = item["Fecha"].ToString();
                Hojadecalculo.Cells[IndiceFilas, 5] = item["Concepto"].ToString();
                
                IndiceFilas++;
            }

            modeloexcel.Close(SaveChanges: true, Type.Missing, Type.Missing);
            excelApplication.Quit();


            EjecutarMacro(ruta608, "validacionglobal");
            EjecutarMacro(ruta608, "GenerarArchivo");
            IdentificarSiEliminoArchivoExcel();
        }
        private void IdentificarSiEliminoArchivoExcel()
        {
            if (GuardarConFormulario == false)
            {
                File.Delete(rutaDestinoCompleta);
            }
        }
        private void CrearRutaArchivoDestino(string NombreArchivo, string formatoDgii)
        {
            var rutadirectorio = "";
            try
            {
                var day = DateTime.Now.Day;
                var month = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hours = DateTime.Now.ToString("hh");
                var minute = DateTime.Now.Minute;
                var second = DateTime.Now.Second;

                 rutadirectorio = rutaDestino + "\\" +
                    formatoDgii + "-" + day + "-" + month + "-" + year + "-" + hours +"-"+ minute + "-" + second;

                if (Directory.Exists(rutadirectorio) == false)
                {
                    Directory.CreateDirectory(rutadirectorio);
                }
                rutaDestinoCompleta = rutadirectorio + NombreArchivo;
                if (File.Exists(rutaDestinoCompleta) == true)
                {
                    CrearRutaArchivoDestino(NombreArchivo, formatoDgii);
                }

            }
            catch (Exception ex)
            {
                
            }
        }
        private void LiberarFilas606(int totalFilas,
            ref string rutaarchivoexcel)
        {
            #region Calculo Fecha
            int month = fechafinal.Month;
            string month2Digitos = "";
            if (month < 10)
            {
                month2Digitos = "0" + month.ToString();
            }
            else
            {
                month2Digitos = month.ToString();
            }
            #endregion
            string periodo = fechafinal.Year.ToString() + month2Digitos;
            string rnc = Obtener_datos.MostrarRncEmpresa();
            CrearRutaArchivoDestino("\\Reporte606.xlsm", "606");

            var ruta606 = Directory.GetCurrentDirectory() + "\\606";

            var excelApplication = new Microsoft.Office.Interop.Excel.Application();
            var modeloexcel = excelApplication.Workbooks.Open(ruta606);

            var Hojadecalculo = excelApplication.ActiveSheet
                      as Microsoft.Office.Interop.Excel.Worksheet;


            Hojadecalculo.Cells[4, 3] = rnc;
            Hojadecalculo.Cells[5, 3] = periodo;
            Hojadecalculo.Cells[6, 3] = totalFilas;

            modeloexcel.SaveAs(rutaDestinoCompleta);
            modeloexcel.Close(true, Type.Missing, Type.Missing);
            excelApplication.Quit();

            EjecutarMacro(rutaDestinoCompleta, "liberarFilas");
            rutaarchivoexcel = rutaDestinoCompleta;
        }
        private void LiberarFilas607(int totalFilas,
            ref string rutaarchivoexcel)
        {
            #region Calculo Fecha
            int month = fechafinal.Month;
            string month2Digitos = "";
            if (month < 10)
            {
                month2Digitos = "0" + month.ToString();
            }
            else
            {
                month2Digitos = month.ToString();
            }
            #endregion
            string periodo = fechafinal.Year.ToString() + month2Digitos;
            string rnc = Obtener_datos.MostrarRncEmpresa();
            CrearRutaArchivoDestino("\\Reporte607.xlsm", "607");

            var ruta607 = Directory.GetCurrentDirectory() + "\\607";

            var excelApplication = new Microsoft.Office.Interop.Excel.Application();
            var modeloexcel = excelApplication.Workbooks.Open(ruta607);

            var Hojadecalculo = excelApplication.ActiveSheet
                      as Microsoft.Office.Interop.Excel.Worksheet;


            Hojadecalculo.Cells[4, 3] = rnc;
            Hojadecalculo.Cells[5, 3] = periodo;
            Hojadecalculo.Cells[6, 3] = totalFilas;

            modeloexcel.SaveAs(rutaDestinoCompleta);

            modeloexcel.Close(true, Type.Missing, Type.Missing);
            excelApplication.Quit();

            EjecutarMacro(rutaDestinoCompleta, "liberarFilas");
            rutaarchivoexcel = rutaDestinoCompleta;
        }
        private void LiberarFilas608(int totalFilas,
            ref string rutaarchivoexcel)
        {
            #region Calculo Fecha
            int month = fechafinal.Month;
            string month2Digitos = "";
            if (month < 10)
            {
                month2Digitos = "0" + month.ToString();
            }
            else
            {
                month2Digitos = month.ToString();
            }
            #endregion
            string periodo = fechafinal.Year.ToString() + month2Digitos;
            string rnc = Obtener_datos.MostrarRncEmpresa();
            CrearRutaArchivoDestino("\\Reporte608.xlsm", "608");

            var ruta607 = Directory.GetCurrentDirectory() + "\\608";

            var excelApplication = new Microsoft.Office.Interop.Excel.Application();
            var modeloexcel = excelApplication.Workbooks.Open(ruta607);

            var Hojadecalculo = excelApplication.ActiveSheet
                      as Microsoft.Office.Interop.Excel.Worksheet;


            Hojadecalculo.Cells[5, 3] = rnc;
            Hojadecalculo.Cells[6, 3] = periodo;
            Hojadecalculo.Cells[7, 3] = totalFilas;

            modeloexcel.SaveAs(rutaDestinoCompleta);

            modeloexcel.Close(true, Type.Missing, Type.Missing);
            excelApplication.Quit();

            EjecutarMacro(rutaDestinoCompleta, "liberarFilas");
            rutaarchivoexcel = rutaDestinoCompleta;
        }
        private void EjecutarMacro(string archivoExcel, string nombreMacro)
        {
            Microsoft.Office.Interop.Excel.Application oExcel =
            default(Microsoft.Office.Interop.Excel.Application);

            //inicia la aplicación de excelApplication
            Type tExe = Type.GetTypeFromProgID("Excel.Application");
            oExcel = (Microsoft.Office.Interop.Excel.Application)System.Activator.CreateInstance(tExe);

            var oBooks = oExcel.Workbooks.Open(archivoExcel);

            //Ejecuta un macro en especifico
            oExcel.Run(nombreMacro);
            System.Threading.Thread.Sleep(1000);

            //salir del documento de excelApplication y liberar el recurso
            oBooks.Close(true, Type.Missing, Type.Missing);
            oExcel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel);
            oExcel = null;
        }

        private void DeclararImpuestos_Load(object sender, EventArgs e)
        {
            ValidarCheckGuardarArchivo();
            IdentificarTipoFormulario();
            ValidarFechas();
            panelOperaciones.Location = new System.Drawing.Point((Width - panelOperaciones.Width ) / 2, (Height - panelOperaciones.Height) / 2);
            panelReporte.Visible = false;
        }
        private void IdentificarTipoFormulario()
        {
            if (rb606.Checked == true)
            {
                TipoFormulario = "606";
                checkPanelReporte607.Visible = false;
            }
            else if (rb607.Checked == true)
            {
                TipoFormulario = "607";
                checkPanelReporte607.Visible = true;
            }
            else if (rb608.Checked == true)
            {
                TipoFormulario = "608";
                checkPanelReporte607.Visible = false;
            }
        }
        private void rb606_CheckedChanged(object sender, EventArgs e)
        {
            IdentificarTipoFormulario();
        }

        private void rb607_CheckedChanged(object sender, EventArgs e)
        {
            IdentificarTipoFormulario();
        }
        private void ValidarCheckGuardarArchivo()
        {
            if (checkGuardarArchivo.Checked == true)
            {
                GuardarConFormulario = true;
            }
            else
            {
                GuardarConFormulario = false;
            }
        }
        private void checkGuardarArchivo_CheckedChanged(object sender, EventArgs e)
        {
            ValidarCheckGuardarArchivo();
        }
        private void ValidarFechas()
        {
            fechafinal = dtFF.Value;
            try
            {
                fechainicial = fechafinal.AddMonths(-1);
                lblFechaInicial.Text = fechainicial.ToString("dd/MM/yyyy")
                    + " (DURANTE 1 MES)";
            }
            catch (Exception ex)
            {

            }
        }
        private void dtFi_ValueChanged(object sender, EventArgs e)
        {
            ValidarFechas();
        }
        private void ProcesarFormulario606()
        {
            var dtCompras = new System.Data.DataTable();
            var dtEgresos = new System.Data.DataTable();

            Obtener_datos.Mostrar606Compras(ref dtCompras, fechainicial, fechafinal);
            Obtener_datos.Mostrar606Gastos(ref dtEgresos, fechainicial, fechafinal);
            ProcesarInformacionExcel606(dtCompras, dtEgresos);
        }
        private void ProcesarFormulario607()
        {
            var dtVentas = new System.Data.DataTable();

            Obtener_datos.Mostrar607(ref dtVentas, fechainicial, fechafinal);

            ProcesarInformacionExcel607(dtVentas);
        }
        private void ProcesarFormulario608()
        {
            var dtComprobantes = new System.Data.DataTable();

            Obtener_datos.Mostrar608(ref dtComprobantes, fechainicial, fechafinal);

            ProcesarInformacionExcel608(dtComprobantes);
        }

        private void ValidarMostrarResumenFacturaConsumo()
        {
            if (checkPanelReporte607.Checked == true)
            {

                panelOperaciones.Visible = false;
                panelTipoFormulario.Visible = false;
                panelTipoFormulario.Dock = DockStyle.None;
                panelReporte.Visible = true;
                panelReporte.Dock = DockStyle.Fill;
                MostrarResumenFacturaConsumo();
            }
            
        }
        private void MostrarResumenFacturaConsumo()
        {
            var models = new LresumenFacturasConsumo();
            Obtener_datos.MostrarFacturasConsumoResumen(ref models, 
                fechainicial.ToString("dd/MM/yyyy"), fechafinal.ToString("dd/MM/yyyy"));

            lblCantidadNCF.Text = models.Cantidadncf;
            lblTotalMontoFacturado.Text = models.MontoFacturado;
            lblTotalItbis.Text = models.TotalImpuesto;
            lblEfectivo.Text = models.Efectivo;
            lblTarjeta.Text = models.Tarjeta;
            lblCredito.Text = models.Credito;
        }
        private async void btnGenerar_Click(object sender, EventArgs e)
        {
            if (txtRutaArchivo.Text == "")
            {
                MessageBox.Show("Especifica una ruta donde se guarde el formulario", "Completa:",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    rutaDestino = txtRutaArchivo.Text;
                    panelEspera.Visible = true;
                    panelEspera.BringToFront();
                    panelEspera.Dock = DockStyle.Fill;
                    this.Cursor = Cursors.WaitCursor;
                }
                catch (Exception ex )
                {
                    MessageBox.Show("Diseno: " + ex.Message);
                }
                
                if (TipoFormulario == "606")
                {
                    await Task.Run(() =>
                    {
                        ProcesarFormulario606();
                    });
                }
                if (TipoFormulario == "607")
                {
                    await Task.Run(() =>
                    {
                        ProcesarFormulario607();

                    });
                    ValidarMostrarResumenFacturaConsumo();
                }
                if (TipoFormulario == "608")
                {
                    await Task.Run(() =>
                    {
                        ProcesarFormulario608();
                    });
                }
               
                CancelarProcesosDeExcel();
                panelEspera.Visible = false;
                panelEspera.Dock = DockStyle.None;
                this.Cursor = Cursors.Default;
            }
        }
        private void CancelarProcesosDeExcel()
        {
            try
            {
                foreach (var process in Process.GetProcessesByName("Microsoft Excel"))
                {
                    process.Kill();
                }
            }
            catch (Exception ex )
            {
                
            }
        }
        private void btnBuscarRuta_Click(object sender, EventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                rutaDestino = folderBrowser.SelectedPath;
                txtRutaArchivo.Text = rutaDestino;
            }
        }

        private void btnVolverResumen_Click(object sender, EventArgs e)
        {
            panelTipoFormulario.Visible = true;
            panelReporte.Visible = false;
            panelReporte.Dock = DockStyle.None;
            panelOperaciones.Visible = true;
        }

        private void rb608_CheckedChanged(object sender, EventArgs e)
        {
            IdentificarTipoFormulario();
        }
    }
}
