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
    public class Dkardex
    {
        int idusuario;
        int idcaja;
        private void MostrarInicioSesion()
        {
            var funcion = new Diniciossesion();
            funcion.mostrar_inicio_De_sesion(ref idusuario);
        }
        private void MostrarIdcajaserial()
        {
            var funcion = new Dcaja();
            funcion.ObtenerIdcaja(ref idcaja);
        }
        public bool insertar_KARDEX_Entrada(LKardex parametros)
        {
            try
            {
                MostrarInicioSesion();
                MostrarIdcajaserial();
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
                MessageBox.Show(ex.Message );
                return false;
            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
    }
}
