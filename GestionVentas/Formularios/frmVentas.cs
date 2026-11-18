using System;
using System.Windows.Forms;
using GestionVentas.Clases;
using GestionVentas.Repositorios;

namespace GestionVentas.Formularios
{
    public partial class frmVentas : Form
    {
        private readonly VentaRepositorio repo = new VentaRepositorio();

        public frmVentas()
        {
            InitializeComponent();
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            Notificador.DatosCambiaron += CargarGrid;
            CargarGrid();
        }

        private void frmVentas_FormClosed(object sender, FormClosedEventArgs e)
        {
            Notificador.DatosCambiaron -= CargarGrid;
        }

        private void CargarGrid()
        {
            dgvVentas.DataSource = repo.ObtenerResumen();

            dgvVentas.Columns["IdVenta"].HeaderText = "N. de venta";
            dgvVentas.Columns["Fecha"].HeaderText = "Fecha";
            dgvVentas.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvVentas.Columns["Cliente"].HeaderText = "Cliente";
            dgvVentas.Columns["Empleado"].HeaderText = "Empleado";
            dgvVentas.Columns["Total"].HeaderText = "Total";
            dgvVentas.Columns["Total"].DefaultCellStyle.Format = "C2";
            dgvVentas.ReadOnly = true;
            dgvVentas.AllowUserToAddRows = false;
            dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnNueva_Click(object sender, EventArgs e)
        {
            using (frmNuevaVenta modal = new frmNuevaVenta())
            {
                modal.ShowDialog();
            }
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            VerDetalle();
        }

        private void dgvVentas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            VerDetalle();
        }

        private void VerDetalle()
        {
            VentaResumen venta = dgvVentas.CurrentRow == null ? null : dgvVentas.CurrentRow.DataBoundItem as VentaResumen;
            if (venta == null)
            {
                MessageBox.Show("Seleccione una venta de la lista.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (frmDetalleVenta modal = new frmDetalleVenta(venta.IdVenta))
            {
                modal.ShowDialog();
            }
        }
    }
}
