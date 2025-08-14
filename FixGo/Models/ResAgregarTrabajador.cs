using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class ResAgregarTrabajador
    {
        public int? IdUsuario { get; set; }
        public bool resultado { get; set; }
        public List<ApiErrorTrabajador>? error { get; set; }
    }

    public class ApiErrorTrabajador
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }

}
