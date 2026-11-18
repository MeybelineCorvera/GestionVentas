using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using GestionVentas.Clases;
using GestionVentas.Repositorios;

namespace GestionVentas.Formularios
{
    //Este es un formulario hijo modal: registra una venta (cliente + carrito de productos).
    //El empleado es el que inicio sesion. Al guardar se descuenta el stock en una transaccion.
    public partial class frmNuevaVenta : Form
    {
        private readonly BindingList<LineaVenta> carrito = new BindingList<LineaVenta>();
        private List<Producto> productos;

        public frmNuevaVenta()
        {
            InitializeComponent();
        }

        private void frmNuevaVenta_Load(object sender, EventArgs e)
        {
            lblEmpleadoValor.Text = Sesion.Actual.NombreCompleto;

            // Clientes
            cboCliente.DisplayMember = "Texto";
            cboCliente.ValueMember = "Id";
            cboCliente.DataSource = new ClienteRepositorio().ObtenerTodos()
                .Select(c => new ItemCombo { Id = c.IdCliente, Texto = c.NombreCompleto + " (" + c.Dui + ")" }).ToList();
            cboCliente.SelectedIndex = -1;

            // Productos con existencias
            productos = new ProductoRepositorio().ObtenerTodos().Where(p => p.Stock > 0).OrderBy(p => p.Nombre).ToList();
            cboProducto.DisplayMember = "Texto";
            cboProducto.ValueMember = "Id";
            cboProducto.DataSource = productos
                .Select(p => new ItemCombo
                {
                    Id = p.IdProducto,
                    Texto = p.Nombre + " (" + p.Marca + ") - " + p.Precio.ToString("C2") + " - Stock: " + p.Stock
                }).ToList();
            cboProducto.SelectedIndex = -1;

            // Carrito
            dgvCarrito.DataSource = carrito;
            dgvCarrito.Columns["IdProducto"].Visible = false;
            dgvCarrito.Columns["Marca"].Visible = false;
            dgvCarrito.Columns["Producto"].HeaderText = "Producto";
            dgvCarrito.Columns["Cantidad"].HeaderText = "Cant.";
            dgvCarrito.Columns["PrecioUnitario"].HeaderText = "Precio";
            dgvCarrito.Columns["PrecioUnitario"].DefaultCellStyle.Format = "C2";
            dgvCarrito.Columns["Subtotal"].HeaderText = "Subtotal";
            dgvCarrito.Columns["Subtotal"].DefaultCellStyle.Format = "C2";
            dgvCarrito.ReadOnly = true;
            dgvCarrito.AllowUserToAddRows = false;
            dgvCarrito.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCarrito.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        //Validacion de teclas (KeyPress)
        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; //Bloquea cualquier otra tecla
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Carrito
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cboProducto.SelectedValue == null)
            {
                Aviso("Seleccione un producto.");
                return;
            }

            int cantidad;
            if (!int.TryParse(txtCantidad.Text.Trim(), out cantidad) || cantidad <= 0)
            {
                Aviso("Escriba una cantidad mayor que 0.");
                txtCantidad.Focus();
                return;
            }

            int idProducto = (int)cboProducto.SelectedValue;
            Producto p = productos.First(x => x.IdProducto == idProducto);

            //Si el producto ya esta en el carrito se suma la cantidad
            LineaVenta existente = carrito.FirstOrDefault(x => x.IdProducto == idProducto);
            int yaEnCarrito = existente == null ? 0 : existente.Cantidad;

            if (yaEnCarrito + cantidad > p.Stock)
            {
                Aviso("Stock insuficiente. Disponible: " + p.Stock +
                      (yaEnCarrito > 0 ? " (ya agrego " + yaEnCarrito + " al carrito)." : "."));
                return;
            }

            if (existente == null)
            {
                carrito.Add(new LineaVenta
                {
                    IdProducto = p.IdProducto,
                    Producto = p.Nombre,
                    Marca = p.Marca,
                    Cantidad = cantidad,
                    PrecioUnitario = p.Precio
                });
            }
            else
            {
                existente.Cantidad += cantidad;
                carrito.ResetBindings();
            }

            ActualizarTotal();
            txtCantidad.Clear();
            cboProducto.SelectedIndex = -1;
            cboProducto.Focus();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            LineaVenta linea = dgvCarrito.CurrentRow == null ? null : dgvCarrito.CurrentRow.DataBoundItem as LineaVenta;
            if (linea == null)
            {
                Aviso("Seleccione una linea del carrito.");
                return;
            }

            carrito.Remove(linea);
            ActualizarTotal();
        }

        private void ActualizarTotal()
        {
            lblTotal.Text = "Total: " + carrito.Sum(x => x.Subtotal).ToString("C2");
        }

        // ---------- Guardar / Cancelar ----------
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cboCliente.SelectedValue == null)
            {
                Aviso("Seleccione el cliente.");
                return;
            }
            if (carrito.Count == 0)
            {
                Aviso("Agregue al menos un producto a la venta.");
                return;
            }

            DialogResult r = MessageBox.Show(
                "Registrar la venta por " + carrito.Sum(x => x.Subtotal).ToString("C2") + "?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r != DialogResult.Yes) return;

            try
            {
                Venta venta = new Venta
                {
                    FechaVenta = DateTime.Now,
                    IdCliente = (int)cboCliente.SelectedValue,
                    IdEmpleado = Sesion.Actual.IdEmpleado
                };

                int idVenta = new VentaRepositorio().RegistrarVenta(venta, carrito.ToList());

                MessageBox.Show("Venta N. " + idVenta + " registrada correctamente.", "Gestion de Ventas",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo registrar la venta:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Aviso(string mensaje)
        {
            MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
