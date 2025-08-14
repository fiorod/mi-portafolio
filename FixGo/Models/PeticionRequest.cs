using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class PeticionRequest
    {
        public PeticionData peticion { get; set; } = new PeticionData();
    }

    public class PeticionData
    {
        public int idCliente { get; set; }
        public string fechasPosibles { get; set; } = string.Empty;
        public string horasPosibles { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public int idCategoria { get; set; }
        public int idSubcategoria { get; set; }
    }
}
