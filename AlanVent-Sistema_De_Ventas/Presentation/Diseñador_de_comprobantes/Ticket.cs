using AlanVent_Sistema_De_Ventas.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.Diseñador_de_comprobantes
{
    public partial class Ticket : Form
    {
        public Ticket()
        {
            InitializeComponent();
        }
        string txttipo;
        string FormatoSeleccionado;
        private void Ticket_Load(object sender, EventArgs e)
        {
            Mostrar_formato_ticket();
            obtener_datos();
            LimpiarDatos();
        }
        private void LimpiarDatos()
        {
            panelTicket.Visible = false;
            panelFactura.Visible = false;
        }

        private void Mostrar_formato_ticket()
        {
            try
            {


                DataTable dt = new DataTable();
                SqlDataAdapter da;
                DataAccess.ConexionMaestra.conectar.Open();
                da = new SqlDataAdapter("Mostrar_formato_ticket", DataAccess.ConexionMaestra.conectar);
                da.Fill(dt);
                datalistado_tickets.DataSource = dt;
                DataAccess.ConexionMaestra.conectar.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        private void obtener_datos()
        {
            try
            {
                txttipo = datalistado_tickets.SelectedCells[13].Value.ToString();
                
                ICONO.BackgroundImage = null;
                byte[] b = (Byte[])datalistado_tickets.SelectedCells[1].Value;
                MemoryStream ms = new MemoryStream(b);
                ICONO.Image = Image.FromStream(ms);

                txtempresaTICKET.Text = datalistado_tickets.SelectedCells[2].Value.ToString();
                txtEmpresa_RUC.Text = datalistado_tickets.SelectedCells[5].Value.ToString();
                txtDireccion.Text = datalistado_tickets.SelectedCells[6].Value.ToString();
                txtProvincia_departamento.Text = datalistado_tickets.SelectedCells[7].Value.ToString();
                txtMoneda_String.Text = datalistado_tickets.SelectedCells[8].Value.ToString();
                txtAgradecimiento.Text = datalistado_tickets.SelectedCells[9].Value.ToString();
                txtpagina_o_facebook.Text = datalistado_tickets.SelectedCells[10].Value.ToString();
                TXTANUNCIO.Text = datalistado_tickets.SelectedCells[11].Value.ToString();
               
                txtRedSocial.Text = datalistado_tickets.SelectedCells[14].Value.ToString();
                if (txtRedSocial.Text== "NO_ACEPTA_CODIGOQR")
                {
                    checkCodigoBarra.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void btnTicket_Click(object sender, EventArgs e)
        {
            MostrarPanelTicket();
            FormatoSeleccionado = "TICKET";
        }
        private void MostrarPanelTicket()
        {
            //Botones Colores
            btnFactura.BackgroundImage = null;
            btnTicket.BackgroundImage = Properties.Resources.azul;

            //Paneles
            panelTicket.Visible = true;
            panelTicket.Dock = DockStyle.Fill;
            panelDTicket.Location = new Point((panelTicket.Width - panelDTicket.Width) / 2, (panelTicket.Height - panelDTicket.Height) / 2);
            panelFactura.Visible = false;
        }
        private void MostrarPanelFactura()
        {
            //Botones Colores
            btnFactura.BackgroundImage = Properties.Resources.azul;
            btnTicket.BackgroundImage = null;

            //Paneles
            panelTicket.Visible = false;
            panelFactura.Visible = true;
            panelFactura.Dock = DockStyle.Fill;
            panelDFactura.Size = new Size(905,600);
            panelDFactura.Location = new Point((panelFactura.Width - panelDFactura.Width) / 2, (panelFactura.Height - panelDFactura.Height) / 2);

            string notas="";
            Obtener_datos.MostrarNotasFacturadiseno(ref notas);
            txtNotas.Text = notas;

        }
        private void btnFacturaBoleta_Click(object sender, EventArgs e)
        {
            MostrarPanelFactura();
            FormatoSeleccionado = "FACTURA";
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "";
            openFileDialog1.Filter = "Imagenes|*.jpg;*.png";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.Title = "Cargador de imagenes de AlanVent";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ICONO.BackgroundImage = null;
                ICONO.Image = new Bitmap(openFileDialog1.FileName);
            }
        }
        private void EditarTicket()
        {
            try
            {
                DataAccess.ConexionMaestra.conectar.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("editar_FORMATO_TICKET", DataAccess.ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Identificador_fiscal", txtEmpresa_RUC.Text);
                cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                cmd.Parameters.AddWithValue("@Provincia_Departamento_Pais", txtProvincia_departamento.Text);
                cmd.Parameters.AddWithValue("@Nombre_de_Moneda", txtMoneda_String.Text);
                cmd.Parameters.AddWithValue("@Agradecimiento", txtAgradecimiento.Text);
                cmd.Parameters.AddWithValue("@pagina_Web_Facebook", txtpagina_o_facebook.Text);
                cmd.Parameters.AddWithValue("@Anuncio", TXTANUNCIO.Text);
                if (txttipo == "Ticket No Fiscal")
                {
                    cmd.Parameters.AddWithValue("@Datos_fiscales_de_autorizacion", "-");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Datos_fiscales_de_autorizacion", "-");
                }
                cmd.Parameters.AddWithValue("@Por_defecto", txttipo);
                cmd.Parameters.AddWithValue("@Nombre_Empresa", txtempresaTICKET.Text);
                cmd.Parameters.AddWithValue("@linkred", txtRedSocial.Text);
                MemoryStream ms = new MemoryStream();
                ICONO.Image.Save(ms, ICONO.Image.RawFormat);
                cmd.Parameters.AddWithValue("@Logo", ms.GetBuffer());
                int resul = cmd.ExecuteNonQuery();
                DataAccess.ConexionMaestra.conectar.Close();
                if (resul > 0)
                {
                    MessageBox.Show("Los datos se han actualizado correctamente", "Actualizando datos", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.StackTrace);
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (FormatoSeleccionado == "TICKET")
            {
                EditarTicket();
            }
            else
            {
                Editar_datos.EditarFormatoFactura(txtNotas.Text);
            }
            LimpiarDatos();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelDTicket_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label25_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label32_Click(object sender, EventArgs e)
        {

        }

        private void txtProvincia_departamento_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkCodigoBarra_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCodigoBarra.Checked == true)
            {
                txtRedSocial.Visible = false;
                txtRedSocial.Text = "NO_ACEPTA_CODIGOQR";
            }
            else
            {
                txtRedSocial.Visible = true;
                txtRedSocial.Text = "LINK PARA RED SOCIAL";
            }
        }
    }
}
