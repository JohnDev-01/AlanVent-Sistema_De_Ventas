
namespace AlanVent_Sistema_De_Ventas.Presentation.Caja
{
    partial class Listado_Gastos_ingresos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Listado_Gastos_ingresos));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.datalistado_Ingresos = new System.Windows.Forms.DataGridView();
            this.EliminarI = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblTotal_ingresos = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.uI_GradientPanel1 = new UIDC.UI_GradientPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.datalistado_Gastos = new System.Windows.Forms.DataGridView();
            this.Eliminar = new System.Windows.Forms.DataGridViewImageColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblTotalGastos = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datalistado_Ingresos)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datalistado_Gastos)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.uI_GradientPanel1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(939, 507);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.datalistado_Ingresos);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(488, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(451, 507);
            this.panel3.TabIndex = 3;
            // 
            // datalistado_Ingresos
            // 
            this.datalistado_Ingresos.AllowUserToAddRows = false;
            this.datalistado_Ingresos.AllowUserToDeleteRows = false;
            this.datalistado_Ingresos.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PaleTurquoise;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.datalistado_Ingresos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.datalistado_Ingresos.BackgroundColor = System.Drawing.Color.White;
            this.datalistado_Ingresos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.datalistado_Ingresos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.datalistado_Ingresos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datalistado_Ingresos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.datalistado_Ingresos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datalistado_Ingresos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EliminarI});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.datalistado_Ingresos.DefaultCellStyle = dataGridViewCellStyle3;
            this.datalistado_Ingresos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datalistado_Ingresos.EnableHeadersVisualStyles = false;
            this.datalistado_Ingresos.Location = new System.Drawing.Point(0, 43);
            this.datalistado_Ingresos.Name = "datalistado_Ingresos";
            this.datalistado_Ingresos.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datalistado_Ingresos.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.datalistado_Ingresos.RowHeadersVisible = false;
            this.datalistado_Ingresos.RowHeadersWidth = 9;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Gainsboro;
            this.datalistado_Ingresos.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.datalistado_Ingresos.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.datalistado_Ingresos.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.datalistado_Ingresos.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            this.datalistado_Ingresos.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.datalistado_Ingresos.RowTemplate.Height = 40;
            this.datalistado_Ingresos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datalistado_Ingresos.Size = new System.Drawing.Size(451, 402);
            this.datalistado_Ingresos.TabIndex = 620;
            this.datalistado_Ingresos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datalistado_Ingresos_CellClick);
            // 
            // EliminarI
            // 
            this.EliminarI.HeaderText = "";
            this.EliminarI.Image = ((System.Drawing.Image)(resources.GetObject("EliminarI.Image")));
            this.EliminarI.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.EliminarI.Name = "EliminarI";
            this.EliminarI.ReadOnly = true;
            this.EliminarI.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lblTotal_ingresos);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 445);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(451, 62);
            this.panel5.TabIndex = 3;
            // 
            // lblTotal_ingresos
            // 
            this.lblTotal_ingresos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotal_ingresos.Font = new System.Drawing.Font("Microsoft New Tai Lue", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal_ingresos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblTotal_ingresos.Location = new System.Drawing.Point(67, 0);
            this.lblTotal_ingresos.Name = "lblTotal_ingresos";
            this.lblTotal_ingresos.Size = new System.Drawing.Size(384, 62);
            this.lblTotal_ingresos.TabIndex = 2;
            this.lblTotal_ingresos.Text = "0.00";
            this.lblTotal_ingresos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Microsoft New Tai Lue", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 62);
            this.label5.TabIndex = 1;
            this.label5.Text = "Total:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(451, 43);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ingresos de caja";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uI_GradientPanel1
            // 
            this.uI_GradientPanel1.BackColor = System.Drawing.Color.White;
            this.uI_GradientPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.uI_GradientPanel1.Location = new System.Drawing.Point(483, 0);
            this.uI_GradientPanel1.Name = "uI_GradientPanel1";
            this.uI_GradientPanel1.Size = new System.Drawing.Size(5, 507);
            this.uI_GradientPanel1.TabIndex = 2;
            this.uI_GradientPanel1.UIBackColor = System.Drawing.Color.Empty;
            this.uI_GradientPanel1.UIBottomLeft = System.Drawing.Color.Black;
            this.uI_GradientPanel1.UIBottomRight = System.Drawing.Color.Fuchsia;
            this.uI_GradientPanel1.UIForeColor = System.Drawing.Color.Empty;
            this.uI_GradientPanel1.UIPrimerColor = System.Drawing.Color.White;
            this.uI_GradientPanel1.UIStyle = UIDC.UI_GradientPanel.GradientStyle.Corners;
            this.uI_GradientPanel1.UITopLeft = System.Drawing.Color.DeepSkyBlue;
            this.uI_GradientPanel1.UITopRight = System.Drawing.Color.Fuchsia;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.datalistado_Gastos);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(483, 507);
            this.panel2.TabIndex = 3;
            // 
            // datalistado_Gastos
            // 
            this.datalistado_Gastos.AllowUserToAddRows = false;
            this.datalistado_Gastos.AllowUserToDeleteRows = false;
            this.datalistado_Gastos.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.PaleTurquoise;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            this.datalistado_Gastos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.datalistado_Gastos.BackgroundColor = System.Drawing.Color.White;
            this.datalistado_Gastos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.datalistado_Gastos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.datalistado_Gastos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datalistado_Gastos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.datalistado_Gastos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datalistado_Gastos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Eliminar});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.datalistado_Gastos.DefaultCellStyle = dataGridViewCellStyle8;
            this.datalistado_Gastos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datalistado_Gastos.EnableHeadersVisualStyles = false;
            this.datalistado_Gastos.Location = new System.Drawing.Point(0, 43);
            this.datalistado_Gastos.Name = "datalistado_Gastos";
            this.datalistado_Gastos.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datalistado_Gastos.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.datalistado_Gastos.RowHeadersVisible = false;
            this.datalistado_Gastos.RowHeadersWidth = 9;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Gainsboro;
            this.datalistado_Gastos.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.datalistado_Gastos.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.datalistado_Gastos.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.datalistado_Gastos.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            this.datalistado_Gastos.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.datalistado_Gastos.RowTemplate.Height = 40;
            this.datalistado_Gastos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datalistado_Gastos.Size = new System.Drawing.Size(483, 402);
            this.datalistado_Gastos.TabIndex = 619;
            this.datalistado_Gastos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datalistado_Gastos_CellClick);
            // 
            // Eliminar
            // 
            this.Eliminar.HeaderText = "";
            this.Eliminar.Image = ((System.Drawing.Image)(resources.GetObject("Eliminar.Image")));
            this.Eliminar.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.ReadOnly = true;
            this.Eliminar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(483, 43);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gastos de caja";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblTotalGastos);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 445);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(483, 62);
            this.panel4.TabIndex = 2;
            // 
            // lblTotalGastos
            // 
            this.lblTotalGastos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalGastos.Font = new System.Drawing.Font("Microsoft New Tai Lue", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalGastos.ForeColor = System.Drawing.Color.Red;
            this.lblTotalGastos.Location = new System.Drawing.Point(67, 0);
            this.lblTotalGastos.Name = "lblTotalGastos";
            this.lblTotalGastos.Size = new System.Drawing.Size(416, 62);
            this.lblTotalGastos.TabIndex = 2;
            this.lblTotalGastos.Text = "0.00";
            this.lblTotalGastos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Microsoft New Tai Lue", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 62);
            this.label3.TabIndex = 1;
            this.label3.Text = "Total:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // Listado_Gastos_ingresos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(939, 507);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Listado_Gastos_ingresos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AlanVent";
            this.Load += new System.EventHandler(this.Listado_Gastos_ingresos_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datalistado_Ingresos)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datalistado_Gastos)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private UIDC.UI_GradientPanel uI_GradientPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblTotal_ingresos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotalGastos;
        internal System.Windows.Forms.DataGridView datalistado_Ingresos;
        internal System.Windows.Forms.DataGridView datalistado_Gastos;
        private System.Windows.Forms.DataGridViewImageColumn Eliminar;
        private System.Windows.Forms.DataGridViewImageColumn EliminarI;
    }
}