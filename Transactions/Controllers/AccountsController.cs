using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transactions.Application.Queries.AccountQueries;

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
}
