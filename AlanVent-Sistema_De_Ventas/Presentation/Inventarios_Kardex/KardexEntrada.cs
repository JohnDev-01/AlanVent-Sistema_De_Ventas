using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;

namespace AlanVent_Sistema_De_Ventas.Presentation.Inventarios_Kardex
{
    public partial class KardexEntrada : Form
    {
        public KardexEntrada()
        {
            InitializeComponent();
        }
        int Idproducto;
        double CantidadActual;
        double PrecioVentaActual;
        double CostoActual;
        double PrecioMayoreoActual;
        double a_partide;
        double CostoNuevo;
        double cantidadAgregada;
        double costoAgregado;
        private void TXTBUSCARProducto_TextChanged(object sender, EventArgs e)
        {
            BuscarProductosKardex();
        }
        private void BuscarProductosKardex()
        {
            DataTable dt = new DataTable();
            Obtener_datos.BUSCAR_PRODUCTOS_KARDEX(ref dt, TXTBUSCARProducto.Text);
            DatalistadoProductos.DataSource = dt;
            DatalistadoProductos.Visible = true;
            DatalistadoProductos.Columns[1].Visible = false;
            DatalistadoProductos.Columns[3].Visible = false;
            DatalistadoProductos.Columns[4].Visible = false;
            DatalistadoProductos.Columns[5].Visible = false;
            DatalistadoProductos.Columns[6].Visible = false;
            DatalistadoProductos.Columns[7].Visible = false;
            DatalistadoProductos.Columns[8].Visible = false;
            DatalistadoProductos.Columns[9].Visible = false;
            DatalistadoProductos.Columns[10].Visible = false;
            DatalistadoProductos.Columns[11].Visible = false;
            DatalistadoProductos.Columns[12].Visible = false;
            DatalistadoProductos.Columns[13].Visible = false;
            DatalistadoProductos.Columns[14].Visible = false;
            DatalistadoProductos.Columns[15].Visible = false;
            DatalistadoProductos.Columns[16].Visible = false;

            Bases.Multilinea(ref DatalistadoProductos);
            DatalistadoProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            DatalistadoProductos.Columns[2].Width = 500;
        }
        private void ObtenerDatos()
        {
            Idproducto = Convert.ToInt32(DatalistadoProductos.SelectedCells[1].Value);
            CantidadActual = Convert.ToDouble(DatalistadoProductos.SelectedCells[5].Value);
            CostoActual = Convert.ToDouble(DatalistadoProductos.SelectedCells[6].Value);
            PrecioVentaActual = Convert.ToDouble(DatalistadoProductos.SelectedCells[8].Value);
            PrecioMayoreoActual = Convert.ToDouble(DatalistadoProductos.SelectedCells[13].Value);
            a_partide = Convert.ToDouble(DatalistadoProductos.SelectedCells[16].Value);
            lblcantidadactual.Text = Bases.AsignarComa(CantidadActual);
            txtcosto.Text = CostoActual.ToString();
            txtprecio_venta.Text = PrecioVentaActual.ToString();
            txtpreciomayoreo.Text = PrecioMayoreoActual.ToString();
            txtApartir.Text = a_partide.ToString();
            lblAnunciodeNuevosPrecios.Text = "";
            txtagregar.Text = "";
            txtcmotivo.Text = "";
            if (PrecioMayoreoActual == 0)
            {
                Label43.Visible = false;
                txtpreciomayoreo.Visible = false;
                txtApartir.Visible = false;
                label2.Visible = false;
            }
            else
            {
                Label43.Visible = true;
                txtpreciomayoreo.Visible = true;
                txtApartir.Visible = true;
                label2.Visible = true;
            }
            TXTBUSCARProducto.Text = DatalistadoProductos.SelectedCells[2].Value.ToString();
            DatalistadoProductos.Visible = false;
        }
        private void DatalistadoProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ObtenerDatos();
        }
        private void validaciones()
        {
            if (!string.IsNullOrEmpty(txtagregar.Text))
            {
                if (!string.IsNullOrEmpty(txtcosto.Text))
                {
                    if (!string.IsNullOrEmpty(txtprecio_venta.Text))
                    {
                        if (!string.IsNullOrEmpty(txtpreciomayoreo.Text))
                        {
                            if (string.IsNullOrEmpty(txtcmotivo.Text))
                            {
                                txtcmotivo.Text = "SIN MOTIVO";
                            }
                            EditarPreciosProductos();
                        }
                        else
                        {
                            MessageBox.Show("El precio de venta al por mayor no puede estar vacio", "Valores vacios");
                            txtpreciomayoreo.Focus();
                        }

                    }
                    else
                    {
                        MessageBox.Show("El precio de venta no puede estar vacio", "Valores vacios");
                        txtprecio_venta.Focus();
                    }

                }
                else
                {
                    MessageBox.Show("El Costo no puede estar vacio", "Valores vacios");
                    txtcosto.Focus();
                }

            }
            else
            {
                MessageBox.Show("El valor a agregar no puede estar vacio", "Valores vacios");
                txtagregar.Focus();
            }


        }

