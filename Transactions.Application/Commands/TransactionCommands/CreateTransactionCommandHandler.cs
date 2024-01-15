using MediatR;
using Transactions.Application.SeedWork.Exceptions;
using Transactions.Application.SeedWork.Interfaces;
using Transactions.Domain.Events.TransactionEvents.cs;
using Transactions.Domain.Models.TransactionModels;

namespace Transactions.Application.Commands.TransactionCommands;

// REFACTOR SUGGESTIONS.
// Here I suggest to move domain events into domain models (this way moving even further to DDD pattern).
// Then Method HandleTransfer will be removed. Transfer model should fire TransferStartedDomainEvent or something like that.
// But for this i need to create Entity object that classes would inherit that contains domain event.
// Also logic for event publishig with method like Entity.Save(), or if using EF and then context.save() (example from eShopOnContainers from Microsoft);
// ALSO using repository would remove sql from here.
public class CreateTransactionCommandHandler(IDatabaseContext db, IMediator mediator) : IRequestHandler<CreateTransactionCommand, int>
{
    private readonly IDatabaseContext _db = db;
    private readonly IMediator _mediator = mediator;

    public async Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = Transaction.Create(request.TransactionTypeId, request.AccountId, request.Amount, request.TransferAccountId);
        var transactionId = await CreateTransaction(transaction);
        await HandleTransfer(transaction);
        return transactionId;
    }

    private async Task HandleTransfer(Transaction transaction)
    {
        if (!transaction.IsOfType(TransactionType.Transfer))
            return;

        var transfer = (SenderTransfer)transaction;
        transaction = transfer.GetRecieverTransfer();
        await CreateTransaction(transaction);
    }

    private async Task<int> CreateTransaction(Transaction transaction)
    {
        var id = await InsertTransaction(transaction);
        await _mediator.Publish(new TransactionCreatedDomainEvent(transaction));
        return id;
    }

    private async Task<int> InsertTransaction(Transaction transaction)
    {
        const string sql = @"
            INSERT INTO `transaction` (`transaction_type_id`,`account_id`,`amount`,`date`,`transfer_account_id`)
            VALUES (@Type,@AccountId,@Amount,@Date,@TransferAccountId); SELECT last_insert_rowid();
        ";
        return await _db.FirstOrDefault<int>(sql, new
        {
            Type = (int)transaction.TransactionType,
            transaction.AccountId,
            Amount = transaction.TransferAmount,
            Date = transaction.Date.ToString("yyyy-MM-dd HH:mm"),
            transaction.TransferAccountId
        });
    }
}
