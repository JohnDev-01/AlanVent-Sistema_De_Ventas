using AlanVent_Sistema_De_Ventas.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.DataAccess
{
    class Editar_datos
    {
        int idcaja;
        //Activaciones de configuraciones
        public static void ModificarEstadoImprimirCierreCaja(string estado)
        {
            ConexionMaestra.abrir();
            SqlCommand cmd = new SqlCommand("ModificarEstadoImprimirCierreCaja", ConexionMaestra.conectar);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tipo", "IMPRIMIRCIERRECAJA");
            cmd.Parameters.AddWithValue("@estado", estado);
            cmd.ExecuteNonQuery();
            ConexionMaestra.cerrar();
        } 
        public static void cambio_de_Caja(int idCaja,int idVenta)
        {
            ConexionMaestra.abrir();
            SqlCommand cmd = new SqlCommand("cambio_de_Caja", ConexionMaestra.conectar);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idcaja", idCaja);
            cmd.Parameters.AddWithValue("@idventa", idVenta);
            cmd.ExecuteNonQuery();
            ConexionMaestra.cerrar();
        }
        public static void ingresar_nombre_a_venta_en_espera(int idVenta,string nombre)
        {
            ConexionMaestra.abrir();
            SqlCommand cmd = new SqlCommand("ingresar_nombre_a_venta_en_espera", ConexionMaestra.conectar);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@idventa", idVenta);
            cmd.ExecuteNonQuery();
            ConexionMaestra.cerrar();
        }
        public static bool editar_Conceptos(int idconcepto, string descripcion)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("editar_Conceptos", DataAccess.ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_concepto", idconcepto);
                cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                cmd.ExecuteNonQuery();
                ConexionMaestra.cerrar();
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public static bool editar_dinero_caja_inicial(int idcaja, double saldo)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("editar_dinero_caja_inicial", DataAccess.ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_caja", idcaja);
                cmd.Parameters.AddWithValue("@saldo", saldo);
                cmd.ExecuteNonQuery();
                ConexionMaestra.cerrar();
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool restaurar_proveedores(Lproveedores p)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("restaurar_Proveedores", ConexionMaestra.conectar);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProveedor", p.IdProveedor);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public bool restaurar_clientes(Lclientes p)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("restaurar_clientes", ConexionMaestra.conectar);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idcliente", p.IdCliente);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static bool EditarFormatoFactura(string notas)
        {
            
            
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("EditarFormatoFactura", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@notas", notas);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        } public static bool ActualizarComprobanteVentas(int idventa)
        {
            
            var new_serie = SerializacionComprobantes.GenerarSerializacionIngresosConsumo();
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("ActualizarComprobanteVentas", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idventa", idventa);
                cmd.Parameters.AddWithValue("@serializacion", new_serie);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static bool ActualizarIdclienteEnVenta(int idventa, int Idcliente)
        {
            
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("ActualizarIdclienteEnVenta", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcliente", Idcliente);
                cmd.Parameters.AddWithValue("@idventa", idventa);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static bool EditarSerializacionNCF(string creditoFiscal, string consumo, string compras, string gastosMenores)
        {
            
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("EditarSerializacionNCF", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@creditoFiscal", creditoFiscal);
                cmd.Parameters.AddWithValue("@Consumo", consumo);
                cmd.Parameters.AddWithValue("@Compras", compras);
                cmd.Parameters.AddWithValue("@GastosMenores", gastosMenores);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static bool ElegirComprobantePorDefectoVentas(int idserie)
        {
           
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("ElegirComprobantePorDefectoVentas", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idserie", idserie);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }public bool editar_clientes(Lclientes P)
        {

            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("editar_clientes", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idcliente", P.IdCliente);
                cmd.Parameters.AddWithValue("@Nombre", P.Nombre);
                cmd.Parameters.AddWithValue("@Direccion", P.Direccion);
                cmd.Parameters.AddWithValue("@Celular", P.Celular);
                cmd.Parameters.AddWithValue("@tipoidentificacion", P.Tipoidentificacion);
                cmd.Parameters.AddWithValue("@cedula", P.Cedula);
                cmd.Parameters.AddWithValue("@rnc", P.Rnc);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public bool editarRespaldos(Lempresa P)
        {

            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("editarRespaldos", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ult_Fecha_copiaBd", P.Ultima_fecha_de_copia_de_seguridad);
                cmd.Parameters.AddWithValue("@Carpeta_Para_copia_de_seguridad", P.Carpeta_para_copias_de_seguridad);
                cmd.Parameters.AddWithValue("@Ult_Fecha_de_copia_date", P.Ultima_fecha_de_copia_date);
                cmd.Parameters.AddWithValue("@Frecuencia_de_copia", P.Frecuencia_de_copias);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public bool editarCorreobase(Lcorreo parametros)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("editar_correo_base", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Correo", parametros.Correo);
                cmd.Parameters.AddWithValue("@Password", parametros.Password);
                cmd.Parameters.AddWithValue("@Estado_De_envio", parametros.Estado);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public bool cerrarCaja(Lmcaja parametros)
        {
            try
            {
                int Idcierre = 0;
                Obtener_datos.MostrarUltimoIdCierreCaja(ref Idcierre);

                ConexionMaestra.abrir();
                var cmd = new SqlCommand("cerrarCaja", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fechafin", parametros.fechafin);
                cmd.Parameters.AddWithValue("@fechacierre", parametros.fechacierre);
                cmd.Parameters.AddWithValue("@ingresos", parametros.ingresos);
                cmd.Parameters.AddWithValue("@egresos", parametros.egresos);
                cmd.Parameters.AddWithValue("@Id_usuario", parametros.Id_usuario);
                cmd.Parameters.AddWithValue("@Total_calculado", parametros.Total_calculado);
                cmd.Parameters.AddWithValue("@Total_real", parametros.Total_real);
                cmd.Parameters.AddWithValue("@Estado", parametros.Estado);
                cmd.Parameters.AddWithValue("@Diferencia", parametros.Diferencia);
                cmd.Parameters.AddWithValue("@Id_caja", parametros.Id_caja);
                cmd.Parameters.AddWithValue("@Idcierre",Idcierre);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return true;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public bool editarMarcan(LMarcan p)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("EDITAR_marcan_a",ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@e",p.E);
                cmd.Parameters.AddWithValue("@fa",p.FA);
                cmd.Parameters.AddWithValue("@f",p.F);
                cmd.Parameters.AddWithValue("@s",p.S);
                cmd.ExecuteNonQuery();
                ConexionMaestra.cerrar();
                return true;
            }
            catch (Exception ex )
            {
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public bool disminuirSaldocliente(Lclientes parametros, double monto)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("disminuirSaldocliente", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcliente", parametros.IdCliente);
                cmd.Parameters.AddWithValue("@monto", monto);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static bool ActualizarSerializacionCreditoFiscal(long CantidadSinCeros)
        {
            try
            {
                ConexionMaestra.abrir();
                var cmd = new SqlCommand("ActualizarSerializacionEgresos", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cantidad", CantidadSinCeros);
                cmd.ExecuteNonQuery();
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
        public static bool ActualizarSerializacionGastosMenores(long CantidadSinCeros)
        {
            try
            {
                ConexionMaestra.abrir();
                var cmd = new SqlCommand("ActualizarSerializacionGastosMenores", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cantidad", CantidadSinCeros);
                cmd.ExecuteNonQuery();
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
        public static bool ActualizarSerializacionProvInformal(long CantidadSinCeros)
        {
            try
            {
                ConexionMaestra.abrir();
                var cmd = new SqlCommand("ActualizarSerializacionProvInformal", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cantidad", CantidadSinCeros);
                cmd.ExecuteNonQuery();
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
        public static bool AplicarConfiguracionImpuestosProductos()
        {
            try
            {
                ConexionMaestra.abrir();
                var cmd = new SqlCommand("AplicarConfiguracionImpuestosProductos", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
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
        public static bool EliminarDetalleCodigoBarras(int iddetalle)
        {
            try
            {
                ConexionMaestra.abrir();
                var cmd = new SqlCommand("EliminarDetalleCodigoBarras", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iddetalle", iddetalle);
                cmd.ExecuteNonQuery();
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
        }public static bool SumarDetalleCodigoBarras(int iddetalle, int cantidad)
        {
            try
            {
                ConexionMaestra.abrir();
                var cmd = new SqlCommand("SumarDetalleCodigoBarras", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iddetalle", iddetalle);
                cmd.Parameters.AddWithValue("@cantidad", cantidad);
                cmd.ExecuteNonQuery();
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
        public static bool ActualizarCantidadCodigosBarras(int iddetalle, int cantidad)
        {
            try
            {
                ConexionMaestra.abrir();
                var cmd = new SqlCommand("ActualizarCantidadCodigosBarras", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cantidad", cantidad);
                cmd.Parameters.AddWithValue("@iddetalle", iddetalle);
                cmd.ExecuteNonQuery();
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
        public static bool RestarDetalleCodigoBarras(int iddetalle)
        {
            try
            {
                ConexionMaestra.abrir();
                var cmd = new SqlCommand("RestarDetalleCodigoBarras", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iddetalle", iddetalle);
                cmd.ExecuteNonQuery();
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
        public static bool ActualizarSerializacionFacturaConsumo(long CantidadSinCeros)
        {
            try
            {
                ConexionMaestra.abrir();
                var cmd = new SqlCommand("ActualizarSerializacionIngresos", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cantidad", CantidadSinCeros);
                cmd.ExecuteNonQuery();
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
        public static bool ActualizarComprobanteModificadoCompras(string NumeroComprobante, int Idcompra)
        {
            try
            {
                ConexionMaestra.abrir();
                var cmd = new SqlCommand("ActualizarComprobanteModificadoCompras", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NumeroComprobante", NumeroComprobante);
                cmd.Parameters.AddWithValue("@Idcompra", Idcompra);
                cmd.ExecuteNonQuery();
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
        //-----Caja-----
        public bool editar_caja_impresoras(Limpresoras parametros)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("editar_caja_impresoras", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcaja", parametros.idcaja);
                cmd.Parameters.AddWithValue("@Impresora_Ticket", parametros.Impresora_Ticket);
                cmd.Parameters.AddWithValue("@Impresora_A4", parametros.Impresora_A4);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public bool EditarBascula(Lcaja parametros)
        {
            try
            {
                Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("EditarBascula", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcaja", idcaja);
                cmd.Parameters.AddWithValue("@Puerto", parametros.PuertoBalanza);
                cmd.Parameters.AddWithValue("@Estado", parametros.EstadoBalanza);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public bool EditarTemaCaja(Lcaja parametros)
        {
            try
            {
                Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("EditarTemaCaja", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcaja", idcaja);
                cmd.Parameters.AddWithValue("@tema", parametros.Tema);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        //-------------------
        public bool aumentarSaldocliente(Lclientes parametros, double monto)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("aumentar_saldo_a_cliente", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcliente", parametros.IdCliente);
                cmd.Parameters.AddWithValue("@Saldo", monto);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static void Aumentar_saldo_tabla_movimientos_cliente(int idRegistro, double monto)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("Aumentar_saldo_tabla_movimientos_cliente", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idRegistro", idRegistro);
                cmd.Parameters.AddWithValue("@monto", monto);
                cmd.ExecuteNonQuery();
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
        //detalle de venta
        public bool aplicar_precio_mayoreo(LdetalleVenta parametros)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("aplicar_precio_mayoreo", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idproducto", parametros.Id_producto);
                cmd.Parameters.AddWithValue("@iddetalleventa", parametros.iddetalle_venta);
                cmd.ExecuteNonQuery();
                return true;


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public bool editarPrecioVenta(LdetalleVenta parametros)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("editarPrecioVenta", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iddetalleventa", parametros.iddetalle_venta);
                cmd.Parameters.AddWithValue("@precio", parametros.preciounitario);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public bool DetalleventaDevolucion(LdetalleVenta parametros)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("DetalleventaDevolucion", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iddetalle", parametros.iddetalle_venta);
                cmd.Parameters.AddWithValue("@cantidad", parametros.cantidad);
                cmd.Parameters.AddWithValue("@cantidadMostrada", parametros.Cantidad_mostrada);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public bool AumentarStockDetalle(LdetalleVenta parametros)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("aumentar_stock_en_detalle_de_venta", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Producto1", parametros.Id_producto);
                cmd.Parameters.AddWithValue("@cantidad", parametros.cantidad);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        //ventas 
        public bool EditarVenta(Lventas parametros)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("editarVenta", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idventa", parametros.idventa);
                cmd.Parameters.AddWithValue("@monto", parametros.Monto_total);
                cmd.ExecuteNonQuery();
                return true; 
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }

        }
        //--------------Productos-----------
        public bool Disminuir_stock(Lproductos parametros)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("disminuir_stock", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idproducto", parametros.Id_Producto1);
                cmd.Parameters.AddWithValue("@cantidad", parametros.Stock);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public bool EditarPreciosProductos(Lproductos parametros)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("EditarPreciosProductos", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idproducto", parametros.Id_Producto1);
                cmd.Parameters.AddWithValue("@precioventa", parametros.Precio_de_venta);
                cmd.Parameters.AddWithValue("@costo", parametros.Precio_de_compra);
                cmd.Parameters.AddWithValue("@preciomayoreo", parametros.Precio_mayoreo);
                cmd.Parameters.AddWithValue("@cantidadAgregada", parametros.Stock);
                cmd.Parameters.AddWithValue("@apartir", parametros.A_partir_de);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public bool aumentarStock(Lproductos parametros)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("aumentarStock", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idproducto", parametros.Id_Producto1);
                cmd.Parameters.AddWithValue("@cantidad", parametros.Stock);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static bool ActualizarCodigoProducto()
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("ActualizarCodigoProducto", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static void Editar_precio_compra_producto(double precio, int idproducto,ref int vuelta )
        {
            try
            {
                ConexionMaestra.abrir();
                var cmd = new  SqlCommand("Editar_precio_compra_desde_COMPRAS", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@precio", precio);
                cmd.Parameters.AddWithValue("@id_producto", idproducto);
               vuelta =  cmd.ExecuteNonQuery();
                
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
        public static void Editar_precio_venta_desde_COMPRAS(double precio, int idproducto)
        {
            try
            {
                ConexionMaestra.abrir();
                var cmd = new SqlCommand("Editar_precio_venta_desde_COMPRAS", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@precio", precio);
                cmd.Parameters.AddWithValue("@id_producto", idproducto);
                cmd.ExecuteNonQuery();

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
        //EMPRESA 
        public static  bool CambiarEstadoRespaldos(string estado)
        {
            try
            {
                ConexionMaestra.abrir();
                var cmd = new SqlCommand("DeshabilitarRespaldos",ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@estado", estado);
                cmd.ExecuteNonQuery();
                return true;
                
            }
            catch ( Exception ex)
            {
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        //Compras 
        public static bool RestarSaldoProveedor_Compras(int IdCompra, double TotalProveedor, double TotalCreditoCompra)
        {
            
            try
            {
                
                
                ConexionMaestra.abrir();

                var cmd = new SqlCommand("RestarSaldoProveedor_Compras", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCompra", IdCompra);
                cmd.Parameters.AddWithValue("@SaldoCalculado", TotalProveedor);
                cmd.Parameters.AddWithValue("@SaldoParaTotalCreditoCompra", TotalCreditoCompra);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }

        }
        //Pago Credito
        public static bool Actualizar_cobro_credito(int IdRegistro, double credito)
        {
            try
            {
                ConexionMaestra.abrir();
                var cmd = new SqlCommand("actualizar_cobro_credito", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idRegistro", IdRegistro);
                cmd.Parameters.AddWithValue("@credito", credito);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }

        }
        public static bool Editar_Cobro_de_vuelta(string No_venta)
        {
            try
            {
                ConexionMaestra.abrir();
                var cmd = new SqlCommand("Editar_Cobro_de_vuelta", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@No_venta", No_venta);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }

        }
    }
}
