1.在项目中添加此包
2.在项目文件appsettings.json中添加Jwt配置，例如：
  "Jwt": {
    "SecurityKey":"BP0rJnkeyS1Ph6uTStgFPncGcMTFW0ZB",
    "Issuer": "Issuer",
    "Audience": "Audience",
    "Expiration": 7200//分钟数
  }
3.在startup.cs中添加服务AddJwt()
  services.AddJwt(Configuration.GetSection("Jwt").Get<JwtOption>());
4.添加认证授权中间件
  app.UseAuthentication();
  app.UseAuthorization();