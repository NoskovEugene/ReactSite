namespace Backend.Common.Configurations.AppConfiguration
{
    public interface IApplicationConfiguration
    {
        CookieConfiguration CookieConfiguration { get; }
        JwtConfiguration JwtConfiguration { get; }
    }
}