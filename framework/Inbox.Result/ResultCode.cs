namespace Inbox.Result
{
    public struct ResultCode
    {
        public const int Success = 0;

        public const int DefaultError = 1000;

        public int Unauthorized => DefaultError + 1;

        public int InvalidRequest => DefaultError + 2;
    }
}
