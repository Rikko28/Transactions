using MediatR;
using Transactions.Application.SeedWork.DTO;

namespace Transactions.Application.Queries.AccountQueries;

public record GetReportQuery(int AccountId) : IRequest<ReportReadDto?>;
