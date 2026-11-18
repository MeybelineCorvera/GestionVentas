namespace GestionVentas.Clases
{
    //Guarda al empleado que inicio sesion mientras la aplicacion esta abierta
    public static class Sesion
    {
        public static Empleado Actual { get; set; }

        public static bool HayUsuario { get { return Actual != null; } }

        public static void Cerrar() { Actual = null; }
    }
}
