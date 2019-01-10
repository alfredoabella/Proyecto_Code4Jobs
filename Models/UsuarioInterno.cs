using BBKBootCamp.Servicios;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBKBootCamp.Models
{
    public class UsuarioInterno : IdentityUser
    {
        public UsuarioInterno() : base()
        {
        }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        //public List<TablaDeEntrevista> Entrevistadora { get; set; }
    }
}
