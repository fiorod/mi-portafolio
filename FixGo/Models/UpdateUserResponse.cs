using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixGo.Models
{
    public class UpdateUserResponse
    {
        public List<string> mensaje { get; set; }
        public bool resultado { get; set; }
        public object error { get; set; }
    }
}
