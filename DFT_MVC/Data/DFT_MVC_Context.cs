using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DFT_MVC.Models;

namespace DFT_MVC.Data
{
    public class DFT_MVC_Context : DbContext
    {
        public DFT_MVC_Context (DbContextOptions<DFT_MVC_Context> options)
            : base(options)
        {
        }

        public DbSet<DFT_MVC.Models.Kategorie> Kategorie { get; set; } = default!;
    }
}
