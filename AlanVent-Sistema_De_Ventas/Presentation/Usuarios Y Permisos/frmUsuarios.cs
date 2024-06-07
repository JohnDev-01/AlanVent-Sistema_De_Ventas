using AlanVent_Sistema_De_Ventas.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.Usuarios_Y_Permisos
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }
        private void RefrescarUsuarios()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();

                
                SqlDataAdapter sdata = new SqlDataAdapter("Mostrar_Usuario", cn);
                sdata.Fill(dt);
                datalistado.DataSource = dt;
                cn.Close();
                datalistado.Visible = true;
                datalistado.Columns[1].Visible = false;
                datalistado.Columns[5].Visible = false;
                datalistado.Columns[6].Visible = false;
                datalistado.Columns[7].Visible = false;
                datalistado.Columns[8].Visible = false;
                Logic.Bases.Multilinea(ref datalistado);
            }
            catch (Exception)
            {

               
            }
        }
        private void Cargar_estado_de_iconos()
        {
            try
            {
                foreach (DataGridViewRow row in datalistado.Rows)
                {

                    try
                    {

                        string Icono = Convert.ToString(row.Cells["Nombre_de_icono"].Value);

                        if (Icono == "1")
                        {
                            pictureBox3.Visible = false;
                        }
                        else if (Icono == "2")
                        {
                            pictureBox4.Visible = false;
                        }
                        else if (Icono == "3")
                        {
                            pictureBox5.Visible = false;
                        }
                        else if (Icono == "4")
                        {
                            pictureBox6.Visible = false;
                        }
                        else if (Icono == "5")
                        {
                            pictureBox7.Visible = false;
                        }
                        else if (Icono == "6")
                        {
                            pictureBox8.Visible = false;
                        }
                        else if (Icono == "7")
                        {
                            pictureBox9.Visible = false;
                        }
                        else if (Icono == "8")
                        {
                            pictureBox10.Visible = false;
                        }
                    }
                    catch (Exception ex)
                    {


                    }


                }
            }
            catch (Exception ex)
            {

            }
        }
        private void LblAnuncioIcono_Click(object sender, EventArgs e)
        {
            Cargar_estado_de_iconos();
            panelICONO.Visible = true;
            panelICONO.Dock = DockStyle.Fill;
        }
        private void CentrarControlesDeAgregar()
        {
            lblNombreYapellidos.Location = new Point(37, 60);
            txtnombre.Location = new Point(199, 57);
            LineaNombreYApellido.Location = new Point(199, 79);
        }
        private void PictureBox2_Click(object sender, EventArgs e)
        {

            //LblAnuncioIcono.Location = new Point(453, 62);
            LblAnuncioIcono.Visible = true;
            panelRegistros.Visible = true;
            panelRegistros.Dock = DockStyle.Fill;

            panelAgregarUsuarios.Dock = DockStyle.Fill;
            panelAgregarUsuarios.Visible = true;
            txtnombre.Text = "";
            txtlogin.Text = "";
            txtPassword.Text = "";
            txtcorreo.Text = "";
            btnGuardar.Visible = true;
            btnGuardar.Text = "Guardar";
            btnGuardarCambios.Visible = false;
            CentrarControlesDeAgregar();
            ICONO.BackgroundImage = null;
            lblCambiar.Visible = false;
            txtcorreo.BringToFront();

        }

        private void panelNuevo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            RefrescarUsuarios();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void VolverInicioUsuarios()
        {
            panelRegistros.Dock = DockStyle.None;
            panelRegistros.Visible = false;
            panelAgregarUsuarios.Dock = DockStyle.None;
            panelAgregarUsuarios.Visible = false;
            datalistado.Visible = true;
            datalistado.Dock = DockStyle.Fill;
            datalistado.Visible = true;
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            panelRegistros.Dock = DockStyle.None;
            panelRegistros.Visible = false;
            panelAgregarUsuarios.Dock = DockStyle.None;
            panelAgregarUsuarios.Visible = false;
            datalistado.Visible = true;
            datalistado.Dock = DockStyle.Fill;
            RefrescarUsuarios();
        }

        private void LblAnuncioIcono_Click_1(object sender, EventArgs e)
        {
            panelAgregarUsuarios.Visible = false;
            panelAgregarUsuarios.Dock = DockStyle.None;

            Cargar_estado_de_iconos();
            panelICONO.Visible = true;
            panelICONO.Dock = DockStyle.Fill;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ICONO.Image = pictureBox3.Image;
            lblnumeroIcono.Text = "1";
            LblAnuncioIcono.Visible = false;
            panelICONO.Visible = false;
            panelAgregarUsuarios.Visible = true;
            panelAgregarUsuarios.Dock = DockStyle.Fill;
            lblCambiar.Visible = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ICONO.Image = pictureBox4.Image;
            lblnumeroIcono.Text = "2";
            LblAnuncioIcono.Visible = false;
            panelICONO.Visible = false;
            panelAgregarUsuarios.Visible = true;
            panelAgregarUsuarios.Dock = DockStyle.Fill;
            lblCambiar.Visible = true;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            ICONO.Image = pictureBox5.Image;
            lblnumeroIcono.Text = "3";
            LblAnuncioIcono.Visible = false;
            panelICONO.Visible = false;
            panelAgregarUsuarios.Visible = true;
            panelAgregarUsuarios.Dock = DockStyle.Fill;
            lblCambiar.Visible = true;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            ICONO.Image = pictureBox6.Image;
            lblnumeroIcono.Text = "4";
            LblAnuncioIcono.Visible = false;
            panelICONO.Visible = false;
            panelAgregarUsuarios.Visible = true;
            panelAgregarUsuarios.Dock = DockStyle.Fill;
            lblCambiar.Visible = true;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            ICONO.Image = pictureBox7.Image;
            lblnumeroIcono.Text = "5";
            LblAnuncioIcono.Visible = false;
            panelICONO.Visible = false;
            panelAgregarUsuarios.Visible = true;
            panelAgregarUsuarios.Dock = DockStyle.Fill;
            lblCambiar.Visible = true;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            ICONO.Image = pictureBox8.Image;
            lblnumeroIcono.Text = "6";
            LblAnuncioIcono.Visible = false;
            panelICONO.Visible = false;
            panelAgregarUsuarios.Visible = true;
            panelAgregarUsuarios.Dock = DockStyle.Fill;
            lblCambiar.Visible = true;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            ICONO.Image = pictureBox9.Image;
            lblnumeroIcono.Text = "7";
            LblAnuncioIcono.Visible = false;
            panelICONO.Visible = false;
            panelAgregarUsuarios.Visible = true;
            panelAgregarUsuarios.Dock = DockStyle.Fill;
            lblCambiar.Visible = true;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            ICONO.Image = pictureBox10.Image;
            lblnumeroIcono.Text = "8";
            LblAnuncioIcono.Visible = false;
            panelICONO.Visible = false;
            panelAgregarUsuarios.Visible = true;
            panelAgregarUsuarios.Dock = DockStyle.Fill;
            lblCambiar.Visible = true;
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            dlg.InitialDirectory = "";
            dlg.Filter = "Imagenes|*.jpg;*.png";
            dlg.FilterIndex = 2;
            dlg.Title = "Cargador de Imagenes AlanVent";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ICONO.BackgroundImage = null;
                ICONO.Image = new Bitmap(dlg.FileName);
                ICONO.SizeMode = PictureBoxSizeMode.Zoom;
                lblnumeroIcono.Text = Path.GetFileName(dlg.FileName);
                LblAnuncioIcono.Visible = false;
                panelICONO.Visible = false;
                panelAgregarUsuarios.Visible = true;
                panelAgregarUsuarios.Dock = DockStyle.Fill;
                lblCambiar.Visible = true;
            }
        }

        private void LblAnuncioIcono_Click_3(object sender, EventArgs e)
        {
            panelAgregarUsuarios.Visible = false;
            panelAgregarUsuarios.Dock = DockStyle.None;

            Cargar_estado_de_iconos();
            panelICONO.Visible = true;
            panelICONO.Dock = DockStyle.Fill;
        }

        private void panelAgregarUsuarios_Paint(object sender, PaintEventArgs e)
        {

        }
        public bool validar_Mail(string sMail)
        {
            return Regex.IsMatch(sMail, @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$");

        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            #region Validation
            if (txtnombre.Text == "")
            {
                MessageBox.Show("Es Obligatorio Asignar Su Nombre Al Usuario que quieres asignar.", "Verifica:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtlogin.Text == "")
            {
                MessageBox.Show("Es Obligatorio Asignar Un Usuario Para Poder Iniciar Sesión.", "Verifica:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (validar_Mail(txtcorreo.Text) == false)
            {
                MessageBox.Show("Dirección de correo electronico no valido, el correo debe tener el formato: nombre@dominio.com, " + " por favor seleccione un correo valido", "Validación de correo electronico", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtcorreo.Focus();
                txtcorreo.SelectAll();
                return;
            }
            if (txtrol.Text == "")
            {
                MessageBox.Show("Es Obligatorio Asignar Un Rol Para Poder Iniciar Sesión.", "Verifica:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (LblAnuncioIcono.Visible == true)
            {
                MessageBox.Show("Asigna Un Icono Para Poder Iniciar Sesión.", "Verifica:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtPassword.Text  == "")
            {
                MessageBox.Show("No puedes registrar un usuario sin introducir una contraseña.", "Verifica:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            #region Variables

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            ICONO.Image.Save(ms, ICONO.Image.RawFormat);

            string Nombre = txtnombre.Text;
            string Login = txtlogin.Text;
            string Contra = Bases.Encriptar(txtPassword.Text.Trim());
            string Correo = txtcorreo.Text;
            string Rol = txtrol.Text;


            #endregion

            #region Insertar
            try
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();

                SqlCommand com = new SqlCommand("CrearUsuarios", cn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@nombreYApellido", Nombre);
                com.Parameters.AddWithValue("@login", Login);
                com.Parameters.AddWithValue("@contra", Contra);
                com.Parameters.AddWithValue("@Icono", ms.GetBuffer());
                com.Parameters.AddWithValue("@NombreIcono", lblnumeroIcono.Text);
                com.Parameters.AddWithValue("@Correo", Correo);
                com.Parameters.AddWithValue("@Rol", Rol);
                com.ExecuteNonQuery();
                cn.Close();
                VolverInicioUsuarios();
                RefrescarUsuarios();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            #endregion
        }

        private void lblCambiar_Click(object sender, EventArgs e)
        {
            panelAgregarUsuarios.Visible = false;
            panelAgregarUsuarios.Dock = DockStyle.None;

            Cargar_estado_de_iconos();
            panelICONO.Visible = true;
            panelICONO.Dock = DockStyle.Fill;
        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void datalistado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void datalistado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblId_usuario.Text = datalistado.SelectedCells[1].Value.ToString();
            txtnombre.Text = datalistado.SelectedCells[2].Value.ToString();
            txtlogin.Text = datalistado.SelectedCells[3].Value.ToString();
            string pass = datalistado.SelectedCells[4].Value.ToString();
            txtPassword.Text = Bases.Desencriptar(pass);

            ICONO.BackgroundImage = null;
            byte[] b = (Byte[])datalistado.SelectedCells[5].Value;
            MemoryStream ms = new MemoryStream(b);
            ICONO.Image = Image.FromStream(ms);

            LblAnuncioIcono.Visible = false;

            lblnumeroIcono.Text = datalistado.SelectedCells[6].Value.ToString();
            txtcorreo.Text = datalistado.SelectedCells[7].Value.ToString();
            txtrol.Text = datalistado.SelectedCells[8].Value.ToString();
            panelRegistros.Visible = true;
            panelRegistros.Dock = DockStyle.Fill;
            panelAgregarUsuarios.Dock = DockStyle.Fill;
            panelAgregarUsuarios.Visible = true;
            btnGuardar.Visible = false;
            btnGuardarCambios.Visible = true;

            lblCambiar.Visible = true;
            txtcorreo.BringToFront();
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            #region Validation
            if (txtnombre.Text == "")
            {
                MessageBox.Show("Es Obligatorio Asignar Su Nombre Al Usuario que quieres asignar.", "Verifica:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtlogin.Text == "")
            {
                MessageBox.Show("Es Obligatorio Asignar Un Usuario Para Poder Iniciar Sesión.", "Verifica:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (validar_Mail(txtcorreo.Text) == false)
            {
                MessageBox.Show("Dirección de correo electronico no valido, el correo debe tener el formato: nombre@dominio.com, " + " por favor seleccione un correo valido", "Validación de correo electronico", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtcorreo.Focus();
                txtcorreo.SelectAll();
                return;
            }
            if (txtrol.Text == "")
            {
                MessageBox.Show("Es Obligatorio Asignar Un Rol Para Poder Iniciar Sesión.", "Verifica:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (LblAnuncioIcono.Visible == true)
            {
                MessageBox.Show("Asigna Un Icono Para Poder Iniciar Sesión.", "Verifica:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            #region Conexion

            try
            {
                string Contra = Bases.Encriptar(txtPassword.Text.Trim());
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                ICONO.Image.Save(ms, ICONO.Image.RawFormat);

                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();
                SqlCommand comand = new SqlCommand("Editar_Usuarios", cn);
                comand.CommandType = CommandType.StoredProcedure;
                comand.Parameters.AddWithValue("@id", lblId_usuario.Text);
                comand.Parameters.AddWithValue("@nombreYApellido",txtnombre.Text);
                comand.Parameters.AddWithValue("@login",txtlogin.Text);
                comand.Parameters.AddWithValue("@contra", Contra);
                comand.Parameters.AddWithValue("@Icono",ms.GetBuffer());
                comand.Parameters.AddWithValue("@NombreIcono",lblnumeroIcono.Text);
                comand.Parameters.AddWithValue("@Correo",txtcorreo.Text);
                comand.Parameters.AddWithValue("@Rol",txtrol.Text);
                comand.ExecuteNonQuery(); 
                cn.Close();
            }
            catch (Exception)
            {

               
            }
            #endregion

            VolverInicioUsuarios();
            RefrescarUsuarios();


        }

        private void datalistado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.datalistado.Columns["Eli"].Index)
            {
                DialogResult result;
                result = MessageBox.Show("¿Realmente desea eliminar este Usuario?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {

                    try
                    {
                        foreach (DataGridViewRow row in datalistado.SelectedRows)
                        {

                            int onekey = Convert.ToInt32(row.Cells["ID"].Value);
                            string usuario = Convert.ToString(row.Cells["Login"].Value);

                            try
                            {

                                try
                                {
                                    SqlCommand cmd;
                                    SqlConnection con = new SqlConnection();
                                    con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                                    con.Open();
                                    cmd = new SqlCommand("eliminar_usuario", con);
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@idusuario", onekey);
                                    cmd.Parameters.AddWithValue("@login", usuario);
                                    cmd.ExecuteNonQuery();

                                    con.Close();

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message);
                            }

                        }
                        RefrescarUsuarios();
                    }

                    catch (Exception ex)
                    {

                    }
                }
            }
        }
        private void buscar_usuario()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("buscar_usuario", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", txtbuscar.Text);
                da.Fill(dt);
                datalistado.DataSource = dt;
                con.Close();

                datalistado.Columns[1].Visible = false;
                datalistado.Columns[5].Visible = false;
                datalistado.Columns[6].Visible = false;
                datalistado.Columns[7].Visible = false;
                datalistado.Columns[8].Visible = false;
                Bases.Multilinea(ref datalistado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            //Bases.Multilinea(ref datalistado);

        }
        private void BusquedaDeUsuarios(object sender, EventArgs e)
        {
            if (txtbuscar.Text == "")
            {
                lblBusca.Visible = true;
            }
            else
            {
                lblBusca.Visible = false;
            }
            buscar_usuario();
        }

        private void panelRegistros_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblBusca_Click(object sender, EventArgs e)
        {
            txtbuscar.Focus();
        }

        private void txtcorreo_TextChanged(object sender, EventArgs e)
        {
            if (txtcorreo.Text == "")
            {
                lblRecuperarConCorreo.Visible = true;
            }
            else
            {
                lblRecuperarConCorreo.Visible = false;
            }
        }
    }
}
