using Microsoft.AspNetCore.Mvc;

namespace ProductCatalog.Api.Shared.Extensions;

public static class ResultExtensions
{
    /// <summary>
    /// Executa uma função assíncrona que retorna um <see cref="Result"/> 
    /// usando o valor contido em um <see cref="Result{TIn}"/> bem-sucedido.
    /// </summary>
    /// <typeparam name="TIn">Tipo do valor contido no resultado de entrada.</typeparam>
    /// <param name="result">Resultado de entrada.</param>
    /// <param name="func">Função a ser executada quando o resultado for bem-sucedido.</param>
    /// <returns>Um <see cref="Task{T}"/> contendo um <see cref="Result"/>.</returns>
    public static async Task<Result> Bind<TIn>(
        this Result<TIn> result,
        Func<TIn, Task<Result>> func) => await func(result.Value);

    /// <summary>
    /// Executa uma função assíncrona que retorna um <see cref="Result{TOut}"/>
    /// usando o valor contido em um <see cref="Result{TIn}"/> bem-sucedido.
    /// </summary>
    /// <typeparam name="TIn">Tipo do valor contido no resultado de entrada.</typeparam>
    /// <typeparam name="TOut">Tipo do valor retornado no resultado de saída.</typeparam>
    /// <param name="result">Resultado de entrada.</param>
    /// <param name="func">Função a ser executada quando o resultado for bem-sucedido.</param>
    /// <returns>Um <see cref="Task{T}"/> contendo um <see cref="Result{TOut}"/>.</returns>
    public static async Task<Result<TOut>> Bind<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, Task<Result<TOut>>> func) => await func(result.Value);

    /// <summary>
    /// Executa um dos delegates fornecidos dependendo do estado de um <see cref="Result"/> assíncrono.
    /// </summary>
    /// <param name="resultTask">Tarefa que produz o resultado.</param>
    /// <param name="onSuccess">Função chamada quando o resultado for bem-sucedido.</param>
    /// <param name="onFailure">Função chamada quando o resultado representar falha.</param>
    /// <returns>Um <see cref="IActionResult"/> resultante do delegate executado.</returns>
    public static async Task<IActionResult> Match(
        this Task<Result> resultTask,
        Func<IActionResult> onSuccess,
        Func<Result, IActionResult> onFailure)
    {
        Result result = await resultTask;

        return result.IsSuccess ? onSuccess() : onFailure(result);
    }

    /// <summary>
    /// Executa um dos delegates fornecidos dependendo do estado de um <see cref="Result{TIn}"/> assíncrono.
    /// </summary>
    /// <typeparam name="TIn">Tipo do valor contido no resultado.</typeparam>
    /// <param name="resultTask">Tarefa que produz o resultado.</param>
    /// <param name="onSuccess">Função chamada quando o resultado for bem-sucedido, recebendo o valor.</param>
    /// <param name="onFailure">Função chamada quando o resultado representar falha.</param>
    /// <returns>Um <see cref="IActionResult"/> resultante do delegate executado.</returns>
    public static async Task<IActionResult> Match<TIn>(
        this Task<Result<TIn>> resultTask,
        Func<TIn, IActionResult> onSuccess,
        Func<Result, IActionResult> onFailure)
    {
        Result<TIn> result = await resultTask;

        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
    }
}
