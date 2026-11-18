using RepoDb.Attributes;

namespace GestionVentas.Clases
{
    //Tabla SQL: Productos
    [Map("Productos")]
    public class Producto
    {
        [Primary, Identity, Map("id_producto")]
        public int IdProducto { get; set; }
        [Map("nombre")]
        public string Nombre { get; set; }
        [Map("marca")]
        public string Marca { get; set; }
        [Map("descripcion")]
        public string Descripcion { get; set; }
        [Map("precio")]
        public decimal Precio { get; set; }
        [Map("stock")]
        public int Stock { get; set; }

        public Producto Copiar() { return (Producto)this.MemberwiseClone(); }
    }
}
