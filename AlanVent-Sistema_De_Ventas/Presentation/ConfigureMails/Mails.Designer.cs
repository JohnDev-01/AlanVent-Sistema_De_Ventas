namespace AlanVent_Sistema_De_Ventas.Presentation.ConfigureMails
{
    partial class Mails
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
            this.btnSends = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.txtmonto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSends
            // 
            this.btnSends.Location = new System.Drawing.Point(244, 42);
            this.btnSends.Name = "btnSends";
            this.btnSends.Size = new System.Drawing.Size(197, 64);
            this.btnSends.TabIndex = 0;
            this.btnSends.Text = "Send";
            this.btnSends.UseVisualStyleBackColor = true;
            this.btnSends.Click += new System.EventHandler(this.btnSends_Click);
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(244, 155);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(197, 64);
            this.btnConvert.TabIndex = 0;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // txtmonto
            // 
            this.txtmonto.Location = new System.Drawing.Point(241, 277);
            this.txtmonto.Name = "txtmonto";
            this.txtmonto.Size = new System.Drawing.Size(381, 20);
            this.txtmonto.TabIndex = 1;
            this.txtmonto.TextChanged += new System.EventHandler(this.txtmonto_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(285, 351);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // Mails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtmonto);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.btnSends);
            this.Name = "Mails";
            this.Text = "Mails";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSends;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.TextBox txtmonto;
        private System.Windows.Forms.Label label1;
    }
}