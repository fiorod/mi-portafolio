using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class LoginRequest
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string Contrasenia { get; set; } = string.Empty;
    }
}
