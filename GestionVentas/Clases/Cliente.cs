using RepoDb.Attributes;

namespace GestionVentas.Clases
{
    //Tabla SQL: Clientes
    [Map("Clientes")]
    public class Cliente
    {
        [Primary, Identity, Map("id_cliente")]
        public int IdCliente { get; set; }
        [Map("nombres")]
        public string Nombres { get; set; }
        [Map("apellidos")]
        public string Apellidos { get; set; }
        [Map("dui")]
        public string Dui { get; set; }
        [Map("telefono")]
        public string Telefono { get; set; }
        [Map("correo")]
        public string Correo { get; set; }
        [Map("direccion")]
        public string Direccion { get; set; }

        public string NombreCompleto { get { return Nombres + " " + Apellidos; } }

        public Cliente Copiar() { return (Cliente)this.MemberwiseClone(); }
    }
}
