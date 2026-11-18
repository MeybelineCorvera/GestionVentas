using System;
using RepoDb.Attributes;

namespace GestionVentas.Clases
{
    //Tabla SQL: Ventas (cabecera). El total NO se guarda: se calcula con el detalle.
    [Map("Ventas")]
    public class Venta
    {
        [Primary, Identity, Map("id_venta")]
        public int IdVenta { get; set; }
        [Map("fecha_venta")]
        public DateTime FechaVenta { get; set; }
        [Map("id_cliente")]
        public int IdCliente { get; set; }
        [Map("id_empleado")]
        public int IdEmpleado { get; set; }
    }
}
