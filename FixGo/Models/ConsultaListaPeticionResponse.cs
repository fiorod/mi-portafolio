using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class ConsultaListaPeticionResponse
    {
        public int IdPeticion { get; set; }
        public List<PeticionDetalle> listaPeticiones { get; set; } = new();
        public bool resultado { get; set; }
        public List<string> error { get; set; } = new();
    }

    public class PeticionDetalle
    {
        public int idPeticion { get; set; }
        public int idCliente { get; set; }
        public string fechasPosibles { get; set; } = string.Empty;
        public string horasPosibles { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public int idCategoria { get; set; }
        public int idSubcategoria { get; set; }
    }
}

