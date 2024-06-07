using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Presentation.Cotizaciones.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.Reporting.Processing;

namespace AlanVent_Sistema_De_Ventas.Presentation.CodigosDeBarras
{
    public partial class MenuCodigoBarra : Form
    {
        public MenuCodigoBarra()
        {
            InitializeComponent();
        }
        int IdSolicitud = 0;
        int Idproducto;
        int iddetalle;
        System.Windows.Forms.Panel panelreferenciaProductos;
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                Ocultar_Productos();
            }
            else if (txtBuscar.Text != "")
            {
                Mostrar_Productos();
            }
            
        }
        private void Mostrar_Productos()
        {
            this.Controls.Remove(panelreferenciaProductos);

            panelreferenciaProductos = new System.Windows.Forms.Panel();
            this.Controls.Add(panelreferenciaProductos);
            panelreferenciaProductos.Visible = true;
            panelreferenciaProductos.Size = new System.Drawing.Size(600, 186);
            panelreferenciaProductos.Location = new Point(panelReferenciaProductos.Location.X, 70);
            panelreferenciaProductos.BackColor = Color.White;

            dgProductos.Visible = true;
            dgProductos.BackgroundColor = Color.White;
            dgProductos.Dock = DockStyle.Fill;

            lbltipodebusqueda2.Visible = false;

            panelreferenciaProductos.Controls.Add(dgProductos);
            panelreferenciaProductos.BringToFront();
            LISTAR_PRODUCTOS_BUSCADOR();



            //dgProductos.Visible = true;
            //dgProductos.BackgroundColor = Color.White;
            //dgProductos.Size = new System.Drawing.Size(600, 186);
            //dgProductos.Location = new Point(panelReferenciaProductos.Location.X, 70);
            //lbltipodebusqueda2.Visible = false;
            //dgProductos.BringToFront();
            //LISTAR_PRODUCTOS_BUSCADOR();
        }
        private void Ocultar_Productos()
        {
            dgProductos.Visible = false;
            lbltipodebusqueda2.Visible = true;
            panelreferenciaProductos.Visible = false;
        }
        private void LISTAR_PRODUCTOS_BUSCADOR()
        {
            try
            {
                DataTable dt = new DataTable();
                Obtener_datos.BuscarProductosCodigoBarras(ref dt, txtBuscar.Text);

                dgProductos.DataSource = dt;
                dgProductos.Columns[0].Visible = false;
                dgProductos.Columns[1].Visible = false;
                dgProductos.Columns[2].Width = 600;
            }
            catch (Exception ex)
            {
                
            }
        }

        private void dgProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Idproducto = Convert.ToInt32(dgProductos.SelectedCells[1].Value);
            if (IdSolicitud == 0)
            {
                CrearSolicitudNueva();
            }
            else
            {
                InsertarProductoASolicitudCodigoBarra();
            }
            
            
            
        }
        private void CrearSolicitudNueva()
        {
            Insertar_datos.InsertarSolicitudCodigoBarras(ref IdSolicitud);
            if (IdSolicitud != 0)
            {
                InsertarProductoASolicitudCodigoBarra();
            }
        }
        private void InsertarProductoASolicitudCodigoBarra()
        {
            if (Insertar_datos.InsertarDetalleCodigoBarras(IdSolicitud,Idproducto) == true)
            {
                txtBuscar.Clear();
                MostrarDetalleSolicitud();
            }
        }
        private void MenuCodigoBarra_Load(object sender, EventArgs e)
        {
            IdSolicitud = 0;
            panelMenu.Dock = DockStyle.Fill;
            panelMenu.Visible = true;
            panelReporte.Visible = false;   
            Cargar_impresoras_del_equipo();
        }
        private void MostrarDetalleSolicitud()
        {
            var dt = new DataTable();
            Obtener_datos.MostrarDetalleCodigoBarras(ref dt,IdSolicitud);
            dgDetalleProductos.DataSource = dt;
            dgDetalleProductos.Columns[0].Width = 35;
            dgDetalleProductos.Columns[1].Width = 35;
            dgDetalleProductos.Columns[2].Width = 35;
            dgDetalleProductos.Columns[3].Visible = false;
        }
        private void MostrarCodigoBarras()
        {
            var dtReporte = new DataTable();
            Obtener_datos.MostrarCodigosBarrasImprimir(ref dtReporte, IdSolicitud);

            var rpt = new rptCodigoBarras();
            rpt.DataSource = dtReporte;
            rpt.table1.DataSource = dtReporte;
            reportViewer1.ReportSource = rpt;
            reportViewer1.RefreshReport();
        }
        private void dgDetalleProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            iddetalle = Convert.ToInt32(dgDetalleProductos.SelectedCells[3].Value);
            if (e.ColumnIndex == dgDetalleProductos.Columns["EL"].Index)
            {
                Editar_datos.EliminarDetalleCodigoBarras(iddetalle);
                MostrarDetalleSolicitud();
            }
            if (e.ColumnIndex == dgDetalleProductos.Columns["S"].Index)
            {
                Editar_datos.SumarDetalleCodigoBarras(iddetalle, 1);
                MostrarDetalleSolicitud();
            }
            if (e.ColumnIndex == dgDetalleProductos.Columns["R"].Index)
            {
                Editar_datos.RestarDetalleCodigoBarras(iddetalle);
                MostrarDetalleSolicitud();
            }
        }
        private void Cargar_impresoras_del_equipo()
        {
            txtImpresora.Items.Clear();
            for (var I = 0; I < PrinterSettings.InstalledPrinters.Count; I++)
            {
                txtImpresora.Items.Add(PrinterSettings.InstalledPrinters[I]);
            }
            Obtener_datos.Mostrar_impresora_Predeterminada(ref txtImpresora);
        }
        private void btnAplicarDescuentoTodo_Click(object sender, EventArgs e)
        {
          
            var nombreProducto = dgDetalleProductos.SelectedCells[5].Value.ToString();
            DialogResult result = MessageBox.Show("Quieres aplicar " + txtCantidad.Text + " Unidades de etiquetas al producto '" + nombreProducto + "'?", "Confirma:",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                iddetalle = Convert.ToInt32(dgDetalleProductos.SelectedCells[3].Value);
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                Editar_datos.ActualizarCantidadCodigosBarras(iddetalle, cantidad);
                MostrarDetalleSolicitud();
                txtCantidad.Clear();
            }
            
        }
        private void ImprimirVistaPrevia()
        {
            panelMenu.Visible = false  ;
            panelReporte.Visible = true;
            panelReporte.Dock = DockStyle.Fill;
            MostrarCodigoBarras();
        }
        private void btnVistaPrevia_Click(object sender, EventArgs e)
        {
            ImprimirVistaPrevia();
        }

        private async void btnImprimirDirecto_Click(object sender, EventArgs e)
        {
            panelEspera.Visible = true;
            var printerName = txtImpresora.Text;
            await Task.Run(() =>
            {
                var DOCUMENTO = new PrintDocument();
                DOCUMENTO.PrinterSettings.PrinterName = printerName;
                if (DOCUMENTO.PrinterSettings.IsValid)
                {
                    PrinterSettings printerSettings = new PrinterSettings();
                    printerSettings.PrinterName = printerName;

                    var dtReporte = new DataTable();
                    Obtener_datos.MostrarCodigosBarrasImprimir(ref dtReporte, IdSolicitud);

                    var rpt = new rptCodigoBarras();
                    rpt.DataSource = dtReporte;

                    rpt.table1.DataSource = dtReporte;

                    ReportProcessor reportProcessor = new ReportProcessor();
                    reportProcessor.PrintReport(rpt, printerSettings);
                    
                }
            });
            panelEspera.Visible = false;
            EliminarDetalleCodigoBarra();
        }
        private void EliminarDetalleCodigoBarra()
        {
            Eliminar_datos.EliminarTodoDetalleCodigoBarras(IdSolicitud);
            MostrarDetalleSolicitud();
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            panelMenu.Dock = DockStyle.Fill;
            panelMenu.Visible = true;
            panelReporte.Visible = false;
        }

        private void btnOCULTARPANEL_Click(object sender, EventArgs e)
        {
          
        }
    }
}
