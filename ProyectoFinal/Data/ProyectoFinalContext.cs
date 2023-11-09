using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;

namespace ProyectoFinal.Data
{
    public class ProyectoFinalContext : DbContext
    {
        public ProyectoFinalContext (DbContextOptions<ProyectoFinalContext> options)
            : base(options)
        {
        }

        public DbSet<ProyectoFinal.Models.Usuario> Usuario { get; set; } = default!;
    }
}
