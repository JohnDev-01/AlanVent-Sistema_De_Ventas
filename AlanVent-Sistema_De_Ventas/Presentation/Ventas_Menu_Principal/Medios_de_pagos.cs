using AlanVent_Sistema_De_Ventas.DataAccess;
using AlanVent_Sistema_De_Ventas.Logic;
using AlanVent_Sistema_De_Ventas.Presentation.Reportes.Ticket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.Reporting.Processing;

namespace AlanVent_Sistema_De_Ventas.Presentation.Ventas_Menu_Principal
{
    public partial class Medios_de_pagos : Form
    {
        public Medios_de_pagos()
        {
            InitializeComponent();
        }
        private PrintDocument DOCUMENTO;
        string moneda;
        int idcliente;
        int idventa;
        double totalAPagar;
        double vuelto = 0;
        double efectivo_calculado = 0;
        double restante = 0;
        int INDICADOR_DE_FOCO;
        bool SECUENCIA1 = true;
        bool SECUENCIA2 = true;
        bool SECUENCIA3 = true;
        string indicador;
        string indicador_de_tipo_de_pago_string;
        string txttipo;
        string TXTTOTAL_STRING;
        string lblproceso;
        double credito = 0;
        int idcomprobante;
        string lblSerialPC;
        string NombreCliente;
        string TipoComprobanteFiscal;

        public static string Estado_Venta_Si_Es_Confirmada = "NO CONFIRMADA";
        //Impuestos
        public string estadoImpuesto;
        public double porcentaje;
        public double totalImpuesto;
        public double subtotal;
        int idclienteEstandar;
        //Cobros
        string No_venta = "";
        private string TipoDeIdentificacion;
       
