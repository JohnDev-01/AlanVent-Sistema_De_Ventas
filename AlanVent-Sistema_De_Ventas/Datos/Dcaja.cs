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
    class Dcaja
    {
        string serialPC;
        public void ObtenerIdcaja(ref int idcaja)
        {
            try
            {
                Bases.Obtener_serialPc(ref serialPC);
                ConexionMaestra.abrir();
                SqlCommand com = new SqlCommand("mostrar_cajas_por_Serial_de_DiscoDuro", ConexionMaestra.conectar);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Serial", serialPC);
                idcaja = Convert.ToInt32(com.ExecuteScalar());



            }
            catch (Exception ex)
            {
                idcaja = 0;
                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                ConexionMaestra.cerrar();

            }
        }
    }
}
