1.����Ŀ����Ӵ˰�
2.����Ŀ�ļ�appsettings.json�����Consul���ã����磺
  "Consul": {
    //Consul Client ��ַ
    "ConsulUrl": "http://127.0.0.1:8500",
    //Key·��
    "ConsulKeyPath": "",
    //��ǰ�������ƣ����Զ��ʵ������
    "ServiceName": "Inbox.Sample",
    //��ǰ�����ַ
    "ServiceUrl": "http://localhost:9100",
    //����tag
    "ServerTags": [ "urlprefix-/identity" ],
    //�������ĵ�ַ����ǰ���񹫲�������һ��api�ӿ�
    "HealthCheckUrl": "/health",
    //�������
    "HealthCheckIntervalInSecond": 20
  }
3.��startup.cs����ӷ���AddConsul()
  var consulOptions = Configuration.GetSection("Consul").Get<ConsulOptions>();
  services.AddConsul(consulOptions.ConsulUrl, loadBalancer: TypeLoadBalancer.RoundRobin);
4.��Configure�������ע�ᵽConsul
  var consulOptions = Configuration.GetSection("Consul").Get<ConsulOptions>();
  app.RegisterToConsul(consulOptions);