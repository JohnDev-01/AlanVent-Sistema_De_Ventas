using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;

namespace AlanVent_Sistema_De_Ventas.Presentation.Caja
{
    public partial class Listado_Gastos_ingresos : Form
    {
        public Listado_Gastos_ingresos()
        {
            InitializeComponent();
        }
        int idcaja;
        DateTime FechaInicial;
        DateTime Fechafinal;
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Listado_Gastos_ingresos_Load(object sender, EventArgs e)
        {
            Fechafinal = DateTime.Now;
            Mostrar_cierres_de_caja_pediente();
            ListarGastos();
            Listar_ingresos();
        }
        private void ListarGastos()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrar_gastos_por_turnos(ref dt, idcaja, FechaInicial, Fechafinal);
            datalistado_Gastos.DataSource = dt;
            sumar_gastos();
            Logic.Bases.Multilinea(ref datalistado_Gastos);
            datalistado_Gastos.Columns[1].Visible = false;
        }
        private void sumar_gastos()
        {
            double total = 0;
            foreach (DataGridViewRow fila in datalistado_Gastos.Rows)
            {
                total += Convert.ToDouble(fila.Cells["Importe"].Value);
            }
            lblTotalGastos.Text = Bases.AsignarComa(total);
        }
        private void Listar_ingresos()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrar_ingresos_por_turnos(ref dt, idcaja, FechaInicial, Fechafinal);
            datalistado_Ingresos.DataSource = dt;
            sumar_Ingresos();
            Logic.Bases.Multilinea(ref datalistado_Ingresos);
            datalistado_Ingresos.Columns[1].Visible = false;    
        }
        private void sumar_Ingresos()
        {
            double total = 0;
            foreach (DataGridViewRow fila in datalistado_Ingresos.Rows)
            {
                total += Convert.ToDouble(fila.Cells["Importe"].Value);
            }
            lblTotal_ingresos.Text = Bases.AsignarComa(total);
        }
        private void Mostrar_cierres_de_caja_pediente()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrar_cierre_de_caja_pendiente(ref dt);
            foreach(DataRow dr in dt.Rows)
            {
                idcaja = Convert.ToInt32(dr["id_caja"]);
                FechaInicial = Convert.ToDateTime( dr["fechainicio"]);
            }
        }

        private void datalistado_Gastos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == datalistado_Gastos.Columns["Eliminar"].Index)
            {
                DialogResult r = MessageBox.Show("¿Realmente deseas eliminar este gasto?", "Confirma:", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
                if (r == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(datalistado_Gastos.SelectedCells[1].Value);
                    Eliminar_datos.eliminar_gastos(id);
                    ListarGastos();
                }
                
            }
        }

        private void datalistado_Ingresos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == datalistado_Ingresos.Columns["EliminarI"].Index)
            {
                DialogResult r = MessageBox.Show("¿Realmente deseas eliminar este ingreso?", "Confirma:", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (r == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(datalistado_Ingresos.SelectedCells[1].Value);
                    Eliminar_datos.eliminar_ingreso(id);
                    Listar_ingresos();
                }

            }
        }
    }
}
