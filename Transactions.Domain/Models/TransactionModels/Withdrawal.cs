namespace Transactions.Domain.Models.TransactionModels;

public class Withdrawal(int accountId, decimal amount) : Transaction(TransactionType.Withdrawal, accountId, amount)
{
    public override int? TransferAccountId => null;

    public override decimal TransferAmount => -Amount;

    public override decimal Modify(decimal balance)
    {
        var newBalance = balance - Amount;
        ValidateBalance(newBalance, balance);
        return newBalance;
    }
}
