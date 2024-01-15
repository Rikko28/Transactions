using Transactions.Domain.Models.TransactionModels;
using Transactions.Domain.SeedWork.Exceptions;

namespace Transactions.Domain.Models.AccountModels;

public class Account
{
    public int Id { get; private set; }
    public int UserId { get; init; }
    public decimal Balance { get; private set; }

    public void AcceptTransaction(Transaction transaction)
    {
        Balance = transaction.Modify(Balance);
    }
}
