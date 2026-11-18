using System.Linq;
using GestionVentas.Clases;
using RepoDb;

namespace GestionVentas.Repositorios
{
    public class EmpleadoRepositorio : RepositorioBase<Empleado>
    {
        //el login: se calcula el SHA-256 de la clave escrita y se compara contra el hash guardado.
        //devuelve el empleado si usuario y clave son correctos; si no, null.
        public Empleado Validar(string usuario, string clave)
        {
            string hash = Seguridad.CalcularHash(clave);
            using (var cn = ConexionBD.ObtenerConexion())
            {
                return cn.Query<Empleado>(x => x.Usuario == usuario && x.Clave == hash)
                         .FirstOrDefault();
            }
        }
    }
}
