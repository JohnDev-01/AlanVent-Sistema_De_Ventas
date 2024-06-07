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

namespace AlanVent_Sistema_De_Ventas.Presentation.NOTIFICACIONES
{
    public partial class Notificaciones_Form : Form
    {
        public Notificaciones_Form()
        {
            InitializeComponent();
        }
        private void Dibujar_Productos_vencidoss()
        {
            try
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataAccess.ConexionMaestra.conexion;
                cn.Open();
                SqlCommand cmd = new SqlCommand("contar_productos_vencidos", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Label b = new Label();
                    Panel p1 = new Panel();
                    Panel p2 = new Panel();
                    PictureBox L1 = new PictureBox();
                    PictureBox L2 = new PictureBox();
                    Label l = new Label();

                    b.Text = "Tienes productos vencidos";
                    b.Name = dr["id"].ToString();
                    b.Size = new Size(430, 35);
                    b.Font = new Font("Microsoft Sans Serif", 10);
                    b.BackColor = Color.Transparent;
                    b.ForeColor = Color.Black;
                    b.Dock = DockStyle.Top;
                    b.TextAlign = ContentAlignment.MiddleLeft;

                    l.Text = "(" + dr["id"].ToString() + ") Producto(s) Vencido(s)";
                    l.Name = dr["id"].ToString();
                    l.Size = new Size(430, 18);
                    l.Font = new Font("Microsoft Sans Serif", 10);
                    l.BackColor = Color.Transparent;
                    l.ForeColor = Color.Gray;
                    l.Dock = DockStyle.Fill;
                    l.TextAlign = ContentAlignment.MiddleLeft;

                    L2.BackgroundImage = Properties.Resources.advertencia;
                    L2.BackgroundImageLayout = ImageLayout.Zoom;
                    L2.Size = new Size(18, 18);
                    L2.Dock = DockStyle.Left;

                    p1.Size = new Size(430, 67);
                    p1.BorderStyle = BorderStyle.FixedSingle;
                    p1.Dock = DockStyle.Top;
                    p1.BackColor = Color.White;

                    p2.Size = new Size(287, 22);
                    p2.Dock = DockStyle.Top;
                    p2.BackColor = Color.Transparent;

                    L1.BackgroundImage = Properties.Resources.calendario;
                    L1.BackgroundImageLayout = ImageLayout.Zoom;
                    L1.Size = new Size(90, 69);
                    L1.Dock = DockStyle.Left;
                    L1.BackColor = Color.Transparent;

                    p1.Controls.Add(b);
                    p1.Controls.Add(L1);
                    p1.Controls.Add(p2);
                    p2.Controls.Add(L2);
                    p2.Controls.Add(l);

                    p2.BringToFront();
                    b.SendToBack();
                    L1.SendToBack();
                    l.BringToFront();
                    PANELCONTENEDORDENOTIFICACIONES.Controls.Add(p1);

                }
                cn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Notificaciones_Form_Load(object sender, EventArgs e)
        {
            Dibujar_Productos_vencidoss();
        }
    }
}
