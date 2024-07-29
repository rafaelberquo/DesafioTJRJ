using DesafioTJRJ.Business.Interfaces.Repository;
using DesafioTJRJ.Business.Interfaces.Services;
using DesafioTJRJ.Business.Services;
using DesafioTJRJ.Business.Services.Base;
using DesafioTJRJ.Data.Repository;
using DesafioTJRJ.Data.BaseRepository.Base;
using DesafioTJRJ.Data.Context;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace DesafioTJRJ.UI.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped<IAssuntoRepository, AssuntoRepository>();
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IFormaCompraRepository, FormaCompraRepository>();
            services.AddScoped<IViewLivroAutorAssuntoRepository, ViewLivroAutorAssuntoRepository>();

            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IAutorService, AutorService>();
            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<IAssuntoService, AssuntoService>();
            services.AddScoped<IFormaCompraService, FormaCompraService>();
            services.AddScoped<IViewLivroAutorAssuntoService, ViewLivroAutorAssuntoService>();

            return services;
        }
    }
}