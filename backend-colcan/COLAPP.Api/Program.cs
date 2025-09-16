using COLAPP.Api;
using Microsoft.AspNetCore.Diagnostics;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
DependencyInjection.ConfigureServices(builder.Services, builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(cfg =>
{
    cfg.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        context.Response.ContentType = "application/json";

        if (exception is ValidationException validationEx)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                Title = "Se encontraron errores de validación",
                Status = 400,
                Errors = validationEx.Errors.Select(e => new
                {
                    Field = e.PropertyName,
                    Error = e.ErrorMessage
                })
            });
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new
            {
                Title = "Ocurrió un error en el servidor",
                Status = 500,
                Message = exception?.Message
            });
        }
    });
});

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
