using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlanVent_Sistema_De_Ventas.Logic;
using Dapper;

namespace AlanVent_Sistema_De_Ventas.DataAccess
{
    class Obtener_datos
    {
        private static string serialPc;
        private static int idcaja;
        public static bool ValidarCajaHabilitada()
        {
            bool estado = false;
            try
            {
                string serialPc = "";
                Bases.Obtener_serialPc(ref serialPc);
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("ValidarCajaHabilitada", ConexionMaestra.conectar);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@serialpc", serialPc);
                var EstadoHabilitada = cmd.ExecuteScalar().ToString();

                ConexionMaestra.cerrar();
                if (EstadoHabilitada == "SI")
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                }
            }
            catch (Exception ex)
            {

            }
            return estado;
        }
        public static bool ObtenerEstadoImprimirCierreCaja()
        {
            bool estado = false;
            try
            {
                
                DataAccess.ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("MostrarEstadoImprimirCierreCaja", ConexionMaestra.conectar);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var valueActivacion = cmd.ExecuteScalar().ToString();

                DataAccess.ConexionMaestra.cerrar();
                if (valueActivacion == "SI")
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                }
            }
            catch (Exception ex)
            {

            }
            return estado;
        }
        public static void Obtener_id_caja_PorSerial(ref int Idcaja)
        {
            
            try
            {
                Bases.Obtener_serialPc(ref serialPc);

                DataAccess.ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("Mostrar_Cajas_Por_Serial_De_DiscoDuro", ConexionMaestra.conectar);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@serial", serialPc);
                Idcaja = Convert.ToInt32(cmd.ExecuteScalar());

                DataAccess.ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
        public static void MostrarNombreLogoEmpresa(ref PictureBox icono, ref Label lblNombreEmpresa)
        {

            try
            {
                var dt = new DataTable();
                DataAccess.ConexionMaestra.abrir();

                SqlDataAdapter da = new SqlDataAdapter("MostrarNombreLogoEmpresa", DataAccess.ConexionMaestra.conectar);
                da.Fill(dt);

                DataAccess.ConexionMaestra.cerrar();

                foreach (DataRow item in dt.Rows)
                {
                    lblNombreEmpresa.Text = item["Nombre_Empresa"].ToString();
                    byte[] bytes = (byte[])item["Logo"];
                    var memory = new MemoryStream(bytes);
                    icono.Image = Image.FromStream(memory);
                }
            }
            catch (Exception ex)
            {

            }

        }
        public static void MostrarNotasFacturadiseno(ref string Notas)
        {

            try
            {
                var dt = new DataTable();
                DataAccess.ConexionMaestra.abrir();

                SqlDataAdapter da = new SqlDataAdapter("MostrarNotasFacturadiseno", DataAccess.ConexionMaestra.conectar);
                da.Fill(dt);

                DataAccess.ConexionMaestra.cerrar();

                foreach (DataRow item in dt.Rows)
                {
                    Notas = item["Notas"].ToString();

                }
            }
            catch (Exception ex)
            {

            }

        }
        public static void MostrarInformacionVisorPrecios(ref Label Descripcion, ref Label Precio, string codigo)
        {

            try
            {
                var dt = new DataTable();
                DataAccess.ConexionMaestra.abrir();

                SqlDataAdapter da = new SqlDataAdapter("MostrarInformacionVisorPrecios", DataAccess.ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@codigo", codigo);
                da.Fill(dt);

                DataAccess.ConexionMaestra.cerrar();

                foreach (DataRow item in dt.Rows)
                {
                    Descripcion.Text = item["Descripcion"].ToString();
                    Precio.Text = item["Precio"].ToString();
                }
                if(dt.Rows.Count == 0)
                {
                    Descripcion.Text = "PRODUCTO NO ENCONTRADO!";
                    Precio.Text = "RD$ 0.00";

                }
            }
            catch (Exception ex)
            {

            }

        }
        public static void Mostrar_TipoBusqueda(ref string TipoBusqueda)
        {
            ConexionMaestra.abrir();
            SqlCommand com = new SqlCommand("select Modo_de_busqueda from EMPRESA", ConexionMaestra.conectar);
            try
            {
                TipoBusqueda = Convert.ToString(com.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }

        }
        public static void Editar_eleccion_de_impresora(int idcaja, string Impresora)
        {
            try
            {
                DataAccess.ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("editar_eleccion_impresoras", DataAccess.ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Impresora_Ticket", Impresora);
                cmd.Parameters.AddWithValue("@idcaja", idcaja);
                cmd.ExecuteNonQuery();
                DataAccess.ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "- - - " + ex.Message);
            }
        }
        public static void mostrar_cajas(ref DataTable dt)
        {
            try
            {

                Bases.Obtener_serialPc(ref serialPc);
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("Mostrar_Cajas_Por_Serial_De_DiscoDuro", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Serial", serialPc);
                da.Fill(dt);
                ConexionMaestra.cerrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void mostrar_ventas_en_espera_con_fecha_y_monto(ref DataTable dt)
        {
           
            try
            {

                DataAccess.ConexionMaestra.abrir();

                SqlDataAdapter da = new SqlDataAdapter("mostrar_ventas_en_espera_con_fecha_y_monto", DataAccess.ConexionMaestra.conectar);
                da.Fill(dt);

                DataAccess.ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }

        }  
        public static void MostrarCierreCajaRealizados(ref DataTable dt, string busqueda)
        {
           
            try
            {

                DataAccess.ConexionMaestra.abrir();

                SqlDataAdapter da = new SqlDataAdapter("MostrarCierreCajaRealizados", DataAccess.ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@busqueda", busqueda);
                da.Fill(dt);

                DataAccess.ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }

        } 
        public static void ImprimirCierreCajaRealizado(ref DataTable dt,int idcierre)
        {
           
            try
            {

                DataAccess.ConexionMaestra.abrir();

                SqlDataAdapter da = new SqlDataAdapter("ImprimirCierreCajaRealizado", DataAccess.ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idcierre", idcierre);
                da.Fill(dt);

                DataAccess.ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }

        } 
        public static void MostrarProveedoresCombo(ref DataTable dt)
        {
           
            try
            {

                DataAccess.ConexionMaestra.abrir();

                SqlDataAdapter da = new SqlDataAdapter("MostrarProveedoresCombo", DataAccess.ConexionMaestra.conectar);
                da.Fill(dt);

                DataAccess.ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }

        }
        public static void mostrar_productos_agregados_a_ventas_en_espera(ref DataTable dt,ref int idventa)
        {
            try
            {

                DataAccess.ConexionMaestra.abrir();
                SqlDataAdapter dta = new SqlDataAdapter("mostrar_productos_agregados_a_ventas_en_espera", ConexionMaestra.conectar);
                dta.SelectCommand.CommandType = CommandType.StoredProcedure;
                dta.SelectCommand.Parameters.AddWithValue("@idventa", idventa);
                dta.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch ( Exception ex)
            {

            }

        }
        public static void Buscar_Conceptos(ref DataTable dt,string buscador)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscar_conceptos", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", buscador);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex )
            {

            }
            
        }
        public static void MostrarCorreoEmisor(ref string Correo)
        {
            try
            {
                ConexionMaestra.abrir();
                string r = "";
                var dt = new DataTable();
                var dta = new SqlDataAdapter("MostrarCorreoEmisor", ConexionMaestra.conectar);
                dta.SelectCommand.CommandType = CommandType.StoredProcedure;
                dta.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                   r = item["Rol"].ToString();
                }
                Correo = Bases.Desencriptar(r);

            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static void mostrar_gastos_por_turnos(ref DataTable dt, int idcaja,DateTime fi,DateTime ff)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_gastos_por_turnos", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idcaja", idcaja); 
                da.SelectCommand.Parameters.AddWithValue("@fi", fi);
                da.SelectCommand.Parameters.AddWithValue("@ff", ff);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
           
        }
        public static void mostrar_ingresos_por_turnos(ref DataTable dt, int idcaja, DateTime fi, DateTime ff)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_ingresos_por_turnos", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idcaja", idcaja);
                da.SelectCommand.Parameters.AddWithValue("@fi", fi);
                da.SelectCommand.Parameters.AddWithValue("@ff", ff);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }

        }
        public static void MostrarUltimoIdCierreCaja(ref int Idcierrecaja)
        {
            Obtener_id_caja_PorSerial(ref idcaja);
            try
            {

                ConexionMaestra.abrir();
                var da = new SqlCommand("MostrarUltimoIdCierreCaja", ConexionMaestra.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                Idcierrecaja = Convert.ToInt32(da.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }

        }
        public static void ObtenerInfoSerializacionCreditoFiscal(ref LSerializacionNCF models)
        {
            try
            {
                var dt = new DataTable();
                ConexionMaestra.abrir();
                var da = new SqlDataAdapter("MostrarSerializacionEgresos", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                ConexionMaestra.cerrar();

                foreach (DataRow item in dt.Rows)
                {
                    models.Serie = item["Serie"].ToString();
                    models.TipoComprobante = item["TipoComprobante"].ToString();
                    models.CantidadCeros = Convert.ToInt32(item["CantidadCeros"].ToString());
                    models.CantidadSinCeros = (long)Convert.ToUInt64(item["CantidadSinCeros"].ToString());
                }
            }
            catch (Exception ex)
            {

            }

        }
        public static void ObtenerInfoSerializacionGastosMenores(ref LSerializacionNCF models)
        {
            try
            {
                var dt = new DataTable();
                ConexionMaestra.abrir();
                var da = new SqlDataAdapter("MostrarSerializacionGastosMenores", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                ConexionMaestra.cerrar();

                foreach (DataRow item in dt.Rows)
                {
                    models.Serie = item["Serie"].ToString();
                    models.TipoComprobante = item["TipoComprobante"].ToString();
                    models.CantidadCeros = Convert.ToInt32(item["CantidadCeros"].ToString());
                    models.CantidadSinCeros = (long)Convert.ToUInt64(item["CantidadSinCeros"].ToString());
                }
            }
            catch (Exception ex)
            {

            }

        } 
        public static void ObtenerInfoSerializacionProvInformal(ref LSerializacionNCF models)
        {
            try
            {
                var dt = new DataTable();
                ConexionMaestra.abrir();
                var da = new SqlDataAdapter("MostrarSerializacionProvInformal", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                ConexionMaestra.cerrar();

                foreach (DataRow item in dt.Rows)
                {
                    models.Serie = item["Serie"].ToString();
                    models.TipoComprobante = item["TipoComprobante"].ToString();
                    models.CantidadCeros = Convert.ToInt32(item["CantidadCeros"].ToString());
                    models.CantidadSinCeros = (long)Convert.ToUInt64(item["CantidadSinCeros"].ToString());
                }
            }
            catch (Exception ex)
            {

            }

        }
        public static void ObtenerInfoSerializacionFacturaConsumo(ref LSerializacionNCF models)
        {
            try
            {
                var dt = new DataTable();
                ConexionMaestra.abrir();
                var da = new SqlDataAdapter("MostrarSerializacionIngresos", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                ConexionMaestra.cerrar();

                foreach (DataRow item in dt.Rows)
                {
                    models.Serie = item["Serie"].ToString();
                    models.TipoComprobante = item["TipoComprobante"].ToString();
                    models.CantidadCeros = Convert.ToInt32(item["CantidadCeros"].ToString());
                    models.CantidadSinCeros = (long)Convert.ToUInt64(item["CantidadSinCeros"].ToString());
                }
            }
            catch (Exception ex)
            {

            }

        } 
        public static void MostrarCodigosBarrasImprimir(ref DataTable dtReporte,int Idsolicitud)
        {
            try
            {
                var dt = new DataTable();
                ConexionMaestra.abrir();
                var da = new SqlDataAdapter("MostrarCodigosBarrasImprimir", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idsolicitud", Idsolicitud);
                da.Fill(dt);
                ConexionMaestra.cerrar();

                //Configuracion del datatable reporte
                dtReporte.Columns.Add("Codigo");
                dtReporte.Columns.Add("Descripcion");
                dtReporte.Columns.Add("Precio");
                dtReporte.Columns.Add("Empresa");
                foreach (DataRow item in dt.Rows)
                {
                    var cantidad = Convert.ToInt32(item["cantidad"].ToString());
                    DataRow Newrow;
                    for (int i = 0; i < cantidad; i++)
                    {
                        Newrow = dtReporte.NewRow();
                        Newrow["Codigo"]= item["Codigo"].ToString();
                        var descripcionSinEditar = item["Descripcion"].ToString();
                        Newrow["Precio"] = AsignarComa(Convert.ToDouble(item["Precio_de_venta"].ToString()));
                        Newrow["Empresa"] = item["Nombre_Empresa"].ToString();
                        string descripcionProcesada = "";
                        //Procesar cantidad de cararcteres
                        if (descripcionSinEditar.Length > 45)
                        {
                            
                                descripcionProcesada += descripcionSinEditar.Substring(0,45);
                            
                        }
                        else
                        {
                            descripcionProcesada = descripcionSinEditar;
                        }

                        Newrow["Descripcion"] = descripcionProcesada;
                        dtReporte.Rows.Add(Newrow);
                    }
                   
                }
            }
            catch (Exception ex)
            {

            }

        }
        private static string AsignarComa(double valor)
        {
            return String.Format("{0:#,##0.##}", valor);
        }
        public static void mostrar_cierre_de_caja_pendiente(ref DataTable dt)
        {
            Obtener_id_caja_PorSerial(ref idcaja);
            try
            {
                
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_cierre_de_caja_pendiente", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idcaja", idcaja);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }

        }
        public static void ImprimirCierreCaja(ref DataTable dt)
        {
            Obtener_id_caja_PorSerial(ref idcaja);
            try
            {

                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ImprimirCierreCaja", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idcaja", idcaja);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }

        }
        public static void MostrarSumaCobrosTarjeta(ref double cobrosefectivo,ref  double cobrostarjeta)
        {
            Obtener_id_caja_PorSerial(ref idcaja);
            var dt = new DataTable(); 
            try
            {

                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("MostrarSumaCobrosTarjeta", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idcaja", idcaja);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
            foreach (DataRow item in dt.Rows)
            {
                cobrosefectivo = Convert.ToDouble(item["CobrosEfectivo"].ToString());
                cobrostarjeta = Convert.ToDouble(item["CobrosTarjeta"].ToString());
            }
        }
        public static double MostrarGananciasEnVentas()
        {
            Obtener_id_caja_PorSerial(ref idcaja);
            double SumGanacias = 0;
            try
            {

                ConexionMaestra.abrir();
                var da = new SqlCommand("MostrarGanaciasVentas", ConexionMaestra.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                da.Parameters.AddWithValue("@fechafin", DateTime.Now);
                SumGanacias = Convert.ToDouble(da.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                SumGanacias = 0;
            }
            return SumGanacias;
        }
        public static double SumarPagosPorCuadreEFECTIVO()
        {
            Obtener_id_caja_PorSerial(ref idcaja);
            double SumPagos = 0;
            try
            {

                ConexionMaestra.abrir();
                var da = new SqlCommand("SumarPagosPorCuadre", ConexionMaestra.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                SumPagos = Convert.ToDouble(da.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                SumPagos = 0;
            }
            return SumPagos;
        }
        public static double SumarPagosPorCuadreTARJETA()
        {
            Obtener_id_caja_PorSerial(ref idcaja);
            double SumPagos = 0;
            try
            {

                ConexionMaestra.abrir();
                var da = new SqlCommand("SumarPagosPorCuadreTarjeta", ConexionMaestra.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                SumPagos = Convert.ToDouble(da.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                SumPagos = 0;
            }
            return SumPagos;
        }
        public static double MostrarImpuestosRecaudadosCierreCaja()
        {
            Obtener_id_caja_PorSerial(ref idcaja);
            double SumGanacias = 0;
            try
            {

                ConexionMaestra.abrir();
                var da = new SqlCommand("MostrarImpuestosRecaudados", ConexionMaestra.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                da.Parameters.AddWithValue("@fechafin", DateTime.Now);
                SumGanacias = Convert.ToDouble(da.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                SumGanacias = 0;
            }
            return SumGanacias;
        }
        public static string MostrarDescuentoPorVenta(int idventa)
        {
            string descuento = "";
            try
            {

                ConexionMaestra.abrir();
                var da = new SqlCommand("MostrarDescuentoPorVentas", ConexionMaestra.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idventa", idventa);
                descuento = da.ExecuteScalar().ToString();
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                descuento = "";
            }
            return descuento;
        } 
        //MOSTRAR SERIALIZACION DE FORMULARIO
        public static string MOSTRAR_SERIALIZACION_NCF_01()
        {
            string Cantidad = "";
            try
            {

                ConexionMaestra.abrir();
                var da = new SqlCommand("MOSTRAR_SERIALIZACION_NCF_01", ConexionMaestra.conectar);
                da.CommandType = CommandType.StoredProcedure;
                Cantidad = da.ExecuteScalar().ToString();
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                Cantidad = "";
            }
            return Cantidad;
        }
        public static string MOSTRAR_SERIALIZACION_NCF_02()
        {
            string Cantidad = "";
            try
            {

                ConexionMaestra.abrir();
                var da = new SqlCommand("MOSTRAR_SERIALIZACION_NCF_02", ConexionMaestra.conectar);
                da.CommandType = CommandType.StoredProcedure;
                Cantidad = da.ExecuteScalar().ToString();
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                Cantidad = "";
            }
            return Cantidad;
        }
        public static string MOSTRAR_SERIALIZACION_NCF_11()
        {
            string Cantidad = "";
            try
            {

                ConexionMaestra.abrir();
                var da = new SqlCommand("MOSTRAR_SERIALIZACION_NCF_11", ConexionMaestra.conectar);
                da.CommandType = CommandType.StoredProcedure;
                Cantidad = da.ExecuteScalar().ToString();
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                Cantidad = "";
            }
            return Cantidad;
        }
        public static string MOSTRAR_SERIALIZACION_NCF_13()
        {
            string Cantidad = "";
            try
            {

                ConexionMaestra.abrir();
                var da = new SqlCommand("MOSTRAR_SERIALIZACION_NCF_13", ConexionMaestra.conectar);
                da.CommandType = CommandType.StoredProcedure;
                Cantidad = da.ExecuteScalar().ToString();
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                Cantidad = "";
            }
            return Cantidad;
        }
        //--------------------

        public static string MostrarCorreoPorUsuario(string nameuser)
        { 
            string Correo="";
            try
            {

                ConexionMaestra.abrir();
                var da = new SqlCommand("MostrarCorreoPorUsuario", ConexionMaestra.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@nameuser", nameuser);
                Correo = da.ExecuteScalar().ToString();
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                Correo = "";
            }
            return Correo;
        }
        public static string ObtenerRolPorUsuario()
        { 
            string Rol="";
            string serialpc = "";
            Bases.Obtener_serialPc(ref serialpc);
            try
            {

                ConexionMaestra.abrir();
                var da = new SqlCommand("ObtenerRolUsuario", ConexionMaestra.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@serialpc", serialpc);
                Rol = da.ExecuteScalar().ToString();

                ConexionMaestra.cerrar();
                
            }
            catch (Exception ex)
            {
                Rol = "";
            }
            return Rol;
        }
        public static void mostrarUsuariosSesion(ref DataTable dt)
        {
            Bases.Obtener_serialPc(ref serialPc);
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_inicio_De_sesion", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@id_serial", serialPc);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        public static void mostrarUsuarios(ref DataTable dt)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Usuarios", ConexionMaestra.conectar);
                da.Fill(dt);


            }
            catch (Exception ex)
            {

            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static void mostrarNombreUsuarioPorID(ref string Name, int IdUsurio)
        {
            try
            {
                ConexionMaestra.abrir();
                var da = new SqlCommand("select NombreYApellido from Usuarios where ID = "+IdUsurio, ConexionMaestra.conectar);
                Name = da.ExecuteScalar().ToString();


            }
            catch (Exception ex)
            {

            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static void MostrarFacturaImpresa(ref DataTable dt, int idventa,string totalletras)
        {
            try
            {
                DataAccess.ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_factura_impresa", DataAccess.ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id_venta", idventa);
                da.SelectCommand.Parameters.AddWithValue("@total_en_letras", totalletras);
                da.Fill(dt);
               
                DataAccess.ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "Message: " + ex.Message);
            }
        }
        public static void mostrar_inicio_De_sesion( ref int IdUsuario)
        {
            Bases.Obtener_serialPc(ref serialPc);
            try
            {
                ConexionMaestra.abrir();
                var cmd = new SqlCommand("mostrar_inicio_De_sesion", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_serial", serialPc);
                IdUsuario = Convert.ToInt32(cmd.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        public static void MostrarIdUsuarioPorCAJAAPERTURADA(ref int IdUsuario)
        {
            try
            {
                string serial = "";
                Bases.Obtener_serialPc(ref serial);
                ConexionMaestra.abrir();
                var cmd = new SqlCommand("MostrarIdUsuarioPorCAJAAPERTURADA", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@serialpc", serial);
                IdUsuario = Convert.ToInt32(cmd.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
        public static void MostrarVerificarSiElUsuarioAdministrador(ref string Estado)
        {
            int IdUser = 0;
            string Rol = "";
            MostrarIdUsuarioPorCAJAAPERTURADA(ref IdUser);
            try
            {
                ConexionMaestra.abrir();
                var cmd = new SqlCommand("VerificarRolUsuario", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", IdUser);
                Rol = Convert.ToString(cmd.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception ex )
            {
            }
            if (Rol == "Administrador (Control total)")
                Estado = "SI";
            else
                Estado = "NO";
        }
        public static void Mostrar_ventas_efectivo_turno(ref double monto, int idcaja, DateTime fi, DateTime ff)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand da = new SqlCommand("Mostrar_ventas_efectivo_turno", ConexionMaestra.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                da.Parameters.AddWithValue("@fi", fi);
                da.Parameters.AddWithValue("@ff", ff);
                monto = Convert.ToDouble(da.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
              
                monto = 0;
            }

        }
        public static void sumar_gastos_por_turno(ref double monto, int idcaja, DateTime fi, DateTime ff)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand da = new SqlCommand("sumar_gastos_por_turno", ConexionMaestra.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                da.Parameters.AddWithValue("@fi", fi);
                da.Parameters.AddWithValue("@ff", ff);
                monto = Convert.ToDouble(da.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

                monto = 0;
            }

        }
        public static void sumar_ingresos_por_turno(ref double monto, int idcaja, DateTime fi, DateTime ff)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand da = new SqlCommand("sumar_ingresos_por_turno", ConexionMaestra.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                da.Parameters.AddWithValue("@fi", fi);
                da.Parameters.AddWithValue("@ff", ff);
                monto = Convert.ToDouble(da.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

                monto = 0;
            }

        }
        public static void M_ventas_tarjeta_turno(int idcaja, DateTime fi,DateTime ff,ref double monto)
        {
            try
            {
                DataAccess.ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("M_ventas_tarjeta_turno", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcaja", idcaja);
                cmd.Parameters.AddWithValue("@fi", fi);
                cmd.Parameters.AddWithValue("@ff", ff);
                monto = Convert.ToDouble(cmd.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception)
            {
                monto = 0;
            }
        }
        public static void M_ventas_credito_turno(int idcaja, DateTime fi, DateTime ff, ref double monto)
        {
            try
            {
                DataAccess.ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("M_ventas_credito_turno", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcaja", idcaja);
                cmd.Parameters.AddWithValue("@fi", fi);
                cmd.Parameters.AddWithValue("@ff", ff);
                monto = Convert.ToDouble(cmd.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception)
            {
                monto = 0;
            }
        }
        public static void mostrar_proveedores(ref DataTable dt)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_Proveedores", ConexionMaestra.conectar);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        public static void MostrarCreditoPorPagar(ref DataTable dt, int Idproveedor)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("MostrarCreditoPorPagar", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idproveedor", Idproveedor);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void Mostrar606Gastos(ref DataTable dt, DateTime fi, DateTime ff)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("Mostrar606Gastos", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fi", fi.ToString("dd/MM/yyyy"));
                da.SelectCommand.Parameters.AddWithValue("@ff", ff.ToString("dd/MM/yyyy"));
                da.Fill(dt);
                
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void Mostrar607(ref DataTable dt, DateTime fi, DateTime ff)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("Mostrar607", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fi", fi.ToString("dd/MM/yyyy"));
                da.SelectCommand.Parameters.AddWithValue("@ff", ff.ToString("dd/MM/yyyy"));
                da.Fill(dt);
                
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void Mostrar608(ref DataTable dt, DateTime fi, DateTime ff)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("Mostrar608", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fi", fi.ToString("dd/MM/yyyy"));
                da.SelectCommand.Parameters.AddWithValue("@ff", ff.ToString("dd/MM/yyyy"));
                da.Fill(dt);
                
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void Mostrar606Compras(ref DataTable dt, DateTime fi, DateTime ff)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("Mostrar606Compras", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fi", fi.ToString("dd/MM/yyyy"));
                da.SelectCommand.Parameters.AddWithValue("@ff", ff.ToString("dd/MM/yyyy"));
                da.Fill(dt);
                
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void buscar_proveedores(ref DataTable dt, string letra)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscar_proveedores", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", letra);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void buscar_proveedoresSOLOACTIVOS(ref DataTable dt, string letra)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscar_proveedoresSOLOACTIVOS", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", letra);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void MostrarFacturaPagos(ref DataTable dt, int Idrecibo)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("MostrarFacturaPagos", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@norecibo", Idrecibo);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void sumar_CreditoPorPagar(int idcaja, DateTime fi, DateTime ff, ref double monto)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand da = new SqlCommand("sumar_CreditoPorPagar", ConexionMaestra.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                da.Parameters.AddWithValue("@fi", fi);
                da.Parameters.AddWithValue("@ff", ff);
                monto = Convert.ToDouble(da.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception)
            {

                monto = 0;
            }
        }
        public static void sumar_creditoPorCobrar(int idcaja, DateTime fi, DateTime ff, ref double monto)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand da = new SqlCommand("sumar_creditoPorCobrar", ConexionMaestra.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                da.Parameters.AddWithValue("@fi", fi);
                da.Parameters.AddWithValue("@ff", ff);
                monto = Convert.ToDouble(da.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception)
            {

                monto = 0;
            }
        }
        public static void MostrarIdProveedorGenerico(ref int id)
        {
            try
            {
                var con = "select top 1 IdProveedor from Proveedores where Nombre = 'GENERICO'";
                ConexionMaestra.abrir();
                var cmd = new SqlCommand(con, ConexionMaestra.conectar);
                id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                id = 0;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static void mostrar_cliente(ref DataTable dt)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_cliente", ConexionMaestra.conectar);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void mostrar_EMPRESA(ref DataTable dt)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("select * from EMPRESA", ConexionMaestra.conectar);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void Mostrar_impresora_Predeterminada(ref ComboBox Listado)
        {

            try
            {
                Bases.Obtener_serialPc(ref serialPc);
                var cmd = new SqlCommand("mostrar_impresoras_por_caja", DataAccess.ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Serial", serialPc);
                try
                {
                    DataAccess.ConexionMaestra.abrir();
                    Listado.Text = Convert.ToString(cmd.ExecuteScalar());
                    DataAccess.ConexionMaestra.cerrar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }

        }
        public static void buscar_clientes(ref DataTable dt, string letra)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscar_clientes", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", letra);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Codigos de baras para imprimir 
        public static void MostrarDetalleCodigoBarras(ref DataTable dt,int idsolicitud)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("MostrarDetalleCodigoBarras", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idsolicitud", idsolicitud);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void mostrarCorreoBase(ref DataTable dt)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("Select * from CorreoBase", ConexionMaestra.conectar);
                da.Fill(dt);
                ConexionMaestra.cerrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void BuscarProductosCodigoBarras(ref DataTable dt,string buscando)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("BuscarProductosCodigoBarras", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", buscando);
                da.Fill(dt);
                ConexionMaestra.cerrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void mostrarEstadosCuentaCliente(ref DataTable dt, int idcliente)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarEstadosCuentaCliente", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idcliente", idcliente);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void mostrar_ControlCobros(ref DataTable dt, int idCliente)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_ControlCobros", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@id", idCliente);
                da.Fill(dt);

                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void ReportePorCobrar(ref double monto )
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand da = new SqlCommand("ReportePorCobrar", ConexionMaestra.conectar);
                monto = Convert.ToDouble(da.ExecuteScalar());

                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                monto = 0;
            }
        }
        public static void mostrarVentasGrafica(ref DataTable dt)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarVentasGrafica", ConexionMaestra.conectar);
                da.Fill(dt);
                ConexionMaestra.cerrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void mostrarVentasGraficaFechas(ref DataTable dt,DateTime fi,DateTime ff)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarVentasGraficaFechas", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fi",fi);
                da.SelectCommand.Parameters.AddWithValue("@ff",ff);
                da.Fill(dt);
                ConexionMaestra.cerrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void ReporteTotalVentas(ref double monto)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand da = new SqlCommand("ReporteTotalVentas", ConexionMaestra.conectar);
                monto = Convert.ToDouble(da.ExecuteScalar());

                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                monto = 0;
            }
        }
        public static void ReportePorPagar(ref double monto)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand da = new SqlCommand("ReportePorPagar", ConexionMaestra.conectar);
                monto = Convert.ToDouble(da.ExecuteScalar());

                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                monto = 0;
            }
        }
        public static bool IdentificarProveedorInformal(int idproveedor)
        {
            bool estado = false;
            try
            {
                ConexionMaestra.abrir();
                var sqlCommand = new SqlCommand("Mostrar_IdproveedorInformal", ConexionMaestra.conectar);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idproveedor", idproveedor);
                var Getvalue = sqlCommand.ExecuteScalar().ToString();
                ConexionMaestra.cerrar();

                if (Getvalue == "SI")
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                }
            }
            catch (Exception ex )
            {

            }
            return estado;
        }
        public static void ReporteGanacias(ref double monto)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand da = new SqlCommand("ReporteGanacias", ConexionMaestra.conectar);
                monto = Convert.ToDouble(da.ExecuteScalar());

                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                monto = 0;
            }
        }
        public static void ReporteCantClientes(ref int Cant)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand da = new SqlCommand("select count(idclientev) from clientes where Estado <> '0'", ConexionMaestra.conectar);
                Cant = Convert.ToInt32(da.ExecuteScalar());

                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                Cant = 0;
            }
        }
        public static void ReporteCantProductos(ref int Cant)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand da = new SqlCommand("select sum(CONVERT(numeric(18,2),Stock)) from Productos1 where Usa_Inventarios = 'SI'", ConexionMaestra.conectar);
                Cant = Convert.ToInt32(da.ExecuteScalar());

                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                Cant = 0;
            }
        }
        public static void ReporteProductoBajoMinimo(ref int monto)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand da = new SqlCommand("ReporteProductoBajoMinimo", ConexionMaestra.conectar);
                monto = Convert.ToInt32(da.ExecuteScalar());

                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                monto = 0;
            }
        }
        public static void ReporteGastos(ref DataTable dt)
        {
            try
            {
                ConexionMaestra.abrir();
                var sql = new SqlDataAdapter("ReporteGastos", ConexionMaestra.conectar);
                sql.Fill(dt);
                ConexionMaestra.cerrar();

            }
            catch (Exception ex)
            {

            }
        }
        public static void ReporteGastosFecha(ref DataTable dt, DateTime fi , DateTime ff)
        {
            try
            {
                ConexionMaestra.abrir();
                var sql = new SqlDataAdapter("ReporteGastosFecha", ConexionMaestra.conectar);
                sql.SelectCommand.CommandType = CommandType.StoredProcedure;
                sql.SelectCommand.Parameters.AddWithValue("@fi", fi.ToString("dd/MM/yyyy"));
                sql.SelectCommand.Parameters.AddWithValue("@ff", ff.ToString("dd/MM/yyyy"));
                sql.Fill(dt);
                ConexionMaestra.cerrar();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void ReporteGananciasFechas(ref double monto, DateTime fi, DateTime ff)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand da = new SqlCommand("ReporteGananciasFechas", ConexionMaestra.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@fi", fi);
                da.Parameters.AddWithValue("@ff", ff);
                monto = Convert.ToDouble(da.ExecuteScalar());

                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                monto = 0;
            }
        }
        public static void ReporteTotalVentasFechas(ref double  monto,DateTime fi,DateTime ff)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand da = new SqlCommand("ReporteTotalVentasFechas", ConexionMaestra.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@fi", fi);
                da.Parameters.AddWithValue("@ff", ff);
                monto = Convert.ToDouble(da.ExecuteScalar());

                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                monto = 0;
            }
        }
        //PRODUCTOS
        public static void MostrarCodigoProductoAlGenerar(ref string Codigo)
        {
            try
            {
                ConexionMaestra.abrir();
                var da = new SqlCommand("MostrarCodigoProductoAlGenerar", ConexionMaestra.conectar);
                da.CommandType = CommandType.StoredProcedure;
                Codigo = da.ExecuteScalar().ToString();


            }
            catch (Exception ex)
            {
                Codigo = "";
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }

        public static void MostrarProductosBajoMinimo(ref DataTable dtInventarios)
        {
            try
            {
                ConexionMaestra.abrir();
                var da = new SqlDataAdapter("Mostrar_Inventarios_bajo_minimo", ConexionMaestra.conectar);
                da.Fill(dtInventarios);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
               
            }
        }
        //EMPRESA
        public static string MostrarRncEmpresa()
        {
            string rnc = "";
            try
            {
                ConexionMaestra.abrir();
                SqlCommand da = new SqlCommand("MostrarRncEmpresa", ConexionMaestra.conectar);
                rnc = da.ExecuteScalar().ToString();
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                rnc = "";
            }
            return rnc;
        }
        public static void MostrarMoneda(ref string moneda)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand da = new SqlCommand("SELECT Moneda FROM EMPRESA", ConexionMaestra.conectar);
                moneda = Convert.ToString(da.ExecuteScalar());

                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                moneda = "$";
            }
        }
        public static void ObtenerEstadoDeCopias(ref string estado)
        {
            try
            {
                ConexionMaestra.abrir();
                var cmd = new SqlCommand("SELECT Ultima_Fecha_copia_seguridad FROM EMPRESA", ConexionMaestra.conectar);
                estado = cmd.ExecuteScalar().ToString(); 
            }
            catch (Exception ex )
            {
                
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static void ObtenerEstadoImpuestos(ref string estado )
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("select Trabajas_Con_Impuestos FROM EMPRESA",ConexionMaestra.conectar);
                estado = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static void MostrarDatosImpuestos(ref DataTable dt)
        {
            try
            {
                ConexionMaestra.abrir();
                var cmd = new SqlDataAdapter("MostrarDatosImpuestosEmpresa", ConexionMaestra.conectar);
                cmd.Fill(dt);
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static void mostrarPmasvendidos(ref DataTable dt)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarPmasvendidos", ConexionMaestra.conectar);
                da.Fill(dt);

                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
               
            }
        }
        public static void ReporteGastosAnioCombo(ref DataTable dt)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteGastosAnioCombo", ConexionMaestra.conectar);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
        public static void ReporteGastosAnio(ref DataTable dt, int ano)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteGastosAnio", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@anio", ano);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
        public static void ReporteGastosMesCombo(ref DataTable dt, int ano)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteGastosMesCombo", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@anio", ano);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
        public static void ReporteGastosAnioMesGrafica(ref DataTable dt, int ano,string mes)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteGastosAnioMesGrafica", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@anio", ano);
                da.SelectCommand.Parameters.AddWithValue("@mes", mes);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
        public static void MostrarNombreEmpresa(ref DataTable dt )
        {
            try
            {
                ConexionMaestra.abrir();
                var cmd = new SqlDataAdapter("select Nombre_Empresa from EMPRESA", ConexionMaestra.conectar);
                cmd.Fill(dt); 
            }
            catch (Exception ex)
            {
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static string MostrarNombreEmpresaVariable()
        {
            string Name = "";
            try
            {
                ConexionMaestra.abrir();
                var cmd = new SqlCommand("select Nombre_Empresa from EMPRESA", ConexionMaestra.conectar);
                Name =   cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
            return Name;
        }
        //Caja
        public static void MostrarEstadoCorreo(ref string resul)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("SELECT Correo_para_envio_de_reportes FROM EMPRESA", ConexionMaestra.conectar);
                resul=  cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static void mostrarPuertos(ref DataTable dt)
        {
            try
            {
                Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarPuertos", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idcaja", idcaja);
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static void mostrarTemaCaja(ref string Tema)
        {
            try
            {
                Obtener_id_caja_PorSerial(ref idcaja);
                ConexionMaestra.abrir();
                SqlCommand da = new SqlCommand("mostrarTemaCaja", ConexionMaestra.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                Tema = da.ExecuteScalar().ToString();
                ConexionMaestra.cerrar();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        //Ventas 
        public static void buscarVentas(ref DataTable dt, string buscador)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("BuscarVentas", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@busqueda", buscador);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void BuscarVentasCodigoBarra(ref DataTable dt, string buscador)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscarVentasCodigoBarra", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@busqueda", buscador);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        
        public static void buscarVentasPorFechas(ref DataTable dt, DateTime fi, DateTime ff)
        {
            try
            {
               

                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscarVentasPorFechas", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fi", fi);
                da.SelectCommand.Parameters.AddWithValue("@ff", ff);

                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        public static void contarVentasEspera(ref int Contador)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand da = new SqlCommand("contarVentasEspera", ConexionMaestra.conectar);
                Contador = Convert.ToInt32(da.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception)
            {
                Contador = 0;


            }
        }
        public static void ReporteResumenVentasHoy(ref DataTable dt)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteResumenVentasHoy", ConexionMaestra.conectar);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void ReporteResumenVentasHoyEmpleado(ref DataTable dt, int idEmpleado)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteResumenVentasHoyEmpleado", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idEmpleado", idEmpleado);

                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        
        public static void ReporteResumenVentasEmpleadoFechas(ref DataTable dt, int idEmpleado, DateTime fi, DateTime ff)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteResumenVentasEmpleadoFechas", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                da.SelectCommand.Parameters.AddWithValue("@fi", fi);
                da.SelectCommand.Parameters.AddWithValue("@ff", ff);

                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        //Detalle Venta 
        public static void AplicarDescuento(int Idproducto, int IddetalleVenta)
        {
            try
            {
                ConexionMaestra.abrir();
                var sqlCommand = new SqlCommand("AplicarDescuento", ConexionMaestra.conectar);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id_producto", Idproducto);
                sqlCommand.Parameters.AddWithValue("@Id_venta", IddetalleVenta);
                sqlCommand.ExecuteNonQuery();
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
        public static void MostrarDetalleVenta(ref DataTable dt, int idventa)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_productos_agregados_a_venta", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idventa", idventa);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        // ----------Ticket -------------
        private static double MostrarPorcentajeImpuestoXVenta(int IdVenta)
        {
            double r = 0;
            try
            {
                ConexionMaestra.abrir();
                var Sql = new SqlCommand("MostrarPorcentajeImpuestoPorVenta", ConexionMaestra.conectar);
                Sql.CommandType = CommandType.StoredProcedure;
                Sql.Parameters.AddWithValue("@IdVenta", IdVenta);
                r =  Convert.ToDouble(Sql.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return r;
        }
        public static void mostrar_ticket_impreso(ref DataTable dt, int idventa, string TotalLetras)
        {
            try
            {
                double Porcentaje = MostrarPorcentajeImpuestoXVenta(idventa);
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_ticket_impreso", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id_venta", idventa);
                da.SelectCommand.Parameters.AddWithValue("@total_en_letras", TotalLetras);
                da.SelectCommand.Parameters.AddWithValue("@porcentaje", Porcentaje);
                da.Fill(dt);
                ConexionMaestra.cerrar();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        //Reportes 
        public static void MostrarReporteKARDEXSalidas( ref DataTable dtSALIDAS, int IdProducto)
        {
            try
            {
                ConexionMaestra.abrir();
                var sqlC = new SqlDataAdapter();
                //salidas 
                sqlC = new SqlDataAdapter("MOSTRAR_MOVIMIENTOS_DE_KARDEX_SALIDAS",ConexionMaestra.conectar);
                sqlC.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlC.SelectCommand.Parameters.AddWithValue("@idProducto", IdProducto);
                sqlC.Fill(dtSALIDAS);
                ConexionMaestra.cerrar();
               
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
               
            }
        }
        public static void MostrarReporteKARDEXEntradas(ref DataTable dtEntradas, int IdProducto)
        {
            try
            {
                ConexionMaestra.abrir();
                //ENTRADAS 
                var sqlC = new SqlDataAdapter("MOSTRAR_MOVIMIENTOS_DE_KARDEX_ENTRADAS", ConexionMaestra.conectar);
                sqlC.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlC.SelectCommand.Parameters.AddWithValue("@idProducto", IdProducto);
                sqlC.Fill(dtEntradas);
                ConexionMaestra.cerrar();

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
        public static void MOSTRAR_Inventarios_bajo_minimo(ref DataTable dt,ref string NombreEmpresa)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("Mostrar_Inventarios_bajo_minimo", ConexionMaestra.conectar);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);

            }
            try
            {
                ConexionMaestra.abrir();
                SqlCommand da = new SqlCommand("select EMPRESA.Nombre_Empresa from EMPRESA", ConexionMaestra.conectar);
                NombreEmpresa = Convert.ToString( da.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
        public static void mostrar_productos_vencidos(ref DataTable dt)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_productos_vencidos", ConexionMaestra.conectar);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);

            }
        }
        public static void imprimir_inventarios_todos(ref DataTable dt)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("imprimir_inventarios_todos", ConexionMaestra.conectar);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }
        public static void ReporteIngresos(ref DataTable dt)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteIngresos", ConexionMaestra.conectar);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void ReporteIngresosFecha(ref DataTable dt,DateTime fi,DateTime ff)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteIngresosFecha", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fi", fi.ToString("dd/MM/yyyy"));
                da.SelectCommand.Parameters.AddWithValue("@ff", ff.ToString("dd/MM/yyyy"));
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void ReporteResumenVentasFechas(ref DataTable dt, DateTime fi, DateTime ff)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteResumenVentasFechas", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fi", fi);
                da.SelectCommand.Parameters.AddWithValue("@ff", ff);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        //------REPORTE DE VENTAS 
        private static void MostrarFechasDeVentas(ref DataTable dt)
        {
            try
            {
                ConexionMaestra.abrir();
                var consul = "select DISTINCT CONVERT(date,fecha_venta) AS Fecha FROM Ventas";
                var da = new SqlDataAdapter(consul, ConexionMaestra.conectar);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static void MostrarFechasDeVentasConsultadas(ref DataTable dt, string fi, string ff)
        {
            try
            {
                ConexionMaestra.abrir();
                var consul = "select DISTINCT CONVERT(date,fecha_venta) AS Fecha FROM Ventas " +
                              "where CONVERT(date, fecha_venta) >= '" + fi+"' and CONVERT(date, fecha_venta) <= '"+ff+"'";
                var da = new SqlDataAdapter(consul, ConexionMaestra.conectar);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
            }
        }
        private static void MostrarFechasDeVentasEmpleado(ref DataTable dt, int IdEmpleado)
        {
            try
            {
                ConexionMaestra.abrir();
                var consul = "select DISTINCT CONVERT(date,fecha_venta) AS Fecha FROM Ventas " +
                              "where Id_usuario = "+ IdEmpleado;
                var da = new SqlDataAdapter(consul, ConexionMaestra.conectar);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
            }
        }
        private static void MostrarFechasDeVentasEmpleadoYFechas
            (ref DataTable dt, int IdEmpleado, string fi, string ff)
        {
            try
            {
                ConexionMaestra.abrir();
                var consul = "select DISTINCT CONVERT(date,fecha_venta) AS Fecha FROM Ventas " +
                              "where Id_usuario = " + IdEmpleado + " and CONVERT(date,fecha_venta) >=  '" + fi + "' and CONVERT(date,fecha_venta) <=  '" + ff+"'";
                var da = new SqlDataAdapter(consul, ConexionMaestra.conectar);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
            }
        }
        public static void BuscarVentasTodaReporte(ref DataTable lst)
        {
            try
            {
                ConexionMaestra.abrir();
                var sql = new SqlDataAdapter("MostrarReporteVentas", ConexionMaestra.conectar) ;
                sql.SelectCommand.CommandType = CommandType.StoredProcedure;
                sql.Fill(lst);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex )
            {

            }
        }
        public static void BuscarVentasPorEmpleados(ref DataTable lst, int IdEmpleado)
        {
            try
            {
                ConexionMaestra.abrir();
                var sql = new SqlDataAdapter("MostrarReporteVentasEmpleados", ConexionMaestra.conectar);
                sql.SelectCommand.CommandType = CommandType.StoredProcedure;
                
                sql.SelectCommand.Parameters.AddWithValue("@idusuario", IdEmpleado);
                sql.Fill(lst);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
        public static void MostrarReporteVentasFechasEmpleados(ref DataTable lst, int IdEmpleado,string fi, string ff)
        {
            try
            {
                ConexionMaestra.abrir();
                var sql = new SqlDataAdapter("MostrarReporteVentasFechasEmpleados", ConexionMaestra.conectar);
                sql.SelectCommand.CommandType = CommandType.StoredProcedure;
                sql.SelectCommand.Parameters.AddWithValue("@fi", fi);
                sql.SelectCommand.Parameters.AddWithValue("@ff", ff);
                sql.SelectCommand.Parameters.AddWithValue("@idusuario", IdEmpleado);
                sql.Fill(lst);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
        public static void BuscarVentasPorFechasConsultadas(ref DataTable lst, string fi, string ff)
        {
            try
            {
                ConexionMaestra.abrir();
                var sql = new SqlDataAdapter("MostrarReporteVentasFechas", ConexionMaestra.conectar);
                sql.SelectCommand.CommandType = CommandType.StoredProcedure;
                sql.SelectCommand.Parameters.AddWithValue("@fi", fi);
                sql.SelectCommand.Parameters.AddWithValue("@ff", ff);
                sql.Fill(lst);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }


        }
        private static void ObtenerTodasVentasPorEmpYFecha( string  fecha,ref DataTable dtVentasPorFecha,int IdUser  )
        {
            try
            {
                ConexionMaestra.abrir();
                var consul = "select idventa FROM Ventas where CONVERT(date,fecha_venta) = '"+fecha+"' and Id_usuario = "+IdUser;
                var da = new SqlDataAdapter(consul, ConexionMaestra.conectar);
                da.Fill(dtVentasPorFecha);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " ObtenerTodasVentasPorFecha");
            }

        }
        private static void ObtenerTodasVentasPorFecha(string fecha, ref DataTable dtVentasPorFecha)
        {
            try
            {
                ConexionMaestra.abrir();
                var consul = "select idventa FROM Ventas where CONVERT(date,fecha_venta) = '" + fecha + "'" ;
                var da = new SqlDataAdapter(consul, ConexionMaestra.conectar);
                da.Fill(dtVentasPorFecha);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " ObtenerTodasVentasPorFecha");
            }

        }
        private static void ObtenerTodasVentasPorFechaYempleado(string fecha, ref DataTable dtVentasPorFecha,int IdUser )
        {
            try
            {
                ConexionMaestra.abrir();
                var consul = "select idventa FROM Ventas where CONVERT(date,fecha_venta) = '" + fecha + "' and Id_usuario = "+IdUser;
                var da = new SqlDataAdapter(consul, ConexionMaestra.conectar);
                da.Fill(dtVentasPorFecha);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " ObtenerTodasVentasPorFecha");
            }

        }
        private static void SumarVentasBrutaPorFecha(string fecha, ref double Resultado)
        {
            try
            {
                ConexionMaestra.abrir();
                var consul = "SELECT SUM(Monto_total) FROM Ventas where CONVERT(date, fecha_venta) = '" + fecha+"'";
                var da = new SqlCommand (consul, ConexionMaestra.conectar);
                Resultado = Convert.ToDouble(da.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private static void SumarVentasBrutaPorFechaYUser(string fecha, ref double Resultado,int IdUser)
        {
            try
            {
                ConexionMaestra.abrir();
                var consul = "SELECT SUM(Monto_total) FROM Ventas where CONVERT(date, fecha_venta) = '" + fecha + "' and Id_usuario = "+IdUser;
                var da = new SqlCommand(consul, ConexionMaestra.conectar);
                Resultado = Convert.ToDouble(da.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private static void SumarCostoProdPorVenta(int Idventa, ref double Resultado)
        {
            var dt = new DataTable();
            double CantidadTotal = 0;
            try
            {
                ConexionMaestra.abrir();
                var consul = $"SELECT cantidad,Id_producto from detalle_venta where idventa = {Idventa}";
                var da = new SqlDataAdapter(consul, ConexionMaestra.conectar);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " 1");
            }
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    int IdProducto = Convert.ToInt32(item["Id_producto"].ToString());
                    double Cantidad = Convert.ToDouble(item["cantidad"].ToString());

                    ConexionMaestra.abrir();
                    var consul = $"select Precio_de_compra from Productos1 where Id_Producto =  {IdProducto}";
                    var da = new SqlCommand(consul, ConexionMaestra.conectar);
                    double variableTemporal = Convert.ToDouble(da.ExecuteScalar());
                    ConexionMaestra.cerrar();

                    CantidadTotal += (variableTemporal * Cantidad);
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message + " 2");
            }
            Resultado += CantidadTotal;
        }
        private static void SumarPorcentajeImpuestoPorVenta(int Idventa, ref double Resultado)
        {
            try
            {
                ConexionMaestra.abrir();
                var consul = "select sum(IGV) from Ventas where idventa =  " + Idventa;
                var da = new SqlCommand(consul, ConexionMaestra.conectar);
                Resultado += Convert.ToDouble(da.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
             
            }

        }
        private static void SumarGananciaPorFecha(int Idventa, ref double Resultado)
        {
            ConexionMaestra.abrir();
            var consul = "select sum(Ganancia) from detalle_venta where idventa = " + Idventa;
            var execute = new SqlCommand(consul, ConexionMaestra.conectar);
            Resultado += Convert.ToDouble(execute.ExecuteScalar());
            ConexionMaestra.cerrar();
        }
        //Reporte de Compras
        public static void ReporteComprasRealizadasFechas(ref DataTable dtReporte, string fi, string ff)
        {

            //Obtener Fechas De compras
            var dtFechas = new DataTable();
            MostrarFechasComprasDistintasFechas(ref dtFechas,fi,ff);
            //CREACION DE COLUMNAS
            string NameEmpresa = MostrarNombreEmpresaVariable();
            dtReporte.Clear();
            dtReporte.Columns.Add("Fecha");
            dtReporte.Columns.Add("Cant");
            dtReporte.Columns.Add("Invertido");
            dtReporte.Columns.Add("Nombre_Empresa");
            dtReporte.Columns.Add("fi");
            dtReporte.Columns.Add("ff");
            foreach (DataRow item in dtFechas.Rows)
            {
                double CantidadProd = 0;
                double TotalComprado = 0;
                //Obtencion de la fecha actual
                DateTime f = Convert.ToDateTime(item["Fecha"].ToString());
                var FechaRecorriendo = f.ToString("dd/MM/yyyy");

                //Obtener datos por fecha
                var dtIdCompras = new DataTable();
                MostrarIdComprasPorFecha(ref dtIdCompras, FechaRecorriendo);
                foreach (DataRow itemIdCompras in dtIdCompras.Rows)
                {
                    int IdCompra = Convert.ToInt32(itemIdCompras["Idcompra"].ToString());
                    SumarCantidadProd_Compra(ref CantidadProd, IdCompra);
                    SumarCantidadPagado_Compra(ref TotalComprado, IdCompra);
                }
                //Asignacion 
                DataRow row = dtReporte.NewRow();
                row["Fecha"] = FechaRecorriendo;
                row["Cant"] = Bases.AsignarComa(CantidadProd);
                row["Invertido"] = Bases.AsignarComa(TotalComprado);
                row["fi"] = fi;
                row["ff"] = ff;
                row["Nombre_Empresa"] = NameEmpresa;
                dtReporte.Rows.Add(row);
            }
            DataRow row2 = dtReporte.NewRow();
            row2["Nombre_Empresa"] = NameEmpresa;
            dtReporte.Rows.Add(row2);
        }
        public static void ReporteComprasRealizadas(ref DataTable dtReporte)
        {

            //Obtener Fechas De compras
            var dtFechas = new DataTable();
            MostrarFechasComprasDistintas(ref dtFechas);
            //CREACION DE COLUMNAS
            string NameEmpresa = MostrarNombreEmpresaVariable();
            dtReporte.Clear();
            dtReporte.Columns.Add("Fecha");
            dtReporte.Columns.Add("Cant");
            dtReporte.Columns.Add("Invertido");
            dtReporte.Columns.Add("Nombre_Empresa");
            foreach (DataRow item in dtFechas.Rows)
            {
                double CantidadProd = 0;
                double TotalComprado = 0;
                //Obtencion de la fecha actual
                DateTime f = Convert.ToDateTime(item["Fecha"].ToString());
                var FechaRecorriendo = f.ToString("dd/MM/yyyy");

                //Obtener datos por fecha
                var dtIdCompras = new DataTable();
                MostrarIdComprasPorFecha(ref dtIdCompras, FechaRecorriendo);
                foreach (DataRow itemIdCompras in dtIdCompras.Rows)
                {
                    int IdCompra = Convert.ToInt32(itemIdCompras["Idcompra"].ToString());
                    SumarCantidadProd_Compra(ref CantidadProd, IdCompra);
                    SumarCantidadPagado_Compra(ref TotalComprado, IdCompra);
                }
                //Asignacion 
                DataRow row = dtReporte.NewRow();
                row["Fecha"] = FechaRecorriendo;
                row["Cant"] = Bases.AsignarComa(CantidadProd);
                row["Invertido"] = Bases.AsignarComa(TotalComprado);
                row["Nombre_Empresa"] = NameEmpresa;
                dtReporte.Rows.Add(row);
            }
            DataRow row2 = dtReporte.NewRow();
            row2["Nombre_Empresa"] = NameEmpresa;
            dtReporte.Rows.Add(row2);
        }
        private static void SumarCantidadProd_Compra(ref double resultado, int IdCompra)
        {
            try
            {
                ConexionMaestra.abrir();
                var consulta = $"select sum(Cantidad) from DetalleCompra where IdCompra = {IdCompra}";
                var da = new SqlCommand(consulta, ConexionMaestra.conectar);
                resultado += Convert.ToDouble(da.ExecuteScalar());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        private static void SumarCantidadPagado_Compra(ref double resultado, int IdCompra)
        {
            try
            {
                ConexionMaestra.abrir();
                var consulta = $"select SUM(Total) from Compras where IdCompra = {IdCompra}";
                var da = new SqlCommand(consulta, ConexionMaestra.conectar);
                resultado += Convert.ToDouble(da.ExecuteScalar());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        private static void MostrarIdComprasPorFecha(ref DataTable dt , string Fecha)
        {
            try
            {
                ConexionMaestra.abrir();
                //Obtener Fechas De compras
                var consulta = $" select Idcompra from Compras where CONVERT(date, fechacompra) = '{Fecha}'";
                var da = new SqlDataAdapter(consulta, ConexionMaestra.conectar);
                da.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        private static void MostrarFechasComprasDistintasFechas(ref DataTable dtFechas,string fi,string ff)
        {
            try
            {
                ConexionMaestra.abrir();
                //Obtener Fechas De compras
                var da = new SqlDataAdapter("MostrarFechasComprasDistintasFECHAS", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fi", fi);
                da.SelectCommand.Parameters.AddWithValue("@ff", ff);
                da.Fill(dtFechas);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        private static void MostrarFechasComprasDistintas(ref DataTable dtFechas)
        {
            try
            {
                ConexionMaestra.abrir();
                //Obtener Fechas De compras
                var da = new SqlDataAdapter("MostrarFechasComprasDistintas", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dtFechas);
            }
            catch (Exception ex )
            {

            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        //Clientes
        public static void Mostrar_cliente_standar(ref int IdclienteGenerico)
        {
            var consult = "select idclientev,Nombre from Clientes where Estado = '0'";
            var dt = new DataTable();
            var com = new SqlDataAdapter(consult, DataAccess.ConexionMaestra.conectar);
            try
            {
                DataAccess.ConexionMaestra.abrir();
                com.Fill(dt);
                DataAccess.ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            //Obtener Data
            foreach (DataRow item in dt.Rows)
            {

                IdclienteGenerico = Convert.ToInt32(item["idclientev"].ToString());
            }

        }
        public static void ReporteCuestasPorCobrar(ref DataTable dt)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteCuestasPorCobrar", ConexionMaestra.conectar);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        //Proveedores
        public static void ReporteCuestasPorPagar(ref DataTable dt)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteCuentasPorPagar", ConexionMaestra.conectar);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + " Message ::: "+ ex.Message);
            }
        }
        public static void MostrarMovimientosPagos(ref DataTable dt, int Idproveedor)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("MostrarMovimientosPagos", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idproveedor", Idproveedor);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }public static void PagosRealizadorProveedor(ref DataTable dt, int Idproveedor)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("PagosRealizadorProveedor", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idproveedor", Idproveedor);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        public static void ReporteCuestasPorPagarPorProveedor(ref DataTable dt, int Idproveedor)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("MostrarCuentaPagarProveedoresReporte", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idproveedor", Idproveedor);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        //Kardex
        public static void BuscarMovKardex_filtros(ref DataTable dt, DateTime fecha, string tipo, string IdUser)
        {
            try
            {
                ConexionMaestra.abrir();

                var da = new SqlDataAdapter("buscar_MOVIMIENTOS_DE_KARDEX_filtros", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fecha", fecha);
                da.SelectCommand.Parameters.AddWithValue("@tipo", tipo);
                da.SelectCommand.Parameters.AddWithValue("@Id_usuario", IdUser);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static void BUSCAR_PRODUCTOS_KARDEX(ref DataTable dt, string buscador)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("BUSCAR_PRODUCTOS_KARDEX", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letrab", buscador);
                da.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
        //Usuarios
        public static bool MostrarIconoNombreNombrePorUsuario(string login,ref PictureBox img,ref Label lbl)
        {
            try
            {
                var dt = new DataTable();
                ConexionMaestra.abrir();
                SqlDataAdapter cmd = new SqlDataAdapter("MostrarIconoNombreNombrePorUsuario", ConexionMaestra.conectar);
                cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmd.SelectCommand.Parameters.AddWithValue("@login", login);
                cmd.Fill(dt);
                //Procesamiento datos
                foreach(DataRow item in dt.Rows)
                {
                    byte[] b = (byte[])item["Icono"];
                    MemoryStream ms = new MemoryStream(b);
                    img.Image = Image.FromStream(ms);

                    lbl.Text = item["NombreYApellido"].ToString();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        //Compras
        public static void MostrarTotalCompra(ref double total, int idcompra)
        {
            try
            {
                ConexionMaestra.abrir();
                var command = "select Sum(Total) from Compras where Idcompra = " + idcompra;
                var sql = new SqlCommand(command, ConexionMaestra.conectar);
                total = Convert.ToDouble(sql.ExecuteScalar());
                
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static void MostrarDetallePagoCompra(ref string TipoPago,ref double Efectivo,ref double credito, int idcompra)
        {
            try
            {
                DataTable dt = new DataTable();
                ConexionMaestra.abrir();
                var command = "select Tipo_Pago,Efectivo,Credito from MediosPago_Compras where Id_Compra =" + idcompra;
                var sql = new SqlDataAdapter(command, ConexionMaestra.conectar);
                sql.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    TipoPago = item["Tipo_Pago"].ToString();
                    Efectivo = Convert.ToDouble(item["Efectivo"].ToString());
                    credito = Convert.ToDouble(item["Credito"].ToString());
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static void MostrarSaldoPorProveedor(ref double saldoactual, int Idproveedor )
        {
            try
            {
                DataTable dt = new DataTable();
                ConexionMaestra.abrir();
                var command = "Select Saldo from Proveedores where IdProveedor =" + Idproveedor;
                var sql = new SqlCommand(command, ConexionMaestra.conectar);
                saldoactual = Convert.ToDouble(sql.ExecuteScalar());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }

        public static void ObtenerSaldoCreditoHistorialCompras(ref double saldoactual, int Idcompra)
        {
            try
            {
                DataTable dt = new DataTable();
                ConexionMaestra.abrir();
                var command = "select Credito from MediosPago_Compras where Id_Compra = " + Idcompra;
                var sql = new SqlCommand(command, ConexionMaestra.conectar);
                saldoactual = Convert.ToDouble(sql.ExecuteScalar());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        //Cobros
        public static void MostrarFacturaCobroCredito(ref DataTable dt, int idIngreso)
        {
            try
            {
                
                ConexionMaestra.abrir();
                
                var sql = new SqlDataAdapter("MostrarFacturaCobroCredito", ConexionMaestra.conectar);
                sql.SelectCommand.CommandType = CommandType.StoredProcedure;
                sql.SelectCommand.Parameters.AddWithValue("@idingreso", idIngreso);
                sql.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        //CUALQUIER COSA
        public static void MostrarFacturasConsumoResumen(ref LresumenFacturasConsumo models, string fi, string ff)
        {
            try
            {
                var dt = new DataTable();
                ConexionMaestra.abrir();
                //Obtener Fechas De compras
                var da = new SqlDataAdapter("MostrarFacturasConsumoResumen", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fi", fi);
                da.SelectCommand.Parameters.AddWithValue("@ff", ff);
                da.Fill(dt);


                //Process Information:
                foreach (DataRow item in dt.Rows)
                {
                    models.Cantidadncf = Obtener_datos.AsignarComa(Convert.ToDouble(item["CantidadNCF"].ToString()));
                    models.MontoFacturado = Obtener_datos.AsignarComa(Convert.ToDouble(item["MontoFaturado"].ToString()));
                    models.TotalImpuesto = Obtener_datos.AsignarComa(Convert.ToDouble(item["TotalImpuesto"].ToString()));
                    models.Efectivo = Obtener_datos.AsignarComa(Convert.ToDouble(item["Efectivo"].ToString()));
                    models.Tarjeta = Obtener_datos.AsignarComa(Convert.ToDouble(item["Tarjeta"].ToString()));
                    models.Credito = Obtener_datos.AsignarComa(Convert.ToDouble(item["Credito"].ToString()));
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
    }
}

