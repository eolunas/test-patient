using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using COLAPP.Application.Common.Behaviors;

namespace COLAPP.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Método para agregar todas las dependencias de capa aplicación.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Registra MediatR para manejar comandos y eventos, a partir del ensamblado actual.
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        // Registrar validadores de fluentvalidation.
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        // Registrar pipe de validaciones automaticas.
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        // Registrar pipe de transaccionalidad.
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionalBehavior<,>));

        return services;
    }

}
