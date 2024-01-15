using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transactions.Application.Queries.AccountQueries;

namespace Transactions.Presentation.Controllers;

public class ReportController(IMediator mediator) : ApiController
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetAccountReport(int accountId)
    {
        return Ok(await _mediator.Send(new GetReportQuery(accountId)));
    }
}
