namespace COLAPP.Application.Common.Interfaces;

/// <summary>
/// Unidad de trabajo que permite controlar las operaciones de persistencia y transacción.
/// </summary>
public interface IUnitOfWork
{

    /// <summary>
    /// Inicia una transacción explícita.
    /// </summary>
    Task BeginTransactionAsync();

    /// <summary>
    /// Confirma la transacción actual.
    /// </summary>
    Task CommitAsync();

    /// <summary>
    /// Revierte la transacción actual.
    /// </summary>
    Task RollbackAsync();
    
    /// <summary>
    /// Guarda todos los cambios realizados en el contexto.
    /// </summary>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}