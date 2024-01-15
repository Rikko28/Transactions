using MediatR;
using Transactions.Application.SeedWork.DTO;

namespace Transactions.Application.Queries.AccountQueries;
public record GetAccountQuery(int Id) : IRequest<AccountReadDto?>;
