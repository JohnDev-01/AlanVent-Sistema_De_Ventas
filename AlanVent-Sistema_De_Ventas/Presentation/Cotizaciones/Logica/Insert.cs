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
    public class Insert
    {
        public static void InsertarCotizacion(Mcotizacion models, ref int IdCotizacion)
        {
            try
            {
                string serial = "";
                Logic.Bases.Obtener_serialPc(ref serial);
                ConexionMaestra.abrir();
                var sql = new SqlCommand("InsertarCotizacion", ConexionMaestra.conectar);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@Idcliente", models.Idcliente);
                sql.Parameters.AddWithValue("@Fecha", models.Fecha);
                sql.Parameters.AddWithValue("@Impuestos", models.Impuestos);
                sql.Parameters.AddWithValue("@subtotal", models.SubTotal);
                sql.Parameters.AddWithValue("@total", models.Total);
                sql.Parameters.AddWithValue("@SerialPc", serial);
                sql.Parameters.AddWithValue("@Descuento", models.Descuento);
                IdCotizacion = Convert.ToInt32(sql.ExecuteScalar());
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
        public static void InsertarDetalleCotizacion(MdetalleCotizacion models)
        {
            try
            {
                
                ConexionMaestra.abrir();
                var sql = new SqlCommand("InsertarDetalleCotizacion", ConexionMaestra.conectar)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sql.Parameters.AddWithValue("@Idventa", models.Idventa);
                sql.Parameters.AddWithValue("@Idproducto", models.Idproducto);
                sql.Parameters.AddWithValue("@PrecioUnitario", models.PrecioUnitario);
                sql.Parameters.AddWithValue("@Impuesto", models.Impuesto);
                sql.Parameters.AddWithValue("@Descuento", models.Descuento);
                sql.Parameters.AddWithValue("@Cantidad", models.Cantidad);
                sql.ExecuteNonQuery();
                ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }


        /*Pues Tengo quehacer que cuando se produzca el evento de cierre deel formulario cierren todas 
         * las cotizaciones en existencia y crear un modulo de restaurar cotizacion
         * *****************************
         * 
         * arreglar la visualizacion de los botones
         * 
         * crear apartado de impuestos, es decir una columna en la tabla que muestre el itbis 
         * 
         * funcionalidad de aumentar cantidad y disminuir
         * 
         * crear funcion para sumar todo el importe
         * 
         * crear funcion de descuento
         * 
         * Crear modulo de reporte o ticket para imprimir
         * 
         * modulo de visualizar todas las cotizaciones y tener la funcionalidad de restaurar
         * 
         * */
    }
}
