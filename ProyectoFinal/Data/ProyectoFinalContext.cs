using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;

namespace ProyectoFinal.Data
{
    public class ProyectoFinalContext : IdentityDbContext
    {
        public ProyectoFinalContext (DbContextOptions<ProyectoFinalContext> options)
            : base(options)
        {
        }

        public DbSet<ProyectoFinal.Models.Usuario> Usuario { get; set; } = default!;
        public DbSet<ProyectoFinal.Models.Categoria> Categoria { get; set; } = default!;
        public DbSet<ProyectoFinal.Models.Articulo> Articulo { get; set; } = default!;
    }
}
