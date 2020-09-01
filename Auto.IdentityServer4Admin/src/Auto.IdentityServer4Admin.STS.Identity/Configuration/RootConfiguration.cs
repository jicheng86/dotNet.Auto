using Auto.IdentityServer4Admin.Shared.Configuration.Identity;
using Auto.IdentityServer4Admin.STS.Identity.Configuration.Interfaces;

namespace Auto.IdentityServer4Admin.STS.Identity.Configuration
{
    public class RootConfiguration : IRootConfiguration
    {      
        public AdminConfiguration AdminConfiguration { get; } = new AdminConfiguration();
        public RegisterConfiguration RegisterConfiguration { get; } = new RegisterConfiguration();
    }
}





