using MediatR;
using Transactions.Application.SeedWork.Interfaces;

namespace Transactions.Application.SeedWork.Behaviors;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IDatabaseContext _databaseContext;

    public TransactionBehavior(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            if (_databaseContext.HasActiveTransaction)
                return await next();

            _databaseContext.StartTransaction();
            var response = await next();
            _databaseContext.CommitTransaction();
            return response;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
