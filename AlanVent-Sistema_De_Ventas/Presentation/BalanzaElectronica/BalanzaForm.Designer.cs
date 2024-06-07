
namespace AlanVent_Sistema_De_Ventas.Presentation.BalanzaElectronica
{
    partial class BalanzaForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BalanzaForm));
            this.puertos = new System.IO.Ports.SerialPort(this.components);
            this.uI_ShadowPanel1 = new UIDC.UI_ShadowPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnenviar = new System.Windows.Forms.Button();
            this.lblestado = new System.Windows.Forms.Label();
            this.btnguardar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtresultado = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnProbar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ListaPuertos = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.uI_ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // puertos
            // 
            this.puertos.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.puertos_DataReceived);
            // 
            // uI_ShadowPanel1
            // 
            this.uI_ShadowPanel1.AddControl = null;
            this.uI_ShadowPanel1.BackColor = System.Drawing.Color.White;
            this.uI_ShadowPanel1.Controls.Add(this.pictureBox1);
            this.uI_ShadowPanel1.Controls.Add(this.btnenviar);
            this.uI_ShadowPanel1.Controls.Add(this.lblestado);
            this.uI_ShadowPanel1.Controls.Add(this.btnguardar);
            this.uI_ShadowPanel1.Controls.Add(this.label5);
            this.uI_ShadowPanel1.Controls.Add(this.txtresultado);
            this.uI_ShadowPanel1.Controls.Add(this.label1);
            this.uI_ShadowPanel1.Controls.Add(this.btnProbar);
            this.uI_ShadowPanel1.Controls.Add(this.label4);
            this.uI_ShadowPanel1.Controls.Add(this.panel2);
            this.uI_ShadowPanel1.Controls.Add(this.panel1);
            this.uI_ShadowPanel1.Controls.Add(this.ListaPuertos);
            this.uI_ShadowPanel1.Controls.Add(this.label2);
            this.uI_ShadowPanel1.Controls.Add(this.label3);
            this.uI_ShadowPanel1.Controls.Add(this.Label14);
            this.uI_ShadowPanel1.Location = new System.Drawing.Point(12, 29);
            this.uI_ShadowPanel1.Name = "uI_ShadowPanel1";
            this.uI_ShadowPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.uI_ShadowPanel1.Size = new System.Drawing.Size(906, 486);
            this.uI_ShadowPanel1.SizeShadow = 10;
            this.uI_ShadowPanel1.TabIndex = 0;
            this.uI_ShadowPanel1.TypeShadowPanel = UIDC.UI_ShadowPanel.ShadowsPanel.Center;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AlanVent_Sistema_De_Ventas.Properties.Resources.IMG_46311;
            this.pictureBox1.Location = new System.Drawing.Point(714, 153);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(131, 74);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 666;
            this.pictureBox1.TabStop = false;
            // 
            // btnenviar
            // 
            this.btnenviar.Location = new System.Drawing.Point(714, 396);
            this.btnenviar.Name = "btnenviar";
            this.btnenviar.Size = new System.Drawing.Size(149, 46);
            this.btnenviar.TabIndex = 665;
            this.btnenviar.Text = "Enviar";
            this.btnenviar.UseVisualStyleBackColor = true;
            this.btnenviar.Visible = false;
            this.btnenviar.Click += new System.EventHandler(this.btnenviar_Click);
            // 
            // lblestado
            // 
            this.lblestado.AutoSize = true;
            this.lblestado.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblestado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.lblestado.ForeColor = System.Drawing.Color.DimGray;
            this.lblestado.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblestado.Location = new System.Drawing.Point(527, 27);
            this.lblestado.Name = "lblestado";
            this.lblestado.Size = new System.Drawing.Size(28, 25);
            this.lblestado.TabIndex = 654;
            this.lblestado.Text = "--";
            // 
            // btnguardar
            // 
            this.btnguardar.BackColor = System.Drawing.Color.Transparent;
            this.btnguardar.BackgroundImage = global::AlanVent_Sistema_De_Ventas.Properties.Resources.verde;
            this.btnguardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnguardar.FlatAppearance.BorderSize = 0;
            this.btnguardar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnguardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnguardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnguardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnguardar.ForeColor = System.Drawing.Color.White;
            this.btnguardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnguardar.Location = new System.Drawing.Point(349, 377);
            this.btnguardar.Name = "btnguardar";
            this.btnguardar.Size = new System.Drawing.Size(244, 78);
            this.btnguardar.TabIndex = 664;
            this.btnguardar.Text = "Guardar";
            this.btnguardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnguardar.UseVisualStyleBackColor = false;
            this.btnguardar.Click += new System.EventHandler(this.btnguardar_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Olive;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(12, 317);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(884, 40);
            this.label5.TabIndex = 663;
            this.label5.Text = "Paso 3: Si ya reconoce el peso, entonces de click a *Guardar* y Efectuaran los ca" +
    "mbios";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtresultado
            // 
            this.txtresultado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtresultado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtresultado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtresultado.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.txtresultado.ForeColor = System.Drawing.Color.Red;
            this.txtresultado.Location = new System.Drawing.Point(92, 242);
            this.txtresultado.Name = "txtresultado";
            this.txtresultado.Size = new System.Drawing.Size(323, 38);
            this.txtresultado.TabIndex = 662;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(18, 250);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 25);
            this.label1.TabIndex = 661;
            this.label1.Text = "Peso:";
            // 
            // btnProbar
            // 
            this.btnProbar.BackColor = System.Drawing.Color.Transparent;
            this.btnProbar.BackgroundImage = global::AlanVent_Sistema_De_Ventas.Properties.Resources.azul;
            this.btnProbar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnProbar.FlatAppearance.BorderSize = 0;
            this.btnProbar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnProbar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnProbar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProbar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProbar.ForeColor = System.Drawing.Color.White;
            this.btnProbar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProbar.Location = new System.Drawing.Point(421, 247);
            this.btnProbar.Name = "btnProbar";
            this.btnProbar.Size = new System.Drawing.Size(95, 33);
            this.btnProbar.TabIndex = 660;
            this.btnProbar.Text = "Probar";
            this.btnProbar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnProbar.UseVisualStyleBackColor = false;
            this.btnProbar.Click += new System.EventHandler(this.btnProbar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Olive;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(13, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(419, 20);
            this.label4.TabIndex = 659;
            this.label4.Text = "Paso 1: Conecte su balanza y seleccione un Puerto";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.Location = new System.Drawing.Point(17, 296);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(600, 1);
            this.panel2.TabIndex = 658;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Location = new System.Drawing.Point(18, 186);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 1);
            this.panel1.TabIndex = 657;
            // 
            // ListaPuertos
            // 
            this.ListaPuertos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListaPuertos.FormattingEnabled = true;
            this.ListaPuertos.Location = new System.Drawing.Point(92, 140);
            this.ListaPuertos.Name = "ListaPuertos";
            this.ListaPuertos.Size = new System.Drawing.Size(526, 28);
            this.ListaPuertos.TabIndex = 656;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(13, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 25);
            this.label2.TabIndex = 655;
            this.label2.Text = "Puerto:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Olive;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(14, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(354, 20);
            this.label3.TabIndex = 653;
            this.label3.Text = "Paso 2: Pruebe el correcto funcionamiento.";
            // 
            // Label14
            // 
            this.Label14.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.Label14.ForeColor = System.Drawing.Color.Black;
            this.Label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label14.Location = new System.Drawing.Point(10, 10);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(886, 53);
            this.Label14.TabIndex = 651;
            this.Label14.Text = "Configuracion de balanza electronica";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BalanzaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(930, 527);
            this.Controls.Add(this.uI_ShadowPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BalanzaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AlanVent";
            this.Load += new System.EventHandler(this.BalanzaForm_Load);
            this.uI_ShadowPanel1.ResumeLayout(false);
            this.uI_ShadowPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UIDC.UI_ShadowPanel uI_ShadowPanel1;
        private System.Windows.Forms.Button btnenviar;
        internal System.Windows.Forms.Label lblestado;
        private System.Windows.Forms.Button btnguardar;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox txtresultado;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnProbar;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox ListaPuertos;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label Label14;
        private System.IO.Ports.SerialPort puertos;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}