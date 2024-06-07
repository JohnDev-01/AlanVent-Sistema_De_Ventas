using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using AlanVent_Sistema_De_Ventas.Logic;

namespace AlanVent_Sistema_De_Ventas.DataAccess
{
    public class Insertar_datos
    {
        int idcaja;
        int idusuario;
        //Gastos
        public static bool insertar_Conceptos(string descripcion)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("insertar_Conceptos", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                cmd.ExecuteNonQuery();
                ConexionMaestra.cerrar();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static void InsertarConceptosPorDefecto()
        {
            try
            {
                ConexionMaestra.abrir();
                var sql = new SqlCommand("InsertarGastosPorDefecto", ConexionMaestra.conectar);
                sql.CommandType = CommandType.StoredProcedure;
                sql.ExecuteNonQuery();
                ConexionMaestra.cerrar();

            }
            catch (Exception ex)
            {

            }
        }
        public static bool InsertarCodigoProductoPorDefecto()
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("InsertarCodigoProductoPorDefecto", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                ConexionMaestra.cerrar();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool insertar_Gastos_varios(Lgastos models)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("insertar_Gastos_varios", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fecha", models.Fecha);
                cmd.Parameters.AddWithValue("@Nro_documento", models.NroDocumento);
                cmd.Parameters.AddWithValue("@Tipo_comprobante", models.TipoComprobante);
                cmd.Parameters.AddWithValue("@Importe", models.Importe);
                cmd.Parameters.AddWithValue("@Descripcion", models.Descripcion);
                cmd.Parameters.AddWithValue("@Id_caja", models.idcaja);
                cmd.Parameters.AddWithValue("@Id_conceptos", models.Idconceptos);
                cmd.Parameters.AddWithValue("@rnc", models.Rnc);
                cmd.Parameters.AddWithValue("@Cedula", models.Cedula);
                cmd.Parameters.AddWithValue("@TipoDocumento", models.TipoDocumento);
                cmd.Parameters.AddWithValue("@NumeroComprobante", models.NumeroComprobante);
                cmd.Parameters.AddWithValue("@NumeroComprobanteModificado", models.NroDocumento);
                cmd.Parameters.AddWithValue("@modopago", models.ModoPago);
                cmd.Parameters.AddWithValue("@tipodegasto", models.TipoGasto);
                cmd.ExecuteNonQuery();
                ConexionMaestra.cerrar();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "-----" + ex.Message);
                return false;
            }
        }
        public static bool insertar_Ingresos_varios(DateTime fecha, string Nro_documento,
         string Tipo_comprobante, double Importe, string Descripcion, int Id_caja)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("insertar_Ingresos_varios", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fecha", fecha);
                cmd.Parameters.AddWithValue("@Nro_comprobante", Nro_documento);
                cmd.Parameters.AddWithValue("@Tipo_comprobante", Tipo_comprobante);
                cmd.Parameters.AddWithValue("@Importe", Importe);
                cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
                cmd.Parameters.AddWithValue("@Id_caja", Id_caja);

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
        public bool insertar_CreditoPorPagar(Logic.LCreditosPorPagar p)
        {
            try
            {
                Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("insertar_CreditoPorPagar", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Descripcion", p.Descripcion);
                cmd.Parameters.AddWithValue("@Fecha_registro", p.Fecha_registro);
                cmd.Parameters.AddWithValue("@Fecha_vencimiento", p.Fecha_vencimiento);
                cmd.Parameters.AddWithValue("@Total", p.Total);
                cmd.Parameters.AddWithValue("@Saldo", p.Saldo);
                cmd.Parameters.AddWithValue("@Estado", "DEBE");
                cmd.Parameters.AddWithValue("@Id_caja", idcaja);
                cmd.Parameters.AddWithValue("@Id_Proveedor", p.Id_Proveedor);
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
        public static bool InsertarCierreCajaRealizados(LcierreCajaCerrado models)
        {
            try
            {
                
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("InsertarCierreCajaRealizados", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fechacierre", models.fechacierre);
                cmd.Parameters.AddWithValue("@SaldoInicial", models.SaldoIncial);
                cmd.Parameters.AddWithValue("@Ventasefectivo", models.VentasEfvo);
                cmd.Parameters.AddWithValue("@VentasTarjeta", models.VentasTarjeta);
                cmd.Parameters.AddWithValue("@VentasCredito", models.VentasCredito);
                cmd.Parameters.AddWithValue("@VentasTotales", models.VentasTotales);
                cmd.Parameters.AddWithValue("@IngresosVarios", models.IngresosVarios);
                cmd.Parameters.AddWithValue("@CobrosEftvo", models.CobrosEfvo);
                cmd.Parameters.AddWithValue("@CobrosTarjeta", models.CobrosTarjeta);
                cmd.Parameters.AddWithValue("@ImpuestosVentas", models.ImpuestosVentas);
                cmd.Parameters.AddWithValue("@CreditoPagar", models.CreditoPagar);
                cmd.Parameters.AddWithValue("@creditoCobrar", models.CreditoCobrar);
                cmd.Parameters.AddWithValue("@GastosVarios", models.GastosVarios);
                cmd.Parameters.AddWithValue("@GananciasVentas", models.GananciasVentas);
                cmd.Parameters.AddWithValue("@eftvoEsperado", models.EfvoEsperado);
                cmd.Parameters.AddWithValue("@diferencia", models.Diferencia);
                cmd.Parameters.AddWithValue("@idcajero", models.Idcajero);
                cmd.Parameters.AddWithValue("@idcaja", models.Idcaja);
                cmd.Parameters.AddWithValue("@PagosEfectivo", models.PagosEfectivo);
                cmd.Parameters.AddWithValue("@PagosTarjeta", models.PagosTarjeta);
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
        //Proveedores
        public static void GuardarPagos(int idproveedor, double saldoanterior, double efectivo,
            double tarjeta, double vuelto, double restante, ref int Numrecibo)
        {
            //Obtener Idcaja
            int Idcaja = 0;
            Obtener_datos.Obtener_id_caja_PorSerial(ref Idcaja);
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("InsertarPagos", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idproveedor", idproveedor);
                cmd.Parameters.AddWithValue("@saldoAnterior", saldoanterior);
                cmd.Parameters.AddWithValue("@Efectivo", efectivo);
                cmd.Parameters.AddWithValue("@Tarjeta", tarjeta);
                cmd.Parameters.AddWithValue("@vuelto", vuelto);
                cmd.Parameters.AddWithValue("@restante", restante);
                cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                cmd.Parameters.AddWithValue("@idcaja", Idcaja);
                Numrecibo = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Numrecibo = 0;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static void AumentarSaldoProveedor(double cantidad, int IdProvedor)
        {

            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("Aumentar_Saldo_Proveedor", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cant", cantidad);
                cmd.Parameters.AddWithValue("@idproveedor", IdProvedor);

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
        public bool Insertar_proveedores(Lproveedores models)
        {

            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("insertar_Proveedores", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", models.Nombre);
                cmd.Parameters.AddWithValue("@Direccion", models.Direccion);
                cmd.Parameters.AddWithValue("@Celular", models.Celular);
                cmd.Parameters.AddWithValue("@Estado", "ACTIVO");
                cmd.Parameters.AddWithValue("@Saldo", 0);
                cmd.Parameters.AddWithValue("@TipoIdentificador", models.TipoIdentificacion);
                cmd.Parameters.AddWithValue("@Cedula", models.Cedula);
                cmd.Parameters.AddWithValue("@Rnc", models.Rnc);
                cmd.Parameters.AddWithValue("@Informal", models.Prov_Informal);
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
        public bool Insertar_clientes(Lclientes P)
        {

            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("insertar_clientes", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", P.Nombre);
                cmd.Parameters.AddWithValue("@Direccion", P.Direccion);
                cmd.Parameters.AddWithValue("@Celular", P.Celular);
                cmd.Parameters.AddWithValue("@Estado", "ACTIVO");
                cmd.Parameters.AddWithValue("@Saldo", 0);
                cmd.Parameters.AddWithValue("@TipoIdentificacion", P.Tipoidentificacion);
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
        public bool editar_Proveedores(Lproveedores P)
        {

            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("editar_Proveedores", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProveedor", P.IdProveedor);
                cmd.Parameters.AddWithValue("@Nombre", P.Nombre);
                cmd.Parameters.AddWithValue("@Direccion", P.Direccion);
                cmd.Parameters.AddWithValue("@Celular", P.Celular);
                cmd.Parameters.AddWithValue("@TipoIdentificacion", P.TipoIdentificacion);
                cmd.Parameters.AddWithValue("@cedula", P.Cedula);
                cmd.Parameters.AddWithValue("@rnc", P.Rnc);
                cmd.Parameters.AddWithValue("@Informal", P.Prov_Informal);
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
        public bool Insertar_ControlCobros(Lcontrolcobros parametros, ref int Idregistro)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("Insertar_ControlCobros", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Monto", parametros.Monto);
                cmd.Parameters.AddWithValue("@Fecha", parametros.Fecha);
                cmd.Parameters.AddWithValue("@Detalle", parametros.Detalle);
                cmd.Parameters.AddWithValue("@IdCliente", parametros.IdCliente);
                cmd.Parameters.AddWithValue("@IdUsuario", parametros.IdUsuario);
                cmd.Parameters.AddWithValue("@IdCaja", parametros.IdCaja);
                cmd.Parameters.AddWithValue("@Comprobante", parametros.Comprobante);
                cmd.Parameters.AddWithValue("@efectivo", parametros.efectivo);
                cmd.Parameters.AddWithValue("@tarjeta", parametros.tarjeta);
                cmd.Parameters.AddWithValue("@credito_debia_pagar", parametros.credito_Debia_pagar);
                cmd.Parameters.AddWithValue("@No_venta", parametros.No_Venta);
                cmd.Parameters.AddWithValue("@SaldoAnterior", parametros.SaldoAnterior);
                Idregistro = Convert.ToInt32(cmd.ExecuteScalar());
                return true;
            }
            catch (Exception ex)
            {
                Idregistro = 0;
                MessageBox.Show(ex.Message);
                return true;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }

        public bool insertar_CreditoPorCobrar(LcreditoPorCobrar parametros)
        {
            try
            {
                Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("insertar_CreditoPorCobrar", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Descripcion", parametros.Descripcion);
                cmd.Parameters.AddWithValue("@Fecha_registro", parametros.Fecha_registro);
                cmd.Parameters.AddWithValue("@Fecha_vencimiento", parametros.Fecha_vencimiento);
                cmd.Parameters.AddWithValue("@Total", parametros.Total);
                cmd.Parameters.AddWithValue("@Saldo", parametros.Saldo);
                cmd.Parameters.AddWithValue("@Estado", "DEBE");
                cmd.Parameters.AddWithValue("@Id_caja", idcaja);
                cmd.Parameters.AddWithValue("@Id_cliente", parametros.Id_cliente);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }

        }
        //--------------------Kardex ----------------
        public bool insertar_KARDEX_Entrada(LKardex parametros)
        {
            try
            {
                Obtener_datos.mostrar_inicio_De_sesion(ref idusuario);
                Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("insertar_KARDEX_Entrada", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fecha", parametros.Fecha);
                cmd.Parameters.AddWithValue("@Motivo", parametros.Motivo);
                cmd.Parameters.AddWithValue("@Cantidad", parametros.Cantidad);
                cmd.Parameters.AddWithValue("@Id_producto", parametros.Id_producto);
                cmd.Parameters.AddWithValue("@Id_usuario", idusuario);
                cmd.Parameters.AddWithValue("@Tipo", "ENTRADA");
                cmd.Parameters.AddWithValue("@Estado", "DESPACHO CONFIRMADO");
                cmd.Parameters.AddWithValue("@Id_caja", idcaja);
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
        public bool insertar_KARDEX_SALIDA(LKardex parametros)
        {
            try
            {
                Obtener_datos.mostrar_inicio_De_sesion(ref idusuario);
                Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("insertar_KARDEX_SALIDA", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fecha", parametros.Fecha);
                cmd.Parameters.AddWithValue("@Motivo", parametros.Motivo);
                cmd.Parameters.AddWithValue("@Cantidad", parametros.Cantidad);
                cmd.Parameters.AddWithValue("@Id_producto", parametros.Id_producto);
                cmd.Parameters.AddWithValue("@Id_usuario", idusuario);
                cmd.Parameters.AddWithValue("@Tipo", "SALIDA");
                cmd.Parameters.AddWithValue("@Estado", "DESPACHO CONFIRMADO");
                cmd.Parameters.AddWithValue("@Id_caja", idcaja);
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
        //-------------------SALIDA PRODUCTO----------
        public bool insertar_Devuelta_producto(Lproductos parametros, int iddetallecompra, int idcompra)
        {
            try
            {
                Obtener_datos.mostrar_inicio_De_sesion(ref idusuario);
                Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("devuelta_cant_producto_proveedor", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idproducto", parametros.Id_Producto1);
                cmd.Parameters.AddWithValue("@cant", parametros.Stock);
                cmd.Parameters.AddWithValue("@iddetalle", iddetallecompra);
                cmd.Parameters.AddWithValue("@idcompra", idcompra);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                return true;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        //Cobros 
        public static void Insertar_Credito_Venta(Lcredito_venta L)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("Insertar_Credito_Venta", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idcliente", L.IdCliente);
                cmd.Parameters.AddWithValue("@No_venta", L.No_Venta);
                cmd.Parameters.AddWithValue("@Fecha", L.Fecha);
                cmd.Parameters.AddWithValue("@Efectivo", L.Efectivo);
                cmd.Parameters.AddWithValue("@Tarjeta", L.Tarjeta);
                cmd.Parameters.AddWithValue("@Credito", L.Credito);
                cmd.Parameters.AddWithValue("@Abono", L.Abono);
                cmd.Parameters.AddWithValue("@Resta", L.Resta);
                cmd.Parameters.AddWithValue("@Estado_pago", L.Estado_pago);
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
        //Codigos De Barrras Imprimir
        public static void InsertarSolicitudCodigoBarras(ref int Idsolicitud)
        {
            try
            {
                int idcaja = 0;
                Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
                var fechaactual = DateTime.Now.Date;
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("InsertarSolicitudCodigoBarras", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fecha", fechaactual);
                cmd.Parameters.AddWithValue("@idcaja", idcaja);
                Idsolicitud = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (Exception ex)
            {
                Idsolicitud = 0;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static bool InsertarDetalleCodigoBarras(int idsolicitud, int idproducto)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("InsertarDetalleCodigoBarras", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idsolicitud", idsolicitud);
                cmd.Parameters.AddWithValue("@idproducto", idproducto);
                cmd.Parameters.AddWithValue("@cantidad", 1);
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
        //Ventas Anuladas
        public static bool InsertarVentasAnuladas(DateTime fecha, string comprobante)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("InsertarVentasAnuladas", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fecha", fecha.ToString("yyyyMMdd"));
                cmd.Parameters.AddWithValue("@NumeroComprobante", comprobante);
                cmd.Parameters.AddWithValue("@concepto", "06 Devolución de Productos");
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
        //Usuarios
        public static bool InsertarInicioSesionVendedor(int idusuario,string rol)
        {
            string serialpc = "";
            Bases.Obtener_serialPc(ref serialpc);
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("InsertarInicioSesionVendedor", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idusuario", idusuario);
                cmd.Parameters.AddWithValue("@Serial", serialpc);
                cmd.Parameters.AddWithValue("@rol",rol);
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
    }
}