        private void Medios_de_pagos_Load(object sender, EventArgs e)
        {
            cambiar_el_formato_de_separador_de_decimales();
            MOSTRAR_comprobante_serializado_POR_DEFECTO();
            Validar_tipos_de_comprobantes();
            mostrar_moneda_de_empresa();
            configuraciones_de_diseño();
            Bases.Obtener_serialPc(ref lblSerialPC);
            Obtener_id_de_venta();
            cargar_impresoras_del_equipo();
            MOSTRAR_cliente_standar();
            calcular_restante();
        }
        void calcular_restante()
        {
            try
            {
                double efectivo = 0;
                double tarjeta = 0;

                #region Validation string to number 
                //Validacion de espacios vacios 
                if (txtefectivo2.Text == "")
                {
                    efectivo = 0;
                }
                else
                {
                    efectivo = Convert.ToDouble(txtefectivo2.Text);
                }

                if (txtcredito2.Text == "")
                {
                    credito = 0;
                }
                else
                {
                    credito = Convert.ToDouble(txtcredito2.Text);
                }

                if (txttarjeta2.Text == "")
                {
                    tarjeta = 0;
                }
                else
                {
                    tarjeta = Convert.ToDouble(txttarjeta2.Text);
                }

                //Validacion de Ceros

                if (txtefectivo2.Text == "0.00")
                {
                    efectivo = 0;
                }
                if (txtcredito2.Text == "0.00")
                {
                    credito = 0;
                }
                if (txttarjeta2.Text == "0.00")
                {
                    tarjeta = 0;

                }
                //Validacion de puntos 


                if (txtefectivo2.Text == ".")
                {
                    efectivo = 0;
                }
                if (txtcredito2.Text == ".")
                {
                    tarjeta = 0;
                }
                if (txttarjeta2.Text == ".")
                {
                    credito = 0;
                }
                #endregion

                /*
                 * EFECTIVO    1000
                 * Total pagar 300
                 * credito     100
                 * tarjeta     100
                 * 
                 * 
                 * 200 - (300 + 25 + 25) = 0 efectivo calculado
                 * 
                 * 600 - (300 - 100 - 100) = 500 de vuelta
                 */


                try
                {
                    if (efectivo > totalAPagar)
                    {
                        efectivo_calculado = efectivo - (totalAPagar + credito + tarjeta);
                        if (efectivo_calculado <= 0)
                        {
                            vuelto = 0;
                            TXTVUELTO.Text = Bases.AsignarComa(vuelto);
                            restante = efectivo_calculado;
                            txtrestante.Text = restante.ToString();
                        }
                        else
                        {
                            vuelto = efectivo - (totalAPagar - credito - tarjeta);
                            TXTVUELTO.Text = Bases.AsignarComa(vuelto);
                            // AQUI OBTENGO LO QUE QUEDARIA POR PAGAR, MAYORMENTE ES PARA QUEDAR 
                            //EN CERO PORQUE DE NO SER ASI SE MANDARA UNA ADVERTENCIA QUE AUMENTE EL CREDITO
                            restante = efectivo - (totalAPagar + credito + tarjeta + efectivo_calculado);
                            txtrestante.Text = Convert.ToString(restante);
                            txtrestante.Text = decimal.Parse(txtrestante.Text).ToString("##0.00");
                        }

                    }
                    else
                    {
                        vuelto = 0;
                        TXTVUELTO.Text = "0";
                        efectivo_calculado = efectivo;
                        restante = totalAPagar - efectivo_calculado - credito - tarjeta;
                        txtrestante.Text = Convert.ToString(restante);
                        txtrestante.Text = decimal.Parse(txtrestante.Text).ToString("##0.00");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void cargar_impresoras_del_equipo()
        {
            txtImpresora.Items.Clear();
            for (var I = 0; I < PrinterSettings.InstalledPrinters.Count; I++)
            {
                txtImpresora.Items.Add(PrinterSettings.InstalledPrinters[I]);
            }
            txtImpresora.Items.Add("Ninguna");

            Obtener_datos.Mostrar_impresora_Predeterminada(ref txtImpresora);
        }

        void Obtener_id_de_venta()
        {
            idventa = frm_VENTAS_MENU_PRINCIPAL.Idventa;
        }
        void configuraciones_de_diseño()
        {
            TXTVUELTO.Text = "0.0";
            txtrestante.Text = "0.0";
            TXTTOTAL.Text = moneda + " " + AsignarComa(frm_VENTAS_MENU_PRINCIPAL.total);
            totalAPagar = Ventas_Menu_Principal.frm_VENTAS_MENU_PRINCIPAL.total;
            txtefectivo2.Text = totalAPagar.ToString();
            idcliente = 0;

        }
        void mostrar_moneda_de_empresa()
        {
            SqlCommand cmd = new SqlCommand("Select Moneda From Empresa", DataAccess.ConexionMaestra.conectar);
            try
            {
                DataAccess.ConexionMaestra.abrir();
                moneda = Convert.ToString(cmd.ExecuteScalar());
                DataAccess.ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void MOSTRAR_comprobante_serializado_POR_DEFECTO()
        {
            SqlCommand cmd = new SqlCommand("select tipodoc from Serializacion Where Por_defecto='SI'", DataAccess.ConexionMaestra.conectar);
            try
            {
                DataAccess.ConexionMaestra.abrir();
               var tipocomprobante = Convert.ToString(cmd.ExecuteScalar());
                DataAccess.ConexionMaestra.cerrar();

                if (tipocomprobante == "TICKET")
                {
                    rbticket.Checked = true;
                }
                else
                {
                    rbFactura.Checked = true;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            //dibujarCOMPROBANTES();
        }
        //private void dibujarCOMPROBANTES()
        //{
        //    FlowLayoutPanel3.Controls.Clear();
        //    try
        //    {
        //        DataAccess.ConexionMaestra.abrir();
        //        string query = "select tipodoc from Serializacion where Destino='VENTAS'";
        //        SqlCommand cmd = new SqlCommand(query, DataAccess.ConexionMaestra.conectar);
        //        SqlDataReader rdr = cmd.ExecuteReader();
        //        while (rdr.Read())
        //        {
        //            Button b = new Button();
        //            b.Text = rdr["tipodoc"].ToString();
        //            b.Size = new System.Drawing.Size(191, 60);
        //            b.BackColor = Color.FromArgb(70, 70, 71);
        //            b.Font = new System.Drawing.Font("Segoe UI", 13);
        //            b.FlatStyle = FlatStyle.Flat;
        //            b.ForeColor = Color.WhiteSmoke;
        //            FlowLayoutPanel3.Controls.Add(b);
        //            if (b.Text == lblComprobante.Text)
        //            {
        //                b.Visible = false;
        //            }
        //            b.Click += miEvento;
        //        }
        //        DataAccess.ConexionMaestra.cerrar();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.StackTrace);
        //    }
        //}
        private void miEvento(object sender, EventArgs e)
        {
            lblComprobante.Text = ((Button)sender).Text;
            //dibujarCOMPROBANTES();
            Validar_tipos_de_comprobantes();
            identificar_el_tipo_de_pago();
            validarPedidodeCliente();
        }



        private void validarPedidodeCliente()
        {

            if (lblComprobante.Text == "FACTURA" && txttipo == "CREDITO")
            {
                panelCliente.Visible = false;
            }
            if (lblComprobante.Text == "FACTURA" && txttipo == "EFECTIVO")
            {
                panelCliente.Visible = true;
                lblindicador_de_factura_1.Text = "Cliente: (Obligatorio)";
                lblindicador_de_factura_1.ForeColor = Color.FromArgb(255, 192, 192);

            }
            else if (lblComprobante.Text != "FACTURA" && txttipo == "EFECTIVO")
            {
                panelCliente.Visible = true;
                lblindicador_de_factura_1.Text = "Cliente: (Opcional)";
                lblindicador_de_factura_1.ForeColor = Color.DimGray;

            }

            if (lblComprobante.Text == "FACTURA" && txttipo == "TARJETA")
            {
                panelCliente.Visible = true;
                lblindicador_de_factura_1.Text = "Cliente: (Obligatorio)";
                lblindicador_de_factura_1.ForeColor = Color.FromArgb(255, 192, 192);

            }
            else if (lblComprobante.Text != "FACTURA" && txttipo == "TARJETA")
            {
                panelCliente.Visible = true;
                lblindicador_de_factura_1.Text = "Cliente: (Opcional)";
                lblindicador_de_factura_1.ForeColor = Color.DimGray;
            }


        }
        void identificar_el_tipo_de_pago()
        {
            int indicadorEfectivo = 4;
            int indicadorCredito = 2;
            int indicadorTarjeta = 3;

            #region Validations
            // validacion para evitar valores vacios
            if (txtefectivo2.Text == "")
            {
                txtefectivo2.Text = "0";
            }
            if (txttarjeta2.Text == "")
            {
                txttarjeta2.Text = "0";
            }
            if (txtcredito2.Text == "")
            {
                txtcredito2.Text = "0";
            }
            //validacion de .
            if (txtefectivo2.Text == ".")
            {
                txtefectivo2.Text = "0";
            }
            if (txttarjeta2.Text == ".")
            {
                txttarjeta2.Text = "0";
            }
            if (txtcredito2.Text == ".")
            {
                txtcredito2.Text = "0";
            }
            //validacion de 0
            if (txtefectivo2.Text == "0")
            {
                indicadorEfectivo = 0;
            }
            if (txttarjeta2.Text == "0")
            {
                indicadorTarjeta = 0;
            }
            if (txtcredito2.Text == "0")
            {
                indicadorCredito = 0;
            }
            #endregion

            //calculo de indicador
            int calculo_identificacion = indicadorCredito + indicadorEfectivo + indicadorTarjeta;
            //consulta al identificador
            if (calculo_identificacion == 4)
            {
                indicador_de_tipo_de_pago_string = "EFECTIVO";
            }
            if (calculo_identificacion == 2)
            {
                indicador_de_tipo_de_pago_string = "CREDITO";
            }
            if (calculo_identificacion == 3)
            {
                indicador_de_tipo_de_pago_string = "TARJETA";
            }
            if (calculo_identificacion > 4)
            {
                indicador_de_tipo_de_pago_string = "MIXTO";
            }
            txttipo = indicador_de_tipo_de_pago_string;

        }
        void Validar_tipos_de_comprobantes()
        {
            var dt = new DataTable();
            buscar_Tipo_de_documentos_para_insertar_en_ventas(ref dt);
            try
            {
                foreach (DataRow item in dt.Rows)
                {

                    txtserie.Text = item["Serie"].ToString();
                    int numerofin = Convert.ToInt32(item["numerofin"].ToString());
                    idcomprobante = Convert.ToInt32(item["Id_serializacion"].ToString());
                    txtnumerofin.Text = Convert.ToString(numerofin);
                    lblCantidad_de_numeros.Text = item["Cantidad_de_numeros"].ToString();
                    lblCorrelativoconCeros.Text = DataAccess.Agregar_ceros_adelante_De_numero.Ceros(txtnumerofin.Text, Convert.ToInt32(lblCantidad_de_numeros.Text));
                }
            }
            catch (Exception ex)
            {

            }
            Editar_datos.ElegirComprobantePorDefectoVentas(idcomprobante);
        }
        void buscar_Tipo_de_documentos_para_insertar_en_ventas(ref DataTable dt)
        {
            try
            {
                DataAccess.ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscar_Tipo_de_documentos_para_insertar_en_ventas", DataAccess.ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", lblComprobante.Text);
                da.Fill(dt);
                DataAccess.ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
            }
        }
        public void cambiar_el_formato_de_separador_de_decimales()
        {
            DataAccess.cambiar_el_formato_de_separador_de_decimales.cambiar();
        }
        private void AsignarValueNumber()
        {
            txtefectivo2.Text = txtefectivo2.Text;
            txttarjeta2.Text = txttarjeta2.Text;
            txtcredito2.Text = txtcredito2.Text;
          
        }
        private void txtefectivo2_TextChanged(object sender, EventArgs e)
        {
            AsignarValueNumber();
            Bases.ValidateSeparatorToNumberInString(ref txtefectivo2);
            calcular_restante();
        }

        private void txttarjeta2_TextChanged(object sender, EventArgs e)
        {
            AsignarValueNumber();
            Bases.ValidateSeparatorToNumberInString(ref txttarjeta2);
            calcular_restante();
        }

        private void txtcredito2_TextChanged(object sender, EventArgs e)
        {
            AsignarValueNumber();
            Bases.ValidateSeparatorToNumberInString(ref txtcredito2);
            calcular_restante();
            ValidarPanelCredito();

        }
        void ValidarPanelCredito()
        {
            try
            {
                double textocredito = 0;
                if (txtcredito2.Text == ".")
                {
                    textocredito = 0;
                }
                if (txtcredito2.Text == "")
                {
                    textocredito = 0;
                }
                else
                {
                    textocredito = Convert.ToDouble(txtcredito2.Text);
                }

                if (textocredito > 0)
                {
                    AlertaSeleccionaClienteObligatorio();
                    panelCliente.Visible = true;
                }
                else
                {
                    panelCliente.Visible = true;
                    AlertaSeleccionaClienteOpcional();
                    idcliente = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AlertaSeleccionaClienteOpcional()
        {
            lblindicador_de_factura_1.Text = "Cliente: (Opcional)";
            lblindicador_de_factura_1.ForeColor = Color.DimGray;
            panelFechaCredito.Visible = false;
        }
        private void AlertaSeleccionaClienteObligatorio()
        {
            lblindicador_de_factura_1.Text = "Cliente: (Obligatorio)";
            lblindicador_de_factura_1.ForeColor = Color.Red;
            panelFechaCredito.Visible = true;
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                txtefectivo2.Text = txtefectivo2.Text + "1";
            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                txttarjeta2.Text = txttarjeta2.Text + "1";
            }
            else if (INDICADOR_DE_FOCO == 3)
            {
                txtcredito2.Text = txtcredito2.Text + "1";
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                txtefectivo2.Text = txtefectivo2.Text + "2";
            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                txttarjeta2.Text = txttarjeta2.Text + "2";
            }
            else if (INDICADOR_DE_FOCO == 3)
            {
                txtcredito2.Text = txtcredito2.Text + "2";
            }
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                txtefectivo2.Text = txtefectivo2.Text + "3";
            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                txttarjeta2.Text = txttarjeta2.Text + "3";
            }
            else if (INDICADOR_DE_FOCO == 3)
            {
                txtcredito2.Text = txtcredito2.Text + "3";
            }
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                txtefectivo2.Text = txtefectivo2.Text + "4";
            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                txttarjeta2.Text = txttarjeta2.Text + "4";
            }
            else if (INDICADOR_DE_FOCO == 3)
            {
                txtcredito2.Text = txtcredito2.Text + "4";
            }
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                txtefectivo2.Text = txtefectivo2.Text + "5";
            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                txttarjeta2.Text = txttarjeta2.Text + "5";
            }
            else if (INDICADOR_DE_FOCO == 3)
            {
                txtcredito2.Text = txtcredito2.Text + "5";
            }
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                txtefectivo2.Text = txtefectivo2.Text + "6";
            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                txttarjeta2.Text = txttarjeta2.Text + "6";
            }
            else if (INDICADOR_DE_FOCO == 3)
            {
                txtcredito2.Text = txtcredito2.Text + "6";
            }
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                txtefectivo2.Text = txtefectivo2.Text + "7";
            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                txttarjeta2.Text = txttarjeta2.Text + "7";
            }
            else if (INDICADOR_DE_FOCO == 3)
            {
                txtcredito2.Text = txtcredito2.Text + "7";
            }
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                txtefectivo2.Text = txtefectivo2.Text + "8";
            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                txttarjeta2.Text = txttarjeta2.Text + "8";
            }
            else if (INDICADOR_DE_FOCO == 3)
            {
                txtcredito2.Text = txtcredito2.Text + "8";
            }
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                txtefectivo2.Text = txtefectivo2.Text + "9";
            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                txttarjeta2.Text = txttarjeta2.Text + "9";
            }
            else if (INDICADOR_DE_FOCO == 3)
            {
                txtcredito2.Text = txtcredito2.Text + "9";
            }
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                txtefectivo2.Text = txtefectivo2.Text + "0";
            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                txttarjeta2.Text = txttarjeta2.Text + "0";
            }
            else if (INDICADOR_DE_FOCO == 3)
            {
                txtcredito2.Text = txtcredito2.Text + "0";
            }
        }

        private void btnpunto_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                if (SECUENCIA1 == true)
                {
                    txtefectivo2.Text = txtefectivo2.Text + ".";
                    SECUENCIA1 = false;
                }

                else
                {
                    return;
                }

            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                if (SECUENCIA2 == true)
                {
                    txttarjeta2.Text = txttarjeta2.Text + ".";
                    SECUENCIA2 = false;
                }

                else
                {
                    return;
                }

            }
            else if (INDICADOR_DE_FOCO == 3)
            {
                if (SECUENCIA3 == true)
                {
                    txtcredito2.Text = txtcredito2.Text + ".";
                    SECUENCIA3 = false;
                }

                else
                {
                    return;
                }

            }

        }

