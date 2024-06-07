using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Datos;
using AlanVent_Sistema_De_Ventas.Logic;
using AlanVent_Sistema_De_Ventas.Presentation.Clientes_Proveedores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.Compras
{
    public partial class RealizarCompra : UserControl
    {
        public RealizarCompra()
        {
            InitializeComponent();
        }
        string estadocompra;
        int idproducto;
        Panel panel_mostrador_de_productos = new Panel();
        public static int idcompra = 0;
        public static int idproveedor;
        string sevendePor;
        double txtpantalla;
        int iddetallecompra;
        bool SECUENCIA = true;
        double Prec_Costo = 0;
        double precio_compra_nuevo;
        public static int IdProd_Editar;
        public static double TotalPagar;
        public static string Nombre_Proveedor;
        public static string EstadoRealizarCompra = "No";
        private string TipodeBusqueda;
        private string EstadoImpuestos;
        private string NombreImpuesto;
        private double PorcentajeImp;
        private string Moneda;
        public static double subtotal;
        public static double ImpuestoCalculado;
        int IdproveedorGenerico;
        public static double totalretencion;
        private void HistorialCompras_Load(object sender, EventArgs e)
        {
            CrearEventoControles();
            Obtener_datos.Mostrar_TipoBusqueda(ref TipodeBusqueda);
            ConfiguracionTipoDeBusqueda();
            MostrarIdProveedorGenerico();
            estadocompra = "COMPRA NUEVA";
            dibujarProveedores();
            eliminarComprasvacias();
        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtbuscar.Text == "")
            {
                ocultar_mostrar_productos();
            }
            else
            {
                mostrar_productos();
                ValidarTipoDeBusqueda();
            }


        }
        private void ValidarTipoDeBusqueda()
        {
            if (TipodeBusqueda == "TECLADO")
            {
                BuscarProductos();
            }
            else
            {
                timerLector.Start();
            }
        }
        private void BuscarProductos()
        {
            try
            {
                var dt = new DataTable();
                var funcion = new Dproductos();
                funcion.buscarProductos(ref dt, txtbuscar.Text);
                dgProductos.DataSource = dt;
                dgProductos.Columns[0].Visible = false;
                dgProductos.Columns[1].Visible = true;
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
            catch (Exception)
            {

            }

        }
        private void BuscarProductosCodigo()
        {
            try
            {
                var dt = new DataTable();
                var funcion = new Dproductos();
                funcion.BuscarProductosCodigo(ref dt, txtbuscar.Text);
                dgProductos.DataSource = dt;
                dgProductos.Columns[0].Visible = false;
                dgProductos.Columns[1].Visible = true;
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
            catch (Exception)
            {

            }

        }

        private void mostrar_productos()
        {
            panel_mostrador_de_productos.Size = new Size(594, 186);
            panel_mostrador_de_productos.BackColor = Color.White;
            panel_mostrador_de_productos.Visible = true;
            dgProductos.Visible = true;
            dgProductos.Dock = DockStyle.Fill;
            dgProductos.BackgroundColor = Color.White;
            lbltipodebusqueda2.Visible = false;
            panel_mostrador_de_productos.Controls.Add(dgProductos);
            this.Controls.Add(panel_mostrador_de_productos);
            panel_mostrador_de_productos.Location = new Point(238, 53);
            panel_mostrador_de_productos.BringToFront();

        }
        private void ocultar_mostrar_productos()
        {
            panel_mostrador_de_productos.Visible = false;
            dgProductos.Visible = false;
            lbltipodebusqueda2.Visible = true;
        }
        private void dibujarProveedores()
        {
            var funcion = new Dproveedores();
            DataTable dt = new DataTable();
            funcion.buscar_Proveedores(ref dt, txtBuscarproveedores.Text);
            FlowpanelProveedor.Controls.Clear();
            foreach (DataRow rdr in dt.Rows)
            {
                Button b = new Button();
                b.Size = new Size(217, 47);
                b.Text = rdr["Nombre"].ToString();
                b.Name = rdr["IdProveedor"].ToString();
                b.BackColor = Color.FromArgb(139, 139, 139);
                b.Font = new Font("Microsoft Sans Serif", 12);
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 0;
                b.ForeColor = Color.Black;
                FlowpanelProveedor.Controls.Add(b);
                b.Click += B_Click;
            }

        }

        private void B_Click(object sender, EventArgs e)
        {
            idproveedor = Convert.ToInt32(((Button)sender).Name);
            Nombre_Proveedor = ((Button)sender).Text;
            //Deseleccionar
            foreach (Button b in FlowpanelProveedor.Controls)
            {
                if (b is Button)
                {
                    b.BackColor = Color.FromArgb(139, 139, 139);
                }
            }
            //Seleccionar button
            foreach (Button b2 in FlowpanelProveedor.Controls)
            {
                if (b2 is Button)
                {
                    if (b2.Name == idproveedor.ToString())
                    {
                        b2.BackColor = Color.FromArgb(81, 175, 73);
                    }
                }
            }

        }

        private void txtBuscarproveedores_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarproveedores.Text == "")
            {
                lblProveedor.Visible = true;
            }
            else
            {
                lblProveedor.Visible = false;
            }
            foreach (Button b in FlowpanelProveedor.Controls)
            {
                if (b is Button)
                {
                    b.BackColor = Color.FromArgb(139, 139, 139);
                }
            }
            MostrarIdProveedorGenerico();
            dibujarProveedores();
        }

        private void dgProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ObtenerDatosProductos();
        }
        
        private void InsertarCompra()
        {
            var funcion = new Dcompras();
            var parametros = new Ldetallecompra();
            parametros.Estado = estadocompra;
            parametros.Cantidad = 1;
            parametros.Costo = Prec_Costo;
            parametros.Moneda = "-";
            parametros.Descripcion = txtbuscar.Text;
            parametros.IdProducto = idproducto;
            

            if (funcion.Insertar_Compras(parametros) == true)
            {
                estadocompra = "COMPRA GENERADA";
                mostrarUltimoIdcompra();
                mostrarDetallecompra();
            }


        }
        private void eliminarComprasvacias()
        {
            var funcion = new Dcompras();
            funcion.eliminarComprasvacias();

        }
        private void mostrarUltimoIdcompra()
        {
            var funcion = new Dcompras();
            funcion.MostrarUltimoIdcompra(ref idcompra);
        }
        private void mostrarDetallecompra()
        {
            var dt = new DataTable();
            var funcion = new Ddetallecompra();
            var parametros = new Ldetallecompra();
            parametros.IdCompra = idcompra;
            funcion.mostrar_DetalleCompra(ref dt, parametros);
            dgDetallecompra.DataSource = dt;
            dgDetallecompra.Columns[1].Visible = false;
            dgDetallecompra.Columns[2].Visible = false;
            dgDetallecompra.Columns[3].Visible = false;
            dgDetallecompra.Columns[8].Visible = false;
            dgDetallecompra.Columns[9].Visible = false;
            Bases.Multilinea(ref dgDetallecompra);
            sumar();


        }
        private void sumar()
        {
            try
            {
                int x;
                x = dgDetallecompra.Rows.Count;
                if (x == 0)
                {
                    txt_total_suma.Text = "0.00";
                }

                subtotal = 0;
                foreach (DataGridViewRow fila in dgDetallecompra.Rows)
                {
                    subtotal += Convert.ToDouble(fila.Cells["Total"].Value);
                }
                ConfirmarEstadoImpuestos();
                txtSubtotal.Text = AsignarComa(subtotal);
                txtImpuestos.Text = AsignarComa(ImpuestoCalculado);
                lblImpuestoRetenido.Text = AsignarComa(totalretencion);
                TotalPagar = (subtotal + ImpuestoCalculado)-totalretencion;
                txt_total_suma.Text = Convert.ToString(AsignarComa(TotalPagar));
            }
            catch (Exception ex)
            {


            }
        }
        private void CalcularRetencionITBIS()
        {
            double porcentajeRetencion = 100;
            //Esto calcula el porcentaje de forma decimal
            porcentajeRetencion = porcentajeRetencion / 100;

            totalretencion = ImpuestoCalculado * porcentajeRetencion; 
        }
        private string AsignarComa(double valor)
        {
            return String.Format("{0:#,##0.##}", valor);
        }
        private void ConfirmarEstadoImpuestos()
        {
            try
            {
                EstadoImpuestos = "";
                Obtener_datos.ObtenerEstadoImpuestos(ref EstadoImpuestos);
                if (EstadoImpuestos == "SI")
                {
                    CALCULAR_Impuesto();
                    CalcularRetencionITBIS();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void CALCULAR_Impuesto()
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
                double Cantidad_Decimal_Impuesto = Math.Ceiling(PorcentajeImp) / 100;
                ImpuestoCalculado = subtotal * Cantidad_Decimal_Impuesto;
            }
            catch (Exception ex)
            {

            }
        }
        private void dgDetallecompra_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            iddetallecompra = Convert.ToInt32(dgDetallecompra.SelectedCells[1].Value);
            idproducto = Convert.ToInt32(dgDetallecompra.SelectedCells[3].Value);
            sevendePor = (dgDetallecompra.SelectedCells[8].Value.ToString());
            if (e.ColumnIndex == dgDetallecompra.Columns["EL"].Index)
            {
                eliminar_detalle_compra();
            }
        }
        private void eliminar_detalle_compra()
        {
            var funcion = new Ddetallecompra();
            var parametros = new Ldetallecompra();
            parametros.IdDetallecompra = iddetallecompra;
            funcion.eliminar_detalle_compra(parametros);
            mostrarDetallecompra();

        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtmonto.Text = txtmonto.Text + "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtmonto.Text = txtmonto.Text + "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtmonto.Text = txtmonto.Text + "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtmonto.Text = txtmonto.Text + "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtmonto.Text = txtmonto.Text + "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtmonto.Text = txtmonto.Text + "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtmonto.Text = txtmonto.Text + "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtmonto.Text = txtmonto.Text + "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtmonto.Text = txtmonto.Text + "9";
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtmonto.Text = txtmonto.Text + "0";
        }

        private void btnSeparador_Click(object sender, EventArgs e)
        {
            if (SECUENCIA == true)
            {
                txtmonto.Text = txtmonto.Text + ".";
                SECUENCIA = false;


            }
            else
            {
                return;
            }
        }

        private void btnborrartodo_Click(object sender, EventArgs e)
        {
            txtmonto.Clear();
            SECUENCIA = true;
        }

        private void btncantidad_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtmonto.Text))
            {
                if (dgDetallecompra.RowCount > 0)
                {
                    iddetallecompra = Convert.ToInt32(dgDetallecompra.SelectedCells[1].Value);
                    idproducto = Convert.ToInt32(dgDetallecompra.SelectedCells[3].Value);
                    sevendePor = (dgDetallecompra.SelectedCells[8].Value.ToString());
                    if (sevendePor == "Unidad")
                    {
                        string cadena = txtmonto.Text;
                        if (cadena.Contains("."))
                        {
                            MessageBox.Show("Este Producto no acepta decimales ya que esta configurado para ser vendido por UNIDAD", "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        }
                        else
                        {
                            BotonCantidad();
                        }


                    }
                    else
                    {
                        BotonCantidad();
                    }

                }
                else
                {
                    txtmonto.Clear();
                    txtmonto.Focus();
                }

            }
        }

        private void btnprecio_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtmonto.Text))
            {

                editar_detalle_compra_Precio();
            }
            else
            {
                txtmonto.Clear();
                txtmonto.Focus();
            }
        }
        private void editar_detalle_compra_Precio()
        {
            try
            {
                txtpantalla = Convert.ToDouble(txtmonto.Text);
                var funcion = new Ddetallecompra();
                var parametros = new Ldetallecompra();
                parametros.IdCompra = idcompra;
                parametros.IdProducto = idproducto;
                parametros.Costo = txtpantalla;
                funcion.editar_detalle_compra_Precio(parametros);
                mostrarDetallecompra();
                Editar_Precio_compra_producto();

                txtmonto.Clear();
            }
            catch (Exception ex)
            {

            }


        }

        private void Editar_Precio_compra_producto()
        {
            IdProd_Editar = idproducto;
            precio_compra_nuevo = Convert.ToDouble(txtmonto.Text);
            int vuelta = 0;
            Editar_datos.Editar_precio_compra_producto(precio_compra_nuevo, idproducto, ref vuelta);
            if (vuelta > 0)
            {
                DialogResult dr = MessageBox.Show("Precio de compra actualizado correctamente, ¿Deseas actualizar el precio de venta ahora?", "Edicion Rapida:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    Actualizar_PrecioVenta frm = new Actualizar_PrecioVenta();
                    frm.ShowDialog();
                }
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            if (dgDetallecompra.Rows.Count > 0)
            {
                //Validacion para seleccionar un proveedor
                if (idproveedor == IdproveedorGenerico || idproveedor == 0)
                {
                    MessageBox.Show("Selecciona un proveedor para esta compra.", "Confirma:",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    EstadoRealizarCompra = "No";
                    var frm = new MediosPago();
                    frm.FormClosing += Frm_FormClosing;
                    frm.ShowDialog();
                }
                
            }
            else
            {
                MessageBox.Show("No se esta efectuando ninguna compra", "Confirma:",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (EstadoRealizarCompra == "SI")
            {
                Limpiar();
            }
        }

        private void txtmonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtmonto, e);

        }
        private void BotonCantidad()
        {
            txtpantalla = Convert.ToDouble(txtmonto.Text);
            double Cantidad;
            Cantidad = Convert.ToDouble(dgDetallecompra.SelectedCells[5].Value);
            string ControlStock;
            ControlStock = dgDetallecompra.SelectedCells[9].Value.ToString();
            if (ControlStock == "SI")
            {
                editar_detalle_compra_Cantidad();
                txtmonto.Clear();
            }
            else
            {
                MessageBox.Show("Este producto no maneja inventario, adaptalo para manejar stock");
            }
        }
        private void editar_detalle_compra_Cantidad()
        {
            var funcion = new Ddetallecompra();
            var parametros = new Ldetallecompra();
            parametros.IdCompra = idcompra;
            parametros.IdProducto = idproducto;
            parametros.Cantidad = txtpantalla;
            funcion.editar_detalle_compra_Cantidad(parametros);
            mostrarDetallecompra();

        }

        private void btnagregarPro_Click(object sender, EventArgs e)
        {
            var frm = new Proveedores();
            frm.FormClosed += Frm_FormClosed;
            frm.ShowDialog();
        }
        private void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            dibujarProveedores();
        }

        private void MostrarIdProveedorGenerico()
        {
            Obtener_datos.MostrarIdProveedorGenerico(ref idproveedor);
            Obtener_datos.MostrarIdProveedorGenerico(ref IdproveedorGenerico);
            Nombre_Proveedor = "Generico";
        }
        private void Limpiar()
        {
            txtmonto.Clear();
            txtbuscar.Clear();
            dibujarProveedores();
            MostrarIdProveedorGenerico();
            idcompra = 0;
            mostrarDetallecompra();
            estadocompra = "COMPRA NUEVA";
        }
        private void txtbuscar_Click(object sender, EventArgs e)
        {

            txtbuscar.SelectAll();
        }

        private void PanelOperaciones_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblProveedor_Click(object sender, EventArgs e)
        {
            txtBuscarproveedores.Focus();
        }

        private void lbltipodebusqueda2_Click(object sender, EventArgs e)
        {
            txtbuscar.Focus();
        }

        private void dgDetallecompra_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void ConfiguracionTipoDeBusqueda()
        {
            if (TipodeBusqueda == "LECTORA")
            {
                ModoLectora();
            }
            if (TipodeBusqueda == "TECLADO")
            {
                ModoTeclado();
            }
        }
        private void btnTeclado_Click(object sender, EventArgs e)
        {
            ModoTeclado();
        }
        private void ModoTeclado()
        {
            TipodeBusqueda = "TECLADO";
            btnTeclado.BackColor = Color.WhiteSmoke;
            btnLectora.BackColor = Color.White;
            txtbuscar.Clear();
            lbltipodebusqueda2.Text = "Buscar producto con Teclado...";
            txtbuscar.Focus();
        }
        private void ModoLectora()
        {
            TipodeBusqueda = "LECTORA";
            btnTeclado.BackColor = Color.White;
            btnLectora.BackColor = Color.WhiteSmoke;
            txtbuscar.Clear();
            lbltipodebusqueda2.Text = "Buscar producto con Lectora...";
            txtbuscar.Focus();
        }
        private void btnLectora_Click(object sender, EventArgs e)
        {
            ModoLectora();
        }
        private void ObtenerDatosProductos()
        {
            Prec_Costo = Convert.ToDouble(dgProductos.SelectedCells[5].Value);
            idproducto = Convert.ToInt32(dgProductos.SelectedCells[1].Value);
            panel_mostrador_de_productos.Visible = false;
            string usaInventarios = dgProductos.SelectedCells[3].Value.ToString();
            if (usaInventarios == "NO")
            {
                MessageBox.Show("Este producto no maneja inventario, adaptalo para manejar stock",
                    "Verifica", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtbuscar.SelectAll();
            }
            else
            {
                InsertarCompra();
                txtbuscar.Clear();
            }

        }
        private void timerLector_Tick(object sender, EventArgs e)
        {
            timerLector.Stop();
            BuscarProductosCodigo();
            if (dgProductos.Rows.Count > 0)
            {
                ObtenerDatosProductos();
            }
            else
            {
                string codigo = txtbuscar.Text;
                txtbuscar.Clear();
                MessageBox.Show("No existe ningun producto con este codigo '" + codigo + "'",
                    "Confirma:", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        private void EventosBusqueda(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                ModoTeclado();
            }
            if (e.KeyCode == Keys.F2)
            {
                ModoLectora();
            }
        }
        private void CrearEventoControles()
        {
            foreach (Control item in this.Controls)
            {
                item.KeyDown += EventoTeclas_KeyDown;
            }
        }

        private void EventoTeclas_KeyDown(object sender, KeyEventArgs e)
        {
            EventosBusqueda(e);
        }

        private void txtbuscar_KeyDown(object sender, KeyEventArgs e)
        {
            EventosBusqueda(e);
        }

        private void txtmonto_KeyDown(object sender, KeyEventArgs e)
        {
            EventosBusqueda(e);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
