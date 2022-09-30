namespace DFT_MVC.Data
{
    using Microsoft.EntityFrameworkCore;
    using DFT_MVC.Models;

    public class DFT_MVC_Context : DbContext
    {
        public DFT_MVC_Context (DbContextOptions<DFT_MVC_Context> options)
            : base(options)
        {
        }

        public DbSet<Kategoria> Kategoria { get; set; } = default!;

        public DbSet<ImageData> ImageData { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Kategoria>()
        //        .HasOne(i => i.ImageData)
        //        .WithOne(a => a.Kategoria)
        //        .HasForeignKey<ImageData>(i => i.KategoriaId);
        //}
    }
}
