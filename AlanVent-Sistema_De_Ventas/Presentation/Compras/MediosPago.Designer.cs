
namespace AlanVent_Sistema_De_Ventas.Presentation.Compras
{
    partial class MediosPago
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MediosPago));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEfectivo = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelFechaCredito = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.dtFechaVenc = new System.Windows.Forms.DateTimePicker();
            this.lblAdvertecia = new System.Windows.Forms.Label();
            this.lblResta = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCredito = new System.Windows.Forms.TextBox();
            this.btnPagar = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgDetallecompra = new System.Windows.Forms.DataGridView();
            this.EL = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.FlowpanelProveedor = new System.Windows.Forms.FlowLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblProvedor = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panelFechaCredito.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetallecompra)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(565, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Medios de pago de compras*";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gainsboro;
            this.label2.Location = new System.Drawing.Point(14, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Efectivo.....:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gainsboro;
            this.label3.Location = new System.Drawing.Point(16, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 24);
            this.label3.TabIndex = 1;
            this.label3.Text = "Credito.....:";
            // 
            // txtEfectivo
            // 
            this.txtEfectivo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.txtEfectivo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEfectivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEfectivo.ForeColor = System.Drawing.Color.White;
            this.txtEfectivo.Location = new System.Drawing.Point(118, 59);
            this.txtEfectivo.Name = "txtEfectivo";
            this.txtEfectivo.Size = new System.Drawing.Size(179, 19);
            this.txtEfectivo.TabIndex = 2;
            this.txtEfectivo.TextChanged += new System.EventHandler(this.txtEfectivo_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panel1.Controls.Add(this.panelFechaCredito);
            this.panel1.Controls.Add(this.lblAdvertecia);
            this.panel1.Controls.Add(this.lblResta);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lblTotal);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.txtEfectivo);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtCredito);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(313, 414);
            this.panel1.TabIndex = 3;
            // 
            // panelFechaCredito
            // 
            this.panelFechaCredito.Controls.Add(this.label6);
            this.panelFechaCredito.Controls.Add(this.dtFechaVenc);
            this.panelFechaCredito.Location = new System.Drawing.Point(5, 142);
            this.panelFechaCredito.Name = "panelFechaCredito";
            this.panelFechaCredito.Size = new System.Drawing.Size(299, 32);
            this.panelFechaCredito.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Yellow;
            this.label6.Location = new System.Drawing.Point(6, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(158, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "Fecha Venc. Credito:";
            // 
            // dtFechaVenc
            // 
            this.dtFechaVenc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFechaVenc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaVenc.Location = new System.Drawing.Point(163, 5);
            this.dtFechaVenc.Name = "dtFechaVenc";
            this.dtFechaVenc.Size = new System.Drawing.Size(127, 26);
            this.dtFechaVenc.TabIndex = 13;
            // 
            // lblAdvertecia
            // 
            this.lblAdvertecia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdvertecia.ForeColor = System.Drawing.Color.Red;
            this.lblAdvertecia.Location = new System.Drawing.Point(3, 181);
            this.lblAdvertecia.Name = "lblAdvertecia";
            this.lblAdvertecia.Size = new System.Drawing.Size(301, 118);
            this.lblAdvertecia.TabIndex = 12;
            this.lblAdvertecia.Text = "-";
            // 
            // lblResta
            // 
            this.lblResta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblResta.Location = new System.Drawing.Point(55, 328);
            this.lblResta.Name = "lblResta";
            this.lblResta.Size = new System.Drawing.Size(219, 20);
            this.lblResta.TabIndex = 10;
            this.lblResta.Text = "0.00";
            this.lblResta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Silver;
            this.label9.Location = new System.Drawing.Point(3, 328);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 20);
            this.label9.TabIndex = 9;
            this.label9.Text = "Resta:";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Silver;
            this.label7.Location = new System.Drawing.Point(3, 348);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(273, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "------------------------------------------------";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.Silver;
            this.lblTotal.Location = new System.Drawing.Point(55, 369);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(251, 20);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Silver;
            this.label5.Location = new System.Drawing.Point(5, 369);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Total:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Navy;
            this.panel3.Location = new System.Drawing.Point(118, 79);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(180, 2);
            this.panel3.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Navy;
            this.panel2.Location = new System.Drawing.Point(119, 125);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(180, 2);
            this.panel2.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 3;
            // 
            // txtCredito
            // 
            this.txtCredito.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.txtCredito.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCredito.ForeColor = System.Drawing.Color.White;
            this.txtCredito.Location = new System.Drawing.Point(118, 102);
            this.txtCredito.Name = "txtCredito";
            this.txtCredito.Size = new System.Drawing.Size(179, 19);
            this.txtCredito.TabIndex = 2;
            this.txtCredito.TextChanged += new System.EventHandler(this.txtCredito_TextChanged);
            // 
            // btnPagar
            // 
            this.btnPagar.BackgroundImage = global::AlanVent_Sistema_De_Ventas.Properties.Resources.azul;
            this.btnPagar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPagar.FlatAppearance.BorderSize = 0;
            this.btnPagar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPagar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPagar.ForeColor = System.Drawing.Color.White;
            this.btnPagar.Location = new System.Drawing.Point(344, 360);
            this.btnPagar.Name = "btnPagar";
            this.btnPagar.Size = new System.Drawing.Size(200, 65);
            this.btnPagar.TabIndex = 4;
            this.btnPagar.Text = "Pagar (ENTER)";
            this.btnPagar.UseVisualStyleBackColor = true;
            this.btnPagar.Click += new System.EventHandler(this.btnPagar_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgDetallecompra);
            this.panel4.Location = new System.Drawing.Point(57, 12);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(22, 23);
            this.panel4.TabIndex = 5;
            // 
            // dgDetallecompra
            // 
            this.dgDetallecompra.AllowUserToAddRows = false;
            this.dgDetallecompra.AllowUserToDeleteRows = false;
            this.dgDetallecompra.AllowUserToResizeRows = false;
            this.dgDetallecompra.BackgroundColor = System.Drawing.Color.White;
            this.dgDetallecompra.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgDetallecompra.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgDetallecompra.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDetallecompra.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDetallecompra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetallecompra.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EL});
            this.dgDetallecompra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDetallecompra.EnableHeadersVisualStyles = false;
            this.dgDetallecompra.GridColor = System.Drawing.Color.Gainsboro;
            this.dgDetallecompra.Location = new System.Drawing.Point(0, 0);
            this.dgDetallecompra.Name = "dgDetallecompra";
            this.dgDetallecompra.ReadOnly = true;
            this.dgDetallecompra.RowHeadersVisible = false;
            this.dgDetallecompra.RowHeadersWidth = 9;
            this.dgDetallecompra.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgDetallecompra.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgDetallecompra.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgDetallecompra.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Gainsboro;
            this.dgDetallecompra.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgDetallecompra.RowTemplate.Height = 36;
            this.dgDetallecompra.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDetallecompra.Size = new System.Drawing.Size(22, 23);
            this.dgDetallecompra.TabIndex = 629;
            // 
            // EL
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.EL.DefaultCellStyle = dataGridViewCellStyle2;
            this.EL.HeaderText = "";
            this.EL.Image = ((System.Drawing.Image)(resources.GetObject("EL.Image")));
            this.EL.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.EL.Name = "EL";
            this.EL.ReadOnly = true;
            this.EL.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.EL.ToolTipText = "Opcional \"Supr\" para Eliminar";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel4);
            this.panel5.Location = new System.Drawing.Point(543, 53);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(10, 10);
            this.panel5.TabIndex = 6;
            // 
            // FlowpanelProveedor
            // 
            this.FlowpanelProveedor.AutoScroll = true;
            this.FlowpanelProveedor.Dock = System.Windows.Forms.DockStyle.Right;
            this.FlowpanelProveedor.Location = new System.Drawing.Point(546, 36);
            this.FlowpanelProveedor.Name = "FlowpanelProveedor";
            this.FlowpanelProveedor.Size = new System.Drawing.Size(19, 414);
            this.FlowpanelProveedor.TabIndex = 635;
            this.FlowpanelProveedor.Visible = false;
            this.FlowpanelProveedor.Paint += new System.Windows.Forms.PaintEventHandler(this.FlowpanelProveedor_Paint);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.panel6.Controls.Add(this.lblProvedor);
            this.panel6.Controls.Add(this.label11);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 450);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(565, 36);
            this.panel6.TabIndex = 636;
            // 
            // lblProvedor
            // 
            this.lblProvedor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProvedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProvedor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblProvedor.Location = new System.Drawing.Point(87, 9);
            this.lblProvedor.Name = "lblProvedor";
            this.lblProvedor.Size = new System.Drawing.Size(457, 20);
            this.lblProvedor.TabIndex = 12;
            this.lblProvedor.Text = "--";
            this.lblProvedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProvedor.Click += new System.EventHandler(this.label10_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(3, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 20);
            this.label11.TabIndex = 11;
            this.label11.Text = "Proveedor:";
            // 
            // MediosPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(565, 486);
            this.Controls.Add(this.FlowpanelProveedor);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.btnPagar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel6);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MediosPago";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AlanVent";
            this.Load += new System.EventHandler(this.MediosPago_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelFechaCredito.ResumeLayout(false);
            this.panelFechaCredito.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetallecompra)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEfectivo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCredito;
        private System.Windows.Forms.Button btnPagar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblResta;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.DataGridView dgDetallecompra;
        private System.Windows.Forms.DataGridViewImageColumn EL;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.FlowLayoutPanel FlowpanelProveedor;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblProvedor;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblAdvertecia;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtFechaVenc;
        private System.Windows.Forms.Panel panelFechaCredito;
    }
}