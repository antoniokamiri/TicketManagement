using Domain.Entities;

namespace Domain.DTO.Response;

public class BaseResponse
{
    internal BaseResponse()
    {
        Errors = new string[] { };
    }

    internal BaseResponse(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public string ErrorMessage => string.Join(", ", Errors ?? new string[] { });

    public bool Succeeded { get; init; }

    public string[] Errors { get; init; }

    public static BaseResponse Success() => new BaseResponse(true, Array.Empty<string>());
    public static Task<BaseResponse> SuccessAsync() => Task.FromResult(Success());
    public static BaseResponse Failure(params string[] errors) => new BaseResponse(false, errors);
    public static Task<BaseResponse> FailureAsync(params string[] errors) => Task.FromResult(Failure(errors));
}

public class BaseResponse<T> : BaseResponse
{
    public T? Data { get; set; }
    public static BaseResponse<T> Success(T data) => new BaseResponse<T> { Succeeded = true, Data = data };
    public static Task<BaseResponse<T>> SuccessAsync(T data) => Task.FromResult(Success(data));
    public static new BaseResponse<T> Failure(params string[] errors) => new BaseResponse<T> { Succeeded = false, Errors = errors.ToArray() };
    public static new Task<BaseResponse<T>> FailureAsync(params string[] errors) => Task.FromResult(new BaseResponse<T> { Succeeded = false, Errors = errors.ToArray() });
}
