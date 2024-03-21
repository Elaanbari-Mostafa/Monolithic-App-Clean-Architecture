﻿namespace Domain.Shared;

public class Result
{
    public bool IsSuccess;
    public bool IsFailure => !IsSuccess;
    public Error Error;

    protected Result(bool isSuccess, Error error)
    {
        if ((isSuccess && error != Error.None) ||
            (!isSuccess && error == Error.None))
        {
            throw new InvalidOperationException();
        }
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
    public static Result<TValue> Create<TValue>(TValue? value, Error? error = null) => value is null
        ? Failure<TValue>(error ?? Error.NullValue)
        : Success(value);
}