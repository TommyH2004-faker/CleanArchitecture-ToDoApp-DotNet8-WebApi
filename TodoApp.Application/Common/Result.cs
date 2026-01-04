namespace TodoApp.Application.Common
{
    // Result Pattern để xử lý kết quả thay vì throw exception
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Data { get; private set; }
        public string ErrorMessage { get; private set; } = string.Empty;
        public List<string> Errors { get; private set; } = new();

        private Result(bool isSuccess, T? data, string errorMessage, List<string>? errors = null)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
            Errors = errors ?? new List<string>();
        }

        // Success result
        public static Result<T> Success(T data)
        {
            return new Result<T>(true, data, string.Empty);
        }

        // Failure result với 1 lỗi
        public static Result<T> Failure(string errorMessage)
        {
            return new Result<T>(false, default, errorMessage);
        }

        // Failure result với nhiều lỗi
        public static Result<T> Failure(List<string> errors)
        {
            return new Result<T>(false, default, string.Join("; ", errors), errors);
        }
    }

    // Result không có data (cho Delete, Update operations)
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; } = string.Empty;
        public List<string> Errors { get; private set; } = new();

        private Result(bool isSuccess, string errorMessage, List<string>? errors = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Errors = errors ?? new List<string>();
        }

        public static Result Success()
        {
            return new Result(true, string.Empty);
        }

        public static Result Failure(string errorMessage)
        {
            return new Result(false, errorMessage);
        }

        public static Result Failure(List<string> errors)
        {
            return new Result(false, string.Join("; ", errors), errors);
        }
    }
}
