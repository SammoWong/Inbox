1.在项目中添加此包
2.在项目文件appsettings.json中添加Redis连接字符串配置
  "Redis": {
    "Configuration":"ConnectionString",
  }
3.在startup.cs中添加服务AddRedisDistributedLock()
  services.AddRedisDistributedLock(Configuration.GetSection("Redis").Get<DistributedLockOption>());
