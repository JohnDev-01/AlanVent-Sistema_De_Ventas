using AlanVent_Sistema_De_Ventas.Logic;
using Microsoft.Win32;
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
    class Eliminar_datos
    {
        public bool eliminar_venta(Lventas parametros)
        {
            try
            {
                DataAccess.ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("eliminar_venta", ConexionMaestra.conectar);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idventa", parametros.idventa);
                cmd.ExecuteNonQuery();
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static void EliminarInicioSesionUsuariosVendedor()
        {
            string serialpc = "";
            Bases.Obtener_serialPc(ref serialpc);
            try
            {
                DataAccess.ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("EliminarInicioSesionUsuarios", ConexionMaestra.conectar);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@serialpc", serialpc);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public static void EliminarVentasSeQuedaronAbiertas()
        {
            try
            {
                DataAccess.ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("EliminarVentasSeQuedaronAbiertas", ConexionMaestra.conectar);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }

        public static void eliminar_gastos(int idgasto)
        {
            try
            {
                DataAccess.ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("eliminar_gasto", ConexionMaestra.conectar);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idgasto", idgasto);
                cmd.ExecuteNonQuery();
                ConexionMaestra.cerrar();
            }
            catch (Exception)
            {

            }
        }
        public static void eliminar_ingreso(int idingreso)
        {
            try
            {
                DataAccess.ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("eliminar_ingresos", ConexionMaestra.conectar);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idingreso", idingreso);
                cmd.ExecuteNonQuery();
                ConexionMaestra.cerrar();
            }
            catch (Exception)
            {

            }
        }
        public bool Eliminar_proveedores(Lproveedores p)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("eliminar_Proveedores", ConexionMaestra.conectar);
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
        public static bool EliminarPagoProveedor(int idpago, int idproveedor)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("EliminarPagoProveedor", ConexionMaestra.conectar);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProveedor", idproveedor);
                cmd.Parameters.AddWithValue("@idpago", idpago);
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
        public bool eliminar_clientes(Lclientes p)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("eliminar_clientes", ConexionMaestra.conectar);
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
        public bool eliminarControlCobro(Lcontrolcobros parametros)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("eliminarControlCobro", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcontrol", parametros.IdcontrolCobro);
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
        public static bool EliminarTodoDetalleCodigoBarras(int Idsolicitud)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("EliminarTodoDetalleCodigoBarras", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdSolicitud", Idsolicitud);
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
        public static void Eliminar_Registro_deuda_credito(int IdRegistro)
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("Eliminar_Registro_deuda_credito", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idRegistro", IdRegistro);
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
    }
}
