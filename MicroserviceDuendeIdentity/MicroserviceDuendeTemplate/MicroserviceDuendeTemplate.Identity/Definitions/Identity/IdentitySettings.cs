using Duende.IdentityServer.Models;
using Duende.IdentityServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MicroserviceDuendeTemplate.Identity.Definitions.Identity.Model;

namespace MicroserviceDuendeTemplate.Identity.Definitions.Identity
{
    public static class IdentitySettings
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
                new IdentityResource()
                {
                    Name = ScopeTypes.Roles,
                    DisplayName = "Your user roles",
                    Required = true,
                    UserClaims = new List<string>() { System.Security.Claims.ClaimTypes.Role }
                },
                new IdentityResource()
                {
                    Name = ScopeTypes.SecurityStamp,
                    DisplayName = "Your security stamp",
                    Required = true,
                    UserClaims = new List<string>() { ClaimTypes.SecurityStamp }
                },
                new IdentityResource()
                {
                    Name = ScopeTypes.UserId,
                    DisplayName = "Your user id",
                    Required = true,
                    UserClaims = new List<string>() { ClaimTypes.UserId }
                },
                new IdentityResource()
                {
                    Name = ScopeTypes.UserName,
                    DisplayName = "Your user id",
                    Required = true,
                    UserClaims = new List<string>() { ClaimTypes.UserName }
                },
                new IdentityResource()
                {
                    Name = ScopeTypes.Policies,
                    DisplayName = "Your user policies",
                    Required = true,
                    UserClaims = new List<string>() { ClaimTypes.UserPolicy }
                },
            };
        }

        public static IEnumerable<Client> GetClients(IEnumerable<ClientIdentity> clients)
        {
            var identityClients = new List<Client>();
            foreach (var configClient in clients)
            {
                if (configClient.Type == JwtBearerDefaults.AuthenticationScheme)
                {
                    identityClients.Add(GetPasswordClient(configClient));
                    continue;
                }

                identityClients.Add(GetCredentialClient(configClient));
            }

            return identityClients;
        }

        public static IEnumerable<ApiResource> GetApiResources(List<IdentityScopeOption> scopes)
        {
            var result = new List<ApiResource>();

            foreach (var scope in scopes)
            {
                result.Add(new ApiResource(scope.Name, scope.Description));
            }

            return result;
        }
        
        public static IEnumerable<ApiScope> GetApiScopes(List<IdentityScopeOption> scopes)
        {
            var result = new List<ApiScope>();

            foreach (var scope in scopes)
            {
                result.Add(new ApiScope(scope.Name, scope.Description));
            }

            return result;
        }

        private static Client GetCredentialClient(ClientIdentity configClient) => new Client()
        {
            ClientName = configClient.Name,
            ClientId = configClient.Id,
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets =
            {
                new Secret(configClient.Secret.Sha256())
            },
            AlwaysIncludeUserClaimsInIdToken = true,
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                ScopeTypes.Roles,
                ScopeTypes.SecurityStamp,
                ScopeTypes.Policies,
            }
        };

        private static Client GetPasswordClient(ClientIdentity configClient) => new Client()
        {
            ClientName = configClient.Name,
            ClientId = configClient.Id,
            ClientSecrets =
            {
                new Secret(configClient.Secret.Sha256())
            },
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            AlwaysIncludeUserClaimsInIdToken = true,
            
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                ScopeTypes.SecurityStamp,
                ScopeTypes.Policies,
                ScopeTypes.Roles,
                ScopeTypes.UserId
            },
            AllowOfflineAccess = true,
            AccessTokenLifetime = (int)(new TimeSpan(100, 0, 0, 0).TotalSeconds),
            AuthorizationCodeLifetime = 1440,
            AbsoluteRefreshTokenLifetime = (int)(new TimeSpan(100, 0, 0, 0).TotalSeconds),
            SlidingRefreshTokenLifetime = 1296000,
            RefreshTokenExpiration = TokenExpiration.Sliding,
            RefreshTokenUsage = TokenUsage.OneTimeOnly,
        };
    }
}
