namespace Inbox.Result
{
    public struct ResultCode
    {
        /// <summary>
        /// 未知错误
        /// </summary>
        public static readonly int Unknown = -1;

        /// <summary>
        /// 操作成功
        /// </summary>
        public static readonly int Success = 0;

        /// <summary>
        /// 操作失败
        /// </summary>
        public static readonly int Fail = 1;

        /// <summary>
        /// 系统错误
        /// </summary>
        public static readonly int SystemError = 10;
    }
}
