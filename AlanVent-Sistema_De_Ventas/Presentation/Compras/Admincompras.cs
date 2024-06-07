using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanVent_Sistema_De_Ventas.Presentation.Compras
{
    public partial class Admincompras : Form
    {
        public Admincompras()
        {
            InitializeComponent();
        }

        private void btncomprar_Click(object sender, EventArgs e)
        {
            Comprar();
        }
        private void Comprar()
        {
            PLcompras.Visible = true;
            PLHistorial.Visible = false;
            panelvisor.Controls.Clear();
            var frm = new RealizarCompra();
            frm.Dock = DockStyle.Fill;
            panelvisor.Controls.Add(frm);
            frm.Show();
        }
        private void btnhistorial_Click(object sender, EventArgs e)
        {
            PLcompras.Visible = false;
            PLHistorial.Visible = true;
            panelvisor.Controls.Clear();
            var frm = new HistorialCompras();
            frm.Dock = DockStyle.Fill;
            panelvisor.Controls.Add(frm);
            frm.Show();
        }

        private void Admincompras_Load(object sender, EventArgs e)
        {
            Comprar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Admin_nivel_Dios.Dashboard_Principal f = new Admin_nivel_Dios.Dashboard_Principal();
            f.ShowDialog();
        }
    }
}
