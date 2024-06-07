using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;
using AlanVent_Sistema_De_Ventas.Presentation.Cotizaciones.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.Cotizaciones.Vista
{
    public partial class CotizacionesMenu : Form
    {
        public CotizacionesMenu()
        {
            InitializeComponent();
        }
        string Tipo_de_Busqueda;
        Panel panel_mostrador_de_productos = new Panel();
        int Idcotizacion = 0;
        int Id_producto = 0;
        double PrecioUnitario = 0;
        string sevendePor;
        public static double Cantidad;
        double subTotalenCotizacion;
        double TotalImpuestos;
        string NombreImpuesto;
        double PorcentajeImp;
        string Moneda;
        double cantidad_Decimal_Impuesto;
        double totalPagar;
        double impuestoCalculado;
        double descuento;
        int idcliente;
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (Tipo_de_Busqueda == "LECTORA")
            {
                lbltipodebusqueda2.Visible = false;
                panel_mostrador_de_productos.Visible = false;
                timerLeerCodigo.Start();
                dgProductos.Visible = false;

            }
            else if (Tipo_de_Busqueda == "TECLADO")
            {

                if (txtBuscar.Text == "")
                {
                    Ocultar_Productos();
                }
                else if (txtBuscar.Text != "")
                {
                    Mostrar_Productos();
                }
                LISTAR_PRODUCTOS_BUSCADOR();
            }
        }
       
        private void CotizacionesMenu_Load(object sender, EventArgs e)
        {
            Tipo_de_Busqueda = ObtenerDatos.Mostrar_Tipo_Busqueda();
            ValidarTipoBusqueda();
        }

        private void Mostrar_Productos()
        {
            panel_mostrador_de_productos.Size = new System.Drawing.Size(600, 186);
            panel_mostrador_de_productos.BackColor = Color.White;
            int D = panelReferenciaProductos.Location.Y;
            panel_mostrador_de_productos.Location = new Point(panelReferenciaProductos.Location.X, 98);
            panel_mostrador_de_productos.Visible = true;
            dgProductos.Visible = true;
            dgProductos.Dock = DockStyle.Fill;
            dgProductos.BackgroundColor = Color.White;
            lbltipodebusqueda2.Visible = false;
            panel_mostrador_de_productos.Controls.Add(dgProductos);


            this.Controls.Add(panel_mostrador_de_productos);
            panel_mostrador_de_productos.BringToFront();
        }
        private void Ocultar_Productos()
        {
            panel_mostrador_de_productos.Visible = false;
            dgProductos.Visible = false;
            lbltipodebusqueda2.Visible = true;
            txtBuscar.Clear();
        }
        private void LISTAR_PRODUCTOS_BUSCADOR()
        {
            try
            {
                var dt = new DataTable();
                ObtenerDatos.ListarProductosBuscadorCotizacion(ref dt, txtBuscar.Text);
                dgProductos.DataSource = dt;

                dgProductos.Columns[0].Visible = false;
                dgProductos.Columns[1].Visible = false;
                dgProductos.Columns[2].Width = 600;
                dgProductos.Columns[3].Visible = false;
                dgProductos.Columns[4].Visible = false;
                dgProductos.Columns[5].Visible = false;
                dgProductos.Columns[6].Visible = false;
                dgProductos.Columns[7].Visible = false;
                dgProductos.Columns[8].Visible = false;
                dgProductos.Columns[9].Visible = false;
                dgProductos.Columns[10].Visible = false;
            }
            catch (Exception ex)
            {
                DataAccess.ConexionMaestra.cerrar();
            }
        }
        private void Modo_Teclado()
        {
            Ocultar_Productos();
            lbltipodebusqueda2.Text = "Buscar con Teclado...";
            Tipo_de_Busqueda = "TECLADO";
            btnTeclado.BackColor = Color.White;
            btnLectora.BackColor = Color.WhiteSmoke;
            ValidarTipoBusqueda();
            txtBuscar.Clear();
            txtBuscar.Focus();
        }
        private void btnTeclado_Click(object sender, EventArgs e)
        {
            Modo_Teclado();
        }

        private void btnLectora_Click(object sender, EventArgs e)
        {
            Modo_Lectora();
        }
        private void Modo_Lectora()
        {
            Ocultar_Productos();
            lbltipodebusqueda2.Text = "Buscar con Lectora de Codigo de Barras...";
            panel_mostrador_de_productos.Visible = false;
            Tipo_de_Busqueda = "LECTORA";
            btnTeclado.BackColor = Color.WhiteSmoke;
            btnLectora.BackColor = Color.White;
            ValidarTipoBusqueda();
            txtBuscar.Clear();
            txtBuscar.Focus();
        }
        private void ValidarTipoBusqueda()
        {


            if (Tipo_de_Busqueda == "TECLADO")
            {
                lbltipodebusqueda2.Text = "Buscar con Teclado...";
                btnTeclado.BringToFront();

                btnLectora.ForeColor = Color.Black;
                btnTeclado.ForeColor = Color.Black;
                btnLectora.BackColor = Color.WhiteSmoke;
                btnTeclado.BackColor = Color.LightGreen;
            }
            else
            {
                btnLectora.BringToFront();

                lbltipodebusqueda2.Text = "Buscar con Lectora de Codigo de Barras...";
                btnLectora.BackColor = Color.LightGreen;
                btnTeclado.BackColor = Color.WhiteSmoke;
                btnLectora.ForeColor = Color.Black;
                btnTeclado.ForeColor = Color.Black;
                txtBuscar.Clear();
                txtBuscar.Focus();


            }
        }
        private void CrearNuevaCotizacion()
        { 
            //Abroi formulario de clientes para sellecionar uno
            var form = new Clientes();
            form.ShowDialog();
            idcliente = form.Idcliente;

            //Obtengo Serial De La PC
            string serialpc = "";
            Bases.Obtener_serialPc(ref serialpc);

            // Creacion del modelo 
            var models = new Models.Mcotizacion()
            {
                Idcliente = idcliente,
                Fecha = DateTime.Now.ToString("dd/MM/yyyy"),
                Impuestos = 0,
                SubTotal = 0,
                Total = 0,
                SerialPc = serialpc,
                Descuento = 0
            };
            Insert.InsertarCotizacion(models, ref Idcotizacion);

        }
        private void dgProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ObtenerDatos.ValidarExisteCotizacion() == false)
            {
                CrearNuevaCotizacion();
                InformacionYCotizar();
            }
            else
            {
                Idcotizacion = ObtenerDatos.ObtenerIdCotizacion();
                InformacionYCotizar();
            }



        }

        private void InsertarProductoAlDetalle()
        {
            bool stateImpuesto = ConfirmarEstadoImpuestos();
            double ValueImpuesto;

            if (stateImpuesto == true)
            {
                double subtotal = Cantidad * PrecioUnitario;
                ValueImpuesto = Calcular_Impuesto(subtotal);
            }
            else
            {
                ValueImpuesto = 0;
            }


            Insert.InsertarDetalleCotizacion(new Models.MdetalleCotizacion()
            {
                Idventa = Idcotizacion,
                Idproducto = Id_producto,
                Cantidad = Cantidad,
                PrecioUnitario = PrecioUnitario,
                Impuesto = ValueImpuesto,
                Descuento = 0
            });

            ListarDetalleCotizacion();
            Ocultar_Productos();
        }
        private void ListarDetalleCotizacion()
        {
            var dt = new DataTable();
            ObtenerDatos.ListarDetalleCotizacion(ref dt, Idcotizacion);
            dgDetalleCotizacion.DataSource = dt;
            dgDetalleCotizacion.Columns[0].Width = 50;
            dgDetalleCotizacion.Columns[1].Width = 40;
            dgDetalleCotizacion.Columns[2].Width = 40;
            dgDetalleCotizacion.Columns[3].Visible = false;
            dgDetalleCotizacion.Columns[4].Visible = false;
            if (ConfirmarEstadoImpuestos() == true)
            {
                dgDetalleCotizacion.Columns[9].Visible = true;
            }
            else
            {
                dgDetalleCotizacion.Columns[9].Visible = false;
            }
            dgDetalleCotizacion.Columns[10].Visible = true;

            PanelOperaciones.Visible = true;
            CalcularTotalCotizacion();
        }
        private void SumarImpuesto()
        {
            TotalImpuestos = 0;
            foreach (DataGridViewRow item in dgDetalleCotizacion.Rows)
            {
                TotalImpuestos += Convert.ToDouble(item.Cells["Impuesto"].Value);
            }
            lblImpuestos.Text = AsignarComa(TotalImpuestos);
        }

        private void SumarSubTotal()
        {
            subTotalenCotizacion = 0;
            foreach (DataGridViewRow item in dgDetalleCotizacion.Rows)
            {
                subTotalenCotizacion += Convert.ToDouble(item.Cells["Importe"].Value);
            }
            lblsubtotal.Text = AsignarComa(subTotalenCotizacion);
        }
        private void SumarDescuento()
        {
            descuento = 0;
            foreach (DataGridViewRow item in dgDetalleCotizacion.Rows)
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
        private bool ConfirmarEstadoImpuestos()
        {

            string EstadoImpuestos = "";
            Obtener_datos.ObtenerEstadoImpuestos(ref EstadoImpuestos);
            if (EstadoImpuestos == "SI")
            {

                return true;
            }
            else
            {
                return false;
            }

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
        
        private string AsignarComa(double valor)
        {
            return String.Format("{0:#,##0.##}", valor);
        }
        private void CotizacionPorGranel()
        {
            Ventas_Menu_Principal.Cantidad_a_granel frm = new Ventas_Menu_Principal.Cantidad_a_granel();
            frm.precio_unitario = PrecioUnitario;
            frm.FormClosing += Frm_FormClosing;
            frm.ShowDialog();

        }

        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            InsertarProductoAlDetalle();
        }

        private void InformacionYCotizar()
        {
            if (Idcotizacion != 0)
            {
                Id_producto = Convert.ToInt32(dgProductos.SelectedCells[1].Value.ToString());
                PrecioUnitario = Convert.ToDouble(dgProductos.SelectedCells[6].Value.ToString());
                sevendePor = dgProductos.SelectedCells[8].Value.ToString();

                if (sevendePor == "Granel")
                {
                    CotizacionPorGranel();
                }
                else if (sevendePor == "Unidad")
                {
                    Cantidad = 1;
                    InsertarProductoAlDetalle();
                }
            }


        }
        private void CerrarCotizaciones()
        {
            Logica.Update.CerrarEstadoCotizaciones();
        }

        private void CotizacionesMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            CerrarCotizaciones();
        }

        private void dgDetalleCotizacion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Iddetalle = Convert.ToInt32(dgDetalleCotizacion.SelectedCells[3].Value);
            double cantidad = Convert.ToDouble(dgDetalleCotizacion.SelectedCells[6].Value);
            double precio = Convert.ToDouble(dgDetalleCotizacion.SelectedCells[7].Value);

            if (e.ColumnIndex == dgDetalleCotizacion.Columns["S"].Index)
            {
                double impuesto;
                if (ConfirmarEstadoImpuestos() == true)
                {
                    impuesto = Calcular_Impuesto((cantidad + 1) * precio);
                }
               else
                {
                    impuesto = 0;
                }
                Logica.Update.SumarProductoDetalleCotizacion(Iddetalle, impuesto);
                ListarDetalleCotizacion();
            }
            if (e.ColumnIndex == dgDetalleCotizacion.Columns["R"].Index)
            {
                
                double impuesto;
                if (ConfirmarEstadoImpuestos() == true)
                {
                    impuesto = Calcular_Impuesto((cantidad - 1) * precio);
                }
                else
                {
                    impuesto = 0;
                }
                Logica.Update.RestarProductoDetalleCotizacion(Iddetalle, impuesto);
                ListarDetalleCotizacion();
            }
            if (e.ColumnIndex == dgDetalleCotizacion.Columns["EL"].Index)
            {
                Logica.Delete.EliminarProductoDetalleCotizacion(Iddetalle);
                ListarDetalleCotizacion();

            }

        }

        private void lblCant_Click(object sender, EventArgs e)
        {
            txtcantidad.Focus();
        }
        private bool ValidateStringToNumber(string text)
        {
            try
            {
                int number = Convert.ToInt32(text);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void AplicarCantProducto(int Iddetalle, double cantidad, double precioUnitario)
        {
           
            double impuesto;
            if (ConfirmarEstadoImpuestos() == true)
            {
                impuesto = Calcular_Impuesto((cantidad) * precioUnitario);
            }
            else
            {
                impuesto = 0;
            }
            Logica.Update.AplicarCantidadCotizacion(Iddetalle, cantidad, impuesto);
        }
        private void btncantidad_Click(object sender, EventArgs e)
        {
            if (ValidateStringToNumber(txtcantidad.Text) == true)
            {
                //Get to values
                double cant = Convert.ToDouble(txtcantidad.Text);
                int Iddetalle = Convert.ToInt32(dgDetalleCotizacion.SelectedCells[3].Value);
                double precioUnit = Convert.ToDouble(dgDetalleCotizacion.SelectedCells[7].Value);
                string descripcion = dgDetalleCotizacion.SelectedCells[5].Value.ToString();

                //Validation 
                DialogResult dlg = MessageBox.Show($"Estas seguro de aplicar la cantidad de {cant}" +
                    $" al producto '{descripcion}'", "Confirma:", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dlg == DialogResult.Yes)
                {
                    AplicarCantProducto(Iddetalle, cant, precioUnit);
                    ListarDetalleCotizacion();
                    txtcantidad.Clear();
                }
                else
                {
                    txtcantidad.Clear();
                }
            }
            else
            {
                txtcantidad.Clear();
            }
        }

        private void txtcantidad_TextChanged(object sender, EventArgs e)
        {
            if (txtcantidad.Text == "")
            {
                lblCant.Visible = true;
            }
            else
            {
                lblCant.Visible = false;
            }
        }

        private void btnAplicarDescuento_Click(object sender, EventArgs e)
        {
            AplicarDescuentoProductoSeleccionado();
        }
        private void AplicarDescuentoProductoSeleccionado()
        {
            int Idproducto = Convert.ToInt32(dgDetalleCotizacion.SelectedCells[4].Value);
            int Iddetalle = Convert.ToInt32(dgDetalleCotizacion.SelectedCells[3].Value);
            double cantidad = Convert.ToDouble(dgDetalleCotizacion.SelectedCells[6].Value);

            var models = new Models.Descuento()
            {
                idproducto = Idproducto,
                iddetalle = Iddetalle,
                Cantidad = cantidad
            };

            Logica.Update.AplicarDescuentoSeleccionado(models);
            ListarDetalleCotizacion();
            CalcularImpuestoTodos();
            ListarDetalleCotizacion();
        }
        private void AplicarDescuentoProductoTodos()
        {
            foreach (DataGridViewRow item in dgDetalleCotizacion.Rows)
            {
                int Idproducto = Convert.ToInt32(item.Cells["Id_Producto"].Value);
                int Iddetalle = Convert.ToInt32(item.Cells["Iddetalle"].Value);
                double cantidad = Convert.ToDouble(item.Cells["Cantidad"].Value);

                var models = new Models.Descuento()
                {
                    idproducto = Idproducto,
                    iddetalle = Iddetalle,
                    Cantidad = cantidad
                };

                Logica.Update.AplicarDescuentoSeleccionado(models);
            }
           
            ListarDetalleCotizacion();
            CalcularImpuestoTodos();
            ListarDetalleCotizacion();
        }   
        private void CalcularImpuestoTodos()
        {
            foreach (DataGridViewRow item in dgDetalleCotizacion.Rows)
            {
                int Iddetalle = Convert.ToInt32(item.Cells["Iddetalle"].Value);
                double Importe = Convert.ToDouble(item.Cells["Importe"].Value);
                double impuesto = Calcular_Impuesto(Importe);
                Logica.Update.ActualizarImpuestoCotizacion(Iddetalle, impuesto);
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnAplicarDescuentoTodo_Click(object sender, EventArgs e)
        {
            AplicarDescuentoProductoTodos();
        }
        private void ConfirmarCotizacion()
        {
            CalcularTotalCotizacion();
            var models = new Models.Mcotizacion()
            {
                Idcotizacion = Idcotizacion,
                Impuestos = TotalImpuestos,
                SubTotal = subTotalenCotizacion,
                Total = totalPagar,
                Descuento = descuento
            };
            Logica.Update.ActualizarSumaCotizacion(models);
        }
       
        private void befectivo_Click(object sender, EventArgs e)
        {
            ConfirmarCotizacion();
            var page = new Impresion();
            page.Idcliente = idcliente;
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
            Idcotizacion = 0;
            ListarDetalleCotizacion();
        }

        private void btnVistaCotizaciones_Click(object sender, EventArgs e)
        {
            var page = new CotizacionesRealizadas();
            page.ShowDialog();
        }
        private void ValidarExistenciaCotizacionCodigoBarra()
        {
            if (ObtenerDatos.ValidarExisteCotizacion() == false)
            {
                CrearNuevaCotizacion();
                dgProductos.Rows[0].Selected = true;
                InformacionYCotizar();
            }
            else
            {
                Idcotizacion = ObtenerDatos.ObtenerIdCotizacion();
                dgProductos.Rows[0].Selected = true;
                InformacionYCotizar();
            }

        }
        private void MostrarProductosCodeBAR()
        {
            try
            {
                dgProductos.Visible = true;
                if (txtBuscar.Text == "")
                {
                    lbltipodebusqueda2.Visible = true;
                    
                }
                if (txtBuscar.Text != "")
                {
                    lbltipodebusqueda2.Visible = false;
                    LISTAR_PRODUCTOS_BUSCADOR();
                    if (dgProductos.Rows.Count > 0)
                    {
                        ValidarExistenciaCotizacionCodigoBarra();
                    }
                    else
                    {
                        MessageBox.Show("No existe producto con este codigo: " + txtBuscar.Text, "Confirma:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                dgProductos.Visible = false;
                txtBuscar.Focus();
            }
            catch (Exception ex)
            {
            }
        }
        private void timerLeerCodigo_Tick(object sender, EventArgs e)
        {
            timerLeerCodigo.Stop();
            MostrarProductosCodeBAR();

        }
    }
}
