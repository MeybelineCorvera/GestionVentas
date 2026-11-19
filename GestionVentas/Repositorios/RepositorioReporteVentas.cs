using System.Collections.Generic;
using System.Linq;
using GestionVentas.Clases;

namespace GestionVentas.Repositorios
{
    public class RepositorioReporteVentas
    {
        //-Productos vendidos: unidades y monto acumulado por producto idProducto = 0 -> todos (solo los que tienen ventas); otro valor -> ese producto.
        public List<ProductoVendido> ObtenerProductosVendidos(int idProducto = 0)
        {
            List<DetalleVenta> detalles = new DetalleVentaRepositorio().ObtenerTodos();

            return (from p in new ProductoRepositorio().ObtenerTodos()
                    where idProducto == 0 || p.IdProducto == idProducto
                    join d in detalles on p.IdProducto equals d.IdProducto into gd
                    let cantidad = gd.Sum(x => x.Cantidad)
                    where idProducto != 0 || cantidad > 0
                    orderby gd.Sum(x => x.Cantidad * x.PrecioUnitario) descending
                    select new ProductoVendido
                    {
                        IdProducto = p.IdProducto,
                        Producto = p.Nombre,
                        Marca = p.Marca,
                        CantidadVendida = cantidad,
                        MontoAcumulado = gd.Sum(x => x.Cantidad * x.PrecioUnitario)
                    }).ToList();
        }

        //-Stock de inventario. idProducto = 0 -> catalogo completo; otro valor -> un solo producto.
        //FILA: Critico (5 o menos), Bajo (de 6 a 10) y Normal (mas de 10).
        public List<StockItem> ObtenerStock(int idProducto = 0)
        {
            return (from p in new ProductoRepositorio().ObtenerTodos()
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
