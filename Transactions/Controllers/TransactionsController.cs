using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transactions.Application.Commands.TransactionCommands;
using Transactions.Application.SeedWork.Exceptions;
using Transactions.Domain.SeedWork.Exceptions;
using Transactions.Presentation.Controllers;

namespace Transactions.Controllers;

public class TransactionsController(IMediator mediator) : ApiController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> MakeTransaction(CreateTransactionCommand command)
    {
        try
        {
            return Ok(await _mediator.Send(command));
        }
        catch (Exception e) when (e is DomainException or IdNotFoundException)
        {
            return BadRequest(e.Message);
        }
    }
}
