using ErrorOr;

using MyMoney.Application.Common.Security.Request;
using MyMoney.Application.Common.Security.Permissions;
using MyMoney.Application.Common.Security.Policies;
using MyMoney.Application.Dtos;

namespace MyMoney.Application.Transactions.CreateTransaction.Commands;

[Authorize(Permissions = Permission.Transaction.Create, Policies = Policy.SelfOrAdmin)]
public record CreateTransactionCommand(Guid UserId, string AccountName, string Category, int Amount, DateTime DateTime) : IAuthorizeableRequest<ErrorOr<TransactionDto>>;