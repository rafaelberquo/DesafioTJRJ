using DesafioTJRJ.Data.Context;
using DesafioTJRJ.UI.AutoMapper;
using DesafioTJRJ.UI.Configurations;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace DesafioTJRJ.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configura o contexto do EF Core
            builder.Services.AddDbContext<LibraryContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).LogTo(Console.WriteLine, LogLevel.Information));

            // Configura a injeção de dependência definida em DependencyInjectionConfig
            builder.Services.ResolveDependencies();

            // Mapeamentos do AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                        var logger = app.Services.GetRequiredService<ILogger<Program>>();
                        if (exceptionHandlerPathFeature?.Error != null)
                        {
                            logger.LogError(exceptionHandlerPathFeature.Error, "Exceção não tratada");
                        }
                        context.Response.Redirect("/Home/Error/500");
                    });
                });

                app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Configura a cultura padrão para pt-BR
            app.UseGlobalizationConfig();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}