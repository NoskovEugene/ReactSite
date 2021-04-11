using System.Text;
using Backend.Common.Configurations.AppConfiguration;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Common.Extensions.ConfigurationExtensions
{
    public static class JwtConfigurationExtensions
    {
        public static SymmetricSecurityKey GetSymmetricSecurityKey(this JwtConfiguration configuration)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.Secret));
        }
    }
}