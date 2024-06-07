using AlanVent_Sistema_De_Ventas.Logic;
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
    public class DLicencias
    {
        DateTime fechaFinal;
        DateTime fechaInicial;
        string estado;
        string serialPcLicencia;
        DateTime fechaSistema = DateTime.Now;
        string serialPC;
        public void ValidarLicencias(ref string resultado, ref string rfechafinal)
        {
            try
            {
                Bases.Obtener_serialPc(ref serialPC);
                DataTable dt = new DataTable();
                ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("select * from Marcan", ConexionMaestra.conectar);
                da.Fill(dt);
                foreach (DataRow rdr in dt.Rows)
                {
                    estado = Bases.Desencriptar(rdr["E"].ToString());
                    fechaFinal = Convert.ToDateTime(Bases.Desencriptar(rdr["F"].ToString()));
                    fechaInicial = Convert.ToDateTime(Bases.Desencriptar(rdr["FA"].ToString())).Date;
                    serialPcLicencia = rdr["S"].ToString();


                }
                if (estado == "VENCIDA")
                {
                    resultado = "VENCIDA";
                }
                else
                {
                    if (fechaFinal >= fechaSistema)
                    {
                        if (fechaInicial <= fechaSistema)
                        {

                            resultado = estado;
                            rfechafinal = fechaFinal.ToString("dd/MM/yyyy");

                        }
                        else
                        {
                            resultado = "VENCIDA";
                        }
                    }
                    else
                    {
                        resultado = "VENCIDA";
                    }
                }


            }
            catch (Exception ex)
            {


            }
            finally
            {
                ConexionMaestra.cerrar();
            }
        }
        public void EditarMarcanVencidas()
        {
            try
            {
                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("MarcanVencidas", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@E", Bases.Encriptar("VENCIDA"));
                cmd.ExecuteNonQuery();
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
