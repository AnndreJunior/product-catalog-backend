using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ProductCatalog.Api.Shared.ExceptionHandlers;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public GlobalExceptionHandler(ProblemDetailsFactory problemDetailsFactory) 
        => _problemDetailsFactory = problemDetailsFactory;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
                                          Exception exception,
                                          CancellationToken cancellationToken)
    {
        ProblemDetails problemDetails = _problemDetailsFactory.CreateProblemDetails(
            httpContext,
            title: "Erro interno do servidor",
            detail: "Ocorreu um erro inesperado. Tente novamente mais tarde.",
            statusCode: StatusCodes.Status500InternalServerError,
            instance: httpContext.Request.Path);

        httpContext.Response.StatusCode = problemDetails.Status!.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
