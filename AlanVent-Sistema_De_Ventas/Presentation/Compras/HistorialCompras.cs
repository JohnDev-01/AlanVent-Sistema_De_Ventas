using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Datos;
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
    public partial class HistorialCompras : UserControl
    {
        public HistorialCompras()
        {
            InitializeComponent();
        }
        int idcompra;
        int Id_Producto;
        double Cant_Productos;
        int IdDetalleCompra;
        double totalDetalle;
        double TotalPorProducto = 0;
        double totalTablaCompras = 0;
        int IdProveedor;
        private void dgDetallecompra_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void buscarCompras()
        {
            var dt = new DataTable();
            var funcion = new Dcompras();
            funcion.buscarCompras(ref dt, txtbusca.Text);
            datalistadoCompras.DataSource = dt;
            Bases.Multilinea(ref datalistadoCompras);
            datalistadoCompras.Columns[1].Visible = false;
            datalistadoCompras.Columns[5].Visible = false;
            datalistadoCompras.Columns[6].Visible = false;

        }

        private void txtbusca_TextChanged(object sender, EventArgs e)
        {
            if (txtbusca.Text   == "")
            {
                lblBuscarAlgo.Visible = true;
            }
            else
            {
                lblBuscarAlgo.Visible = false;
            }
            buscarCompras();
        }
        private void eliminarComprasvacias()
        {
            var funcion = new Dcompras();
            funcion.eliminarComprasvacias();

        }
        private void HistorialCompras_Load(object sender, EventArgs e)
        {
            eliminarComprasvacias();
            buscarCompras();
            lblAviso.Visible = true;
        }

        private void datalistadoCompras_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idcompra = Convert.ToInt32(datalistadoCompras.SelectedCells[1].Value);
            IdProveedor = Convert.ToInt32(datalistadoCompras.SelectedCells[5].Value);
            SumarTotal();
            mostrarDetalleventa();
            dgDetallecompra.Visible = true;
            lblAviso.Visible = false;
        }
        private void SumarTotal()
        {
            totalDetalle = 0;
            try
            {
                foreach (DataGridViewRow item in dgDetallecompra.Rows)
                {
                    totalDetalle += Convert.ToDouble(item.Cells["Total"].Value.ToString());

                }
            }
            catch (Exception ex)
            {

            }
            lbltotal.Text = Bases.AsignarComa(totalDetalle);
            SumarCredito();
        }
        private void SumarCredito()
        {
            double resultado = 0;
            Obtener_datos.ObtenerSaldoCreditoHistorialCompras(ref resultado, idcompra);
            lblTotalCredito.Text = Bases.AsignarComa(resultado);
        }
        private void mostrarDetalleventa()
        {
            DataTable dt = new DataTable();
            var funcion = new Ddetallecompra();
            var parametros = new Ldetallecompra();
            parametros.IdCompra = idcompra;
            funcion.mostrar_DetalleCompra(ref dt, parametros);
            dgDetallecompra.DataSource = dt;
            Bases.Multilinea(ref dgDetallecompra);
            dgDetallecompra.Columns[1].Visible = false;
            dgDetallecompra.Columns[2].Visible = false;
            dgDetallecompra.Columns[3].Visible = false;
            dgDetallecompra.Columns[8].Visible = false;
            dgDetallecompra.Columns[9].Visible = false;
            SumarTotal();

        }
        
        private void dgDetallecompra_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgDetallecompra.Columns["R"].Index)
            {
                DialogResult r = MessageBox.Show("¿Estas seguro de querer devolver este suministro?", "Confirma:", MessageBoxButtons.YesNo, MessageBoxIcon.Question); 
                if (r == DialogResult.Yes)
                {
                    IdDetalleCompra = Convert.ToInt32(dgDetallecompra.SelectedCells[1].Value.ToString());
                    Id_Producto = Convert.ToInt32(dgDetallecompra.SelectedCells[3].Value.ToString());
                    Cant_Productos = Convert.ToDouble(dgDetallecompra.SelectedCells[5].Value.ToString());
                    TotalPorProducto = Convert.ToDouble(dgDetallecompra.SelectedCells[7].Value.ToString());
                    Devolver_Cantidad_Producto();
                }
            }
        }
        
        private void ActualizarNumeroComprobante()
        {
           
            var documentoSerializado = SerializacionComprobantes.
                                            GenerarSerializacionCreditoFiscal();

            Editar_datos.ActualizarComprobanteModificadoCompras(documentoSerializado, idcompra);
        }
        private void Devolver_Cantidad_Producto()
        {
            Obtener_datos.MostrarTotalCompra(ref totalTablaCompras, idcompra);
            ActualizarNumeroComprobante();
            var pr = new Lproductos();
            var fun = new Insertar_datos();
            pr.Id_Producto1 = Id_Producto;
            pr.Stock = Cant_Productos.ToString();
            if ( fun.insertar_Devuelta_producto(pr, IdDetalleCompra,idcompra) == true)
            {
                
                insertarKardex();
                Datos_detalle_DevolucionSaldoProveedor_compra();
                eliminarComprasvacias();
                buscarCompras();
                mostrarDetalleventa();
            }
        }
        private void Datos_detalle_DevolucionSaldoProveedor_compra()
        {
            //Variable para utilizar 
            string Tipo = "";
            double Efec = 0;
            double Credit = 0;
            double totalNuevoSaldo_PORCOMPRA;
            double totalNuevoSaldo_PROVEEDOR;
            double SaldoActual_Proveedor = 0;

            //Obtencion de datos: ******
            #region 
            //Aqui muestro los detalles de pago de la compra como efectivo y credito
            Obtener_datos.MostrarDetallePagoCompra(ref Tipo, ref Efec, ref Credit, idcompra);
            //Aqui obtengo el saldo actual del proveedor 
            Obtener_datos.MostrarSaldoPorProveedor(ref SaldoActual_Proveedor, IdProveedor);
            #endregion
            //Validacion de tipo de pago:  *******
            #region 
            if (Tipo == "Credito") 
            {
                //Aqui se calcula el saldo nuevo de la compra: Lo que hay de credito menos
                //menos el total en el producto a devolver
                totalNuevoSaldo_PORCOMPRA = Credit - TotalPorProducto;

                //Se valida que el total de producto sea a menor al credito que ya tiene el 
                //proveedor 
                if (TotalPorProducto <= SaldoActual_Proveedor)
                {
                    //se calcula el nuevo saldo del proveedor restando su saldo actual menos el total 
                    // en productos y luego se ingresa a la bd 
                    totalNuevoSaldo_PROVEEDOR = SaldoActual_Proveedor - TotalPorProducto;
                    Editar_datos.RestarSaldoProveedor_Compras(idcompra, totalNuevoSaldo_PROVEEDOR, totalNuevoSaldo_PORCOMPRA);
                }
               
            }
            if (Tipo == "MIXTO") 
            {
                //Si el total del producto es menor al credito que tiene ingresado como pago
                //en la compra mixta 
                if (TotalPorProducto <= Credit)
                {
                    //Se calcula el nuevo saldo de la compra con lo que tiene de credito menos 
                    //el total de producto  
                    totalNuevoSaldo_PORCOMPRA = Credit - TotalPorProducto;
                    //se calcula el saldo nuevo del proveedor restando el saldo actual menos el 
                    // total del producto 
                    totalNuevoSaldo_PROVEEDOR = SaldoActual_Proveedor - TotalPorProducto;
                    //Ingreso a la base de datos 
                    Editar_datos.RestarSaldoProveedor_Compras(idcompra, totalNuevoSaldo_PROVEEDOR,totalNuevoSaldo_PORCOMPRA);
                }
                else
                {
                    //Si el total de producto es mayor al credito entonces el CREDITO de la compra se ingresa
                    //como cero y saldo del proveedor se restar al actual menos lo que hay en credito de la compra
                    totalNuevoSaldo_PORCOMPRA = 0;
                    totalNuevoSaldo_PROVEEDOR = SaldoActual_Proveedor - Credit;
                    Editar_datos.RestarSaldoProveedor_Compras(idcompra, totalNuevoSaldo_PROVEEDOR, totalNuevoSaldo_PORCOMPRA);
                }

               
            }
            #endregion
        }
        private void insertarKardex()
        {
            var parametros = new LKardex();
            var funcion = new Insertar_datos();
            parametros.Fecha = DateTime.Now;
            parametros.Motivo = "DEVUELTA DE PRODUCTO A PROVEEDOR";
            parametros.Cantidad = Cant_Productos;
            parametros.Id_producto = Id_Producto;
            funcion.insertar_KARDEX_SALIDA(parametros);
        }

        private void lblBuscarAlgo_Click(object sender, EventArgs e)
        {
            txtbusca.Focus();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            txtbusca.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SumarTotal();
        }
    }
}
