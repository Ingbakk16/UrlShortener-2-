


using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;
using UrlShortener_2_.Entities;

namespace UrlShortener_2_.Data
{
    public class ShortenerDbContext : DbContext
    {
       

        public DbSet<NewUrl> NewUrls { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }


        public ShortenerDbContext(DbContextOptions<ShortenerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            

            modelBuilder.Entity<NewUrl>()
                      .HasOne(u => u.User)
                      .WithMany()
                      .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<NewUrl>()
             .HasOne(u => u.Category)
             .WithMany()
             .HasForeignKey(u => u.CategoryId);

            modelBuilder.Entity<Category>()
          .HasKey(c => c.CategoryId);



            base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
