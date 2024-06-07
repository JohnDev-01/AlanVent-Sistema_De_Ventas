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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using AlanVent_Sistema_De_Ventas.Presentation.Membrecias;
using AlanVent_Sistema_De_Ventas.DataAccess;

namespace AlanVent_Sistema_De_Ventas
{
    public partial class frmInicioSeccion : Form
    {
        string txtlogin;
        int contador;
        int contador_movimientos_validar_caja;
        int contadorCajas;
        public static int idusuariovariable;
        public static int idcajavariable;
        string rol_seleccionadoUser;
        string cajero = "Cajero (Si esta autorizado para manejar dinero)";
        string vendedor = "Solo Ventas (no esta autorizado para manejar dinero)";
        string administrador = "Administrador (Control total)";
        int idusuarioVerificador;
        string lblSerialPC;
        string lblSerialPc_local;
        string labelrol;
        string lblApertura_De_Caja;
        string resultado_licencia;
        string fechaFinal;
        string Ip;
        string FormatoHtml;
        public frmInicioSeccion()
        {
            InitializeComponent();
        }
        
        private void Label3_Click(object sender, EventArgs e)
        {

        }
        private void cargarusuarios()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("validar_usuario_para_ingresar", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@password", Logic.Bases.Encriptar(txtpaswwor.Text));
                da.SelectCommand.Parameters.AddWithValue("@login", txtlogin);

                da.Fill(dt);
                datalistado.DataSource = dt;
                con.Close();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
          


        }
        private void Listarcierres_de_caja()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("MOSTRAR_MOVIMIENTOS_DE_CAJA_POR_SERIAL", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //da.SelectCommand.Parameters.AddWithValue("@serial", lblSerialPc);
                da.Fill(dt);
                //datalistado_detalle_cierre_de_caja.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }
        
        private void ListarCierres_Caja()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("MOSTRAR_MOVIMIENTOS_DE_CAJA_POR_SERIAL", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@serial", lblSerialPC);
                da.Fill(dt);
                datalistado_detalle_cierre_caja.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            //Bases.Multilinea(ref datalistado);

        }
        private void contarCierre_De_Caja()
        {
            int x;

            x = datalistado_detalle_cierre_caja.Rows.Count;
            contadorCajas = x;
        }
       
        private void Aperturar_detalle_cierre_caja()
        {
            try
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();

                SqlCommand com = new SqlCommand("insertar_DETALLE_cierre_de_caja", cn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@fechaini", DateTime.Now);
                com.Parameters.AddWithValue("@fechafin", DateTime.Now);
                com.Parameters.AddWithValue("@fechacierre", DateTime.Now);
                com.Parameters.AddWithValue("@ingresos","0.00" );
                com.Parameters.AddWithValue("@egresos", "0.00");
                com.Parameters.AddWithValue("@saldo", "0.00");
                com.Parameters.AddWithValue("@idusuario", idusuariovariable);
                com.Parameters.AddWithValue("@totalcaluclado", "0.00");
                com.Parameters.AddWithValue("@totalreal", "0.00");
                com.Parameters.AddWithValue("@estado","CAJA APERTURADA" );
                com.Parameters.AddWithValue("@diferencia", "0.00");
                com.Parameters.AddWithValue("@id_caja", idcajavariable);
                com.ExecuteNonQuery();
                cn.Close();
              

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void obtener_id_usuario()
        {
            try
            {

                idusuariovariable = Convert.ToInt32(datalistado.SelectedCells[1].Value.ToString());

            }
            catch (Exception ex)
            {

            }
        }
        private void IniciarSesion_Correcto ()
        {
         
                cargarusuarios();
                contar();
            
            if (contador > 0)
            {
                obtener_id_usuario();
                mostrar_roles();
                
                if (rol_seleccionadoUser != cajero)
                {
                    timerValidarRol.Start();
                }
                else if(rol_seleccionadoUser == cajero)
                {
                    validar_aperturas_de_caja();

                }
            }
        }
        private void obtener_usuario_que_aperturo_caja()
        {
            try
            {
                lblUsuario_queInicioCaja.Text = datalistado_detalle_cierre_caja.SelectedCells[1].Value.ToString();
                lblNombreDelCajero.Text = datalistado_detalle_cierre_caja.SelectedCells[2].Value.ToString();
            }
            catch
            {

            }
        }
        private void validar_aperturas_de_caja()
        {
            ListarCierres_Caja();
            contarCierre_De_Caja();
            if (contadorCajas == 0)
            {
                Aperturar_detalle_cierre_caja();
                lblApertura_De_Caja = "Nuevo*****";
                timerValidarRol.Start();

            }
            else
            {
                Mostrar_movimientos_de_caja_por_serial_y_usuario();
                Contar_movimientos_de_caja_por_usuario();
               
                if (contador_movimientos_validar_caja == 0)
                {
                    obtener_usuario_que_aperturo_caja();
                    MessageBox.Show("Para Poder Continuar Con El Turno de *" + lblNombreDelCajero.Text + "* Inicia sesion con el usuario " + lblUsuario_queInicioCaja.Text +
                               " O el Usuario *admin*", "Caja Iniciada:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblApertura_De_Caja = "Aperturado";
                    timerValidarRol.Start();
                }
            }
        }

        private void Contar_movimientos_de_caja_por_usuario()
        {
            int x;

            x = datalistado_movimientos_validar.Rows.Count;
            contador_movimientos_validar_caja = x;
        }
        private void Mostrar_movimientos_de_caja_por_serial_y_usuario()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("MOSTRAR_MOVIMIENTOS_DE_CAJA_POR_SERIAL_y_usuario", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@serial", lblSerialPC);
                da.SelectCommand.Parameters.AddWithValue("@idusuario", idusuariovariable);
                da.Fill(dt);
                datalistado_movimientos_validar.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            //Bases.Multilinea(ref datalistado);

        }
        private void mostrar_roles()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = DataAccess.ConexionMaestra.conexion;

            SqlCommand com = new SqlCommand("mostrar_permisos_por_usuario_ROL_UNICO", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@idusuario", idusuariovariable);



            try
            {
                con.Open();
                rol_seleccionadoUser = Convert.ToString(com.ExecuteScalar());
                con.Close();


            }
            catch (Exception ex)
            {
             
            }

           

        }
       
        private void contar()
        {
            int x;

            x = datalistado.Rows.Count;
            contador = x;
        }
        private void txtpaswwor_TextChanged(object sender, EventArgs e)
        {
           IniciarSesion_Correcto();
        }

        private void MenuStrip15_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtpaswwor.Text = txtpaswwor.Text + "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtpaswwor.Text = txtpaswwor.Text + "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtpaswwor.Text = txtpaswwor.Text + "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtpaswwor.Text = txtpaswwor.Text + "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtpaswwor.Text = txtpaswwor.Text + "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtpaswwor.Text = txtpaswwor.Text + "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtpaswwor.Text = txtpaswwor.Text + "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtpaswwor.Text = txtpaswwor.Text + "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtpaswwor.Text = txtpaswwor.Text + "9";
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtpaswwor.Text = txtpaswwor.Text + "0";
        }

        private void btnborrartodo_Click(object sender, EventArgs e)
        {
            txtpaswwor.Clear();
        }

        private void tver_Click(object sender, EventArgs e)
        {
            txtpaswwor.PasswordChar = '\0';
            tocultar.Visible = true;
            tver.Visible = false;
        }
        public static string Mid(string param, int startIndex, int length)
        {
            string result = param.Substring(startIndex, length);
            return result;
        }
        private void tocultar_Click(object sender, EventArgs e)
        {
            txtpaswwor.PasswordChar = '*';
            tocultar.Visible = false;
            tver.Visible = true;
        }

        private void btnborrarderecha_Click(object sender, EventArgs e)
        {
            try
            {
                int largo;
                if (txtpaswwor.Text != "")
                {
                    largo = txtpaswwor.Text.Length;
                    //label4.Text = Convert.ToString(largo);
                    txtpaswwor.Text = Mid(txtpaswwor.Text, 0, largo - 1);
                }
            }
            catch
            {

            }
        }

        private void btn_insertar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Los datos ingresados no son valido, por favor verifica.", "Verifica:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        private void miEventoImagen(System.Object sender, EventArgs e)
        {
            txtlogin = Convert.ToString(((PictureBox)sender).Tag);
            
            PanelUsuarios.Visible = false;
            PanelLogo.Visible = false;
            
            PanelIngreso_de_contraseña.Visible = true;
            PanelIngreso_de_contraseña.Dock = DockStyle.Fill;
            panelcontra.Location = new Point((Width - panelcontra.Width) / 2, (Height - panelcontra.Height) / 2);
            Obtener_datos.MostrarIconoNombreNombrePorUsuario(txtlogin, ref IconoUsuario, ref lblNombreUsuario);
            txtpaswwor.Focus();
        }

        private void mieventoLabel(System.Object sender, EventArgs e)
        {
            txtlogin = ((Label)sender).Text;
            PanelUsuarios.Visible = false;
            PanelLogo.Visible = false;
            
            PanelIngreso_de_contraseña.Visible = true;
            PanelIngreso_de_contraseña.Dock = DockStyle.Fill;
            panelcontra.Location = new Point((Width - panelcontra.Width) / 2, (Height - panelcontra.Height) / 2);
            Obtener_datos.MostrarIconoNombreNombrePorUsuario(txtlogin, ref IconoUsuario, ref lblNombreUsuario);
            txtpaswwor.Focus();
        }
        
        public void DIBUJARusuarios()

        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("select * from Usuarios WHERE Estado = 'ACTIVO'", con);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Label b = new Label();
                    Panel p1 = new Panel();
                    PictureBox I1 = new PictureBox();

                    b.Text = rdr["Login"].ToString();
                    b.Name = rdr["ID"].ToString();
                    b.Size = new System.Drawing.Size(236, 51);
                    b.Font = new System.Drawing.Font("Microsoft Sans Serif", 15);
                    b.BackColor = Color.FromArgb(29, 29, 29);
                    b.ForeColor = Color.White;
                    b.Dock = DockStyle.Bottom;
                    b.TextAlign = ContentAlignment.MiddleCenter;
                    b.Cursor = Cursors.Hand;

                    p1.Size = new System.Drawing.Size(215, 208);
                    p1.BorderStyle = BorderStyle.None;
                    p1.BackColor = Color.FromArgb(29, 29, 29);


                    I1.Size = new System.Drawing.Size(190, 160);
                    I1.Dock = DockStyle.Top;
                    I1.BackgroundImage = null;
                    byte[] bi = (Byte[])rdr["Icono"];

                    MemoryStream ms = new MemoryStream(bi);
                    I1.Image = Image.FromStream(ms);
                    I1.SizeMode = PictureBoxSizeMode.Zoom;
                    I1.Tag = rdr["Login"].ToString();
                    I1.Cursor = Cursors.Hand;

                    p1.Controls.Add(b);
                    p1.Controls.Add(I1);
                    b.BringToFront();
                    flowLayoutPanel1.Controls.Add(p1);

                    b.Click += new EventHandler(mieventoLabel);
                    I1.Click += new EventHandler(miEventoImagen);
                }
                con.Close();

            }
            catch (Exception ex)
            {

            }
        }
        private void Mostrar_Licencia_Temporal()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("select * from Marcan", con);

