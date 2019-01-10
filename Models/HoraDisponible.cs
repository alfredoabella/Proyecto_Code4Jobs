using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBKBootCamp.Models
{
    public class HoraDisponible
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public string Estado { get; set; }
    }
}
