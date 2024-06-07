using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;
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
using Panel = System.Windows.Forms.Panel;

namespace AlanVent_Sistema_De_Ventas.Presentation.Pagos
{
    public partial class RealizarPagos : Form
    {
        public RealizarPagos()
        {
            InitializeComponent();
        }
        Panel pBuscar;
        int Idproveedor;
        string Nombreproveedor;
        double saldoProveedor;
        double totalIngresado;
        double ingresadoDisminuirSaldo;
        double efectivo;
        double tarjeta;
        double restante;
        double vuelto;
        int No_Recibo;
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                lblBuscarEtiqueta.Visible = true;
                this.Controls.Remove(pBuscar);
            }
            else
            {
                lblBuscarEtiqueta.Visible = false;
                buscar();
            }
            
        }
        private void buscar()
        {
            DataTable dt = new DataTable();
            Obtener_datos.buscar_proveedoresSOLOACTIVOS(ref dt, txtBuscar.Text);
            dgProveedores.DataSource = dt;
            Bases.Multilinea(ref dgProveedores);
            dgProveedores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            
            dgProveedores.Size = new Size(504, 189);
            dgProveedores.Visible = true;
            dgProveedores.Dock = DockStyle.Fill;
            //Arreglo columnas 
            dgProveedores.Columns[0].Visible = false;
            dgProveedores.Columns[2].Visible = false;
            dgProveedores.Columns[3].Visible = false;
            dgProveedores.Columns[4].Visible = false;
            dgProveedores.Columns[5].Visible = false;
            dgProveedores.Columns[6].Visible = false;
            dgProveedores.Columns[7].Visible = false;
            dgProveedores.Columns[1].Width = 400;
            this.Controls.Remove(pBuscar);
            this.Controls.Add(pBuscar);

            pBuscar.Size = new Size(504, 189);
            pBuscar.Controls.Add(dgProveedores);
            pBuscar.Location = new Point(7, 70);
            pBuscar.BringToFront();
            pBuscar.Visible = true;
            pBuscar.BackColor = Color.Blue;
            
        }

        

        private void RealizarPagos_Load(object sender, EventArgs e)
        {
            pBuscar = new Panel();
            OrganizarControles();
            cargar_impresoras_del_equipo();
        }
        void cargar_impresoras_del_equipo()
        {
            txtImpresora.Items.Clear();
            for (var I = 0; I < PrinterSettings.InstalledPrinters.Count; I++)
            {
                txtImpresora.Items.Add(PrinterSettings.InstalledPrinters[I]);
            }
            txtImpresora.Items.Add("Ninguna");

            Obtener_datos.Mostrar_impresora_Predeterminada(ref txtImpresora);
        }

        private void btnhistorial_Click(object sender, EventArgs e)
        {
            Bases.Multilinea(ref datalistadoHistorial);
            panelM.Visible = false;
            panelH.Visible = true;
            panelHistorial.Visible = true;
            panelHistorial.Dock = DockStyle.Fill;
            panelMovimientos.Visible = false;
            MostrarHistorial();
        }
        private void MostrarHistorial()
        {
            var dt = new DataTable();
            Obtener_datos.MostrarCreditoPorPagar(ref dt,Idproveedor);
            datalistadoHistorial.DataSource = dt;
            Bases.Multilinea(ref datalistadoHistorial);
        }
        private void MostrarMovimientos()
        {
            var dt = new DataTable();
            Obtener_datos.MostrarMovimientosPagos(ref dt, Idproveedor);
            datalistadoMovimientos.DataSource = dt;
            Bases.Multilinea(ref datalistadoMovimientos);
        }
        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            Bases.Multilinea(ref datalistadoMovimientos);
            panelH.Visible = false;
            panelM.Visible = true;
            panelMovimientos.Visible = true;
            panelMovimientos.Dock = DockStyle.Fill;
            panelHistorial.Visible = false;
            MostrarMovimientos();
        }

        private void dgProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MostrarInformacionProveedor();
            panelInicio.Visible = false;
            panelCobro.Visible = true;
            panelCobro.Dock = DockStyle.Fill;
            txtBuscar.Clear();
            btnhistorial_Click(sender, e);
            txtefectivo2.Clear();
            txttarjeta2.Clear();
            txtefectivo2.Text = "0";
            txttarjeta2.Text = "0";
        }
        private void MostrarInformacionProveedor()
        {
            Idproveedor = Convert.ToInt32(dgProveedores.SelectedCells[0].Value);
            saldoProveedor = Convert.ToDouble(dgProveedores.SelectedCells[8].Value);
            Nombreproveedor = dgProveedores.SelectedCells[1].Value.ToString();
            lblNombreProveedor.Text = Nombreproveedor;
            lblSaldoVigente.Text = Bases.AsignarComa(saldoProveedor);
        }
        private void CalcularValores()
        {
            efectivo = Convert.ToDouble(txtefectivo2.Text);
            tarjeta = Convert.ToDouble(txttarjeta2.Text);
            totalIngresado = efectivo + tarjeta;
            if (totalIngresado > saldoProveedor)
            {
                ingresadoDisminuirSaldo = saldoProveedor;
            }
            else
            {
                ingresadoDisminuirSaldo = totalIngresado;
            }
            if ( tarjeta > saldoProveedor)
            {
                MessageBox.Show("Ingresa un pago con tarjeta menor a la deuda.", "Verifica:",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txttarjeta2.Text = saldoProveedor.ToString();
            }
            else
            {
                //Calculo restante
                restante = saldoProveedor - totalIngresado;
                if (restante < 0)
                {
                    restante = 0;
                }
                txtRestante.Text = Bases.AsignarComa(restante);

                // Calculo devuelta
                vuelto = totalIngresado - saldoProveedor;
                if (vuelto < 0)
                {
                    vuelto = 0;
                }
                txtVuelto.Text = Bases.AsignarComa(vuelto);
            }
          
        }
        private void txtefectivo2_TextChanged(object sender, EventArgs e)
        {
            if (txtefectivo2.Text == "")
            {
                txtefectivo2.Text = "0";
            }
            if (txttarjeta2.Text == "")
            {
                txttarjeta2.Text = "0";
            }
            Bases.ValidateSeparatorToNumberInString(ref txtefectivo2);
            CalcularValores();
        }

        private void txttarjeta2_TextChanged(object sender, EventArgs e)
        {
            if (txttarjeta2.Text == "")
            {
                txttarjeta2.Text = "0";
            }
            if (txtefectivo2.Text == "")
            {
                txtefectivo2.Text = "0";
            }
            Bases.ValidateSeparatorToNumberInString(ref txttarjeta2);
            CalcularValores();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            OrganizarControles();
        }
        private bool AbonarProveedor()
        {
            if( saldoProveedor > 0)
            {
                if (ingresadoDisminuirSaldo > 0)
                {
                    var dialog = MessageBox.Show("Estas seguro de querer pagar la cantidad de '" +
                    Bases.AsignarComa(ingresadoDisminuirSaldo) + "' al proveedor: " + Nombreproveedor + "? ",
                    "CONFIRMA EL PAGO:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialog == DialogResult.Yes)
                    {
                        Insertar_datos.GuardarPagos(Idproveedor, saldoProveedor, efectivo,
                            tarjeta, vuelto, restante, ref No_Recibo);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Ingresa un monto");
                    return false;
                    
                }
            }
            else
            {
                MessageBox.Show("No hay pagos que realizar");
                return false;
            }
        }
        private void btnAbonar_Click(object sender, EventArgs e)
        {
            if (AbonarProveedor() == true)
            {
                ImprimirPagoDirecto();
            }
        }

        private void btnGuardarEnPantalla_Click(object sender, EventArgs e)
        {
            if (AbonarProveedor() == true)
            {
                ImprimirPagoVistaPrevia();
            }
           
        }
        private void ImprimirPagoDirecto()
        {
            var dt = new DataTable();
            Obtener_datos.MostrarFacturaPagos(ref dt, No_Recibo);
            var rpt = new rptComprobantePago();
            rpt.DataSource = dt;
            rpt.table1.DataSource = dt;
            var DOCUMENTO = new PrintDocument();
            DOCUMENTO.PrinterSettings.PrinterName = txtImpresora.Text;
            if (DOCUMENTO.PrinterSettings.IsValid)
            {
                PrinterSettings printerSettings = new PrinterSettings();
                printerSettings.PrinterName = txtImpresora.Text;
                ReportProcessor reportProcessor = new ReportProcessor();
                reportProcessor.PrintReport(rpt, printerSettings);
            }
            OrganizarControles();
        }
        private void ImprimirPagoVistaPrevia()
        {
            var dt = new DataTable();
            Obtener_datos.MostrarFacturaPagos(ref dt, No_Recibo);
            var rpt = new rptComprobantePago();
            rpt.DataSource = dt;
            rpt.table1.DataSource = dt;
            reportViewer1.ReportSource = rpt;
            reportViewer1.RefreshReport();
            OrganizarControles();
            panelInicio.Visible = false;
            panelInicio.Dock = DockStyle.None;
            panelVistaPrevia.Visible = true;
            panelVistaPrevia.Dock = DockStyle.Fill;
        }
        private void OrganizarControles()
        {
            panelInicio.Visible = true;
            panelInicio.Dock = DockStyle.Fill;
            panelCobro.Visible = false;
            panelCobro.Dock = DockStyle.None;
            panelVistaPrevia.Visible = false;
            panelVistaPrevia.Dock = DockStyle.None;
            
        }
        private void btnVolverImpresion_Click(object sender, EventArgs e)
        {
            OrganizarControles();
        }
        private void RetornarPago()
        {
            int idpago = Convert.ToInt32(datalistadoMovimientos.SelectedCells[1].Value);
            if (Eliminar_datos.EliminarPagoProveedor(idpago, Idproveedor)== true)
            {
                MessageBox.Show("Se ha eliminado el pago correctamente.", "Retorno:",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void datalistadoMovimientos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == datalistadoMovimientos.Columns[0].Index)
            {
                var dialog = MessageBox.Show("Estas seguro de eliminar el pago seleccionado?", "Confirma:",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    RetornarPago();
                    OrganizarControles();
                }
            }
            
        }
    }
}