                da.Fill(dt);
                datalistadr_Licencia_tmporal.DataSource = dt;
                
                con.Close();


            }
            catch (Exception)
            {

                
            }
        }
        private void frmInicioSeccion_Load(object sender, EventArgs e)
        {
            validar_conexion();
            escalar_paneles();
            ObtenerIpLocal();
            Eliminar_datos.EliminarInicioSesionUsuariosVendedor();
            FormatoHtml = richTextBox_Codigo_Gmail.Text;
        }
        private void ObtenerIpLocal()
        {
            Bases.ObtenerIp(ref Ip);
            this.Text = "IP Para Conexion Remota: "+Ip;
        }
        private void ValidarLicencia()
        {
            DLicencias funcion = new DLicencias();
            funcion.ValidarLicencias(ref resultado_licencia, ref fechaFinal);
            if (resultado_licencia == "?ACTIVO?")
            {
                lblestadoLicencia.Text = "Licencia de prueba activada hasta: " + fechaFinal;
            }
            if (resultado_licencia == "?ACTIVADO PRO?")
            {
                lblestadoLicencia.Text = "Licencia PROFESIONAL Activada hasta el: " + fechaFinal;
            }
            if (resultado_licencia == "VENCIDA")
            {
                funcion.EditarMarcanVencidas();
                Dispose();
                Membresias_Nuevo  frm = new Membresias_Nuevo();
                frm.ShowDialog();
            }

        }
        private void escalar_paneles()
        {

            //panelcontra.Location = new Point((Width - panelcontra.Width) / 2, (Height - panelcontra.Width) / 2);
            //PanelIngreso_de_contraseña.Location = new Point((Width - PanelIngreso_de_contraseña.Width) / 2, (Height - PanelIngreso_de_contraseña.Width) / 2);
            pSolicitarPin.Location = new Point((Width - pSolicitarPin.Width) / 2, (Height - pSolicitarPin.Width) / 2);
            PanelUsuarios.Visible = true;
            PanelUsuarios.Dock = DockStyle.Fill;
            PdeCarga.Visible = false;

        }
        private void btncambioUsuario_Click(object sender, EventArgs e)
        {
            txtpaswwor.Clear();
            PanelIngreso_de_contraseña.Dock = DockStyle.None;
            PanelIngreso_de_contraseña.Visible = false;
            PanelLogo.Visible = true;
            PanelLogo.Dock = DockStyle.Top;
            PanelUsuarios.Visible = true;
            PanelUsuarios.Dock = DockStyle.Fill;
            
        }
        private void mostrar_correos()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("select Correo from Usuarios where Estado='ACTIVO'", con);

                da.Fill(dt);
                txtcorreo.DisplayMember = "Correo";
                txtcorreo.ValueMember = "Correo";
                txtcorreo.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }



        }
        private void button1_Click(object sender, EventArgs e)
        {
            pSolicitarPin.Visible = true;
            pSolicitarPin.Location = new Point((Width - pSolicitarPin.Width) / 2, (Height - pSolicitarPin.Height) / 2);

            pSolicitarPin.BringToFront();
            mostrar_correos();
        }
        private void mostrar_contra_por_usuario(string user)
        {
            try
            {
                //string resultadoDiferencia;
                //
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                SqlCommand da = new SqlCommand("Buscar_Contra_PorUsuario", con);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@user", user);

                con.Open();
                lblResultadoContraseña.Text = Convert.ToString(da.ExecuteScalar());
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnEnviarCorreo_Click(object sender, EventArgs e)
        {
           
        }
        private void EnviarCorreo(string correo)
        {
            try
            {
                mostrar_contra_por_usuario(txtlogin);
                var contra = Bases.Desencriptar(lblResultadoContraseña.Text);
                FormatoHtml = FormatoHtml.Replace("@pass", contra);
                bool resul =  Bases.enviarCorreo("alanvent2107@gmail.com", "twugguemnkptugxm", FormatoHtml, "Solicitud de Contraseña", correo, "");
                if (resul == true)
                {
                    MessageBox.Show("Se a enviado tu contraseña al correo electronico por favor verifica.", "Contraseña Restaurada:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pSolicitarPin.Visible = false;
                    FormatoHtml = richTextBox_Codigo_Gmail.Text;
                }
            }
            catch(Exception ex)
            {
            }
        }
        string Indicador;
        
        private void MostrarUsuariosRegistrado()
        {
            try
            {
                
                ConexionMaestra.abrir();
                
                var Query = "select ID from Usuarios";
                SqlCommand data = new SqlCommand(Query, ConexionMaestra.conectar);
                idusuarioVerificador = Convert.ToInt32(data.ExecuteScalar());
                ConexionMaestra.cerrar();
                Indicador = "CORRECTO";
            }
            catch (Exception)
            {
                Indicador = "INCORRECTO";
                idusuarioVerificador = 0;
            }
        }
        int txtContadorUsuarios;
        private void Validar_CajaHabilitada()
        {
            if (Obtener_datos.ValidarCajaHabilitada() == false)
            {
                MessageBox.Show("Esta caja se encuentra deshabilitada en el sistema principal.", "CAJA DESHABILITADA:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Dispose();
                Application.Exit();
                Application.ExitThread();
            }
        }
        private void validar_conexion()
        {
            MostrarUsuariosRegistrado();
            if (Indicador == "CORRECTO")
            {


                if (idusuarioVerificador == 0)
                {
                     Dispose();
                    Presentation.Asistente_De_Instalacion_Servidor.Registro_de_Empresa FRM = new Presentation.Asistente_De_Instalacion_Servidor.Registro_de_Empresa();
                    FRM.ShowDialog();
                   
                }
                else
                {
                    Validar_CajaHabilitada();
                    ValidarLicencia();
                    DIBUJARusuarios();
                }

            }

            if (Indicador == "INCORRECTO")
            {
                Dispose();
                Presentation.Asistente_De_Instalacion_Servidor.Eleccion_Servidor_O_Remoto FRM = new Presentation.Asistente_De_Instalacion_Servidor.Eleccion_Servidor_O_Remoto();
                FRM.ShowDialog();
                
               
            }

            try
            {
              
               
                Bases.Obtener_serialPc(ref lblSerialPC);
                Mostrar_Caja_Por_Serial();
                try
                {
                    idcajavariable = Convert.ToInt32(datalistado_caja.SelectedCells[1].Value.ToString());
                    
                    lblCaja.Text = datalistado_caja.SelectedCells[2].Value.ToString();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
            catch (Exception Ex)
            {


            }
            Mostrar_Licencia_Temporal();
            try
            {
                
                fecha_final_licencia_temporal.Value = Convert.ToDateTime(Logic.Bases.Desencriptar(datalistadr_Licencia_tmporal.SelectedCells[3].Value.ToString()));
                lblSerialPc_local = Logic.Bases.Desencriptar(datalistadr_Licencia_tmporal.SelectedCells[2].Value.ToString());
                lblEstadoLicenciaLocal.Text = Logic.Bases.Desencriptar(datalistadr_Licencia_tmporal.SelectedCells[4].Value.ToString());
                TxtFechaInicnioLicencia.Value = Convert.ToDateTime(Logic.Bases.Desencriptar(datalistadr_Licencia_tmporal.SelectedCells[5].Value.ToString()));
            }
            catch (Exception ex )
            {

            }
            if (lblestadoLicencia.Text != "VENCIDO")
            {

                string fechaHoy = Convert.ToString(DateTime.Now);
                DateTime fechaHoySinH = Convert.ToDateTime(fechaHoy.Split(' ')[0]);
                if (fecha_final_licencia_temporal.Value >= fechaHoySinH)
                {

                    if (TxtFechaInicnioLicencia.Value <= fechaHoySinH)
                    {
                        if (lblEstadoLicenciaLocal.Text == "?ACTIVO?")
                        {

                            Ingresar_por_licencia_temporal();
                        }
                        else if (lblEstadoLicenciaLocal.Text == "?ACTIVADO PRO?")
                        {
                            Ingresar_por_licencia_pago();
                        }
                    }


                }
                else
                {
                    Dispose();
                    Membresias_Nuevo frm = new Membresias_Nuevo();
                    frm.ShowDialog();
                }
            }
            else
            {
                Dispose();
                Membresias_Nuevo frm = new Membresias_Nuevo();
                frm.ShowDialog();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            
        }
        private void Ingresar_por_licencia_pago()
        {
            lblestadoLicencia.Text = "Licencia PROFESIONAL Activada Hasta El: " + fecha_final_licencia_temporal.Text;
            lblLicenciaDibujarUsuarios.Text = "Licencia PROFESIONAL Activada Hasta El: " + fecha_final_licencia_temporal.Text;
            lblestadoLicencia.ForeColor = Color.LightGray;
            lblLicenciaDibujarUsuarios.ForeColor = Color.LightGray;
        }
        private void Ingresar_por_licencia_temporal()
        {
            lblestadoLicencia.Text = "Licencia De Prueba Activada Hasta El: " + fecha_final_licencia_temporal.Text;
            lblLicenciaDibujarUsuarios.Text = "Licencia De Prueba Activada Hasta El: " + fecha_final_licencia_temporal.Text;
            lblestadoLicencia.ForeColor = Color.Red;
            lblLicenciaDibujarUsuarios.ForeColor = Color.Red;
        }
        private void Mostrar_Caja_Por_Serial ()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = DataAccess.ConexionMaestra.conexion;
                con.Open();

                da = new SqlDataAdapter("Mostrar_Cajas_Por_Serial_De_DiscoDuro", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@serial", lblSerialPC);
                da.Fill(dt);
                datalistado_caja.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            //Bases.Multilinea(ref datalistado);

        }
        private void editar_inicio_sesion()
        {
            try
            {
                var NumberSerialPc = "";
                Bases.Obtener_serialPc(ref NumberSerialPc);

                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();
                SqlCommand coman = new SqlCommand("editar_inicio_De_sesion", cn);
                coman.CommandType = CommandType.StoredProcedure;
                coman.Parameters.AddWithValue("@Id_serial_Pc", NumberSerialPc);
                coman.Parameters.AddWithValue("@id_usuario", idusuariovariable);
                coman.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        private void Ocultar_panel_correo_Click(object sender, EventArgs e)
        {
            pSolicitarPin.Visible = false;
        }

        private void timerValidarRol_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                BackColor = Color.FromArgb(26, 26, 26);
                progressBar1.Value = progressBar1.Value + 10;
                PanelUsuarios.Visible = false;
                PanelIngreso_de_contraseña.Visible = false;
                pSolicitarPin.Visible = false;

                PdeCarga.Visible = true;
                PdeCarga.Dock = DockStyle.Fill;
                panelMarcaSistema.Location = new Point((Width - panelMarcaSistema.Width) / 2, (Height - panelMarcaSistema.Height) / 2);
            }
            else
            {
                progressBar1.Value = 0;
                timerValidarRol.Stop();
                if (rol_seleccionadoUser == administrador)
                {
                    editar_inicio_sesion();
                    this.Dispose();
                    Presentation.Admin_nivel_Dios.Dashboard_Principal frm = new Presentation.Admin_nivel_Dios.Dashboard_Principal();
                    frm.Show();
                    
                }
                else
                {
                    if (lblApertura_De_Caja == "Nuevo*****" & rol_seleccionadoUser == cajero)
                    {
                        
                        editar_inicio_sesion();
                        this.Dispose();
                        Presentation.Caja.frm_Apertura_de_caja Apertura_de_caja = new Presentation.Caja.frm_Apertura_de_caja();
                        Apertura_de_caja.Show();
                        
                    }
                    else if (lblApertura_De_Caja == "Aperturado" & rol_seleccionadoUser == cajero)
                    {
                        editar_inicio_sesion();
                         this.Dispose();
                        Presentation.Ventas_Menu_Principal.frm_VENTAS_MENU_PRINCIPAL MENU_PRINCIPAL = new Presentation.Ventas_Menu_Principal.frm_VENTAS_MENU_PRINCIPAL();
                        MENU_PRINCIPAL.Show();
                         
                       
                    }
                    else if (rol_seleccionadoUser == vendedor)
                    {
                        editar_inicio_sesion();
                        Insertar_datos.InsertarInicioSesionVendedor(idusuariovariable, rol_seleccionadoUser);
                         this.Dispose();
                        var MENU_PRINCIPAL = new Presentation.Ventas_Menu_Principal.frm_VENTAS_MENU_PRINCIPAL();
                        MENU_PRINCIPAL.Show();
                     
                        
                    }
                }
            }
        }

        private void btnOlvidates_contra_Click(object sender, EventArgs e)
        {
            var message = MessageBox.Show("Se enviara tu contraseña al correo que configuraste con el usuario, ¿deseas continuar?",
                "Recuperacion de cuenta:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (message== DialogResult.Yes)
            {
                EnviarCorreo(Obtener_datos.MostrarCorreoPorUsuario(txtlogin));
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void panelcontra_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PanelIngreso_de_contraseña_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {

        }
    }
}
