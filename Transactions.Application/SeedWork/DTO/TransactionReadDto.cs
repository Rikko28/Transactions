namespace Transactions.Application.SeedWork.DTO;

public class TransactionReadDto
{
    public int Id { get; set; }
    public int TransactionTypeId { get; set; }
    public decimal Amount { get; set; }
    public string Date { get; set; } = string.Empty;
    public int? TransferAccountId { get; set; }
}
