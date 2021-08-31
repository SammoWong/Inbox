namespace Inbox.Result
{
    public struct ResultCode
    {
        public static readonly int Success = 0;

        public static int DefaultError = 1000;

        public static int Unauthorized => DefaultError + 1;

        public static int InvalidRequest => DefaultError + 2;
    }
}
