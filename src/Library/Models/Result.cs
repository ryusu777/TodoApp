namespace Library.Models;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result<T> Success<T>(T value) => new Result<T>(true, Error.None, value);
    public static Result<T> Failure<T>(Error error) => new Result<T>(false, error, default);
    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
}

public class Result<T> : Result
{
    public T? Value { get; }
    public Result(bool isSuccess, Error error, T? value)
        : base(isSuccess, error)
    {
        Value = value;
    }

}