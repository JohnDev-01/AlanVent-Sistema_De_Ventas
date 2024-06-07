using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;
using AlanVent_Sistema_De_Ventas.Presentation.Reportes.CierreCaja;
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

namespace AlanVent_Sistema_De_Ventas.Presentation.Caja
{
    public partial class CierreTurno : Form
    {
        public CierreTurno()
        {
            InitializeComponent();
        }
        double dinero_Calculado;
        double resultadoDiferencia;
        string correobase;
        string contraseña;
        string estado;
        string correoReceptor;
        int idusuario;
        int idcaja;
        string usuario;
        string estadoCorreo;
        public LcierreCajaCerrado modelsCierreCaja;
        private void CierreTurno_Load(object sender, EventArgs e)
        {
            lblDeberiaHaber.Text = Bases.AsignarComa(CierreDeCaja.dineroEnCaja);
            dinero_Calculado = Convert.ToDouble(lblDeberiaHaber.Text);
            mostrarCorreoBase();
            mostrarcorreodeEnvio();
            mostrarUsuarioSesion();
            ObtenerEstadoCorreo();
        }
        private void ObtenerEstadoCorreo()
        {
            Obtener_datos.MostrarEstadoCorreo(ref estadoCorreo);
            if (estadoCorreo == "SIN REGISTRO")
            {
                txtcorreo.Enabled = false;
                checkCorreo.Enabled = false;
                checkCorreo.Checked = false;
            }

        }
        private void mostrarUsuarioSesion()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrarUsuariosSesion(ref dt);
            foreach (DataRow row in dt.Rows)
            {
                usuario = row["NombreYApellido"].ToString();
            }
        }
        private void mostrarCorreoBase()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrarCorreoBase(ref dt);
            foreach (DataRow row in dt.Rows)
            {
                estado = Bases.Desencriptar(row["EstadoEnvio"].ToString());
                correobase = Bases.Desencriptar(row["Correo"].ToString());
                contraseña = Bases.Desencriptar(row["Password"].ToString());
            }
            if (estado == "Sincronizado")
            {
                checkCorreo.Checked = true;
            }
            else
            {
                checkCorreo.Checked = false;

            }
        }
        public void mostrarcorreodeEnvio()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrar_EMPRESA(ref dt);
            foreach (DataRow row in dt.Rows)
            {
                correoReceptor = row["Correo_para_envio_de_reportes"].ToString();
                txtcorreo.Text = correoReceptor;
            }
        }
        private void txthay_TextChanged(object sender, EventArgs e)
        {
            calcular();
        }
        private void Validacion_calcular()
        {
            if (resultadoDiferencia == 0)
            {
                lblanuncio.Text = "Genial, todo esta perfecto.";
                lblanuncio.ForeColor = Color.FromArgb(0, 166, 63);
                lbldiferencia.ForeColor = Color.FromArgb(0, 166, 63);
                lblanuncio.Visible = true;
            }
            if (resultadoDiferencia < dinero_Calculado & resultadoDiferencia != 0)
            {
                lblanuncio.Text = "La diferencia sera registrada en su turno y se enviara a Gerencia";
                lblanuncio.ForeColor = Color.FromArgb(231, 63, 67);
                lbldiferencia.ForeColor = Color.FromArgb(231, 63, 67);
                lblanuncio.Visible = true;

            }
            if (resultadoDiferencia > dinero_Calculado)
            {
                lblanuncio.Text = "La diferencia sera registrada en su turno y se enviara a Gerencia";
                lblanuncio.ForeColor = Color.FromArgb(231, 63, 67);
                lbldiferencia.ForeColor = Color.FromArgb(231, 63, 67);
                lblanuncio.Visible = true;
            }
        }
        private void calcular()
        {
            try
            {

                double hay;
                hay = Convert.ToDouble(txthay.Text);

                if (string.IsNullOrEmpty(txthay.Text)) hay = 0;

                resultadoDiferencia = hay - dinero_Calculado;
                lbldiferencia.Text = resultadoDiferencia.ToString();
                Validacion_calcular();
            }
            catch (Exception ex)
            {

            }

        }

        private void txthay_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void checkCorreo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkCorreo_Click(object sender, EventArgs e)
        {
        }
        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mostrarCorreoBase();
        }
        public void ReemplazarHtml()
        {


            string CobrosTarjeta = Bases.AsignarComa(CierreDeCaja.cobrosTarjeta);
            htmldeEnvio.Text = htmldeEnvio.Text.Replace("@ventas_totales", Bases.AsignarComa(CierreDeCaja.VentasTotales));
            htmldeEnvio.Text = htmldeEnvio.Text.Replace("@Ganancias", Bases.AsignarComa(CierreDeCaja.ganancias));
            htmldeEnvio.Text = htmldeEnvio.Text.Replace("@fecha", DateTime.Now.ToString("dd/MM/yyyy hh-mm-ss"));
            htmldeEnvio.Text = htmldeEnvio.Text.Replace("@usuario_nombre", usuario);
            htmldeEnvio.Text = htmldeEnvio.Text.Replace("@fondo_caja", Bases.AsignarComa(CierreDeCaja.saldoInicial));
            htmldeEnvio.Text = htmldeEnvio.Text.Replace("@ventas_efectivo", Bases.AsignarComa(CierreDeCaja.VentaEfectivo));
            htmldeEnvio.Text = htmldeEnvio.Text.Replace("@cobros", Bases.AsignarComa(CierreDeCaja.cobrosEfectivo));
            htmldeEnvio.Text = htmldeEnvio.Text.Replace("@ingresosvarios", Bases.AsignarComa(CierreDeCaja.totalingresos));
            htmldeEnvio.Text = htmldeEnvio.Text.Replace("@gastosvarios", Bases.AsignarComa(CierreDeCaja.totalGastos));
            htmldeEnvio.Text = htmldeEnvio.Text.Replace("@esperado", Bases.AsignarComa(Convert.ToDouble(lblDeberiaHaber.Text)));
            htmldeEnvio.Text = htmldeEnvio.Text.Replace("@vefectivo", Bases.AsignarComa(CierreDeCaja.VentaEfectivo));
            htmldeEnvio.Text = htmldeEnvio.Text.Replace("@vtarjeta", Bases.AsignarComa(CierreDeCaja.ventastarjeta));
            htmldeEnvio.Text = htmldeEnvio.Text.Replace("@vcredito", Bases.AsignarComa(CierreDeCaja.ventascredito));
            htmldeEnvio.Text = htmldeEnvio.Text.Replace("@Tventas", Bases.AsignarComa(CierreDeCaja.VentasTotales));
            htmldeEnvio.Text = htmldeEnvio.Text.Replace("@diferencia", Bases.AsignarComa(resultadoDiferencia));
            htmldeEnvio.Text = htmldeEnvio.Text.Replace("@Impuestosventas", Bases.AsignarComa(CierreDeCaja.impuestosRecaudados));
            htmldeEnvio.Text = htmldeEnvio.Text.Replace("@cboTarjet", CobrosTarjeta);
            htmldeEnvio.Text = htmldeEnvio.Text.Replace("@creditocobrar", Bases.AsignarComa(CierreDeCaja.creditosporCobrar));
            htmldeEnvio.Text = htmldeEnvio.Text.Replace("@creditopagar", Bases.AsignarComa(CierreDeCaja.creditosPorPagar));
            htmldeEnvio.Text = htmldeEnvio.Text.Replace("@Pagosefectivo", Bases.AsignarComa(CierreDeCaja.pagosEfectivo));
            htmldeEnvio.Text = htmldeEnvio.Text.Replace("@Pagostarjeta", Bases.AsignarComa(CierreDeCaja.pagosTarjeta));
            if (resultadoDiferencia >= 0)
            {
                htmldeEnvio.Text = htmldeEnvio.Text.Replace("@colordiferencia", "green");
            }
            else
            {
                htmldeEnvio.Text = htmldeEnvio.Text.Replace("@colordiferencia", "red");
            }
        }

        private void BtnCerrar_turno_Click(object sender, EventArgs e)
        {
            if (txthay.Text == "")
            {
                MessageBox.Show("Por Favor ingresa el monto actual en caja", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                cerrarCaja();

            }

        }
        
        private void ImprimirInforme()
        {
            var report = new CierreInforme();
            var dt = new DataTable();
            ComboBox cbImpresora = new ComboBox();
            Obtener_datos.Mostrar_impresora_Predeterminada(ref cbImpresora);
            string ImpresoraPredeterminada = cbImpresora.Text;

            Obtener_datos.ImprimirCierreCaja(ref dt);
            report.DataSource = dt;

            //Procesar Impresion 
            var printDocument = new PrintDocument();
            printDocument.PrinterSettings.PrinterName = ImpresoraPredeterminada;
            if (printDocument.PrinterSettings.IsValid == true)
            {
                var printerSetting = new PrinterSettings();
                printerSetting.PrinterName = ImpresoraPredeterminada;

                var procesadorReporting = new ReportProcessor();
                procesadorReporting.PrintReport(report, printerSetting);
            }
        }
        private void cerrarCaja()
        {

            Obtener_datos.mostrar_inicio_De_sesion(ref idusuario);
            Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);

            Lmcaja parametros = new Lmcaja();
            Editar_datos funcion = new Editar_datos();
            parametros.fechafin = DateTime.Now;
            parametros.fechacierre = DateTime.Now;
            parametros.ingresos = CierreDeCaja.Ingresos;
            parametros.egresos = CierreDeCaja.Egresos;
            parametros.Id_usuario = idusuario;
            parametros.Total_calculado = dinero_Calculado;
            try
            {
                parametros.Total_real = Convert.ToDouble(txthay.Text);
            }
            catch (Exception EX)
            {
                MessageBox.Show("Por Favor ingresa un monto valido", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            parametros.Estado = "CAJA CERRADA";
            parametros.Diferencia = resultadoDiferencia;
            parametros.Id_caja = idcaja;
            if (funcion.cerrarCaja(parametros) == true)
            {
                InsertarCierreCajaRealizado();
                if (Obtener_datos.ObtenerEstadoImprimirCierreCaja() == true)
                {
                    ImprimirInforme();
                }


                if (estadoCorreo != "SIN REGISTRO")
                {
                    enviarcorreo();
                }
                else
                {
                    MessageBox.Show("Cierre de caja realizado");
                    generarCopiaBd();
                }
            }
        }
        private void InsertarCierreCajaRealizado()
        {
            var models = modelsCierreCaja;
            models.fechacierre = DateTime.Now;
            models.Diferencia = resultadoDiferencia;
            models.Idcaja = idcaja;
            Insertar_datos.InsertarCierreCajaRealizados(models);
        }
        private void enviarcorreo()
        {

            if (checkCorreo.Checked == true)
            {
                ReemplazarHtml();
                bool estado;
                estado = Bases.enviarCorreo(correobase, "twugguemnkptugxm", htmldeEnvio.Text, "Cierre de caja AlanVent", txtcorreo.Text, "");
                if (estado == true)
                {
                    MessageBox.Show("Reporte de cierre de caja enviado");
                    generarCopiaBd();

                }
                else
                {

                    generarCopiaBd();
                }

            }
            else
            {
                generarCopiaBd();
            }

        }
        private void generarCopiaBd()
        {
            Dispose();
            string estado = "";
            Obtener_datos.ObtenerEstadoDeCopias(ref estado);
            if (estado == "--")
            {
                Dispose();
                Application.ExitThread();
            }
            else
            {
                CopiasBd.GeneradorAutomatico frm = new CopiasBd.GeneradorAutomatico();
                frm.ShowDialog();
            }
            
        }


    }
}
