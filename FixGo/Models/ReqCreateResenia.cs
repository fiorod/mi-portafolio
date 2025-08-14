using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class ReqCreateResenia
    {
        public Resenia resenia { get; set; } = new();
    }

    public class Resenia
    {
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public int calificacion { get; set; }
        public int IdTrabajador { get; set; }
    }
}

