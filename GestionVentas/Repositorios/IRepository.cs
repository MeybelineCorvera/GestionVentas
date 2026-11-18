using System.Collections.Generic;

namespace GestionVentas.Repositorios
{
    //Interfaz generica: contrato de las operaciones basicas de persistencia
    public interface IRepository<T> where T : class
    {
        List<T> ObtenerTodos();
        T ObtenerPorId(int id);
        void Insertar(T entidad);
        void Actualizar(T entidad);
        void Eliminar(T entidad);
    }
}
