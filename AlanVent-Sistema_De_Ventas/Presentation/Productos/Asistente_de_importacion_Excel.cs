using SpreadsheetLight;
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

namespace AlanVent_Sistema_De_Ventas.Presentation.Productos
{
    public partial class Asistente_de_importacion_Excel : Form
    {
        public Asistente_de_importacion_Excel()
        {
            InitializeComponent();
        }
        bool estadoArchivo = false;
        private void Asistente_de_importacion_Excel_Load(object sender, EventArgs e)
        {
            B1.Enabled = true;
            B2.Enabled = false;
            B3.Enabled = false;
            Paso1.Visible = true;
            Paso2.Visible = false;
            Paso3.Visible = false;
        }

        private void TSIGUIENTE_Y_GUARDAR__Click(object sender, EventArgs e)
        {
            PanelDescarga_de_archivo.Visible = false;
            PanelCargarArchivo.Visible = true;
            B1.Enabled = false;
            B2.Enabled = true;
            B3.Enabled = false;
            Paso1.Visible = false;
            Paso2.Visible = true;
            Paso3.Visible = false;
        }
        private void archivo_correcto()
        {
            PanelCargarArchivo.BackColor = Color.White;
            lblarchivoCargado.Visible = true;
            label3.Visible = false;
            MenuStrip1.Visible = true;
            Pcsv.Visible = true;
            LinkLabel3.LinkColor = Color.Black;
            lblnombre_Del_archivo.ForeColor = Color.FromArgb(64, 64, 64);
            PanelCargarArchivo.BackgroundImage = null;

        }
        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog myFileDialog = new OpenFileDialog();
            myFileDialog.InitialDirectory = @"c:\\temp\";
            myFileDialog.Filter = "CSV files|*.CSV;*.csv)";
            myFileDialog.FilterIndex = 2;
            myFileDialog.RestoreDirectory = true;
            myFileDialog.Title = "Elija el Archivo .CSV";
            if (myFileDialog.ShowDialog() == DialogResult.OK)
            {
                lblnombre_Del_archivo.Text = myFileDialog.SafeFileName.ToString();
                lblArchivoListo.Text = lblnombre_Del_archivo.Text;
                lblRuta.Text = myFileDialog.FileName.ToString();
                archivo_correcto();
                estadoArchivo = true;
            }
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (estadoArchivo == true)
            {
                PanelCargarArchivo.Visible = false;
                PanelGuardarData.Visible = true;
                B1.Enabled = false;
                B2.Enabled = false;
                B3.Enabled = true;
                Paso1.Visible = false;
                Paso2.Visible = false;
                Paso3.Visible = true;
            }
            else
            {
                MessageBox.Show("Por Favor carga el archivo .csv para poder continuar", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void guardar_datos_Precargados()
        {
            string Textlines = "";
            string[] Splitline;
            if (System.IO.File.Exists(lblRuta.Text) == true)
            {
                System.IO.StreamReader objReader = new System.IO.StreamReader(lblRuta.Text);
                while (objReader.Peek() != -1)
                {
                    Textlines = objReader.ReadLine();
                    Splitline = Textlines.Split(';');
                    datalistado.ColumnCount = Splitline.Length;
                    datalistado.Rows.Add(Splitline);

                }
            }
            else
            {
                MessageBox.Show("Archivo Inexistente", "CSV Inexistente");
            }

            try
            {
                foreach (DataGridViewRow row in datalistado.Rows)
                {
                    rellenar_vacios();
                    string CODIGO = Convert.ToString(row.Cells["Codigo"].Value);
                    string descripcion = Convert.ToString(row.Cells["Descripcion"].Value);
                    SqlCommand cmd;
                    DataAccess.ConexionMaestra.conectar.Open();
                    cmd = new SqlCommand("insertar_Producto_Importacion", DataAccess.ConexionMaestra.conectar);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@Usa_inventarios", "SI");
                    cmd.Parameters.AddWithValue("@Stock", 0);
                    cmd.Parameters.AddWithValue("@Precio_de_compra", 0);
                    cmd.Parameters.AddWithValue("@Fecha_de_vencimiento", "NO APLICA");
                    cmd.Parameters.AddWithValue("@Precio_de_venta", 0);
                    cmd.Parameters.AddWithValue("@Codigo", CODIGO);

                    cmd.Parameters.AddWithValue("@Se_vende_a", "Unidad");
                    cmd.Parameters.AddWithValue("@Impuesto", 0);
                    cmd.Parameters.AddWithValue("@Stock_minimo", 0);
                    cmd.Parameters.AddWithValue("@Precio_mayoreo", 0);
                    cmd.Parameters.AddWithValue("@A_partir_de", 0);
                    cmd.Parameters.AddWithValue("@Fecha", DateTime.Today);
                    cmd.Parameters.AddWithValue("@Motivo", "Registro inicial de Producto");
                    cmd.Parameters.AddWithValue("@Cantidad", 0);
                    cmd.Parameters.AddWithValue("@Id_usuario", Productos.frm_Productos.idusuario);
                    cmd.Parameters.AddWithValue("@Tipo", "ENTRADA");
                    cmd.Parameters.AddWithValue("@Estado", "CONFIRMADO");
                    cmd.Parameters.AddWithValue("@Id_caja", Productos.frm_Productos.idcaja);
                    int r = cmd.ExecuteNonQuery();
                    DataAccess.ConexionMaestra.conectar.Close();


                }
                MessageBox.Show("Importación Exitosa", "Importacion de Datos",MessageBoxButtons.OK,MessageBoxIcon.Information);
                Dispose();


            }
            catch (Exception ex)
            {
                MessageBox.Show("No se han encontrado datos.","",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

        }
        private void rellenar_vacios()
        {
            foreach (DataGridViewRow row in datalistado.Rows)
            {
                if (row.Cells["Descripcion"].Value.ToString() == "")
                {
                    row.Cells["Descripcion"].Value = "VACIO@";
                }
                if (row.Cells["Codigo"].Value.ToString() == "")
                {
                    row.Cells["Codigo"].Value = "VACIO@";
                }
            }
        }
        private void Label11_Click(object sender, EventArgs e)
        {
            guardar_datos_Precargados();
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string ruta;
                FolderBrowserDialog fd = new FolderBrowserDialog();
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    ruta = fd.SelectedPath + @"\Plantilla_Productos_AlanVent.csv";
                    SLDocument nombre_de_excel = new SLDocument();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Descripcion", typeof(string));
                    dt.Columns.Add("Codigo", typeof(string));
                    nombre_de_excel.ImportDataTable(1, 1, dt, true);
                    nombre_de_excel.SaveAs(ruta);
                    TSIGUIENTE_Y_GUARDAR_.Text = "Siguiente";
                    MessageBox.Show("Plantilla obtenida ubicala en: " + ruta, "Archivo Excel Creado:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        private void PanelCargarArchivo_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void PanelCargarArchivo_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (String[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string path in files)
            {
                lblRuta.Text = path;
                string ruta = lblRuta.Text;
                if (ruta.Contains(".csv"))
                {
                    archivo_correcto();
                    lblnombre_Del_archivo.Text = Path.GetFileName(ruta);
                    lblArchivoListo.Text = lblnombre_Del_archivo.Text;
                    estadoArchivo = true;
                }
                else
                {
                    MessageBox.Show("Archivo Incorrecto", "Formato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }
    }
}
