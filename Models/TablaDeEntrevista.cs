using BBKBootCamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBKBootCamp.Models

{
    public class TablaDeEntrevista
    {
        public int Id { get; set; }
        public UsuarioInterno Entrevistadora { get; set; }
        public Solicitante Solicitante { get; set; }
        public HoraDisponible HoraDisponible { get; set; }
        public string Estado { get; set; }
    }
}
