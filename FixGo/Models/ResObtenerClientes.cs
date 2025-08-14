using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class ResObtenerClientes
    {
        public bool resultado { get; set; }
        public List<ApiErrorCliente>? error { get; set; }
        public List<Cliente>? listaClientes { get; set; }

        public ResObtenerClientes()
        {
            listaClientes = new List<Cliente>();
        }
    }

    public class Cliente
    {
        public int IdCliente { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string NumeroCasa { get; set; }
    }

}
