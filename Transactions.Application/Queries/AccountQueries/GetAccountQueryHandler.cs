using MediatR;
using Transactions.Application.SeedWork.DTO;
using Transactions.Application.SeedWork.Interfaces;

namespace Transactions.Application.Queries.AccountQueries;
public class GetAccountQueryHandler(IDatabaseContext db) : IRequestHandler<GetAccountQuery, AccountReadDto?>
{
    public async Task<AccountReadDto?> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        const string sql = @"
                    SELECT `account`.`id`,`account`.`user_id`,`user`.`name` as `UserName`,`account`.`balance`
                    FROM `account`
                    JOIN `user` ON `user`.`id`=`account`.`user_id`
                    WHERE `account`.`id`=@Id
        ";
        return await db.FirstOrDefault<AccountReadDto>(sql, new { request.Id });
    }
}
