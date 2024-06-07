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

namespace AlanVent_Sistema_De_Ventas.Presentation.Compras
{
    public partial class Actualizar_PrecioVenta : Form
    {
        public Actualizar_PrecioVenta()
        {
            InitializeComponent();
        }
        double precio;
        int IdProducto = RealizarCompra.IdProd_Editar;
        bool estado;
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }
        private void Guardar()
        {
            try
            {
                precio = Convert.ToDouble(txtNuevoPrecio.Text);
                estado = true;
            }
            catch (Exception ex)
            {
                estado = false;
                MessageBox.Show("Por Favor digita un formato correcto");
            }
            if (estado == true)
            {
                Editar_Precio_venta();
            }
        }
        private void Editar_Precio_venta()
        {
            Editar_datos.Editar_precio_venta_desde_COMPRAS(precio, IdProducto);
            Editar_datos.AplicarConfiguracionImpuestosProductos();
            Dispose();
        }

        private void txtNuevoPrecio_TextChanged(object sender, EventArgs e)
        {
            Bases.ValidateSeparatorToNumberInString(ref txtNuevoPrecio);
        }
    }
}
