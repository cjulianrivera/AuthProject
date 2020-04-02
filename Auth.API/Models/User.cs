namespace Auth.API.Models
{
  public class User
  {
    public long CloudUsersId { get; set; }
    public string Username { get; set; }
    public string CompanyKey { get; set; }
    public string TenantID { get; set; }
    public string UsersID { get; set; }
  }
}