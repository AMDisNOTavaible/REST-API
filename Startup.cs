using Microsoft.EntityFrameworkCore;
using TryApi.Controllers;
using TryApi.ProductData;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // Метод, используемый для добавления служб в контейнер
    public void ConfigureServices(IServiceCollection services)
    {
        // Настройка контекста базы данных
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        // Добавление MVC / API
        services.AddControllers();

        // Настройки для других служб могут быть добавлены здесь
    }

    // Метод, используемый для настройки HTTP-запросов
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        // Настройка конечных точек для запросов
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Для API
            // или endpoints.MapDefaultControllerRoute(); // Для MVC
        });
    }
}
