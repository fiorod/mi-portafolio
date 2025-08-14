using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class RegisterResponse
    {
        public List<string>? mensaje { get; set; }
        public int? PersonaID { get; set; }
        public int? UsuarioID { get; set; }
        public int? RolID { get; set; }
        public bool resultado { get; set; }
        public List<ErrorResponse>? error { get; set; }
    }

    public class ErrorResponse
    {
        public int? ErrorCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
