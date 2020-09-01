using System.Collections.Generic;
using Auto.IdentityServer4Admin.Admin.Configuration.Identity;

namespace Auto.IdentityServer4Admin.Admin.Configuration.IdentityServer
{
    public class Client : global::IdentityServer4.Models.Client
    {
        public List<Claim> ClientClaims { get; set; } = new List<Claim>();
    }
}






