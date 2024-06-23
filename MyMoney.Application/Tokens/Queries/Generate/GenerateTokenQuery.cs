using ErrorOr;

using MediatR;

namespace MyMoney.Application.Tokens.Queries.Generate;

public record GenerateTokenQuery(
    Guid? Id,
    string FirstName,
    string LastName,
    string Email) : IRequest<ErrorOr<GenerateTokenResult>>;