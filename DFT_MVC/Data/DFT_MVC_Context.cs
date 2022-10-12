namespace DFT_MVC.Data
{
    using Microsoft.EntityFrameworkCore;
    using DFT_MVC.Models;

    public class DFT_MVC_Context : DbContext
    {
        public DFT_MVC_Context(DbContextOptions<DFT_MVC_Context> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<ImageData> ImageDatas { get; set; } = default!;
        public DbSet<Subcategory> Subcategories { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(eb =>
            {
                eb.HasMany(i => i.Subcategories)
                .WithOne(i => i.Category)
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

                //eb.HasOne(i => i.ImageData)
                //.WithOne(a => a.Category)
                //.IsRequired(false)
                //.HasForeignKey<ImageData>(i => i.CategoryId)
                //.OnDelete(DeleteBehavior.NoAction);

            });

            modelBuilder.Entity<Subcategory>(eb =>
            {
                //eb.HasOne(i => i.Category)
                //.WithMany(i => i.Subcategories)
                //.IsRequired()
                //.HasForeignKey(i => i.CategoryId)
                //.OnDelete(DeleteBehavior.Cascade);

                //eb.HasOne(i => i.ImageData)
                //.WithOne(i => i.Subcategory)
                //.IsRequired(false)
                //.HasForeignKey<ImageData>(i => i.SubcategoryId)
                //.OnDelete(DeleteBehavior.NoAction);
                
            });
        }
    }
}
