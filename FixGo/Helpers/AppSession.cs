using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Helpers
{
    public static class AppSession
    {
        public static int UsuarioID { get; set; }
        public static int PersonaID { get; set; }
        public static string NombreUsuario { get; set; } = string.Empty;
        public static string NombreCompleto { get; set; } = string.Empty;
        public static string TipoRol { get; set; } = string.Empty;
        public static int? RolID { get; set; }

        // Otros campos que quieras guardar globalmente:
        public static string Email { get; set; } = string.Empty;
        public static string Telefono { get; set; } = string.Empty;
        public static string Direccion { get; set; } = string.Empty;
        public static string Empresa { get; set; } = string.Empty;
        //public static int CategoriaID { get; set; }
        //public static string NombreCategoria { get; set; } = string.Empty;

        public static void LimpiarSesion()
        {
            RolID = null;
            NombreUsuario = null;
            NombreCompleto = null;
            Direccion = null;
            Email = null;
            Empresa = null;
            Telefono = null;
        }
    }
 }
