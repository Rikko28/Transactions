using MediatR;

namespace Transactions.Application.Commands.AccountCommands;
public record CreateAccountCommand(int UserId) : IRequest<int>;