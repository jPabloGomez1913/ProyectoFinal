using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data;
using ProyectoFinal.Models;
using ProyectoFinal.Models.ViewModel;

namespace ProyectoFinal.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ProyectoFinalContext _context;

        public UsuariosController(ProyectoFinalContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
             return _context.Usuario != null ? 
                         View( await _context.Usuario.ToListAsync()) :
                         Problem("Entity set 'ProyectoFinalContext.Usuario'  is null."); 
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViewUsuario usuario)
        {
            if (UsuarioExists(usuario.Documento))
            {
                ViewBag.Mensaje = "El documento ya está en uso";
                return View();
            }
            if (CorreoExists(usuario.Email))
            {
                ViewBag.Mensaje = "El correo ya está en uso";
                return View();
            }

            if (!usuario.Password.Equals(usuario.PasswordConfir)) 
            {
                ViewBag.Mensaje = "Las contraseñas no coinciden";
                return View();
            }


            
            if (ModelState.IsValid)
            {
               var usuarioBd =new Usuario {
                Documento= usuario.Documento,
                Password = usuario.Password,
                FechaRegistro= DateTime.Now,
                Email= usuario.Email,
                Nombre=usuario.Nombre,
                Direccion=usuario.Direccion,
                
                };

                _context.Add(usuarioBd);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuarioId,Documento,Nombre,Email,Direccion,Password,FechaRegistro")] Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.UsuarioId))
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
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuario == null)
            {
                return Problem("Entity set 'ProyectoFinalContext.Usuario'  is null.");
            }
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuario.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(string doc)
        {
          return (_context.Usuario?.Any(e => e.Documento.Equals(doc))).GetValueOrDefault();
        }

        private bool UsuarioExists(int id)
        {
            return (_context.Usuario?.Any(e => e.UsuarioId==id)).GetValueOrDefault();
        }

        private bool CorreoExists(string correo)
        {
            return (_context.Usuario?.Any(e => e.Email.Equals(correo))).GetValueOrDefault();
        }




    }
}
