using System.Collections.Generic;
using System.Linq;
using GestionVentas.Clases;
using RepoDb;

namespace GestionVentas.Repositorios
{
    //Implementacion generica de IRepository<T> con RepoDb. Cada tabla hereda de aqui: RepositorioBase<Cliente>, RepositorioBase<Producto>, etc.
    public abstract class RepositorioBase<T> : IRepository<T> where T : class
    {
        public List<T> ObtenerTodos()
        {
            using (var cn = ConexionBD.ObtenerConexion())
            {
                return cn.QueryAll<T>().ToList();
            }
        }

        //Busca por la llave primaria (la que se marca con [Primary] en la clase)
        public T ObtenerPorId(int id)
        {
            using (var cn = ConexionBD.ObtenerConexion())
            {
                return cn.Query<T>(id).FirstOrDefault();
            }
        }

        public void Insertar(T entidad)
        {
            using (var cn = ConexionBD.ObtenerConexion())
            {
                cn.Insert<T>(entidad);
            }
            Notificador.Avisar();
        }

        public void Actualizar(T entidad)
        {
            using (var cn = ConexionBD.ObtenerConexion())
            {
                cn.Update<T>(entidad);
            }
            Notificador.Avisar();
        }

        public void Eliminar(T entidad)
        {
            using (var cn = ConexionBD.ObtenerConexion())
            {
                cn.Delete<T>(entidad);
            }
            Notificador.Avisar();
        }
    }
}
