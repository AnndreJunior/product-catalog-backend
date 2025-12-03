namespace ProductCatalog.Api.Shared.Logic;

public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public Error Error { get; }
    public bool IsFailure => !IsSuccess;

    public static Result Success() => new(true, Error.None);
    public static Result<T> Success<T>(T value) => new(value, true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result<T> Failure<T>(Error error) => new(default, false, error);
}

public class Result<TValue> : Result
{
    protected internal Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)
        => Value = value;

    public TValue Value
    {
        get
        {
            if (IsFailure)
            {
                throw new InvalidOperationException("The value of a failure result cannot be accessed.");
            }

            return field!;
        }
    }

    public static implicit operator Result<TValue>(TValue? value)
        => value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
}
