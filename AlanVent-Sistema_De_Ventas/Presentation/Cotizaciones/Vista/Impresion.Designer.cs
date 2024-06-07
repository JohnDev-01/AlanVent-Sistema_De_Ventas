namespace AlanVent_Sistema_De_Ventas.Presentation.Cotizaciones.Vista
{
    partial class Impresion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Impresion));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTicket = new System.Windows.Forms.Button();
            this.btnFactura = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.VistaReporte = new Telerik.ReportViewer.WinForms.ReportViewer();
            this.panelDirecto = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnImprimirDirecto = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtImpresora = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbVistaPrevia = new System.Windows.Forms.RadioButton();
            this.rbDirecta = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelDirecto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnTicket);
            this.panel1.Controls.Add(this.btnFactura);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(150, 490);
            this.panel1.TabIndex = 0;
            // 
            // btnTicket
            // 
            this.btnTicket.BackgroundImage = global::AlanVent_Sistema_De_Ventas.Properties.Resources.Rojo1;
            this.btnTicket.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTicket.FlatAppearance.BorderSize = 0;
            this.btnTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTicket.ForeColor = System.Drawing.Color.White;
            this.btnTicket.Location = new System.Drawing.Point(3, 93);
            this.btnTicket.Name = "btnTicket";
            this.btnTicket.Size = new System.Drawing.Size(141, 55);
            this.btnTicket.TabIndex = 0;
            this.btnTicket.Text = "Ticket";
            this.btnTicket.UseVisualStyleBackColor = true;
            this.btnTicket.Click += new System.EventHandler(this.btnTicket_Click);
            // 
            // btnFactura
            // 
            this.btnFactura.BackgroundImage = global::AlanVent_Sistema_De_Ventas.Properties.Resources.azul;
            this.btnFactura.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFactura.FlatAppearance.BorderSize = 0;
            this.btnFactura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFactura.ForeColor = System.Drawing.Color.White;
            this.btnFactura.Location = new System.Drawing.Point(3, 32);
            this.btnFactura.Name = "btnFactura";
            this.btnFactura.Size = new System.Drawing.Size(141, 55);
            this.btnFactura.TabIndex = 0;
            this.btnFactura.Text = "Factura";
            this.btnFactura.UseVisualStyleBackColor = true;
            this.btnFactura.Click += new System.EventHandler(this.btnFactura_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.VistaReporte);
            this.panel2.Controls.Add(this.panelDirecto);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(150, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(827, 490);
            this.panel2.TabIndex = 1;
            // 
            // VistaReporte
            // 
            this.VistaReporte.AccessibilityKeyMap = null;
            this.VistaReporte.Location = new System.Drawing.Point(183, 160);
            this.VistaReporte.Name = "VistaReporte";
            this.VistaReporte.Size = new System.Drawing.Size(516, 259);
            this.VistaReporte.TabIndex = 0;
            this.VistaReporte.ViewMode = Telerik.ReportViewer.WinForms.ViewMode.PrintPreview;
            this.VistaReporte.Load += new System.EventHandler(this.VistaReporte_Load);
            // 
            // panelDirecto
            // 
            this.panelDirecto.Controls.Add(this.pictureBox1);
            this.panelDirecto.Controls.Add(this.btnImprimirDirecto);
            this.panelDirecto.Controls.Add(this.label2);
            this.panelDirecto.Controls.Add(this.txtImpresora);
            this.panelDirecto.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDirecto.Location = new System.Drawing.Point(0, 47);
            this.panelDirecto.Name = "panelDirecto";
            this.panelDirecto.Size = new System.Drawing.Size(827, 55);
            this.panelDirecto.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(330, 95);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(223, 174);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 583;
            this.pictureBox1.TabStop = false;
            // 
            // btnImprimirDirecto
            // 
            this.btnImprimirDirecto.BackgroundImage = global::AlanVent_Sistema_De_Ventas.Properties.Resources.amarillo;
            this.btnImprimirDirecto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImprimirDirecto.FlatAppearance.BorderSize = 0;
            this.btnImprimirDirecto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimirDirecto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimirDirecto.ForeColor = System.Drawing.Color.Black;
            this.btnImprimirDirecto.Location = new System.Drawing.Point(449, 5);
            this.btnImprimirDirecto.Name = "btnImprimirDirecto";
            this.btnImprimirDirecto.Size = new System.Drawing.Size(92, 35);
            this.btnImprimirDirecto.TabIndex = 2;
            this.btnImprimirDirecto.Text = "Imprimir";
            this.btnImprimirDirecto.UseVisualStyleBackColor = true;
            this.btnImprimirDirecto.Click += new System.EventHandler(this.btnImprimirDirecto_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 20);
            this.label2.TabIndex = 582;
            this.label2.Text = "Impresora:";
            // 
            // txtImpresora
            // 
            this.txtImpresora.BackColor = System.Drawing.Color.White;
            this.txtImpresora.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtImpresora.FormattingEnabled = true;
            this.txtImpresora.Location = new System.Drawing.Point(101, 10);
            this.txtImpresora.Name = "txtImpresora";
            this.txtImpresora.Size = new System.Drawing.Size(342, 28);
            this.txtImpresora.TabIndex = 581;
            this.txtImpresora.SelectedValueChanged += new System.EventHandler(this.txtImpresora_SelectedValueChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel3.Controls.Add(this.rbVistaPrevia);
            this.panel3.Controls.Add(this.rbDirecta);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(827, 47);
            this.panel3.TabIndex = 0;
            // 
            // rbVistaPrevia
            // 
            this.rbVistaPrevia.AutoSize = true;
            this.rbVistaPrevia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbVistaPrevia.Location = new System.Drawing.Point(350, 11);
            this.rbVistaPrevia.Name = "rbVistaPrevia";
            this.rbVistaPrevia.Size = new System.Drawing.Size(110, 24);
            this.rbVistaPrevia.TabIndex = 1;
            this.rbVistaPrevia.TabStop = true;
            this.rbVistaPrevia.Text = "Vista Previa";
            this.rbVistaPrevia.UseVisualStyleBackColor = true;
            this.rbVistaPrevia.CheckedChanged += new System.EventHandler(this.rbVistaPrevia_CheckedChanged);
            // 
            // rbDirecta
            // 
            this.rbDirecta.AutoSize = true;
            this.rbDirecta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDirecta.Location = new System.Drawing.Point(272, 11);
            this.rbDirecta.Name = "rbDirecta";
            this.rbDirecta.Size = new System.Drawing.Size(78, 24);
            this.rbDirecta.TabIndex = 1;
            this.rbDirecta.TabStop = true;
            this.rbDirecta.Text = "Directa";
            this.rbDirecta.UseVisualStyleBackColor = true;
            this.rbDirecta.CheckedChanged += new System.EventHandler(this.rbDirecta_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Selecciona el tipo de impresion:";
            // 
            // Impresion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(977, 490);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Impresion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AlanVent";
            this.Load += new System.EventHandler(this.Impresion_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panelDirecto.ResumeLayout(false);
            this.panelDirecto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFactura;
        private System.Windows.Forms.Button btnTicket;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbVistaPrevia;
        private System.Windows.Forms.RadioButton rbDirecta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelDirecto;
        internal System.Windows.Forms.ComboBox txtImpresora;
        private System.Windows.Forms.Button btnImprimirDirecto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Telerik.ReportViewer.WinForms.ReportViewer VistaReporte;
    }
}