namespace PropertyApp.Domain.Common;

public class AuthenticationSetting
{
    public string? JwtKey { get; set; }
    public int JwtExpireTime { get; set; }
    public string? JwtIssuer { get; set; }
}
