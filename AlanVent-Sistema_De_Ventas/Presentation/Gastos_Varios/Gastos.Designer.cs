
namespace AlanVent_Sistema_De_Ventas.Presentation.Gastos_Varios
{
    partial class Gastos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Gastos));
            this.label1 = new System.Windows.Forms.Label();
            this.PanelDetalle = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rbBienes = new System.Windows.Forms.RadioButton();
            this.rbServicios = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            this.cbModoPago = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtfecha = new System.Windows.Forms.DateTimePicker();
            this.txtdetalle = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnComprobante = new System.Windows.Forms.CheckBox();
            this.panelcomprobante = new System.Windows.Forms.Panel();
            this.txtnrocomprobante = new System.Windows.Forms.TextBox();
            this.txttipocomprobante = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtimporte = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PanelbuscadorConceptos = new System.Windows.Forms.Panel();
            this.Panel6 = new System.Windows.Forms.Panel();
            this.CONTADO = new System.Windows.Forms.RadioButton();
            this.Label11 = new System.Windows.Forms.Label();
            this.TXTIDCONCEPTO = new System.Windows.Forms.TextBox();
            this.Id_usuario = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscarconcepto = new System.Windows.Forms.TextBox();
            this.datalistadoConceptos = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.PanelDetalle.SuspendLayout();
            this.panel4.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelcomprobante.SuspendLayout();
            this.PanelbuscadorConceptos.SuspendLayout();
            this.Panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datalistadoConceptos)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft PhagsPa", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(604, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "REGISTRO DE GASTOS: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PanelDetalle
            // 
            this.PanelDetalle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.PanelDetalle.Controls.Add(this.panel4);
            this.PanelDetalle.Controls.Add(this.label13);
            this.PanelDetalle.Controls.Add(this.cbModoPago);
            this.PanelDetalle.Controls.Add(this.label12);
            this.PanelDetalle.Controls.Add(this.txtfecha);
            this.PanelDetalle.Controls.Add(this.txtdetalle);
            this.PanelDetalle.Controls.Add(this.flowLayoutPanel1);
            this.PanelDetalle.Controls.Add(this.txtimporte);
            this.PanelDetalle.Controls.Add(this.label3);
            this.PanelDetalle.Controls.Add(this.label4);
            this.PanelDetalle.Controls.Add(this.label2);
            this.PanelDetalle.Location = new System.Drawing.Point(10, 9);
            this.PanelDetalle.Name = "PanelDetalle";
            this.PanelDetalle.Size = new System.Drawing.Size(560, 407);
            this.PanelDetalle.TabIndex = 1;
            this.PanelDetalle.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelDetalle_Paint);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.rbBienes);
            this.panel4.Controls.Add(this.rbServicios);
            this.panel4.Location = new System.Drawing.Point(133, 212);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 26);
            this.panel4.TabIndex = 15;
            // 
            // rbBienes
            // 
            this.rbBienes.AutoSize = true;
            this.rbBienes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbBienes.ForeColor = System.Drawing.Color.DarkGray;
            this.rbBienes.Location = new System.Drawing.Point(89, 2);
            this.rbBienes.Name = "rbBienes";
            this.rbBienes.Size = new System.Drawing.Size(75, 21);
            this.rbBienes.TabIndex = 13;
            this.rbBienes.Text = "Bienes";
            this.rbBienes.UseVisualStyleBackColor = true;
            this.rbBienes.CheckedChanged += new System.EventHandler(this.rbBienes_CheckedChanged);
            // 
            // rbServicios
            // 
            this.rbServicios.AutoSize = true;
            this.rbServicios.Checked = true;
            this.rbServicios.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbServicios.ForeColor = System.Drawing.Color.DarkGray;
            this.rbServicios.Location = new System.Drawing.Point(3, 1);
            this.rbServicios.Name = "rbServicios";
            this.rbServicios.Size = new System.Drawing.Size(84, 21);
            this.rbServicios.TabIndex = 14;
            this.rbServicios.TabStop = true;
            this.rbServicios.Text = "Servicio";
            this.rbServicios.UseVisualStyleBackColor = true;
            this.rbServicios.CheckedChanged += new System.EventHandler(this.rbServicios_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(40, 212);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(91, 20);
            this.label13.TabIndex = 12;
            this.label13.Text = "Tipo Gasto:";
            // 
            // cbModoPago
            // 
            this.cbModoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModoPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbModoPago.FormattingEnabled = true;
            this.cbModoPago.Items.AddRange(new object[] {
            "01 - EFECTIVO",
            "02 - CHEQUES/TRANSFERENCIAS/DEPÓSITO",
            "03 - TARJETA CRÉDITO/DÉBITO",
            "07 - MIXTO"});
            this.cbModoPago.Location = new System.Drawing.Point(133, 248);
            this.cbModoPago.Name = "cbModoPago";
            this.cbModoPago.Size = new System.Drawing.Size(202, 28);
            this.cbModoPago.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(14, 253);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(119, 20);
            this.label12.TabIndex = 10;
            this.label12.Text = "Modo De Pago:";
            // 
            // txtfecha
            // 
            this.txtfecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtfecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtfecha.Location = new System.Drawing.Point(137, 35);
            this.txtfecha.Name = "txtfecha";
            this.txtfecha.Size = new System.Drawing.Size(202, 26);
            this.txtfecha.TabIndex = 2;
            // 
            // txtdetalle
            // 
            this.txtdetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdetalle.Location = new System.Drawing.Point(137, 74);
            this.txtdetalle.Multiline = true;
            this.txtdetalle.Name = "txtdetalle";
            this.txtdetalle.Size = new System.Drawing.Size(409, 132);
            this.txtdetalle.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnGuardar);
            this.flowLayoutPanel1.Controls.Add(this.btnVolver);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(21, 324);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(410, 55);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Teal;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(3, 3);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(114, 45);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.Color.Silver;
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.ForeColor = System.Drawing.Color.Black;
            this.btnVolver.Location = new System.Drawing.Point(123, 3);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(119, 45);
            this.btnVolver.TabIndex = 3;
            this.btnVolver.Text = "Volver";
            this.btnVolver.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnComprobante);
            this.panel1.Controls.Add(this.panelcomprobante);
            this.panel1.Location = new System.Drawing.Point(248, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(10, 10);
            this.panel1.TabIndex = 537;
            // 
            // btnComprobante
            // 
            this.btnComprobante.AutoSize = true;
            this.btnComprobante.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComprobante.ForeColor = System.Drawing.Color.Maroon;
            this.btnComprobante.Location = new System.Drawing.Point(35, 12);
            this.btnComprobante.Name = "btnComprobante";
            this.btnComprobante.Size = new System.Drawing.Size(184, 29);
            this.btnComprobante.TabIndex = 2;
            this.btnComprobante.Text = "Sin Comprobante";
            this.btnComprobante.UseVisualStyleBackColor = true;
            this.btnComprobante.CheckedChanged += new System.EventHandler(this.btnComprobante_CheckedChanged);
            // 
            // panelcomprobante
            // 
            this.panelcomprobante.BackColor = System.Drawing.Color.White;
            this.panelcomprobante.Controls.Add(this.txtnrocomprobante);
            this.panelcomprobante.Controls.Add(this.txttipocomprobante);
            this.panelcomprobante.Controls.Add(this.panel3);
            this.panelcomprobante.Controls.Add(this.label5);
            this.panelcomprobante.Controls.Add(this.label7);
            this.panelcomprobante.Location = new System.Drawing.Point(29, 47);
            this.panelcomprobante.Name = "panelcomprobante";
            this.panelcomprobante.Size = new System.Drawing.Size(150, 10);
            this.panelcomprobante.TabIndex = 3;
            // 
            // txtnrocomprobante
            // 
            this.txtnrocomprobante.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnrocomprobante.Location = new System.Drawing.Point(190, 58);
            this.txtnrocomprobante.Name = "txtnrocomprobante";
            this.txtnrocomprobante.Size = new System.Drawing.Size(323, 26);
            this.txtnrocomprobante.TabIndex = 5;
            // 
            // txttipocomprobante
            // 
            this.txttipocomprobante.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttipocomprobante.FormattingEnabled = true;
            this.txttipocomprobante.Items.AddRange(new object[] {
            "SIN COMPROBANTE",
            "FACTURA",
            "BOLETA",
            "BOLETO DE VIAJE",
            "TICKET DE FACTURA",
            "Otro"});
            this.txttipocomprobante.Location = new System.Drawing.Point(190, 12);
            this.txttipocomprobante.Name = "txttipocomprobante";
            this.txttipocomprobante.Size = new System.Drawing.Size(323, 28);
            this.txttipocomprobante.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(150, 4);
            this.panel3.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(21, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(158, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Nro de comprobante:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(21, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(163, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "Tipo de comprobante:";
            // 
            // txtimporte
            // 
            this.txtimporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtimporte.Location = new System.Drawing.Point(137, 3);
            this.txtimporte.Name = "txtimporte";
            this.txtimporte.Size = new System.Drawing.Size(263, 26);
            this.txtimporte.TabIndex = 1;
            this.txtimporte.TextChanged += new System.EventHandler(this.txtimporte_TextChanged);
            this.txtimporte.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtimporte_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(73, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Fecha:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(73, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Detalle:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(73, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Monto:";
            // 
            // PanelbuscadorConceptos
            // 
            this.PanelbuscadorConceptos.BackColor = System.Drawing.Color.Transparent;
            this.PanelbuscadorConceptos.Controls.Add(this.Panel6);
            this.PanelbuscadorConceptos.Controls.Add(this.lblBuscar);
            this.PanelbuscadorConceptos.Controls.Add(this.txtBuscarconcepto);
            this.PanelbuscadorConceptos.Location = new System.Drawing.Point(12, 52);
            this.PanelbuscadorConceptos.Name = "PanelbuscadorConceptos";
            this.PanelbuscadorConceptos.Size = new System.Drawing.Size(545, 60);
            this.PanelbuscadorConceptos.TabIndex = 472;
            // 
            // Panel6
            // 
            this.Panel6.Controls.Add(this.CONTADO);
            this.Panel6.Controls.Add(this.Label11);
            this.Panel6.Controls.Add(this.TXTIDCONCEPTO);
            this.Panel6.Controls.Add(this.Id_usuario);
            this.Panel6.Controls.Add(this.Label9);
            this.Panel6.Location = new System.Drawing.Point(560, 141);
            this.Panel6.Name = "Panel6";
            this.Panel6.Size = new System.Drawing.Size(11, 26);
            this.Panel6.TabIndex = 465;
            // 
            // CONTADO
            // 
            this.CONTADO.AutoSize = true;
            this.CONTADO.Checked = true;
            this.CONTADO.Location = new System.Drawing.Point(103, 2);
            this.CONTADO.Margin = new System.Windows.Forms.Padding(4);
            this.CONTADO.Name = "CONTADO";
            this.CONTADO.Size = new System.Drawing.Size(70, 17);
            this.CONTADO.TabIndex = 231;
            this.CONTADO.TabStop = true;
            this.CONTADO.Text = "PAGADO";
            this.CONTADO.UseVisualStyleBackColor = true;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.BackColor = System.Drawing.Color.Transparent;
            this.Label11.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label11.ForeColor = System.Drawing.Color.Black;
            this.Label11.Location = new System.Drawing.Point(17, 33);
            this.Label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(43, 14);
            this.Label11.TabIndex = 215;
            this.Label11.Text = "Estado:";
            // 
            // TXTIDCONCEPTO
            // 
            this.TXTIDCONCEPTO.Location = new System.Drawing.Point(24, 0);
            this.TXTIDCONCEPTO.Margin = new System.Windows.Forms.Padding(4);
            this.TXTIDCONCEPTO.Name = "TXTIDCONCEPTO";
            this.TXTIDCONCEPTO.Size = new System.Drawing.Size(47, 20);
            this.TXTIDCONCEPTO.TabIndex = 459;
            // 
            // Id_usuario
            // 
            this.Id_usuario.AutoSize = true;
            this.Id_usuario.BackColor = System.Drawing.Color.Transparent;
            this.Id_usuario.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Id_usuario.ForeColor = System.Drawing.Color.Black;
            this.Id_usuario.Location = new System.Drawing.Point(10, 11);
            this.Id_usuario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Id_usuario.Name = "Id_usuario";
            this.Id_usuario.Size = new System.Drawing.Size(13, 14);
            this.Id_usuario.TabIndex = 216;
            this.Id_usuario.Text = "0";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(4, 46);
            this.Label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(111, 13);
            this.Label9.TabIndex = 222;
            this.Label9.Text = "Tipo de comprobante:";
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.lblBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscar.ForeColor = System.Drawing.Color.Silver;
            this.lblBuscar.Location = new System.Drawing.Point(10, 21);
            this.lblBuscar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(114, 17);
            this.lblBuscar.TabIndex = 457;
            this.lblBuscar.Text = "Buscar concepto";
            // 
            // txtBuscarconcepto
            // 
            this.txtBuscarconcepto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.txtBuscarconcepto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscarconcepto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscarconcepto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtBuscarconcepto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtBuscarconcepto.ForeColor = System.Drawing.Color.White;
            this.txtBuscarconcepto.Location = new System.Drawing.Point(4, 17);
            this.txtBuscarconcepto.Margin = new System.Windows.Forms.Padding(4);
            this.txtBuscarconcepto.Name = "txtBuscarconcepto";
            this.txtBuscarconcepto.Size = new System.Drawing.Size(344, 26);
            this.txtBuscarconcepto.TabIndex = 456;
            this.txtBuscarconcepto.Click += new System.EventHandler(this.txtBuscarconcepto_Click);
            this.txtBuscarconcepto.TextChanged += new System.EventHandler(this.txtBuscarconcepto_TextChanged);
            // 
            // datalistadoConceptos
            // 
            this.datalistadoConceptos.AllowUserToAddRows = false;
            this.datalistadoConceptos.AllowUserToDeleteRows = false;
            this.datalistadoConceptos.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PaleTurquoise;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.datalistadoConceptos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.datalistadoConceptos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.datalistadoConceptos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.datalistadoConceptos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.datalistadoConceptos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datalistadoConceptos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.datalistadoConceptos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.datalistadoConceptos.DefaultCellStyle = dataGridViewCellStyle3;
            this.datalistadoConceptos.EnableHeadersVisualStyles = false;
            this.datalistadoConceptos.Location = new System.Drawing.Point(13, 9);
            this.datalistadoConceptos.Name = "datalistadoConceptos";
            this.datalistadoConceptos.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datalistadoConceptos.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.datalistadoConceptos.RowHeadersVisible = false;
            this.datalistadoConceptos.RowHeadersWidth = 9;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Gainsboro;
            this.datalistadoConceptos.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.datalistadoConceptos.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.datalistadoConceptos.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.datalistadoConceptos.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            this.datalistadoConceptos.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.datalistadoConceptos.RowTemplate.Height = 40;
            this.datalistadoConceptos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datalistadoConceptos.Size = new System.Drawing.Size(197, 86);
            this.datalistadoConceptos.TabIndex = 536;
            this.datalistadoConceptos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datalistadoConceptos_CellClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.PanelDetalle);
            this.panel2.Controls.Add(this.datalistadoConceptos);
            this.panel2.Location = new System.Drawing.Point(12, 124);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(577, 426);
            this.panel2.TabIndex = 537;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // Gastos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.ClientSize = new System.Drawing.Size(604, 530);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.PanelbuscadorConceptos);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Gastos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AlanVent";
            this.Load += new System.EventHandler(this.Gastos_Load);
            this.PanelDetalle.ResumeLayout(false);
            this.PanelDetalle.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelcomprobante.ResumeLayout(false);
            this.panelcomprobante.PerformLayout();
            this.PanelbuscadorConceptos.ResumeLayout(false);
            this.PanelbuscadorConceptos.PerformLayout();
            this.Panel6.ResumeLayout(false);
            this.Panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datalistadoConceptos)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel PanelDetalle;
        private System.Windows.Forms.DateTimePicker txtfecha;
        private System.Windows.Forms.TextBox txtimporte;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtdetalle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox btnComprobante;
        private System.Windows.Forms.Panel panelcomprobante;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtnrocomprobante;
        private System.Windows.Forms.ComboBox txttipocomprobante;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnVolver;
        internal System.Windows.Forms.Panel PanelbuscadorConceptos;
        internal System.Windows.Forms.Panel Panel6;
        internal System.Windows.Forms.RadioButton CONTADO;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.TextBox TXTIDCONCEPTO;
        internal System.Windows.Forms.Label Id_usuario;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label lblBuscar;
        internal System.Windows.Forms.TextBox txtBuscarconcepto;
        internal System.Windows.Forms.DataGridView datalistadoConceptos;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbModoPago;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RadioButton rbBienes;
        private System.Windows.Forms.RadioButton rbServicios;
        private System.Windows.Forms.Panel panel4;
    }
}