using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;
using AlanVent_Sistema_De_Ventas.Presentation.Reportes.FacturaCobros;
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
using TextBox = System.Windows.Forms.TextBox;

namespace AlanVent_Sistema_De_Ventas.Presentation.Cobros
{
    public partial class CobrosForm : Form
    {
        public CobrosForm()
        {
            InitializeComponent();
        }
        public static int idcliente;
        public static double saldo;
        int idcaja;
        int idusuario;
        //
        double efectivo;
        double tarjeta;
        double vuelto;
        double restante;
        double montoabonado;
        string No_Venta = "";
        double credito_tenia_pagar;
        int IdregistroPago;
        string tipoImpresion;
        private void buscar()
        {
            DataTable dt = new DataTable();
            Obtener_datos.buscar_clientes(ref dt, txtclientesolicitante.Text);
            datalistadoClientes.DataSource = dt;
            datalistadoClientes.Columns[0].Visible = false;

            datalistadoClientes.Columns[1].Visible = false;

            datalistadoClientes.Columns[3].Visible = false;
            datalistadoClientes.Columns[4].Visible = false;
            datalistadoClientes.Columns[5].Visible = false;
            datalistadoClientes.Columns[6].Visible = false;
            datalistadoClientes.Columns[7].Visible = false;
            datalistadoClientes.Columns[8].Visible = false;
            datalistadoClientes.Columns[2].Width =500;
            datalistadoClientes.BringToFront();
            datalistadoClientes.Visible = true;
            //datalistadoClientes.Location = new Point(panelRegistros.Location.X, panelRegistros.Location.Y);
            datalistadoClientes.Size = new Size(538, 220);

        }
        private void Cargar_impresoras_del_equipo()
        {
            txtImpresora.Items.Clear();
            for (var I = 0; I < PrinterSettings.InstalledPrinters.Count; I++)
            {
                txtImpresora.Items.Add(PrinterSettings.InstalledPrinters[I]);
            }
            txtImpresora.Items.Add("Ninguna");

            Obtener_datos.Mostrar_impresora_Predeterminada(ref txtImpresora);
        }
        private void CobrosForm_Load(object sender, EventArgs e)
        {
            OcultarControles();
            Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
            Obtener_datos.mostrar_inicio_De_sesion(ref idusuario);
            CalculoRestante2();
            Cargar_impresoras_del_equipo();
        }
        private void OcultarControles()
        {
            //btnabonar.Visible = false;
            lblSaldo.Visible = false;   
            txttotal_saldo.Visible = false;
            reportViewer1.Visible = false;
            panelReporte.Visible = false;
            panel1.Visible = false;
            pCobro.Visible = false;
            txtefectivo2.Text = "0";
            txttarjeta2.Text = "0";
            DIBUJAR_Bienvenida();
        }
        private void MostrarControles()
        {
            //btnabonar.Visible = true;
            lblSaldo.Visible = true;
            txttotal_saldo.Visible = true;
            panel1.Visible = true;
            pCobro.Visible = true;
            panelBienvenida.Visible = false;    
        }
        private void mostrarControlCobros()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrar_ControlCobros(ref dt, idcliente);
            datalistadoMovimientos.DataSource = dt;
            Bases estilo = new Bases();
            estilo.MultilineaCobros(ref datalistadoMovimientos);
            datalistadoMovimientos.Columns[1].Visible = false;
            datalistadoMovimientos.Columns[5].Visible = false;
            datalistadoMovimientos.Columns[6].Visible = false;
            datalistadoMovimientos.Columns[7].Visible = false;
            datalistadoMovimientos.Columns[11].Visible = false;
            datalistadoMovimientos.Columns[12].Visible = false;
            datalistadoMovimientos.Columns[13].Visible = false;


