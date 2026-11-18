namespace GestionVentas.Formularios
{
    partial class frmReportes
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblReporte = new System.Windows.Forms.Label();
            this.cboReporte = new System.Windows.Forms.ComboBox();
            this.lblProducto = new System.Windows.Forms.Label();
            this.cboProducto = new System.Windows.Forms.ComboBox();
            this.btnExportar = new System.Windows.Forms.Button();
            this.lblLeyenda = new System.Windows.Forms.Label();
            this.lblResumen = new System.Windows.Forms.Label();
            this.dgvReporte = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(120, 29);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Reportes";
            // 
            // lblReporte
            // 
            this.lblReporte.AutoSize = true;
            this.lblReporte.Location = new System.Drawing.Point(20, 63);
            this.lblReporte.Name = "lblReporte";
            this.lblReporte.Size = new System.Drawing.Size(56, 16);
            this.lblReporte.TabIndex = 1;
            this.lblReporte.Text = "Reporte";
            // 
            // cboReporte
            // 
            this.cboReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReporte.FormattingEnabled = true;
            this.cboReporte.Items.AddRange(new object[] {
            "Productos vendidos (cantidades y montos)",
            "Stock de inventario"});
            this.cboReporte.Location = new System.Drawing.Point(90, 60);
            this.cboReporte.Name = "cboReporte";
            this.cboReporte.Size = new System.Drawing.Size(320, 24);
            this.cboReporte.TabIndex = 2;
            this.cboReporte.SelectedIndexChanged += new System.EventHandler(this.cboReporte_SelectedIndexChanged);
            // 
            // lblProducto
            // 
            this.lblProducto.AutoSize = true;
            this.lblProducto.Location = new System.Drawing.Point(430, 63);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(61, 16);
            this.lblProducto.TabIndex = 3;
            this.lblProducto.Text = "Producto";
            // 
            // cboProducto
            // 
            this.cboProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProducto.FormattingEnabled = true;
            this.cboProducto.Location = new System.Drawing.Point(500, 60);
            this.cboProducto.Name = "cboProducto";
            this.cboProducto.Size = new System.Drawing.Size(300, 24);
            this.cboProducto.TabIndex = 4;
            this.cboProducto.SelectedIndexChanged += new System.EventHandler(this.cboProducto_SelectedIndexChanged);
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(820, 56);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(130, 30);
            this.btnExportar.TabIndex = 5;
            this.btnExportar.Text = "Exportar PDF";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // lblLeyenda
            // 
            this.lblLeyenda.AutoSize = true;
            this.lblLeyenda.Location = new System.Drawing.Point(20, 100);
            this.lblLeyenda.Name = "lblLeyenda";
            this.lblLeyenda.Size = new System.Drawing.Size(0, 16);
            this.lblLeyenda.TabIndex = 6;
            // 
            // lblResumen
            // 
            this.lblResumen.AutoSize = true;
            this.lblResumen.Location = new System.Drawing.Point(20, 125);
            this.lblResumen.Name = "lblResumen";
            this.lblResumen.Size = new System.Drawing.Size(0, 16);
            this.lblResumen.TabIndex = 7;
            // 
            // dgvReporte
            // 
            this.dgvReporte.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReporte.Location = new System.Drawing.Point(20, 155);
            this.dgvReporte.Name = "dgvReporte";
            this.dgvReporte.RowHeadersWidth = 51;
            this.dgvReporte.RowTemplate.Height = 24;
            this.dgvReporte.Size = new System.Drawing.Size(950, 420);
            this.dgvReporte.TabIndex = 8;
            this.dgvReporte.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvReporte_CellFormatting);
            // 
            // frmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblReporte);
            this.Controls.Add(this.cboReporte);
            this.Controls.Add(this.lblProducto);
            this.Controls.Add(this.cboProducto);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.lblLeyenda);
            this.Controls.Add(this.lblResumen);
            this.Controls.Add(this.dgvReporte);
            this.Name = "frmReportes";
            this.Text = "Reportes";
            this.Load += new System.EventHandler(this.frmReportes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblReporte;
        private System.Windows.Forms.ComboBox cboReporte;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.ComboBox cboProducto;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Label lblLeyenda;
        private System.Windows.Forms.Label lblResumen;
        private System.Windows.Forms.DataGridView dgvReporte;
    }
}
