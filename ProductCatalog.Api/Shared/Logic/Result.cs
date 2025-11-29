namespace ProductCatalog.Api.Shared.Logic;

/// <summary>
/// Representa o resultado de uma operação, indicando sucesso ou falha.
/// </summary>
public class Result
{
    public Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error value", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Indica se a operação foi bem-sucedida.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Erro associado à operação, quando existente.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    /// Indica se a operação falhou.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Cria um resultado bem-sucedido.
    /// </summary>
    /// <returns>Uma instância de <see cref="Result"/> representando sucesso.</returns>
    public static Result Success() => new(true, Error.None);

    /// <summary>
    /// Cria um resultado bem-sucedido contendo um valor.
    /// </summary>
    /// <typeparam name="T">Tipo do valor retornado.</typeparam>
    /// <param name="value">Valor retornado pela operação.</param>
    /// <returns>Uma instância de <see cref="Result{T}"/> representando sucesso.</returns>
    public static Result<T> Success<T>(T value) => new(value, true, Error.None);

    /// <summary>
    /// Cria um resultado indicando falha com o erro especificado.
    /// </summary>
    /// <param name="error">Erro associado à operação.</param>
    /// <returns>Uma instância de <see cref="Result"/> representando falha.</returns>
    public static Result Failure(Error error) => new(false, error);

    /// <summary>
    /// Cria um resultado indicando falha com o erro especificado e tipo de valor.
    /// </summary>
    /// <typeparam name="T">Tipo esperado do valor da operação.</typeparam>
    /// <param name="error">Erro associado à operação.</param>
    /// <returns>Uma instância de <see cref="Result{T}"/> representando falha.</returns>
    public static Result<T> Failure<T>(Error error) => new(default, false, error);
}

/// <summary>
/// Representa o resultado de uma operação que retorna um valor.
/// </summary>
/// <typeparam name="TValue">Tipo do valor retornado.</typeparam>
public class Result<TValue> : Result
{
    public Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error)
        => Value = value;

    /// <summary>
    /// Valor resultante da operação quando bem-sucedida.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Lançada quando tentado acessar o valor de um resultado que representa falha.
    /// </exception>
    public TValue Value
    {
        get
        {
            if (!IsSuccess)
            {
                throw new InvalidOperationException("Cannot get the value of a failure result");
            }

            return field!;
        }
    }

    public static implicit operator Result<TValue>(TValue? value)
        => value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
}
