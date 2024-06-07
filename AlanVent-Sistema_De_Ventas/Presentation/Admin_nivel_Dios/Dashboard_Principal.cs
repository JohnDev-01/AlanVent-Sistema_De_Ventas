using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;
using AlanVent_Sistema_De_Ventas.Presentation.Configuracion;
using AlanVent_Sistema_De_Ventas.Presentation.Membrecias;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.Admin_nivel_Dios
{
    public partial class Dashboard_Principal : Form
    {
        public Dashboard_Principal()
        {
            InitializeComponent();
        }
        string txtlogin;
        int contador;
        int contador_movimientos_validar_caja;

        public static int idusuariovariable;
        public static int idcajavariable;
        string lblRol;
        string cajero = "Cajero (Si esta autorizado para manejar dinero)";
        string vendedor = "Solo Ventas (no esta autorizado para manejar dinero)";
        string administrador = "Administrador (Control totalAPagar)";
        int contadorCajas;
        string lblApertura_De_Caja;
        string lblSerial;
        string Base_De_datos = "AlanVent_SistemaDeVentas";
        string Servidor = @".\SQLEXPRESS";
        string ruta;
        private string resultado_licencia;
        private string fechaFinal;
        double PorCobrar;
        double PorPagar;
        double GananciasGenerales;
        int ProductoMinimo;
        int CantClientes;
        int CantProductos;
        string moneda;
        DataTable dtVentas;
        DataTable dtProductos;
        double totalVentas;
        double Gananciasfecha;
        int año;  
        private void Dashboard_Principal_Load(object sender, EventArgs e)
        {
            ValidarLicencia();
            Logic.Bases.Obtener_serialPc(ref lblSerial);
            Obtener_datos.Obtener_id_caja_PorSerial(ref idcajavariable);
            Obtener_datos.mostrar_inicio_De_sesion(ref idusuariovariable);
            Mostrar_Moneda();
            ReportePorCobrar();
            ReportePorPagar();
            ObtenerMesAnoActual();
            ReporteProductoBajoMinimo();
            ReporteCantClientes();
            ReporteCantProductos();
            mostrarVentasGrafica();
            checkFiltros.Checked = false;
            mostrarPmasvendidos();
            ReporteGastosAnioCombo();
            mostrarPanelInicio();
        }
        private void mostrarPanelInicio()
        {
            panelInicio.Visible = true;
            panelInicio.Dock = DockStyle.Fill;
            panelInicio.BringToFront();
            panelResumenVentas.Visible = false;
            panelResumenGastos.Visible = false;
            btnInicio.BackgroundImage = AlanVent_Sistema_De_Ventas.Properties.Resources.azul;
            btnResumenVentas.BackgroundImage = null;
            btnResumenGastos.BackgroundImage = null;
        }
        private void ObtenerMesAnoActual()
        {
            int ano = DateTime.Today.Year;
            DateTime fActual = DateTime.Now;
            string mes = fActual.ToString("MMMM") + " " + ano.ToString();
            lblFechaHoy.Text = mes;
        }
        private void ReporteGananciasFechas()
        {
            Obtener_datos.ReporteGananciasFechas(ref Gananciasfecha, txtFi.Value, txtff.Value);
            lblgananciasok.Text = moneda + " " + Bases.AsignarComa(Gananciasfecha);
        }
        private void ReporteTotalVentas()
        {
            Obtener_datos.ReporteTotalVentas(ref totalVentas);
            txtventas.Text = moneda + " " + Bases.AsignarComa(totalVentas);

        }
        private void ReporteTotalVentasFechas()
        {
            Obtener_datos.ReporteTotalVentasFechas(ref totalVentas, txtFi.Value, txtff.Value);
            txtventas.Text = moneda + " " + Bases.AsignarComa(totalVentas);
        }
        private void mostrarVentasGrafica()
        {
            ArrayList fecha = new ArrayList();
            ArrayList monto = new ArrayList();
            dtVentas = new DataTable();
            Obtener_datos.mostrarVentasGrafica(ref dtVentas);
            foreach (DataRow item in dtVentas.Rows)
            {
                fecha.Add(item["fecha"]);
                monto.Add(item["Total"]);
            }
            chartVentas.Series[0].Points.DataBindXY(fecha, monto);
            ReporteTotalVentas();
            ReporteGanacias();
        }
        private void mostrarPmasvendidos()
        {
            ArrayList cantidad = new ArrayList();
            ArrayList producto = new ArrayList();
            dtProductos = new DataTable();
            Obtener_datos.mostrarPmasvendidos(ref dtProductos);
            foreach (DataRow item in dtProductos.Rows)
            {

                cantidad.Add(Bases.AsignarComa(Convert.ToDouble(item["Cantidad"])));
                producto.Add(item["Descripcion"]);
            }
            chartProductos.Series[0].Points.DataBindXY( producto, cantidad);

        }
        private void ReporteGastosAnioCombo()
        {
            DataTable dt = new DataTable();
            Obtener_datos.ReporteGastosAnioCombo(ref dt);
            cbGastosAno.DisplayMember = "anio";
            cbGastosAno.ValueMember = "anio";
            cbGastosAno.DataSource = dt;
        }
        private void mostrarVentasGraficaFechas()
        {
            ArrayList fecha = new ArrayList();
            ArrayList monto = new ArrayList();
            dtVentas = new DataTable();
            Obtener_datos.mostrarVentasGraficaFechas(ref dtVentas,txtFi.Value,txtff.Value);
            foreach (DataRow item in dtVentas.Rows)
            {
                fecha.Add(item["fecha"]);
                monto.Add(item["Total"]);
            }
            chartVentas.Series[0].Points.DataBindXY(fecha, monto);
            ReporteTotalVentasFechas();
            ReporteGananciasFechas();
        }
        private void Mostrar_Moneda()
        {
            Obtener_datos.MostrarMoneda(ref moneda);
        }
        private void ReporteCantProductos()
        {
            Obtener_datos.ReporteCantProductos(ref CantProductos);
            lblNproductos.Text = Bases.AsignarComa(CantProductos);
        }
        private void ReporteCantClientes()
        {
            Obtener_datos.ReporteCantClientes(ref CantClientes);
            lblNClientes.Text = Bases.AsignarComa(CantClientes);
        }
        private void ReporteProductoBajoMinimo()
        {
            Obtener_datos.ReporteProductoBajoMinimo(ref ProductoMinimo);
            lblStockMinimo.Text = Bases.AsignarComa(ProductoMinimo);
        }
        private void ReportePorCobrar()
        {
          Obtener_datos.ReportePorCobrar(ref PorCobrar);
            lblPorCobrar.Text = moneda + " " + Bases.AsignarComa(PorCobrar);
        }
        private void ReporteGanacias()
        {
            Obtener_datos.ReporteGanacias(ref GananciasGenerales);
            lblganancias.Text = moneda + " " + Bases.AsignarComa(GananciasGenerales);
            lblgananciasok.Text = lblganancias.Text;
        }
        private  void ReportePorPagar()
        {
            Obtener_datos.ReportePorPagar(ref PorPagar);
            lblPorPagar.Text = moneda + " " + Bases.AsignarComa(PorPagar);
        }
        private void ValidarLicencia()
        {
            DLicencias funcion = new DLicencias();
            funcion.ValidarLicencias(ref resultado_licencia, ref fechaFinal);
            if (resultado_licencia == "?ACTIVO?")
            {
                lblestadoLicencia.Text = "Licencia de prueba activada hasta: " + fechaFinal;
                lblestadoLicencia.ForeColor = Color.Red;
                btnLicencia.Visible = true;
            }
            if (resultado_licencia == "?ACTIVADO PRO?")
            {
                lblestadoLicencia.Text = "Licencia PROFESIONAL Activada hasta el: " + fechaFinal;
                lblestadoLicencia.ForeColor = Color.DimGray;
                btnLicencia.Visible = false;
            }
            if (resultado_licencia == "VENCIDA")
            {
                funcion.EditarMarcanVencidas();
                Dispose();
                Membresias_Nuevo frm = new Membresias_Nuevo();
                frm.ShowDialog();
            }

        }
        private void btnConfiguraciones_Click(object sender, EventArgs e)
        {
            Dispose();
            Panel_Configueraciones frm = new Panel_Configueraciones();
            frm.ShowDialog();
        }
        private void btnInventarios_Click(object sender, EventArgs e)
        {
            Inventarios_Kardex.Inventarios_menu frm = new Inventarios_Kardex.Inventarios_menu();
            frm.ShowDialog();

        }


        private void btnvender_Click(object sender, EventArgs e)
        {
            validar_aperturas_de_caja();
        }
        private void ListarCierres_Caja()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("MOSTRAR_MOVIMIENTOS_DE_CAJA_POR_SERIAL", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@serial", lblSerial);
                da.Fill(dt);
                datalistado_detalle_cierre_caja.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            //Bases.Multilinea(ref datalistado);

        }
        private void contarCierre_De_Caja()
        {
            int x;

            x = datalistado_detalle_cierre_caja.Rows.Count;
            contadorCajas = x;
        }
        private void Aperturar_detalle_cierre_caja()
        {
            try
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();

                SqlCommand com = new SqlCommand("insertar_DETALLE_cierre_de_caja", cn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@fechaini", DateTime.Now);
                com.Parameters.AddWithValue("@fechafin", DateTime.Now);
                com.Parameters.AddWithValue("@fechacierre", DateTime.Now);
                com.Parameters.AddWithValue("@ingresos", "0.00");
                com.Parameters.AddWithValue("@egresos", "0.00");
                com.Parameters.AddWithValue("@saldo", "0.00");
                com.Parameters.AddWithValue("@idusuario", idusuariovariable);
                com.Parameters.AddWithValue("@totalcaluclado", "0.00");
                com.Parameters.AddWithValue("@totalreal", "0.00");
                com.Parameters.AddWithValue("@estado", "CAJA APERTURADA");
                com.Parameters.AddWithValue("@diferencia", "0.00");
                com.Parameters.AddWithValue("@id_caja", idcajavariable);
                com.ExecuteNonQuery();
                cn.Close();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void Mostrar_movimientos_de_caja_por_serial_y_usuario()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("MOSTRAR_MOVIMIENTOS_DE_CAJA_POR_SERIAL_y_usuario", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@serial", lblSerial);
                da.SelectCommand.Parameters.AddWithValue("@idusuario", idusuariovariable);
                da.Fill(dt);
                datalistado_movimientos_validar.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        private void validar_aperturas_de_caja()
        {
            ListarCierres_Caja();
            contarCierre_De_Caja();
            if (contadorCajas == 0)
            {
                Aperturar_detalle_cierre_caja();
                lblApertura_De_Caja = "Nuevo*****";
                Ingresar_ventas();

            }
            else
            {
                Mostrar_movimientos_de_caja_por_serial_y_usuario();
                Contar_movimientos_de_caja_por_usuario();

                if (contador_movimientos_validar_caja == 0)
                {
                    obtener_usuario_que_aperturo_caja();
                    MessageBox.Show("Continuaras Con El Turno de *" + lblNombreDelCajero.Text + "* Todos los registros se haran con este Usuario"
                     , "Caja Iniciada:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                    lblApertura_De_Caja = "Aperturado";
                    Ingresar_ventas();
            }
        }
        private void Ingresar_ventas()
        {

            if (lblApertura_De_Caja == "Nuevo*****")
            {
                Dispose();
                Caja.frm_Apertura_de_caja frmCaja = new Caja.frm_Apertura_de_caja();
                frmCaja.ShowDialog();
            }

            else if (lblApertura_De_Caja == "Aperturado")
            {
                Dispose();
                Ventas_Menu_Principal.frm_VENTAS_MENU_PRINCIPAL frmVentas = new Ventas_Menu_Principal.frm_VENTAS_MENU_PRINCIPAL();
                frmVentas.EstadoInicioAdmin = "SI";
                frmVentas.ShowDialog();
            }
        }
        private void obtener_usuario_que_aperturo_caja()
        {
            try
            {
                lblUsuario_queInicioCaja.Text = datalistado_detalle_cierre_caja.SelectedCells[1].Value.ToString();
                lblNombreDelCajero.Text = datalistado_detalle_cierre_caja.SelectedCells[2].Value.ToString();
            }
            catch
            {

            }
        }
        private void Contar_movimientos_de_caja_por_usuario()
        {
            int x;

            x = datalistado_movimientos_validar.Rows.Count;
            contador_movimientos_validar_caja = x;
        }
        private void btnConfigurar_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Panel_Configueraciones frm = new Panel_Configueraciones();
            //frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
            frm.Show();

        }
        private void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
            Dashboard_Principal frm = new Dashboard_Principal();
            frm.ShowDialog();
        }
        private void btnCopiasSeguridad_Click(object sender, EventArgs e)
        {
            CopiasBd.CrearCopiaBd frm = new CopiasBd.CrearCopiaBd();
            frm.ShowDialog();
        }
        private void btnRestaurarBd_Click(object sender, EventArgs e)
        {
            RestaurarBdExpress();
        }
        private void RestaurarBdExpress()
        {
            dlg.InitialDirectory = "";
            dlg.Filter = "Backup " + Base_De_datos + "|*.bak";
            dlg.FilterIndex = 2;
            dlg.Title = "Cargador de Backup";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ruta = Path.GetFullPath(dlg.FileName);
                DialogResult pregunta = MessageBox.Show("Usted está a punto de restaurar la base de datos," + " asegurese de que el archivo .bak sea reciente, de" + " lo contrario podría perder información y no podrá" + " recuperarla, ¿desea continuar?", "Restauración de base de datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (pregunta == DialogResult.Yes)
                {
                    SqlConnection cnn = new SqlConnection("Server=" + Servidor + ";database=master; integrated security=yes");
                    try
                    {
                        cnn.Open();
                        string Proceso = "EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'" + Base_De_datos + "' USE [master] ALTER DATABASE [" + Base_De_datos + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE DROP DATABASE [" + Base_De_datos + "] RESTORE DATABASE " + Base_De_datos + " FROM DISK = N'" + ruta + "' WITH FILE = 1, NOUNLOAD, REPLACE, STATS = 10";
                        SqlCommand BorraRestaura = new SqlCommand(Proceso, cnn);
                        BorraRestaura.ExecuteNonQuery();
                        MessageBox.Show("La base de datos ha sido restaurada satisfactoriamente! Vuelve a Iniciar El Sistema", "Restauración de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Dispose();
                        Application.ExitThread();

                    }
                    catch (Exception)
                    {
                        RestaurarNoExpress();
                    }
                    finally
                    {
                        if (cnn.State == ConnectionState.Open)
                        {
                            cnn.Close();
                        }

                    }


                }
            }
        }
        private void RestaurarNoExpress()
        {
            Servidor = ".";
            SqlConnection cnn = new SqlConnection("Server=" + Servidor + ";database=master; integrated security=yes");
            try
            {
                cnn.Open();
                string Proceso = "EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'" + Base_De_datos + "' USE [master] ALTER DATABASE [" + Base_De_datos + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE DROP DATABASE [" + Base_De_datos + "] RESTORE DATABASE " + Base_De_datos + " FROM DISK = N'" + ruta + "' WITH FILE = 1, NOUNLOAD, REPLACE, STATS = 10";
                SqlCommand BorraRestaura = new SqlCommand(Proceso, cnn);
                BorraRestaura.ExecuteNonQuery();
                MessageBox.Show("La base de datos ha sido restaurada satisfactoriamente! Vuelve a Iniciar El Sistema", "Restauración de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Dispose();
                Application.ExitThread();

            }
            catch (Exception)
            {

            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }

            }
        }

        private void btnLicencia_Click(object sender, EventArgs e)
        {
            Membrecias.Membresias_Nuevo frm = new Membrecias.Membresias_Nuevo();
            frm.ShowDialog();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void checkFiltros_CheckedChanged(object sender, EventArgs e)
        {
            if (checkFiltros.Checked == true)
            {
                PanelHoy.Visible = false;
                panelfiltros.Visible = true;
                mostrarVentasGraficaFechas();
            }
            else
            {
                mostrarVentasGrafica();
                PanelHoy.Visible = true;
                panelfiltros.Visible = false;
            }
        }

        private void txtFi_ValueChanged(object sender, EventArgs e)
        {
            mostrarVentasGraficaFechas();
        }

        private void txtff_ValueChanged(object sender, EventArgs e)
        {
            mostrarVentasGraficaFechas();
        }

        private void lblhastaHoy_Click(object sender, EventArgs e)
        {
            mostrarVentasGrafica();

        }

        private void cbGastosAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReporteGastosAni();
            ReporteGastosMesCombo();
        }
        private void ReporteGastosAni()
        {
            DataTable dt = new DataTable();
            año = Convert.ToInt32(cbGastosAno.Text);
            Obtener_datos.ReporteGastosAnio(ref dt, año);
            ArrayList monto = new ArrayList();
            ArrayList descripcion = new ArrayList();
            foreach (DataRow item in dt.Rows)
            {
                monto.Add(item["Monto"]);
                descripcion.Add(item["Descripcion"]);
            }
            charGastosAnio.Series[0].Points.DataBindXY( descripcion, monto);
        }
        private void ReporteGastosAnioMesGrafica()
        {
            DataTable dt = new DataTable();
            año = Convert.ToInt32(cbGastosAno.Text);
            Obtener_datos.ReporteGastosAnioMesGrafica(ref dt, año,cbMesGasto.Text);
            ArrayList monto = new ArrayList();
            ArrayList descripcion = new ArrayList();
            foreach (DataRow item in dt.Rows)
            {
                monto.Add(item["Monto"]);
                descripcion.Add(item["Descripcion"]);
            }
            charGastosMes.Series[0].Points.DataBindXY(descripcion, monto);
        }
        private void ReporteGastosMesCombo()
        {
            DataTable dt = new DataTable();
            año = Convert.ToInt32(cbGastosAno.Text);
            Obtener_datos.ReporteGastosMesCombo(ref dt, año);
            cbMesGasto.DisplayMember = "mes";
            cbMesGasto.ValueMember = "mes";
            cbMesGasto.DataSource = dt;
        }

        private void cbMesGasto_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReporteGastosAnioMesGrafica();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            Reportes.MenuReportes frm = new Reportes.MenuReportes();
            frm.ShowDialog();
        }

        private void btncomprar_Click(object sender, EventArgs e)
        {
            var frm = new Compras.Admincompras();
            frm.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (checkFiltros.Checked == true)
            {
                checkFiltros.Checked = false;

            }
            else if(checkFiltros.Checked == false)
            {
                checkFiltros.Checked = true;
            }
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            mostrarPanelInicio();
        }

        private void btnResumenVentas_Click(object sender, EventArgs e)
        {
            //Paneles 
            panelResumenVentas.Visible = true;
            panelResumenVentas.Dock = DockStyle.Fill;
            panelResumenVentas.BringToFront();
            panelResumenGastos.Visible = false;
            panelInicio.Visible = false;
            //Botones
            btnInicio.BackgroundImage = null;
            btnResumenGastos.BackgroundImage = null;
            btnResumenVentas.BackgroundImage = Properties.Resources.azul;

        }

        private void btnResumenGastos_Click(object sender, EventArgs e)
        {
            //Paneles 
            panelResumenGastos.Visible = true;
            panelResumenGastos.Dock = DockStyle.Fill;
            panelResumenGastos.BringToFront();
            panelResumenVentas.Visible = false;
            panelInicio.Visible = false;
            //Botones
            btnInicio.BackgroundImage = null;
            btnResumenVentas.BackgroundImage = null;
            btnResumenGastos.BackgroundImage = Properties.Resources.azul;

        }
    }
}
