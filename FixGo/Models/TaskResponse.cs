using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    internal class TaskResponse
    {
        public string Servicio { get; set; }
        public string Subcategoria { get; set; }
        public string Dia { get; set; }
        public string Hora { get; set; }
        public string Encargado { get; set; }
        public string Empresa { get; set; }
    }
}
