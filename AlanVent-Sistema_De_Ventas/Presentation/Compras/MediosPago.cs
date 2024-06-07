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
    public partial class MediosPago : Form
    {
        //-----Variable ----
        double total = RealizarCompra.TotalPagar;
        double impuestos = RealizarCompra.ImpuestoCalculado;
        double subtotal = RealizarCompra.subtotal;
        int IdProveedor = RealizarCompra.idproveedor;
        int idcompra = RealizarCompra.idcompra;
        double impuestoretenido = RealizarCompra.totalretencion;
        string Nombre_Proveedor = RealizarCompra.Nombre_Proveedor;
        double efectivo_ingresado = 0;
         double credito_ingresado = 0;
        int Id;
        double resta;
        bool Estado_Para_compar = false;
        string TipoCompra;
        double totalcalculado;
        private string Modopago;

        // -------------------------------------------------------
        public MediosPago()
        {
            InitializeComponent();
        }
        private void dibujarProveedores()
        {
            var funcion = new Dproveedores();
            DataTable dt = new DataTable();
            funcion.buscar_Proveedores(ref dt, "");
            FlowpanelProveedor.Controls.Clear();
            foreach (DataRow rdr in dt.Rows)
            {
                Button b = new Button();
                b.Size = new Size(230,35);
                b.Text = rdr["Nombre"].ToString();
                b.Name = rdr["IdProveedor"].ToString();
                b.BackColor = Color.FromArgb(139, 139, 139);
                b.Font = new Font("Microsoft Sans Serif", 12);
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 0;
                b.ForeColor = Color.Black;
                FlowpanelProveedor.Controls.Add(b);
                b.Click += B_Click;
            }
            FlowpanelProveedor.Visible = true;
        }

        private void B_Click(object sender, EventArgs e)
        {
            IdProveedor = Convert.ToInt32(((Button)sender).Name);
            Nombre_Proveedor = ((Button)sender).Text;

            //Deseleccionar
            foreach (Button b in FlowpanelProveedor.Controls)
            {
                if (b is Button)
                {
                    b.BackColor = Color.FromArgb(139, 139, 139);
                }
            }
            //Seleccionar button
            foreach (Button b2 in FlowpanelProveedor.Controls)
            {
                if (b2 is Button)
                {
                    if (b2.Name == IdProveedor.ToString())
                    {
                        b2.BackColor = Color.FromArgb(81, 175, 73);
                    }
                }
            }
            lblProvedor.Text = Nombre_Proveedor;
            FlowpanelProveedor.Visible = false;
            validarEfectivo();
        }
        private void InsertarCreditoPorPagar()
        {
            LCreditosPorPagar parametros = new LCreditosPorPagar();
            Insertar_datos fn = new Insertar_datos();
            parametros.Descripcion = "Compra de productos";
            parametros.Fecha_registro = DateTime.Now;
            parametros.Fecha_vencimiento = dtFechaVenc.Value;
            parametros.Total = credito_ingresado;
            parametros.Saldo = credito_ingresado;
            parametros.Id_Proveedor = IdProveedor;
            fn.insertar_CreditoPorPagar(parametros);
            
        }
        
        private void GenerarNumeroComprobante(ref string Serializacion)
        {
            var documentoSerializado = "";
            if (Obtener_datos.IdentificarProveedorInformal(IdProveedor)== true)
                documentoSerializado = SerializacionComprobantes.GenerarSerializacioProvInformal();
            else
                documentoSerializado = SerializacionComprobantes.GenerarSerializacionCreditoFiscal();

            Serializacion = documentoSerializado;

        }
        private void confirmarCompra()
        {
            string comprobante = "";
            Dcompras.ActualizarComprobanteCompras(ref comprobante);

            string numeroserializacion = "";
            string estadoImpuestos = "";

            Obtener_datos.ObtenerEstadoImpuestos(ref estadoImpuestos);
            if (estadoImpuestos == "SI")
            {
                GenerarNumeroComprobante(ref numeroserializacion);
            }
            else
            {
                numeroserializacion = comprobante;
            }


          var funcion = new Dcompras();
          var parametros = new Lcompras();
          parametros.Idcompra = idcompra;
          parametros.Total = totalcalculado;
          parametros.IdProveedor = IdProveedor;
            parametros.TipoPago = TipoCompra;
            parametros.Efectivo = efectivo_ingresado;
            parametros.Credito = credito_ingresado;
            parametros.Impuestos = impuestos;
            parametros.Subtotal = subtotal;
            parametros.Modopago = Modopago;
            parametros.Comprobante = comprobante;
            parametros.NumeroComprobante = numeroserializacion;
            parametros.ImpuestoRetenido = impuestoretenido;
          if (funcion.confirmarCompra(parametros) == true)
          {
             insertarKardex();
             insertarStock();
             if (credito_ingresado > 0)
             {
                    
                    InsertarCreditoPorPagar();
             }
             MessageBox.Show("La compra se realizo correctamente.", "Suministro:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RealizarCompra.EstadoRealizarCompra = "SI";
                Dispose();
          }
        }
        
        private void insertarKardex()
        {
            var parametros = new LKardex();
            var funcion = new Dkardex();
            parametros.Fecha = DateTime.Now;
            parametros.Motivo = "COMPRAS";
            foreach (DataGridViewRow rdr in dgDetallecompra.Rows)
            {
                double cantidad = Convert.ToDouble(rdr.Cells["Cantidad"].Value);
                int idproducto = Convert.ToInt32(rdr.Cells["Id_Producto"].Value);
                parametros.Cantidad = cantidad;
                parametros.Id_producto = idproducto;
                funcion.insertar_KARDEX_Entrada(parametros);

            }

        }
        private void insertarStock()
        {
            var parametros = new Lproductos();
            var funcion = new Dproductos();
            foreach (DataGridViewRow rdr in dgDetallecompra.Rows)
            {
                double cantidad = Convert.ToDouble(rdr.Cells["Cantidad"].Value);
                int idproducto = Convert.ToInt32(rdr.Cells["Id_Producto"].Value);
                parametros.Stock = cantidad.ToString();
                parametros.Id_Producto1 = idproducto;
                funcion.aumentarStock(parametros);

            }
           
        }
        private void mostrarDetallecompra()
        {
            var dt = new DataTable();
            var funcion = new Ddetallecompra();
            var parametros = new Ldetallecompra();
            parametros.IdCompra = idcompra;
            funcion.mostrar_DetalleCompra(ref dt, parametros);
            dgDetallecompra.DataSource = dt;
            dgDetallecompra.Columns[1].Visible = false;
            dgDetallecompra.Columns[2].Visible = false;
            dgDetallecompra.Columns[3].Visible = false;
            dgDetallecompra.Columns[8].Visible = false;
            dgDetallecompra.Columns[9].Visible = false;
            Bases.Multilinea(ref dgDetallecompra);
            

        }
        private void MostrarIdProveedorGenerico()
        {
           
            Obtener_datos.MostrarIdProveedorGenerico(ref Id);
            
        }
        private void txtEfectivo_TextChanged(object sender, EventArgs e)
        {
            Bases.ValidateSeparatorToNumberInString(ref txtEfectivo);
            try
            {
                efectivo_ingresado = Convert.ToDouble(txtEfectivo.Text);
                credito_ingresado = Convert.ToDouble(txtCredito.Text);
                validarEfectivo();
            }
            catch (Exception ex)
            {
                efectivo_ingresado = 0;

                MessageBox.Show("Ingresa un monto valido por favor", "Validacion:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEfectivo.SelectAll();
            }
            IdentificarTipoDePago();
        }
        private void validarEfectivo()
        {
            //Variable donde se suma el total del ingreso 
            // Efectivo mas credito
            totalcalculado = efectivo_ingresado + credito_ingresado;
            bool estadoValidar = true;
            //---
            //Primero obtengo el id proveedor generico
            MostrarIdProveedorGenerico();
            if (Id == IdProveedor)
            {
                if(credito_ingresado > 0)
                {
                    //Se identifica si el total es menor o igual al total a pagar
                    if (totalcalculado <= total)
                    {
                        FlowpanelProveedor.Visible = true;
                        estadoValidar = false;
                        lblAdvertecia.Text = "Selecciona un proveedor para comprar a credito";
                        dibujarProveedores();
                    }
                   
                }
            }
            //Se identifica lo que resta es decir lo pagado menos el total a pagar 
            double calculo = (total - totalcalculado);
            if (calculo < 0)
            {
                calculo = 0;
            }
                
            lblResta.Text = calculo.ToString();

            if (estadoValidar == true)
            {
                //Aqui identifico de que forma esta pagando
                Identificar_Pago();

                resta = (total - totalcalculado);
                if (totalcalculado > total)
                {
                    lblAdvertecia.Text = "La cantidad supera el total.";
                    lblAdvertecia.ForeColor = Color.Red;
                    Estado_Para_compar = false;
                }
                if (totalcalculado < total)
                {
                    lblAdvertecia.Text = "Pago incompleto.";
                    lblAdvertecia.ForeColor = Color.Red;
                    Estado_Para_compar = false;
                }
                if (totalcalculado == total)
                {
                    lblAdvertecia.Text = "Correcto.";
                    lblAdvertecia.ForeColor = Color.FromArgb(0, 192, 0);
                    Estado_Para_compar = true;
                    FlowpanelProveedor.Visible = false;
                }
            }
            
        }
        private void Identificar_Pago()
        {
            //---Variable
            if (efectivo_ingresado > 0 && credito_ingresado > 0)
            {
                TipoCompra = "MIXTO";
            }
            if (efectivo_ingresado > 0 && credito_ingresado == 0)
            {
                TipoCompra = "Efectivo";
            }
            if (credito_ingresado > 0 && efectivo_ingresado == 0)
            {
                TipoCompra = "Credito";
            }
        }
        private void MediosPago_Load(object sender, EventArgs e)
        {
            lblProvedor.Text = Nombre_Proveedor;
            txtCredito.Text = "0.00";
            txtEfectivo.Text = "0.00";
            lblTotal.Text = total.ToString();
            txtEfectivo.Text = total.ToString();
            mostrarDetallecompra();
            
        }

        private void label10_Click(object sender, EventArgs e)
        {
            FlowpanelProveedor.Visible = true;
            dibujarProveedores();
        }

        private void txtCredito_TextChanged(object sender, EventArgs e)
        {
            Bases.ValidateSeparatorToNumberInString(ref txtCredito);
            if (txtCredito.Text == "")
            {
                txtCredito.Text = "0.00";
            }
            if(txtEfectivo.Text == "")
            {
                txtEfectivo.Text = "0.00";
            }
            try
            {
               
                efectivo_ingresado = Convert.ToDouble(txtEfectivo.Text);
                credito_ingresado = Convert.ToDouble(txtCredito.Text);
                validarEfectivo();
            }
            catch (Exception ex)
            {
                efectivo_ingresado = 0;

                MessageBox.Show("Ingresa un monto valido por favor txtcredito", "Validacion:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEfectivo.SelectAll();
            }
            IdentificarTipoDePago();
        }

        private void FlowpanelProveedor_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Pagar()
        {
            if (Estado_Para_compar == true)
            {
                confirmarCompra();
            }
            else
            {
                MessageBox.Show(lblAdvertecia.Text, "Verifica:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void btnPagar_Click(object sender, EventArgs e)
        {
            Pagar();
        }
        private void IdentificarTipoDePago()
        {
            Modopago = "";
            if (efectivo_ingresado > 0 && credito_ingresado <= 0)
            {
                Modopago = "EFECTIVO";
            }
            if(credito_ingresado > 0 && efectivo_ingresado <=0)
            {
                Modopago = "CREDITO";
            }
            if(credito_ingresado>0 && efectivo_ingresado>0)
            {
                Modopago = "MIXTO";
            }
            if(credito_ingresado == 0 && efectivo_ingresado == 0)
            {
                Modopago = "NINGUNO";
            }
            
            if (Modopago == "EFECTIVO" || Modopago == "NINGUNO")
            {
                panelFechaCredito.Visible = false;
            }
            else
            {
                panelFechaCredito.Visible = true;
            }
        }
    }
}
