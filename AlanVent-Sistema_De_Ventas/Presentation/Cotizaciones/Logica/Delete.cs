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
    public class Delete
    {
        public static void EliminarProductoDetalleCotizacion(int iddetalle)
        {
            try
            {


                ConexionMaestra.abrir();
                var sql = new SqlCommand("EliminarProductoDetalleCotizacion", ConexionMaestra.conectar);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@idDetalle", iddetalle);
                sql.ExecuteNonQuery();
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
        public static void EliminarCotizacion(int Idcotizacion)
        {
            try
            {


                ConexionMaestra.abrir();
                var sql = new SqlCommand("EliminarCotizacion", ConexionMaestra.conectar);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@Idcotizacion", Idcotizacion);
                sql.ExecuteNonQuery();
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
