using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    class CrearCitaResponse
    {
        public bool Resultado { get; set; }
        public object? IdCita { get; set; }
        public List<CitaResponseError>? Error { get; set; }
    }

    public class CitaResponseError
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
    }
}
