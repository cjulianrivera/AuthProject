using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Auth.API.Models;
using Dapper;
using Microsoft.Extensions.Configuration;


namespace Auth.API.Data
{
  public class AuthRepository : IAuthRepository
  {
    private readonly IConfiguration _config;

    public AuthRepository(IConfiguration config)
    {
      this._config = config;

    }

    public async Task<User> Login(string companyKey, string username, string password)
    {
      User user = new User();
      DynamicParameters parameter = new DynamicParameters();
      parameter.Add("@userName", username);
      parameter.Add("@password", password);
      parameter.Add("@companyKey", companyKey);
      using (IDbConnection conn = new SqlConnection(_config.GetSection("ConnectionStrings:DefaultConnection").Value))
      {
        const string sql = @"SELECT CU.CloudUsersID,CU.UsersID, CU.UserName, CT.CloudTenantConnectionUser AS TenantID, CT.CloudTenantCompanyKey AS CompanyKey 
                                         FROM CloudUsers CU WITH (NOLOCK)
                                         INNER JOIN CloudTenant CT WITH (NOLOCK) ON CT.CloudTenantID = CU.CloudTenantCode 
                                         WHERE CU.UserName = @userName AND CU.Password = @password AND CU.Active =1 AND CT.Active = 1 
                                         AND CT.CloudTenantCompanyKey = @companyKey";

        user = await conn.QueryFirstOrDefaultAsync<User>(sql, parameter);
      }

      if (user != null)
      {
        user.TenantID = user.TenantID.Replace(this._config.GetSection("AppSettings:ConnectionPrefix").Value, "");
      }

      return user;
    }
  }
}