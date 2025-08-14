using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class TicketDto
    {
        public int idTiquete { get; set; }
        public int idPeticion { get; set; }
        public int idCliente { get; set; }
        public string descripcion { get; set; }
        public int idTrabajador { get; set; }
        public int idCita { get; set; }
        public string fechaCita { get; set; }
        public string horaCita { get; set; }
        public string SubCategoria { get; set; }
        public string Categoria { get; set; }
        public string descripcionPeticion { get; set; }
        public string direccionCliente { get; set; }
        public string nombreEncargado { get; set; }
        public string empresaTrabjador { get; set; }
        public string estado { get; set; }
    }
}
