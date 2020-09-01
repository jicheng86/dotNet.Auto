using Auto.IdentityServer4Admin.Shared.Configuration.Identity;

namespace Auto.IdentityServer4Admin.STS.Identity.Configuration.Interfaces
{
    public interface IRootConfiguration
    {
        AdminConfiguration AdminConfiguration { get; }

        RegisterConfiguration RegisterConfiguration { get; }
    }
}





