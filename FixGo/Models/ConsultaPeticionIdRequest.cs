using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    internal class ConsultaPeticionIdRequest
    {
        public int IdPeticion { get; set; }
        public int IdCliente { get; set; }
        public int idCategoria { get; set; }
        public int IdSubCategoria { get; set; }
    }
}
