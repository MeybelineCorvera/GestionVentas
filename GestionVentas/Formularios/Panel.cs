using System;
using System.Windows.Forms;
using GestionVentas.Clases;

namespace GestionVentas.Formularios
{
    // Formulario principal: contenedor MDI con el menu de navegacion
    public partial class Panel : Form
    {
        public Panel()
        {
            InitializeComponent();
        }

        private void Panel_Load(object sender, EventArgs e)
        {
            if (Sesion.HayUsuario)
                lblUsuario.Text = "Empleado: " + Sesion.Actual.NombreCompleto + "   |   Cargo: " + Sesion.Actual.Cargo;
        }

        private void CerrarFormularioActivo()
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }
        }

        private void AbrirFormulario(Form frm)
        {
            CerrarFormularioActivo();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        //el menu
        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmProductos());
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmClientes());
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmVentas());
        }

        private void reportesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmReportes());
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sesion.Cerrar();   // Program.cs vuelve a mostrar el login
            Close();
        }
    }
}
