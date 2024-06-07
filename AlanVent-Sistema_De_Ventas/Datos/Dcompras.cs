using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Datos
{
    public class Dcompras
    {
        int Idcaja;
        public bool Insertar_Compras(Ldetallecompra parametros)
        {
            try
            {
                var funcion = new Dcaja();
                funcion.ObtenerIdcaja(ref Idcaja);
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("insertar_Compras", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fechacompra", DateTime.Now);
                cmd.Parameters.AddWithValue("@Cantidad", parametros.Cantidad);
                cmd.Parameters.AddWithValue("@Costo", parametros.Costo);
                cmd.Parameters.AddWithValue("@Moneda", parametros.Moneda);
                cmd.Parameters.AddWithValue("@IdProducto", parametros.IdProducto);
                cmd.Parameters.AddWithValue("@Descripcion", parametros.Descripcion);
                cmd.Parameters.AddWithValue("@Estado", parametros.Estado);
                cmd.Parameters.AddWithValue("@Idcaja", Idcaja);
                cmd.Parameters.AddWithValue("@NumeroComprobante", "-");
                cmd.Parameters.AddWithValue("@ImpuestoRetenido", 0);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message );
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public void MostrarUltimoIdcompra(ref int idcompra)
        {

            try
            {
                var funcion = new Dcaja();
                funcion.ObtenerIdcaja(ref Idcaja);
                ConexionMaestra.abrir();
                SqlCommand com = new SqlCommand("MostrarUltimoIdcompra", ConexionMaestra.conectar);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Idcaja", Idcaja);
                idcompra = Convert.ToInt32(com.ExecuteScalar());
            }
            catch (Exception ex)
            {
                idcompra = 0;
                MessageBox.Show(ex.StackTrace);

            }
            finally
            {
                ConexionMaestra.cerrar();

            }
        }
        public bool eliminarComprasvacias()
        {
            try
            {
                var funcion = new Dcaja();
                funcion.ObtenerIdcaja(ref Idcaja);
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("eliminarComprasvacias", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idcaja", Idcaja);

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
        public static void ActualizarComprobanteCompras(ref string ComprobanteActualizado)
        {
            try
            {
                var dt = new DataTable();
                ConexionMaestra.abrir();
                var cmd = new SqlDataAdapter("ActualizarComprobanteCompras", ConexionMaestra.conectar);
                cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmd.Fill(dt);


                foreach (DataRow item in dt.Rows)
                {
                    ComprobanteActualizado = item["Serializacion"].ToString();
                }

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
        public bool confirmarCompra(Lcompras parametros)
        {
            try
            {
                var funcion = new Dcaja();
                funcion.ObtenerIdcaja(ref Idcaja);
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("confirmarCompra", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idcompra", parametros.Idcompra);
                cmd.Parameters.AddWithValue("@Total", parametros.Total);
                cmd.Parameters.AddWithValue("@Idcaja", Idcaja);
                cmd.Parameters.AddWithValue("@Idproveedor", parametros.IdProveedor);
                cmd.Parameters.AddWithValue("@fechacompra", DateTime.Now);
                cmd.Parameters.AddWithValue("@Efectivo", parametros.Efectivo);
                cmd.Parameters.AddWithValue("@Credito", parametros.Credito);
                cmd.Parameters.AddWithValue("@TipoPago", parametros.TipoPago);
                cmd.Parameters.AddWithValue("@Impuestos", parametros.Impuestos);
                cmd.Parameters.AddWithValue("@subtotal", parametros.Subtotal);
                cmd.Parameters.AddWithValue("@modopago", parametros.Modopago);
                cmd.Parameters.AddWithValue("@comprobante", parametros.Comprobante);
                cmd.Parameters.AddWithValue("@serializacion", parametros.NumeroComprobante);
                cmd.Parameters.AddWithValue("@impuestoretenido", parametros.ImpuestoRetenido);
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
        public void buscarCompras(ref DataTable dt, string buscador)
        {

            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscarCompras", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@buscar", buscador);
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);

            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
    }
}
