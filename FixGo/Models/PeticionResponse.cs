using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class PeticionResponse
    {
        public int? IdPeticion { get; set; }
        public bool resultado { get; set; }
        public List<ApiErrorPeticion>? error { get; set; }
    }

    public class ApiErrorPeticion
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
