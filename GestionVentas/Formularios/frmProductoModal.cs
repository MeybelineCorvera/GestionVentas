using System;
using System.Globalization;
using System.Windows.Forms;
using GestionVentas.Clases;
using GestionVentas.Repositorios;

namespace GestionVentas.Formularios
{
    //Este es un formulario hijo modal: alta y edicion de un producto.
    public partial class frmProductoModal : Form
    {
        private readonly ProductoRepositorio repo = new ProductoRepositorio();
        private readonly Producto producto;   //null = producto nuevo

        public frmProductoModal() : this(null)
        {
        }

        public frmProductoModal(Producto producto)
        {
            this.producto = producto == null ? null : producto.Copiar();
            InitializeComponent();
        }

        private void frmProductoModal_Load(object sender, EventArgs e)
        {
            if (producto == null)
            {
                lblTitulo.Text = "Nuevo producto";
                return;
            }

            lblTitulo.Text = "Editar producto";
            txtNombre.Text = producto.Nombre;
            txtMarca.Text = producto.Marca;
            txtDescripcion.Text = producto.Descripcion;
            txtPrecio.Text = producto.Precio.ToString("0.00", CultureInfo.InvariantCulture);
            txtStock.Text = producto.Stock.ToString();
        }

        //Validacion de teclas (KeyPress)
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && " -.,\"/()+&'".IndexOf(e.KeyChar) < 0)
            {
                e.Handled = true; //Bloquea cualquier otra tecla
                MessageBox.Show("Caracter no permitido en el nombre", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && " -.&'".IndexOf(e.KeyChar) < 0)
            {
                e.Handled = true; //Bloquea cualquier otra tecla
                MessageBox.Show("Caracter no permitido en la marca", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && " -.,\"/()+&':".IndexOf(e.KeyChar) < 0)
            {
                e.Handled = true; //Bloquea cualquier otra tecla
                MessageBox.Show("Caracter no permitido en la descripcion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool puntoRepetido = e.KeyChar == '.' && txtPrecio.Text.Contains(".");
            if ((!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar)) || puntoRepetido)
            {
                e.Handled = true; //Bloquea cualquier otra tecla
                MessageBox.Show("Solo numeros y un punto decimal", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; //Bloquea cualquier otra tecla
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Botones
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string marca = txtMarca.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();
            decimal precio;
            int stock;

            if (nombre == "" || marca == "")
            {
                Aviso("El nombre y la marca son obligatorios.");
                return;
            }
            if (!decimal.TryParse(txtPrecio.Text.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out precio) || precio <= 0)
            {
                Aviso("El precio debe ser un numero mayor que 0.");
                txtPrecio.Focus();
                return;
            }
            if (!int.TryParse(txtStock.Text.Trim(), out stock) || stock < 0)
            {
                Aviso("El stock debe ser un numero entero (0 o mas).");
                txtStock.Focus();
                return;
            }

            try
            {
                if (producto == null)
                {
                    repo.Insertar(new Producto
                    {
                        Nombre = nombre,
                        Marca = marca,
                        Descripcion = descripcion == "" ? null : descripcion,
                        Precio = Math.Round(precio, 2),
                        Stock = stock
                    });
                    MessageBox.Show("Producto guardado.", "Gestion de Ventas",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    producto.Nombre = nombre;
                    producto.Marca = marca;
                    producto.Descripcion = descripcion == "" ? null : descripcion;
                    producto.Precio = Math.Round(precio, 2);
                    producto.Stock = stock;
                    repo.Actualizar(producto);
                    MessageBox.Show("Producto actualizado.", "Gestion de Ventas",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar:\n" + ex.Message, "Error",
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
