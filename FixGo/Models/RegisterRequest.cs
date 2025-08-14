using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class RegisterRequest
    {
        public Usuario usuario { get; set; }
        public string TipoRol { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Senas { get; set; } = string.Empty;
        public string NumeroCasa { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public int CategoriaID { get; set; }
        public string NombreCategoria { get; set; }
        public RegisterRequest()
        {
            usuario = new Usuario();
        }
    }

    public class Usuario
    {
        public int ID { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Contrasenia { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int? PersonaID { get; set; }
        public Persona Persona { get; set; } = new Persona();
    }

    public class Persona
    {
        public int IdPersona { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string nombre2 { get; set; } = string.Empty;
        public string apellido1 { get; set; } = string.Empty;
        public string apellido2 { get; set; } = string.Empty;
    }
}
