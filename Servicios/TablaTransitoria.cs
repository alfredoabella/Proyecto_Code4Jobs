using BBKBootCamp.Data;
using BBKBootCamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BBKBootCamp.Servicios
{
    public class TablaTransitoria
    {
        private readonly ApplicationDbContext _context;

        public TablaTransitoria(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Solicitante> GetSolicitantesDB()////////ESTO ES UNA PRUEBA/////
        {
            List<Solicitante> Solicitantes = _context.Solicitante.ToList();
            return Solicitantes;
        }
        public List<HoraDisponible> GetHoraDisponiblesDB()
        {
            List<HoraDisponible> HoraDisponibles = _context.HoraDisponible.ToList();
            return HoraDisponibles;
        }




        //public List<TablaDeEntrevista> GetTablaDeEntrevistasDB()///////ESTO ES UNA PRUEBA PARA LAS ENTREVISTAS/////
        //{
        //    List<TablaDeEntrevista> Entrevistadora = _context.TablaDeEntrevista.ToList();
        //    return Entrevistadora;
        //}
        public List<UsuarioInterno> GetUsuarioInternosDB()///////PRUEBA PARA VINCULAR USUARIOS Y ENTREVISTAS/////
        {
            List<UsuarioInterno> UsuarioInternos = _context.UsuarioInterno.ToList();
            return UsuarioInternos;
        }


    }
}
