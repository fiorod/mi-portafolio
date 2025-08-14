using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class UpdateUserRequest
    {
        public UsuarioDto usuario { get; set; }
        public string TipoRol { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Senas { get; set; }
        public string NumeroCasa { get; set; }
        public string Empresa { get; set; }
        public int CategoriaID { get; set; }
        public int RolID { get; set; }
    }

    public class UsuarioDto
    {
        public int ID { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
        public string Email { get; set; }
        public int PersonaID { get; set; }
        public PersonaDto Persona { get; set; }
    }

    public class PersonaDto
    {
        public int IdPersona { get; set; }
        public string nombre { get; set; }
        public string nombre2 { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
    }
}
