using MediatR;
using Transactions.Application.SeedWork.Exceptions;
using Transactions.Application.SeedWork.Interfaces;

namespace Transactions.Application.Commands.AccountCommands;

public class CreateAccountCommandHandler(IDatabaseContext db) : IRequestHandler<CreateAccountCommand, int>
{
    public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        await EnsureUserExist(request.UserId);
        return await CreateAccount(request.UserId);
    }

    private async Task EnsureUserExist(int userId)
    {
        const string sql = "SELECT COUNT(`id`) > 0 FROM `user` WHERE `id`=@Id";
        var exist = await db.FirstOrDefault<bool>(sql, new { Id = userId });
        if (!exist)
            throw new IdNotFoundException($"User with Id {userId} doesnt exist");
    }

    private async Task<int> CreateAccount(int userId)
    {
        const string sql = "INSERT INTO `account` (user_id, balance) VALUES (@UserId, 0); SELECT last_insert_rowid()";
        return await db.FirstOrDefault<int>(sql, new { UserId = userId });
    }
}