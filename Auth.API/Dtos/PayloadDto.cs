namespace Auth.API.Dtos
{
  public class PayloadDto
  {
    public string mail_siigo { get; set; }
    public string tenant_id { get; set; }
    public string cloud_tenant_company_key { get; set; }
    public long users_id { get; set; }
    public long nbf { get; set; }
    public long exp { get; set; }
    public long iat { get; set; }
  }
}