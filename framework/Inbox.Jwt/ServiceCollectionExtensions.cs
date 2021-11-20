using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Inbox.Jwt
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJwt(this IServiceCollection services, JwtOption option)
        {
            //添加认证配置
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                //验证配置，比如是否验证发布者，订阅者，密钥，以及过期时间等
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = option.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(option.SecurityKey)),
                    ValidateAudience = true,
                    ValidAudience = option.Audience,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    //ClockSkew = TimeSpan.FromMinutes(option.ClockSkew)//总的Token有效时间 = JwtRegisteredClaimNames.Exp + ClockSkew
                };
            });
        }
    }
}
