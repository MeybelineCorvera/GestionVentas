using System.Collections.Generic;
using System.Linq;
using GestionVentas.Clases;
using RepoDb;

namespace GestionVentas.Repositorios
{
    public class DetalleVentaRepositorio : RepositorioBase<DetalleVenta>
    {
        public List<DetalleVenta> ObtenerPorVenta(int idVenta)
        {
            using (var cn = ConexionBD.ObtenerConexion())
            {
                return cn.Query<DetalleVenta>(x => x.IdVenta == idVenta).ToList();
            }
        }
    }
}
