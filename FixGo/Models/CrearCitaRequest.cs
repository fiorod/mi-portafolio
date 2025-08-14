using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    class CrearCitaRequest
    {
        public Cita cita { get; set; }
    }
    public class Cita
    {
        public int IdCita { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan hora { get; set; }
        public int IdPeticion { get; set; }
        public int? IdTrabajador { get; set; }



    }
}
