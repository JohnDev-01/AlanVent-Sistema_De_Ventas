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

namespace AlanVent_Sistema_De_Ventas.Presentation.HistorialVentas
{
    public partial class HistorialVentasForm : Form
    {
        public HistorialVentasForm()
        {
            InitializeComponent();
        }
        int idventa;
        double Total;
        int iddetalleventa;
        double Cantidad;
        string ControlStock;
        int idproducto;
        double TotalNuevo;
        double PrecioUnitario;
        string TotalenterosString;
        string TotalEnLetras;
        private string Tipo_de_Busqueda;
        bool ValidarCambioFechaLoad;
        string SerializacionVentaSeleccionada;
        DateTime fechaventaseleccionada;
        string TipoComprobante;
        string TipoFactura;
        int idcliente;
        int idclienteGenerico;
        private void txtbusca_TextChanged(object sender, EventArgs e)
        {
            if (txtbusca.Text == "")
            {
                lblBuscarPor.Visible = true;
            }
            else
            {
                lblBuscarPor.Visible = false;
            }
            ValidarBusquedaVentasPorTipo();
        }
        private void ValidarBusquedaVentasPorTipo()
        {
            if (Tipo_de_Busqueda == "LECTORA")
            {
                timer1.Start();
            }
            if (Tipo_de_Busqueda == "TECLADO")
            {
                BuscarVentas();
            }
        }
        private void BuscarVentasPorLectora()
        {
            BuscarVentasCodigoBarra();
            if (datalistadoVentas.Rows.Count > 0)
            {
                datalistadoVentas.ClearSelection();
                datalistadoVentas.Rows[0].Selected = true;
                ObtenerDatosVentaSeleccionada();
                Modo_Teclado();
                txtbusca.Clear();
            }
            else
            {
                MessageBox.Show("No existe ninguna venta con este identificador '" + txtbusca.Text + "'",
                    "Confirma tu factura:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void BuscarVentas()
        {

            DataTable dt = new DataTable();
            Obtener_datos.buscarVentas(ref dt, txtbusca.Text);
            datalistadoVentas.DataSource = dt;
            datalistadoVentas.Columns[1].Visible = false;
            datalistadoVentas.Columns[4].Visible = false;
            datalistadoVentas.Columns[5].Visible = false;
            datalistadoVentas.Columns[6].Visible = false;
            datalistadoVentas.Columns[8].Visible = false;
            datalistadoVentas.Columns[9].Visible = false;
            datalistadoVentas.Columns[10].Visible = false;
            datalistadoVentas.Columns[11].Visible = false;
            datalistadoVentas.Columns[13].Visible = false;
            datalistadoVentas.Columns[14].Visible = false;
            datalistadoVentas.Columns[15].Visible = false;

            Bases.Multilinea(ref datalistadoVentas);
            btnHastaHoy.BackColor = Color.FromArgb(90, 90, 90);
            btnHastaHoy.ForeColor = Color.White;

        }
        private void BuscarVentasCodigoBarra()
        {

            DataTable dt = new DataTable();
            Obtener_datos.BuscarVentasCodigoBarra(ref dt, txtbusca.Text);
            datalistadoVentas.DataSource = dt;
            datalistadoVentas.Columns[1].Visible = false;
            datalistadoVentas.Columns[4].Visible = false;
            datalistadoVentas.Columns[5].Visible = false;
            datalistadoVentas.Columns[6].Visible = false;
            datalistadoVentas.Columns[8].Visible = false;
            datalistadoVentas.Columns[9].Visible = false;
            datalistadoVentas.Columns[10].Visible = false;
            datalistadoVentas.Columns[11].Visible = false;
            datalistadoVentas.Columns[13].Visible = false;
            datalistadoVentas.Columns[14].Visible = false;
            datalistadoVentas.Columns[15].Visible = false;

            Bases.Multilinea(ref datalistadoVentas);
            btnHastaHoy.BackColor = Color.FromArgb(90, 90, 90);
            btnHastaHoy.ForeColor = Color.White;

        }
        private void MostrarInicioPaneles()
        {
            panelBienvenida.Dock = DockStyle.Fill;
            panelBienvenida.BringToFront();
            panelDetalle.Visible = false;
            panelClientes.Visible = false;

        }
        private void HistorialVentasForm_Load(object sender, EventArgs e)
        {
            ValidarCambioFechaLoad = false;
            MostrarInicioPaneles();
            fi.Value = DateTime.Now;
            ff.Value = DateTime.Now;
            MostrarTipoBusqueda();
            ValidarCambioFechaLoad = true;
            PanelTICKET.Dock = DockStyle.Left;
            Obtener_datos.Mostrar_cliente_standar(ref idclienteGenerico);
        }
        private void ObtenerDatosVentaSeleccionada()
        {
            if (datalistadoVentas.RowCount > 0)
            {
                try
                {
                    fechaventaseleccionada = Convert.ToDateTime(datalistadoVentas.SelectedCells[2].Value);
                }
                catch (Exception ex)
                {

                }
                lblfecha.Text = fechaventaseleccionada.ToString("dd-MM-yyyy");
                idventa = Convert.ToInt32(datalistadoVentas.SelectedCells[1].Value);
                lblcomprobante.Text = datalistadoVentas.SelectedCells[3].Value.ToString();
                lbltotal.Text = Bases.AsignarComa(Convert.ToDouble(datalistadoVentas.SelectedCells[4].Value.ToString()));
                Total = Convert.ToDouble(datalistadoVentas.SelectedCells[4].Value);
                lblcajero.Text = datalistadoVentas.SelectedCells[5].Value.ToString();
                lblpagocon.Text = Bases.AsignarComa(Convert.ToDouble(datalistadoVentas.SelectedCells[6].Value.ToString()));
                lblcliente.Text = datalistadoVentas.SelectedCells[8].Value.ToString();
                LBLTipodePagoOK.Text = datalistadoVentas.SelectedCells[9].Value.ToString();
                SerializacionVentaSeleccionada = datalistadoVentas.SelectedCells[13].Value.ToString();
                TipoComprobante = datalistadoVentas.SelectedCells[14].Value.ToString();
                idcliente = Convert.ToInt32(datalistadoVentas.SelectedCells[15].Value.ToString());
                lblvuelto.Text = Bases.AsignarComa(Convert.ToDouble(datalistadoVentas.SelectedCells[10].Value.ToString()));
                lblImpuestos.Text = Bases.AsignarComa(Convert.ToDouble(datalistadoVentas.SelectedCells[11].Value.ToString()));
                lblSubtotal.Text = Bases.AsignarComa(Convert.ToDouble(datalistadoVentas.SelectedCells[12].Value.ToString()));
                PanelTICKET.Visible = true;
                panelDetalle.Visible = true;
                panelBienvenida.Visible = false;
                Panelcantidad.Visible = false;
                panelReporte.Visible = false;
                panelClientes.Visible = false;
                panelDetalle.Dock = DockStyle.Fill;
                MostrarDetalleVenta();
            }
        }
        private void MostrarDetalleVenta()
        {
            DataTable dt = new DataTable();
            Obtener_datos.MostrarDetalleVenta(ref dt, idventa);
            datalistadoDetalleVenta.DataSource = dt;
            datalistadoDetalleVenta.Columns[6].Visible = false;
            datalistadoDetalleVenta.Columns[7].Visible = false;
            datalistadoDetalleVenta.Columns[8].Visible = false;
            datalistadoDetalleVenta.Columns[9].Visible = false;
            datalistadoDetalleVenta.Columns[10].Visible = false;
            datalistadoDetalleVenta.Columns[11].Visible = false;
            datalistadoDetalleVenta.Columns[12].Visible = false;
            datalistadoDetalleVenta.Columns[13].Visible = false;
            datalistadoDetalleVenta.Columns[14].Visible = false;
            datalistadoDetalleVenta.Columns[15].Visible = false;
            datalistadoDetalleVenta.Columns[16].Visible = false;
            datalistadoDetalleVenta.Columns[17].Visible = false;
            datalistadoDetalleVenta.Columns[18].Visible = false;
            Bases.Multilinea(ref datalistadoDetalleVenta);
            datalistadoDetalleVenta.BackgroundColor = Color.WhiteSmoke;
            datalistadoDetalleVenta.RowTemplate.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            datalistadoDetalleVenta.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;

        }
        private void datalistadoVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ObtenerDatosVentaSeleccionada();
        }

        private void datalistadoDetalleVenta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == datalistadoDetalleVenta.Columns["Devolver"].Index)
            {
                ObtenerDatosDetalle();
            }
        }
        private void ObtenerDatosDetalle()
        {
            lblCantActual.Text = datalistadoDetalleVenta.SelectedCells[3].Value.ToString();
            Cantidad = Convert.ToDouble(datalistadoDetalleVenta.SelectedCells[3].Value);
            PrecioUnitario = Convert.ToDouble(datalistadoDetalleVenta.SelectedCells[4].Value);
            idproducto = Convert.ToInt32(datalistadoDetalleVenta.SelectedCells[6].Value);
            iddetalleventa = Convert.ToInt32(datalistadoDetalleVenta.SelectedCells[7].Value);
            ControlStock = datalistadoDetalleVenta.SelectedCells[14].Value.ToString();

            txtcantidad.Clear();
            txtcantidad.Focus();
            Panelcantidad.Location = new Point(lblcomprobante.Location.X, lblcomprobante.Location.Y);
            Panelcantidad.Size = new Size(620, 474);
            Panelcantidad.Visible = true;
            Panelcantidad.BringToFront();
           
            

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Panelcantidad.Visible = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DetalleventaDevolucion();
        }
        private void IdentificarVentaVacia()
        {

            if (datalistadoDetalleVenta.Rows.Count == 0)
            {
                EliminarVentas();
                MostrarInicioPaneles();
            }
        }

