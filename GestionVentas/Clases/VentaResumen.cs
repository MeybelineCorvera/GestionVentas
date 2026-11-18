using System;

namespace GestionVentas.Clases
{
    //venta + cliente + empleado + total (para la grilla de ventas)
    public class VentaResumen
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Empleado { get; set; }
        public decimal Total { get; set; }
    }
}
