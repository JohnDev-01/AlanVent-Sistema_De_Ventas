using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;
using AlanVent_Sistema_De_Ventas.Presentation.Reportes.Reportes_de_kardex.Reporte_de_inventarios;
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

namespace AlanVent_Sistema_De_Ventas.Presentation.Inventarios_Kardex
{
    public partial class Inventarios_menu : Form
    {
        public Inventarios_menu()
        {
            InitializeComponent();
        }
        /*
         * Kardex_Salidas frm = new Kardex_Salidas();
            frm.ShowDialog();

        KardexEntrada frm = new KardexEntrada();
            frm.ShowDialog();

        */
        int IdproductoKardex = 0;

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void Buscar_productos_movimientos()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("BUSCAR_PRODUCTOS_KARDEX", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letrab", txtBuscarMovimiento.Text);
                da.Fill(dt);
                DATALISTADO_Autocompletar.DataSource = dt;
                con.Close();


                DATALISTADO_Autocompletar.Columns[1].Visible = false;
                DATALISTADO_Autocompletar.Columns[3].Visible = false;
                DATALISTADO_Autocompletar.Columns[4].Visible = false;
                DATALISTADO_Autocompletar.Columns[5].Visible = false;
                DATALISTADO_Autocompletar.Columns[6].Visible = false;
                DATALISTADO_Autocompletar.Columns[7].Visible = false;
                DATALISTADO_Autocompletar.Columns[8].Visible = false;
                DATALISTADO_Autocompletar.Columns[9].Visible = false;
                DATALISTADO_Autocompletar.Columns[10].Visible = false;
                DATALISTADO_Autocompletar.Columns[11].Visible = false;
                DATALISTADO_Autocompletar.Columns[12].Visible = false;
                DATALISTADO_Autocompletar.Columns[13].Visible = false;
                DATALISTADO_Autocompletar.Columns[14].Visible = false;
                DATALISTADO_Autocompletar.Columns[15].Visible = false;
                DATALISTADO_Autocompletar.Columns[16].Visible = false;
                DATALISTADO_Autocompletar.Columns[2].Width = 320;
                DATALISTADO_Autocompletar.BringToFront();


            }
            catch (Exception ex)
            {

            }
        }

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {

            if (txtBuscarMovimiento.Text == "")
            {
                DATALISTADO_Autocompletar.Visible = false;
                lblBuscarProductoMovimiento.Visible = true;
            }
            else
            {
                DATALISTADO_Autocompletar.Location = new Point(208, 36);
                this.Controls.Add(DATALISTADO_Autocompletar);
                lblBuscarProductoMovimiento.Visible = false;
                DATALISTADO_Autocompletar.Visible = true;
                DATALISTADO_Autocompletar.BringToFront();
                Buscar_productos_movimientos();
            }

        }
        public static int IdProducto = 0;
        private void DATALISTADO_Autocompletar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblNombreP.Text = "Producto: " + DATALISTADO_Autocompletar.SelectedCells[2].Value.ToString();
                DATALISTADO_Autocompletar.Visible = false;
                txtBuscarMovimiento.Clear();
                buscar_MOVIMIENTOS_DE_KARDEX();
                IdProducto = Convert.ToInt32(DATALISTADO_Autocompletar.SelectedCells[1].Value.ToString());
            }
            catch
            {

            }
        }
        private void CargarMovimientosKardex()
        {
            try
            {
                var Query = "Select Kardex.Fecha,Productos1.Id_Producto,Kardex.Motivo as Movimiento, Kardex.Habia,Kardex.Tipo,Kardex.Cantidad,Kardex.Hay,Usuarios.NombreYApellido as Cajero,Grupo_de_Productos.Linea as Categoria" +
                 "From Kardex inner Join Productos1 on Productos1.Id_Producto = Kardex.Id_producto inner join Usuarios on Usuarios.ID = Kardex.Id_usuario" +
                "cross join EMPRESA" +
                 "inner join Grupo_de_Productos on Grupo_de_Productos.Idline = Productos1.Id_Grupo" +
                 "where order by Kardex.Fecha desc";

                DataTable dt = new DataTable();
                SqlDataAdapter dta;
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();

                dta = new SqlDataAdapter(Query, cn);
                dta.Fill(dt);
                DatalistadoMovimientos.DataSource = dt;
                cn.Close();
                Logic.Bases.Multilinea(ref DatalistadoMovimientos);
            }
            catch (Exception)
            {


            }
        }
        private void buscar_MOVIMIENTOS_DE_KARDEX()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("Buscar_Movimientos_De_Kardex", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idProducto", DATALISTADO_Autocompletar.SelectedCells[1].Value.ToString());
                da.Fill(dt);

                DatalistadoMovimientos.DataSource = dt;

                con.Close();


                DatalistadoMovimientos.Columns[0].Visible = false;
                DatalistadoMovimientos.Columns[10].Visible = false;
                DatalistadoMovimientos.Columns[11].Visible = false;

                Logic.Bases.Multilinea(ref DatalistadoMovimientos);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void buscar_MOVIMIENTOS_FILTROS_ACUMULADO()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("buscar_MOVIMIENTOS_DE_KARDEX_filtros_acumulado", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fecha", dtDelDia.Value);
                da.SelectCommand.Parameters.AddWithValue("@tipo", cbTiposMovimientos.Text);
                da.SelectCommand.Parameters.AddWithValue("@Id_usuario", txtIdusuario.Text);


                da.Fill(dt);
                datalistadoMovientoAcumuladoProductos.DataSource = dt;
                con.Close();

                datalistadoMovientoAcumuladoProductos.Columns[0].Visible = false;
                datalistadoMovientoAcumuladoProductos.Columns[4].Visible = false;
                datalistadoMovientoAcumuladoProductos.Columns[5].Visible = false;
                datalistadoMovientoAcumuladoProductos.Columns[6].Visible = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Mostrar_Inventarios_bajo_minimo()
        {
            try
            {
                DataTable dt = new DataTable();
                Obtener_datos.MostrarProductosBajoMinimo(ref dt);
                datalistadoinventariosBajos.DataSource = dt;

                datalistadoinventariosBajos.Columns[0].Visible = false;
                datalistadoinventariosBajos.Columns[4].Visible = false;
                datalistadoinventariosBajos.Columns[7].Visible = false;
                datalistadoinventariosBajos.Columns[8].Visible = false;
                datalistadoinventariosBajos.Columns[9].Visible = false;
                Logic.Bases.Multilinea(ref datalistadoinventariosBajos);
            }
            catch (Exception)
            {

            }
        }
        internal void Buscar_id_USUARIOS()
        {

            string resultado;
            string queryMoneda;
            queryMoneda = "Buscar_id_USUARIOS";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = DataAccess.ConexionMaestra.conexion;

            SqlCommand comMoneda = new SqlCommand(queryMoneda, con);
            comMoneda.CommandType = CommandType.StoredProcedure;
            comMoneda.Parameters.AddWithValue("@Nombre_Y_Apellidos", cbVendedor.Text);
            try
            {
                con.Open();
                resultado = Convert.ToString(comMoneda.ExecuteScalar()); //asignamos el valor del importe
                txtIdusuario.Text = resultado;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                resultado = "";
            }
        }
        private void BuscarUsuario()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter data;
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();

                data = new SqlDataAdapter("select * from Usuarios where Estado = 'ACTIVO'", cn);
                data.Fill(dt);
                cbVendedor.DisplayMember = "NombreYApellido";
                cbVendedor.ValueMember = "ID";
                cbVendedor.DataSource = dt;

                cn.Close();
                Buscar_id_USUARIOS();
            }
            catch (Exception)
            {

            }
        }
        private void buscar_MOVIMIENTOS_FILTROS()
        {
            try
            {
                var Obj = new
                {
                    fecha = dtDelDia.Value,
                    tipo = cbTiposMovimientos.Text,
                    IdUsuario = txtIdusuario.Text
                };
                DataTable dt = new DataTable();
                Obtener_datos.BuscarMovKardex_filtros(ref dt, Obj.fecha, Obj.tipo, Obj.IdUsuario);
                DatalistadoMovimientos.DataSource = dt;
                DatalistadoMovimientos.Columns[0].Visible = false;
                DatalistadoMovimientos.Columns[10].Visible = false;
                DatalistadoMovimientos.Columns[11].Visible = false;
                DatalistadoMovimientos.Columns[9].Visible = false;
                DatalistadoMovimientos.Columns[13].Visible = false;
                DatalistadoMovimientos.Columns[14].Visible = false;
                DatalistadoMovimientos.Columns[12].Visible = false;
                Logic.Bases.Multilinea(ref DatalistadoMovimientos);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ActivarfiltrosAvanzadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            DATALISTADO_Autocompletar.Visible = false;
            cbTiposMovimientos.Text = "--Todos--";
            buscar_MOVIMIENTOS_FILTROS();
            buscar_MOVIMIENTOS_FILTROS_ACUMULADO();
            panel6.Visible = true;
        }
        //================================================================================================
        private void panel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbVendedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Buscar_id_USUARIOS();
            BuscarMovimientosFiltros();
        }

        private void ocultarFiltroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarFiltrosMovimientos();
            menuStrip3.Visible = true;

        }
        private void OcultarFiltrosMovimientos()
        {
            groupBox1.Visible = false;
            panel6.Visible = false;

            DataTable dt = new DataTable();
            DatalistadoMovimientos.DataSource = null;
            cbTiposMovimientos.Text = "--Todos--";
            txtBuscarInventarios.Clear();
            menuStrip3.Visible = true;
        }
        private void dtDelDia_ValueChanged(object sender, EventArgs e)
        {
            BuscarMovimientosFiltros();
        }
        private void BuscarMovimientosFiltros()
        {
            if (groupBox1.Visible == true)
            {
                buscar_MOVIMIENTOS_FILTROS();
                buscar_MOVIMIENTOS_FILTROS_ACUMULADO();
            }
        }
        private void cbTiposMovimientos_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuscarMovimientosFiltros();

        }
        Reportes.Reportes_de_kardex.Reportes_de_kardex_diseno.Report_Kardex_Movimientos rpt = new Reportes.Reportes_de_kardex.Reportes_de_kardex_diseno.Report_Kardex_Movimientos();
        private void Mostrar_kardex_inventarios()
        {
            try
            {
                var dtSALIDAS = new DataTable();
                var dtENTRADAS = new DataTable();
                var dtSumas = new DataTable();
                Obtener_datos.MostrarReporteKARDEXSalidas(ref dtSALIDAS, IdproductoKardex);
                Obtener_datos.MostrarReporteKARDEXEntradas(ref dtENTRADAS, IdproductoKardex);
                SumarCamposKARDEX(ref dtSumas, dtENTRADAS, dtSALIDAS);
                var rpt = new Reportes.Reportes_de_kardex.Reportes_de_kardex_diseno.Report_Kardex_Movimientos();
                rpt.DataSource = dtSALIDAS;
                rpt.tableEntradas.DataSource = dtENTRADAS;
                rpt.tableSalidas.DataSource = dtSALIDAS;
                rpt.tableSumas.DataSource = dtSumas;
                reportViewer1.Report = rpt;
                reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void SumarCamposKARDEX(ref DataTable dtResultados, DataTable dtEntradas, DataTable dtSalidas)
        {
            double TotalSalidas = 0;
            double TotalEntradas = 0;
            double CostoEx = 0;
            double ExistenciaActual = 0;
            double CantidadSalidas = 0;
            double CantidadEntradas = 0;
            //Salidas:
            try
            {
                foreach (DataRow item in dtSalidas.Rows)
                {
                    TotalSalidas += Convert.ToDouble(item["Total"].ToString());
                    ExistenciaActual = Convert.ToDouble(item["Stock"].ToString());
                    CantidadSalidas += Convert.ToDouble(item["Cantidad"].ToString());
                }
            }
            catch (Exception ex)
            {

            }
            //Entradas:
            try
            {
                foreach (DataRow item in dtEntradas.Rows)
                {
                    TotalEntradas += (Convert.ToDouble(item["Total"].ToString()));
                    CantidadEntradas += Convert.ToDouble(item["Cantidad"].ToString());
                }
            }
            catch (Exception ex)
            {

            }
            CostoEx = TotalEntradas - TotalSalidas;

            #region Asignacion de Resultados 
            dtResultados.Columns.Add("TotalSalidas");
            dtResultados.Columns.Add("TotalEntradas");
            dtResultados.Columns.Add("CostoEx");
            dtResultados.Columns.Add("Stock");
            dtResultados.Columns.Add("CantSalidas");
            dtResultados.Columns.Add("CantEntradas");

            DataRow row = dtResultados.NewRow();
            row["TotalSalidas"] = Bases.AsignarComa(TotalSalidas);
            row["TotalEntradas"] = Bases.AsignarComa(TotalEntradas);
            row["CostoEx"] = Bases.AsignarComa(CostoEx);
            row["Stock"] = Bases.AsignarComa(ExistenciaActual);
            row["CantSalidas"] = Bases.AsignarComa(CantidadSalidas);
            row["CantEntradas"] = Bases.AsignarComa(CantidadEntradas);
            dtResultados.Rows.Add(row);
            #endregion
        }
        private void Inventarios_menu_Load(object sender, EventArgs e)
        {
            BotonKardexAsync();
            BuscarUsuario();


        }


        private void Mostrar_Inventarios_Todos()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("mostrar_inventarios_todos", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", txtBuscarInventarios.Text);


                da.Fill(dt);
                DatalistadoInventariosReport.DataSource = dt;
                con.Close();

                DatalistadoInventariosReport.Columns[0].Visible = false;
                DatalistadoInventariosReport.Columns[9].Visible = false;
                DatalistadoInventariosReport.Columns[10].Visible = false;
                DatalistadoInventariosReport.Columns[11].Visible = false;

                Logic.Bases.Multilinea(ref DatalistadoInventariosReport);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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

            double importe;
            string query;
            query = "SELECT      CONVERT(NUMERIC(18,2),sum(Productos1.Precio_de_compra * Stock )) as suma FROM  Productos1 where  Usa_inventarios ='SI'";

            SqlCommand com = new SqlCommand(query, con);
            try
            {
                con.Open();
                importe = Convert.ToDouble(com.ExecuteScalar()); //asignamos el valor del importe
                con.Close();
                // lblCostoInventarios.Text = resultadoDiferencia + " " + importe;
                lblCostoInventario.Text = "$" + " " + Bases.AsignarComa(importe);
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);

                lblCostoInventario.Text = resultado + " " + 0;
            }

            double conteoresultado;
            string querycontar;
            querycontar = "select count(Id_Producto) from Productos1 ";
            SqlCommand comcontar = new SqlCommand(querycontar, con);
            try
            {
                con.Open();
                conteoresultado = Convert.ToDouble(comcontar.ExecuteScalar()); //asignamos el valor del importe
                con.Close();
                lblCantidadDeProductosEnInventarios.Text = Bases.AsignarComa(conteoresultado);
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);

                conteoresultado = 0;
                lblCantidadDeProductosEnInventarios.Text = "0";
            }

        }
        private void txtBuscarInventarios_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarInventarios.Text != "")
            {
                lblBusqueda.Visible = false;

                Mostrar_Inventarios_Todos();
            }
            else
            {
                lblBusqueda.Visible = true;
            }
        }


        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            txtBuscarInventarios.Clear();
            Mostrar_Inventarios_Todos();
        }
        private void buscar_productos_Vencidos()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("buscar_productos_vencidos", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", txtBuscarVencimientos.Text);


                da.Fill(dt);
                datalistadoVencimientos.DataSource = dt;
                con.Close();

                datalistadoVencimientos.Columns[0].Visible = false;
                datalistadoVencimientos.Columns[1].Visible = false;
                datalistadoVencimientos.Columns[6].Visible = false;
                datalistadoVencimientos.Columns[7].Visible = false;

                Logic.Bases.Multilinea(ref datalistadoVencimientos);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void txtBuscarVencimientos_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarVencimientos.Text == "")
            {
                label10.Visible = true;
            }
            else
            {
                label10.Visible = false;
                buscar_productos_Vencidos();
                CheckPorVencer30Dias.Checked = false;
                checkProductosVencidos.Checked = false;
            }
        }

        private void txtBuscarVencimientos_Click(object sender, EventArgs e)
        {
            txtBuscarVencimientos.SelectAll();
        }
        private void buscar_productos_Vencidos_menos_30_dias()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("mostrar_productos_vencidos_en_menos_de_30_dias", con);


                da.Fill(dt);
                datalistadoVencimientos.DataSource = dt;
                con.Close();

                datalistadoVencimientos.Columns[0].Visible = false;
                datalistadoVencimientos.Columns[1].Visible = false;

                Logic.Bases.Multilinea(ref datalistadoVencimientos);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void mostrar_productos_Vencidos()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("mostrar_productos_vencidos", con);


                da.Fill(dt);
                datalistadoVencimientos.DataSource = dt;
                con.Close();

                datalistadoVencimientos.Columns[0].Visible = false;
                datalistadoVencimientos.Columns[1].Visible = false;
                datalistadoVencimientos.Columns[7].Visible = false;

                Logic.Bases.Multilinea(ref datalistadoVencimientos);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void CheckPorVencer30Dias_CheckedChanged(object sender, EventArgs e)
        {
            buscar_productos_Vencidos_menos_30_dias();
            txtBuscarVencimientos.Clear();
        }

        private void checkProductosVencidos_CheckedChanged(object sender, EventArgs e)
        {
            mostrar_productos_Vencidos();
            txtBuscarVencimientos.Clear();
        }

        private void kardexToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void BotonKardexAsync()
        {

            panelInventariosBajos.Visible = false;
            panelMovimientos.Visible = false;
            PanelVencimientos.Visible = false;
            PanelKardex.Visible = true;
            Panel_Reportes_Inventarios.Visible = false;

            PanelKardex.BringToFront();
            PanelKardex.Dock = DockStyle.Fill;
            OcultarPColores();
            CK.Visible = true;
            lblNombreProducto.Text = "";
            reportViewer1.ReportSource = null;
            reportViewer1.RefreshReport();


        }
        private void OcultarPColores()
        {
            CK.Visible = false;
            CI.Visible = false;
            CM.Visible = false;
            CR.Visible = false;
            CV.Visible = false;
        }

        private void buscarProductos_Kardex()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("BUSCAR_PRODUCTOS_KARDEX", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letrab", txtBuscarKardexMovimientos.Text);


                da.Fill(dt);
                datalistadoproductoskardex.DataSource = dt;
                con.Close();

                datalistadoproductoskardex.Columns[0].Visible = false;
                datalistadoproductoskardex.Columns[1].Visible = false;
                datalistadoproductoskardex.Columns[3].Visible = false;
                datalistadoproductoskardex.Columns[4].Visible = false;
                datalistadoproductoskardex.Columns[5].Visible = false;
                datalistadoproductoskardex.Columns[6].Visible = false;
                datalistadoproductoskardex.Columns[7].Visible = false;
                datalistadoproductoskardex.Columns[8].Visible = false;
                datalistadoproductoskardex.Columns[9].Visible = false;
                datalistadoproductoskardex.Columns[10].Visible = false;
                datalistadoproductoskardex.Columns[11].Visible = false;
                datalistadoproductoskardex.Columns[12].Visible = false;
                datalistadoproductoskardex.Columns[13].Visible = false;
                datalistadoproductoskardex.Columns[14].Visible = false;
                datalistadoproductoskardex.Columns[15].Visible = false;
                //Logic.Bases.Multilinea(ref datalistadoproductoskardex);
                datalistadoproductoskardex.Columns[2].Width = 375;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarKardexMovimientos.Text == "")
            {
                datalistadoproductoskardex.Visible = false;
                lblBuscarEtiqueta.Visible = true;
            }
            else
            {

                lblBuscarEtiqueta.Visible = false;
                datalistadoproductoskardex.Visible = true;
                buscarProductos_Kardex();
            }
        }

        private void datalistadoproductoskardex_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblNombreProducto.Text = "Producto: " + datalistadoproductoskardex.SelectedCells[2].Value.ToString();
            txtBuscarKardexMovimientos.Clear();
            IdproductoKardex = Convert.ToInt32(datalistadoproductoskardex.SelectedCells[1].Value.ToString());
            datalistadoproductoskardex.Visible = false;
            Mostrar_kardex_inventarios();
        }

        private void imprimirSinFiltroToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormMovimientoBuscar frm = new FormMovimientoBuscar();
            frm.ShowDialog();
        }

        private void DATALISTADO_Autocompletar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public static string TipoMovimiento;
        public static DateTime Fecha;
        public static int Id_Usuario;
        private void imprimirConFiltroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TipoMovimiento = cbTiposMovimientos.Text;
            Fecha = dtDelDia.Value;
            Id_Usuario = Convert.ToInt32(txtIdusuario.Text);

            //FormReporteMovimientosFiltros rm = new FormReporteMovimientosFiltros();
            //rm.ShowDialog();
        }
        //¡¡¡¡¡¡¡¡¡¡¡==========================================================================
        private void lblEntrada_Click(object sender, EventArgs e)
        {
            KardexEntrada frm = new KardexEntrada();
            frm.ShowDialog();

        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            KardexEntrada frm = new KardexEntrada();
            frm.ShowDialog();
        }

        private void lblSalida_Click(object sender, EventArgs e)
        {
            Kardex_Salidas frm = new Kardex_Salidas();
            frm.ShowDialog();
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            Kardex_Salidas frm = new Kardex_Salidas();
            frm.ShowDialog();
        }

        private void btnKardex_Click(object sender, EventArgs e)
        {
            BotonKardexAsync();
        }
        private void BotonMovimientos()
        {
            panelInventariosBajos.Visible = false;
            panelMovimientos.Visible = true;
            PanelVencimientos.Visible = false;
            PanelKardex.Visible = false;
            Panel_Reportes_Inventarios.Visible = false;

            panelMovimientos.BringToFront();
            panelMovimientos.Dock = DockStyle.Fill;

            OcultarPColores();
            CM.Visible = true;
            lblNombreP.Text = "";
            CargarMovimientosKardex();
        }
        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            BotonMovimientos();
            //OcultarFiltrosMovimientos();
        }
        private void BotonInventarios()
        {
            panelInventariosBajos.Visible = true;
            panelMovimientos.Visible = false;
            PanelVencimientos.Visible = false;
            PanelKardex.Visible = false;
            Panel_Reportes_Inventarios.Visible = false;

            panelInventariosBajos.BringToFront();
            panelInventariosBajos.Dock = DockStyle.Fill;

            OcultarPColores();
            CI.Visible = true;


            Mostrar_Inventarios_bajo_minimo();
        }
        private void btnInventariosB_Click(object sender, EventArgs e)
        {
            BotonInventarios();
        }
        private void BotonReporteInv()
        {
            panelInventariosBajos.Visible = false;
            panelMovimientos.Visible = false;
            PanelVencimientos.Visible = false;
            PanelKardex.Visible = false;
            Panel_Reportes_Inventarios.Visible = true;

            Panel_Reportes_Inventarios.BringToFront();
            Panel_Reportes_Inventarios.Dock = DockStyle.Fill;

            OcultarPColores();
            CR.Visible = true;

            Mostrar_Inventarios_Todos();
            sumar_costo_de_inventario_CONTAR_PRODUCTOS();
        }
        private void btnReporteInv_Click(object sender, EventArgs e)
        {
            BotonReporteInv();
        }
        private void BotonVencimiento()
        {
            panelInventariosBajos.Visible = false;
            panelMovimientos.Visible = false;
            PanelVencimientos.Visible = true;
            PanelKardex.Visible = false;
            Panel_Reportes_Inventarios.Visible = false;

            PanelVencimientos.BringToFront();
            PanelVencimientos.Dock = DockStyle.Fill;

            OcultarPColores();
            CV.Visible = true;

            buscar_productos_Vencidos();
        }
        private void btnVencimiento_Click(object sender, EventArgs e)
        {
            BotonVencimiento();
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            BotonKardexAsync();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            BotonMovimientos();

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            BotonInventarios();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            BotonReporteInv();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            BotonVencimiento();
        }

        private void lblBuscarEtiqueta_Click(object sender, EventArgs e)
        {
            txtBuscarKardexMovimientos.Focus();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            txtBuscarKardexMovimientos.Focus();
        }
        private void OcultarDGV_Busquedas()
        {
            if (datalistadoproductoskardex.Visible == true)
            {
                datalistadoproductoskardex.Visible = false;
            }
        }
        private void panel15_Click(object sender, EventArgs e)
        {
            OcultarDGV_Busquedas();
        }

        private void lblNombreProducto_Click(object sender, EventArgs e)
        {
            OcultarDGV_Busquedas();
        }

        private void reportViewer1_Click(object sender, EventArgs e)
        {
            OcultarDGV_Busquedas();
        }

        private void lblBuscarProductoMovimiento_Click(object sender, EventArgs e)
        {
            txtBuscarMovimiento.Focus();
        }

        private void DATALISTADO_Autocompletar_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        public static void OcultarVisorInventariosB()
        {

        }
        public static void OcultarVisorReporte()
        {

        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panelInventariosBajos.Visible = false;
            panelRptInventariosBajos.Visible = true;
            panelRptInventariosBajos.Dock = DockStyle.Fill;
            CargarReporteInv_Bajos();
        }
        private void CargarReporteInv_Bajos()
        {
            DataTable dt = new DataTable();
            string NombreEmpresa = "";
            Obtener_datos.MOSTRAR_Inventarios_bajo_minimo(ref dt, ref NombreEmpresa);
            var dataNameEmpresa = new DataTable();
            dataNameEmpresa.Columns.Add("Nombre_Empresa");
            dataNameEmpresa.Rows.Add(NombreEmpresa);
            ReportePbajomin rpt = new ReportePbajomin();
            rpt.table1.DataSource = dt;
            rpt.DataSource = dt;
            rpt.DataSource = dataNameEmpresa;
            reportViewer2.Report = rpt;
            reportViewer2.RefreshReport();
        }
        private void btnAtras_Click(object sender, EventArgs e)
        {
            panelInventariosBajos.Visible = true;
            panelRptInventariosBajos.Visible = false;

        }

        private void btnMostrarTodosInv_Click(object sender, EventArgs e)
        {
            txtBuscarInventarios.Clear();
            Mostrar_Inventarios_Todos();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimirInventarios_Click(object sender, EventArgs e)
        {
            var frm = new Reportes.Inventarios.Inventario_Reporte();
            frm.busqueda = txtBuscarInventarios.Text;
            frm.ShowDialog();
        }

        private void PanelKardex_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
