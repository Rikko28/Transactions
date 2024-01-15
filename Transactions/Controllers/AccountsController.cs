using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transactions.Application.Commands.AccountCommands;
using Transactions.Application.Queries.AccountQueries;
using Transactions.Application.SeedWork.Exceptions;

namespace Transactions.Presentation.Controllers;

public class AccountsController(IMediator mediator) : ApiController
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetAccount(int accountId)
    {
        var result = await _mediator.Send(new GetAccountQuery(accountId));
        return Ok(result);
    }

    // REFACTOR NOTES
    // Instead of using try catch we can create exception handling middleware so we return user friendly exception messages instead of this.
    // Catch known exception - return badrequest(msg),
    // Catch all exceptions - return 500 error with "please try again".
    [HttpPost]
    public async Task<IActionResult> CreateAccount(CreateAccountCommand command)
    {
        try
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }
        catch(IdNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
