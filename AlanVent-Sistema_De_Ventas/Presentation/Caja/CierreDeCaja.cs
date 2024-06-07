using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Management;
using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;

namespace AlanVent_Sistema_De_Ventas.Presentation.Caja
{
    public partial class CierreDeCaja : Form
    {
        public CierreDeCaja()
        {
            InitializeComponent();
        }
        public static double dineroEnCaja;
        public static int idcaja;
        public static DateTime FechaInicial;
        public static double saldoInicial;
        public static double VentaEfectivo;
        DateTime FechaFinal = DateTime.Now;
        public static double totalGastos;
        public static double totalingresos;
        public static double ventascredito;
        public static double ventastarjeta;
        public static double creditosPorPagar;
        public static double creditosporCobrar;
        public static double ganancias;
        public static double pagosEfectivo;
        public static double pagosTarjeta;
        public static double impuestosRecaudados;
        //---Para Calculo----
        public static double efectivoenCaja;
        public static double VentasTotales;
        public static double Ingresos;
        public static double Egresos;
        public static double CobrosTotal;
        public static double cobrosEfectivo;
        public static double cobrosTarjeta;
        private void CierreDeCaja_Load(object sender, EventArgs e)
        {
            
            Mostrar_cierres_de_caja_pediente();
            lbldesdeHastaFecha.Text = "Corte de caja desde: " + FechaInicial + " Hasta: " + DateTime.Now;
            MostrarPagosRealizados();
            obtener_saldo_inicial_en_caja();
            ObtenerVentasEnEfectivo();
            sumar_gastos_por_turno();
            sumar_ingresos_por_turno();
            sumar_CreditoPorPagar();
            sumar_creditoPorCobrar();
            M_ventas_tarjeta_turno();
            M_ventas_credito_turno();
            MostrarGanancias();
            MostrarImpuestosRecaudados();
            MostrarTotalesCobros();
            calcular();
        }
        private void MostrarTotalesCobros()
        {
            Obtener_datos.MostrarSumaCobrosTarjeta(ref cobrosEfectivo, ref cobrosTarjeta);
            lblCobrosEfectivo.Text = Bases.AsignarComa(cobrosEfectivo);
            lblcobroTarjeta.Text = Bases.AsignarComa(cobrosTarjeta);
        }
        private void MostrarImpuestosRecaudados()
        {
            impuestosRecaudados = Obtener_datos.MostrarImpuestosRecaudadosCierreCaja();
            lblImpuestosRecaudados.Text = Bases.AsignarComa(impuestosRecaudados);
        }
        private void MostrarGanancias()
        {
            ganancias = Obtener_datos.MostrarGananciasEnVentas();
            lblGananciasVentas.Text = Bases.AsignarComa(ganancias);
        }
        private void MostrarPagosRealizados()
        {
            pagosEfectivo = Obtener_datos.SumarPagosPorCuadreEFECTIVO();
            pagosTarjeta = Obtener_datos.SumarPagosPorCuadreTARJETA();
            lblPagoEfectivo.Text = Bases.AsignarComa(pagosEfectivo);   
            lblPagosTarjeta.Text = Bases.AsignarComa(pagosTarjeta);   
        }
        private void sumar_CreditoPorPagar()
        {
            Obtener_datos.sumar_CreditoPorPagar(idcaja, FechaInicial, FechaFinal, ref creditosPorPagar);
            lblporpagar.Text = Bases.AsignarComa(creditosPorPagar);
        }
        private void sumar_creditoPorCobrar()
        {
            Obtener_datos.sumar_creditoPorCobrar(idcaja, FechaInicial, FechaFinal, ref creditosporCobrar);
            lblcreditoPorCobrar.Text = Bases.AsignarComa(creditosporCobrar);
        }
        private void calcular()
        {
            double efectivoencajaSinValidar = saldoInicial + VentaEfectivo + CobrosTotal + totalingresos - totalGastos - pagosEfectivo;
            if (efectivoencajaSinValidar < 0)
                efectivoencajaSinValidar = 0;

            efectivoenCaja = efectivoencajaSinValidar;
            CobrosTotal = cobrosEfectivo + cobrosTarjeta;
            
            VentasTotales = ventascredito + ventastarjeta + VentaEfectivo;
            //-Mostrado en labels 
            lblTotalDineroEnCaja.Text = Bases.AsignarComa(efectivoenCaja);
            lblEfectivoEnCaja.Text = Bases.AsignarComa(efectivoenCaja);
            lblVentasTotales.Text = Bases.AsignarComa(VentasTotales);
            lblTotalVentas.Text = Bases.AsignarComa(VentasTotales);
            Ingresos = VentaEfectivo + CobrosTotal + totalingresos+ventastarjeta;
            Egresos = totalGastos+ pagosEfectivo+pagosTarjeta;
        }
        private void M_ventas_tarjeta_turno()
        {
            Obtener_datos.M_ventas_tarjeta_turno(idcaja, FechaInicial, FechaFinal, ref ventastarjeta);
            lblventasTarjeta.Text = Bases.AsignarComa(ventastarjeta);
        }
        private void M_ventas_credito_turno()
        {
            Obtener_datos.M_ventas_credito_turno(idcaja, FechaInicial, FechaFinal, ref ventascredito);
            lblVentasCredito.Text = Bases.AsignarComa(ventascredito);
        }
        private void sumar_gastos_por_turno()
        {
            Obtener_datos.sumar_gastos_por_turno(ref totalGastos, idcaja, FechaInicial, FechaFinal);
            lblgastosvarios.Text = Bases.AsignarComa(totalGastos);
        }
        private void sumar_ingresos_por_turno()
        {
            Obtener_datos.sumar_ingresos_por_turno(ref totalingresos, idcaja, FechaInicial, FechaFinal);
            lbligresosvarios.Text = Bases.AsignarComa(totalingresos);
        }
        private void ObtenerVentasEnEfectivo()
        {
            Obtener_datos.Mostrar_ventas_efectivo_turno(ref VentaEfectivo, idcaja, FechaInicial, FechaFinal);
            lblVEfecetivo.Text = Bases.AsignarComa(VentaEfectivo);
            lblVentaEfectivoGeneral.Text = Bases.AsignarComa(VentaEfectivo);
        }
        private void obtener_saldo_inicial_en_caja()
        {
           
            lblfondo.Text = Bases.AsignarComa(saldoInicial);
           
        }
        private void Mostrar_cierres_de_caja_pediente()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrar_cierre_de_caja_pendiente(ref dt);
            foreach (DataRow dr in dt.Rows)
            {
                idcaja = Convert.ToInt32(dr["id_caja"]);
                FechaInicial = Convert.ToDateTime(dr["fechainicio"]);
                saldoInicial = Convert.ToDouble(dr["SaldoInicial"]);
                
            }
         
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrarTurno_Click(object sender, EventArgs e)
        {
            dineroEnCaja = Convert.ToDouble( lblTotalDineroEnCaja.Text);
            var models = new LcierreCajaCerrado();
            LLenarModeloCierreCaja(ref models);
            CierreTurno frm = new CierreTurno();
            frm.modelsCierreCaja = models;
            frm.ShowDialog();
        }
        private void LLenarModeloCierreCaja(ref LcierreCajaCerrado models)
        {
            models = new LcierreCajaCerrado()
            {
                SaldoIncial = saldoInicial,
                VentasEfvo = VentaEfectivo,
                VentasTarjeta = ventastarjeta,
                VentasCredito = ventascredito,
                VentasTotales = VentasTotales,
                IngresosVarios  = totalingresos,
                CobrosEfvo = cobrosEfectivo,
                CobrosTarjeta = cobrosTarjeta,
                ImpuestosVentas = impuestosRecaudados,
                CreditoPagar = creditosPorPagar,
                CreditoCobrar = creditosporCobrar,
                GastosVarios = totalGastos,
                GananciasVentas =ganancias,
                EfvoEsperado = efectivoenCaja,
                Idcajero = idcaja,
                PagosEfectivo = pagosEfectivo,
                PagosTarjeta = pagosTarjeta
            };
        }
        private void CierreDeCaja_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Dispose();
            Ventas_Menu_Principal.frm_VENTAS_MENU_PRINCIPAL frm = new Ventas_Menu_Principal.frm_VENTAS_MENU_PRINCIPAL();
            frm.ShowDialog();
        }
    }
}
