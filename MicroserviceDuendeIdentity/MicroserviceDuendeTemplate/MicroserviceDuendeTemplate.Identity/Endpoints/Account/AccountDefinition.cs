using Brueder.Architecture.Base.Attributes;
using Brueder.Architecture.Base.Definition;
using IdentityModel.Client;
using MicroserviceDuendeTemplate.DAL.Models.Identity;
using MicroserviceDuendeTemplate.Identity.Definitions.Identity.Model;
using MicroserviceDuendeTemplate.Identity.Endpoints.Account.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MicroserviceDuendeTemplate.Identity.Endpoints.Account;

public class AccountDefinition : Definition
{
    public override void ConfigureApplicationAsync(WebApplication app)
    {
        app.MapPost("/api/account/login", Login).WithOpenApi();
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Account")]
    private static async Task<IResult> Login(
        [FromBody] LoginRequest request,
        UserManager<ApplicationUser> _userManager,
        IOptions<ClientIdentity> _currentIdentityClient,
        IdentityAddressOption _address)
    {
        var user = await _userManager.FindByNameAsync(request.Login);
        var password = request.Password;
        if (user == null)
        {
            return Results.NotFound();
        }

        var clientHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };

        var client = new HttpClient(clientHandler);

        var currentClient = _currentIdentityClient.Value;
        var response = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
        {
            Address = $"{_address.Url}/connect/token",
            ClientId = currentClient.Id,
            ClientSecret = currentClient.Secret,
            UserName = user.UserName,
            Password = password
        });

        if (response.IsError)
        {
            return Results.BadRequest($"{response.Error} {response.ErrorDescription}");
        }

        var result = new IdTokenResponse 
        { 
            RefreshToken = response.RefreshToken, 
            AccessToken = response.AccessToken 
        };

        return Results.Ok(result);
    }
}