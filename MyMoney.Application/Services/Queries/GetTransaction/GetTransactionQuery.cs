using ErrorOr;

using MyMoney.Application.Common.Security.Request;
using MyMoney.Application.Common.Security.Permissions;
using MyMoney.Application.Dtos;

namespace MyMoney.Application.Services.FetchAllTransactions.Queries;

[Authorize(Permissions = Permission.Transaction.Get)]
public record GetTransactionQuery(Guid UserId, Guid TransactionId) : IAuthorizeableRequest<ErrorOr<TransactionDto>>;