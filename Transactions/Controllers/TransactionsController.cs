using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transactions.Application.Commands.TransactionCommands;
using Transactions.Application.Queries.TransactionQueries;
using Transactions.Application.SeedWork.Exceptions;
using Transactions.Domain.SeedWork.Exceptions;
using Transactions.Presentation.Controllers;

namespace Transactions.Controllers;

public class TransactionsController(IMediator mediator) : ApiController
{
    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetAccountTransactions(int accountId)
    {
        return Ok(await mediator.Send(new GetTransactionsQuery(accountId)));
    }

    [HttpPost]
    public async Task<IActionResult> MakeTransaction(CreateTransactionCommand command)
    {
        try
        {
            return Ok(await mediator.Send(command));
        }
        catch (Exception e) when (e is DomainException or IdNotFoundException)
        {
            return BadRequest(e.Message);
        }
    }
}
