using Microsoft.AspNetCore.Mvc;

namespace ProductCatalog.Api.Shared.Extensions;

public static class ControllerResultExtensions
{
    /// <summary>
    /// Converte um <see cref="Result"/> que representa falha em um <see cref="IActionResult"/> apropriado,
    /// utilizando o tipo do erro para definir o status HTTP e o corpo da resposta.
    /// </summary>
    /// <param name="controller">Instância do controlador que gera a resposta.</param>
    /// <param name="result">Resultado contendo a falha a ser tratada.</param>
    /// <returns>Um <see cref="IActionResult"/> representando o erro.</returns>
    /// <exception cref="InvalidOperationException">
    /// Lançado caso o método seja chamado com um resultado bem-sucedido.
    /// </exception>
    public static IActionResult HandleFailure(this ControllerBase controller, Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Cannot handle success result as failure.");
        }

        PathString path = controller.HttpContext.Request.Path;
        Error error = result.Error;

        if (error.Type == ErrorType.Failure)
        {
            return controller.Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Erro Interno",
                detail: "Ocorreu um erro inesperado. Tente novamente mais tarde",
                instance: path);
        }

        return controller.Problem(
            statusCode: GetStatusCode(error.Type),
            title: error.Title,
            detail: error.Detail,
            instance: path);
    }

    private static int? GetStatusCode(ErrorType type)
        => type switch
        {
            _ => StatusCodes.Status500InternalServerError
        };
}
