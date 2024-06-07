using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.Ventas_Menu_Principal
{
    public partial class Cantidad_a_granel : Form
    {
        public Cantidad_a_granel()
        {
            InitializeComponent();
        }
        string puertoBalanza;
        string estadoBalanza;
        public double precio_unitario;
        public string descripcion;
        private string BufeerRespuesta;
        private delegate void DelegadoAcceso(string accion);
        public static bool productQuantityValidator;
        private void AccesoForm(string accion)
        {
            BufeerRespuesta = accion;
            txtValorPesado.Text = BufeerRespuesta;
        }
        private void accesoInterrupcion(string accion)
        {
            DelegadoAcceso Var_delagadoacceso;
            Var_delagadoacceso = new DelegadoAcceso(AccesoForm);
            Object[] arg = { accion };
            base.Invoke(Var_delagadoacceso, arg);
        }
        private void puertos_DataReceived(Object sender, SerialDataReceivedEventArgs e)
        {
            accesoInterrupcion(puertos.ReadExisting());
        }
        private void Cantidad_a_granel_Load(object sender, EventArgs e)
        {
            txtprecioUnitario.Text = precio_unitario.ToString();
            txtproducto.Text = descripcion;
            MostrarPuertos();
            txtValorPesado.Focus();
            ExecuteProductQuantityValidator();
        }
        private void MostrarPuertos()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrarPuertos(ref dt);
            foreach (DataRow item in dt.Rows)
            {
                puertoBalanza = item["PuertoBalanza"].ToString();
                estadoBalanza = item["EstadoBalanza"].ToString();
            }
            if (estadoBalanza == "CONFIRMADO")
            {
                Abrir_Puertos_Balanza();
            }
        }
        private void Abrir_Puertos_Balanza()
        {
            puertos.Close();
            try
            {
                puertos.BaudRate = 9600;
                puertos.DataBits = 8;
                puertos.Parity = Parity.None;
                puertos.StopBits = (StopBits)1;
                puertos.PortName = puertoBalanza;
                puertos.Open();
                if (puertos.IsOpen)
                {
                    estadoBalanza = "Conectado";
                }
                else
                {
                    estadoBalanza = "Fallo la conexion";

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.StackTrace);
            }
        }
        private void ExecuteProductQuantityValidator()
        {
            double number = 0;
            try
            {
                number = Convert.ToDouble(txtValorPesado.Text) ;
            }
            catch (Exception)
            {

            }
            if (txtValorPesado.Text == "" || number <= 0)
                productQuantityValidator = false;
            else
                productQuantityValidator = true;
        }
        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {

            if (txtValorPesado.Text == "")
            {
                lblTotal.Text = "0.00";
            }
            else
            {
                ExecuteProductQuantityValidator();
                calcular_total();
            }
        }
        private void calcular_total()
        {
            #region Validation To Number in String
            var cad = txtValorPesado.Text;
            try
            {
                double number = Convert.ToDouble(cad);
            }
            catch (Exception ex)
            {
                int counterlength = cad.Length;
                if (counterlength == 1)
                    txtValorPesado.Text = "";
                else
                {
                    var newCad = cad.Remove(cad.Length - 1);
                    txtValorPesado.Text = newCad;
                }

            }
            #endregion

            if (txtValorPesado.Text.Contains(',') == true)
            {
                txtValorPesado.Text = txtValorPesado.Text.Replace(',', '.');

            }

            try
            {
                double total;
                double cantidad = Convert.ToDouble(txtValorPesado.Text);
                total = precio_unitario * cantidad;
                lblTotal.Text = Bases.AsignarComa(total);
            }
            catch (Exception ex)
            {
                txtValorPesado.Text = txtValorPesado.Text.Remove(txtValorPesado.Text.Length - 1);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            frm_VENTAS_MENU_PRINCIPAL.txtpantalla = Convert.ToDouble(txtValorPesado.Text);
            Cotizaciones.Vista.CotizacionesMenu.Cantidad = Convert.ToDouble(txtValorPesado.Text);
            Dispose();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
