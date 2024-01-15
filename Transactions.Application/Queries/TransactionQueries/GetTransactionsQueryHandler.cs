using MediatR;
using Transactions.Application.SeedWork.DTO;
using Transactions.Application.SeedWork.Interfaces;

namespace Transactions.Application.Queries.TransactionQueries;

public class GetTransactionsQueryHandler(IDatabaseContext db) : IRequestHandler<GetTransactionsQuery, IEnumerable<TransactionReadDto>>
{
    private readonly IDatabaseContext _db = db;

    public async Task<IEnumerable<TransactionReadDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT `id`,`transaction_type_id` as `TransactionTypeId`,`amount`,`date`,`transfer_account_id` as `TransferAccountId`
            FROM `transaction`
            WHERE `account_id`=@Id
        ";
        return await _db.GetMany<TransactionReadDto>(sql, new { Id = request.AccountId });
    }
}
