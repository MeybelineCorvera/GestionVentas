namespace GestionVentas.Clases
{
    //fila del reporte de existencias (stock)
    public class StockItem
    {
        public int IdProducto { get; set; }
        public string Producto { get; set; }
        public string Marca { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Estado { get; set; }   //Critico (<= 5), Bajo (6 a 10) o Normal
    }
}
