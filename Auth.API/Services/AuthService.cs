using System;
using System.Text;
using Auth.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Linq;
using Auth.API.Dtos;

namespace Auth.API.Services
{
  public class AuthService : IAuthService
  {
    private readonly IConfiguration _config;

    public AuthService(IConfiguration config)
    {
      this._config = config;

    }
    public string GenerateToken(User user)
    {
      var claims = new[]
      {
          //new Claim("cloud_users_id", user.CloudUsersId.ToString()),
          new Claim("mail_siigo", user.Username),
          new Claim("tenant_id", user.TenantID),
          new Claim("cloud_tenant_company_key", user.CompanyKey),
          new Claim("users_id", user.UsersID)
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(1),
        SigningCredentials = creds
      };

      var tokenHandler = new JwtSecurityTokenHandler();

      var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }


    public PayloadDto DecodeToken(string token)
    {
      string payload = "{";
      var tokenHandler = new JwtSecurityTokenHandler();

      if (tokenHandler.CanReadToken(token))
      {
        var securityToken = tokenHandler.ReadJwtToken(token);
        foreach (Claim c in securityToken.Claims)
        {
          payload += '"' + c.Type + "\":\"" + c.Value + "\",";
        }

      }
      payload += "}";

      PayloadDto payloadDto = JsonConvert.DeserializeObject<PayloadDto>(payload);

      return payloadDto;
    }
  }
}