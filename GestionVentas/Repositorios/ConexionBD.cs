using System.Configuration;
using Microsoft.Data.SqlClient;

namespace GestionVentas.Repositorios
{
    public static class ConexionBD
    {
        private static readonly string cadena =
            ConfigurationManager.ConnectionStrings["conexionBD"].ConnectionString;

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadena);
        }
    }
}
