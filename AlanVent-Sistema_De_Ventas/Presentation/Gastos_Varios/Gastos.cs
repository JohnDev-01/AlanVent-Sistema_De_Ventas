using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using AlanVent_Sistema_De_Ventas.Logic;
using AlanVent_Sistema_De_Ventas.DataAccess;

namespace AlanVent_Sistema_De_Ventas.Presentation.Gastos_Varios
{
    public partial class Gastos : Form
    {
        public Gastos()
        {
            InitializeComponent();
        }
        int idconcepto;
        int idcaja;
        string TipoDocumento;
        string tipoGasto;
        private void Gastos_Load(object sender, EventArgs e)
        {
            limpiar_inicio();
            buscador_de_conceptos();
            Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
           
            ValidarTipoGasto();
        }
        private void mostrar_datalistadoConceptos()
        {
            datalistadoConceptos.Visible = true;
            datalistadoConceptos.Location = new Point(PanelDetalle.Location.X, PanelDetalle.Location.Y);
            datalistadoConceptos.BringToFront();
            buscador_de_conceptos();
        }
        private void ocultar_panelConceptos()
        {
            txtBuscarconcepto.Text = " ";
            txtBuscarconcepto.Text = "";
        }
        private void btnguardarConceptos_Click(object sender, EventArgs e)
        {

        }
        private void buscador_de_conceptos()
        {
            DataTable dt = new DataTable();
            DataAccess.Obtener_datos.Buscar_Conceptos(ref dt, txtBuscarconcepto.Text);
            datalistadoConceptos.DataSource = dt;
            datalistadoConceptos.Columns[0].Visible = false;
            Bases.MultilineaTemaOscuro(ref datalistadoConceptos);
            datalistadoConceptos.Visible = true;
            datalistadoConceptos.Dock = DockStyle.Fill;
        }

        private void btnNuevoconcepto_Click(object sender, EventArgs e)
        {
            mostrar_panelconceptos();

        }
        private void mostrar_panelconceptos()
        {

            datalistadoConceptos.Visible = false;
            PanelDetalle.Visible = true;

        }
        private void limpiar_inicio()
        {
            MostrarConceptosDiseno();
            txtBuscarconcepto.Clear();
            mostrar_datalistadoConceptos();
            btnComprobante.Checked = true;
            panelcomprobante.Visible = false;
            txtimporte.Clear();
            txtdetalle.Clear();
            txtnrocomprobante.Clear();

        }

        private void txtBuscarconcepto_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarconcepto.Text == "")
            {
                lblBuscar.Visible = true;
            }
            else
            {
                lblBuscar.Visible = false;

                MostrarConceptosDiseno();
            }
            buscador_de_conceptos();
        }
        private void MostrarConceptosDiseno()
        {
            datalistadoConceptos.Dock = DockStyle.Fill;
            datalistadoConceptos.Visible = true;
            PanelDetalle.Visible = false;
        }
        private void datalistadoConceptos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                idconcepto = Convert.ToInt32(datalistadoConceptos.SelectedCells[0].Value);
                txtBuscarconcepto.Text = datalistadoConceptos.SelectedCells[1].Value.ToString();
                datalistadoConceptos.Visible = false;
                PanelDetalle.Visible = true;
                PanelDetalle.Dock = DockStyle.Fill;

                mostrar_panelconceptos();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void btnComprobante_CheckedChanged(object sender, EventArgs e)
        {
            if (btnComprobante.Checked == true)
            {
                panelcomprobante.Visible = false;
            }
            else
            {
                panelcomprobante.Visible = true;
            }
        }
        private void rellenar_campos_vacios()
        {
            if (string.IsNullOrEmpty(txtdetalle.Text)) { txtdetalle.Text = "Sin detallar"; }
            if (string.IsNullOrEmpty(txtnrocomprobante.Text)) { txtnrocomprobante.Text = "-"; }
            if (string.IsNullOrEmpty(txttipocomprobante.Text)) { txttipocomprobante.Text = "-"; }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtimporte.Text == "")
            {
                MessageBox.Show("Ingresa un monto valido", "Confirma:",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (ValidacionesInsertarGastos() == true)
                {
                    
                    var documentoSerializado = SerializacionComprobantes.GenerarSerializacionGastosMenores();

                    var models = new Lgastos()
                    {
                        Fecha = txtfecha.Value,
                        NroDocumento = txtnrocomprobante.Text,
                        TipoComprobante = txttipocomprobante.Text,
                        Importe = Convert.ToDouble(txtimporte.Text),
                        Descripcion = txtdetalle.Text,
                        idcaja = idcaja,
                        Idconceptos = idconcepto,
                        Rnc = "-",
                        Cedula = "-",
                        TipoDocumento = "-",
                        NumeroComprobante = documentoSerializado,
                        NroComprobanteModi = "",
                        ModoPago = cbModoPago.Text,
                        TipoGasto = tipoGasto
                    };
                    bool estado = DataAccess.Insertar_datos.insertar_Gastos_varios(models);
                    if (estado == true)
                    {
                        limpiar_inicio();
                        MessageBox.Show("Datos ingresados correctamente", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }

        }
        private bool ValidacionesInsertarGastos()
        {
            int Validacion = 1;
            bool EstadoValidacion = false;
            string ModoPago = "";
            string Cedula = "";
            string rnc = "";
            rellenar_campos_vacios();
            if (cbModoPago.Text == "")
            {
                Validacion += 1;
                ModoPago += "Modo de pago ";
            }
            else
            {
                Validacion -= 1;
                ModoPago = "";
            }

            

            if (Validacion > 0)
            {
                EstadoValidacion = false;
                MessageBox.Show(Cedula + rnc + ModoPago + " Sin Validar", "Validacion:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (Validacion == 0)
            {
                EstadoValidacion = true;
            }

            return EstadoValidacion;
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void txtimporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtimporte, e);

        }
        private string AsignarComa(double valor)
        {
            return String.Format("{0:#,##0.##}", valor);
        }
        private void btnguardarcambiosConceptos_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ocultar_panelConceptos();
        }

        private void txtBuscarconcepto_Click(object sender, EventArgs e)
        {
            txtBuscarconcepto.SelectAll();
            mostrar_datalistadoConceptos();
        }
        
        private void rbCedula_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void rbRNC_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PanelDetalle_Paint(object sender, PaintEventArgs e)
        {

        }
        private void ValidarTipoGasto()
        {
            if (rbServicios.Checked == true)
            {
                tipoGasto = "SERVICIOS";
            }
            if (rbBienes.Checked == true)
            {
                tipoGasto = "BIENES";
            }
        }

        private void rbServicios_CheckedChanged(object sender, EventArgs e)
        {
            ValidarTipoGasto();
        }

        private void rbBienes_CheckedChanged(object sender, EventArgs e)
        {
            ValidarTipoGasto();
        }

        private void txtimporte_TextChanged(object sender, EventArgs e)
        {
            Bases.ValidateSeparatorToNumberInString(ref txtimporte);
        }
    }
}
