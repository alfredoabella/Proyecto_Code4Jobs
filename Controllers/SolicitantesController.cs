using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BBKBootCamp.Data;
using BBKBootCamp.Models;
using MimeKit;
using MailKit.Net.Smtp;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;

namespace BBKBootCamp.Controllers
{
    public class SolicitantesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UsuarioInterno> _userManager;///ESTO LO AÑADO PARA VINCULAR ENTREVISTADORA CON TABLAS///

        public SolicitantesController(ApplicationDbContext context, UserManager<UsuarioInterno> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Solicitantes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Solicitante.ToListAsync());
        }
        public async Task<IActionResult> GraficoEstadistica()
        {
            return View(await _context.Solicitante.ToListAsync());
        }

        // GET: Solicitantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitante = await _context.Solicitante
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solicitante == null)
            {
                return NotFound();
            }

            return View(solicitante);
        }

        // GET: Solicitantes/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult SendMailAdministradora()
        {
            //UsuarioInterno interno = await _userManager.GetUserAsync(User);  //////DESCOMENTAR SI LO OTRO NO FUNCIONA/////

            //UsuarioInterno interno = await _userManager.Users.Include(u => u.Roles).FirstOrDefault(u => u.Email == "test@test.de");

            //var user = userManager.Users.Include(u => u.Roles).FirstOrDefault(u => u.Email == "test@test.de");


            /////////////CORECCION PARA ENVIO DE CORREO SIN ESTAR LOGGEADO////////////
            UsuarioInterno interno = _context.Users.Single(x => x.Email == "nicolas_m_s@hotmail.es");
            /////////////CORECCION PARA ENVIO DE CORREO SIN ESTAR LOGGEADO////////////

            string correo = interno.Email;
            System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.live.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("nicolas-m-s@hotmail.com");//correo de noticador

            if (interno.Email != null)
            {
                mail.To.Add(interno.Email);//a quien lo envio

                mail.Subject = "BBK BOOTCAMP";
                mail.IsBodyHtml = true;
                string htmlBody;
                htmlBody = "!!Se ha inscrito un nuevo Solicitante/a!!. Ingresa a tu area personal para ver los datos ";
                mail.Body = htmlBody;
                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("nicolas-m-s@hotmail.com", "chul1t0-");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            return View();
        }

        public IActionResult SendMail([Bind("Id,CorreoElectronico,Nombre,Apellido,FechaNacimiento,SexoTipo,Pregunta1,Pregunta2,Pregunta3,Pregunta4,Pregunta5,Pregunta6,Proceso")] Solicitante solicitante)
        {
            //////ES UNA PRUEBA PARA EL ENVIO AUTOMATICO DE CORREO//////
            string correo = solicitante.CorreoElectronico;
            System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.live.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("nicolas-m-s@hotmail.com");//correo de noticador
            mail.To.Add(solicitante.CorreoElectronico);//a quien lo envio
            mail.Subject = "BBK BOOTCAMP";
            mail.IsBodyHtml = true;
            string htmlBody;
            htmlBody = "!!Gracias por inscribirte "+ solicitante.Nombre +"!! \n En breve nos pondremos en contacto contigo para concertar un entrevista";
            mail.Body = htmlBody;
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("nicolas-m-s@hotmail.com", "chul1t0-");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            ////////ES UNA PRUEBA PARA EL ENVIO AUTOMATICO DE CORREO//////
            //return View();  //DESCOMENTAR SI GENERA ERROR
            return RedirectToAction("SendMailAdministradora", solicitante);
        }
        // POST: Solicitantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CorreoElectronico,Nombre,Apellido,FechaNacimiento,SexoTipo,Pregunta1,Pregunta2,Pregunta3,Pregunta4,Pregunta5,Pregunta6,Proceso")] Solicitante solicitante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(solicitante);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(VistaNotificacion));
                //return RedirectToAction(nameof(SendMail));   //DESCOMENTAR SI GENERA ERROR

            }
            //return View(SendMail(solicitante));
            return RedirectToAction("SendMail",solicitante);
        }
        //////////ESTO ES PARA QUE NO VUELVA AL INDEX POR DEFECTO//////

        //////////ESTO ES PARA QUE NO VUELVA AL INDEX POR DEFECTO//////

        // GET: Solicitantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitante = await _context.Solicitante.FindAsync(id);
            if (solicitante == null)
            {
                return NotFound();
            }
            return View(solicitante);
        }

        // POST: Solicitantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CorreoElectronico,Nombre,Apellido,FechaNacimiento,SexoTipo,Pregunta1,Pregunta2,Pregunta3,Pregunta4,Pregunta5,Pregunta6,Proceso")] Solicitante solicitante)
        {
            if (id != solicitante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitanteExists(solicitante.Id))
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
            return View(solicitante);
        }

        // GET: Solicitantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitante = await _context.Solicitante
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solicitante == null)
            {
                return NotFound();
            }

            return View(solicitante);
        }

        // POST: Solicitantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solicitante = await _context.Solicitante.FindAsync(id);
            _context.Solicitante.Remove(solicitante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitanteExists(int id)
        {
            return _context.Solicitante.Any(e => e.Id == id);
        }
    }
}
