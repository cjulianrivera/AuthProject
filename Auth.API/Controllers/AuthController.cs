using System.Threading.Tasks;
using Auth.API.Data;
using Auth.API.Dtos;
using Auth.API.Services;
using Microsoft.AspNetCore.Mvc;


namespace Auth.API.Controllers
{
  [Route("[Controller]")]
  [ApiController]
  [Produces("application/json")]
  public class AuthController : ControllerBase
  {
    private readonly IAuthRepository _repo;
    private readonly IAuthService _service;

    public AuthController(IAuthRepository repo, IAuthService service)
    {
      this._service = service;
      this._repo = repo;
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
    {
      var userFromRepo = await _repo.Login(userForLoginDto.CompanyKey, userForLoginDto.Username, userForLoginDto.Password);

      if (userFromRepo == null)
        return Unauthorized();

      string token = _service.GenerateToken(userFromRepo);

      return Ok(new
      {
        token = token
      });



    }

  }
}