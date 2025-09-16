using Microsoft.AspNetCore.Mvc;
using COLAPP.Application;
using COLAPP.Shared.DependencyInjection;

namespace COLAPP.Api;

public static class DependencyInjection
{
    public static void ConfigureServices(IServiceCollection services, IConfigurationManager configuration)
    {
        // Piliticas de CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());
        });

        // Agrega los controladores de la aplicación, que son responsables de manejar las solicitudes HTTP.
        services.AddControllers();

        // Configura la inyección de dependencias para agregar la configuración de MVC.
        services.AddMvc(config =>
        {
            // Establece que todas las respuestas de los controladores sean en formato JSON por defecto.
            config.Filters.Add(new ProducesAttribute("application/json")); 
        });

        // Configuracion de swgger:
        services.AddSwaggerGen();

        // Agregar servicios dedicados de aplicación:
        services.AddApplication();

        // Agregar servicios y repositorios.
        services.ScanAndAddServices();
    }

    /// <summary>
    /// Buscar y agrega servicios a la colección de servicios utilizando Scrutor.
    /// </summary>
    /// <returns></returns>
    public static IServiceCollection ScanAndAddServices(this IServiceCollection services) =>
        services.Scan(scan => scan
        .FromAssemblies(
            typeof(Application.DependencyInjection).Assembly,
            typeof(Infrastructure.DependencyInjection).Assembly
        )

        // Scoped
        .AddClasses(c => c.WithAttribute<ScopedAttribute>())
            .AsImplementedInterfaces().AsSelf()
            .WithScopedLifetime()

        // Singleton
        .AddClasses(c => c.WithAttribute<SingletonAttribute>())
            .AsImplementedInterfaces().AsSelf()
            .WithSingletonLifetime()

        // Transient
        .AddClasses(c => c.WithAttribute<TransientAttribute>())
            .AsImplementedInterfaces().AsSelf()
            .WithTransientLifetime()
        );
}
