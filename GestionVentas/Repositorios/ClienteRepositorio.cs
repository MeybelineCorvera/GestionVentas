using GestionVentas.Clases;
using RepoDb;

namespace GestionVentas.Repositorios
{
    public class ClienteRepositorio : RepositorioBase<Cliente>
    {
        //idExcluir sirve al editar: no compara el registro contra si mismo
        public bool ExisteDui(string dui, int idExcluir = 0)
        {
            using (var cn = ConexionBD.ObtenerConexion())
            {
                return cn.Count<Cliente>(x => x.Dui == dui && x.IdCliente != idExcluir) > 0;
            }
        }

        public bool ExisteCorreo(string correo, int idExcluir = 0)
        {
            using (var cn = ConexionBD.ObtenerConexion())
            {
                return cn.Count<Cliente>(x => x.Correo == correo && x.IdCliente != idExcluir) > 0;
            }
        }
    }
}
