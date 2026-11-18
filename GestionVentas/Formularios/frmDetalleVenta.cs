using System;
using System.Linq;
using System.Windows.Forms;
using GestionVentas.Clases;
using GestionVentas.Repositorios;

namespace GestionVentas.Formularios
{
    //Este es un formulario hijo modal de solo lectura: productos, cantidades y subtotales de una venta
    public partial class frmDetalleVenta : Form
    {
        private readonly int idVenta;

        public frmDetalleVenta() : this(0)
        {
        }

        public frmDetalleVenta(int idVenta)
        {
            this.idVenta = idVenta;
            InitializeComponent();
        }

        private void frmDetalleVenta_Load(object sender, EventArgs e)
        {
            VentaRepositorio repo = new VentaRepositorio();

            VentaResumen venta = repo.ObtenerResumen().FirstOrDefault(v => v.IdVenta == idVenta);
            if (venta != null)
            {
                lblInfo.Text = "Venta N. " + venta.IdVenta + "   |   " + venta.Fecha.ToString("dd/MM/yyyy HH:mm") +
                               "\nCliente: " + venta.Cliente + "   |   Empleado: " + venta.Empleado;
                lblTotal.Text = "Total: " + venta.Total.ToString("C2");
            }

            dgvDetalle.DataSource = repo.ObtenerDetalle(idVenta);
            dgvDetalle.Columns["IdProducto"].Visible = false;
            dgvDetalle.Columns["Producto"].HeaderText = "Producto";
            dgvDetalle.Columns["Marca"].HeaderText = "Marca";
            dgvDetalle.Columns["Cantidad"].HeaderText = "Cantidad";
            dgvDetalle.Columns["PrecioUnitario"].HeaderText = "Precio unitario";
            dgvDetalle.Columns["PrecioUnitario"].DefaultCellStyle.Format = "C2";
            dgvDetalle.Columns["Subtotal"].HeaderText = "Subtotal";
            dgvDetalle.Columns["Subtotal"].DefaultCellStyle.Format = "C2";
            dgvDetalle.ReadOnly = true;
            dgvDetalle.AllowUserToAddRows = false;
            dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
