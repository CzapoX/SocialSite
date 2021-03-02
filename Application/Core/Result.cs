namespace Application.Core
{
    public class Result<T>
    {
        public T Value { get; set; }
        public string Error { get; set; }
        public ResultStatus ResultStatus { get; set; }

        public static Result<T> Success(T value)
        {
            return new Result<T>
            {
                ResultStatus = ResultStatus.IsSuccess,
                Value = value
            };
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T>
            {
                ResultStatus = ResultStatus.Error,
                Error = error
            };
        }

        public static Result<T> Unauthorized(string error)
        {
            return new Result<T>
            {
                ResultStatus = ResultStatus.IsUnauthorized,
                Error = error
            };
        }
    }

    public enum ResultStatus
    {
        IsSuccess,
        Error,
        IsUnauthorized,
    }
}
