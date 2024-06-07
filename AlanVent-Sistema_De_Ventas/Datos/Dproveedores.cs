using AlanVent_Sistema_De_Ventas.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using AlanVent_Sistema_De_Ventas.Logic;

namespace AlanVent_Sistema_De_Ventas.Datos
{
	public class Dproveedores
	{
		public bool insertar_Proveedores(Lproveedores parametros)
		{
			try
			{
				ConexionMaestra.abrir();
				SqlCommand cmd = new SqlCommand("insertar_Proveedores", ConexionMaestra.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Nombre", parametros.Nombre);
				cmd.Parameters.AddWithValue("@Direccion", parametros.Direccion);
				cmd.Parameters.AddWithValue("@Celular", parametros.Celular);
				cmd.Parameters.AddWithValue("@Estado", "ACTIVO");
				cmd.Parameters.AddWithValue("@Saldo", 0);
				cmd.Parameters.AddWithValue("@TipoIdentificador", parametros.TipoIdentificacion);
				cmd.Parameters.AddWithValue("@Cedula", parametros.Cedula);
				cmd.Parameters.AddWithValue("@Rnc", parametros.Rnc);
				cmd.Parameters.AddWithValue("@Informal", parametros.Prov_Informal);
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
		public void buscar_Proveedores(ref DataTable dt, string buscador)
		{
			try
			{
				ConexionMaestra.abrir();
				SqlDataAdapter da = new SqlDataAdapter("buscar_proveedores", ConexionMaestra.conectar);
				da.SelectCommand.CommandType = CommandType.StoredProcedure;
				da.SelectCommand.Parameters.AddWithValue("@letra", buscador);
				da.Fill(dt);

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.StackTrace);
			}
			finally
			{
				ConexionMaestra.cerrar();
			}
		}

	}
}
