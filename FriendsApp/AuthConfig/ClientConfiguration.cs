using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsApi.AuthConfig
{
    public class ClientConfiguration
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            yield return new ApiResource
            {
                Name = "FriendsApi",
                DisplayName = "Friends Api",
                Scopes = new[]
                {
                  "FriendsApi"
                }
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "FriendsApi",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials ,
                    RequireClientSecret = false,
                    AllowOfflineAccess=true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes = new[] { "FriendsApi" },
                    ClientName = "Friends",
                    ClientSecrets = new[] { new Secret("5Aue2ks34fj".Sha256()) },
                    AccessTokenLifetime = 3600*24*365*10 
                }
            };
        }
    }
}
