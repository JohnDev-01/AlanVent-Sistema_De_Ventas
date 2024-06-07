using AlanVent_Sistema_De_Ventas.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanVent_Sistema_De_Ventas.Presentation.Cotizaciones.Logica
{
    public class ObtenerDatos
    {
        public static string Mostrar_Tipo_Busqueda()
        {
            var Tipo_de_Busqueda = "";
            ConexionMaestra.abrir() ;
            var com = new SqlCommand("select Modo_de_busqueda from EMPRESA", ConexionMaestra.conectar);
            try
            {
                Tipo_de_Busqueda = Convert.ToString(com.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
            return Tipo_de_Busqueda;
        }
        public static void ListarProductosBuscador(ref DataTable dt,string buscando)
        {
            try
            {
                ConexionMaestra.abrir();
                var command = new SqlDataAdapter("BUSCAR_PRODUCTOS_oka", DataAccess.ConexionMaestra.conectar);
                command.SelectCommand.CommandType = CommandType.StoredProcedure;
                command.SelectCommand.Parameters.AddWithValue("@letra", buscando);
                command.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }

            
        }
        public static void ListarProductosBuscadorCotizacion(ref DataTable dt,string buscando)
        {
            try
            {
                ConexionMaestra.abrir();
                var command = new SqlDataAdapter("BUSCAR_PRODUCTOS_oka_Cotizacion", DataAccess.ConexionMaestra.conectar);
                command.SelectCommand.CommandType = CommandType.StoredProcedure;
                command.SelectCommand.Parameters.AddWithValue("@letra", buscando);
                command.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }

            
        }
        public static void MostrarCotizaciones(ref DataTable dt,string buscando)
        {
            try
            {
                ConexionMaestra.abrir();
                var command = new SqlDataAdapter("MostrarCotizaciones", DataAccess.ConexionMaestra.conectar);
                command.SelectCommand.CommandType = CommandType.StoredProcedure;
                command.SelectCommand.Parameters.AddWithValue("@cliente", buscando);
                command.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }

            
        } 
        public static void MostrarInformacionCliente(ref DataTable dt,int Idcliente )
        {
            try
            {
                ConexionMaestra.abrir();
                var command = new SqlDataAdapter("MostrarInformacionCliente", DataAccess.ConexionMaestra.conectar);
                command.SelectCommand.CommandType = CommandType.StoredProcedure;
                command.SelectCommand.Parameters.AddWithValue("@Idcliente", Idcliente);
                command.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }

            
        }
        public static void MostrarCotizacionReporte(ref DataTable dt,int Idcotizacion )
        {
            try
            {
                ConexionMaestra.abrir();
                var command = new SqlDataAdapter("MostrarCotizacionReporte", DataAccess.ConexionMaestra.conectar);
                command.SelectCommand.CommandType = CommandType.StoredProcedure;
                command.SelectCommand.Parameters.AddWithValue("@idcotizacion", Idcotizacion);
                command.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }

            
        }
        public static void MostrarBalanceCotizacion(ref DataTable dt,int Idcotizacion )
        {
            try
            {
                ConexionMaestra.abrir();
                var command = new SqlDataAdapter("MostrarBalanceCotizacion", DataAccess.ConexionMaestra.conectar);
                command.SelectCommand.CommandType = CommandType.StoredProcedure;
                command.SelectCommand.Parameters.AddWithValue("@Idcotizacion", Idcotizacion);
                command.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }

            
        }
        public static int ObtenerIdCotizacion()
        {
            int IdCotizacion = 0;

            try
            {
                string serial = "";
                string estado = "";
                Logic.Bases.Obtener_serialPc(ref serial);
                ConexionMaestra.abrir();
                var command = new SqlCommand("MostrarIdcotizacion", DataAccess.ConexionMaestra.conectar);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@serial", serial);
                IdCotizacion = Convert.ToInt32(command.ExecuteScalar().ToString());
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }

            return IdCotizacion;
        }
        public static bool ValidarExisteCotizacion()
        {
            bool estadoValidacion = false;

            try
            {
                string serial = "";
                string estado = "";
                Logic.Bases.Obtener_serialPc(ref serial);
                ConexionMaestra.abrir();
                var command = new SqlCommand("ValidarExisteCotizacion", DataAccess.ConexionMaestra.conectar);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Serial", serial);
                var value = command.ExecuteScalar().ToString();
                ConexionMaestra.cerrar();

                if (value == null)
                {
                    estadoValidacion = false;
                }
                else
                {
                    estado = value;
                } 

                if (estado == "ABIERTA")
                {
                    estadoValidacion =  true;
                }
                else if (estado == "CERRADA")
                {
                    estadoValidacion =  false;
                }
            }
            catch (Exception ex)
            {

            }

            return estadoValidacion;
        }
        public static void ListarDetalleCotizacion (ref DataTable dt,int Idcotizacion)
        {
            try
            {
                ConexionMaestra.abrir();
                var command = new SqlDataAdapter("MostraDetalleCotizacion", DataAccess.ConexionMaestra.conectar);
                command.SelectCommand.CommandType = CommandType.StoredProcedure;
                command.SelectCommand.Parameters.AddWithValue("@IdCotizacion", Idcotizacion);
                command.Fill(dt);
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