            panelH.Visible = false;
            panelM.Visible = true;
            panelHistorial.Visible = false;
            panelMovimientos.Visible = true;
            panelMovimientos.Dock = DockStyle.Fill;
            panelHistorial.Dock = DockStyle.None;
        }
        private void AumentarSaldo()
        {
            double monto = Convert.ToDouble(datalistadoMovimientos.SelectedCells[2].Value);
            Lclientes parametros = new Lclientes();
            Editar_datos funcion = new Editar_datos();
            parametros.IdCliente = idcliente;
            if (funcion.aumentarSaldocliente(parametros, monto) == true)
            {
                Aumentar_saldo_tablaMovimientos();
                eliminarControlCobros();
            }
        }
        private void eliminarControlCobros()
        {
            Lcontrolcobros parametros = new Lcontrolcobros();
            Eliminar_datos funcion = new Eliminar_datos();
            parametros.IdcontrolCobro = Convert.ToInt32(datalistadoMovimientos.SelectedCells[1].Value);
            if (funcion.eliminarControlCobro(parametros) == true)
            {
                OcultarControles();
                MessageBox.Show("Abono eliminado correctamente.", "Resultado:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtclientesolicitante.Clear();
                txtclientesolicitante.Focus();

            }
        }
        private void datalistadoClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idcliente = (int)datalistadoClientes.SelectedCells[1].Value;
            txtclientesolicitante.Text = datalistadoClientes.SelectedCells[2].Value.ToString();
            obtenerSaldo();
            datalistadoClientes.Visible = false;
            mostrarEstadosCuentaCliente();
            MostrarControles();
            if (saldo <= 0)
            {
                pCobro.Visible = false;
                panelEnCero.Visible = true;
                lblCero.Text = saldo.ToString();
            }
            else
            {
                panelEnCero.Visible = false;
            }
            txtefectivo2.Text = "0";
            txttarjeta2.Text = "0";
            CalculoRestante2();
        }
        private void obtenerSaldo()
        {
            txttotal_saldo.Text = Bases.AsignarComa(Convert.ToDouble(datalistadoClientes.SelectedCells[6].Value.ToString()));
            saldo = Convert.ToDouble(datalistadoClientes.SelectedCells[6].Value); 
        }
        private void mostrarEstadosCuentaCliente()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrarEstadosCuentaCliente(ref dt, idcliente);
            datalistadoHistorial.DataSource = dt;
            Bases estilo = new Bases();
            estilo.MultilineaCobros(ref datalistadoHistorial);
            panelH.Visible = true;
            panelM.Visible = false;
            panelHistorial.Visible = true;
            panelHistorial.Dock = DockStyle.Fill;
            panelMovimientos.Visible = false;
            panelMovimientos.Dock = DockStyle.None;
            //Para No hacer visible columnas:
            datalistadoHistorial.Columns[1].Visible = false;
            datalistadoHistorial.Columns[2].Visible = false;
            datalistadoHistorial.Columns[8].Visible = false;
            datalistadoHistorial.Columns[9].Visible = false;
            datalistadoHistorial.Columns[10].Visible = false;
        }
        private void DIBUJAR_Bienvenida()
        {
            panelBienvenida.Visible = true;
            panelBienvenida.Dock = DockStyle.Bottom;
            panelBienvenida.Size = new Size(panelBienvenida.Width, 136);
            panelBienvenida.BringToFront();
        }
        private void txtclientesolicitante_TextChanged(object sender, EventArgs e)
        {
            if (txtclientesolicitante.Text == "")
            {
                lblBuscar.Visible = true;
                datalistadoClientes.Visible = false;
            }
            else
            {
                lblBuscar.Visible = false;
                buscar();
                OcultarControles();
                DIBUJAR_Bienvenida();
            }
            
        }

        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            mostrarControlCobros();
        }

        private void btnhistorial_Click(object sender, EventArgs e)
        {
            mostrarEstadosCuentaCliente();
        }
        private void RestarCuentaACliente()
        {
            //Variable estado cuenta de cliente 
            double pagado = montoabonado;
            //Procesamiento de datos:
            DataTable dt = new DataTable();
            Obtener_datos.mostrarEstadosCuentaCliente(ref dt, idcliente);
            foreach (DataRow item in dt.Rows)
            {
                double credito_recorrido = Convert.ToDouble(item["Credito"].ToString());
                int IdRegistro = Convert.ToInt32(item["IdPrestamo"].ToString());
                Identificar_Pago_en_deuda(credito_recorrido, pagado, IdRegistro,ref pagado);
                No_Venta = item["No_venta"].ToString();
                credito_tenia_pagar = Convert.ToDouble(item["Credito"].ToString());
            }
            
        }
        private void Identificar_Pago_en_deuda(double deuda_existencia,double pagado,int idRegistro,ref double pago_actualizar)
        {
            if (deuda_existencia == pagado)
            {
                EliminarRegistroDeuda(idRegistro);
                pago_actualizar = pagado - deuda_existencia;
            }
            if(pagado < deuda_existencia && pagado > 0)
            {
                double restar_credito = deuda_existencia - pagado;
                Editar_datos.Actualizar_cobro_credito(idRegistro, restar_credito);
                pago_actualizar = pagado - deuda_existencia;
            }
            if (pagado > deuda_existencia)
            {
                pago_actualizar = pagado - deuda_existencia;
                EliminarRegistroDeuda(idRegistro);
            }
        }
        private void EliminarRegistroDeuda(int id)
        {
            Eliminar_datos.Eliminar_Registro_deuda_credito(id);
        }
        private void btnabonar_Click(object sender, EventArgs e)
        {
            montoabonado = efectivo + tarjeta;
            if (montoabonado > 0)
            {
                
                disminuirSaldocliente();
                RestarCuentaACliente();
                insertarControlCobro();
                txtclientesolicitante.Clear();
            }
            else
            {
                MessageBox.Show("Especifique un monto a cobrar...","Especifique:",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
        private void insertarControlCobro()
        {
            double monto = efectivo + tarjeta;
            Lcontrolcobros parametros = new Lcontrolcobros();
            Insertar_datos funcion = new Insertar_datos();
            parametros.Monto = monto;
            parametros.Fecha = DateTime.Now;
            parametros.Detalle = "Cobro a cliente";
            parametros.IdCliente = idcliente;
            parametros.IdUsuario = idusuario;
            parametros.IdCaja = idcaja;
            parametros.Comprobante = "-";
            parametros.efectivo = efectivo;
            parametros.tarjeta = tarjeta;
            parametros.credito_Debia_pagar = credito_tenia_pagar;
            parametros.No_Venta = No_Venta;
            parametros.SaldoAnterior = saldo;
            if (funcion.Insertar_ControlCobros(parametros, ref IdregistroPago) == true)
            {
                OcultarControles();
                
            }
        }
        private void disminuirSaldocliente()
        {
            Lclientes parametros = new Lclientes();
            Editar_datos funcion = new Editar_datos();
            parametros.IdCliente = idcliente;
            funcion.disminuirSaldocliente(parametros, montoabonado);

        }
        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            buscar();
            obtenerSaldo();
            mostrarControlCobros();
        }
        //Parte de devolver pago que devuelva el historial
        private void txtclientesolicitante_Click(object sender, EventArgs e)
        {

        }

        private void datalistadoMovimientos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == datalistadoMovimientos.Columns["Eli"].Index)
            {
                DialogResult result = MessageBox.Show("¿Realmente desea eliminar esta Abono?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    AumentarSaldo();
                }
            }
        }
     
        private void Aumentar_saldo_tablaMovimientos()
        {
            int IdRegistro = 0;
            double monto = 0;
            DataTable dt = new DataTable();
            Obtener_datos.mostrarEstadosCuentaCliente(ref dt, idcliente);
            //Contar Deudas vigentes 
            int contador = dt.Rows.Count;
            if (contador == 0)
            {
                string Noventa_ParaRecuperar = datalistadoMovimientos.SelectedCells[12].Value.ToString();
                Editar_datos.Editar_Cobro_de_vuelta(Noventa_ParaRecuperar);
            }
            else
            {
                foreach (DataRow item in dt.Rows)
                {
                    IdRegistro = Convert.ToInt32(item["IdPrestamo"]);
                }
                try
                {
                    monto = Convert.ToDouble(datalistadoMovimientos.SelectedCells[2].Value);
                }
                catch (Exception ex)
                {
                }
                Editar_datos.Aumentar_saldo_tabla_movimientos_cliente(IdRegistro, monto);
            }
                    
        }
        private void CalculoRestante2()
        {
            try
            {
                efectivo = 0;
                tarjeta = 0;
                double TotalPagando = 0;
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

                //Variable del total pagando:
                TotalPagando = efectivo + tarjeta;
                //Verificar que la tarjeta no pague de mas:
                if (TotalPagando > saldo)
                {
                    double MaximoTarjeta = efectivo- saldo; 
                    if (tarjeta > MaximoTarjeta)
                    {
                        MessageBox.Show("El pago con tarjeta sobrepasa su limite. Se aplicara el monto de su limite", ":", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txttarjeta2.Text = MaximoTarjeta.ToString();
                        efectivo = Convert.ToDouble(txtefectivo2.Text);
                        tarjeta = Convert.ToDouble(txttarjeta2.Text);
                        TotalPagando = efectivo + tarjeta;
                    }
                }
                //Calculo del restante: 
                if (TotalPagando < saldo)
                {
                    txtRestante.Text = Bases.AsignarComa(saldo - TotalPagando);
                }
                if(TotalPagando >= saldo)
                {
                    txtRestante.Text = "0";
                }
                //Calculo de la de vuelta
                if (TotalPagando > saldo)
                {
                    txtVuelto.Text = Bases.AsignarComa(TotalPagando - saldo);
                }
                if (TotalPagando == saldo || TotalPagando < saldo)
                    txtVuelto.Text = "0";
            }
            catch (Exception ex)
            {

            }
        }
        
        private void txtefectivo2_TextChanged(object sender, EventArgs e)
        {
            Bases.ValidateSeparatorToNumberInString(ref txtefectivo2);
            if (ValidarNumeros(ref txtefectivo2) == true)
            {
                CalculoRestante2();
            }
          
        }
        private bool ValidarNumeros(ref TextBox txt)
        {
            try
            {
                if (txt.Text != "")
                {
                    double r = Convert.ToDouble(txt.Text);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                var cad1 = txt.Text;
                var sinCaracter = cad1.Remove(cad1.Length - 1);
                txt.Text = sinCaracter;
                return false;
            }
        }

        private void txttarjeta2_TextChanged(object sender, EventArgs e)
        {
            Bases.ValidateSeparatorToNumberInString(ref txttarjeta2);
            if (txttarjeta2.Text == "")
            {
                txttarjeta2.Text = "0";
            }
            if (ValidarNumeros(ref txttarjeta2) == true)
            {
                CalculoRestante2();
            }
            
        }

        private void txtefectivo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtefectivo2, e);
        }

        private void txttarjeta2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtefectivo2, e);
        }
        private void ImprimirFacturaDirecto()
        {
            var rpt = new facturaCobro();
            MostrarFactura(ref rpt);
            try
            {
                var document = new PrintDocument();
                document.PrinterSettings.PrinterName = txtImpresora.Text;
                if (document.PrinterSettings.IsValid)
                {
                    var printerSettings = new PrinterSettings();
                    printerSettings.PrinterName = txtImpresora.Text;
                    ReportProcessor reportProcessor = new ReportProcessor();
                    reportProcessor.PrintReport(rpt, printerSettings);
                }
                
            }
            catch (Exception ex)
            {

            }
        }
        private void ValidarImpresion()
        {
            if (IdregistroPago != 0)
            {
                if (tipoImpresion == "VISTA")
                {
                    var rpt = new facturaCobro();
                    MostrarFactura(ref rpt);
                    reportViewer1.ReportSource = rpt;
                    reportViewer1.RefreshReport();


                    
                    reportViewer1.Visible = true;
                    panelReporte.Visible = true;
                    panelReporte.Dock = DockStyle.Fill;

                    panelBienvenida.Visible = false;
                }
                else
                {
                    ImprimirFacturaDirecto();
                }
                MessageBox.Show("Pago realizado corrrectamente.", "Resultado:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al procesar la factura");
            }
        }
        private void MostrarFactura(ref facturaCobro rpt)
        {
            var dt = new DataTable();
            Obtener_datos.MostrarFacturaCobroCredito(ref dt, IdregistroPago);
            rpt.DataSource = dt;
            rpt.table1.DataSource = dt;
        }
        private void Cobrar()
        {
            montoabonado = efectivo + tarjeta;
            if (montoabonado > 0)
            {
                insertarControlCobro();
                disminuirSaldocliente();
                ValidarImpresion();
                txtclientesolicitante.Clear();
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

        private void btnGuardarEnPantalla_Click(object sender, EventArgs e)
        {
            tipoImpresion = "VISTA";
            Cobrar();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            OcultarControles();
        }
    }
}
