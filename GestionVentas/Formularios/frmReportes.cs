using System;
using System.Collections.Generic;
using GestionVentas.Reportes;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using GestionVentas.Clases;
using GestionVentas.Repositorios;

namespace GestionVentas.Formularios
{
    //-Productos vendidos: cantidades y montos acumulados
    //-Stock de inventario con semaforo (bonus). Ambos se pueden filtrar por UN producto o ver todos.
    public partial class frmReportes : Form
    {
        private bool cargando = true;   //evita generar el reporte mientras se llenan los combos

        public frmReportes()
        {
            InitializeComponent();
        }

        private void frmReportes_Load(object sender, EventArgs e)
        {
            List<ItemCombo> lista = new List<ItemCombo> { new ItemCombo { Id = 0, Texto = "Todos los productos" } };
            lista.AddRange(new ProductoRepositorio().ObtenerTodos().OrderBy(p => p.Nombre)
                .Select(p => new ItemCombo { Id = p.IdProducto, Texto = p.Nombre + " (" + p.Marca + ")" }));

            cboProducto.DisplayMember = "Texto";
            cboProducto.ValueMember = "Id";
            cboProducto.DataSource = lista;

            cargando = false;
            cboReporte.SelectedIndex = 0;   //dispara el primer reporte
        }

        private void cboReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerarReporte();
        }

        private void cboProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerarReporte();
        }

        private void GenerarReporte()
        {
            if (cargando || cboReporte.SelectedIndex < 0 || cboProducto.SelectedValue == null) return;

            int idProducto = (int)cboProducto.SelectedValue;

            try
            {
                if (cboReporte.SelectedIndex == 0)
                    ReporteVendidos(idProducto);
                else
                    ReporteStock(idProducto);

                dgvReporte.ReadOnly = true;
                dgvReporte.AllowUserToAddRows = false;
                dgvReporte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo generar el reporte:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Resumen de productos vendidos
        private void ReporteVendidos(int idProducto)
        {
            List<ProductoVendido> lista = new RepositorioReporteVentas().ObtenerProductosVendidos(idProducto);
            dgvReporte.DataSource = lista;

            dgvReporte.Columns["IdProducto"].Visible = false;
            dgvReporte.Columns["Producto"].HeaderText = "Producto";
            dgvReporte.Columns["Marca"].HeaderText = "Marca";
            dgvReporte.Columns["CantidadVendida"].HeaderText = "Unidades vendidas";
            dgvReporte.Columns["MontoAcumulado"].HeaderText = "Monto acumulado";
            dgvReporte.Columns["MontoAcumulado"].DefaultCellStyle.Format = "C2";

            lblLeyenda.Text = "";
            lblResumen.Text = "Productos: " + lista.Count + "   |   Unidades vendidas: " + lista.Sum(x => x.CantidadVendida) +
                              "   |   Monto total: " + lista.Sum(x => x.MontoAcumulado).ToString("C2");
        }

        //Stock de inventario con semaforo
        private void ReporteStock(int idProducto)
        {
            List<StockItem> lista = new RepositorioReporteVentas().ObtenerStock(idProducto);
            dgvReporte.DataSource = lista;

            dgvReporte.Columns["IdProducto"].Visible = false;
            dgvReporte.Columns["Producto"].HeaderText = "Producto";
            dgvReporte.Columns["Marca"].HeaderText = "Marca";
            dgvReporte.Columns["Precio"].HeaderText = "Precio";
            dgvReporte.Columns["Precio"].DefaultCellStyle.Format = "C2";
            dgvReporte.Columns["Stock"].HeaderText = "Existencias";
            dgvReporte.Columns["Estado"].HeaderText = "Estado";

            lblLeyenda.Text = "Semaforo:  ROJO = stock critico (5 o menos)  |  AMBAR = stock bajo (6 a 10)";
            lblResumen.Text = "Productos: " + lista.Count + "   |   Criticos: " + lista.Count(x => x.Stock <= 5) +
                              "   |   Bajos: " + lista.Count(x => x.Stock > 5 && x.Stock <= 10) +
                              "   |   Unidades en inventario: " + lista.Sum(x => x.Stock);
        }

        //BONUS Semaforo de alerta en el reporte de stock
        private void dgvReporte_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            StockItem item = dgvReporte.Rows[e.RowIndex].DataBoundItem as StockItem;
            if (item == null) return;

            if (item.Stock <= 5)
            {
                e.CellStyle.BackColor = Color.LightCoral;
                e.CellStyle.ForeColor = Color.Black;
            }
            else if (item.Stock <= 10)
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 213, 79);
                e.CellStyle.ForeColor = Color.Black;
            }
        }

        //Exportar a PDF
        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvReporte.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "Documento PDF (*.pdf)|*.pdf";
                dlg.FileName = "Reporte_" + (cboReporte.SelectedIndex == 0 ? "ProductosVendidos" : "Stock") +
                               "_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".pdf";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                try
                {
                    string generadoPor = Sesion.Actual.NombreCompleto;

                    if (cboReporte.SelectedIndex == 0)
                        ReporteProductosVendidosPdf.Exportar(dlg.FileName,
                            (List<ProductoVendido>)dgvReporte.DataSource, cboProducto.Text, generadoPor);
                    else
                        ReporteStockPdf.Exportar(dlg.FileName,
                            (List<StockItem>)dgvReporte.DataSource, cboProducto.Text, generadoPor);

                    DialogResult abrir = MessageBox.Show("Reporte generado correctamente.\n\nDesea abrirlo ahora?",
                        "Gestion de Ventas", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (abrir == DialogResult.Yes) Process.Start(dlg.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo generar el PDF:\n" + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
