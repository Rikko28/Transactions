using Transactions.Domain.SeedWork.Exceptions;

namespace Transactions.Domain.Models.TransactionModels;

public abstract class Transaction
{
    protected decimal Amount;

    protected Transaction()
    {
    }

    protected Transaction(TransactionType transactionType, int accountId, decimal amount)
    {
        TransactionType = transactionType;
        AccountId = accountId;
        Amount = Math.Round(amount, 2);
        Date = DateTime.UtcNow;
    }

    public int Id { get; private set; }
    public TransactionType TransactionType { get; init; }
    public int AccountId { get; protected set; }
    public DateTime Date { get; init; }
    public abstract int? TransferAccountId { get; }
    public abstract decimal TransferAmount { get; }

    public static Transaction Create(int typeId, int accountId, decimal amount, int? transferAccountId = null)
    {
        if (accountId == 0)
            throw new DomainException("Transaction requires account id");
        if (amount <= 0)
            throw new DomainException("Invalid transaction amount");
        return CreateNewTransaction(typeId, accountId, amount, transferAccountId);
    }

    private static Transaction CreateNewTransaction(int typeId, int accountId, decimal amount, int? transferAccountId)
    {
        var type = (TransactionType)typeId;
        switch (type)
        {
            case TransactionType.Deposit:
                return new Deposit(accountId, amount);
            case TransactionType.Withdrawal:
                return new Withdrawal(accountId, amount);
            case TransactionType.Transfer:
                if (transferAccountId == null || transferAccountId.Value == 0)
                    throw new DomainException($"Transfer requres transfer account id to not be null or zero");
                return new SenderTransfer(accountId, amount, transferAccountId.Value);
            default: throw new DomainException("Unknown transaction type");
        }
    }

    public bool IsOfType(TransactionType type)
    {
        return TransactionType == type;
    }

    public abstract decimal Modify(decimal balance);

    protected void ValidateBalance(decimal newBalance, decimal oldBalance)
    {
        if (newBalance < 0)
            throw new DomainException($"Can't make transaction, balance too low. Current balance: {oldBalance}, tried to transfer: {Amount}");
    }
}
