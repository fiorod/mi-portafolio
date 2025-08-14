using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class PeticionGeneralRequest
    {
        public int? IdPeticion { get; set; }
        public int? IdCliente { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdSubCategoria { get; set; }
    }
}
