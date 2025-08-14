using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class AssignWorkerResponse
    {
        public int idServicio { get; set; }
        public string Servicio { get; set; }
        public int idSubcategoria { get; set; }
        public string Subcategoria { get; set; }
        public string Cliente { get; set; }
        public string Descripcion { get; set; }
        public string LunesHoras { get; set; }
        public string MartesHoras { get; set; }
        public string ViernesHoras { get; set; }
        public int idPeticion { get; set; }
        public int idCliente { get; set; }
    }
}
