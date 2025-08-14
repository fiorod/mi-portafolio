using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class ReqAgregarCliente
    {
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string NumeroCasa { get; set; }
    }

}
