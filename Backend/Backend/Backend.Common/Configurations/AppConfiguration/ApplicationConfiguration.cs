using Microsoft.Extensions.Options;

namespace Backend.Common.Configurations.AppConfiguration
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        private IOptions<CookieConfiguration> cookieConfiguration;

        private IOptions<JwtConfiguration> jwtConfiguration;
        
        public ApplicationConfiguration(IOptions<CookieConfiguration> cookieConfig,
                                        IOptions<JwtConfiguration> jwtConfig)
        {
            cookieConfiguration = cookieConfig;
            jwtConfiguration = jwtConfig;
        }

        public CookieConfiguration CookieConfiguration => cookieConfiguration.Value;

        public JwtConfiguration JwtConfiguration => jwtConfiguration.Value;

    }
}