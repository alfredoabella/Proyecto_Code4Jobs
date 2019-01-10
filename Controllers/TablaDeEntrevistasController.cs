using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BBKBootCamp.Data;
using BBKBootCamp.Servicios;
using Microsoft.AspNetCore.Identity;
using BBKBootCamp.Models;
using MimeKit;
using MailKit.Net.Smtp;
using System.Net.Mail;

namespace BBKBootCamp.Controllers
{
    public class TablaDeEntrevistasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UsuarioInterno> _userManager;///ESTO LO AÑADO PARA VINCULAR ENTREVISTADORA CON TABLAS///

        public TablaDeEntrevistasController(ApplicationDbContext context, UserManager<UsuarioInterno> userManager)//AÑADIDO//
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TablaDeEntrevistas
        public async Task<IActionResult> Index()
        {
            //List<Solicitante> SolicitanteList = new List<Solicitante>();
            //List<HoraDisponible> HoraDisponibleList = new List<HoraDisponible>();
            //List<UsuarioInterno> UsuarioInternoList = new List<UsuarioInterno>();  ///////PRUEBA PARA VINCULAR USUARIOS Y ENTREVISTAS/////

            UsuarioInterno interno = await _userManager.GetUserAsync(User);
            /////////ESTO LO AÑADO PARA CREAR ENTREVISTAS/////
            //List<UsuarioInterno> UsuarioInternos = interno.UsuarioInternos;
            if (User.Identity.Name == "carla@hotmail.com")
            {
                return View(await _context.TablaDeEntrevista.Include(x => x.Solicitante).Include(x => x.HoraDisponible).Include(x => x.Entrevistadora).ToListAsync());
            }
            else
            {
                return View(await _context.TablaDeEntrevista.Include(x => x.Solicitante).Include(x => x.HoraDisponible).Include(x => x.Entrevistadora).Where(x => x.Entrevistadora.UserName == User.Identity.Name).ToListAsync());
            }

            
            //return View(await _context.TablaDeEntrevista.ToListAsync());
        }

        // GET: TablaDeEntrevistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tablaDeEntrevista = await _context.TablaDeEntrevista.Include(x=>x.Solicitante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tablaDeEntrevista == null)
            {
                return NotFound();
            }

            return View(tablaDeEntrevista);
        }

        // GET: TablaDeEntrevistas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TablaDeEntrevistas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Solicitante,HoraDisponible,Estado,Entrevistadora")] TablaDeEntrevista tablaDeEntrevista, int idSolicitante, int idHoraDisponible, string idEstado, string idEntrevistadora)
        {
     
            Solicitante solicitante = _context.Solicitante.Single(x => x.Id == idSolicitante);
            HoraDisponible hora = _context.HoraDisponible.Single(x => x.Id == idHoraDisponible);
            UsuarioInterno usuario = _context.UsuarioInterno.Single(x => x.Id == idEntrevistadora);

            tablaDeEntrevista.Solicitante = solicitante;
            tablaDeEntrevista.Entrevistadora = usuario;
            tablaDeEntrevista.HoraDisponible = hora;               

            if (ModelState.IsValid)
            {
                ///// Esto modifica el estado de la hora a no disponible//////////
                HoraDisponible horaOcupada =_context.HoraDisponible.Where(x => x.Id == hora.Id).First();
                horaOcupada.Estado = "Ocupada";

                Solicitante concertado = _context.Solicitante.Where(x => x.Id == solicitante.Id).First();
                concertado.Proceso = "Entrevista Concertada";
                ///// Esto modifica el estado del candidato a entrevista concertada/////////

                _context.Add(tablaDeEntrevista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tablaDeEntrevista);


        }
     
        // GET: TablaDeEntrevistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tablaDeEntrevista = await _context.TablaDeEntrevista.FindAsync(id);
            if (tablaDeEntrevista == null)
            {
                return NotFound();
            }
            return View(tablaDeEntrevista);
        }

        // POST: TablaDeEntrevistas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HoraDisponible,Estado,Entrevistadora")] TablaDeEntrevista tablaDeEntrevista,int idSolicitante, int idHoraDisponible, string idEstado, string idEntrevistadora)
        {
            Solicitante solicitante = _context.Solicitante.Single(x => x.Id == idSolicitante);
            HoraDisponible hora = _context.HoraDisponible.Single(x => x.Id == idHoraDisponible);
            UsuarioInterno usuario = _context.UsuarioInterno.Single(x => x.Id == idEntrevistadora);

            tablaDeEntrevista.Entrevistadora = usuario;
            tablaDeEntrevista.HoraDisponible = hora;

            if (id != tablaDeEntrevista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tablaDeEntrevista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TablaDeEntrevistaExists(tablaDeEntrevista.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tablaDeEntrevista);
        }

        // GET: TablaDeEntrevistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tablaDeEntrevista = await _context.TablaDeEntrevista
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tablaDeEntrevista == null)
            {
                return NotFound();
            }

            return View(tablaDeEntrevista);
        }

        // POST: TablaDeEntrevistas/Delete/5
        //[HttpPost]

        [HttpPost, ActionName("Delete")]   ////DESCOMENTAR SI NO RESULTA//////
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)  ////DESCOMENTAR SI NO RESULTA//////
     
        {
            ///////////ESTO ES PARA QUE AL ELIMINAR UNA ENTREVISTA SE RESTAUREN A "En Espera"///////
            ///////////Y "Disponible"Y ESTEN NUEVAMENTE DISPONIBLE EN LOS DROPDOWN//////////
            var tablaDeEntrevista = await _context.TablaDeEntrevista.Include(x => x.Solicitante).Include(x => x.HoraDisponible).FirstOrDefaultAsync(x => x.Id == id);
            
            if (ModelState.IsValid)
            {
                tablaDeEntrevista.Solicitante.Proceso = "En Espera";
                tablaDeEntrevista.HoraDisponible.Estado = "Disponible";
            }
            ///////////ESTO ES PARA QUE AL ELIMINAR UNA ENTREVISTA SE RESTAUREN A "En Espera"///////
            ///////////Y "Disponible"Y ESTEN NUEVAMENTE DISPONIBLE EN LOS DROPDOWN//////////

            _context.TablaDeEntrevista.Remove(tablaDeEntrevista);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
        private bool TablaDeEntrevistaExists(int id)
        {        
            return _context.TablaDeEntrevista.Any(e => e.Id == id);
        }
    }
}
