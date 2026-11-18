using System;
using RepoDb.Attributes;

namespace GestionVentas.Clases
{
    //Tabla SQL: Empleados (tambien es la tabla de usuarios del login)
    [Map("Empleados")]
    public class Empleado
    {
        [Primary, Identity, Map("id_empleado")]
        public int IdEmpleado { get; set; }
        [Map("nombres")]
        public string Nombres { get; set; }
        [Map("apellidos")]
        public string Apellidos { get; set; }
        [Map("cargo")]
        public string Cargo { get; set; }
        [Map("telefono")]
        public string Telefono { get; set; }
        [Map("correo")]
        public string Correo { get; set; }
        [Map("fecha_contratacion")]
        public DateTime FechaContratacion { get; set; }
        [Map("usuario")]
        public string Usuario { get; set; }
        [Map("clave")]
        public string Clave { get; set; }   //hash SHA-256, nunca la clave real

        public string NombreCompleto { get { return Nombres + " " + Apellidos; } }
    }
}
