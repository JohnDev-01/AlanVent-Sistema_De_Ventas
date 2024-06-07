using AlanVent_Sistema_De_Ventas.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanVent_Sistema_De_Ventas.Logic
{
    public class SerializacionComprobantes
    {
        public static string GenerarSerializacionGastosMenores()
        {
            var models = new LSerializacionNCF();
            Obtener_datos.ObtenerInfoSerializacionGastosMenores(ref models);


            string serializacionCompleta;

            long cantidadSinCeros = models.CantidadSinCeros + 1;

            int longitudCantidad = cantidadSinCeros.ToString().Length;
            int Cantidadceros = models.CantidadCeros - longitudCantidad;
            string cerosAgregar = "";
            for (int i = 0; i < Cantidadceros; i++)
            {
                cerosAgregar += "0";
            }
            serializacionCompleta = models.Serie + models.TipoComprobante + cerosAgregar + cantidadSinCeros;

            Editar_datos.ActualizarSerializacionGastosMenores(cantidadSinCeros);

            return serializacionCompleta;
        }
        public static string GenerarSerializacionCreditoFiscal()
        {
            var models = new LSerializacionNCF();
            Obtener_datos.ObtenerInfoSerializacionCreditoFiscal(ref models);


            string serializacionCompleta;

            long cantidadSinCeros = models.CantidadSinCeros + 1;

            int longitudCantidad = cantidadSinCeros.ToString().Length;
            int Cantidadceros = models.CantidadCeros - longitudCantidad;
            string cerosAgregar = "";
            for (int i = 0; i < Cantidadceros; i++)
            {
                cerosAgregar += "0";
            }
            serializacionCompleta = models.Serie + models.TipoComprobante + cerosAgregar + cantidadSinCeros;

            Editar_datos.ActualizarSerializacionCreditoFiscal(cantidadSinCeros);

            return serializacionCompleta;
        }
        public static string GenerarSerializacioProvInformal()
        {
            var models = new LSerializacionNCF();
            Obtener_datos.ObtenerInfoSerializacionProvInformal(ref models);


            string serializacionCompleta;

            long cantidadSinCeros = models.CantidadSinCeros + 1;

            int longitudCantidad = cantidadSinCeros.ToString().Length;
            int Cantidadceros = models.CantidadCeros - longitudCantidad;
            string cerosAgregar = "";
            for (int i = 0; i < Cantidadceros; i++)
            {
                cerosAgregar += "0";
            }
            serializacionCompleta = models.Serie + models.TipoComprobante + cerosAgregar + cantidadSinCeros;

            Editar_datos.ActualizarSerializacionProvInformal(cantidadSinCeros);

            return serializacionCompleta;
        }
        public static string GenerarSerializacionIngresosConsumo()
        {
            var models = new LSerializacionNCF();
            Obtener_datos.ObtenerInfoSerializacionFacturaConsumo(ref models);

            string serializacionCompleta;

            long cantidadSinCeros = models.CantidadSinCeros + 1;

            int longitudCantidad = cantidadSinCeros.ToString().Length;
            int Cantidadceros = models.CantidadCeros - longitudCantidad;
            string cerosAgregar = "";
            for (int i = 0; i < Cantidadceros; i++)
            {
                cerosAgregar += "0";
            }
            serializacionCompleta = models.Serie + models.TipoComprobante + cerosAgregar + cantidadSinCeros;

            Editar_datos.ActualizarSerializacionFacturaConsumo(cantidadSinCeros);

            return serializacionCompleta;
        }
       
    }
}
