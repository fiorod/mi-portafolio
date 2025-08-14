using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class LoginResponse
    {
        public List<string>? mensaje { get; set; }
        public int UsuarioID { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int PersonaID { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string TipoRol { get; set; } = string.Empty;
        public int RolID { get; set; }
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public int CategoriaID { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;
        public bool resultado { get; set; }
        public List<ApiError>? error { get; set; }
    }

    public class ApiError
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
