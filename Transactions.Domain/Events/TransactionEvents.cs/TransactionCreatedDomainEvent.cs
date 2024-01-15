using MediatR;
using Transactions.Domain.Models.TransactionModels;

namespace Transactions.Domain.Events.TransactionEvents.cs;

public record TransactionCreatedDomainEvent(Transaction Transaction) : INotification;
