using ErrorOr;
using MediatR;
using MyMoney.Application.Common.Interfaces;

namespace MyMoney.Application.Tokens.Queries.Generate;

public class GenerateTokenQueryHandler : IRequestHandler<GenerateTokenQuery, ErrorOr<GenerateTokenResult>>
{
    private readonly IJwtGenerator _jwtGenerator;

    public GenerateTokenQueryHandler(IJwtGenerator jwtTokenGenerator)
    {
        _jwtGenerator = jwtTokenGenerator;
    }
    public async Task<ErrorOr<GenerateTokenResult>> Handle(GenerateTokenQuery query, CancellationToken cancellationToken)
    {
        var id = query.Id ?? Guid.NewGuid();

        var token = await _jwtGenerator.GenerateToken(
            id,
            query.FirstName,
            query.LastName,
            query.Email);

        var authResult = new GenerateTokenResult(
            id,
            query.FirstName,
            query.LastName,
            query.Email,
            token);

        return await Task.FromResult(ErrorOrFactory.From(authResult));
    }
}