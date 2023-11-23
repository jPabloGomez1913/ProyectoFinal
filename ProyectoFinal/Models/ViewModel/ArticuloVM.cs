using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoFinal.Models;

namespace ProyectoFinal.Models.ViewModel
{
    public class ArticuloVM
    {
        public Articulo Articulo { get; set; }
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
    }
}
