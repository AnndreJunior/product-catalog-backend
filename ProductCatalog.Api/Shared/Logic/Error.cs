namespace ProductCatalog.Api.Shared.Logic;

public record Error(string Title, string Detail, ErrorType Type)
{
    public static readonly Error None = new(
        string.Empty,
        string.Empty,
        ErrorType.Failure);
    public static readonly Error NullValue = new(
        "Valor vazio",
        "O valor fornecido é vazio.",
        ErrorType.Failure);
}
