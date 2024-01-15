namespace Transactions.Domain.Models.TransactionModels;

public class RecieverTransfer(int accountId, decimal amount, int transferAccountId) : SenderTransfer(accountId, amount, transferAccountId)
{
    public override decimal? TransferAmount => Amount;
    public override decimal Modify(decimal balance)
    {
        return balance + Amount;
    }
}