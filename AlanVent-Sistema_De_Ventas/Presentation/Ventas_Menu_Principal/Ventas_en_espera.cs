using AlanVent_Sistema_De_Ventas.DataAccess;
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

namespace AlanVent_Sistema_De_Ventas.Presentation.Ventas_Menu_Principal
{
    public partial class Ventas_en_espera : Form
    {
        public Ventas_en_espera()
        {
            InitializeComponent();
        }
        int idcaja;
        int idventa;

        private void Ventas_en_espera_Load(object sender, EventArgs e)
        {
            mostrar_ventas_en_espera_con_fecha_y_monto();
            DataAccess.Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
        }
        private void mostrar_ventas_en_espera_con_fecha_y_monto()
        {
            try
            {
                DataTable dt = new DataTable();
                Obtener_datos.mostrar_ventas_en_espera_con_fecha_y_monto(ref dt);
                datalistado_ventas_en_espera.DataSource = dt;
                datalistado_ventas_en_espera.Columns[1].Visible = false;
                datalistado_ventas_en_espera.Columns[4].Visible = false;
                Bases.Multilinea(ref datalistado_ventas_en_espera);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void datalistado_ventas_en_espera_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               idventa = Convert.ToInt32(datalistado_ventas_en_espera.SelectedCells[1].Value.ToString());
                lblfechadeventa.Text = datalistado_ventas_en_espera.SelectedCells[3].Value.ToString();
                mostrar_detalle_venta();
            }
            catch (Exception)
            {

               
            }
            
        }
        private void mostrar_detalle_venta()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrar_productos_agregados_a_ventas_en_espera(ref dt, ref idventa);
            datalistadodetalledeventasarestaurar.DataSource = dt;
            Bases.Multilinea(ref datalistadodetalledeventasarestaurar);
        }
        private void btneliminar_Click(object sender, EventArgs e)
        {
            Lventas obj = new Lventas();
            obj.idventa = idventa;
            Eliminar_datos obj2 = new Eliminar_datos();
            obj2.eliminar_venta(obj);

            idventa = 0;
            mostrar_ventas_en_espera_con_fecha_y_monto();
            mostrar_detalle_venta();
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            Ventas_Menu_Principal.frm_VENTAS_MENU_PRINCIPAL.Idventa = idventa;
            Editar_datos.cambio_de_Caja(idcaja, idventa);
            Dispose();
        }
        private void buscarventa_espera()
        {
            try
            {
                ConexionMaestra.abrir();
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter("buscar_ventas_en_espera_con_fecha_y_monto", ConexionMaestra.conectar);
                dta.SelectCommand.CommandType = CommandType.StoredProcedure;
                dta.SelectCommand.Parameters.AddWithValue("@letra", txtbusca.Text);
                dta.Fill(dt);
                datalistado_ventas_en_espera.DataSource = dt;
                ConexionMaestra.cerrar();
                Bases.Multilinea(ref datalistado_ventas_en_espera);
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        private void txtbusca_TextChanged(object sender, EventArgs e)
        {
            if (txtbusca.Text == "")
            {
                lblBusqueda.Visible = true;
            }
            else
            {
                lblBusqueda.Visible = false;
            }
            buscarventa_espera();
        }

        private void ToolStripMenuItem5_Click(object sender, EventArgs e)
        {

        }

        private void lblBusqueda_Click(object sender, EventArgs e)
        {
            txtbusca.Focus();
        }
    }
}
