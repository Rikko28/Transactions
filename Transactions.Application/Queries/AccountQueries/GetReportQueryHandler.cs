using MediatR;
using Transactions.Application.Queries.TransactionQueries;
using Transactions.Application.SeedWork.DTO;
using Transactions.Application.SeedWork.Extensions;

namespace Transactions.Application.Queries.AccountQueries;
internal class GetReportQueryHandler(IMediator mediator) : IRequestHandler<GetReportQuery, ReportReadDto?>
{
    public async Task<ReportReadDto?> Handle(GetReportQuery request, CancellationToken cancellationToken)
    {
        var transactions = await mediator.Send(new GetTransactionsQuery(request.AccountId)).ToArray();
        if (!transactions.Any())
            return null;
        return new ReportReadDto(transactions);
    }
}
