namespace Inbox.Result
{
    public class Result : IResult
    {
        protected Result() { }

        public int Code { get; set; }

        public string Message { get; set; }

        public static IResult Success()
        {
            return new Result { Code = ResultCode.Success };
        }

        public static IResult Success(string message)
        {
            return new Result { Code = ResultCode.Success, Message = message };
        }

        public static IResult Fail(int code)
        {
            return new Result { Code = code };
        }

        public static IResult Fail(string message)
        {
            return new Result { Code = ResultCode.DefaultError, Message = message };
        }

        public static IResult Fail(int code, string message)
        {
            return new Result { Code = code, Message = message };
        }

        public static IResult FromResult(IResult result)
        {
            return new Result { Code = result.Code, Message = result.Message };
        }
    }

    public class Result<T> : Result, IResult<T>
    {
        protected Result() { }

        public T Data { get; private init; }

        public static new IResult<T> Success()
        {
            return new Result<T> { Code = ResultCode.Success };
        }

        public static new IResult<T> Success(string message)
        {
            return new Result<T> { Code = ResultCode.Success, Message = message };
        }

        public static IResult<T> Success(T data)
        {
            return new Result<T> { Code = ResultCode.Success, Data = data };
        }

        public static IResult<T> Success(T data, string message)
        {
            return new Result<T> { Code = ResultCode.Success, Message = message, Data = data };
        }

        public static new IResult<T> Fail(int code)
        {
            return new Result<T> { Code = code };
        }

        public static IResult<T> Fail(string message, int code)
        {
            return new Result<T> { Code = code, Message = message };
        }

        public static IResult<T> Fail(string message, T data, int code)
        {
            return new Result<T> { Code = code, Data = data, Message = message };
        }

        public static Result<T> FromData(T data)
        {
            return new Result<T> { Code = ResultCode.Success, Data = data };
        }
    }
}
