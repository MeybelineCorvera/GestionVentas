using System.Collections.Generic;
using System.Linq;
using GestionVentas.Clases;

namespace GestionVentas.Repositorios
{
    public class ProductoRepositorio : RepositorioBase<Producto>
    {
        //Reporte de existencias. idProducto = 0 -> catalogo completo; otro valor -> un solo producto.
        //Semaforo: Critico (5 o menos), Bajo (de 6 a 10) y Normal (mas de 10).
        public List<StockItem> ObtenerStock(int idProducto = 0)
        {
            return (from p in ObtenerTodos()
                    where idProducto == 0 || p.IdProducto == idProducto
                    orderby p.Stock, p.Nombre
                    select new StockItem
                    {
                        IdProducto = p.IdProducto,
                        Producto = p.Nombre,
                        Marca = p.Marca,
                        Precio = p.Precio,
                        Stock = p.Stock,
                        Estado = p.Stock <= 5 ? "Critico" : (p.Stock <= 10 ? "Bajo" : "Normal")
                    }).ToList();
        }
    }
}
