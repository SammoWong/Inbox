using Inbox.Core.DistributedLock;
using RedLockNet;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;

namespace Inbox.DistributedLock.Redis
{
    public class DistributedLockManager : IDistributedLockManager
    {
        private readonly RedLockFactory _redlockFactory;
        private readonly List<IRedLock> ManagedLocks = new List<IRedLock>();

        public DistributedLockManager(string connectionString)
        {
            _redlockFactory = RedLockFactory.Create(new List<RedLockMultiplexer> { ConnectionMultiplexer.Connect(connectionString) });
        }

        /// <summary>
        /// 获取分布式锁，成功返回true，失败返回false
        /// </summary>
        /// <param name="id">锁Id</param>
        /// <param name="expiredTime">过期时间</param>
        /// <returns></returns>
        public bool AcquireLock(string id, TimeSpan expiredTime)
        {
            var redLock = _redlockFactory.CreateLock(id, expiredTime);
            if (redLock.IsAcquired)
            {
                lock (ManagedLocks)
                {
                    ManagedLocks.Add(redLock);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取分布式锁，成功返回true，失败返回false
        /// </summary>
        /// <param name="id">锁Id</param>
        /// <param name="expiredTime">过期时间</param>
        /// <param name="waitTime">等待时间</param>
        /// <param name="retryTime">重试间隔</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool AcquireLock(string id, TimeSpan expiredTime, TimeSpan waitTime, TimeSpan retryTime)
        {
            var redLock = _redlockFactory.CreateLock(id, expiredTime, waitTime, retryTime);

            if (redLock.IsAcquired)
            {
                lock (ManagedLocks)
                {
                    ManagedLocks.Add(redLock);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取分布式锁，如果失败按照一定时间重试
        /// </summary>
        /// <param name="id">锁Id</param>
        /// <param name="expiredTime">过期时间</param>
        /// <param name="waitTime">等待时间</param>
        /// <param name="retryTime">重试间隔</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> AcquireLockAsync(string id, TimeSpan expiredTime, TimeSpan waitTime, TimeSpan retryTime, CancellationToken? cancellationToken = null)
        {
            var redLock = await _redlockFactory.CreateLockAsync(id, expiredTime, waitTime, retryTime, cancellationToken);

            if (redLock.IsAcquired)
            {
                lock (ManagedLocks)
                {
                    ManagedLocks.Add(redLock);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="id"></param>
        public void ReleaseLock(string id)
        {
            lock (ManagedLocks)
            {
                foreach (var redLock in ManagedLocks)
                {
                    if (redLock.Resource == id)
                    {
                        redLock.Dispose();
                        ManagedLocks.Remove(redLock);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task ReleaseLockAsync(string id)
        {
            lock (ManagedLocks)
            {
                foreach (var redLock in ManagedLocks)
                {
                    if (redLock.Resource == id)
                    {
                        redLock.Dispose();
                        ManagedLocks.Remove(redLock);
                        break;
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
