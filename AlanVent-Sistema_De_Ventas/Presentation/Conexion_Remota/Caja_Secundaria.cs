using AlanVent_Sistema_De_Ventas.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.Conexion_Remota
{
    public partial class Caja_Secundaria : Form
    {
        public Caja_Secundaria()
        {
            InitializeComponent();
        }
        string SerialPc;
        public static string lblconexion;
        private void Caja_Secundaria_Load(object sender, EventArgs e)
        {
            Bases.Obtener_serialPc(ref SerialPc);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtcaja.Text))
            {
                Ingresar_caja();
            }
            else
            {
                MessageBox.Show("Datos incompletos");
            }
        }
        private void Ingresar_caja()
        {
            try
            {
                SqlConnection conexionExpress = new SqlConnection(lblconexion);
                conexionExpress.Open();
                SqlCommand com = new SqlCommand("Insertar_caja", conexionExpress);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@descripcion", txtcaja.Text);
                com.Parameters.AddWithValue("@Tema", "Redentor");
                com.Parameters.AddWithValue("@Serial_PC", SerialPc);
                com.Parameters.AddWithValue("@Impresora_Ticket", "Ninguna");
                com.Parameters.AddWithValue("@Impresora_A4", "Ninguna");
                com.Parameters.AddWithValue("@Tipo", "SECUNDARIA");
                com.ExecuteNonQuery();
                conexionExpress.Close();
                insertar_inicio_De_sesion();
                MessageBox.Show("Listo ya Tienes Esta CAJA Habilitada", "Caja Registrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void insertar_inicio_De_sesion()
        {
            try
            {
                SqlConnection conexionExpress = new SqlConnection(lblconexion);
                conexionExpress.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("insertar_inicio_seccion", conexionExpress);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_serial_pc", SerialPc);
                cmd.ExecuteNonQuery();
                conexionExpress.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
