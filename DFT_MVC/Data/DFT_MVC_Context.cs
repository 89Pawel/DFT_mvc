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

        public DbSet<Category> Category { get; set; } = default!;

        public DbSet<ImageData> ImageData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasOne(i => i.ImageData)
                .WithOne(a => a.Category)
                .HasForeignKey<ImageData>(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Cascade); ;
            
        }
    }
}
