namespace Transactions.Domain.Models.TransactionModels;

public class Deposit(int accountId, decimal amount) : Transaction(TransactionType.Deposit, accountId, amount)
{
    public override int? TransferAccountId => null;

    public override decimal TransferAmount => Amount;

    public override decimal Modify(decimal balance)
    {
        return balance + Amount;
    }
}