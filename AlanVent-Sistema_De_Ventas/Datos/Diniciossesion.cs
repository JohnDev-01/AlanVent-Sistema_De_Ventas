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
    public class Diniciossesion
    {
        string serialpc;
        public void mostrar_inicio_De_sesion(ref int idusuario)
        {
            Bases.Obtener_serialPc(ref serialpc);
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("mostrar_inicio_De_sesion", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_serial", serialpc);
                idusuario = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                idusuario = 0;
                MessageBox.Show(ex.StackTrace);

            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
    }
}
