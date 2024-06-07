using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;
using AlanVent_Sistema_De_Ventas.Presentation.Admin_nivel_Dios;
using AlanVent_Sistema_De_Ventas.Presentation.Caja;
using AlanVent_Sistema_De_Ventas.Presentation.CodigosDeBarras;
using AlanVent_Sistema_De_Ventas.Presentation.DeclaracionImpuestos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.Configuracion
{
    public partial class Panel_Configueraciones : Form
    {
        public Panel_Configueraciones()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void BtnVolver_Administrador_Click(object sender, EventArgs e)
        {
            Dispose();
            Dashboard_Principal frm = new Dashboard_Principal();
            frm.ShowDialog();
        }

        private void Panel_Configueraciones_Load(object sender, EventArgs e)
        {
            panelConfig.Location = new Point((this.Width - panelConfig.Width) / 2, 67);
            ActivarCheckCopias();
            ActivarCheckCierreCaja();
        }
        private void ActivarCheckCopias()
        {
            string estado = "";
            Obtener_datos.ObtenerEstadoDeCopias(ref estado);
            if (estado == "--")
            {
                IndicadorRespaldos.Checked = false;
            }
            else
            {
                IndicadorRespaldos.Checked = true;
            }
        }
        private void ActivarCheckCierreCaja()
        {
            var estado = Obtener_datos.ObtenerEstadoImprimirCierreCaja();
            if (estado == true)
            {
                indicadorImprimirCierreCaja.Checked = true;

            }
            else
            {
                indicadorImprimirCierreCaja.Checked = false;
            }
        }
        private void btnProductos_Click(object sender, EventArgs e)
        {
            Dispose();
            Productos.frm_Productos frm = new Productos.frm_Productos();
            frm.FormClosing += frm_FormClosing;
            frm.ShowDialog();
        }

        private void frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Panel_Configueraciones frm = new Panel_Configueraciones();
            frm.Show();
        }

        private void btnEmpresa_Click(object sender, EventArgs e)
        {
            EMPRESA_CONFIG.EMPRESA_CONFIG_ frm = new EMPRESA_CONFIG.EMPRESA_CONFIG_();
            frm.ShowDialog();
        }

        private void lblEmpresa_Click(object sender, EventArgs e)
        {
            EMPRESA_CONFIG.EMPRESA_CONFIG_ frm = new EMPRESA_CONFIG.EMPRESA_CONFIG_();
            frm.ShowDialog();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            Usuarios_Y_Permisos.frmUsuarios fr = new Usuarios_Y_Permisos.frmUsuarios();
            fr.ShowDialog();
        }

        private void lblUsuarios_Click(object sender, EventArgs e)
        {
            Usuarios_Y_Permisos.frmUsuarios fr = new Usuarios_Y_Permisos.frmUsuarios();
            fr.ShowDialog();
        }

        private void lblProductos_Click(object sender, EventArgs e)
        {
            Dispose();
            Productos.frm_Productos frm = new Productos.frm_Productos();
            frm.FormClosing += frm_FormClosing;
            frm.ShowDialog();
        }

        private void btnCajas_Click(object sender, EventArgs e)
        {
            Caja.Cajas_Form frm = new Caja.Cajas_Form();
            frm.ShowDialog();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            CopiasBd.CrearCopiaBd frm = new CopiasBd.CrearCopiaBd();
            frm.ShowDialog();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            CopiasBd.CrearCopiaBd frm = new CopiasBd.CrearCopiaBd();
            frm.ShowDialog();
        }

        private void btnCorreo_Click(object sender, EventArgs e)
        {
            CorreoBase.ConfigurarCorreo frm = new CorreoBase.ConfigurarCorreo();
            frm.ShowDialog();
        }

        private void btnCorreo2_Click(object sender, EventArgs e)
        {
            CorreoBase.ConfigurarCorreo frm = new CorreoBase.ConfigurarCorreo();
            frm.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            CorreoBase.ConfigurarCorreo frm = new CorreoBase.ConfigurarCorreo();
            frm.ShowDialog();
        }

        private void btnBalanza_Click(object sender, EventArgs e)
        {
            BalanzaElectronica.BalanzaForm frm = new BalanzaElectronica.BalanzaForm();
            frm.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private void IndicadorRespaldos_CheckedChanged(object sender, EventArgs e)
        {
            if (IndicadorRespaldos.Checked == true)
            {
                Editar_datos.CambiarEstadoRespaldos(DateTime.Now.ToString());

            }
            else
            {
                Editar_datos.CambiarEstadoRespaldos("--");
            }
        }

        private void lblCajas_Click(object sender, EventArgs e)
        {
            Caja.Cajas_Form frm = new Caja.Cajas_Form();
            frm.ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Caja.Cajas_Form frm = new Caja.Cajas_Form();
            frm.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Usuarios_Y_Permisos.frmUsuarios fr = new Usuarios_Y_Permisos.frmUsuarios();
            fr.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            EMPRESA_CONFIG.EMPRESA_CONFIG_ frm = new EMPRESA_CONFIG.EMPRESA_CONFIG_();
            frm.ShowDialog();
        }

        private void lblserializacion_Click(object sender, EventArgs e)
        {
            var trabajaImpuesto = "";
            Obtener_datos.ObtenerEstadoImpuestos(ref trabajaImpuesto);
            if (trabajaImpuesto == "SI")
            {
                var frm = new Serializacion_de_comprobantes.Serializacion();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Es necesario trabajar con impuestos para serializar los comprobantes fiscales",
                    "Valida:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }    
        }

        private void btnSerializacion_Click(object sender, EventArgs e)
        {
            var trabajaImpuesto = "";
            Obtener_datos.ObtenerEstadoImpuestos(ref trabajaImpuesto);
            if (trabajaImpuesto == "SI")
            {
                var frm = new Serializacion_de_comprobantes.Serializacion();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Es necesario trabajar con impuestos para serializar los comprobantes fiscales",
                    "Valida:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            var trabajaImpuesto = "";
            Obtener_datos.ObtenerEstadoImpuestos(ref trabajaImpuesto);
            if (trabajaImpuesto == "SI")
            {
                var frm = new Serializacion_de_comprobantes.Serializacion();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Es necesario trabajar con impuestos para serializar los comprobantes fiscales",
                    "Valida:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Dispose();
            Productos.frm_Productos frm = new Productos.frm_Productos();
            frm.FormClosing += frm_FormClosing;
            frm.ShowDialog();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            Dispose();
            Clientes_Proveedores.Clientes frm = new Clientes_Proveedores.Clientes();
            frm.FormClosing += frm_FormClosing;
            frm.ShowDialog();

        }

        private void lblProvedores_Click(object sender, EventArgs e)
        {
            Dispose();
            var frm = new Clientes_Proveedores.Proveedores();
            frm.FormClosing += frm_FormClosing;
            frm.ShowDialog();
        }

        private void lblClientes_Click(object sender, EventArgs e)
        {
            Dispose();
            Clientes_Proveedores.Clientes frm = new Clientes_Proveedores.Clientes();
            frm.FormClosing += frm_FormClosing;
            frm.ShowDialog();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Dispose();
            Clientes_Proveedores.Clientes frm = new Clientes_Proveedores.Clientes();
            frm.FormClosing += frm_FormClosing;
            frm.ShowDialog();
        }

        private void btnProvedores_Click(object sender, EventArgs e)
        {
            Dispose();
            var frm = new Clientes_Proveedores.Proveedores();
            frm.FormClosing += frm_FormClosing;
            frm.ShowDialog();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Dispose();
            var frm = new Clientes_Proveedores.Proveedores();
            frm.FormClosing += frm_FormClosing;
            frm.ShowDialog();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Diseñador_de_comprobantes.Ticket frm = new Diseñador_de_comprobantes.Ticket();
            frm.ShowDialog();
        }

        private void lblDisenoComprobantes_Click(object sender, EventArgs e)
        {
            Diseñador_de_comprobantes.Ticket frm = new Diseñador_de_comprobantes.Ticket();
            frm.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Diseñador_de_comprobantes.Ticket frm = new Diseñador_de_comprobantes.Ticket();
            frm.ShowDialog();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Dispose();
            var frm = new Impresoras.Admin_impresoras();
            frm.FormClosing += frm_FormClosing;
            frm.ShowDialog();

        }

        private void label10_Click(object sender, EventArgs e)
        {
            Dispose();
            var frm = new Impresoras.Admin_impresoras();
            frm.FormClosing += frm_FormClosing;
            frm.ShowDialog();
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            Dispose();
            var frm = new Impresoras.Admin_impresoras();
            frm.FormClosing += frm_FormClosing;
            frm.ShowDialog();
        }

        private void btnBalanza2_Click(object sender, EventArgs e)
        {
            BalanzaElectronica.BalanzaForm frm = new BalanzaElectronica.BalanzaForm();
            frm.ShowDialog();
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            BalanzaElectronica.BalanzaForm frm = new BalanzaElectronica.BalanzaForm();
            frm.ShowDialog();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            CorreoBase.ConfigurarCorreo frm = new CorreoBase.ConfigurarCorreo();
            frm.ShowDialog();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            CopiasBd.CrearCopiaBd frm = new CopiasBd.CrearCopiaBd();
            frm.ShowDialog();
        }
        private void Impuestos()
        {
            if (ValidateExcel.CheckInstalledExcel()== true)
            {
                string estadoImpuestos = "";
                Obtener_datos.ObtenerEstadoImpuestos(ref estadoImpuestos);
                if (estadoImpuestos == "SI")
                {
                    var page = new DeclararImpuestos();
                    page.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Para declarar impuestos deberias de trabajar con impuestos.",
                 "EMPRESA SIN IMPUESTOS CONFIGURADO:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Para generar las declaraciones de impuestos debes de tener EXCEL instalado",
                    "INSTALA EXCEL:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnImpuestos_Click(object sender, EventArgs e)
        {
            Impuestos();
        }

        private void lblImpuestos_Click(object sender, EventArgs e)
        {
            Impuestos();
        }

        private void pictureBox8_Click_1(object sender, EventArgs e)
        {
            Impuestos();
        }

        private void btnCodigoBarras_Click(object sender, EventArgs e)
        {
            var page = new MenuCodigoBarra();
            page.ShowDialog();
        }

        private void lblCodigoBarras_Click(object sender, EventArgs e)
        {
            var page = new MenuCodigoBarra();
            page.ShowDialog();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            var page = new MenuCodigoBarra();
            page.ShowDialog();
        }

        private void btnVisorPrecios_Click(object sender, EventArgs e)
        {
            Dispose();
            var page = new Visor_Precios.BuscadorPrecios();
            page.FormClosing += frm_FormClosing;
            page.ShowDialog();
        }

        

        private void lblVisorPrecios_Click(object sender, EventArgs e)
        {
            Dispose();
            var page = new Visor_Precios.BuscadorPrecios();
            page.FormClosing += frm_FormClosing;
            page.ShowDialog();
        }

        private void pbVisorPrecios_Click(object sender, EventArgs e)
        {
            Dispose();
            var page = new Visor_Precios.BuscadorPrecios();
             page.FormClosing += frm_FormClosing;
            page.ShowDialog();
        }

        private void indicadorImprimirCierreCaja_CheckedChanged(object sender, EventArgs e)
        {
            
            if (indicadorImprimirCierreCaja.Checked == true)
            {
                Editar_datos.ModificarEstadoImprimirCierreCaja("SI");
            }
            else
            {
                Editar_datos.ModificarEstadoImprimirCierreCaja("NO");
            }
        }

        private void pbCorteCaja_Click(object sender, EventArgs e)
        {
            MostrarCierreCaja();
        }

        private void lblCorteCaja_Click(object sender, EventArgs e)
        {
            MostrarCierreCaja();
        }

        private void pbIrCorteCaja_Click(object sender, EventArgs e)
        {
            MostrarCierreCaja();
        }
        private void MostrarCierreCaja()
        {
            var page = new CierreCajaRealizados();
            page.ShowDialog();
        }
    }
}
