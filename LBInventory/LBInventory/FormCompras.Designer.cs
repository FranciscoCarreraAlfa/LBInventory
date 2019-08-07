namespace LBInventory
{
    partial class FormCompras
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCompras));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtOrdenCompra = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblOrden = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblRFC = new System.Windows.Forms.Label();
            this.dataGridCompras = new System.Windows.Forms.DataGridView();
            this.btnCapturarCodigo = new System.Windows.Forms.Button();
            this.btnGenTRecepcion = new System.Windows.Forms.Button();
            this.btnReporte = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCompras)).BeginInit();
            this.SuspendLayout();
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
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // txtOrdenCompra
            // 
            this.txtOrdenCompra.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtOrdenCompra.Location = new System.Drawing.Point(481, 70);
            this.txtOrdenCompra.Name = "txtOrdenCompra";
            this.txtOrdenCompra.Size = new System.Drawing.Size(350, 26);
            this.txtOrdenCompra.TabIndex = 4;
            this.txtOrdenCompra.TextChanged += new System.EventHandler(this.TxtOrdenCompra_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(322, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "Orden de Compra:";
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
            this.btnBuscar.TabIndex = 6;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(527, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(274, 24);
            this.label2.TabIndex = 7;
            this.label2.Text = "Recepción de Mercancia";
            // 
            // lblOrden
            // 
            this.lblOrden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOrden.AutoSize = true;
            this.lblOrden.Location = new System.Drawing.Point(200, 120);
            this.lblOrden.Name = "lblOrden";
            this.lblOrden.Size = new System.Drawing.Size(153, 19);
            this.lblOrden.TabIndex = 8;
            this.lblOrden.Text = "Orden de Compra:";
            // 
            // lblNombre
            // 
            this.lblNombre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(200, 170);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(162, 19);
            this.lblNombre.TabIndex = 9;
            this.lblNombre.Text = "Nombre Proveedor:";
            // 
            // lblRFC
            // 
            this.lblRFC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRFC.AutoSize = true;
            this.lblRFC.Location = new System.Drawing.Point(700, 120);
            this.lblRFC.Name = "lblRFC";
            this.lblRFC.Size = new System.Drawing.Size(135, 19);
            this.lblRFC.TabIndex = 10;
            this.lblRFC.Text = "Importe total:";
            // 
            // dataGridCompras
            // 
            this.dataGridCompras.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridCompras.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridCompras.BackgroundColor = System.Drawing.Color.White;
            this.dataGridCompras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridCompras.Location = new System.Drawing.Point(94, 231);
            this.dataGridCompras.Name = "dataGridCompras";
            this.dataGridCompras.Size = new System.Drawing.Size(990, 320);
            this.dataGridCompras.TabIndex = 11;
            this.dataGridCompras.DataSourceChanged += new System.EventHandler(this.DataGridCompras_DataSourceChanged);
            this.dataGridCompras.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DataGridCompras_KeyPress);
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
            this.btnCapturarCodigo.TabIndex = 12;
            this.btnCapturarCodigo.Text = "Capturar Codigos";
            this.btnCapturarCodigo.UseVisualStyleBackColor = false;
            this.btnCapturarCodigo.Click += new System.EventHandler(this.CapturarCodigos_Click);
            // 
            // btnGenTRecepcion
            // 
            this.btnGenTRecepcion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenTRecepcion.Enabled = false;
            this.btnGenTRecepcion.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenTRecepcion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenTRecepcion.Location = new System.Drawing.Point(697, 598);
            this.btnGenTRecepcion.Name = "btnGenTRecepcion";
            this.btnGenTRecepcion.Size = new System.Drawing.Size(250, 60);
            this.btnGenTRecepcion.TabIndex = 13;
            this.btnGenTRecepcion.Text = "Generar recepción";
            this.btnGenTRecepcion.UseVisualStyleBackColor = false;
            this.btnGenTRecepcion.Click += new System.EventHandler(this.BtnGenTRecepcion_Click);
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
            this.btnReporte.TabIndex = 14;
            this.btnReporte.Text = "Generar Reporte";
            this.btnReporte.UseVisualStyleBackColor = false;
            this.btnReporte.Click += new System.EventHandler(this.BtnReporte_Click);
            // 
            // FormCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 658);
            this.Controls.Add(this.btnReporte);
            this.Controls.Add(this.btnGenTRecepcion);
            this.Controls.Add(this.btnCapturarCodigo);
            this.Controls.Add(this.dataGridCompras);
            this.Controls.Add(this.lblRFC);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.lblOrden);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOrdenCompra);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "FormCompras";
            this.Text = "FormCompras";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCompras)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBuscar;
        public System.Windows.Forms.TextBox txtOrdenCompra;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOrden;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblRFC;
        private System.Windows.Forms.DataGridView dataGridCompras;
        private System.Windows.Forms.Button btnCapturarCodigo;
        private System.Windows.Forms.Button btnGenTRecepcion;
        private System.Windows.Forms.Button btnReporte;
    }
}