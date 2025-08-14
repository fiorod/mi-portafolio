using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class FeedbackResponse
    {
        public int IdResenia { get; set; }
        public string titulo { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public double calificacion { get; set; }
        public int IdTrabajador { get; set; }
        public string nombreTrabajador { get; set; } = string.Empty;
    }
}
