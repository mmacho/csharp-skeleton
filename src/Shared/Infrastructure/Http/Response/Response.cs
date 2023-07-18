namespace Aseme.Shared.Infrastructure.Http.Response
{
    public class BaseResponse : IResponse
    {
        public bool Succeeded { get; set; }

        public BaseResponse()
        {

        }

    }

    public interface IResponse
    {
        bool Succeeded { get; set; }
    }

    public interface IResponse<out T> : IResponse
    {
        T Data { get; }
    }

    // para cuando vaya bien
    public class Response<T> : BaseResponse, IResponse<T>
    {
        public T Data { get; set; }

        public Response()
        {
        }

        public static Response<T> Success(T data)
        {
            return new() { Succeeded = true, Data = data };
        }


    }

    // para cuando vaya mal
    public class ErrorResponse<T> : BaseResponse, IErrorResponse<T>
    {
        public T Error { get; set; }

        public ErrorResponse()
        {
        }

        public static Task<ErrorResponse<T>> ReturnErrorAsync()
        {
            return Task.FromResult(ReturnError());
        }

        public static ErrorResponse<T> ReturnError()
        {
            return new() { Succeeded = false };
        }

    }

    public interface IErrorResponse<out T> : IResponse
    {
        T Error { get; }
    }


}
