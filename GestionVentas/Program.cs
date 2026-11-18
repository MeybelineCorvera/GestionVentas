using System;
using System.Windows.Forms;
using RepoDb;
using GestionVentas.Clases;
using GestionVentas.Formularios;

namespace GestionVentas
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            //Configura RepoDb para SQL Server
            GlobalConfiguration.Setup().UseSqlServer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Login -> Panel. Si se cierra la sesion desde el Panel, vuelve al login.
            while (true)
            {
                using (frmLogin login = new frmLogin())
                {
                    if (login.ShowDialog() != DialogResult.OK) break;
                }

                Application.Run(new Formularios.Panel());

                //Si la sesion sigue abierta, el usuario cerro la ventana con la X: salir.
                if (Sesion.HayUsuario) break;
            }
        }
    }
}
