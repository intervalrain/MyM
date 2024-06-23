using ErrorOr;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

using MediatR;

using MyMoney.Application.Common.Behaviors;
using MyMoney.Application.Dtos;
using MyMoney.Application.Services.CreateTransaction.Commands;
using MyMoney.TestCommon.Transactions;

using NSubstitute;

namespace MyMoney.Application.Application.UnitTests.Common.Behaviors;

[TestClass]
public class ValidationBehaviorTests
{
    private readonly ValidationBehavior<CreateTransactionCommand, ErrorOr<TransactionDto>> _validationBehavior;
    private readonly IValidator<CreateTransactionCommand> _mockValidator;
    private readonly RequestHandlerDelegate<ErrorOr<TransactionDto>> _mockNextBehavior;

    public ValidationBehaviorTests()
    {
        _mockNextBehavior = Substitute.For<RequestHandlerDelegate<ErrorOr<TransactionDto>>>();
        _mockValidator = Substitute.For<IValidator<CreateTransactionCommand>>();

        _validationBehavior = new(_mockValidator);
    }

    [TestMethod]
    public async Task InvokeValidationBehavior_WhenValidatorResultIsValid_ShouldInvokeNextBehavior()
    {
        // Arrange
        var createTransactionCommand = TransactionCommandFactory.CreateCreateTransactionCommand();
        var transaction = TransactionDto.ToDto(TransactionFactory.CreateTransaction());
        

        _mockValidator
            .ValidateAsync(createTransactionCommand, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult());

        _mockNextBehavior.Invoke().Returns(transaction);

        // Act
        var result = await _validationBehavior.Handle(createTransactionCommand, _mockNextBehavior, default);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeEquivalentTo(transaction);
    }

    [TestMethod]
    public async Task InvokeValidationBehavior_WhenValidatorResultIsNotValid_ShouldReturnListOfErrors()
    {
        // Arrange
        var createTransactionCommand = TransactionCommandFactory.CreateCreateTransactionCommand();
        List<ValidationFailure> validationFailures = new() { new(propertyName: "foo", errorMessage: "bad foo") };

        _mockValidator
            .ValidateAsync(createTransactionCommand, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult(validationFailures));

        // Act
        var result = await _validationBehavior.Handle(createTransactionCommand, _mockNextBehavior, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be("foo");
        result.FirstError.Description.Should().Be("bad foo");
    }

    [TestMethod]
    public async Task InvokeValidationBehavior_WhenNoValidator_ShouldInvokeNextBehavior()
    {
        // Arrange
        var CreateTransactionCommand = TransactionCommandFactory.CreateCreateTransactionCommand();
        var validationBehavior = new ValidationBehavior<CreateTransactionCommand, ErrorOr<TransactionDto>>();

        var transaction = TransactionDto.ToDto(TransactionFactory.CreateTransaction());
        _mockNextBehavior.Invoke().Returns(transaction);

        // Act
        var result = await validationBehavior.Handle(CreateTransactionCommand, _mockNextBehavior, default);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(transaction);
    }
}

