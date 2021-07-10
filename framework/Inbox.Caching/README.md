1.在项目中添加此包
2.在业务服务项目的startup.cs中添加服务：
  services.AddCaching(option => option.UseInMemory());//内存缓存