        private void DetalleventaDevolucion()
        {

            if (!string.IsNullOrEmpty(txtcantidad.Text))
            {
                double CantidadDevolucion;
                CantidadDevolucion = Convert.ToDouble(txtcantidad.Text);
                //Actualizacion e insercion de numero de comprobantes anulados
                if (Cantidad > 1)
                {
                    Editar_datos.ActualizarComprobanteVentas(idventa);
                    Insertar_datos.InsertarVentasAnuladas(fechaventaseleccionada, SerializacionVentaSeleccionada);
                }
                else
                {
                    Insertar_datos.InsertarVentasAnuladas(fechaventaseleccionada, SerializacionVentaSeleccionada);
                }
                //-------------------------------------
                if (CantidadDevolucion > 0)
                {
                    if (CantidadDevolucion <= Cantidad)
                    {
                        LdetalleVenta parametros = new LdetalleVenta();
                        Editar_datos funcion = new Editar_datos();
                        parametros.iddetalle_venta = iddetalleventa;
                        parametros.cantidad = Convert.ToDouble(CantidadDevolucion);
                        parametros.Cantidad_mostrada = Convert.ToDouble(CantidadDevolucion);
                        if (funcion.DetalleventaDevolucion(parametros) == true)
                        {
                            if (ControlStock == "SI")
                            {
                                aumentarStock();
                                AumentarStockDetalle();
                                insertar_KARDEX_Entrada();
                                lbltotal.Text = TotalNuevo.ToString();
                                EditarVenta();
                                Panelcantidad.Visible = false;
                                ValidarPaneles();
                                ObtenerDatosVentaSeleccionada();
                                IdentificarVentaVacia();
                                BuscarVentas();
                            }
                            else
                            {
                                lbltotal.Text = TotalNuevo.ToString();
                                EditarVenta();
                                Panelcantidad.Visible = false;
                                ValidarPaneles();
                                ObtenerDatosVentaSeleccionada();
                                IdentificarVentaVacia();
                                BuscarVentas();
                            }

                            ObtenerDatosVentaSeleccionada();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Estas Exediendo la cantidad a devolver", "Datos incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                else
                {
                    MessageBox.Show("La cantidad a delvolver debe ser mayor a 0", "Datos incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            else
            {
                MessageBox.Show("Ingrese una cantidad a devolver", "Datos incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


        }
        private void ValidarPaneles()
        {
            if (TotalNuevo == 0)
            {
                PanelTICKET.Visible = false;

                panelDetalle.Visible = false;
                panelBienvenida.Visible = false;
            }
        }
        private void EditarVenta()
        {
            Lventas parametros = new Lventas();
            Editar_datos funcion = new Editar_datos();
            parametros.idventa = idventa;
            parametros.Monto_total = TotalNuevo;
            funcion.EditarVenta(parametros);
        }
        private void insertar_KARDEX_Entrada()
        {
            LKardex parametros = new LKardex();
            Insertar_datos funcion = new Insertar_datos();
            parametros.Fecha = DateTime.Now;
            parametros.Motivo = "Devolucion de producto Venta #" + lblcomprobante.Text;
            parametros.Cantidad = Convert.ToDouble(txtcantidad.Text);
            parametros.Id_producto = idproducto;
            funcion.insertar_KARDEX_Entrada(parametros);
        }
        private void aumentarStock()
        {
            Lproductos parametros = new Lproductos();
            Editar_datos funcion = new Editar_datos();
            parametros.Id_Producto1 = idproducto;
            parametros.Stock = txtcantidad.Text;
            funcion.aumentarStock(parametros);
        }
        private void AumentarStockDetalle()
        {
            LdetalleVenta parametros = new LdetalleVenta();
            Editar_datos funcion = new Editar_datos();
            parametros.Id_producto = idproducto;
            parametros.cantidad = Convert.ToDouble(txtcantidad.Text);
            funcion.AumentarStockDetalle(parametros);
        }

        private void txtcantidad_TextChanged(object sender, EventArgs e)
        {
            CalcularNuevoTotal();
        }
        private void CalcularNuevoTotal()
        {
            try
            {
                double CantidadTexto;
                CantidadTexto = Convert.ToDouble(txtcantidad.Text);
                TotalNuevo = Total - (CantidadTexto * PrecioUnitario);

            }
            catch (Exception)
            {

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estas seguro de Eliminar esta Venta?", "Eliminando Registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                foreach (DataGridViewRow row in datalistadoDetalleVenta.Rows)
                {
                    ControlStock = row.Cells["Usa_inventarios"].Value.ToString();
                    if (ControlStock == "SI")
                    {
                        idproducto = Convert.ToInt32(row.Cells["Id_producto"].Value);
                        txtcantidad.Text = row.Cells["Cant"].Value.ToString();
                        aumentarStock();
                        AumentarStockDetalle();
                        insertar_KARDEX_Entrada();


                    }
                }
                Insertar_datos.InsertarVentasAnuladas(fechaventaseleccionada, SerializacionVentaSeleccionada);
                TotalNuevo = 0;
                EliminarVentas();
                ValidarPaneles();
                BuscarVentas();
                panelBienvenida.Dock = DockStyle.Fill;
                panelBienvenida.BringToFront();
            }
        }
        private void EliminarVentas()
        {
            Lventas parametros = new Lventas();
            Eliminar_datos funcion = new Eliminar_datos();
            parametros.idventa = idventa;
            funcion.eliminar_venta(parametros);
        }

        private void btnReimprimir_Click(object sender, EventArgs e)
        {
            convertirTotalEnLetras();
            ValidarTipoImpresion();
        }
        private void ValidarTipoImpresion()
        {
            if (TipoComprobante == "TICKET")
            {
                ReimprimirTicket();
            }
            else if (TipoComprobante == "FACTURA")
            {
                //Validar si trabaja con impuestos
                string estado = "";
                Obtener_datos.ObtenerEstadoImpuestos(ref estado);
                if (estado == "SI")
                {
                    var page = new Ventas_Menu_Principal.SeleccionTipoFactura();
                    page.ShowDialog();
                    TipoFactura = page.TipoComprobante;
                }
                else
                {
                    TipoFactura = "CONSUMIDORFINAL";
                }

               
                if (TipoFactura == "CREDITOFISCAL")
                {
                    ValidarClienteSeleccionado();
                }
                else if (TipoFactura == "CONSUMIDORFINAL")
                {
                    ImprimirFactura();
                }
            }
        }


        private void ValidarClienteSeleccionado()
        {
            if (idcliente == idclienteGenerico)
            {
                //Hacer configuraciones para seleccionar un cliente 
                PanelTICKET.Visible = false;
                panelClientes.Visible = true;
                panelClientes.Dock = DockStyle.Left;
            }
            else
            {
                ImprimirFactura();
            }
        }
        private void ImprimirFactura()
        {
            DataTable dt = new DataTable();
            Obtener_datos.MostrarFacturaImpresa(ref dt, idventa, TotalEnLetras);
            dynamic rpt = "";
            if (TipoFactura == "CONSUMIDORFINAL")
            {
                rpt = new Reportes.Impresion_de_comprobantes.Factura.Facturarpt();
            }
            else if (TipoFactura == "CREDITOFISCAL")
            {
                rpt = new Reportes.Impresion_de_comprobantes.Factura.rtpFacturaCreditoFiscal();
            }

            if (TipoFactura != "-")
            {
                rpt.table1.DataSource = dt;
                rpt.DataSource = dt;
                reportViewer1.Report = rpt;
                reportViewer1.RefreshReport();
                reportViewer1.ZoomMode = Telerik.ReportViewer.WinForms.ZoomMode.PageWidth;
                panelReporte.Visible = true;
                PanelTICKET.Visible = false;
                panelClientes.Visible = false;
                panelReporte.Dock = DockStyle.Fill;
                panelReporte.BringToFront();
            }

        }
        private void convertirTotalEnLetras()
        {
            try
            {
                TotalNuevo = Convert.ToDouble(lbltotal.Text);
                TotalenterosString = total_en_letras.Num2Text(Math.Floor(TotalNuevo));
                string[] a = lbltotal.Text.Split('.');

                string TotalDecimales = a[1];

                TotalEnLetras = TotalenterosString + " CON " + TotalDecimales + "/100";
            }
            catch (Exception ex)
            {
                TotalNuevo = Convert.ToDouble(lbltotal.Text);
                TotalenterosString = total_en_letras.Num2Text(Math.Floor(TotalNuevo));
                TotalEnLetras = TotalenterosString + " CON " + "0/100";
            }
        }
        private void ReimprimirTicket()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrar_ticket_impreso(ref dt, idventa, TotalEnLetras);

            #region Validacion si codigo qr es visible
            var textQr = "";
            bool valueVisible = true;
            foreach (DataRow row in dt.Rows)
            {
                textQr = row["RedSocial"].ToString();
            }
            if (textQr == "NO_ACEPTA_CODIGOQR")
                valueVisible = false;
            else
                valueVisible = true;
            #endregion


            Reportes.Ticket.Ticket rpt = new Reportes.Ticket.Ticket();
            rpt = new Reportes.Ticket.Ticket();
            rpt.table1.DataSource = dt;
            rpt.DataSource = dt;
            rpt.barcode2.Visible = valueVisible;


            reportViewer1.Report = rpt;
            reportViewer1.RefreshReport();
            reportViewer1.ZoomMode= Telerik.ReportViewer.WinForms.ZoomMode.Percent;
            panelReporte.Visible = true;
            PanelTICKET.Visible = false;
            panelClientes.Visible = false;
            panelReporte.Dock = DockStyle.Fill;
            panelReporte.BringToFront();

        }

        private void btnHoy_Click(object sender, EventArgs e)
        {
            FiltrarFechas();
            BuscarVentas();
        }
        private void FiltrarFechas()
        {
            panelDetalle.Visible = false;
            panelBienvenida.Visible = true;
            panelBienvenida.Dock = DockStyle.Fill;
        }
        private void buscarVentasPorFechas()
        {

            DataTable dt = new DataTable();
            btnHastaHoy.BackColor = Color.FromArgb(226, 226, 226);
            btnHastaHoy.ForeColor = Color.Black;
            Obtener_datos.buscarVentasPorFechas(ref dt, fi.Value, ff.Value);
            datalistadoVentas.DataSource = dt;
            datalistadoVentas.Columns[1].Visible = false;
            datalistadoVentas.Columns[4].Visible = false;
            datalistadoVentas.Columns[5].Visible = false;
            datalistadoVentas.Columns[6].Visible = false;
            datalistadoVentas.Columns[8].Visible = false;
            datalistadoVentas.Columns[9].Visible = false;
            datalistadoVentas.Columns[10].Visible = false;
            datalistadoVentas.Columns[11].Visible = false;
            datalistadoVentas.Columns[13].Visible = false;
            Bases.Multilinea(ref datalistadoVentas);

        }

        private void fi_ValueChanged(object sender, EventArgs e)
        {
            if (ValidarCambioFechaLoad == true)
            {
                FiltrarFechas();
                buscarVentasPorFechas();
            }

        }

        private void ff_ValueChanged(object sender, EventArgs e)
        {
            if (ValidarCambioFechaLoad == true)
            {
                FiltrarFechas();
                buscarVentasPorFechas();
            }

        }

        private void txtcantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtcantidad, e);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void MostrarTipoBusqueda()
        {
            Obtener_datos.Mostrar_TipoBusqueda(ref Tipo_de_Busqueda);
            if (Tipo_de_Busqueda == "LECTORA")
            {
                Modo_Lectora();
            }
            else
            {
                Modo_Teclado();
            }
        }
        private void lblBuscarPor_Click(object sender, EventArgs e)
        {
            txtbusca.Focus();
        }
        private void Modo_Teclado()
        {
            btnLectora.BackColor = Color.White;
            btnTeclado.BackColor = Color.WhiteSmoke;
            lblBuscarPor.Text = "Buscar Ventas con Teclado...";
            Tipo_de_Busqueda = "TECLADO";
            txtbusca.Clear();
            txtbusca.Focus();
        }
        private void btnTeclado_Click(object sender, EventArgs e)
        {
            Modo_Teclado();
        }
        private void Modo_Lectora()
        {
            btnLectora.BackColor = Color.WhiteSmoke;
            btnTeclado.BackColor = Color.White;
            lblBuscarPor.Text = "Buscar con Lectora de Codigo de Barras...";
            Tipo_de_Busqueda = "LECTORA";
            txtbusca.Clear();
            txtbusca.Focus();
        }
        private void btnLectora_Click(object sender, EventArgs e)
        {
            Modo_Lectora();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            BuscarVentasPorLectora();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblAnuncioBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscarCliente.Focus();
        }
        private void MostrarClientes()
        {
            var dt = new DataTable();
            Obtener_datos.buscar_clientes(ref dt, txtBuscarCliente.Text);
            datalistadoClientes.DataSource = dt;
            Bases.Multilinea(ref datalistadoClientes);
            datalistadoClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            datalistadoClientes.Columns[1].Visible = false;
            datalistadoClientes.Columns[0].Width = 100;
            datalistadoClientes.Columns[2].Width = 250;
            datalistadoClientes.Columns[3].Visible = false;
            datalistadoClientes.Columns[4].Visible = false;
            datalistadoClientes.Columns[5].Visible = false;
            datalistadoClientes.Columns[6].Visible = false;
            datalistadoClientes.Columns[7].Visible = false;
            datalistadoClientes.Columns[8].Visible = false;
        }
        private void txtBuscarCliente_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarCliente.Text == "")
            {
                lblAnuncioBusqueda.Visible = true;
            }
            else
            {
                lblAnuncioBusqueda.Visible = false;
            }
            MostrarClientes();

        }

        private void datalistadoClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == datalistadoClientes.Columns["elegir"].Index)
            {
                int Idcliente = Convert.ToInt32(datalistadoClientes.SelectedCells[1].Value);
                idcliente = Idcliente;
                Editar_datos.ActualizarIdclienteEnVenta(idventa, Idcliente);
                ImprimirFactura();
            }
        }
    }
}
