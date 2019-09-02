namespace LBInventory.Remision
{
    partial class FormSalidaMercancia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSalidaMercancia));
            this.btnReporte = new System.Windows.Forms.Button();
            this.btnGenTRemision = new System.Windows.Forms.Button();
            this.btnCapturarCodigo = new System.Windows.Forms.Button();
            this.dataGridVentas = new System.Windows.Forms.DataGridView();
            this.lblRFC = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblOrden = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOrdenVenta = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVentas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReporte
            // 
            this.btnReporte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReporte.Enabled = false;
            this.btnReporte.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReporte.Image = ((System.Drawing.Image)(resources.GetObject("btnReporte.Image")));
            this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReporte.Location = new System.Drawing.Point(444, 598);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Size = new System.Drawing.Size(250, 60);
            this.btnReporte.TabIndex = 26;
            this.btnReporte.Text = "Generar Reporte";
            this.btnReporte.UseVisualStyleBackColor = false;
            // 
            // btnGenTRemision
            // 
            this.btnGenTRemision.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenTRemision.Enabled = false;
            this.btnGenTRemision.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenTRemision.Image = ((System.Drawing.Image)(resources.GetObject("btnGenTRemision.Image")));
            this.btnGenTRemision.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenTRemision.Location = new System.Drawing.Point(697, 598);
            this.btnGenTRemision.Name = "btnGenTRemision";
            this.btnGenTRemision.Size = new System.Drawing.Size(250, 60);
            this.btnGenTRemision.TabIndex = 25;
            this.btnGenTRemision.Text = "Generar Salida";
            this.btnGenTRemision.UseVisualStyleBackColor = false;
            // 
            // btnCapturarCodigo
            // 
            this.btnCapturarCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCapturarCodigo.Enabled = false;
            this.btnCapturarCodigo.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapturarCodigo.Image = ((System.Drawing.Image)(resources.GetObject("btnCapturarCodigo.Image")));
            this.btnCapturarCodigo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCapturarCodigo.Location = new System.Drawing.Point(950, 598);
            this.btnCapturarCodigo.Name = "btnCapturarCodigo";
            this.btnCapturarCodigo.Size = new System.Drawing.Size(250, 60);
            this.btnCapturarCodigo.TabIndex = 24;
            this.btnCapturarCodigo.Text = "Capturar Codigos";
            this.btnCapturarCodigo.UseVisualStyleBackColor = false;
            // 
            // dataGridVentas
            // 
            this.dataGridVentas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridVentas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridVentas.BackgroundColor = System.Drawing.Color.White;
            this.dataGridVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridVentas.Location = new System.Drawing.Point(94, 231);
            this.dataGridVentas.Name = "dataGridVentas";
            this.dataGridVentas.Size = new System.Drawing.Size(990, 320);
            this.dataGridVentas.TabIndex = 23;
            this.dataGridVentas.DataSourceChanged += new System.EventHandler(this.DataGridVentas_DataSourceChanged);
            // 
            // lblRFC
            // 
            this.lblRFC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRFC.AutoSize = true;
            this.lblRFC.Location = new System.Drawing.Point(700, 120);
            this.lblRFC.Name = "lblRFC";
            this.lblRFC.Size = new System.Drawing.Size(135, 19);
            this.lblRFC.TabIndex = 22;
            this.lblRFC.Text = "Importe total:";
            // 
            // lblNombre
            // 
            this.lblNombre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(200, 170);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(144, 19);
            this.lblNombre.TabIndex = 21;
            this.lblNombre.Text = "Nombre Cliente:";
            // 
            // lblOrden
            // 
            this.lblOrden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOrden.AutoSize = true;
            this.lblOrden.Location = new System.Drawing.Point(200, 120);
            this.lblOrden.Name = "lblOrden";
            this.lblOrden.Size = new System.Drawing.Size(144, 19);
            this.lblOrden.TabIndex = 20;
            this.lblOrden.Text = "Orden de Venta:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(527, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 24);
            this.label2.TabIndex = 19;
            this.label2.Text = "Salida de Mercancia";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(844, 65);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(35, 35);
            this.btnBuscar.TabIndex = 18;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(324, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 19);
            this.label1.TabIndex = 17;
            this.label1.Text = "Orden de Venta :";
            // 
            // txtOrdenVenta
            // 
            this.txtOrdenVenta.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtOrdenVenta.Location = new System.Drawing.Point(481, 70);
            this.txtOrdenVenta.Name = "txtOrdenVenta";
            this.txtOrdenVenta.Size = new System.Drawing.Size(350, 26);
            this.txtOrdenVenta.TabIndex = 16;
            this.txtOrdenVenta.TextChanged += new System.EventHandler(this.TxtOrdenVenta_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1157, 13);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 29);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // FormSalidaMercancia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 658);
            this.Controls.Add(this.btnReporte);
            this.Controls.Add(this.btnGenTRemision);
            this.Controls.Add(this.btnCapturarCodigo);
            this.Controls.Add(this.dataGridVentas);
            this.Controls.Add(this.lblRFC);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.lblOrden);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOrdenVenta);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormSalidaMercancia";
            this.Text = "SalidaMercancia";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVentas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReporte;
        private System.Windows.Forms.Button btnGenTRemision;
        private System.Windows.Forms.Button btnCapturarCodigo;
        private System.Windows.Forms.DataGridView dataGridVentas;
        private System.Windows.Forms.Label lblRFC;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblOrden;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtOrdenVenta;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}