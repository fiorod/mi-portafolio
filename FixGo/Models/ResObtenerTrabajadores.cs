using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class ResObtenerTrabajadores
    {
        public bool resultado { get; set; }
        public List<ApiErrorTrabajador>? error { get; set; }
        public List<Trabajador>? listaTrabajadores { get; set; }

        public ResObtenerTrabajadores()
        {
            listaTrabajadores = new List<Trabajador>();
        }
    }

    public class Trabajador
    {
        public int IdTrabajador { get; set; }
        public string NombreCompleto { get; set; }
        public string Empresa { get; set; }
        public string Telefono { get; set; }
        public string Categoria { get; set; }
    }

}
