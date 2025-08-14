using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class Subcategoria
    {
        public int IdSubCategoria { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int IdCategoria { get; set; }
        public string? NombreCategoria { get; set; }
    }

    public class SubcategoriaResponse
    {
        public List<Subcategoria> listaSubCategorias { get; set; } = new();
        public bool resultado { get; set; }
        public List<ApiError>? error { get; set; }
    }
}
