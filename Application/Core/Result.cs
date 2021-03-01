namespace Application.Core
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public bool IsUnauthorized { get; set; }
        public T Value { get; set; }
        public string Error { get; set; }
        public bool IsBadRequest { get; set; }

        public static Result<T> Success(T value)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Value = value
            };
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Error = error
            };
        }

        public static Result<T> Unauthorized(string error)
        {
            return new Result<T>
            {
                IsUnauthorized = true,
                Error = error
            };
        }

        public static Result<T> BadRequest(string error)
        {
            return new Result<T>
            {
                IsBadRequest = true,
                Error = error
            };
        }
    }
}
