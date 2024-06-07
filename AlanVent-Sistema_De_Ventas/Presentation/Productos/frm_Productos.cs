using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.Productos
{
    public partial class frm_Productos : Form
    {
        public frm_Productos()
        {
            InitializeComponent();
        }
        public static int idusuario;
        public static int idcaja;
        int txtcontador;
        string lblidserial;
        private string NombreImpuesto;
        private double PorcentajeImp;
        private string Moneda;
        private double Cantidad_Decimal_Impuesto;
        double TotalPrecioDeVenta;
        double ImpuestoDeVenta;
        double TotalPrecioDeMayoreo;
        double ImpuestoMayoreo;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtbusca.Text == "")
            {
                lblBuscarEtiqueta.Visible = true;
            }
            else
            {
                lblBuscarEtiqueta.Visible = false;
            }
            buscar();
        }
        private void mostrar_grupos()
        {
            PanelGRUPOSSELECT.Visible = true;
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("mostrar_grupos", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@buscar", txtGrupo.Text);
                da.Fill(dt);
                datalistadoGrupos.DataSource = dt;
                con.Close();

                datalistadoGrupos.DataSource = dt;
                datalistadoGrupos.Columns[2].Visible = false;
                datalistadoGrupos.Columns[3].Width = 500;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Bases.MultilineaTemaOscuro(ref datalistado);
        }
        private void guardarGrupoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("insertar_Grupo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Grupo", txtGrupo.Text);
                cmd.Parameters.AddWithValue("@Por_defecto", "NO");
                cmd.ExecuteNonQuery();
                con.Close();
                mostrar_grupos();

                lblIdGrupo.Text = datalistadoGrupos.SelectedCells[2].Value.ToString();
                txtGrupo.Text = datalistadoGrupos.SelectedCells[3].Value.ToString();

                PanelGRUPOSSELECT.Visible = false;
                btnguardarGrupo.Visible = false;
                BtnGuardarCambios.Visible = false;
                BtnCancelar.Visible = false;
                btnNuevoGrupo.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNuevoGrupo_Click(object sender, EventArgs e)
        {
            txtGrupo.Text = "*Escribe el Nuevo GRUPO*";
            txtGrupo.SelectAll();
            txtGrupo.Focus();

            PanelGRUPOSSELECT.Visible = false;
            btnguardarGrupo.Visible = true;
            BtnGuardarCambios.Visible = false;
            BtnCancelar.Visible = true;
            btnNuevoGrupo.Visible = false;
        }

        public void buscar()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("buscar_producto_por_descripcion", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", txtbusca.Text);
                da.Fill(dt);
                datalistado.DataSource = dt;
                con.Close();

                datalistado.Columns[2].Visible = false;
                datalistado.Columns[7].Visible = false;
                datalistado.Columns[10].Visible = false;
                datalistado.Columns[15].Visible = false;
                datalistado.Columns[16].Visible = false;
                datalistado.Columns[17].Visible = false;
                datalistado.Columns[18].Visible = false;
                Bases.MultilineaTemaOscuro(ref datalistado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            //Bases.Multilinea(ref datalistado);
            sumar_costo_de_inventario_CONTAR_PRODUCTOS();
        }
        internal void sumar_costo_de_inventario_CONTAR_PRODUCTOS()
        {


            string resultado;
            string queryMoneda;
            queryMoneda = "SELECT Moneda  FROM EMPRESA";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = DataAccess.ConexionMaestra.conexion;
            SqlCommand comMoneda = new SqlCommand(queryMoneda, con);
            try
            {
                con.Open();
                resultado = Convert.ToString(comMoneda.ExecuteScalar()); //asignamos el valor del importe
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                resultado = "";
            }

            string importe;
            string query;
            query = "SELECT      CONVERT(NUMERIC(18,2),sum(Productos1.Precio_de_compra * Stock )) as suma FROM  Productos1 where  Usa_inventarios ='SI'";

            SqlCommand com = new SqlCommand(query, con);
            try
            {
                con.Open();
                importe = Convert.ToString(com.ExecuteScalar()); //asignamos el valor del importe
                con.Close();
                // lblCostoInventarios.Text = resultadoDiferencia + " " + importe;
                lblCostoInventarios.Text = "$" + " " + Bases.AsignarComa(Convert.ToDouble(importe));
            }
            catch (Exception ex)
            {
                con.Close();

                lblCostoInventarios.Text = resultado + " " + 0;
            }

            string conteoresultado;
            string querycontar;
            querycontar = "select Sum(CONVERT(NUMERIC(18,2),Stock)) from Productos1 where Usa_Inventarios = 'SI'";
            SqlCommand comcontar = new SqlCommand(querycontar, con);
            try
            {
                con.Open();
                conteoresultado = Convert.ToString(comcontar.ExecuteScalar()); //asignamos el valor del importe
                con.Close();
                lblCantidadProductos.Text = Bases.AsignarComa(Convert.ToDouble(conteoresultado));
            }
            catch (Exception ex)
            {
                con.Close();

                conteoresultado = "";
                lblCantidadProductos.Text = "0";
            }

        }
        private void frm_Productos_Load(object sender, EventArgs e)
        {
            PanelGRUPOSSELECT.Visible = false;
            Logic.Bases.Cambiar_idioma_regional();
            Panel_RegistrarProductos.Visible = false;
            buscar();
            mostrar_grupos();
            sumar_costo_de_inventario_CONTAR_PRODUCTOS();
            //Obtener_datos.mostrar_inicio_De_sesion(ref idusuario);
            Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
            Logic.Bases.Obtener_serialPc(ref lblidserial);
            Obtener_datos.mostrar_inicio_De_sesion(ref idusuario);

        }



        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            PanelGRUPOSSELECT.Visible = false;
            btnguardarGrupo.Visible = false;
            BtnGuardarCambios.Visible = false;
            BtnCancelar.Visible = false;
            btnNuevoGrupo.Visible = true;
            txtGrupo.Clear();
            mostrar_grupos();
        }

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            panelBackBuscador.Visible = false;
            Panel_RegistrarProductos.Dock = DockStyle.Fill;
            Panel_RegistrarProductos.Visible = true;
            Panel_RegistrarProductos.BringToFront();
            TGUARDARCAMBIOS.Visible = false;
            PanelGRUPOSSELECT.Visible = false;
            btnguardarGrupo.Visible = false;
            BtnCancelar.Visible = false;
            BtnGuardarCambios.Visible = false;
            txtDescripcion.Clear();
            txtPrecioCosto.Text = "0";
            txtganancia.Clear();
            txtPrecioVenta.Text = "0";
            txtMayoreo.Text = "0";
            txtGrupo.Clear();
            txtaPartirDe.Text = "0";
            txtcodigodebarras.Clear();
            txtstock2.Text = "0";
            txtstockminimo.Text = "0";
            txtbusca.Clear();
            CheckInventarios.Checked = true;
            rb_porUnidad.Checked = true;
            PANELINVENTARIO.Visible = true;
            TXTIDPRODUCTOOk.Text = "0";
            lblEstadoDeCodigo.Text = "NUEVO";
            txtDescripcion.Focus();
            TGUARDAR.Visible = true;
            txtDescripcion.AutoCompleteCustomSource = DataAccess.WinAutoComplete.LoadAutoComplete();
            txtDescripcion.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtDescripcion.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void txtGrupo_TextChanged(object sender, EventArgs e)
        {
            if (BtnGuardarCambios.Visible == true & BtnCancelar.Visible == true)
            {
                PanelGRUPOSSELECT.Visible = false;
                return;
            }
            if (btnguardarGrupo.Visible == true & BtnCancelar.Visible == true)
            {
                return;
            }
            if (txtGrupo.Text == "")
            {
                PanelGRUPOSSELECT.Visible = false;
            }
            else
            {
                PanelGRUPOSSELECT.Visible = true;
                mostrar_grupos();
            }

        }
        private void Insertar_Producto()
        {
            if (txtMayoreo.Text == "0" | txtMayoreo.Text == "")
                txtaPartirDe.Text = "0";

            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                SqlCommand cmd = new SqlCommand("insertar_Producto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text);
                cmd.Parameters.AddWithValue("@Precio_de_compra", Convert.ToDouble(txtPrecioCosto.Text));
                cmd.Parameters.AddWithValue("@Precio_de_venta", TotalPrecioDeVenta);
                cmd.Parameters.AddWithValue("@Codigo", txtcodigodebarras.Text);
                cmd.Parameters.AddWithValue("@Impuesto", ImpuestoDeVenta);
                cmd.Parameters.AddWithValue("@A_partir_de", txtaPartirDe.Text);
                cmd.Parameters.AddWithValue("@Precio_mayoreo", TotalPrecioDeMayoreo);

                if (rb_porUnidad.Checked == true)
                    txtse_vende_a.Text = "Unidad";
                if (rb_aGranel.Checked == true)
                    txtse_vende_a.Text = "Granel";

                cmd.Parameters.AddWithValue("@Se_vende_a", txtse_vende_a.Text);
                cmd.Parameters.AddWithValue("@Id_grupo", lblIdGrupo.Text);

                if (PANELINVENTARIO.Visible == true)
                {
                    cmd.Parameters.AddWithValue("@Usa_inventarios", "SI");
                    cmd.Parameters.AddWithValue("@Stock_minimo", txtstockminimo.Text);
                    cmd.Parameters.AddWithValue("@Stock", txtstock2.Text);

                    if (No_aplica_fecha.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Fecha_de_vencimiento", "NO APLICA");
                    }
                    if (No_aplica_fecha.Checked == false)
                    {
                        cmd.Parameters.AddWithValue("@Fecha_de_vencimiento", txtfechaoka.Text);
                    }

                }
                if (PANELINVENTARIO.Visible == false)
                {
                    cmd.Parameters.AddWithValue("@Usa_inventarios", "NO");
                    cmd.Parameters.AddWithValue("@Stock_minimo", 0);
                    cmd.Parameters.AddWithValue("@Fecha_de_vencimiento", "NO APLICA");
                    cmd.Parameters.AddWithValue("@Stock", "99999");
                }
                cmd.Parameters.AddWithValue("@Fecha", DateTime.Today);
                cmd.Parameters.AddWithValue("@Motivo", "Registro inicial de Producto");
                cmd.Parameters.AddWithValue("@Cantidad ", txtstock2.Text);
                cmd.Parameters.AddWithValue("@Id_usuario", idusuario);
                cmd.Parameters.AddWithValue("@Tipo", "ENTRADA");
                cmd.Parameters.AddWithValue("@Estado", "CONFIRMADO");
                cmd.Parameters.AddWithValue("@Id_caja", idcaja);
                cmd.Parameters.AddWithValue("@SubtotalPrecioVenta", Convert.ToDouble(txtPrecioVenta.Text));
                cmd.Parameters.AddWithValue("@subtotalMayoreo", Convert.ToDouble(txtMayoreo.Text));
                cmd.Parameters.AddWithValue("@Impuestomayoreo", ImpuestoMayoreo);
                int r = cmd.ExecuteNonQuery();
                con.Close();

                Panel_RegistrarProductos.Visible = false;
                panelBackBuscador.Visible = true;
                buscar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        private void TGUARDAR_Click(object sender, EventArgs e)
        {
            try
            {
                //Declaration and validations
                double rtxtpreciomayoreo = Convert.ToDouble(txtMayoreo.Text);
                double rtxtapartirDe = Convert.ToDouble(txtaPartirDe.Text);
                double rtxtcosto1 = Convert.ToDouble(txtPrecioCosto.Text);
                double rtxtprecioVenta = Convert.ToDouble(txtPrecioVenta.Text);
                if (txtMayoreo.Text == "")
                    txtMayoreo.Text = "0";

                if (txtaPartirDe.Text == "")
                    txtaPartirDe.Text = "0";

                if (txtDescripcion.Text != "")
                {
                    if ((rtxtpreciomayoreo > 0 & Convert.ToDouble(txtaPartirDe.Text) > 0) | (rtxtpreciomayoreo == 0 & rtxtapartirDe == 0))
                    {
                        if (rtxtcosto1 >= rtxtprecioVenta)
                        {

                            DialogResult result;
                            result = MessageBox.Show("El precio de Venta es menor que el COSTO, Esto Te puede Generar Perdidas", "Producto con Perdidas", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                            if (result == DialogResult.OK)
                            {
                                Insertar_Producto();
                            }
                            else
                            {
                                txtPrecioVenta.Focus();
                            }


                        }
                        else if (rtxtcosto1 < rtxtprecioVenta)
                        {
                            Insertar_Producto();
                        }
                    }
                    else if (rtxtpreciomayoreo != 0 | rtxtapartirDe != 0)
                    {
                        MessageBox.Show("Estas configurando PrecioBase mayoreo, debes completar los campos de PrecioBase mayoreo y A partir de, si no deseas configurarlo dejalos en blanco", "Datos incompletos", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    }
                    sumar_costo_de_inventario_CONTAR_PRODUCTOS();
                }
                else
                {
                    MessageBox.Show("Producto sin descripcion agregada, agrega una descripcion referente al producto",
                        "Valida:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch
            {

            }
        }

        private void datalistadoGrupos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.datalistadoGrupos.Columns["EliminarG"].Index)
            {
                DialogResult result;
                result = MessageBox.Show("¿Realmente desea eliminar este Grupo?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    SqlCommand cmd;
                    try
                    {
                        foreach (DataGridViewRow row in datalistadoGrupos.SelectedRows)
                        {

                            int onekey = Convert.ToInt32(row.Cells["Idline"].Value);

                            try
                            {

                                try
                                {

                                    SqlConnection con = new SqlConnection();
                                    con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                                    con.Open();
                                    cmd = new SqlCommand("eliminar_grupos", con);
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@id", onekey);
                                    cmd.ExecuteNonQuery();

                                    con.Close();

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message);
                            }

                        }
                        txtGrupo.Text = "GENERAL";
                        mostrar_grupos();
                        lblIdGrupo.Text = datalistadoGrupos.SelectedCells[2].Value.ToString();
                        PanelGRUPOSSELECT.Visible = true;
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            if (e.ColumnIndex == this.datalistadoGrupos.Columns["EditarG"].Index)

            {
                lblIdGrupo.Text = datalistadoGrupos.SelectedCells[2].Value.ToString();
                txtGrupo.Text = datalistadoGrupos.SelectedCells[3].Value.ToString();
                PanelGRUPOSSELECT.Visible = false;
                btnguardarGrupo.Visible = false;
                BtnGuardarCambios.Visible = true;
                BtnCancelar.Visible = true;
                btnNuevoGrupo.Visible = false;
            }
            if (e.ColumnIndex == this.datalistadoGrupos.Columns["Grupo"].Index)
            {
                lblIdGrupo.Text = datalistadoGrupos.SelectedCells[2].Value.ToString();
                txtGrupo.Text = datalistadoGrupos.SelectedCells[3].Value.ToString();
                PanelGRUPOSSELECT.Visible = false;
                btnguardarGrupo.Visible = false;
                BtnGuardarCambios.Visible = false;
                BtnCancelar.Visible = false;
                btnNuevoGrupo.Visible = true;
                if (lblEstadoDeCodigo.Text == "NUEVO")
                {
                    GENERAR_CODIGO_DE_BARRAS_AUTOMATICO();
                }

            }


        }
        private void GENERAR_CODIGO_DE_BARRAS_AUTOMATICO()
        {
            if (Editar_datos.ActualizarCodigoProducto() == true)
            {
                string codigo = "";
                Obtener_datos.MostrarCodigoProductoAlGenerar(ref codigo);
                txtcodigodebarras.Text = codigo;
            }
        }
        private void CheckInventarios_CheckedChanged(object sender, EventArgs e)
        {

            if (TXTIDPRODUCTOOk.Text != "0" & Convert.ToDouble(txtstock2.Text) > 0)
            {
                if (CheckInventarios.Checked == false)
                {
                    MessageBox.Show("Hay Aun En Stock, Dirijete al Modulo Inventarios para Ajustar el Inventario a cero", "Stock Existente", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    PANELINVENTARIO.Visible = true;
                    CheckInventarios.Checked = true;
                }
            }

            if (TXTIDPRODUCTOOk.Text != "0" & Convert.ToDouble(txtstock2.Text) == 0)
            {
                if (CheckInventarios.Checked == false)
                {
                    PANELINVENTARIO.Visible = false;

                }
            }

            if (TXTIDPRODUCTOOk.Text == "0")
            {
                if (CheckInventarios.Checked == false)
                {
                    PANELINVENTARIO.Visible = false;

                }
            }

            if (CheckInventarios.Checked == true)
            {

                PANELINVENTARIO.Visible = true;
            }
        }
        private void mostrar_descripcion_produco_sin_repetir()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("mostrar_descripcion_produco_sin_repetir", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@buscar", txtDescripcion.Text);
                da.Fill(dt);
                DATALISTADO_Autocompletar.DataSource = dt;
                con.Close();

                //datalistado.Columns[1].Width = 500;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }



        }
        private void contar()
        {
            int x;

            x = DATALISTADO_Autocompletar.Rows.Count;
            txtcontador = (x);

        }
        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

            DATALISTADO_Autocompletar.Visible = false;

            mostrar_descripcion_produco_sin_repetir();
            contar();

            if (txtcontador == 0)
            {
                DATALISTADO_Autocompletar.Visible = false;
            }
            if (txtcontador > 0)
            {
                DATALISTADO_Autocompletar.Visible = true;
            }
            if (TGUARDAR.Visible == false)
            {
                DATALISTADO_Autocompletar.Visible = false;
            }
        }

        private void BtnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();

                SqlCommand com = new SqlCommand("editar_grupos", cn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", Convert.ToInt32(lblIdGrupo.Text));
                com.Parameters.AddWithValue("@des", txtGrupo.Text);
                int r = com.ExecuteNonQuery();
                cn.Close();

                if (r == 1)
                {
                    MessageBox.Show("Grupo Actualizado Correctamente", "Correcto:", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                BtnGuardarCambios.Visible = false;
                BtnCancelar.Visible = false;
                btnNuevoGrupo.Visible = true;
            }
            catch (Exception)
            {

            }
        }

        private void TimerCalcular_precio_venta_Tick(object sender, EventArgs e)
        {
            TimerCalcular_precio_venta.Stop();

            try
            {
                double TotalVentaVariabledouble;
                double txtcostov = Convert.ToDouble(txtPrecioCosto.Text);
                double txtPorcentajeGananciav = Convert.ToDouble(txtganancia.Text);

                TotalVentaVariabledouble = txtcostov + ((txtcostov * txtPorcentajeGananciav) / 100);

                if (TotalVentaVariabledouble > 0 & txtganancia.Focused == true)
                {
                    this.txtPrecioVenta.Text = Convert.ToString(TotalVentaVariabledouble);
                }
                else
                {
                    //Me.txtPorcentajeGanancia.Text = 0
                }
            }
            catch (Exception)
            {

            }
        }

        private void timerCalular_porcentaje_ganancia_Tick(object sender, EventArgs e)
        {
            timerCalular_porcentaje_ganancia.Stop();
            try
            {


                double TotalVentaVariabledouble;
                double TXTPRECIODEVENTA2V = Convert.ToDouble(txtPrecioVenta.Text);
                double txtcostov = Convert.ToDouble(txtPrecioCosto.Text);

                TotalVentaVariabledouble = ((TXTPRECIODEVENTA2V - txtcostov) / (txtcostov)) * 100;

                if (TotalVentaVariabledouble > 0)
                {
                    this.txtganancia.Text = Convert.ToString(TotalVentaVariabledouble);
                }
                else
                {
                    //Me.txtPorcentajeGanancia.Text = 0
                }


            }
            catch (Exception ex)
            {

            }
        }

        private void txtganancia_TextChanged(object sender, EventArgs e)
        {
            calcularPrecioVenta();
        }
        private void calcularPrecioVenta()
        {
            timerCalular_porcentaje_ganancia.Stop();

            TimerCalcular_precio_venta.Start();
            timerCalular_porcentaje_ganancia.Stop();
        }
        private void datalistadoGrupos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnGenerarCodigoBarra_Click(object sender, EventArgs e)
        {
            GENERAR_CODIGO_DE_BARRAS_AUTOMATICO();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            VolverLimpiar();
        }
        private void VolverLimpiar()
        {
            Panel_RegistrarProductos.Visible = false;
            panelBackBuscador.Visible = true;
            datalistado.Visible = true;
            sumar_costo_de_inventario_CONTAR_PRODUCTOS();
        }
        private void Proceso_para_obtener_datos_productos()
        {
            try
            {
                TGUARDAR.Visible = false;
                TGUARDARCAMBIOS.Visible = true;
                btnNuevoGrupo.Visible = true;
                TXTIDPRODUCTOOk.Text = datalistado.SelectedCells[2].Value.ToString();
                lblEstadoDeCodigo.Text = "EDITAR";
                PanelGRUPOSSELECT.Visible = false;
                BtnGuardarCambios.Visible = false;
                btnguardarGrupo.Visible = false;
                BtnCancelar.Visible = false;
                btnNuevoGrupo.Visible = true;
                panelBackBuscador.Visible = false;
                Panel_RegistrarProductos.Dock = DockStyle.Fill;
                Panel_RegistrarProductos.Visible = true;
                Panel_RegistrarProductos.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {

                TXTIDPRODUCTOOk.Text = datalistado.SelectedCells[2].Value.ToString();
                txtcodigodebarras.Text = datalistado.SelectedCells[3].Value.ToString();
                txtGrupo.Text = datalistado.SelectedCells[4].Value.ToString();

                txtDescripcion.Text = datalistado.SelectedCells[5].Value.ToString();
                txtnumeroigv.Text = datalistado.SelectedCells[6].Value.ToString();
                lblIdGrupo.Text = datalistado.SelectedCells[15].Value.ToString();


                // LBL_ESSERVICIO.Text = datalistado.SelectedCells[7].Value.ToString();



                txtPrecioCosto.Text = datalistado.SelectedCells[8].Value.ToString();
                txtPrecioVenta.Text = datalistado.SelectedCells[17].Value.ToString();
                txtMayoreo.Text = datalistado.SelectedCells[18].Value.ToString();
                LBLSEVENDEPOR.Text = datalistado.SelectedCells[10].Value.ToString();
                if (LBLSEVENDEPOR.Text == "Unidad")
                {
                    rb_porUnidad.Checked = true;

                }
                if (LBLSEVENDEPOR.Text == "Granel")
                {
                    rb_aGranel.Checked = true;
                }
                txtstockminimo.Text = datalistado.SelectedCells[11].Value.ToString();
                lblfechasvenci.Text = datalistado.SelectedCells[12].Value.ToString();
                if (lblfechasvenci.Text == "NO APLICA")
                {
                    No_aplica_fecha.Checked = true;
                }
                if (lblfechasvenci.Text != "NO APLICA")
                {
                    No_aplica_fecha.Checked = false;
                }
                txtstock2.Text = datalistado.SelectedCells[13].Value.ToString();
                
                try
                {

                    double TotalVentaVariabledouble;
                    double TXTPRECIODEVENTA2V = Convert.ToDouble(txtPrecioVenta.Text);
                    double txtcostov = Convert.ToDouble(txtPrecioCosto.Text);

                    TotalVentaVariabledouble = ((TXTPRECIODEVENTA2V - txtcostov) / (txtcostov)) * 100;

                    if (TotalVentaVariabledouble > 0)
                    {
                        this.txtganancia.Text = Convert.ToString(TotalVentaVariabledouble);
                    }
                    else
                    {
                        //Me.txtPorcentajeGanancia.Text = 0
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                //if (LBL_ESSERVICIO.Text == "SI")
                //{

                //    PANELINVENTARIO.Visible = true;
                //    PANELINVENTARIO.Visible = true;
                //    txtstock2.ReadOnly = true;
                //    CheckInventarios.Checked = true;

                //}
                //if (LBL_ESSERVICIO.Text == "NO")
                //{
                //    CheckInventarios.Checked = false;

                //    PANELINVENTARIO.Visible = false;
                //    PANELINVENTARIO.Visible = false;
                //    txtstock2.ReadOnly = true;
                //    txtstock2.Text = "0";
                //    txtstockminimo.Text = "0";
                //    No_aplica_fecha.Checked = true;
                //    txtstock2.ReadOnly = false;
                //}
                txtaPartirDe.Text = datalistado.SelectedCells[16].Value.ToString();


                PanelGRUPOSSELECT.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void datalistado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.datalistado.Columns["Eliminar"].Index)
            {
                DialogResult resul = MessageBox.Show("¿Realmente Deseas Eliminar Este Producto?", "Confirma:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resul == DialogResult.Yes)
                {
                    SqlCommand cma;
                    try
                    {
                        foreach (DataGridViewRow row in datalistado.SelectedRows)
                        {
                            int onekey = Convert.ToInt32(row.Cells["Id_Producto"].Value);
                            try
                            {
                                try
                                {
                                    SqlConnection cn = new SqlConnection();
                                    cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                                    cn.Open();

                                    cma = new SqlCommand("eliminar_producto", cn);
                                    cma.CommandType = CommandType.StoredProcedure;
                                    cma.Parameters.AddWithValue("@id", onekey);
                                    int r = cma.ExecuteNonQuery();
                                    cn.Close();
                                }
                                catch (Exception)
                                {

                                }
                            }
                            catch (Exception)
                            {


                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                buscar();
            }
            if (e.ColumnIndex == this.datalistado.Columns["Editar"].Index)
            {
                Proceso_para_obtener_datos_productos();
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
        private void editar_productos()
        {
            if (txtMayoreo.Text == "0" | txtMayoreo.Text == "") txtaPartirDe.Text = "0";

            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("editar_Producto1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Producto1", TXTIDPRODUCTOOk.Text);
                cmd.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text);

                cmd.Parameters.AddWithValue("@Precio_de_compra", Convert.ToDouble(txtPrecioCosto.Text));
                cmd.Parameters.AddWithValue("@Precio_de_venta", TotalPrecioDeVenta);
                cmd.Parameters.AddWithValue("@Codigo", txtcodigodebarras.Text);
                cmd.Parameters.AddWithValue("@A_partir_de", Convert.ToDouble(txtaPartirDe.Text));
                cmd.Parameters.AddWithValue("@Impuesto", ImpuestoDeVenta);
                cmd.Parameters.AddWithValue("@Precio_mayoreo", TotalPrecioDeMayoreo);
                if (rb_porUnidad.Checked == true) txtse_vende_a.Text = "Unidad";
                if (rb_aGranel.Checked == true) txtse_vende_a.Text = "Granel";

                cmd.Parameters.AddWithValue("@Se_vende_a", txtse_vende_a.Text);
                cmd.Parameters.AddWithValue("@Id_grupo", lblIdGrupo.Text);
                if (PANELINVENTARIO.Visible == true)
                {
                    cmd.Parameters.AddWithValue("@Usa_inventarios", "SI");
                    cmd.Parameters.AddWithValue("@Stock_minimo", txtstockminimo.Text);
                    cmd.Parameters.AddWithValue("@Stock", txtstock2.Text);

                    if (No_aplica_fecha.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Fecha_de_vencimiento", "NO APLICA");
                    }

                    if (No_aplica_fecha.Checked == false)
                    {
                        cmd.Parameters.AddWithValue("@Fecha_de_vencimiento", txtfechaoka.Text);
                    }


                }
                if (PANELINVENTARIO.Visible == false)
                {
                    cmd.Parameters.AddWithValue("@Usa_inventarios", "NO");
                    cmd.Parameters.AddWithValue("@Stock_minimo", 0);
                    cmd.Parameters.AddWithValue("@Fecha_de_vencimiento", "NO APLICA");
                    cmd.Parameters.AddWithValue("@Stock", "99999");

                }
                cmd.Parameters.AddWithValue("@SubtotalPrecioVenta", Convert.ToDouble(txtPrecioVenta.Text));
                cmd.Parameters.AddWithValue("@subtotalMayoreo", Convert.ToDouble(txtMayoreo.Text));
                cmd.Parameters.AddWithValue("@Impuestomayoreo", Convert.ToDouble(ImpuestoMayoreo));
                cmd.ExecuteNonQuery();


                con.Close();
                VolverLimpiar();
                buscar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void TGUARDARCAMBIOS_Click(object sender, EventArgs e)
        {
            double txtpreciomayoreoV = Convert.ToDouble(txtMayoreo.Text);

            double txtapartirdeV = Convert.ToDouble(txtaPartirDe.Text);
            double txtcostoV = Convert.ToDouble(txtPrecioCosto.Text);
            double TXTPRECIODEVENTA2V = Convert.ToDouble(txtPrecioVenta.Text);
            if (txtMayoreo.Text == "") txtMayoreo.Text = "0";
            if (txtaPartirDe.Text == "") txtaPartirDe.Text = "0";
            //TXTPRECIODEVENTA2.Text = TXTPRECIODEVENTA2.Text.Replace(lblmoneda.Text + " ", "");
            //TXTPRECIODEVENTA2.Text = System.String.Format(((decimal)TXTPRECIODEVENTA2.Text), "##0.00");
            if ((txtpreciomayoreoV > 0 & Convert.ToDouble(txtaPartirDe.Text) > 0) | (txtpreciomayoreoV == 0 & txtapartirdeV == 0))
            {
                if (txtcostoV >= TXTPRECIODEVENTA2V)
                {

                    DialogResult result;
                    result = MessageBox.Show("El precio de Venta es menor que el COSTO, Esto Te puede Generar Perdidas", "Producto con Perdidas", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    if (result == DialogResult.OK)
                    {
                        editar_productos();
                    }
                    else
                    {
                        txtPrecioVenta.Focus();
                    }


                }
                else if (txtcostoV < TXTPRECIODEVENTA2V)
                {
                    if (txtDescripcion.Text != "")
                    {
                        editar_productos();
                    }
                    else
                    {
                        MessageBox.Show("Producto sin descripcion agregada, agrega una descripcion referente al producto",
                            "Valida:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else if (txtpreciomayoreoV != 0 | txtapartirdeV != 0)
            {
                MessageBox.Show("Estas configurando Precio mayoreo, debes completar los campos de Precio mayoreo y A partir de, si no deseas configurarlo dejalos en blanco", "Datos incompletos", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            }
            sumar_costo_de_inventario_CONTAR_PRODUCTOS();
        }



        private void btniMPORTAR_Click(object sender, EventArgs e)
        {
            Asistente_de_importacion_Excel frm = new Asistente_de_importacion_Excel();
            frm.ShowDialog();
        }

        private void lblBuscarEtiqueta_Click(object sender, EventArgs e)
        {
            txtbusca.Focus();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtbusca.Clear();
        }

        private void Label42_Click(object sender, EventArgs e)
        {

            if (TXTIDPRODUCTOOk.Text != "0" & Convert.ToDouble(txtstock2.Text) > 0)
            {
                if (CheckInventarios.Checked == false)
                {
                    MessageBox.Show("Hay Aun En Stock, Dirijete al Modulo Inventarios para Ajustar el Inventario a cero", "Stock Existente", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    PANELINVENTARIO.Visible = true;
                    CheckInventarios.Checked = true;
                }
            }

            if (TXTIDPRODUCTOOk.Text != "0" & Convert.ToDouble(txtstock2.Text) == 0)
            {
                if (CheckInventarios.Checked == false)
                {
                    PANELINVENTARIO.Visible = false;

                }
            }

            if (TXTIDPRODUCTOOk.Text == "0")
            {
                if (CheckInventarios.Checked == false)
                {
                    PANELINVENTARIO.Visible = false;

                }
            }

            if (CheckInventarios.Checked == true)
            {

                PANELINVENTARIO.Visible = true;
            }
        }

        private void txtPrecioCosto_TextChanged(object sender, EventArgs e)
        {
            Bases.ValidateSeparatorToNumberInString(ref txtPrecioCosto);
            if (txtganancia.Text != "")
            {
                calcularPrecioVenta();
            }
        }

        private void frm_Productos_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dispose();
        }
        private void CalcularImpuestos(double PrecioBase, ref double precioImpuestos, ref double Total)
        {
            try
            {
                string EstadoImpuestos = "";
                Obtener_datos.ObtenerEstadoImpuestos(ref EstadoImpuestos);
                if (EstadoImpuestos == "SI")
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
                        Cantidad_Decimal_Impuesto = Math.Ceiling(PorcentajeImp) / 100;
                        precioImpuestos = PrecioBase * Cantidad_Decimal_Impuesto;
                        Total = PrecioBase + precioImpuestos;
                        Total = Math.Round(Total, 2);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    precioImpuestos = 0;
                    Total = PrecioBase;
                }
            }
            catch (Exception ex)
            {

            }
        }


        private void txtPrecioVenta_TextChanged(object sender, EventArgs e)
        {
            Bases.ValidateSeparatorToNumberInString(ref txtPrecioVenta);
            try
            {
                double precioBase = Convert.ToDouble(txtPrecioVenta.Text);
                CalcularImpuestos(precioBase, ref ImpuestoDeVenta, ref TotalPrecioDeVenta);
                lblImpuestosVenta.Text = "+ " + Moneda + Bases.AsignarComa(ImpuestoDeVenta) +
                    "(" + PorcentajeImp + "% " + NombreImpuesto + ")";

                lblPrecioVentaFinal.Text = Moneda + Bases.AsignarComa(TotalPrecioDeVenta);
            }
            catch (Exception ex)
            {

            }

        }

        private void txtMayoreo_TextChanged(object sender, EventArgs e)
        {
            Bases.ValidateSeparatorToNumberInString(ref txtMayoreo);

            if (txtMayoreo.Text == "")
                txtMayoreo.Text = "0";

            double precioBase = Convert.ToDouble(txtMayoreo.Text);
            double precioBaseVenta = 0;

            if (txtPrecioVenta.Text != "")
                precioBaseVenta = Convert.ToDouble(txtPrecioVenta.Text);



            if (precioBaseVenta > precioBase || precioBaseVenta == 0 && precioBase == 0)
            {
                try
                {

                    CalcularImpuestos(precioBase, ref ImpuestoMayoreo, ref TotalPrecioDeMayoreo);
                    lblImpuestosMayoreo.Text = "+ " + Moneda + Bases.AsignarComa(ImpuestoMayoreo) +
                        "(" + PorcentajeImp + "% " + NombreImpuesto + ")";

                    lblPrecioMayoreoFinal.Text = Moneda + Bases.AsignarComa(TotalPrecioDeMayoreo);
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                MessageBox.Show("El precio de mayoreo no puede ser mayor o igual al precio de venta",
                   "Verifica:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMayoreo.Text = "0";
            }



        }
    }
}
