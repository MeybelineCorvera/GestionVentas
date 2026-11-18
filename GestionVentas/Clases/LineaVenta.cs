namespace GestionVentas.Clases
{
    //una linea del carrito o del detalle de una venta
    public class LineaVenta
    {
        public int IdProducto { get; set; }
        public string Producto { get; set; }
        public string Marca { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal { get { return Cantidad * PrecioUnitario; } }
    }
}
