using MediatR;
using Microsoft.Extensions.Logging;
using COLAPP.Application.Common.Interfaces;

namespace COLAPP.Application.Common.Behaviors;

/// <summary>
/// Comportamiento de MediatR que maneja automáticamente las transacciones.
/// Solo se aplica a comandos que implementan la interfaz ITransactionalRequest.
/// </summary>
/// <typeparam name="TRequest">Tipo de solicitud</typeparam>
/// <typeparam name="TResponse">Tipo de respuesta</typeparam>
public class TransactionalBehavior<TRequest, TResponse>(
    IUnitOfWork unitOfWork,
    ILogger<TransactionalBehavior<TRequest, TResponse>> logger
) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<TransactionalBehavior<TRequest, TResponse>> _logger = logger;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Solo aplica si el request implementa ITransactionalRequest
        if (request is not ITransactionalRequest)
            return await next();

        _logger.LogDebug("Iniciando transacción para {Request}", typeof(TRequest).Name);

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var response = await next(); // Ejecuta el handler

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync();

            _logger.LogDebug("Transacción confirmada para {Request}", typeof(TRequest).Name);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error en la transacción de {Request}", typeof(TRequest).Name);
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}