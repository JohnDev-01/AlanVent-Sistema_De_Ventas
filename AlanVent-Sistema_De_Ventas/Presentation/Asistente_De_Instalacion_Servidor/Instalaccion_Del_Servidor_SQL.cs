using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AlanVent_Sistema_De_Ventas.Presentation.Asistente_De_Instalacion_Servidor
{
    public partial class Instalaccion_Del_Servidor_SQL : Form
    {
        public Instalaccion_Del_Servidor_SQL()
        {
            InitializeComponent();
        }
        string Nombre_del_Equipo_Usuario;
        private void Instalaccion_Del_Servidor_SQL_Load(object sender, EventArgs e)
        {
            centrar_paneles();
            Remplazar();
            Conectar(); 
        }
        private void Conectar()
        {
            Comprobar_Si_Ya_Hay_Servidor_Instalado_SQL_EXPRESS();
            if (btnInstalarServidor.Visible == true)
            {
                Comprobar_Si_Ya_Hay_Servidor_Instalado_SQL_NORMAL();
            }
        }
        private void Remplazar()
        {
            txtEliminarBase.Text = txtEliminarBase.Text.Replace("BASEADACURSO", TXTbasededatos.Text);
            txtCrearUsuarioDb.Text = txtCrearUsuarioDb.Text.Replace("ada369", txtusuario.Text);
            txtCrearUsuarioDb.Text = txtCrearUsuarioDb.Text.Replace("BASEADA", TXTbasededatos.Text);
            txtCrearUsuarioDb.Text = txtCrearUsuarioDb.Text.Replace("softwarereal", lblcontraseña.Text);
            //Adjuntando al TextBox Que Contiene los procedimientos almacenados
            txtCrear_procedimientos.Text = txtCrear_procedimientos.Text + Environment.NewLine + txtCrearUsuarioDb.Text;
        }
        private void centrar_paneles()
        {
            Panel2.Location = new Point((Width - Panel2.Width) / 2, (Height - Panel2.Height) / 2);
            Panel4.Visible = false;
            Panel4.Dock = DockStyle.None;
            Cursor = Cursors.WaitCursor;
            Nombre_del_Equipo_Usuario = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }
        public void SavetoXML(object dbcnString)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("ConnectionString.xml");
            XmlElement root = doc.DocumentElement;
            root.Attributes[0].Value = Convert.ToString(dbcnString);
            XmlTextWriter writer = new XmlTextWriter("ConnectionString.xml", null);
            writer.Formatting = Formatting.Indented;
            doc.Save(writer);
            writer.Close();
        }
        private DataAccess.AES aes = new DataAccess.AES();
       
        private void EjecutarScriptCrearBaseComprobacion_Inicio()
        {
            var cnn = new SqlConnection("Server=" + txtservidor.Text + "; " + "database=master; Integrated Security=True");
            string s = "CREATE DATABASE " + TXTbasededatos.Text;
            var cmd = new SqlCommand(s, cnn);
            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                SavetoXML(aes.Encrypt("Data Source=" + txtservidor.Text + ";Initial Catalog=" + TXTbasededatos.Text + ";Integrated Security=True", DataAccess.Desencryptacion.appPwdUnique, int.Parse("256")));
                ejecutar_scryt_crearProcedimientos_almacenados_y_tablas();
                Panel4.Visible = true;
                Panel4.Dock = DockStyle.Fill;
                label3.Text = @"Instancia Encontrada...
            No Cierre esta Ventana, se cerrara Automaticamente cuando este todo Listo";
                Panel6.Visible = false;
                timer4.Start();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Panel6.Visible = true;
                btnInstalarServidor.Visible = true;
                Panel4.Visible = false;
                Panel4.Dock = DockStyle.None;
                lblbuscador_de_servidores.Text = "De click a Instalar Servidor, luego de click a SI cuando se le pida, luego presione ACEPTAR y espere por favor ";
            }


            finally
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
        }
        string ruta;
        private void ejecutar_scryt_crearProcedimientos_almacenados_y_tablas()
        {
            ruta = Path.Combine(Directory.GetCurrentDirectory(), txtnombre_scrypt.Text + ".txt");
            FileInfo fi = new FileInfo(ruta);
            StreamWriter sw;

            try
            {
                if (File.Exists(ruta) == false)
                {

                    sw = File.CreateText(ruta);
                    sw.WriteLine(txtCrear_procedimientos.Text);
                    sw.Flush();
                    sw.Close();

                }
                else if (File.Exists(ruta) == true)
                {
                    File.Delete(ruta);
                    sw = File.CreateText(ruta);
                    sw.WriteLine(txtCrear_procedimientos.Text);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {

            }

            try
            {
 
                Process Pross = new Process();
                Pross.StartInfo.FileName = "sqlcmd";
                Pross.StartInfo.Arguments = " -S " + txtservidor.Text + " -i " + txtnombre_scrypt.Text + ".txt";
                Pross.Start();
            }
            catch (Exception ex)
            {
               
            }
        }
        private void Comprobar_Si_Ya_Hay_Servidor_Instalado_SQL_EXPRESS()
        {
            txtservidor.Text = @".\" + lblnombredeservicio.Text;
            Ejecutar_Script_eliminar_base_comprobacion_inicio();
            EjecutarScriptCrearBaseComprobacion_Inicio();
        }
        private void Comprobar_Si_Ya_Hay_Servidor_Instalado_SQL_NORMAL()
        {
            txtservidor.Text = ".";
            Ejecutar_Script_eliminar_base_comprobacion_inicio();
            EjecutarScriptCrearBaseComprobacion_Inicio();
        }
        private void Ejecutar_Script_eliminar_base_comprobacion_inicio()
        {
            string str;
            SqlConnection cn = new SqlConnection("Data Source= " + txtservidor.Text + "; Initial Catalog= master;Integrated Security=True");
            
            str = txtEliminarBase.Text;
            SqlCommand coman = new SqlCommand(str,cn);
            try
            {
                cn.Open();
                coman.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                if((cn.State == ConnectionState.Open))
                {
                    cn.Close();
                }
            }
        }
        public static int milisegundo;
        public static int segundos;

        private void timer4_Tick(object sender, EventArgs e)
        {

            timer2.Stop();
            timer3.Stop();
            milisegundo += 1;
            mil3.Text = Convert.ToString(milisegundo);

            if (milisegundo == 60)
            {
                segundos += 1;
                seg3.Text = Convert.ToString(segundos);
                milisegundo = 0;
            }

            if (segundos == 40)
            {
                timer4.Stop();

                try
                {
                    File.Delete(ruta);
                }
                catch (Exception ex)
                {

                }

                Dispose();
                Application.Restart();
            }

        }
        //alanventexitoso
        //Nombre_del_Equipo_Usuario
        private void executa()
        {
            try
            {
                Process Pross = new Process();
                Pross.StartInfo.FileName = "SQLEXPR_x86_ENU.exe";
                Pross.StartInfo.Arguments = "/ConfigurationFile=ConfigurationFile.ini /ACTION=Install /IACCEPTSQLSERVERLICENSETERMS /SECURITYMODE=SQL /SAPWD=" + lblcontraseña.Text + " /SQLSYSADMINACCOUNTS=" + Nombre_del_Equipo_Usuario;

                Pross.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                Pross.Start();

                Panel4.Visible = true;
                Panel4.Dock = DockStyle.Fill;
               
            }
            catch (Exception ex)
            {
                
            }
        }
        private void btnInstalarServidor_Click(object sender, EventArgs e)
        {
            try
            {
                txtArgumentosini.Text = txtArgumentosini.Text.Replace("PRUEBAFINAL22", lblnombredeservicio.Text);
                timerCRARINI.Start(); // Crea Archivo de configuracion de Sql
                executa();//Configura instalacion de Sql
                timer2.Start();//Inicia contador de minutos
                Panel4.Visible = true;
                Panel4.Dock = DockStyle.Fill;
            }
            catch
            {

            }
        }

        private void timerCRARINI_Tick(object sender, EventArgs e)
        {
            string rutaPREPARAR;
            StreamWriter sw;
            rutaPREPARAR = Path.Combine(Directory.GetCurrentDirectory(), "ConfigurationFile.ini");
            rutaPREPARAR = rutaPREPARAR.Replace("ConfigurationFile.ini", @"SQLEXPR_x86_ENU\ConfigurationFile.ini");


            if (File.Exists(rutaPREPARAR) == true)
            {
                timerCRARINI.Stop();
            }

            try
            {
                sw = File.CreateText(rutaPREPARAR);
                sw.WriteLine(txtArgumentosini.Text);
                sw.Flush();
                sw.Close();
                timerCRARINI.Stop();
            }
            catch (Exception ex)
            {

            }
        }
        private void ejecutar_scryt_crearBase()
        {
            txtservidor.Text = @".\" + lblnombredeservicio.Text;
            var cnn = new SqlConnection("Data Source= " + txtservidor.Text + "; Initial Catalog= master; Integrated Security=True");

            string s = "CREATE DATABASE " + TXTbasededatos.Text;
            var cmd = new SqlCommand(s, cnn);
            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                SavetoXML(aes.Encrypt("Data Source=" + txtservidor.Text + ";Initial Catalog=" + TXTbasededatos.Text + ";Integrated Security=True", DataAccess.Desencryptacion.appPwdUnique, int.Parse("256")));
                ejecutar_scryt_crearProcedimientos_almacenados_y_tablas();
                timer4.Start();
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
        }
        private void ejecutar_scryt_ELIMINARBase()
        {
            txtservidor.Text = @".\" + lblnombredeservicio.Text;
           
            string str;
            SqlConnection cn = new SqlConnection("Data Source= " + txtservidor.Text + "; Initial Catalog= master; Integrated Security=True");

            str = txtEliminarBase.Text;
            SqlCommand coman = new SqlCommand(str, cn);
            try
            {
                cn.Open();
                
                coman.ExecuteNonQuery();
            }
            catch(Exception ex)//----
            {
                MessageBox.Show(ex.Message );
            }
            finally
            {
                if ((cn.State == ConnectionState.Open))
                {
                    cn.Close();
                }
            }
        }
        public static int milisegundo1;
        public static int segundos1;
        public static int minutos1;
        private void timer2_Tick(object sender, EventArgs e)
        {
            
            milisegundo1 += 1;
            milise.Text = Convert.ToString(milisegundo1);
            if (milisegundo1 == 60)
            {
                segundos1 += 1;
                seg.Text = Convert.ToString(segundos1);

                milisegundo1 = 0;

            }

            if (segundos1 == 60)
            {
                minutos1 += 1;

                min.Text = Convert.ToString(minutos1);
                segundos1 = 0;
            }

            if (minutos1 == 10)
            {
                timer2.Enabled = false;

                ejecutar_scryt_ELIMINARBase();
                ejecutar_scryt_crearBase();

                timer3.Start();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            milisegundo1 += 1;
            milise.Text = Convert.ToString(milisegundo1);
            if (milisegundo1 == 60)
            {
                segundos1 += 1;
                seg.Text = Convert.ToString(segundos1);

                milisegundo1 = 0;

            }

            if (segundos1 == 60)
            {
                minutos1 += 1;

                min.Text = Convert.ToString(minutos1);
                segundos1 = 0;
            }

            if (minutos1 == 1)
            {
              
                ejecutar_scryt_ELIMINARBase();
                ejecutar_scryt_crearBase();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ejecutar_scryt_ELIMINARBase();
            ejecutar_scryt_crearBase();

        }
    }
}
