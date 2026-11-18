using RepoDb.Attributes;

namespace GestionVentas.Clases
{
    //Tabla SQL: DetalleVenta (resuelve la relacion N:M entre Ventas y Productos)
    [Map("DetalleVenta")]
    public class DetalleVenta
    {
        [Primary, Identity, Map("id_detalle")]
        public int IdDetalle { get; set; }
        [Map("id_venta")]
        public int IdVenta { get; set; }
        [Map("id_producto")]
        public int IdProducto { get; set; }
        [Map("cantidad")]
        public int Cantidad { get; set; }
        [Map("precio_unitario")]
        public decimal PrecioUnitario { get; set; }
    }
}
