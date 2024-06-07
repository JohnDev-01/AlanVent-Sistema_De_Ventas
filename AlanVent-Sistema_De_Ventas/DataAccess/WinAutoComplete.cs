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
    public static class WinAutoComplete
    {
        public static DataTable LoadDataTable()
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = DataAccess.ConexionMaestra.conexion;
            con.Open();

            da = new SqlDataAdapter("SELECT TOP 100 Descripcion FROM Productos1", con);

            da.Fill(dt);


            return dt;

        }
        public static AutoCompleteStringCollection LoadAutoComplete()
        {
            DataTable dt = LoadDataTable();

            AutoCompleteStringCollection stringCol = new AutoCompleteStringCollection();

            foreach (DataRow row in dt.Rows)
            {
                stringCol.Add(Convert.ToString(row["Descripcion"]));
            }

            return stringCol;
        }
    }
}
