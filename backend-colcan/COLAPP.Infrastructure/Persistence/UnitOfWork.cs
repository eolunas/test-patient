//using COLAPP.Application.Common.Interfaces;
//using COLAPP.Shared.DependencyInjection;

//namespace COLAPP.Infrastructure.Persistence;

///// <summary>
///// Implementación de la unidad de trabajo que gestiona transacciones y persistencia.
///// </summary>
//[Scoped]
//public class UnitOfWork(DapperContext context) : IUnitOfWork
//{
//    private readonly DapperContext _context = context;
//    private IDbContextTransaction? _transaction;


//    public async Task BeginTransactionAsync()
//    {
//        if (_transaction is not null)
//            return; // ya hay una transacción activa

//        _transaction = await _context.Database.BeginTransactionAsync();
//    }

//    public async Task CommitAsync()
//    {
//        if (_transaction is null)
//            throw new InvalidOperationException("No hay transacción activa para confirmar.");

//        await _transaction.CommitAsync();
//        await _transaction.DisposeAsync();
//        _transaction = null;
//    }

//    public async Task RollbackAsync()
//    {
//        if (_transaction is null)
//            return;

//        await _transaction.RollbackAsync();
//        await _transaction.DisposeAsync();
//        _transaction = null;
//    }
//    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
//        await _context.SaveChangesAsync(cancellationToken);
//}
