using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Presentation.Cotizaciones.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanVent_Sistema_De_Ventas.Presentation.Cotizaciones.Logica
{
    public class Update
    {
        public static void CerrarEstadoCotizaciones()
        {
            try
            {
                string serial = "";
                Logic.Bases.Obtener_serialPc(ref serial);

                ConexionMaestra.abrir();
                var sql = new SqlCommand("CerraCotizacionesAbiertas", ConexionMaestra.conectar);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@Serial", serial);
                sql.ExecuteNonQuery();
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
        public static void SumarProductoDetalleCotizacion(int iddetalle, double impuesto)
        {
            try
            {


                ConexionMaestra.abrir();
                var sql = new SqlCommand("SumarProductoDetalleCotizacion", ConexionMaestra.conectar);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@Iddetallecotizacion", iddetalle);
                sql.Parameters.AddWithValue("@impuesto", impuesto);
                sql.ExecuteNonQuery();
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
        public static void RestarProductoDetalleCotizacion(int iddetalle, double impuesto)
        {
            try
            {
                ConexionMaestra.abrir();
                var sql = new SqlCommand("RestarProductoDetalleCotizacion", ConexionMaestra.conectar);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@Iddetallecotizacion", iddetalle);
                sql.Parameters.AddWithValue("@impuesto", impuesto);
                sql.ExecuteNonQuery();
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
        public static void AplicarCantidadCotizacion(int iddetalle, double cantidad, double Impuesto)
        {
            try
            {
                ConexionMaestra.abrir();
                var sql = new SqlCommand("AplicarCantidadCotizacion", ConexionMaestra.conectar);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@iddetalle", iddetalle);
                sql.Parameters.AddWithValue("@cant", cantidad);
                sql.Parameters.AddWithValue("@Impuesto", Impuesto);
                sql.ExecuteNonQuery();
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
        public static void AplicarDescuentoSeleccionado(Descuento models)
        {
            try
            {
                ConexionMaestra.abrir();
                var sql = new SqlCommand("AplicarDescuentoCotizacionSeleccionado", ConexionMaestra.conectar);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@idproducto", models.idproducto);
                sql.Parameters.AddWithValue("@iddetalle", models.iddetalle);
                sql.Parameters.AddWithValue("@cantidad", models.Cantidad);
                sql.ExecuteNonQuery();
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
        public static void ActualizarImpuestoCotizacion(int iddetalle, double impuesto)
        {
            try
            {
                ConexionMaestra.abrir();
                var sql = new SqlCommand("ActualizarImpuestoCotizacion", ConexionMaestra.conectar);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@iddetalle", iddetalle);
                sql.Parameters.AddWithValue("@impuesto", impuesto);
                sql.ExecuteNonQuery();
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
        public static void ActualizarSumaCotizacion(Mcotizacion models)
        {
            try
            {
                ConexionMaestra.abrir();
                var sql = new SqlCommand("ActualizarSumaCotizacion", ConexionMaestra.conectar);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@idcotizacion", models.Idcotizacion);
                sql.Parameters.AddWithValue("@Impuestos", models.Impuestos);
                sql.Parameters.AddWithValue("@Subtotal", models.SubTotal);
                sql.Parameters.AddWithValue("@Total", models.Total);
                sql.Parameters.AddWithValue("@Descuento", models.Descuento);
                sql.ExecuteNonQuery();
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
