﻿using Duende.IdentityServer.Models;

namespace Planthor.IdentityServerAspNetIdentity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        ];

    public static IEnumerable<ApiScope> ApiScopes =>
        [
            new ApiScope("scope1"),
            new ApiScope("scope2"),
        ];

    public static IEnumerable<Client> Clients =>
        [
            // m2m client credentials flow client
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { "scope1" },
            },

            new Client
            {
                ClientId = "planthor-web",
                ClientName = "Planthor Web Client",
                ClientSecrets = { new Secret("Planthor@123".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,

                // TODO: Trung: find a way to dynamically seed these in db and configurable in IDP.
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "scope2" },
            },
        ];
}
