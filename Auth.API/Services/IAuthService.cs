using System.Threading.Tasks;
using Auth.API.Dtos;
using Auth.API.Models;

namespace Auth.API.Services
{
  public interface IAuthService
  {
    string GenerateToken(User user);
    PayloadDto DecodeToken(string token);
  }
}