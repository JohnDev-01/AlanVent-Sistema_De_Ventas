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

namespace AlanVent_Sistema_De_Ventas.Presentation.Cobros
{
    public partial class Medios_Cobros : Form
    {
        public Medios_Cobros()
        {
            InitializeComponent();
        }
        double saldo;
        int idcliente;
        int idcaja;
        int idusuario;
      //----------------
        double efectivo;
        double tarjeta;
        double vuelto;
        double restante;
        double efectivoCalculado;
        double montoabonado;
        //----------------
        string tipoImpresion;
        int IdregistroPago;
        private void Medios_Cobros_Load(object sender, EventArgs e)
        {
            saldo = CobrosForm.saldo;
            lbltotal.Text = saldo.ToString();
            idcliente = CobrosForm.idcliente;
            Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
            Obtener_datos.mostrar_inicio_De_sesion(ref idusuario);
        }
        private void calcularRestante()
        {
            try
            {
                efectivo = 0;
                tarjeta = 0;
                if (string.IsNullOrEmpty(txtefectivo2.Text))
                {
                    efectivo = 0;
                }
                else
                {
                    efectivo = Convert.ToDouble(txtefectivo2.Text);

                }
                if (string.IsNullOrEmpty(txttarjeta2.Text))
                {
                    tarjeta = 0;
                }
                else
                {
                    tarjeta = Convert.ToDouble(txttarjeta2.Text);
                }
                //calculo de vuelto
                if (efectivo > saldo)
                {
                    vuelto = efectivo - saldo;
                    efectivoCalculado = (efectivo - vuelto);
                    TXTVUELTO.Text = vuelto.ToString();
                }
                else
                {
                    vuelto = 0;
                    efectivoCalculado = efectivo;
                    TXTVUELTO.Text = vuelto.ToString();

                }

                //calculo del restante
                restante = saldo - efectivoCalculado - tarjeta;
                txtrestante.Text = restante.ToString();
                if (restante < 0)
                {
                    txtrestante.Visible = false;
                    Label8.Visible = false;
                }
                else
                {
                    txtrestante.Visible = true;
                    Label8.Visible = true;
                }

                if (tarjeta == saldo)
                {
                    efectivo = 0;
                    txtefectivo2.Text = efectivo.ToString();
                }
                if (tarjeta > saldo)
                {
                    MessageBox.Show("El pago con tarjeta no puede ser mayor que el saldo");
                    tarjeta = 0;
                    txttarjeta2.Text = tarjeta.ToString();
                }
            }
            catch (Exception)
            {

            }
        }

        private void txtefectivo2_TextChanged(object sender, EventArgs e)
        {
            calcularRestante();
        }

        private void txttarjeta2_TextChanged(object sender, EventArgs e)
        {
            calcularRestante();
        }
        private void Cobrar()
        {
            montoabonado = efectivoCalculado + tarjeta;
            if (montoabonado > 0)
            {
                insertarControlCobro();
                disminuirSaldocliente();
                ValidarImpresion();
            }
            else
            {
                MessageBox.Show("Especifique un monto a abonar");
            }
        }
        private void btncobrar_Click(object sender, EventArgs e)
        {
            tipoImpresion = "DIRECTO";
            Cobrar();
        }
        private void insertarControlCobro()
        {
            Lcontrolcobros parametros = new Lcontrolcobros();
            Insertar_datos funcion = new Insertar_datos();
            parametros.Monto = efectivoCalculado + tarjeta;
            parametros.Fecha = DateTime.Now;
            parametros.Detalle = "Cobro a cliente";
            parametros.IdCliente = idcliente;
            parametros.IdUsuario = idusuario;
            parametros.IdCaja = idcaja;
            parametros.Comprobante = "-";
            parametros.efectivo = efectivoCalculado;
            parametros.tarjeta = tarjeta;
            funcion.Insertar_ControlCobros(parametros, ref IdregistroPago);
            
        }
        private void disminuirSaldocliente()
        {
            Lclientes parametros = new Lclientes();
            Editar_datos funcion = new Editar_datos();
            parametros.IdCliente = idcliente;
            funcion.disminuirSaldocliente(parametros, montoabonado);

        }

        private void txtefectivo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtefectivo2, e);
        }

        private void txttarjeta2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtefectivo2, e);
        }

        private void TXTVUELTO_TextChanged(object sender, EventArgs e)
        {

        }
        private void ValidarImpresion()
        {
            if (IdregistroPago != 0)
            {
                if (tipoImpresion == "VISTA")
                {
                    MostrarFactura();
                }
            }
            else
            {
                MessageBox.Show("Error al procesar la factura");
            }
        }
        private void MostrarFactura()
        {
            var dt = new DataTable();
            Obtener_datos.MostrarFacturaCobroCredito(ref dt, IdregistroPago);
            Reportes.FacturaCobros.facturaCobro rpt = new Reportes.FacturaCobros.facturaCobro();
            rpt.DataSource = dt;
            rpt.table1.DataSource = dt;
            reportViewer1.ReportSource = rpt;
            reportViewer1.RefreshReport();

            reportViewer1.Visible = true;
            reportViewer1.Dock = DockStyle.Fill;
        }
        private void btnGuardarEnPantalla_Click(object sender, EventArgs e)
        {
            tipoImpresion = "VISTA";
            Cobrar();
        }
    }
}