        private void btn_insertar_Click(object sender, EventArgs e)
        {
            validaciones();
        }
        private void EditarPreciosProductos()
        {
            Lproductos parametros = new Lproductos();
            Editar_datos funcion = new Editar_datos();
            parametros.Id_Producto1 = Idproducto;
            parametros.Precio_de_venta = Convert.ToDouble(txtprecio_venta.Text);
            parametros.Precio_de_compra = CostoNuevo;
            parametros.Precio_mayoreo = Convert.ToDouble(txtpreciomayoreo.Text);
            parametros.Stock = txtagregar.Text;
            parametros.A_partir_de = a_partide;
            if (funcion.EditarPreciosProductos(parametros) == true)
            {
                Editar_datos.AplicarConfiguracionImpuestosProductos();
                insertar_KARDEX_Entrada();
            }
        }
        private void insertar_KARDEX_Entrada()
        {
            LKardex parametros = new LKardex();
            Insertar_datos funcion = new Insertar_datos();
            parametros.Fecha = txtfecha.Value;
            parametros.Motivo = txtcmotivo.Text;
            parametros.Cantidad = Convert.ToDouble(txtagregar.Text);
            parametros.Id_producto = Idproducto;
            if (funcion.insertar_KARDEX_Entrada(parametros) == true)
            {
                TXTBUSCARProducto.Text = "";
                TXTBUSCARProducto.Focus();
                DatalistadoProductos.Visible = true;
                MessageBox.Show("Registro realizado correctamente");
            }

        }
        private void calcular()
        {
            if (!string.IsNullOrEmpty(txtagregar.Text))
            {
                if (!string.IsNullOrEmpty(txtcosto.Text))
                {
                    cantidadAgregada = Convert.ToDouble(txtagregar.Text);
                    costoAgregado = Convert.ToDouble(txtcosto.Text);
                    CostoNuevo = ((CostoActual * CantidadActual) + (costoAgregado * cantidadAgregada)) / (CantidadActual + cantidadAgregada);
                    lblAnunciodeNuevosPrecios.Text = "Se recibiran " + txtagregar.Text + " de stock, el nuevo costo sera de " + Bases.AsignarComa(CostoNuevo);
                }


            }
        }

        private void txtagregar_TextChanged(object sender, EventArgs e)
        {
            Bases.ValidateSeparatorToNumberInString(ref txtagregar);
            calcular();
        }

        private void txtcosto_TextChanged(object sender, EventArgs e)
        {
            Bases.ValidateSeparatorToNumberInString(ref txtcosto);
            calcular();
        }

        private void KardexEntrada_Load(object sender, EventArgs e)
        {
            lblAnunciodeNuevosPrecios.Text = "";
        }

        private void txtagregar_KeyPress(object sender, KeyPressEventArgs e)
        {

            Bases.Separador_de_Numeros(txtagregar, e);
        }

        private void txtcosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtcosto, e);
        }

        private void txtprecio_venta_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtprecio_venta, e);
        }

        private void txtpreciomayoreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtpreciomayoreo, e);
        }

        private void txtprecio_venta_TextChanged(object sender, EventArgs e)
        {
            Bases.ValidateSeparatorToNumberInString(ref txtprecio_venta);
        }

        private void txtpreciomayoreo_TextChanged(object sender, EventArgs e)
        {
            Bases.ValidateSeparatorToNumberInString(ref txtpreciomayoreo);
        }
    }
}
