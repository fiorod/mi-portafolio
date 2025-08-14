using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class ResAgregarCliente
    {
        public int? IdUsuario { get; set; }
        public bool resultado { get; set; }
        public List<ApiErrorCliente>? error { get; set; }
    }

    public class ApiErrorCliente
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }


}
