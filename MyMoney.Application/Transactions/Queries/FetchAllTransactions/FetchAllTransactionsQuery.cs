using ErrorOr;

using MyMoney.Application.Common.Security.Request;
using MyMoney.Application.Common.Security.Permissions;
using MyMoney.Application.Dtos;

namespace MyMoney.Application.Transactions.FetchAllTransactions.Queries;

[Authorize(Permissions = Permission.Transaction.FetchAll)]
public record FetchAllTransactionsQuery(Guid UserId) : IAuthorizeableRequest<ErrorOr<List<TransactionDto>>>;