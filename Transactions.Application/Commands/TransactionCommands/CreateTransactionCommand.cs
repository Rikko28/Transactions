using MediatR;

namespace Transactions.Application.Commands.TransactionCommands;

/// <summary>
/// Creates new transaction
/// </summary>
/// <param name="TransactionTypeId"></param>
/// <param name="AccountId"></param>
/// <param name="Amount"></param>
/// <param name="TransferAccountId"></param>
/// <exception cref="SeedWork.Exceptions.IdNotFoundException"/>
/// <exception cref="Domain.SeedWork.Exceptions.DomainException"/>
public record CreateTransactionCommand(int TransactionTypeId, int AccountId, decimal Amount, int? TransferAccountId) : IRequest<int>;