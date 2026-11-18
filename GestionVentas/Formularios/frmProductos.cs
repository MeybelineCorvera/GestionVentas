using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GestionVentas.Clases;
using GestionVentas.Repositorios;

namespace GestionVentas.Formularios
{
    //Catalogo de productos con semaforo de stock (bonus).
    public partial class frmProductos : Form
    {
        private readonly ProductoRepositorio repo = new ProductoRepositorio();

        public frmProductos()
        {
            InitializeComponent();
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            Notificador.DatosCambiaron += CargarGrid;
            CargarGrid();
        }

        private void frmProductos_FormClosed(object sender, FormClosedEventArgs e)
        {
            Notificador.DatosCambiaron -= CargarGrid;
        }

        //datagrid
        private void CargarGrid()
        {
            string texto = txtBuscar.Text.Trim().ToLower();

            dgvProductos.DataSource = repo.ObtenerTodos()
                .Where(p => texto == "" || p.Nombre.ToLower().Contains(texto) || p.Marca.ToLower().Contains(texto))
                .OrderBy(p => p.IdProducto)
                .ToList();

            dgvProductos.Columns["IdProducto"].Visible = false;
            dgvProductos.Columns["Nombre"].HeaderText = "Producto";
            dgvProductos.Columns["Marca"].HeaderText = "Marca";
            dgvProductos.Columns["Descripcion"].HeaderText = "Descripcion";
            dgvProductos.Columns["Precio"].HeaderText = "Precio";
            dgvProductos.Columns["Precio"].DefaultCellStyle.Format = "C2";
            dgvProductos.Columns["Stock"].HeaderText = "Stock";
            dgvProductos.ReadOnly = true;
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        //BONUS Semaforo de alerta: pinta la fila segun el stock 5 o menos   -> rojo de 6 a 10   -> ambar
        private void dgvProductos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            Producto p = dgvProductos.Rows[e.RowIndex].DataBoundItem as Producto;
            if (p == null) return;

            if (p.Stock <= 5)
            {
                e.CellStyle.BackColor = Color.LightCoral;
                e.CellStyle.ForeColor = Color.Black;
            }
            else if (p.Stock <= 10)
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 213, 79);
                e.CellStyle.ForeColor = Color.Black;
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarGrid();
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Editar();
        }

        //Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //Formulario hijo MODAL: bloquea esta ventana hasta que se guarde o se cancele
            using (frmProductoModal modal = new frmProductoModal())
            {
                modal.ShowDialog();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void Editar()
        {
            Producto seleccionado = dgvProductos.CurrentRow == null ? null : dgvProductos.CurrentRow.DataBoundItem as Producto;
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un producto de la lista.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (frmProductoModal modal = new frmProductoModal(seleccionado))
            {
                modal.ShowDialog();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Producto seleccionado = dgvProductos.CurrentRow == null ? null : dgvProductos.CurrentRow.DataBoundItem as Producto;
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un producto de la lista.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult r = MessageBox.Show("Desea eliminar el producto " + seleccionado.Nombre + "?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r != DialogResult.Yes) return;

            try
            {
                repo.Eliminar(seleccionado);
            }
            catch (Exception)
            {
                MessageBox.Show("No se puede eliminar: el producto tiene ventas registradas.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
