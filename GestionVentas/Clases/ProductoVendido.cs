namespace GestionVentas.Clases
{
    //fila del reporte de productos vendidos
    public class ProductoVendido
    {
        public int IdProducto { get; set; }
        public string Producto { get; set; }
        public string Marca { get; set; }
        public int CantidadVendida { get; set; }
        public decimal MontoAcumulado { get; set; }
    }
}
