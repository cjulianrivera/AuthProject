using System.Threading.Tasks;
using Auth.API.Models;

namespace Auth.API.Data
{
  public interface IAuthRepository
  {
    Task<User> Login(string companyKey, string username, string password);
  }
}