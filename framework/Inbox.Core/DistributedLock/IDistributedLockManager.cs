﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inbox.Core.DistributedLock
{
    public interface IDistributedLockManager
    {
        /// <summary>
        /// 获取分布式锁，成功返回true，失败返回false
        /// </summary>
        /// <param name="id">锁Id</param>
        /// <param name="expiredTime">过期时间</param>
        /// <returns></returns>
        bool AcquireLock(string id, TimeSpan expiredTime);

        /// <summary>
        /// 获取分布式锁，如果失败按照一定时间重试
        /// </summary>
        /// <param name="id">锁Id</param>
        /// <param name="expiredTime">过期时间</param>
        /// <param name="waitTime">等待时间</param>
        /// <param name="retryTime">重试间隔</param>
        /// <returns></returns>
        bool AcquireLock(string id, TimeSpan expiredTime, TimeSpan waitTime, TimeSpan retryTime);

        /// <summary>
        /// 获取分布式锁，如果失败按照一定时间重试
        /// </summary>
        /// <param name="id">锁Id</param>
        /// <param name="expiredTime">过期时间</param>
        /// <param name="waitTime">等待时间</param>
        /// <param name="retryTime">重试间隔</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AcquireLockAsync(string id, TimeSpan expiredTime, TimeSpan waitTime, TimeSpan retryTime, CancellationToken? cancellationToken = null);

        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="id">锁Id</param>
        void ReleaseLock(string id);

        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="id">锁Id</param>
        /// <returns></returns>
        Task ReleaseLockAsync(string id);
    }
}
