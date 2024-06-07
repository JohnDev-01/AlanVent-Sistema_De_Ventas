using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Presentation.Cotizaciones.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.Cotizaciones.Vista
{
    public partial class CotizacionesRealizadas : Form
    {
        public CotizacionesRealizadas()
        {
            InitializeComponent();
        }
        int Idcotizacion;
        int Idcliente;
        double subTotalenCotizacion;
        double TotalImpuestos;
        string NombreImpuesto;
        double PorcentajeImp;
        string Moneda;
        double cantidad_Decimal_Impuesto;
        double totalPagar;
        double impuestoCalculado;
        double descuento;
        private void MostrarCotizaciones()
        {
            var dt = new DataTable();
            ObtenerDatos.MostrarCotizaciones(ref dt, txtBuscar.Text);
            dgCotizaciones.DataSource = dt;
            dgCotizaciones.Columns[0].Visible = false;
            dgCotizaciones.Columns[1].Visible = false;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void CotizacionesRealizadas_Load(object sender, EventArgs e)
        {
            MostrarCotizaciones();
            panelDetalle.Visible = false;
        }

        private void dgCotizaciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Idcotizacion = Convert.ToInt32(dgCotizaciones.SelectedCells[0].Value);
            Idcliente = Convert.ToInt32(dgCotizaciones.SelectedCells[1].Value);
            if (Idcotizacion != 0 && Idcliente != 0)
            {
                panelDetalle.Visible = true;
                panelDetalle.Dock = DockStyle.Fill;
                MostrarDetalleCotizacion();
                CalcularTotalCotizacion();
            }
        }
        private void MostrarDetalleCotizacion()
        {
            var dt = new DataTable();
            ObtenerDatos.ListarDetalleCotizacion(ref dt, Idcotizacion);
            dgDetalle.DataSource = dt;
            dgDetalle.Columns[0].Width = 50;
            dgDetalle.Columns[1].Width = 40;
            dgDetalle.Columns[2].Width = 40;
            dgDetalle.Columns[3].Visible = false;
            dgDetalle.Columns[4].Visible = false;
        }
        private void SumarImpuesto()
        {
            TotalImpuestos = 0;
            foreach (DataGridViewRow item in dgDetalle.Rows)
            {
                TotalImpuestos += Convert.ToDouble(item.Cells["Impuesto"].Value);
            }
            lblImpuestos.Text = AsignarComa(TotalImpuestos);
        }

        private void SumarSubTotal()
        {
            subTotalenCotizacion = 0;
            foreach (DataGridViewRow item in dgDetalle.Rows)
            {
                subTotalenCotizacion += Convert.ToDouble(item.Cells["Importe"].Value);
            }
            lblsubtotal.Text = AsignarComa(subTotalenCotizacion);
        }
        private void SumarDescuento()
        {
            descuento = 0;
            foreach (DataGridViewRow item in dgDetalle.Rows)
            {
                descuento += Convert.ToDouble(item.Cells["Descuento"].Value);
            }
            totalDescuento.Text = AsignarComa(descuento);
        }
        private void CalcularTotalCotizacion()
        {
            SumarImpuesto();
            SumarSubTotal();
            SumarDescuento();
            totalPagar = subTotalenCotizacion + TotalImpuestos;
            txt_total_suma.Text = "Total: " + AsignarComa(totalPagar);
        }
        private string AsignarComa(double valor)
        {
            return String.Format("{0:#,##0.##}", valor);
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            var page = new Impresion();
            page.Idcliente = Idcliente;
            page.Idcotizacion = Idcotizacion;
            page.FormClosed += Page_FormClosed;
            page.ShowDialog();
        }

        private void Page_FormClosed(object sender, FormClosedEventArgs e)
        {
            LimpiarCotizacion();
        }
        private void LimpiarCotizacion()
        {
            Idcliente = 0;
            Idcotizacion = 0;
            panelDetalle.Visible = false;
        }
        private double Calcular_Impuesto(double subtotal)
        {
            
            try
            {
                DataTable dt = new DataTable();
                Obtener_datos.MostrarDatosImpuestos(ref dt);
                foreach (DataRow item in dt.Rows)
                {
                    NombreImpuesto = item["Impuesto"].ToString();
                    PorcentajeImp = Convert.ToDouble(item["Porcentaje_impuesto"].ToString());
                    Moneda = item["Moneda"].ToString();
                }

                //Calculo para impuesto:

                cantidad_Decimal_Impuesto = Math.Ceiling(PorcentajeImp) / 100;
                impuestoCalculado = subtotal * cantidad_Decimal_Impuesto;
            }
            catch (Exception ex)
            {

            }
            return impuestoCalculado;
        }
        private void dgDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Iddetalle = Convert.ToInt32(dgDetalle.SelectedCells[3].Value);
            double cantidad = Convert.ToDouble(dgDetalle.SelectedCells[6].Value);
            double precio = Convert.ToDouble(dgDetalle.SelectedCells[7].Value);

            if (e.ColumnIndex == dgDetalle.Columns["S"].Index)
            {
                double impuesto = Calcular_Impuesto((cantidad + 1) * precio);
                Logica.Update.SumarProductoDetalleCotizacion(Iddetalle, impuesto);
                MostrarDetalleCotizacion();
            }
            if (e.ColumnIndex == dgDetalle.Columns["R"].Index)
            {
                double impuesto = Calcular_Impuesto((cantidad - 1) * precio);
                Logica.Update.RestarProductoDetalleCotizacion(Iddetalle, impuesto);
                MostrarDetalleCotizacion();
            }
            if (e.ColumnIndex == dgDetalle.Columns["EL"].Index)
            {
                Logica.Delete.EliminarProductoDetalleCotizacion(Iddetalle);
                MostrarDetalleCotizacion();

            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                lblEtiqueta.Visible = true;
            }
            else
            {
                lblEtiqueta.Visible = false;
                MostrarCotizaciones();
            }
        }

        private void btnEliminarTODO_Click(object sender, EventArgs e)
        {
            Delete.EliminarCotizacion(Idcotizacion);
            MostrarCotizaciones();
            LimpiarCotizacion();
        }
    }
}
