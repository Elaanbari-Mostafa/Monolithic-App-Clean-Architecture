using Microsoft.AspNetCore.Mvc;
using static Domain.Shared.Result;

namespace Domain.Shared;

public static class ResultExtension
{
    public static Result<TValue> Ensure<TValue>(this Result<TValue> result, Func<TValue, bool> predicate, Error error)
        => (result.IsSuccess && !predicate(result.Value))
                ? result
                : Failure<TValue>(error);

    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> predicate)
        => result.IsSuccess
                ? predicate(result.Value)
                : Failure<TOut>(result.Error);

    public static Result<TValue> OnSuccess<TValue>(this Result<TValue> result, Action<TValue> action)
    {
        if (result.IsSuccess)
        {
            action(result.Value);
        }

        return result;
    }

    public static IActionResult MapActionResult<TValue>(
        this Result<TValue> result,
        Func<TValue, IActionResult> onSuccess,
        Func<Error, IActionResult> onFailure) => result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Error);

    public static IActionResult MapActionResult(
        this Result result,
        Func<IActionResult> onSuccess,
        Func<Error, IActionResult> onFailure) => result.IsSuccess ? onSuccess() : onFailure(result.Error);
}