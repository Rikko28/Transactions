using MediatR;
using Transactions.Application.SeedWork.Exceptions;
using Transactions.Application.SeedWork.Interfaces;
using Transactions.Domain.Events.TransactionEvents.cs;
using Transactions.Domain.Models.AccountModels;

namespace Transactions.Application.EventHandlers;

public class UpdateAccountBalanceWhenTransactionCreatedDomainEventHandler(IDatabaseContext db) : INotificationHandler<TransactionCreatedDomainEvent>
{
    public async Task Handle(TransactionCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var account = await GetAccount(notification.Transaction.AccountId);
        account.AcceptTransaction(notification.Transaction);
        await UpdateAccountBalance(account);
    }

    private async Task UpdateAccountBalance(Account account)
    {
        const string sql = "UPDATE `account` SET `balance`=@Balance WHERE `id`=@Id";
        await db.Execute(sql, account);
    }

    private async Task<Account> GetAccount(int accountId)
    {
        const string sql = "SELECT `id`,`user_id`,`balance` FROM `account` WHERE `id`=@Id";
        var result = await db.FirstOrDefault<Account>(sql, new {Id = accountId});
        if (result == null)
            throw new IdNotFoundException($"Cant update balance for account {accountId} because it doesn't exist");
        return result;
    }
}