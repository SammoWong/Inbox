1.����Ŀ����Ӵ˰�
2.����Ŀ�ļ�appsettings.json�����Jwt���ã����磺
  "Jwt": {
    "SecurityKey":"BP0rJnkeyS1Ph6uTStgFPncGcMTFW0ZB",
    "Issuer": "Issuer",
    "Audience": "Audience",
    "Expiration": 7200//������
  }
3.��startup.cs����ӷ���AddJwt()
  services.AddJwt(Configuration.GetSection("Jwt").Get<JwtOption>());
4.�����֤��Ȩ�м��
  app.UseAuthentication();
  app.UseAuthorization();