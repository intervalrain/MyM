using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MyMoney.Application;
using MyMoney.Application.Tokens.Queries.Generate;
using MyMoney.Contracts.Tokens;

namespace MyMoney.Api.Controllers;

[Route("tokens")]
[AllowAnonymous]
public class TokensController : ApiController
{
    private readonly IMediator _mediator;

    public TokensController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("generate")]
    public async Task<IActionResult> GenerateToken(GenerateTokenRequest request)
    {
        var query = QueryFactory.GenerateTokenQuery(
            request.Id,
            request.FirstName,
            request.LastName,
            request.Email);

        var result = await _mediator.Send(query);

        return result.Match(
            generateTokenResult => Ok(ToDto(generateTokenResult)),
            Problem);
    }

    private static TokenResponse ToDto(GenerateTokenResult authResult)
    {
        return new TokenResponse(
            authResult.Id,
            authResult.FirstName,
            authResult.LastName,
            authResult.Email,
            authResult.Token);
    }
}