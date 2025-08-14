using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class ResEliminarTrabajador
    {
        public bool resultado { get; set; }
        public List<ApiErrorTrabajador>? error { get; set; }
    }

}
