using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data;
using ProyectoFinal.Models;
using ProyectoFinal.Models.ViewModel;

namespace ProyectoFinal.Controllers
{
    public class ArticulosController : Controller
    {

        private readonly ProyectoFinalContext _context;

        public ArticulosController(ProyectoFinalContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Articulo> listaArticulos = _context.Articulo.Include(c => c.Categoria).ToList();
            return View(listaArticulos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ArticuloVM articuloCategorias = new ArticuloVM();
            articuloCategorias.ListaCategorias = _context.Categoria.Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.IdCategoria.ToString()
            });


            return View(articuloCategorias);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                _context.Articulo.Add(articulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ArticuloVM articuloCategorias = new ArticuloVM();
            articuloCategorias.ListaCategorias = _context.Categoria.Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.IdCategoria.ToString()
            });
            return View(articuloCategorias);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ArticuloVM articuloCategorias = new ArticuloVM();
            articuloCategorias.ListaCategorias = _context.Categoria.Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.IdCategoria.ToString()
            });

            articuloCategorias.Articulo = _context.Articulo.FirstOrDefault(a => a.IdArticulo == id);
            if(articuloCategorias==null)
            {
                return NotFound();
            }
            
            return View(articuloCategorias);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                _context.Articulo.Update(articulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(articulo);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ArticuloVM articuloCategorias = new ArticuloVM();
            articuloCategorias.ListaCategorias = _context.Categoria.Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.IdCategoria.ToString()
            });
            articuloCategorias.Articulo = _context.Articulo.FirstOrDefault(a => a.IdArticulo == id);
            if (articuloCategorias == null)
            {
                return NotFound();
            }

            return View(articuloCategorias);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var articulo = await _context.Articulo.FindAsync(id);
            if (articulo == null)
            {
                return View();
            }

            //Borrado
            _context.Articulo.Remove(articulo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