        private void btnborrartodo_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                txtcredito2.Clear();
                SECUENCIA1 = true;
            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                txttarjeta2.Clear();
                SECUENCIA2 = true;
            }
            else if (INDICADOR_DE_FOCO == 3)
            {
                txtcredito2.Clear();
                SECUENCIA3 = true;
            }
        }

        private void txtclientesolicitabnte3_TextChanged(object sender, EventArgs e)
        {
            if (txtclientesolicitabnte3.Text == "")
            {
                datalistadoclientes3.Visible = false;

            }
            else
            {
                buscarclientes3();
                datalistadoclientes3.Visible = true;
                datalistadoclientes3.BringToFront();
            }


        }

        private void txtclientesolicitabnte2_TextChanged(object sender, EventArgs e)
        {


        }

        void buscarclientes3()
        {
            try
            {
                DataTable dt = new DataTable();
                Obtener_datos.buscar_clientes(ref dt, txtclientesolicitabnte3.Text);
                datalistadoclientes3.DataSource = dt;
                datalistadoclientes3.Columns[1].Visible = false;
                datalistadoclientes3.Columns[3].Visible = false;
                datalistadoclientes3.Columns[4].Visible = false;
                datalistadoclientes3.Columns[5].Visible = false;
                datalistadoclientes3.Columns[2].Width = 420;
                DataAccess.ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {

            }
        }

        private void datalistadoclientes2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            PanelregistroClientes.Visible = true;
            PanelregistroClientes.Dock = DockStyle.Fill;
            PanelregistroClientes.BringToFront();
            limpiar_datos_de_registrodeclientes();
        }
        void limpiar_datos_de_registrodeclientes()
        {
            txtnombrecliente.Clear();
            txtDireccionCliente.Clear();
            txtcelular.Clear();
            txtCedula.Clear();
            txtRNC.Clear();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            insertar_cliente();
            
        }
        private void rellenarCamposVaciosClientes()
        {
            if (string.IsNullOrEmpty(txtcelular.Text)) { txtcelular.Text = "-"; };
            if (string.IsNullOrEmpty(txtDireccionCliente.Text)) { txtDireccionCliente.Text = "-"; };
        }
        private bool ValidarNumeroDeDocumento()
        {
            bool estado = false;

            int numValidation = 2;
            if (rbCedula.Checked == true && (txtCedula.Text.Length) - 2 != 11)
            {
                MessageBox.Show("Digita un numero de cedula valido", "Valida:",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                numValidation++;
            }
            else
            {
                numValidation--;
            }
            if (rbRnc.Checked == true && (txtRNC.Text.Length) != 9)
            {
                MessageBox.Show("Digita un RNC valido", "Valida:",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                numValidation++;
            }
            else
            {
                numValidation--;
            }

            if (numValidation == 0)
                estado = true;
            else
                estado = false;

            return estado;
        }
        
        private void GuardarCliente()
        {
            Lclientes parametros = new Lclientes();
            Insertar_datos funcion = new Insertar_datos();
            parametros.Nombre = txtnombrecliente.Text;
            parametros.Celular = txtcelular.Text;
            parametros.Direccion = txtDireccionCliente.Text;
            parametros.Tipoidentificacion = TipoDeIdentificacion;
            parametros.Cedula = txtCedula.Text;
            parametros.Rnc = txtRNC.Text;
            if (funcion.Insertar_clientes(parametros) == true)
            {
                PanelregistroClientes.Visible = false;
                panelInformacionCobro.Visible = true;
            }

        }
        void insertar_cliente()
        {
            if (!string.IsNullOrEmpty(txtnombrecliente.Text))
            {
                rellenarCamposVaciosClientes();
                if (ValidarNumeroDeDocumento() == true)
                {
                    GuardarCliente();
                }
            }
            else
            {
                MessageBox.Show("Ingrese un nombre", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void BtnVolver_Click(object sender, EventArgs e)
        {
            PanelregistroClientes.Visible = false;
            panelInformacionCobro.Visible = true;
        }

        private void txtefectivo2_Click(object sender, EventArgs e)
        {
            calcular_restante();
            INDICADOR_DE_FOCO = 1;
            if (txtrestante.Text == "0.00")
            {
                txtefectivo2.Text = "";
            }
            else
            {
                txtefectivo2.Text = txtrestante.Text;
            }
        }

        private void txttarjeta2_Click(object sender, EventArgs e)
        {
            calcular_restante();
            INDICADOR_DE_FOCO = 2;
            if (txtrestante.Text == "0.00")
            {
                txttarjeta2.Text = "";
            }
            else
            {
                txttarjeta2.Text = txtrestante.Text;
            }
        }

        private void txtcredito2_Click(object sender, EventArgs e)
        {
            calcular_restante();
            INDICADOR_DE_FOCO = 3;
            if (txtrestante.Text == "0.00")
            {
                txtcredito2.Text = "";
            }
            else
            {
                txtcredito2.Text = txtrestante.Text;
                ValidarPanelCredito();
            }
        }
        private void ValidacionesCobrar()
        {
           
            if (txttipo == "EFECTIVO" && vuelto >= 0)
            {
                vender_en_efectivo();

            }
            else if (txttipo == "EFECTIVO" && vuelto < 0)
            {
                MessageBox.Show("El vuelto no puede ser menor a el Total pagado ", "Datos Incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            // condicional para que si es credito tenga un  cliente seleccionado
            var validationClienteGenerico = Validar_ClienteSeleccionadoEstandar();
            if (txttipo == "CREDITO" && validationClienteGenerico == false)
            {
                vender_en_efectivo();
            }
            else if (txttipo == "CREDITO" || txttipo == "MIXTO" && credito > 0 && validationClienteGenerico == true)
            {
                AlertaSeleccionaClienteObligatorio();
                MessageBox.Show("Por favor seleccione un cliente para pagar a credito", "Datos Incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            

            if (txttipo == "TARJETA")
            {
                vender_en_efectivo();
            }


            if (txttipo == "MIXTO" && validationClienteGenerico == false || txttipo == "MIXTO" && validationClienteGenerico == true && credito <=0)
            {
                vender_en_efectivo();
            }
        }
        void INGRESAR_LOS_DATOS()
        {
            CONVERTIR_TOTAL_A_LETRAS();
            completar_con_ceros_los_texbox_de_otros_medios_de_pago();

            //Validaciones para que al emitir una factura mayor a 250,000 se seleccione un cliente
            if (idcliente == idclienteEstandar && efectivo_calculado >= 250000)
            {
                MessageBox.Show("Es necesario seleccionar cliente para emitir una venta mayor o igual a 250,000",
                    "Selecciona un cliente:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AlertaSeleccionaClienteObligatorio();
            }
            else
            {
                AlertaSeleccionaClienteOpcional();
                //Validacion de facturas de credito fiscal o de consumo:
                if (lblComprobante.Text == "FACTURA")
                {
                    //Validate si venden con impuestos
                    string estado = "";
                    Obtener_datos.ObtenerEstadoImpuestos(ref estado);
                    if (estado == "SI")
                    {
                        var page = new SeleccionTipoFactura();
                        page.ShowDialog();
                        TipoComprobanteFiscal = page.TipoComprobante;
                    }
                    else
                    {
                        TipoComprobanteFiscal = "CONSUMIDORFINAL";
                    }

                    if (TipoComprobanteFiscal == "CREDITOFISCAL")
                    {
                        if (idcliente == idclienteEstandar)
                        {
                            MessageBox.Show("Selecciona un cliente para poder emitir una factura de credito fiscal",
                                   "Cliente:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            AlertaSeleccionaClienteObligatorio();
                            panelFechaCredito.Visible = false;
                        }
                        else
                        {
                            AlertaSeleccionaClienteOpcional();
                            ValidacionesCobrar();
                        }

                    }
                    if (TipoComprobanteFiscal == "CONSUMIDORFINAL")
                    {
                        ValidacionesCobrar();
                    }
                }
                else
                {
                    //Si entro aqui es porque no esta seleccionando un tipo de comprobante como factura
                    TipoComprobanteFiscal = "CONSUMIDORFINAL";
                    ValidacionesCobrar();
                }
            }
        }

        private void vender_en_efectivo()
        {
            if (idcliente == 0)
            {
                MOSTRAR_cliente_standar();
            }
            if (lblComprobante.Text == "FACTURA" && idcliente == 0 && txttipo != "CREDITO")
            {
                MessageBox.Show("Seleccione un Cliente, para Facturas es Obligatorio", "Datos Incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (lblComprobante.Text == "FACTURA" && idcliente != 0)
            {
                procesar_venta_efectivo();
            }

            else if (lblComprobante.Text != "FACTURA" && txttipo != "CREDITO")
            {
                procesar_venta_efectivo();
            }
            else if (lblComprobante.Text != "FACTURA" && txttipo == "CREDITO")
            {
                procesar_venta_efectivo();
            }
        }
        void procesar_venta_efectivo()
        {
            
            actualizar_serie_mas_uno();
            Validar_tipos_de_comprobantes();
            CONFIRMAR_VENTA_EFECTIVO();
            if (lblproceso == "PROCEDE")
            {
                reportViewer1.Focus();
                frm_VENTAS_MENU_PRINCIPAL.EstadoMediosPago = true;
                disminuir_stock_productos();
                INSERTAR_KARDEX_SALIDA();
                aumentar_monto_a_cliente();
                Validar_tipo_de_impresion();
                reportViewer1.Focus();
            }
        }
        void INSERTAR_KARDEX_SALIDA()
        {
            try
            {
                foreach (DataGridViewRow row in datalistadoDetalleVenta.Rows)
                {
                    int Id_producto = Convert.ToInt32(row.Cells["Id_producto"].Value);
                    double cantidad = Convert.ToDouble(row.Cells["Cant"].Value);
                    string STOCK = Convert.ToString(row.Cells["Stock"].Value);
                    if (STOCK != "Ilimitado")
                    {
                        DataAccess.ConexionMaestra.abrir();
                        SqlCommand cmd = new SqlCommand("insertar_KARDEX_SALIDA", DataAccess.ConexionMaestra.conectar);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Fecha", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Motivo", "Venta #" + lblComprobante.Text + " " + lblCorrelativoconCeros.Text);
                        cmd.Parameters.AddWithValue("@Cantidad ", cantidad);
                        cmd.Parameters.AddWithValue("@Id_producto", Id_producto);
                        cmd.Parameters.AddWithValue("@Id_usuario", frm_VENTAS_MENU_PRINCIPAL.idusuario_que_inicio_sesion);
                        cmd.Parameters.AddWithValue("@Tipo", "SALIDA");
                        cmd.Parameters.AddWithValue("@Estado", "DESPACHO CONFIRMADO");
                        cmd.Parameters.AddWithValue("@Id_caja", frm_VENTAS_MENU_PRINCIPAL.Id_caja);
                        cmd.ExecuteNonQuery();
                        DataAccess.ConexionMaestra.cerrar();

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.StackTrace);
            }
        }
        void mostrar_productos_agregados_a_venta()
        {
            try
            {
                DataTable dt = new DataTable();
                DataAccess.ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_productos_agregados_a_venta", DataAccess.ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idventa", idventa);
                da.Fill(dt);
                datalistadoDetalleVenta.DataSource = dt;
                DataAccess.ConexionMaestra.cerrar();

            }
            catch (Exception ex)
            {
                DataAccess.ConexionMaestra.cerrar();
                MessageBox.Show(ex.Message);
            }
        }
        void disminuir_stock_productos()
        {
            mostrar_productos_agregados_a_venta();
            foreach (DataGridViewRow row in datalistadoDetalleVenta.Rows)
            {
                int idproducto = Convert.ToInt32(row.Cells["Id_producto"].Value);
                double cantidad = Convert.ToInt32(row.Cells["Cant"].Value);
                try
                {

                    DataAccess.ConexionMaestra.abrir();
                    SqlCommand cmd = new SqlCommand("disminuir_stock", DataAccess.ConexionMaestra.conectar);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idproducto", idproducto);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.ExecuteNonQuery();
                    DataAccess.ConexionMaestra.cerrar();
                }
                catch (Exception ex)
                {
                    DataAccess.ConexionMaestra.cerrar();
                    MessageBox.Show(ex.Message);
                }
            }


        }
        void actualizar_serie_mas_uno()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataAccess.ConexionMaestra.abrir();
                cmd = new SqlCommand("actualizar_serializacion_mas_uno", DataAccess.ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idserie", idcomprobante);
                cmd.ExecuteNonQuery();
                DataAccess.ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void Validar_tipo_de_impresion()
        {
            if (indicador == "VISTA PREVIA")
            {
                if (lblComprobante.Text == "FACTURA")
                {
                   
                    mostrar_Factura_impresa_VISTA_PREVIA();
                }
                else
                {
                    mostrar_ticket_impreso_VISTA_PREVIA();
                }

            }
            else if (indicador == "DIRECTO")
            {
                //La validacion si es una factura o ticket se hace dentro de este proceso
                imprimir_directo();
            }
        }
        void imprimir_directo()
        {
            dynamic rpt = "";

            //Validaciones de impresion de documento, aqui se valida que tipo
            //de Comprobante es y si es una factura se valida lo que es un 
            //Factura de consumo o de credito fiscal
            if (lblComprobante.Text == "FACTURA")
            {
                
                if (TipoComprobanteFiscal == "CONSUMIDORFINAL")
                {
                    rpt = new Reportes.Impresion_de_comprobantes.Factura.Facturarpt();
                }
                else if (TipoComprobanteFiscal == "CREDITOFISCAL")
                {
                    rpt = new Reportes.Impresion_de_comprobantes.Factura.rtpFacturaCreditoFiscal();
                }

                DataTable dt = new DataTable();
                try
                {
                    DataAccess.ConexionMaestra.abrir();
                    SqlDataAdapter da = new SqlDataAdapter("mostrar_factura_impresa", DataAccess.ConexionMaestra.conectar);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Id_venta", idventa);
                    da.SelectCommand.Parameters.AddWithValue("@total_en_letras", txtnumeroconvertidoenletra.Text);

                    da.Fill(dt);
                    rpt.table1.DataSource = dt;
                    rpt.DataSource = dt;
                    DataAccess.ConexionMaestra.cerrar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Message: " + ex.Message);
                }
            }
            else
            {
                
                mostrar_Ticket_lleno(ref rpt);

            }
            //Impresion de documento
            try
            {
                DOCUMENTO = new PrintDocument();
                DOCUMENTO.PrinterSettings.PrinterName = txtImpresora.Text;
                if (DOCUMENTO.PrinterSettings.IsValid)
                {
                    PrinterSettings printerSettings = new PrinterSettings();
                    printerSettings.PrinterName = txtImpresora.Text;
                    ReportProcessor reportProcessor = new ReportProcessor();
                    reportProcessor.PrintReport(rpt, printerSettings);
                }
                Dispose();
            }
            catch (Exception ex)
            {

            }
        }
        
        void mostrar_Ticket_lleno(ref dynamic rpt)
        {

            DataTable dt = new DataTable();
            try
            {

                DataAccess.ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_ticket_impreso", DataAccess.ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id_venta", idventa);
                da.SelectCommand.Parameters.AddWithValue("@total_en_letras", txtnumeroconvertidoenletra.Text);
                da.SelectCommand.Parameters.AddWithValue("@porcentaje", porcentaje.ToString()) ;
                da.Fill(dt);

                #region Validacion si codigo qr es visible
                var textQr = "";
                bool valueVisible = true;
                foreach (DataRow row in dt.Rows)
                {
                    textQr = row["RedSocial"].ToString();
                }
                if (textQr == "NO_ACEPTA_CODIGOQR")
                    valueVisible = false;
                else
                    valueVisible = true;
                #endregion


                rpt = new Ticket();
                rpt.table1.DataSource = dt;
                rpt.barcode2.Visible = valueVisible;
                rpt.DataSource = dt;
                DataAccess.ConexionMaestra.cerrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void mostrar_ticket_impreso_VISTA_PREVIA()
        {
            panelInformacionCobro.Visible = false;
            PanelImpresionvistaprevia.Visible = true;
            PanelImpresionvistaprevia.Dock = DockStyle.Fill;
            panelDetalles.Visible = false;
            panelTotal.Visible = false;
            Reportes.Ticket.Ticket rpt = new Reportes.Ticket.Ticket();
            DataTable dt = new DataTable();
            try
            {
                DataAccess.ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_ticket_impreso", DataAccess.ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id_venta", idventa);
                da.SelectCommand.Parameters.AddWithValue("@total_en_letras", txtnumeroconvertidoenletra.Text);
                da.SelectCommand.Parameters.AddWithValue("@porcentaje", porcentaje.ToString());
                da.Fill(dt);

                #region Validacion si codigo qr es visible
                var textQr = "";
                bool valueVisible = true;
                foreach(DataRow row in dt.Rows)
                {
                    textQr = row["RedSocial"].ToString();
                }
                if (textQr == "NO_ACEPTA_CODIGOQR")
                    valueVisible = false;
                else
                    valueVisible = true;
                #endregion

                rpt = new Reportes.Ticket.Ticket();
                rpt.table1.DataSource = dt;
                rpt.DataSource = dt;
                rpt.barcode2.Visible = valueVisible;
                reportViewer1.Report = rpt;
                reportViewer1.RefreshReport();
                DataAccess.ConexionMaestra.cerrar();
                reportViewer1.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }

        }
        void mostrar_Factura_impresa_VISTA_PREVIA()
        {
            panelInformacionCobro.Visible = false;
            PanelImpresionvistaprevia.Visible = true;
            PanelImpresionvistaprevia.Dock = DockStyle.Fill;
            panelDetalles.Visible = false;
            panelTotal.Visible = false;

            dynamic rpt = "";
            if (TipoComprobanteFiscal == "CONSUMIDORFINAL")
            {
                rpt = new Reportes.Impresion_de_comprobantes.Factura.Facturarpt();
            }
            else if (TipoComprobanteFiscal == "CREDITOFISCAL")
            {
                rpt =new Reportes.Impresion_de_comprobantes.Factura.rtpFacturaCreditoFiscal();
            }
            
            DataTable dt = new DataTable();
            try
            {
                DataAccess.ConexionMaestra.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_factura_impresa", DataAccess.ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id_venta", idventa);
                da.SelectCommand.Parameters.AddWithValue("@total_en_letras", txtnumeroconvertidoenletra.Text);
                
                da.Fill(dt);
                rpt.table1.DataSource = dt;
                rpt.DataSource = dt;
                reportViewer1.Report = rpt;
                reportViewer1.RefreshReport();
                reportViewer1.ZoomMode = Telerik.ReportViewer.WinForms.ZoomMode.PageWidth;
                DataAccess.ConexionMaestra.cerrar();
                reportViewer1.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace+"Message: "+ex.Message);
            }

        }
        private void VentaSinImpuesto()
        {
            No_venta = (txtserie.Text + "-" + lblCorrelativoconCeros.Text);
            try
            {

                DataAccess.ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("Confirmar_ventaSinImpuesto", DataAccess.ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idventa", idventa);
                cmd.Parameters.AddWithValue("@montototal", totalAPagar);
                cmd.Parameters.AddWithValue("@IGV", 0);
                cmd.Parameters.AddWithValue("@Saldo", vuelto);
                cmd.Parameters.AddWithValue("@Tipo_de_pago", txttipo);
                cmd.Parameters.AddWithValue("@Estado", "CONFIRMADO");
                cmd.Parameters.AddWithValue("@idcliente", idcliente);
                cmd.Parameters.AddWithValue("@Comprobante", lblComprobante.Text);
                cmd.Parameters.AddWithValue("@Numero_de_doc", No_venta);
                cmd.Parameters.AddWithValue("@fecha_venta", DateTime.Now);
                cmd.Parameters.AddWithValue("@ACCION", "VENTA");
                cmd.Parameters.AddWithValue("@Fecha_de_pago", txtfecha_de_pago.Value);
                cmd.Parameters.AddWithValue("@Pago_con", Convert.ToDouble(txtefectivo2.Text));
                cmd.Parameters.AddWithValue("@Referencia_tarjeta", "NULO");
                cmd.Parameters.AddWithValue("@Vuelto", vuelto);
                cmd.Parameters.AddWithValue("@Efectivo", efectivo_calculado);
                cmd.Parameters.AddWithValue("@Credito", Convert.ToDouble(txtcredito2.Text));
                cmd.Parameters.AddWithValue("@Tarjeta", Convert.ToDouble(txttarjeta2.Text));
                cmd.Parameters.AddWithValue("@subtotal", subtotal);
                cmd.Parameters.AddWithValue("@serializacion", No_venta);
                cmd.ExecuteNonQuery();
                DataAccess.ConexionMaestra.cerrar();
                lblproceso = "PROCEDE";
                Insertar_Credito_VENTA();
            }
            catch (Exception ex)
            {
                DataAccess.ConexionMaestra.cerrar();
                lblproceso = "NO PROCEDE";
                //MessageBox.Show(ex.Message);
            }
        }
        private void Insertar_Credito_VENTA()
        {
            Logic.Lcredito_venta l = new Lcredito_venta();
            l.IdCliente = idcliente;
            l.No_Venta = No_venta;
            l.Fecha = DateTime.Now;
            l.Efectivo = efectivo_calculado;
            l.Credito = Convert.ToDouble(txtcredito2.Text);
            l.Abono = 0;
            l.Resta = Convert.ToDouble(txtcredito2.Text);
            l.Estado_pago = "NO";
            Insertar_datos.Insertar_Credito_Venta(l);
        }
        private void VentaConImpuesto()
        {
            try
            {
                No_venta = (txtserie.Text + "-" + lblCorrelativoconCeros.Text);
                string serializer = "";
                if (TipoComprobanteFiscal == "CONSUMIDORFINAL")
                {
                    serializer = SerializacionComprobantes.GenerarSerializacionIngresosConsumo();
                }
                else if (TipoComprobanteFiscal == "CREDITOFISCAL")
                {
                    serializer = SerializacionComprobantes.GenerarSerializacionCreditoFiscal();
                }

                ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("Confirmar_ventaCONImpuesto", DataAccess.ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idventa", idventa);
                cmd.Parameters.AddWithValue("@montototal", totalAPagar);
                cmd.Parameters.AddWithValue("@IGV", totalImpuesto);
                cmd.Parameters.AddWithValue("@Saldo", vuelto);
                cmd.Parameters.AddWithValue("@Tipo_de_pago", txttipo);
                cmd.Parameters.AddWithValue("@Estado", "CONFIRMADO");
                cmd.Parameters.AddWithValue("@idcliente", idcliente);
                cmd.Parameters.AddWithValue("@Comprobante", lblComprobante.Text);
                cmd.Parameters.AddWithValue("@Numero_de_doc", No_venta);
                cmd.Parameters.AddWithValue("@fecha_venta", DateTime.Now);
                cmd.Parameters.AddWithValue("@ACCION", "VENTA");
                cmd.Parameters.AddWithValue("@Fecha_de_pago", txtfecha_de_pago.Value);
                cmd.Parameters.AddWithValue("@Pago_con", Convert.ToDouble(txtefectivo2.Text));
                cmd.Parameters.AddWithValue("@Referencia_tarjeta", "NULO");
                cmd.Parameters.AddWithValue("@Vuelto", vuelto);
                cmd.Parameters.AddWithValue("@Efectivo", efectivo_calculado);
                cmd.Parameters.AddWithValue("@Credito", Convert.ToDouble(txtcredito2.Text));
                cmd.Parameters.AddWithValue("@Tarjeta", Convert.ToDouble(txttarjeta2.Text));
                cmd.Parameters.AddWithValue("@Porcentaje", porcentaje);
                cmd.Parameters.AddWithValue("@subtotal", subtotal);
                cmd.Parameters.AddWithValue("@serializacion", serializer);
                cmd.ExecuteNonQuery();
                ConexionMaestra.cerrar();
                lblproceso = "PROCEDE";
                Insertar_Credito_VENTA();
            }
            catch (Exception ex)
            {
                DataAccess.ConexionMaestra.cerrar();
                lblproceso = "NO PROCEDE";
                //MessageBox.Show(ex.Message);
            }
        }
        void CONFIRMAR_VENTA_EFECTIVO()
        {
            if (estadoImpuesto == "NO")
            {
                VentaSinImpuesto();
            }
            else if (estadoImpuesto == "SI")
            {
                VentaConImpuesto();
            }
        }
        void aumentar_monto_a_cliente()
        {
            if (credito > 0)
            {
                try
                {
                    DataAccess.ConexionMaestra.abrir();
                    SqlCommand cmd = new SqlCommand("aumentar_saldo_a_cliente", DataAccess.ConexionMaestra.conectar);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Saldo", Convert.ToDouble(txtcredito2.Text));
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);
                    cmd.ExecuteNonQuery();
                    DataAccess.ConexionMaestra.cerrar();

                }
                catch (Exception ex)
                {
                    DataAccess.ConexionMaestra.cerrar();
                    MessageBox.Show(ex.StackTrace + ex.Message);
                }
            }

        }
        void MOSTRAR_cliente_standar()
        {
            var consult = "select idclientev,Nombre from Clientes where Estado = '0'";
            var dt = new DataTable();
            var com = new SqlDataAdapter(consult, DataAccess.ConexionMaestra.conectar);
            try
            {
                DataAccess.ConexionMaestra.abrir();
                com.Fill(dt);
                DataAccess.ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            //Obtener Data
            foreach (DataRow item in dt.Rows)
            {
                idcliente = Convert.ToInt32(item["idclientev"].ToString());
                idclienteEstandar = Convert.ToInt32(item["idclientev"].ToString());
                NombreCliente = item["Nombre"].ToString();
            }
            lblNombreCliente.Text = NombreCliente;
            
        }
        private bool Validar_ClienteSeleccionadoEstandar()
        {
            var consult = "select idclientev,Nombre from Clientes where Estado = '0'";
            var dt = new DataTable();
            int IdclienteStandar = 0;
            #region Obtener id cliente standar
            var com = new SqlDataAdapter(consult, DataAccess.ConexionMaestra.conectar);
            try
            {
                DataAccess.ConexionMaestra.abrir();
                com.Fill(dt);
                DataAccess.ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            //Obtener Data
            foreach (DataRow item in dt.Rows)
            {
                IdclienteStandar = Convert.ToInt32(item["idclientev"].ToString());
            }
            #endregion
            if (idcliente == IdclienteStandar)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        void completar_con_ceros_los_texbox_de_otros_medios_de_pago()
        {
            if (txtefectivo2.Text == "")
            {
                txtefectivo2.Text = "0";
            }
            if (txtcredito2.Text == "")
            {
                txtcredito2.Text = "0";
            }
            if (txttarjeta2.Text == "")
            {
                txttarjeta2.Text = "0";
            }
            if (TXTVUELTO.Text == "")
            {
                TXTVUELTO.Text = "0";
            }
        }
        void CONVERTIR_TOTAL_A_LETRAS()
        {
            try
            {
                
                var TotalAPagar = Convert.ToDecimal(totalAPagar).ToString("##0.00");
                int numero = Convert.ToInt32(Math.Floor(Convert.ToDouble(totalAPagar)));
                TXTTOTAL_STRING = DataAccess.total_en_letras.Num2Text(numero);
                string[] a = TotalAPagar.Split('.');
                txttotaldecimal.Text = a[1];
                txtnumeroconvertidoenletra.Text = TXTTOTAL_STRING + " CON " + txttotaldecimal.Text + "/100 ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnGuardarImprimirdirecto_Click(object sender, EventArgs e)
        {
            ProcesoImprimirdirecto();
        }
        private void ProcesoImprimirdirecto()
        {
            if (restante == 0)
            {
                if (idcliente == idclienteEstandar && totalAPagar >= 250000)
                {
                    MessageBox.Show("Al superar la cantidad de 250,000 es necesario que selecciones un cliente con credenciales validas",
                        "Valida:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Estado_Venta_Si_Es_Confirmada = "CONFIRMADA";
                    Obtener_datos.Editar_eleccion_de_impresora(frm_VENTAS_MENU_PRINCIPAL.Id_caja, txtImpresora.Text);
                    indicador = "DIRECTO";
                    identificar_el_tipo_de_pago();
                    INGRESAR_LOS_DATOS();
                }
            }
            else
            {
                MessageBox.Show("El restante debe ser 0", "Datos incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ProcesoVerenpantalla()
        {
            if (restante == 0)
            {
                Estado_Venta_Si_Es_Confirmada = "CONFIRMADA";
                indicador = "VISTA PREVIA";
                identificar_el_tipo_de_pago();
                INGRESAR_LOS_DATOS();
            }
            else
            {
                MessageBox.Show("El restante debe ser 0", "Datos incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void TGuardarSinImprimir_Click(object sender, EventArgs e)
        {
            ProcesoVerenpantalla();
        }

        private void txtefectivo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtefectivo2, e);
        }

        private void txttarjeta2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txttarjeta2, e);
        }

        private void txtcredito2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtcredito2, e);
        }

        private void txtefectivo2_KeyDown(object sender, KeyEventArgs e)
        {
            EventoCobro(e);
        }
        private void EventoCobro(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcesoImprimirdirecto();
            }
            if (e.KeyCode == Keys.F1)
            {
                ProcesoVerenpantalla();
            }
        }

        private void txttarjeta2_KeyDown(object sender, KeyEventArgs e)
        {
            EventoCobro(e);
        }

        private void txtcredito2_KeyDown(object sender, KeyEventArgs e)
        {
            EventoCobro(e);
        }

        private void txtclientesolicitabnte3_KeyDown(object sender, KeyEventArgs e)
        {
            EventoCobro(e);
        }

        private void txtclientesolicitabnte2_KeyDown(object sender, KeyEventArgs e)
        {
            EventoCobro(e);
        }

        private void reportViewer1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    Dispose();
            //}
        }

        private void txtImpresora_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnagregarCliente_Click(object sender, EventArgs e)
        {
            PanelregistroClientes.Visible = true;
            panelInformacionCobro.Visible = false;
            PanelregistroClientes.Dock = DockStyle.Fill;
            PanelregistroClientes.BringToFront();
            limpiar_datos_de_registrodeclientes();
            ValidarPanelesChecked();
        }

        private void datalistadoclientes3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void datalistadoclientes3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtclientesolicitabnte3.Text = datalistadoclientes3.SelectedCells[2].Value.ToString();
            idcliente = Convert.ToInt32(datalistadoclientes3.SelectedCells[1].Value.ToString());
            NombreCliente = datalistadoclientes3.SelectedCells[2].Value.ToString();
            lblNombreCliente.Text = NombreCliente;
            datalistadoclientes3.Visible = false;
        }
        private string AsignarComa(double valor)
        {
            return String.Format("{0:#,##0.##}", valor);
        }

        private void rbCedula_CheckedChanged(object sender, EventArgs e)
        {
            ValidarPanelesChecked();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void ValidarPanelesChecked()
        {
            if (rbCedula.Checked == true)
            {
                panelCedula.Visible = true;
                panelrnc.Visible = false;
                TipoDeIdentificacion = "CEDULA";
            }
            else
            {
                panelrnc.Visible = true;
                panelCedula.Visible = false;
                TipoDeIdentificacion = "RNC";
            }
        }
        private void rbRnc_CheckedChanged(object sender, EventArgs e)
        {
            ValidarPanelesChecked();
        }

        private void rbticket_CheckedChanged(object sender, EventArgs e)
        {
            ValidateTipoComprobante();
        }

        private void rbFactura_CheckedChanged(object sender, EventArgs e)
        {
            ValidateTipoComprobante();
        }
        private void ValidateTipoComprobante()
        {
            if(rbticket.Checked == true)
            {
                lblComprobante.Text = "TICKET";
            }
            if (rbFactura.Checked == true)
            {
                lblComprobante.Text = "FACTURA";
            }
            Validar_tipos_de_comprobantes();
        }
    }
}
