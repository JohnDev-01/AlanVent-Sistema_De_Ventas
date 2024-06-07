using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.CorreoBase
{
    public partial class ConfigurarCorreo : Form
    {
        public ConfigurarCorreo()
        {
            InitializeComponent();
        }
        int contadorPaneles;
        bool EstadoCorreoBase;
        private string correobase = "alanvent2107@gmail.com";
        private string contrabase = "alanvent210720";
        private void btnsincronizar_Click(object sender, EventArgs e)
        {
            if (EstadoCorreoBase == false)
            {
                ConfirmarCorreoSINBASE();
            }
            else
            {
                ConfirmarCorreoCONBASE();
            }
            
        }
        private void ConfirmarCorreoCONBASE()
        {
            bool estado;
            estado = Bases.enviarCorreo(correobase, contrabase, "Sincronizacion con AlanVent creada Correctamente", "Sincronizacion con AlanVent", correobase, "");
            if (estado == true)
            {
                editarCorreoCONBASE();
                MessageBox.Show("Sincronizacion Creada Correctamente", "Sincronizacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Dispose();
            }
            else
            {
                MessageBox.Show("Sincronizacion Fallida, revisa el Video de Nuevo", "Sincronizacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        private void ConfirmarCorreoSINBASE()
        {
            bool estado;
            estado = Bases.enviarCorreo(TXTCORREO.Text, txtpass.Text, "Sincronizacion con AlanVent creada Correctamente", "Sincronizacion con AlanVent", TXTCORREO.Text, "");
            if (estado == true)
            {
                editarCorreoSINBASE();
                MessageBox.Show("Sincronizacion Creada Correctamente", "Sincronizacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Dispose();
            }
            else
            {
               // MessageBox.Show("Sincronizacion Fallida, revisa el Video de Nuevo", "Sincronizacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        public void editarCorreoSINBASE()
        {
            Lcorreo parametros = new Lcorreo();
            Editar_datos funcion = new Editar_datos();
            parametros.Correo = Bases.Encriptar(TXTCORREO.Text);
            parametros.Password = Bases.Encriptar(txtpass.Text);
            parametros.Estado = Bases.Encriptar("Sincronizado");
            funcion.editarCorreobase(parametros);
        }
        public void editarCorreoCONBASE()
        {
            Lcorreo parametros = new Lcorreo();
            Editar_datos funcion = new Editar_datos();
            parametros.Correo = Bases.Encriptar(correobase);
            parametros.Password = Bases.Encriptar(contrabase);
            parametros.Estado = Bases.Encriptar("Sincronizado");
            funcion.editarCorreobase(parametros);
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSiguientePanelesDemostracion_Click(object sender, EventArgs e)
        {
            AvanzarPaneles();
        }
        private void AvanzarPaneles()
        {
            if (contadorPaneles == 1)
            {
                ocultarTodosP();
                panelp2.Visible = true;
                contadorPaneles++;
                return;
            }
            if (contadorPaneles == 2)
            {
                ocultarTodosP();
                panelp3.Visible = true;
                contadorPaneles++;
                return;
            }
            if (contadorPaneles == 3)
            {
                ocultarTodosP();
                panelp4.Visible = true;
                contadorPaneles++;
                return;
            }
            if (contadorPaneles == 4)
            {
                ocultarTodosP();
                panelp5.Visible = true;
                contadorPaneles++;
                return;
            }
            if (contadorPaneles == 5)
            {
                ocultarTodosP();
                contadorPaneles = 1;
                panelp1.Visible = true;
                return;
            }

        }
        private void ocultarTodosP()
        {
            panelp1.Visible = false;
            panelp2.Visible = false;
            panelp3.Visible = false;
            panelp4.Visible = false;
            panelp5.Visible = false;
        }
        private void InicializarPaneles()
        {
            contadorPaneles = 1;
            panelp1.Visible = true;
            panelp2.Visible = false;
            panelp3.Visible = false;
            panelp4.Visible = false;
            panelp5.Visible = false;
           
        }
        private void ConfigurarCorreo_Load(object sender, EventArgs e)
        {
            InicializarPaneles();
            EscalarPanelesDatos();
            inicializarCorreo();
        }
        private void inicializarCorreo()
        {
            string correoObt = "";
            Obtener_datos.MostrarCorreoEmisor(ref correoObt);
            if (correoObt == correobase)
            {
                CheckCuenta.Checked = true;
            }
        }
        private void EscalarPanelesDatos()
        {
            //Panel contenedor de datos
            panelIngresoDatos.Location = new Point((panel4.Width - panelIngresoDatos.Width) / 2 , (panel4.Height - panelIngresoDatos.Height) / 2);

            //Limpieza de controles
            txtpass.Clear();
            TXTCORREO.Clear();

        }
        private void VolverPaneles()
        {
            if (contadorPaneles == 1)
            {
                ocultarTodosP();
                panelp5.Visible = true;
                contadorPaneles = 5;
                return;
            }
            if (contadorPaneles == 2)
            {
                ocultarTodosP();
                panelp1.Visible = true;
                contadorPaneles--;
                return;
            }
            if (contadorPaneles == 3)
            {
                ocultarTodosP();
                panelp2.Visible = true;
                contadorPaneles--;
                return;
            }
            if (contadorPaneles == 4)
            {
                ocultarTodosP();
                panelp3.Visible = true;
                contadorPaneles--;
                return;
            }
            if (contadorPaneles == 5)
            {
                ocultarTodosP();
                contadorPaneles--;
                panelp4.Visible = true;
                return;
            }
        }
        private void btnVolverPaneles_Click(object sender, EventArgs e)
        {
            VolverPaneles();
        }

        private void TXTCORREO_TextChanged(object sender, EventArgs e)
        {
            if(TXTCORREO.Text == "")
            {
                lblIngreseCorreo.Visible = true;
            }
            else
            {
                lblIngreseCorreo.Visible = false;
            }        
        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {
            if (txtpass.Text == "")
            {
                lblIngreseContra.Visible = true;
            }
            else
            {
                lblIngreseContra.Visible = false;
            }
        }

        private void CheckCuenta_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckCuenta.Checked == true)
            {
                panelContraPersonal.Visible = false;
                btnsincronizar.Location = new Point(62, 80);
                txtpass.Clear();
                TXTCORREO.Clear();
                EstadoCorreoBase = true;
            }
            else
            {
                panelContraPersonal.Visible = true;
                btnsincronizar.Location = new Point(66, 171);
                txtpass.Clear();
                TXTCORREO.Clear();
                EstadoCorreoBase = false;
            }
        }
    }
}
