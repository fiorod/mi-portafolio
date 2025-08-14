using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class ResEliminarCliente
    {
        public bool resultado { get; set; }
        public List<ApiErrorCliente>? error { get; set; }
    }

}
