1.����Ŀ����Ӵ˰�
2.����Ŀ�ļ�appsettings.json�����Redis�����ַ�������
  "Redis": {
    "Configuration":"ConnectionString",
  }
3.��startup.cs����ӷ���AddRedisDistributedLock()
  services.AddRedisDistributedLock(Configuration.GetSection("Redis").Get<DistributedLockOption>());
