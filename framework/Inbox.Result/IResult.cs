namespace Inbox.Result
{
    public interface IResult
    {
        int Code { get; }

        string Message { get; }
    }

    public interface IResult<T> : IResult
    {
        T Data { get; }
    }
}
