using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Inbox.Jwt
{
    public class JwtTokenBuilder
    {
        public static JwtToken BuildJwtToken(Claim[] claims, JwtOption option)
        {
            var now = DateTime.Now;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(option.SecurityKey));
            //实例化JwtSecurityToken
            var jwt = new JwtSecurityToken(
                issuer: option.Issuer,
                audience: option.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(option.Expiration),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new JwtToken
            {
                AccessToken = encodedJwt,
                Expiration = now.AddMinutes(option.Expiration),
            };
        }
    }
}
