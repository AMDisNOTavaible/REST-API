using Microsoft.EntityFrameworkCore;
using TryApi.ProductData;

var builder = WebApplication.CreateBuilder(args);

// Подключение к базе данных SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=products.db"));

// Добавление необходимых сервисов
builder.Services.AddControllers();

var app = builder.Build();

// Инициализация базы данных
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated(); // Создает базу данных при необходимости
}

// Настройка маршрутизации
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();