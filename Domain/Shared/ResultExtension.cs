using static Domain.Shared.Result;

namespace Domain.Shared;

public static class ResultExtension
{
    public static Result<TValue> Ensure<TValue>(this Result<TValue> result, Func<TValue, bool> predicate, Error error)
        => (result.IsSuccess && !predicate(result.Value)) 
                ? result 
                : Failure<TValue>(error);

    public static Result<TOut> Map<TIn,TOut>(this Result<TIn> result, Func<TIn,TOut> predicate)
        => result.IsSuccess
                ? predicate(result.Value)
                : Failure<TOut>(result.Error);
}
