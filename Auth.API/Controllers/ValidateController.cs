using System.Net.Http;
using System.Threading.Tasks;
using Auth.API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
  [Authorize]
  [Route("[Controller]")]
  [ApiController]
  public class ValidateController : ControllerBase
  {
    private readonly IAuthService _service;
    private static readonly HttpClient HttpClient = new HttpClient();
    public ValidateController(IAuthService service)
    {
      this._service = service;

    }
    [HttpGet]
    public async Task<IActionResult> ValidateAsync()
    {
      string token = await HttpContext.GetTokenAsync("access_token");

      return Ok(this._service.DecodeToken(token));
    }
  }
}