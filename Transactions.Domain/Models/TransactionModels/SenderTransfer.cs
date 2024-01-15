namespace Transactions.Domain.Models.TransactionModels;

public class SenderTransfer(int accountId, decimal amount, int transferAccountId) : Transaction(TransactionType.Transfer, accountId, amount)
{

    public override int? TransferAccountId => transferAccountId;

    public override decimal? TransferAmount => -Amount;

    public override decimal Modify(decimal balance)
    {
        var newBalance = balance - Amount;
        ValidateBalance(newBalance, balance);
        return newBalance;
    }

    public SenderTransfer GetRecieverTransfer()
    {
        return new RecieverTransfer(transferAccountId, Amount, AccountId);
    }
}