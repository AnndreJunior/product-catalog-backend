namespace ProductCatalog.Api.Shared.Logic;

/// <summary>
/// Representa um erro ocorrido durante a execução de uma operação,
/// armazenando informações úteis para diagnóstico.
/// </summary>
/// <param name="Title">
/// Título resumido que descreve o erro ocorrido.
/// </param>
/// <param name="Detail">
/// Descrição detalhada do erro, fornecendo contexto adicional.
/// </param>
/// <param name="Type">
/// Classificação do erro, indicando sua natureza ou categoria.
/// </param>
public record Error(string Title, string Detail, ErrorType Type)
{
    /// <summary>
    /// Representa a ausência de erro. Utilizado quando nenhuma falha ocorreu.
    /// </summary>
    public static readonly Error None = new(
        string.Empty,
        string.Empty,
        ErrorType.Failure);

    /// <summary>
    /// Representa um erro disparado quando um valor obrigatório não foi informado.
    /// </summary>
    public static readonly Error NullValue = new(
        "Valor vazio",
        "O valor fornecido está ausente ou é inválido.",
        ErrorType.Failure);
}
