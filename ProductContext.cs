using Microsoft.EntityFrameworkCore;
using TryApi.Products;

namespace TryApi.ProductData
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) 
        { 
        }

        // Пример добавления DbSet для ваших сущностей
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Здесь можно настроить конфигурацию сущностей (например, ограничения и т.д.)
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired() // Пример обязательного свойства
                .HasMaxLength(100); // Пример длины строки

            // Дополнительные настройки могут быть добавлены здесь
        }
    }
}