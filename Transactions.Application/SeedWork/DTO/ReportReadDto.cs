using Transactions.Domain.Models.TransactionModels;

namespace Transactions.Application.SeedWork.DTO;

public class ReportReadDto
{
    public ReportReadDto(params TransactionReadDto[] transactions)
    {
        foreach (var t in transactions)
        {
            if (t.Amount < 0)
                TotalOutcome += t.Amount;
            else
                TotalIncome += t.Amount;
            Balance += t.Amount;
        }
    }

    public decimal Balance { get; set; }
    public decimal TotalIncome { get; set; }

    // Is this expense summaries? *confused smile* I hope so.
    public decimal TotalOutcome { get; set; }
}