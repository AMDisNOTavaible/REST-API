using Microsoft.EntityFrameworkCore;
using TryApi.Products;

namespace TryApi.ProductData
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet для продуктов
        public DbSet<Product> Products { get; set; }

        // DbSet для пользователей (если добавляется)
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройки для модели Product
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired() // Обязательное поле
                .HasMaxLength(100); // Максимальная длина

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)"); // Тип для цены

            // Дополнительные настройки могут быть добавлены здесь
        }
    }
}
