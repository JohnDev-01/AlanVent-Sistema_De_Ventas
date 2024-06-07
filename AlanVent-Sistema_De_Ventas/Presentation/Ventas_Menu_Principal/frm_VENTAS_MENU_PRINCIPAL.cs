using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;
using AlanVent_Sistema_De_Ventas.Presentation.Inventarios_Kardex;
using AlanVent_Sistema_De_Ventas.Presentation.Membrecias;
using AlanVent_Sistema_De_Ventas.Presentation.Pagos;
using AlanVent_Sistema_De_Ventas.Presentation.Productos;
using DocumentFormat.OpenXml.Office2013.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace AlanVent_Sistema_De_Ventas.Presentation.Ventas_Menu_Principal
{
    public partial class frm_VENTAS_MENU_PRINCIPAL : Form
    {
        public frm_VENTAS_MENU_PRINCIPAL()
        {
            InitializeComponent();
        }
        int contador_stock_detalle_venta;
        int Id_producto;
        int idClienteEstandar;
        public static int idusuario_que_inicio_sesion;
        public static int Idventa;
        int iddetalleventa;
        int contador;
        public static double total;
        public static double txtpantalla;
        double lblStock_de_Productos;
        Panel panel_mostrador_de_productos = new Panel();
        string serial_pc;
        string sevendePor;
        double ImpuestoPorProductoUnitario;
        double subtotalDgvProductos;
        string txtventagenerada;
        double txtprecio_unitario;
        string Usa_Inventarios;
        string resultado_licencia;
        string fechaFinal;
        double cantidad;
        string Tema;
        int contadorVentasEspera;
        string Ip;
        bool EstadoCobrar = false;
        public static bool EstadoMediosPago = false;
        bool estadoLoginVendedor;
        bool estadoCerrarVentanabtnAdmin;
        //Impuestos---
        double totalpagar;
        string NombreImpuesto;
        double PorcentajeImp;
        string Moneda;
        string EstadoImpuestos;
        public static double subtotal;
        public string EstadoInicioAdmin = "NO";
        double Cantidad_Decimal_Impuesto = 0;
        double ImpuestoCalculado;
        //Disenos:
        Panel pl = new Panel();
        FlowLayoutPanel flow = new FlowLayoutPanel();
        bool estadoCerrarSistema = false;
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (Tipo_de_Busqueda == "LECTORA")
            {

                lbltipodebusqueda2.Visible = false;
                panel_mostrador_de_productos.Visible = false;
                TimerBUSCADORcodigodebarras.Start();
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
        private string AsignarComa(double valor)
        {
            return String.Format("{0:#,##0.##}", valor);
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
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Productos nd = new frm_Productos();
            nd.ShowDialog();
        }

        private void btnCerrarTurno_Click(object sender, EventArgs e)
        {
            Dispose();
            Caja.CierreDeCaja frm = new Caja.CierreDeCaja();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Inventarios_Kardex.Inventarios_menu c = new Inventarios_Kardex.Inventarios_menu();
            c.ShowDialog();
        }
        private void ValidarLicencia()
        {
            DLicencias funcion = new DLicencias();

            funcion.ValidarLicencias(ref resultado_licencia, ref fechaFinal);
            if (resultado_licencia == "VENCIDA")
            {
                funcion.EditarMarcanVencidas();
                Dispose();
                Membresias_Nuevo frm = new Membresias_Nuevo();
                frm.ShowDialog();
            }

        }
        private void frm_VENTAS_MENU_PRINCIPAL_Load(object sender, EventArgs e)
        {
            Eliminar_datos.EliminarVentasSeQuedaronAbiertas();
            ConfiguracionDiseno_Vendedor();
            Obtener_datos.Mostrar_TipoBusqueda(ref Tipo_de_Busqueda);
            ValidarTipoBusqueda();
            ValidarTemaCaja();
            Limpiar_para_venta_nueva();
            ValidarLicencia();
            Bases.Cambiar_idioma_regional();
            Bases.Obtener_serialPc(ref serial_pc);
            DataAccess.Obtener_datos.Obtener_id_caja_PorSerial(ref Id_caja);
            Obtener_id_de_cliente_estandar();
            Obtener_datos.mostrar_inicio_De_sesion(ref idusuario_que_inicio_sesion);
            ObtenerIpLocal();
            SaberSIEsAdmin();
            pl.Visible = false;
            txtBuscar.Focus();
            estadoCerrarSistema = true;
            estadoCerrarVentanabtnAdmin = false;
        }
        private void ConfiguracionDiseno_Vendedor()
        {
            var rol = Obtener_datos.ObtenerRolPorUsuario();
            if (rol == "Solo Ventas (no esta autorizado para manejar dinero)")
            {
                panelBotonesOpcionales.Visible = false;
                panelbtnCobrar.Visible = false;
                btnCerrarTurno.Visible = false;
                btndevoluciones.Visible = false;
                panelRestaurarVentaDiseno.Visible = false;
                panelVisorPrecios.Visible = true;
                estadoLoginVendedor = true;
                btnIngresoProd.Visible = false;
                btnSalidaProd.Visible = false;

            }
            else
            {
                panelBotonesOpcionales.Visible = true;
                panelbtnCobrar.Visible = true;
                btnCerrarTurno.Visible = true;
                btndevoluciones.Visible = true;
                panelRestaurarVentaDiseno.Visible = true;
                estadoLoginVendedor = false;
                panelVisorPrecios.Visible = false;
                btnIngresoProd.Visible = true;
                btnSalidaProd.Visible = true;
            }
        }

        private void SaberSIEsAdmin()
        {
            string estado = "";
            Obtener_datos.MostrarVerificarSiElUsuarioAdministrador(ref estado);
            if (EstadoInicioAdmin != "SI")
            {
                if (estado == "SI")
                {
                    btnAdministrador.Visible = true;
                }
                else
                {
                    btnAdministrador.Visible = false;
                }
            }

        }
        private void ObtenerIpLocal()
        {
            Bases.ObtenerIp(ref Ip);
            this.Text = "IP Para Conexion Remota: " + Ip;
        }
        private void ContarVentasEspera()
        {
            Obtener_datos.contarVentasEspera(ref contadorVentasEspera);
            if (contadorVentasEspera == 0)
            {
                panelNotificacionEspera.Visible = false;
            }
            else
            {
                if (estadoLoginVendedor == false)
                {
                    panelNotificacionEspera.Visible = true;
                    lblContadorEspera.Text = contadorVentasEspera.ToString();
                }
                else
                {
                    panelNotificacionEspera.Visible = false;
                }
            }
        }
        private void ValidarTemaCaja()
        {
            Obtener_datos.mostrarTemaCaja(ref Tema);
            if (Tema == "Redentor")
            {
                pbIcono.Image = Properties.Resources.IMG_4631;
                pbIcono2.Image = Properties.Resources.IMG_4631;

                TemaClaro();
                IndicadorTema.Checked = false;
            }
            else
            {
                pbIcono.Image = Properties.Resources.IconoPngLineaBlanca;
                pbIcono2.Image = Properties.Resources.IconoPngLineaBlanca;
                TemaOscuro();
                IndicadorTema.Checked = true;
            }
            DisenarPanelHerramientas();
        }

        private void ValidarTipoBusqueda()
        {


            if (Tipo_de_Busqueda == "TECLADO")
            {
                lbltipodebusqueda2.Text = "Buscar con Teclado...";
                btnTeclado.BringToFront();
                if (IndicadorTema.Checked == true)
                {
                    btnLectora.BackColor = Color.FromArgb(39, 39, 39);
                    btnLectora.ForeColor = Color.White;
                    btnTeclado.ForeColor = Color.Black;
                    btnTeclado.BackColor = Color.LightGreen;
                }
                else
                {
                    btnLectora.ForeColor = Color.Black;
                    btnTeclado.ForeColor = Color.Black;
                    btnLectora.BackColor = Color.WhiteSmoke;
                    btnTeclado.BackColor = Color.LightGreen;
                }

            }
            else
            {
                btnLectora.BringToFront();
                if (IndicadorTema.Checked == true)
                {
                    lbltipodebusqueda2.Text = "Buscar con Lectora de Codigo de Barras...";
                    btnLectora.BackColor = Color.LightGreen;
                    btnTeclado.BackColor = Color.FromArgb(39, 39, 39);
                    btnLectora.ForeColor = Color.Black;
                    btnTeclado.ForeColor = Color.White;
                    txtBuscar.Clear();
                    txtBuscar.Focus();

                }
                else
                {
                    lbltipodebusqueda2.Text = "Buscar con Lectora de Codigo de Barras...";
                    btnLectora.BackColor = Color.LightGreen;
                    btnTeclado.BackColor = Color.WhiteSmoke;
                    btnLectora.ForeColor = Color.Black;
                    btnTeclado.ForeColor = Color.Black;
                    txtBuscar.Clear();
                    txtBuscar.Focus();

                }

            }
        }
        private void Limpiar_para_venta_nueva()
        {
            Idventa = 0;
            ListarProductosAgregados();
            txtventagenerada = "VENTA NUEVA";
            Sumar();
            PanelEnEspera.Visible = false;
            panelBienvenida.Visible = true;
            PanelOperaciones.Visible = false;
            ContarVentasEspera();
            EstadoMediosPago = false;
        }
        private void Sumar()
        {
            try
            {

                int x;
                x = dgDetalleVenta.Rows.Count;
                if (x == 0)
                {
                    txt_total_suma.Text = "Total: 0.00";
                }

                totalpagar = 0;
                subtotal = 0;
                ImpuestoCalculado = 0;
                foreach (DataGridViewRow fila in dgDetalleVenta.Rows)
                {
                    totalpagar += Convert.ToDouble((fila.Cells["Importe"].Value));
                    subtotal += Convert.ToDouble((fila.Cells["subtotal"].Value));
                    ImpuestoCalculado += Convert.ToDouble((fila.Cells["TotalImpuestos"].Value));
                }


                DataTable dt = new DataTable();
                Obtener_datos.MostrarDatosImpuestos(ref dt);
                foreach (DataRow item in dt.Rows)
                {
                    NombreImpuesto = item["Impuesto"].ToString();
                    PorcentajeImp = Convert.ToDouble(item["Porcentaje_impuesto"].ToString());
                    Moneda = item["Moneda"].ToString();
                }
                lblsubtotal.Text = Moneda + AsignarComa(subtotal);
                lblNombreImpuestos.Text = NombreImpuesto + " (" + PorcentajeImp + "%) :";

                totalpagar = Math.Round(totalpagar, 2);
                lblImpuestos.Text = Moneda + AsignarComa(ImpuestoCalculado);
                txt_total_suma.Text = $"Total: {Moneda}" + AsignarComa(totalpagar);

                //Descuento por venta 
                var descuento = Obtener_datos.MostrarDescuentoPorVenta(Idventa);
                lblDescuento.Text = descuento;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void LISTAR_PRODUCTOS_BUSCADOR()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                DataAccess.ConexionMaestra.abrir();
                da = new SqlDataAdapter("BUSCAR_PRODUCTOS_oka", DataAccess.ConexionMaestra.conexion);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", txtBuscar.Text);
                da.Fill(dt);
                dgProductos.DataSource = dt;
                DataAccess.ConexionMaestra.cerrar();

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
                dgProductos.Columns[11].Visible = false;
                dgProductos.Columns[12].Visible = false;

            }
            catch (Exception ex)
            {
                DataAccess.ConexionMaestra.cerrar();
            }
        }
        public static int Id_caja;

        string Tipo_de_Busqueda;


        private void btnTeclado_Click(object sender, EventArgs e)
        {
            Modo_Teclado();
        }
        private void Modo_Teclado()
        {
            Ocultar_Productos();
            lbltipodebusqueda2.Text = "Buscar con Teclado...";
            Tipo_de_Busqueda = "TECLADO";
            ValidarTipoBusqueda();
            txtBuscar.Clear();
            txtBuscar.Focus();
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
            ValidarTipoBusqueda();
            txtBuscar.Clear();
            txtBuscar.Focus();
        }
        private void lbltipodebusqueda2_Click(object sender, EventArgs e)
        {
            txtBuscar.Focus();
        }

        private void DATALISTADO_PRODUCTOS_OKA_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Vender_por_Teclado();
            }
            catch (Exception ex)
            {
            }

        }
        private void Obtener_id_venta_recien_creada()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
            cn.Open();
            SqlCommand cmd = new SqlCommand("Mostrar_id_venta_por_id_caja", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id_caja", Id_caja);
            try
            {
                Idventa = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            cn.Close();
        }


        private void Vender_por_Teclado()
        {
            Id_producto = Convert.ToInt32(dgProductos.SelectedCells[1].Value.ToString());
            txtEnBusqueda.Text = dgProductos.SelectedCells[10].Value.ToString();
            //mostramos los registros del producto en el detalle de venta
            Mostrar_stock_detalle_de_venta();
            Contar_Stock_detalle_venta();
            if (contador_stock_detalle_venta == 0)
            {
                var StockDgv = dgProductos.SelectedCells[4].Value.ToString();
                if (StockDgv == "99999")
                {
                    StockDgv = "99999";
                }
                //Si el producto no esta agregado a las ventas se tomara el stockExistenteDetalle de la tabla Productos
                lblStock_de_Productos = Convert.ToDouble(StockDgv);
            }
            else
            {

                // en caso de que el producto ya este agregado en el detalle de venta se va a extraer de la tabla detalle de venta
                lblStock_de_Productos = Convert.ToDouble(datalistado_stock_detalle_venta.SelectedCells[1].Value.ToString());
            }
            //Extraemos los datos del producto de la tabla productos directamente
            Usa_Inventarios = dgProductos.SelectedCells[3].Value.ToString();
            lblDescripcion.Text = dgProductos.SelectedCells[9].Value.ToString();
            lblcodigo.Text = dgProductos.SelectedCells[10].Value.ToString();
            lblCosto.Text = dgProductos.SelectedCells[5].Value.ToString();
            txtprecio_unitario = Convert.ToDouble(dgProductos.SelectedCells[6].Value.ToString());
            sevendePor = dgProductos.SelectedCells[8].Value.ToString();
            ImpuestoPorProductoUnitario = Convert.ToDouble(dgProductos.SelectedCells[11].Value.ToString());
            subtotalDgvProductos = Convert.ToDouble(dgProductos.SelectedCells[12].Value.ToString());

            //preguntamos que tipo de producto sera el que se agregue al detalle de venta
            if (sevendePor == "Granel")
            {
                vender_a_granel();
            }
            else if (sevendePor == "Unidad")
            {

                txtpantalla = 1;
                vender_por_unidad();
            }
            txtBuscar.Focus();
        }
        private void vender_a_granel()
        {
            Cantidad_a_granel frm = new Cantidad_a_granel();
            frm.precio_unitario = txtprecio_unitario;
            frm.descripcion = lblDescripcion.Text;
            frm.FormClosing += frmb_FormClosing;
            frm.ShowDialog();

        }

        private void frmb_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool Validation = Cantidad_a_granel.productQuantityValidator;
            if (Validation == true)
            {
                ejecutar_ventas_a_granel();
            }
            else
            {
                ListarProductosAgregados();
                txtBuscar.Text = "";
                txtBuscar.Focus();
            }
        }

        public void ejecutar_ventas_a_granel()
        {

            ejecutar_insertar_ventas();
            if (txtventagenerada == "VENTA GENERADA")
            {
                insertar_detalle_venta();
                ListarProductosAgregados();
                txtBuscar.Text = "";
                txtBuscar.Focus();

            }

        }
        private void Obtener_id_de_cliente_estandar()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = DataAccess.ConexionMaestra.conexion;
            SqlCommand com = new SqlCommand("select idclientev  from Clientes where Estado='0'", con);
            try
            {
                con.Open();
                idClienteEstandar = Convert.ToInt32(com.ExecuteScalar());
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void vender_por_unidad()
        {

            try
            {
                if (txtEnBusqueda.Text == dgProductos.SelectedCells[10].Value.ToString())
                {
                    dgProductos.Visible = false;
                    ejecutar_insertar_ventas();
                    //Si la venta esta generada ya continua 
                    if (txtventagenerada == "VENTA GENERADA")
                    {
                        //se inserta el detalle de venta y luego se muestra:
                        insertar_detalle_venta();
                        ListarProductosAgregados();
                        txtBuscar.Text = "";
                        txtBuscar.Clear();
                        dgProductos.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void ejecutar_insertar_ventas()
        {
            if (txtventagenerada == "VENTA NUEVA")
            {
                //Aqui se identifica si la venta es nueva 
                //Luego se crea la venta y se el estado cambia a que esta creada
                try
                {

                    SqlConnection cn = new SqlConnection();
                    cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("insertar_venta", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idcliente", idClienteEstandar);
                    cmd.Parameters.AddWithValue("@fecha_venta", DateTime.Today);
                    cmd.Parameters.AddWithValue("@nume_documento", 0);
                    cmd.Parameters.AddWithValue("@montototal", 0);
                    cmd.Parameters.AddWithValue("@Tipo_de_pago", 0);
                    cmd.Parameters.AddWithValue("@estado", "EN ESPERA VENDIENDO");
                    cmd.Parameters.AddWithValue("@IGV", 0);
                    cmd.Parameters.AddWithValue("@Comprobante", 0);
                    cmd.Parameters.AddWithValue("@id_usuario", idusuario_que_inicio_sesion);
                    cmd.Parameters.AddWithValue("@Fecha_de_pago", DateTime.Today);
                    cmd.Parameters.AddWithValue("@ACCION", "VENTA");
                    cmd.Parameters.AddWithValue("@Saldo", 0);
                    cmd.Parameters.AddWithValue("@Pago_con", 0);
                    cmd.Parameters.AddWithValue("@Porcentaje_IGV", 0);
                    cmd.Parameters.AddWithValue("@Id_caja", Id_caja);
                    cmd.Parameters.AddWithValue("@Referencia_tarjeta", 0);
                    cmd.Parameters.AddWithValue("@Serializacion", "-");
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    Obtener_id_venta_recien_creada();
                    txtventagenerada = "VENTA GENERADA";
                    mostrar_panel_de_cobro();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void mostrar_panel_de_cobro()
        {
            panelBienvenida.Visible = false;
            PanelOperaciones.Visible = true;
        }
        private void insertar_detalle_venta()
        {
            try
            {


                if (Usa_Inventarios == "SI")
                {
                    if (lblStock_de_Productos >= txtpantalla)
                    {
                        Insertar_detalle_venta_Validado();
                    }
                    else
                    {
                        TimerLABEL_STOCK.Start();
                    }
                }
                else if (Usa_Inventarios == "NO")
                {
                    Inserta_detalle_venta_SIN_VALIDAR();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Insertar_detalle_venta_Validado()
        {

            try
            {

                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();
                SqlCommand cmd = new SqlCommand("insertar_detalle_venta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idventa", Idventa);
                cmd.Parameters.AddWithValue("@Id_presentacionfraccionada", Id_producto);
                cmd.Parameters.AddWithValue("@cantidad", txtpantalla);
                cmd.Parameters.AddWithValue("@preciounitario", txtprecio_unitario);

                cmd.Parameters.AddWithValue("@moneda", 0);
                cmd.Parameters.AddWithValue("@unidades", "Unidad");
                cmd.Parameters.AddWithValue("@Cantidad_mostrada", txtpantalla);
                cmd.Parameters.AddWithValue("@Estado", "EN ESPERA");
                cmd.Parameters.AddWithValue("@Descripcion", lblDescripcion.Text);
                cmd.Parameters.AddWithValue("@Codigo", lblcodigo.Text);
                cmd.Parameters.AddWithValue("@Stock", lblStock_de_Productos);
                cmd.Parameters.AddWithValue("@Se_vende_a", sevendePor);
                cmd.Parameters.AddWithValue("@Usa_inventarios", Usa_Inventarios);
                cmd.Parameters.AddWithValue("@Costo", lblCosto.Text);
                cmd.Parameters.AddWithValue("@ImpuestoUnitario", ImpuestoPorProductoUnitario);
                cmd.Parameters.AddWithValue("@subtotal", subtotalDgvProductos);
                cmd.ExecuteNonQuery();
                cn.Close();
                disminuir_stock_en_detalle_de_venta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void disminuir_stock_en_detalle_de_venta()
        {
            try
            {
                DataAccess.ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("disminuir_stock_en_detalle_de_venta", DataAccess.ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Producto1", Id_producto);
                cmd.Parameters.AddWithValue("@cantidad", txtpantalla);
                cmd.ExecuteNonQuery();
                DataAccess.ConexionMaestra.cerrar();
            }
            catch (Exception)
            {


            }
        }
        private void Inserta_detalle_venta_SIN_VALIDAR()
        {

            try
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();
                SqlCommand cmd = new SqlCommand("insertar_detalle_venta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idventa", Idventa);
                cmd.Parameters.AddWithValue("@Id_presentacionfraccionada", Id_producto);
                cmd.Parameters.AddWithValue("@cantidad", txtpantalla);
                cmd.Parameters.AddWithValue("@preciounitario", txtprecio_unitario);

                cmd.Parameters.AddWithValue("@moneda", 0);
                cmd.Parameters.AddWithValue("@unidades", "Unidad");
                cmd.Parameters.AddWithValue("@Cantidad_mostrada", txtpantalla);
                cmd.Parameters.AddWithValue("@Estado", "EN ESPERA");
                cmd.Parameters.AddWithValue("@Descripcion", lblDescripcion.Text);
                cmd.Parameters.AddWithValue("@Codigo", lblcodigo.Text);
                cmd.Parameters.AddWithValue("@Stock", lblStock_de_Productos);
                cmd.Parameters.AddWithValue("@Se_vende_a", sevendePor);
                cmd.Parameters.AddWithValue("@Usa_inventarios", Usa_Inventarios);
                cmd.Parameters.AddWithValue("@Costo", lblCosto.Text);
                cmd.Parameters.AddWithValue("@ImpuestoUnitario", ImpuestoPorProductoUnitario);
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.StackTrace + ex.Message + "  ---");
            }
        }
        private void Contar_Stock_detalle_venta()
        {
            int x;
            x = datalistado_stock_detalle_venta.Rows.Count;
            contador_stock_detalle_venta = x;
        }

        private void Mostrar_stock_detalle_de_venta()
        {
            try
            {
                string serial = "";
                Bases.Obtener_serialPc(ref serial);
                DataTable dt = new DataTable();
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_stock_de_detalle_de_ventas_PORCAJA", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id_producto", Id_producto);
                da.SelectCommand.Parameters.AddWithValue("@serial", serial);
                da.Fill(dt);
                datalistado_stock_detalle_venta.DataSource = dt;
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ListarProductosAgregados()
        {
            DataTable dt = new DataTable();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter("mostrar_productos_agregados_a_venta", cn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@idventa", Idventa);
            da.Fill(dt);
            dgDetalleVenta.DataSource = dt;
            cn.Close();
            dgDetalleVenta.Columns[0].Width = 50;
            dgDetalleVenta.Columns[1].Width = 50;
            dgDetalleVenta.Columns[2].Width = 50;
            dgDetalleVenta.Columns[3].Visible = false;
            dgDetalleVenta.Columns[4].Width = 250;
            dgDetalleVenta.Columns[5].Width = 100;
            dgDetalleVenta.Columns[6].Width = 100;
            dgDetalleVenta.Columns[7].Width = 100;
            dgDetalleVenta.Columns[8].Visible = false;
            dgDetalleVenta.Columns[9].Visible = false;
            dgDetalleVenta.Columns[10].Visible = false;
            dgDetalleVenta.Columns[11].Width = dgDetalleVenta.Width - (dgDetalleVenta.Columns[0].Width - dgDetalleVenta.Columns[1].Width - dgDetalleVenta.Columns[2].Width -
            dgDetalleVenta.Columns[4].Width - dgDetalleVenta.Columns[5].Width - dgDetalleVenta.Columns[6].Width - dgDetalleVenta.Columns[7].Width);
            dgDetalleVenta.Columns[12].Visible = false;
            dgDetalleVenta.Columns[13].Visible = false;
            dgDetalleVenta.Columns[14].Visible = false;
            dgDetalleVenta.Columns[15].Visible = false;
            dgDetalleVenta.Columns[16].Visible = false;
            dgDetalleVenta.Columns[17].Visible = false;
            dgDetalleVenta.Columns[18].Visible = false;
            //Esta columna es la de los impuestos:
            dgDetalleVenta.Columns[19].Visible = false;
            //Columna subtotal
            dgDetalleVenta.Columns[20].Visible = false;

            if (Tema == "Redentor")
            {
                Bases.Multilinea(ref dgDetalleVenta);
            }
            else
            {
                Bases.MultilineaTemaOscuro(ref dgDetalleVenta);
            }

            Sumar();
        }
        private void ejecutar_editar_detalle_venta_sumar(double cantidadAumentar)
        {
            try
            {
                SqlCommand cmd;
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();
                cmd = new SqlCommand("editar_detalle_venta_sumar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_producto", Id_producto);
                cmd.Parameters.AddWithValue("@cantidad", cantidadAumentar);
                cmd.Parameters.AddWithValue("@Cantidad_mostrada", cantidadAumentar);
                cmd.Parameters.AddWithValue("@Id_venta", Idventa);
                cmd.ExecuteNonQuery();

                cn.Close();
            }
            catch (Exception ex)
            {

            }
        }
        private void editar_detalle_de_venta_sumar(double cantidadaumentar)
        {
            try
            {

                if (Usa_Inventarios == "SI")
                {
                    lblStock_de_Productos = Convert.ToDouble(dgDetalleVenta.SelectedCells[15].Value.ToString());
                    if (lblStock_de_Productos > 0)
                    {

                        ejecutar_editar_detalle_venta_sumar(cantidadaumentar);
                        disminuir_stock_en_detalle_de_venta();
                    }
                    else
                    {
                        TimerLABEL_STOCK.Start();
                    }

                }
                else
                {
                    ejecutar_editar_detalle_venta_sumar(cantidadaumentar);
                }
                ListarProductosAgregados();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void ejecutar_editar_detalle_de_venta_restar()
        {
            try
            {
                SqlCommand cmd;
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();
                cmd = new SqlCommand("editar_detalle_venta_restar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iddetalle_venta", iddetalleventa);
                cmd.Parameters.AddWithValue("@cantidad", txtpantalla);
                cmd.Parameters.AddWithValue("@Cantidad_mostrada", txtpantalla);
                cmd.Parameters.AddWithValue("@Id_producto", Id_producto);
                cmd.Parameters.AddWithValue("@Id_venta", Idventa);
                cmd.ExecuteNonQuery();

                cn.Close();
                ListarProductosAgregados();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void aumentar_stock_en_detalle_de_venta()
        {
            try
            {
                DataAccess.ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("aumentar_stock_en_detalle_de_venta", DataAccess.ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Producto1", Id_producto);
                cmd.Parameters.AddWithValue("@cantidad", txtpantalla);
                cmd.ExecuteNonQuery();
                DataAccess.ConexionMaestra.cerrar();
            }
            catch (Exception)
            {


            }
        }
        private void editar_detalle_de_venta_restar()
        {
            try
            {
                
                if (Usa_Inventarios == "SI")
                {

                    ejecutar_editar_detalle_de_venta_restar();
                    aumentar_stock_en_detalle_de_venta();


                }
                else
                {
                    ejecutar_editar_detalle_venta_sumar(1);
                }
                ListarProductosAgregados();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void Obtener_datos_del_detalle_venta()
        {
            try
            {
                iddetalleventa = Convert.ToInt32(dgDetalleVenta.SelectedCells[9].Value.ToString());
                Id_producto = Convert.ToInt32(dgDetalleVenta.SelectedCells[8].Value.ToString());
                sevendePor = dgDetalleVenta.SelectedCells[17].Value.ToString();
                Usa_Inventarios = dgDetalleVenta.SelectedCells[16].Value.ToString();
                cantidad = Convert.ToDouble(dgDetalleVenta.SelectedCells[5].Value);
            }
            catch (Exception ex)
            {

            }
        }
        private void Eliminar_venta_al_agregar_producto()
        {
            try
            {
                SqlCommand cmd;
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();
                cmd = new SqlCommand("eliminar_venta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idventa", Idventa);
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void datalistadoDetalleVenta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Obtener_datos_del_detalle_venta();

            if (e.ColumnIndex == this.dgDetalleVenta.Columns["R"].Index)
            {
                txtpantalla = 1;
                editar_detalle_de_venta_restar();
                Contar_tablas_ventas();
                if (contador == 0)
                {
                    Eliminar_venta_al_agregar_producto();
                    txtventagenerada = "VENTA NUEVA";
                }
            }
            if (e.ColumnIndex == this.dgDetalleVenta.Columns["S"].Index)
            {
                txtpantalla = 1;
                editar_detalle_de_venta_sumar(1);
            }
            if (e.ColumnIndex == this.dgDetalleVenta.Columns["EL"].Index)
            {
                foreach (DataGridViewRow row in dgDetalleVenta.SelectedRows)
                {
                    int iddetalle_venta = Convert.ToInt32(row.Cells["iddetalle_venta"].Value);
                    try
                    {
                        SqlCommand cmd;
                        SqlConnection con = new SqlConnection();
                        con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                        con.Open();
                        cmd = new SqlCommand("eliminar_detalle_venta", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iddetalleventa", iddetalle_venta);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                ListarProductosAgregados();
                VerificarVentaVacia_para_eliminar();
            }

        }
        private void AplicarDescuento()
        {
            if (dgDetalleVenta.RowCount > 0)
            {
                var dialog = MessageBox.Show("Se le aplicara un descuento al producto seleccionado, desea continuar?", "Descuento:",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialog == DialogResult.Yes)
                {
                    Obtener_datos_del_detalle_venta();
                    Obtener_datos.AplicarDescuento(Id_producto, iddetalleventa);
                    ListarProductosAgregados();
                }

            }

        }
        private void AplicarDescuentoTodo()
        {
            if (dgDetalleVenta.RowCount > 0)
            {
                var dialog = MessageBox.Show("Se le aplicara un descuento a todos los productos, desea continuar?", "Descuento:",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialog == DialogResult.Yes)
                {
                    foreach (DataGridViewRow item in dgDetalleVenta.Rows)
                    {
                        iddetalleventa = Convert.ToInt32(item.Cells[9].Value.ToString());
                        Id_producto = Convert.ToInt32(item.Cells[8].Value.ToString());
                        Obtener_datos.AplicarDescuento(Id_producto, iddetalleventa);
                    }
                    ListarProductosAgregados();
                }

            }

        }
        private void VerificarVentaVacia_para_eliminar()
        {
            if (dgDetalleVenta.Rows.Count == 0)
            {
                ELIMINAR_VENTA();
            }
        }
        private void Contar_tablas_ventas()
        {
            int x;
            x = dgDetalleVenta.Rows.Count;
            contador = x;
        }

        private void datalistadoDetalleVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dgDetalleVenta.RowCount > 0)
            {
                Obtener_datos_del_detalle_venta();
                if (e.KeyChar == Convert.ToChar("+"))
                {

                    editar_detalle_de_venta_sumar(1);
                }
                if (e.KeyChar == Convert.ToChar("-"))
                {
                    editar_detalle_de_venta_restar();
                    Contar_tablas_ventas();
                    if (contador == 0)
                    {
                        Eliminar_venta_al_agregar_producto();
                        txtventagenerada = "VENTA NUEVA";
                    }
                }
            }

        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtCantidad.Text = txtCantidad.Text + "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtCantidad.Text = txtCantidad.Text + "2";

        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtCantidad.Text = txtCantidad.Text + "3";

        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtCantidad.Text = txtCantidad.Text + "4";

        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtCantidad.Text = txtCantidad.Text + "5";

        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtCantidad.Text = txtCantidad.Text + "6";

        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtCantidad.Text = txtCantidad.Text + "7";

        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtCantidad.Text = txtCantidad.Text + "8";

        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtCantidad.Text = txtCantidad.Text + "9";

        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtCantidad.Text = txtCantidad.Text + "0";

        }

        private void btnborrartodo_Click(object sender, EventArgs e)
        {
            txtCantidad.Clear();
            SECUENCIA = true;
        }
        bool SECUENCIA = true;
        private void btnSeparador_Click(object sender, EventArgs e)
        {
            if (SECUENCIA == true)
            {
                txtCantidad.Text = txtCantidad.Text + ".";
                SECUENCIA = false;
            }
            else
            {
                return;
            }
        }

        private void TimerBUSCADORcodigodebarras_Tick(object sender, EventArgs e)
        {
            TimerBUSCADORcodigodebarras.Stop();
            vender_por_lectora_de_barras();
        }

        private void vender_por_lectora_de_barras()
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
                        Id_producto = Convert.ToInt32(dgProductos.SelectedCells[1].Value.ToString());
                        Mostrar_stock_detalle_de_venta();
                        Contar_Stock_detalle_venta();

                        if (contador_stock_detalle_venta == 0)
                        {
                            lblStock_de_Productos = Convert.ToDouble(dgProductos.SelectedCells[4].Value.ToString());
                        }
                        else
                        {
                            lblStock_de_Productos = Convert.ToDouble(datalistado_stock_detalle_venta.SelectedCells[1].Value.ToString());
                        }

                        lblCosto.Text = dgProductos.SelectedCells[5].Value.ToString();
                        Usa_Inventarios = dgProductos.SelectedCells[3].Value.ToString();
                        lblDescripcion.Text = dgProductos.SelectedCells[9].Value.ToString();
                        lblcodigo.Text = dgProductos.SelectedCells[10].Value.ToString();
                        lblStock_de_Productos = Convert.ToDouble(dgProductos.SelectedCells[5].Value.ToString());
                        txtprecio_unitario = Convert.ToDouble(dgProductos.SelectedCells[6].Value.ToString());
                        sevendePor = dgProductos.SelectedCells[8].Value.ToString();
                        if (sevendePor == "Unidad")
                        {
                            txtEnBusqueda.Text = txtBuscar.Text;
                            txtpantalla = 1;
                            vender_por_unidad();
                        }
                        else
                        {
                            vender_a_granel();
                        }
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

        private void txtmonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtCantidad, e);
        }

        private void editar_detalle_venta_cantidad()
        {
            try
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();
                SqlCommand cmd = new SqlCommand("editar_detalle_venta_CANTIDAD", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_producto", Id_producto);
                cmd.Parameters.AddWithValue("@cantidad", txtCantidad.Text);
                cmd.Parameters.AddWithValue("@cantidad_mostrada", txtCantidad.Text);
                cmd.Parameters.AddWithValue("@Id_venta", Idventa);
                cmd.ExecuteNonQuery();
                cn.Close();
                ListarProductosAgregados();
                txtCantidad.Clear();
                txtCantidad.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " ----" + ex.StackTrace);

            }
        }
        private void btncantidad_Click(object sender, EventArgs e)
        {
            string cantidad = txtCantidad.Text;
            if (!string.IsNullOrEmpty(txtCantidad.Text))
            {
                if (dgDetalleVenta.RowCount > 0)
                {
                    var result = MessageBox.Show($"Se aplicara la cantidad de: {cantidad}," +
                        $" Esta cantidad se agregara al producto que tienes seleccionado. ¿Deseas continuar?",
                        "Confirma", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                    if (result == DialogResult.Yes)
                    {
                        if (sevendePor == "Unidad")
                        {
                            string amountString = txtCantidad.Text;
                            if (amountString.Contains("."))
                            {
                                MessageBox.Show("Este Producto no acepta decimales ya que esta configurado para ser vendido por UNIDAD",
                                    "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                ValidationBotonCantidad();
                            }
                        }
                        else if (sevendePor == "Granel")
                        {
                            ValidationBotonCantidad();
                        }
                        txtCantidad.Clear();
                    }
                    else
                        txtCantidad.Clear();
                }
                else
                {
                    txtCantidad.Clear();
                    txtCantidad.Focus();
                }
            }
        }
        private void ValidationBotonCantidad()
        {
            double cantidadAplicar = Convert.ToDouble(txtCantidad.Text);
            double stockAplicado = Convert.ToDouble(dgDetalleVenta.SelectedCells[5].Value);

            double stockExistenteDetalle;
            double totalStockExistencia;

            string ControlStock = dgDetalleVenta.SelectedCells[16].Value.ToString();
            if (ControlStock == "SI")
            {
                stockExistenteDetalle = Convert.ToDouble(dgDetalleVenta.SelectedCells[11].Value);
                totalStockExistencia = stockAplicado + stockExistenteDetalle;
                if (totalStockExistencia >= cantidadAplicar)
                {
                    AplicarCantidadPorBoton();
                }
                else
                {
                    TimerLABEL_STOCK.Start();
                }
            }
            else
            {
                AplicarCantidadPorBoton();
            }


        }
        private void AplicarCantidadPorBoton()
        {
            double CantidadAplicar = Convert.ToDouble(txtCantidad.Text);
            double CantidadYaAplicada = Convert.ToDouble(dgDetalleVenta.SelectedCells[5].Value);

            if (CantidadAplicar > CantidadYaAplicada)
            {
                txtpantalla = CantidadAplicar - CantidadYaAplicada;
                editar_detalle_de_venta_sumar(txtpantalla);
            }
            else if (CantidadAplicar < CantidadYaAplicada)
            {
                txtpantalla = CantidadYaAplicada - CantidadAplicar;
                editar_detalle_de_venta_restar();
            }
        }
        private void buttonNotificacion_Click(object sender, EventArgs e)
        {

        }

        private void befectivo_Click(object sender, EventArgs e)
        {
            Cobrar();
        }
        private void Cobrar()
        {
            if (dgDetalleVenta.RowCount > 0)
            {
                Obtener_datos.ObtenerEstadoImpuestos(ref EstadoImpuestos);
                total = totalpagar;

                Medios_de_pagos frm = new Medios_de_pagos();
                frm.FormClosed += new FormClosedEventHandler(frm_FormClosing);
                frm.estadoImpuesto = EstadoImpuestos;
                frm.porcentaje = PorcentajeImp;
                frm.totalImpuesto = ImpuestoCalculado;
                frm.subtotal = subtotal;
                frm.ShowDialog();
            }
        }
        private void frm_FormClosing(object sender, FormClosedEventArgs e)
        {
            //if (Medios_de_pagos.Estado_Venta_Si_Es_Confirmada == "NO CONFIRMADA")
            //{

            //}
            //else if (Medios_de_pagos.Estado_Venta_Si_Es_Confirmada == "CONFIRMADA")
            //{
            //    Limpiar_para_venta_nueva();
            //}

            if (EstadoMediosPago == true)
            {
                Limpiar_para_venta_nueva();
            }

        }
        private void datalistadoDetalleVenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnprecio_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCantidad.Text))
            {
                LdetalleVenta parametros = new LdetalleVenta();
                Editar_datos funcion = new Editar_datos();
                parametros.iddetalle_venta = iddetalleventa;
                parametros.preciounitario = Convert.ToDouble(txtCantidad.Text);
                if (funcion.editarPrecioVenta(parametros) == true)
                {
                    ListarProductosAgregados();
                }
            }
        }

        private void TimerLABEL_STOCK_Tick(object sender, EventArgs e)
        {
            if (ProgressBarETIQUETA_STOCK.Value < 100)
            {
                ProgressBarETIQUETA_STOCK.Value = ProgressBarETIQUETA_STOCK.Value + 10;
                LABEL_STOCK.Visible = true;
                LABEL_STOCK.Dock = DockStyle.Fill;
            }
            else
            {
                LABEL_STOCK.Visible = false;
                LABEL_STOCK.Dock = DockStyle.None;
                ProgressBarETIQUETA_STOCK.Value = 0;
                TimerLABEL_STOCK.Stop();
            }
        }

        private void btnVolverAdministrador_Click(object sender, EventArgs e)
        {
            estadoCerrarVentanabtnAdmin = true;
            this.Hide();
            Admin_nivel_Dios.Dashboard_Principal frm = new Admin_nivel_Dios.Dashboard_Principal();
            
            frm.ShowDialog();
            
        }

        private void btnRestaurarVenta_Click(object sender, EventArgs e)
        {
            Ventas_en_espera frm = new Ventas_en_espera();
            frm.FormClosing += FrmENESPERA;
            frm.ShowDialog();
        }

        private void FrmENESPERA(object sender, FormClosingEventArgs e)
        {
            ListarProductosAgregados();
            mostrar_panel_de_cobro();
            ContarVentasEspera();
        }
        private void ELIMINAR_VENTA()
        {
            Lventas obj = new Lventas();
            obj.idventa = Idventa;
            Eliminar_datos obj2 = new Eliminar_datos();
            obj2.eliminar_venta(obj);
            Limpiar_para_venta_nueva();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgDetalleVenta.RowCount > 0)
            {
                DialogResult r = MessageBox.Show("¿Realmente deseas eliminar esta venta?", "Confirma:", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r == DialogResult.Yes)
                {
                    ELIMINAR_VENTA();
                }
            }


        }

        private void btnPonerEn_espera_Click(object sender, EventArgs e)
        {
            PonerenEspera();
        }
        private void PonerenEspera()
        {
            if (dgDetalleVenta.RowCount > 0)
            {
                PanelEnEspera.Visible = true;
                PanelEnEspera.BringToFront();
                PanelEnEspera.Dock = DockStyle.Fill;
                txtnombre.Clear();
                ContarVentasEspera();
            }
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            ocultar_panel_en_espera();
        }
        private void ocultar_panel_en_espera()
        {
            PanelEnEspera.Visible = false;
            PanelEnEspera.Dock = DockStyle.None;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtnombre.Text))
            {
                editar_venta_espera();

            }
            else
            {
                MessageBox.Show("Ingrese Una Referencia");
            }

        }
        private void editar_venta_espera()
        {
            Editar_datos.ingresar_nombre_a_venta_en_espera(Idventa, txtnombre.Text);
            Limpiar_para_venta_nueva();
            ocultar_panel_en_espera();
        }
        private void btnGuardarAutomatico_Click(object sender, EventArgs e)
        {
            txtnombre.Text = "Ticket" + Idventa;
            editar_venta_espera();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lbltipodebusqueda2.Text = "Buscar con Lectora de Codigo de Barras...";
            btnLectora.BackColor = Color.LightGreen;
            btnTeclado.BackColor = Color.WhiteSmoke;
            Tipo_de_Busqueda = "LECTORA";
            txtBuscar.Clear();
            txtBuscar.Focus();
        }

        private void btnVerMovimientosCaja_Click(object sender, EventArgs e)
        {
            Caja.Listado_Gastos_ingresos frm = new Caja.Listado_Gastos_ingresos();
            frm.ShowDialog();
        }
        private void btnPagarDeudaProveedor_Click(object sender, EventArgs e)
        {
            var frm = new RealizarPagos();
            frm.ShowDialog();
        }

        private void btnGastos_Click(object sender, EventArgs e)
        {
            Gastos_Varios.Gastos frm = new Gastos_Varios.Gastos();
            frm.ShowDialog();
            DibujarMenuPrincipal();
        }

        private void btnIngresosCaja_Click(object sender, EventArgs e)
        {
            Ingresos_Varios.IngresosVarios frm = new Ingresos_Varios.IngresosVarios();
            frm.ShowDialog();
            DibujarMenuPrincipal();
        }

        private void btnAperturar_creditoPagar_Click(object sender, EventArgs e)
        {
            Apertura_de_credito.PorPagar frm = new Apertura_de_credito.PorPagar();
            frm.ShowDialog();
            DibujarMenuPrincipal();
        }

        private void btnCreditoPorCobrar_Click(object sender, EventArgs e)
        {
            Apertura_de_credito.Por_Cobrar frm = new Apertura_de_credito.Por_Cobrar();
            frm.ShowDialog();
            DibujarMenuPrincipal();
        }

        private void frm_VENTAS_MENU_PRINCIPAL_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (estadoCerrarVentanabtnAdmin == true)
            {
                e.Cancel = true;
            }
            else
            {
                if (estadoCerrarSistema == true)
                {
                    DialogResult dr = MessageBox.Show("¿Realmente deseas cerrar el sistema?", "Cerrando:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        Dispose();
                        string estado = "";
                        Obtener_datos.ObtenerEstadoDeCopias(ref estado);
                        if (estado != "--")
                        {

                            CopiasBd.GeneradorAutomatico frm = new CopiasBd.GeneradorAutomatico();
                            frm.ShowDialog();
                        }

                        Application.ExitThread();

                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    var page = new frmInicioSeccion();
                    page.Show();
                }
            }
           
        }

        private void btnCobros_Click(object sender, EventArgs e)
        {
            Cobros.CobrosForm frm = new Cobros.CobrosForm();
            frm.ShowDialog();
            DibujarMenuPrincipal();
        }

        private void btnMayoreo_Click(object sender, EventArgs e)
        {
            aplicar_precio_mayoreo();
            DibujarMenuPrincipal();
        }
        private void aplicar_precio_mayoreo()
        {
            if (dgDetalleVenta.Rows.Count > 0)
            {
                LdetalleVenta parametros = new LdetalleVenta();
                Editar_datos funcion = new Editar_datos();
                parametros.Id_producto = Id_producto;
                parametros.iddetalle_venta = iddetalleventa;
                if (funcion.aplicar_precio_mayoreo(parametros) == true)
                {
                    ListarProductosAgregados();
                }
            }

        }

        private void IndicadorTema_CheckedChanged(object sender, EventArgs e)
        {
            if (IndicadorTema.Checked == true)
            {
                Tema = "Oscuro";
                EditarTemaCaja();
                TemaOscuro();
                ListarProductosAgregados();
                ValidarTemaCaja();
                pl.Visible = false;

            }
            else
            {
                Tema = "Redentor";
                EditarTemaCaja();
                TemaClaro();
                ListarProductosAgregados();
                ValidarTemaCaja();
                pl.Visible = false;

            }
        }
        private void EditarTemaCaja()
        {
            Lcaja parametros = new Lcaja();
            Editar_datos funcion = new Editar_datos();
            parametros.Tema = Tema;
            funcion.EditarTemaCaja(parametros);

        }
        private void TemaOscuro()
        {
            //PanelC1 Encabezado
            PanelC1.BackColor = Color.FromArgb(19, 19, 19);
            lblNombreSoftware.ForeColor = Color.White;
            btnAdministrador.ForeColor = Color.White;
            btnVisorPrecios.ForeColor = Color.White;

            txtBuscar.BackColor = Color.FromArgb(20, 20, 20);
            txtBuscar.ForeColor = Color.White;
            txtBuscar.BorderStyle = BorderStyle.None;
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            lbltipodebusqueda2.BackColor = Color.FromArgb(20, 20, 20);
            ValidarTipoBusqueda();
            //PanelC2 Intermedio
            panelC2.BackColor = Color.FromArgb(35, 35, 35);
            lblBusquedapor.ForeColor = Color.White;
            pl.BackColor = Color.FromArgb(49, 49, 49);

            PanelEnEspera.BackColor = Color.FromArgb(35, 35, 35);
            btnGuardar.BackColor = Color.FromArgb(35, 35, 35);

            label4.ForeColor = Color.White;
            label5.ForeColor = Color.White;
            btnGuardar.ForeColor = Color.White;

            btnAplicarDescuento.BackColor = Color.FromArgb(45,45,45) ;
            btnAplicarDescuento.ForeColor = Color.White;

            btnAplicarDescuentoTodo.BackColor = Color.FromArgb(45, 45, 45);
            btnAplicarDescuentoTodo.ForeColor = Color.White;
            
            btncantidad.BackColor = Color.FromArgb(45, 45, 45);
            btncantidad.ForeColor = Color.White;

            LABEL_STOCK.BackColor = Color.FromArgb(45, 45, 45);

            //PanelC3


            //PanelC4 Pie de pagina
            panelC4.BackColor = Color.FromArgb(19, 19, 19);
            btnPonerEn_espera.BackColor = Color.FromArgb(19, 19, 19);
            btnPonerEn_espera.ForeColor = Color.White;
            btnRestaurarVenta.BackColor = Color.FromArgb(19, 19, 19);
            btnRestaurarVenta.ForeColor = Color.White;
            btnEliminar.BackColor = Color.FromArgb(19, 19, 19);
            btnEliminar.ForeColor = Color.White;
            btndevoluciones.BackColor = Color.FromArgb(19, 19, 19);
            btndevoluciones.ForeColor = Color.White;
            //PanelOperaciones
            PanelOperaciones.BackColor = Color.FromArgb(28, 28, 28);
            txt_total_suma.ForeColor = Color.WhiteSmoke;
            //PanelBienvenida
            panelBienvenida.BackColor = Color.FromArgb(35, 35, 35);
            label8.ForeColor = Color.WhiteSmoke;
            ListarProductosAgregados();

        }
        private void TemaClaro()
        {
            //PanelC1 encabezado
            PanelC1.BackColor = Color.Gainsboro;
            lblNombreSoftware.ForeColor = Color.Black;
            btnAdministrador.ForeColor = Color.Black;
            btnVisorPrecios.ForeColor = Color.Black;
            txtBuscar.BackColor = Color.White;
            txtBuscar.ForeColor = Color.Black;
            lbltipodebusqueda2.BackColor = Color.White;
            ValidarTipoBusqueda();
            //PanelC2 intermedio
            panelC2.BackColor = Color.White;
            lblBusquedapor.ForeColor = Color.Black;

            PanelEnEspera.BackColor = Color.White;
            label4.ForeColor = Color.Black;
            label5.ForeColor = Color.Black;
            btnGuardar.ForeColor = Color.Black;
            btnGuardar.BackColor = Color.White;

            btnAplicarDescuento.BackColor = Color.LightGray;
            btnAplicarDescuento.ForeColor = Color.Black;

            btnAplicarDescuentoTodo.BackColor = Color.LightGray;
            btnAplicarDescuentoTodo.ForeColor = Color.Black;

            btncantidad.BackColor = Color.LightGray;
            btncantidad.ForeColor = Color.Black;
            LABEL_STOCK.BackColor = Color.White;
            //PanelC4 pie de pagina
            panelC4.BackColor = Color.Gainsboro;
            btnPonerEn_espera.BackColor = Color.Gainsboro;
            btnPonerEn_espera.ForeColor = Color.Black;
            btnRestaurarVenta.BackColor = Color.Gainsboro;
            btnRestaurarVenta.ForeColor = Color.Black;
            btnEliminar.BackColor = Color.Gainsboro;
            btnEliminar.ForeColor = Color.Black;
            btndevoluciones.BackColor = Color.Gainsboro;
            btndevoluciones.ForeColor = Color.Black;
            //PanelOperaciones
            PanelOperaciones.BackColor = Color.FromArgb(242, 243, 245);
            txt_total_suma.ForeColor = Color.Black;
            //PanelBienvenida
            panelBienvenida.BackColor = Color.White;
            label8.ForeColor = Color.FromArgb(64, 64, 64);
            ListarProductosAgregados();


        }

        private void btndevoluciones_Click(object sender, EventArgs e)
        {
            HistorialVentas.HistorialVentasForm frm = new HistorialVentas.HistorialVentasForm();
            frm.ShowDialog();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (Tipo_de_Busqueda != "LECTORA")
            {
                EventosTipoBusqueda(e);
                EventoNavegarDgProductos(e);
                EventoNavegarDgDetalleventa(e);
            }

        }

        private void EventoNavegarDgProductos(KeyEventArgs e)
        {
            EstadoCobrar = true;
            if (dgProductos.Visible == true)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    EstadoCobrar = false;
                    Vender_por_Teclado();
                }
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                {
                    dgProductos.Focus();
                }
            }
            else
            {
                if (e.KeyCode == Keys.Enter && EstadoCobrar == true)
                {
                    Cobrar();
                }
            }
        }
        private void EventoNavegarDgDetalleventa(KeyEventArgs e)
        {
            if (dgProductos.Visible == false)
            {

                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                {
                    dgDetalleVenta.Focus();
                }

            }
        }
        private void EventoCobros(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgProductos.Visible == false)
            {
                Cobrar();
            }
        }
        private void EventosTipoBusqueda(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Modo_Teclado();
            }
            if (e.KeyCode == Keys.F2)
            {
                Modo_Lectora();
            }
            if (e.KeyCode == Keys.F3)
            {
                PonerenEspera();
            }
            if (e.KeyCode == Keys.Escape)
            {
                Obtener_datos.Mostrar_TipoBusqueda(ref Tipo_de_Busqueda);
                ValidarTipoBusqueda();
            }
            if (e.KeyCode == Keys.F3)
            {
                PonerenEspera();
            }
        }

        private void datalistadoDetalleVenta_KeyDown(object sender, KeyEventArgs e)
        {
            EventosTipoBusqueda(e);
            EventoCobros(e);
        }

        private void dgProductos_KeyDown(object sender, KeyEventArgs e)
        {
            EventosTipoBusqueda(e);
            EventoNavegarDgProductos(e);
        }

        private void btndevoluciones_Click_1(object sender, EventArgs e)
        {
            HistorialVentas.HistorialVentasForm frm = new HistorialVentas.HistorialVentasForm();
            frm.ShowDialog();
        }

        private void btnDibujarPanel_Click(object sender, EventArgs e)
        {

        }

        private void DibujarMenuPrincipal()
        {

            if (pl.Visible == false)
            {
                pl = new Panel();
                pl.Visible = true;

                pl.Size = new Size(468, 500);
                pl.Location = new Point((this.Width - pl.Width) / 2, (this.Height - pl.Height) / 2);
                pl.BorderStyle = BorderStyle.FixedSingle;

                this.Controls.Add(pl);
                pl.BringToFront();

            }
            else
            {
                this.Controls.Remove(pl);
                pl.Visible = false;
            }
            ValidarTemaCaja();
        }

        private void DisenarPanelHerramientas()
        {
            pl.Controls.Clear();
            //Panel Arriba para cerrar
            var p2 = new Panel();
            var btnCerrar = new Button();
            var lbl = new Label();

            btnCerrar.Font = new Font("Microsoft Sans Serif", 17);
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.Text = "X";
            btnCerrar.Size = new Size(25, 1);
            btnCerrar.Dock = DockStyle.Right;
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Click += BtnCerrar_Click;

            lbl.Font = new Font("Microsoft Sans Serif", 17);
            lbl.Text = "Acciones Rápidas";
            lbl.Size = new Size(25, 1);
            lbl.Dock = DockStyle.Fill;
            lbl.Name = "lblHerramientas";
            lbl.AutoSize = false;
            lbl.TextAlign = ContentAlignment.MiddleLeft;

            p2.Size = new Size(1, 35);
            p2.Dock = DockStyle.Top;
            p2.BorderStyle = BorderStyle.None;

            p2.Controls.Add(btnCerrar);
            p2.Controls.Add(lbl);
            pl.Controls.Add(p2);
            if (Tema == "Redentor")
            {
                btnCerrar.ForeColor = Color.Black;
                lbl.ForeColor = Color.Black;
            }
            else
            {
                btnCerrar.ForeColor = Color.White;
                lbl.ForeColor = Color.White;
            }
            flow = new FlowLayoutPanel();
            flow.Dock = DockStyle.Fill;
            flow.AutoScroll = true;
            pl.Controls.Add(flow);
            flow.BringToFront();
            DibujarBotonesPanel(ref flow);
            CambiarColorPanelHerramientas();

        }
        private void CambiarColorPanelHerramientas()
        {
            if (Tema == "Redentor")
            {
                foreach (var ControlP in flow.Controls)
                {
                    if (ControlP is Panel)
                    {
                        Panel p = (Panel)ControlP;
                        foreach (var itemPanel in p.Controls)
                        {
                            if (itemPanel is Button)
                            {
                                Button b = (Button)itemPanel;
                                b.ForeColor = Color.Black;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var ControlP in flow.Controls)
                {
                    if (ControlP is Panel)
                    {
                        Panel p = (Panel)ControlP;
                        foreach (var itemPanel in p.Controls)
                        {
                            if (itemPanel is Button)
                            {
                                Button b = (Button)itemPanel;
                                b.ForeColor = Color.White;
                            }
                        }
                    }
                }
            }

        }
        private void AjustarMedidasPanelesBotones(ref Panel p)
        {
            p.Size = new Size(225, 70);
            p.BorderStyle = BorderStyle.None;
            p.BackColor = Color.Transparent;
        }
        private void DibujarBotonesPanel(ref FlowLayoutPanel flow)
        {
            //Paneles 
            Panel p1 = new Panel();
            Panel p2 = new Panel();
            Panel p3 = new Panel();
            Panel p4 = new Panel();
            Panel p5 = new Panel();
            Panel p6 = new Panel();
            Panel p7 = new Panel();
            Panel p8 = new Panel();
            //Botones
            Button btn1 = new Button();
            Button btn2 = new Button();
            Button btn3 = new Button();
            Button btn4 = new Button();
            Button btn5 = new Button();
            Button btn6 = new Button();
            Button btn7 = new Button();
            Button btn8 = new Button();
            //Diseno de paneles 
            AjustarMedidasPanelesBotones(ref p1);
            AjustarMedidasPanelesBotones(ref p2);
            AjustarMedidasPanelesBotones(ref p3);
            AjustarMedidasPanelesBotones(ref p4);
            AjustarMedidasPanelesBotones(ref p5);
            AjustarMedidasPanelesBotones(ref p6);
            AjustarMedidasPanelesBotones(ref p7);
            AjustarMedidasPanelesBotones(ref p8);


            List<Button> Properties_btn = new List<Button>();
            Properties_btn.Add(btn1);
            Properties_btn.Add(btn2);
            Properties_btn.Add(btn3);
            Properties_btn.Add(btn4);
            Properties_btn.Add(btn5);
            Properties_btn.Add(btn6);
            Properties_btn.Add(btn7);
            Properties_btn.Add(btn8);
            foreach (var btn in Properties_btn)
            {
                btn.Dock = DockStyle.Fill;
                btn.Name = "btn";
                btn.Cursor = Cursors.Hand;
                btn.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                btn.FlatAppearance.MouseDownBackColor = Color.DimGray;
                btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
                btn.ImageAlign = ContentAlignment.MiddleCenter;
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.TextImageRelation = TextImageRelation.ImageBeforeText;
                p1.Controls.Add(btn);
            }

            //Properties Secondary
            btn1.Image = Properties.Resources.mano_sosteniendo_la_bolsa_de_dinero_de_dolares__2_;
            btn2.Image = Properties.Resources.dinero__1_;
            btn3.Image = Properties.Resources.iconos_de_dinero;
            btn4.Image = Properties.Resources.mejor_precio;
            btn5.Image = Properties.Resources.hacia_arriba;
            btn6.Image = Properties.Resources.hacia_arriba__1_;
            btn7.Image = Properties.Resources.salida;
            btn8.Image = Properties.Resources.IconoPagar;


            btn1.Text = "Cobros                    ";
            btn2.Text = "Aperturar Credito X Cobrar";
            btn3.Text = "Aperturar Credito X Pagar ";
            btn4.Text = "Mayoreo                   ";
            btn5.Text = "Ingresar Dinero           ";
            btn6.Text = "Registrar Salida Dinero   ";
            btn7.Text = "Ver Ingresos y Salidas";
            btn8.Text = "Pagar";


            btn1.Click += btnCobros_Click;
            btn2.Click += btnCreditoPorCobrar_Click;
            btn3.Click += btnAperturar_creditoPagar_Click;
            btn4.Click += btnMayoreo_Click;
            btn5.Click += btnIngresosCaja_Click;
            btn6.Click += btnGastos_Click;
            btn7.Click += btnVerMovimientosCaja_Click;
            btn8.Click += btnPagarDeudaProveedor_Click;
            //Asignacion a botones 
            p1.Controls.Add(btn1);
            p2.Controls.Add(btn2);
            p3.Controls.Add(btn3);
            p4.Controls.Add(btn4);
            p5.Controls.Add(btn5);
            p6.Controls.Add(btn6);
            p7.Controls.Add(btn7);
            p8.Controls.Add(btn8);


            flow.Controls.Add(p1);
            flow.Controls.Add(p8);
            flow.Controls.Add(p4);
            flow.Controls.Add(p5);
            flow.Controls.Add(p6);
            flow.Controls.Add(p3);
            flow.Controls.Add(p2);
            flow.Controls.Add(p7);
        }
        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            DibujarMenuPrincipal();
        }
        private void btnMenu_Click(object sender, EventArgs e)
        {
            DibujarMenuPrincipal();
        }

        private void BtnTecladoV_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void txtmonto_TextChanged(object sender, EventArgs e)
        {
            if (txtCantidad.Text == "")
            {
                lblCant.Visible = true;
            }
            else
            {
                lblCant.Visible = false;
            }
        }

        private void lblCant_Click(object sender, EventArgs e)
        {
            txtCantidad.Focus();
        }

        private void dgProductos_Leave(object sender, EventArgs e)
        {

        }

        private void btnAplicarDescuento_Click(object sender, EventArgs e)
        {
            AplicarDescuento();

        }

        private void btnAplicarDescuentoTodo_Click(object sender, EventArgs e)
        {
            AplicarDescuentoTodo();
        }

        private void btnCotizar_Click(object sender, EventArgs e)
        {
            var page = new Cotizaciones.Vista.CotizacionesMenu();
            this.Hide();
            page.FormClosed += Page_FormClosed;
            page.ShowDialog();
        }

        private void Page_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void panelReferenciaProductos_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnVisorPrecios_Click(object sender, EventArgs e)
        {
            var page = new Visor_Precios.BuscadorPrecios();
            page.ShowDialog();
        }

        private void lblCerrarSesion_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show("Estas seguro de querer cerrar la sesion?", "Confirma:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                estadoCerrarSistema = false;
                this.Hide();
                var frm = new frmInicioSeccion();

                
                frm.ShowDialog();
                Application.Run();
                
            }
        }

        private void btnIngresoProd_Click(object sender, EventArgs e)
        {
            var page = new KardexEntrada();
            page.ShowDialog();

        }

        private void btnSalidaProd_Click(object sender, EventArgs e)
        {
            var page = new Kardex_Salidas();
            page.ShowDialog();
        }
    }
}
