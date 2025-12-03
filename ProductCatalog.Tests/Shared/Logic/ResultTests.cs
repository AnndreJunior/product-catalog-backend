using ProductCatalog.Api.Shared.Logic;

namespace ProductCatalog.Tests.Shared.Logic;

public sealed class ResultTests
{
    [Fact]
    public void GivenSuccessCalled_WhenCreatingResult_ThenResultShouldBeSuccess()
    {
        // Act
        var result = Result.Success();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(Error.None, result.Error);
    }

    [Fact]
    public void GivenFailureCalled_WhenCreatingResult_ThenResultShouldContainError()
    {
        // Arrange
        var error = new Error("Error Title", "Error Detail", ErrorType.Failure);

        // Act
        var result = Result.Failure(error);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void GivenSuccessWithValue_WhenCreatingResult_ThenResultShouldContainValue()
    {
        // Arrange
        const int ExpectedValue = 5;

        // Act
        var result = Result.Success(ExpectedValue);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ExpectedValue, result.Value);
        Assert.Equal(Error.None, result.Error);
    }

    [Fact]
    public void GivenFailureWithError_WhenCreatingResult_ThenAccessingValueShouldThrow()
    {
        // Arrange
        var error = new Error("Error Title", "Error Detail", ErrorType.Failure);

        // Act
        var result = Result.Failure<object>(error);

        // Assert
        Assert.Throws<InvalidOperationException>(() => _ = result.Value);
    }

    [Fact]
    public void GivenImplicitConversion_WhenValueIsNotNull_ThenShouldReturnSuccessResult()
    {
        // Arrange
        const int Value = 5;

        // Act
        Result<int> result = Value;

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(Value, result.Value);
    }

    [Fact]
    public void GivenImplicitConversion_WhenValueIsNull_ThenShouldReturnFailureResult()
    {
        // Arrange
        int? value = null;

        // Act
        Result<int?> result = value;

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(Error.NullValue, result.Error);
    }
}
