using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data;
using ProyectoFinal.Models;
using ProyectoFinal.Models.ViewModel;
using System.Linq;

namespace ProyectoFinal.Controllers
{
    public class ArticulosController : Controller
    {

        private readonly ProyectoFinalContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ArticulosController(ProyectoFinalContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Articulo> listaArticulos = _context.Articulo.Include(c => c.Categoria).ToList();
            return View(listaArticulos);
        }

        [HttpGet]
        public IActionResult Componentes()
        {
            var articulo = _context.Articulo.Where(a => !a.Categoria.Nombre.Contains("computadores")).ToList();
            return View(articulo);
        }

        [HttpGet]
        public IActionResult Computadores()
        {
            var articulo = _context.Articulo.Where(a => a.Categoria.Nombre.Contains("computadores")).ToList();
            return View(articulo);
        }

        [HttpGet]
        public IActionResult Detalle(int? id)
        {
            var art = _context.Articulo.Find(id);
            return View(art);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ArticuloVM artivm = new ArticuloVM();
            artivm.ListaCategorias = _context.Categoria.Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.IdCategoria.ToString()
            });


            return View(artivm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArticuloVM artiVM)
        {
            /* if (ModelState.IsValid)
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
            */

            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if (artiVM.Articulo.IdArticulo == 0)
                {
                    //Nuevo artículo
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    artiVM.Articulo.UrlImagen = @"\imagenes\articulos\" + nombreArchivo + extension;

                    _context.Articulo.Add(artiVM.Articulo);
                    await _context.SaveChangesAsync();
                    TempData["Correcto"] = "Articulo creado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
            }


            artiVM.ListaCategorias = _context.Categoria.Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.IdCategoria.ToString()
            });
            return View(artiVM);
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ArticuloVM artiVM = new ArticuloVM();
            artiVM.ListaCategorias = _context.Categoria.Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.IdCategoria.ToString()
            });

            artiVM.Articulo = _context.Articulo.FirstOrDefault(a => a.IdArticulo == id);
            if (artiVM == null)
            {
                return NotFound();
            }

            return View(artiVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ArticuloVM artiVM)
        {
            if (ModelState.IsValid)
            {
                _context.Articulo.Update(artiVM.Articulo);
                await _context.SaveChangesAsync();
                TempData["Correcto"] = "Articulo editado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            return View(artiVM);
        }
            /*
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var articuloDesdeDb = _context.Articulo.Find(artiVM.Articulo.IdArticulo);

                if (archivos.Count() > 0)
                {
                    //Nueva imagen para el artículo
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, articuloDesdeDb.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    //Nuevamente subimos el archivo
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    artiVM.Articulo.UrlImagen = @"\imagenes\articulos\" + nombreArchivo + extension;


                    _context.Articulo.Update(artiVM.Articulo);
                   await _context.SaveChangesAsync();
                   
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //Aquí sería cuando la imagen ya existe y se conserva
                    artiVM.Articulo.UrlImagen = articuloDesdeDb.UrlImagen;
                }

                _context.Articulo.Update(artiVM.Articulo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(artiVM);

        }
            */

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            /* if (id == null)
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
         } */

            if (id == null)
            {
                return NotFound();
            }

            ArticuloVM artiVM = new ArticuloVM();
            artiVM.ListaCategorias = _context.Categoria.Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.IdCategoria.ToString()
            });

            artiVM.Articulo = _context.Articulo.FirstOrDefault(a => a.IdArticulo == id);
            if (artiVM == null)
            {
                return NotFound();
            }

            return View(artiVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            /*   var articulo = await _context.Articulo.FindAsync(id);
               if (articulo == null)
               {
                   return View();
               }

               //Borrado
               _context.Articulo.Remove(articulo);
               await _context.SaveChangesAsync();
               return RedirectToAction(nameof(Index));
           }
           */

            var articuloDesdeDb = _context.Articulo.Find(id);
           /* string rutaDirectorioPrincipal = _hostingEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, articuloDesdeDb.UrlImagen.TrimStart('\\'));

            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }
           */
            if (articuloDesdeDb == null)
            {
                TempData["Error"] = "Error borrando";
            }

            _context.Articulo.Remove(articuloDesdeDb);
           await  _context.SaveChangesAsync();
            TempData["Correcto"] = "Borrado exitosamente";
            return RedirectToAction(nameof(Index));
        }
        
    }
}
