using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }

    public class CategoriaApiResponse
    {
        public bool exito { get; set; }
        public List<Categoria> categorias { get; set; } = new();
        public string mensaje { get; set; } = string.Empty;
    }
}
