namespace GestionVentas.Formularios
{
    partial class frmNuevaVenta
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
            this.lblCliente = new System.Windows.Forms.Label();
            this.cboCliente = new System.Windows.Forms.ComboBox();
            this.lblEmpleado = new System.Windows.Forms.Label();
            this.lblEmpleadoValor = new System.Windows.Forms.Label();
            this.lblProducto = new System.Windows.Forms.Label();
            this.cboProducto = new System.Windows.Forms.ComboBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.dgvCarrito = new System.Windows.Forms.DataGridView();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(60, 17);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Nueva venta";
            //
            // lblCliente
            //
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(20, 73);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(60, 17);
            this.lblCliente.TabIndex = 1;
            this.lblCliente.Text = "Cliente";
            //
            // cboCliente
            //
            this.cboCliente.Location = new System.Drawing.Point(110, 70);
            this.cboCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCliente.FormattingEnabled = true;
            this.cboCliente.Name = "cboCliente";
            this.cboCliente.Size = new System.Drawing.Size(330, 24);
            this.cboCliente.TabIndex = 2;
            //
            // lblEmpleado
            //
            this.lblEmpleado.AutoSize = true;
            this.lblEmpleado.Location = new System.Drawing.Point(20, 113);
            this.lblEmpleado.Name = "lblEmpleado";
            this.lblEmpleado.Size = new System.Drawing.Size(60, 17);
            this.lblEmpleado.TabIndex = 3;
            this.lblEmpleado.Text = "Empleado";
            //
            // lblEmpleadoValor
            //
            this.lblEmpleadoValor.AutoSize = true;
            this.lblEmpleadoValor.Location = new System.Drawing.Point(110, 113);
            this.lblEmpleadoValor.Name = "lblEmpleadoValor";
            this.lblEmpleadoValor.Size = new System.Drawing.Size(60, 17);
            this.lblEmpleadoValor.TabIndex = 4;
            this.lblEmpleadoValor.Text = "";
            //
            // lblProducto
            //
            this.lblProducto.AutoSize = true;
            this.lblProducto.Location = new System.Drawing.Point(20, 153);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(60, 17);
            this.lblProducto.TabIndex = 5;
            this.lblProducto.Text = "Producto";
            //
            // cboProducto
            //
            this.cboProducto.Location = new System.Drawing.Point(110, 150);
            this.cboProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProducto.FormattingEnabled = true;
            this.cboProducto.Name = "cboProducto";
            this.cboProducto.Size = new System.Drawing.Size(330, 24);
            this.cboProducto.TabIndex = 6;
            //
            // lblCantidad
            //
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(20, 193);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(60, 17);
            this.lblCantidad.TabIndex = 7;
            this.lblCantidad.Text = "Cantidad";
            //
            // txtCantidad
            //
            this.txtCantidad.Location = new System.Drawing.Point(110, 190);
            this.txtCantidad.MaxLength = 4;
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(80, 22);
            this.txtCantidad.TabIndex = 8;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            //
            // btnAgregar
            //
            this.btnAgregar.Location = new System.Drawing.Point(110, 235);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(120, 34);
            this.btnAgregar.TabIndex = 9;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            //
            // btnGuardar
            //
            this.btnGuardar.Location = new System.Drawing.Point(110, 440);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(150, 34);
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.Text = "Registrar venta";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            //
            // btnCancelar
            //
            this.btnCancelar.Location = new System.Drawing.Point(270, 440);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 34);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // dgvCarrito
            //
            this.dgvCarrito.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCarrito.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCarrito.Location = new System.Drawing.Point(470, 70);
            this.dgvCarrito.Name = "dgvCarrito";
            this.dgvCarrito.RowHeadersWidth = 51;
            this.dgvCarrito.RowTemplate.Height = 24;
            this.dgvCarrito.Size = new System.Drawing.Size(410, 340);
            this.dgvCarrito.TabIndex = 12;
            //
            // btnQuitar
            //
            this.btnQuitar.Location = new System.Drawing.Point(470, 420);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(100, 34);
            this.btnQuitar.TabIndex = 13;
            this.btnQuitar.Text = "Quitar";
            this.btnQuitar.UseVisualStyleBackColor = true;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            //
            // lblTotal
            //
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(600, 428);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(60, 17);
            this.lblTotal.TabIndex = 14;
            this.lblTotal.Text = "Total: $0.00";
            //
            // frmNuevaVenta
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(900, 520);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.cboCliente);
            this.Controls.Add(this.lblEmpleado);
            this.Controls.Add(this.lblEmpleadoValor);
            this.Controls.Add(this.lblProducto);
            this.Controls.Add(this.cboProducto);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.dgvCarrito);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.lblTotal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNuevaVenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nueva venta";
            this.Load += new System.EventHandler(this.frmNuevaVenta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.ComboBox cboCliente;
        private System.Windows.Forms.Label lblEmpleado;
        private System.Windows.Forms.Label lblEmpleadoValor;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.ComboBox cboProducto;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.DataGridView dgvCarrito;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Label lblTotal;
    }
}
