using System;
using System.Collections.Generic;
using System.Text;
using BBKBootCamp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BBKBootCamp.Servicios;

namespace BBKBootCamp.Data
{
    public class ApplicationDbContext : IdentityDbContext<UsuarioInterno, AppRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BBKBootCamp.Models.Solicitante> Solicitante { get; set; }
        public DbSet<BBKBootCamp.Models.HoraDisponible> HoraDisponible { get; set; }
        public DbSet<BBKBootCamp.Models.TablaDeEntrevista> TablaDeEntrevista { get; set; }     

        public DbSet<UsuarioInterno> UsuarioInterno { get; set; }//ESTA ES UNA PRUEBA QUE HAGO PARA VINCULAR USUARIO Y ENTREVISTA//
    }
}
