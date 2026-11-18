using System;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;
using GestionVentas.Clases;
using GestionVentas.Repositorios;

namespace GestionVentas.Formularios
{
    public partial class frmClientes : Form
    {
        private readonly ClienteRepositorio repo = new ClienteRepositorio();
        private Cliente seleccionado = null;

        public frmClientes()
        {
            InitializeComponent();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            Notificador.DatosCambiaron += CargarGrid;
            CargarGrid();
        }

        private void frmClientes_FormClosed(object sender, FormClosedEventArgs e)
        {
            Notificador.DatosCambiaron -= CargarGrid;
        }

        //datagrid
        private void CargarGrid()
        {
            dgvClientes.DataSource = repo.ObtenerTodos().OrderBy(c => c.IdCliente).ToList();

            dgvClientes.Columns["IdCliente"].Visible = false;
            dgvClientes.Columns["NombreCompleto"].Visible = false;
            dgvClientes.Columns["Nombres"].HeaderText = "Nombres";
            dgvClientes.Columns["Apellidos"].HeaderText = "Apellidos";
            dgvClientes.Columns["Dui"].HeaderText = "DUI";
            dgvClientes.Columns["Telefono"].HeaderText = "Telefono";
            dgvClientes.Columns["Correo"].HeaderText = "Correo";
            dgvClientes.Columns["Direccion"].HeaderText = "Direccion";
            dgvClientes.ReadOnly = true;
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            Cliente fila = (Cliente)dgvClientes.Rows[e.RowIndex].DataBoundItem;
            seleccionado = fila.Copiar();

            txtNombres.Text = seleccionado.Nombres;
            txtApellidos.Text = seleccionado.Apellidos;
            txtDui.Text = seleccionado.Dui;
            txtTelefono.Text = seleccionado.Telefono;
            txtCorreo.Text = seleccionado.Correo;
            txtDireccion.Text = seleccionado.Direccion;
            btnGuardar.Text = "Actualizar";
        }

        //Validacion de teclas (KeyPress)
        private void txtNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; //Se bloquea cualquier otra tecla
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtApellidos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; //Se bloquea cualquier otra tecla
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtDui_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; //Bloquea cualquier otra tecla
                MessageBox.Show("El DUI solo lleva numeros y un guion (########-#)", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; //Bloquea cualquier otra tecla
                MessageBox.Show("Solo se permiten numeros y guion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //buttons
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombres = txtNombres.Text.Trim();
            string apellidos = txtApellidos.Text.Trim();
            string dui = txtDui.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string correo = txtCorreo.Text.Trim();
            string direccion = txtDireccion.Text.Trim();

            if (nombres == "" || apellidos == "")
            {
                Aviso("Los nombres y apellidos son obligatorios.");
                return;
            }

            //el DUI con formato ########-#
            if (dui.Length != 10 || dui[8] != '-' || !(dui.Substring(0, 8) + dui[9]).All(char.IsDigit))
            {
                Aviso("El DUI debe tener el formato ########-# (ejemplo: 01234567-8).");
                txtDui.Focus();
                return;
            }

            if (telefono != "" && telefono.Length < 8)
            {
                Aviso("El telefono debe tener al menos 8 digitos.");
                txtTelefono.Focus();
                return;
            }

            //El correo es obligatorio porque en la base de datos es UNICO
            if (correo == "" || !CorreoValido(correo))
            {
                Aviso("Escriba un correo valido (ejemplo: nombre@mail.com).");
                txtCorreo.Focus();
                return;
            }

            int idActual = seleccionado == null ? 0 : seleccionado.IdCliente;
            if (repo.ExisteDui(dui, idActual))
            {
                Aviso("Ya existe un cliente con ese DUI.");
                txtDui.Focus();
                return;
            }
            if (repo.ExisteCorreo(correo, idActual))
            {
                Aviso("Ya existe un cliente con ese correo.");
                txtCorreo.Focus();
                return;
            }

            try
            {
                if (seleccionado == null)
                {
                    repo.Insertar(new Cliente
                    {
                        Nombres = nombres,
                        Apellidos = apellidos,
                        Dui = dui,
                        Telefono = telefono == "" ? null : telefono,
                        Correo = correo,
                        Direccion = direccion == "" ? null : direccion
                    });
                    MessageBox.Show("Cliente guardado.", "Gestion de Ventas",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    seleccionado.Nombres = nombres;
                    seleccionado.Apellidos = apellidos;
                    seleccionado.Dui = dui;
                    seleccionado.Telefono = telefono == "" ? null : telefono;
                    seleccionado.Correo = correo;
                    seleccionado.Direccion = direccion == "" ? null : direccion;
                    repo.Actualizar(seleccionado);
                    MessageBox.Show("Cliente actualizado.", "Gestion de Ventas",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (seleccionado == null)
            {
                Aviso("Haga doble clic en un cliente de la lista para seleccionarlo.");
                return;
            }

            DialogResult r = MessageBox.Show("Desea eliminar a " + seleccionado.NombreCompleto + "?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r != DialogResult.Yes) return;

            try
            {
                repo.Eliminar(seleccionado);
                LimpiarCampos();
            }
            catch (Exception)
            {
                MessageBox.Show("No se puede eliminar: el cliente tiene ventas registradas.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //las Utilidades
        private bool CorreoValido(string correo)
        {
            try { return new MailAddress(correo).Address == correo; }
            catch { return false; }
        }

        private void LimpiarCampos()
        {
            seleccionado = null;
            txtNombres.Clear();
            txtApellidos.Clear();
            txtDui.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            btnGuardar.Text = "Guardar";
            txtNombres.Focus();
        }

        private void Aviso(string mensaje)
        {
            MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
