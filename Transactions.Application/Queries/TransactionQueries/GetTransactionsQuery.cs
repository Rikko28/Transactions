using MediatR;
using Transactions.Application.SeedWork.DTO;

namespace Transactions.Application.Queries.TransactionQueries;

public record GetTransactionsQuery(int AccountId) : IRequest<IEnumerable<TransactionReadDto>>;
