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
    class Dproductos
    {
        public object CONEXIONMAESTRA { get; private set; }

        public void BuscarProductosCodigo(ref DataTable dt, string codigo)
        {

            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("BUSCAR_PRODUCTOS_Codigo", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", codigo);
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
        public void buscarProductos(ref DataTable dt, string buscador)
        {

            try
            {
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("BUSCAR_PRODUCTOS_oka", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", buscador);
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
