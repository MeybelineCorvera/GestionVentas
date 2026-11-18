using System;
using System.Collections.Generic;
using System.Linq;
using GestionVentas.Clases;
using RepoDb;

namespace GestionVentas.Repositorios
{
    public class VentaRepositorio : RepositorioBase<Venta>
    {
        //Registra la venta completa en UNA transaccion: cabecera + detalle + descuento de stock.
        //Si algo falla (por ejemplo stock insuficiente) se revierte todo.
        public int RegistrarVenta(Venta venta, List<LineaVenta> lineas)
        {
            using (var cn = ConexionBD.ObtenerConexion())
            {
                cn.Open();
                using (var tx = cn.BeginTransaction())
                {
                    try
                    {
                        int idVenta = Convert.ToInt32(cn.Insert<Venta>(venta, transaction: tx));

                        foreach (LineaVenta l in lineas)
                        {
                            Producto p = cn.Query<Producto>(l.IdProducto, transaction: tx).FirstOrDefault();
                            if (p == null || p.Stock < l.Cantidad)
                                throw new InvalidOperationException("Stock insuficiente de " + l.Producto + ".");

                            cn.Insert<DetalleVenta>(new DetalleVenta
                            {
                                IdVenta = idVenta,
                                IdProducto = l.IdProducto,
                                Cantidad = l.Cantidad,
                                PrecioUnitario = l.PrecioUnitario
                            }, transaction: tx);

                            p.Stock -= l.Cantidad;
                            cn.Update<Producto>(p, transaction: tx);
                        }

                        tx.Commit();
                        Notificador.Avisar();
                        return idVenta;
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        //Ventas con cliente, empleado y TOTAL calculado (suma del detalle)
        public List<VentaResumen> ObtenerResumen()
        {
            List<Cliente> clientes = new ClienteRepositorio().ObtenerTodos();
            List<Empleado> empleados = new EmpleadoRepositorio().ObtenerTodos();
            List<DetalleVenta> detalles = new DetalleVentaRepositorio().ObtenerTodos();

            return (from v in ObtenerTodos()
                    join c in clientes on v.IdCliente equals c.IdCliente
                    join e in empleados on v.IdEmpleado equals e.IdEmpleado
                    orderby v.FechaVenta descending
                    select new VentaResumen
                    {
                        IdVenta = v.IdVenta,
                        Fecha = v.FechaVenta,
                        Cliente = c.NombreCompleto,
                        Empleado = e.NombreCompleto,
                        Total = detalles.Where(d => d.IdVenta == v.IdVenta).Sum(d => d.Cantidad * d.PrecioUnitario)
                    }).ToList();
        }

        //Productos de una venta con cantidad, precio y subtotal
        public List<LineaVenta> ObtenerDetalle(int idVenta)
        {
            List<Producto> productos = new ProductoRepositorio().ObtenerTodos();

            return (from d in new DetalleVentaRepositorio().ObtenerPorVenta(idVenta)
                    join p in productos on d.IdProducto equals p.IdProducto
                    select new LineaVenta
                    {
                        IdProducto = p.IdProducto,
                        Producto = p.Nombre,
                        Marca = p.Marca,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario
                    }).ToList();
        }

        //Reporte de productos vendidos: unidades y monto acumulado por producto.
        //idProducto = 0 -> todos (solo los que tienen ventas); otro valor -> ese producto.
        public List<ProductoVendido> ProductosVendidos(int idProducto = 0)
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
    }
}
