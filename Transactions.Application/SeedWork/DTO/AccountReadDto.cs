namespace Transactions.Application.SeedWork.DTO;

public class AccountReadDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public float Balance { get; set; }
}