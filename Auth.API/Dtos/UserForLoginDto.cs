namespace Auth.API.Dtos
{
  public class UserForLoginDto
  {
    public string CompanyKey { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
  }
}