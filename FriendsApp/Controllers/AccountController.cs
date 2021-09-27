using FriendsApi.Models.Account;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Common.Resources;
using System.Threading.Tasks;
using Common.Helpers;

namespace FriendsApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AccountController : BaseController
    {
        readonly IConfiguration _configuration;
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<LoginResponseModel> Login([FromBody] LoginRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return new LoginResponseModel { UserMessage = nameof(RsStrings.InvalidUser).GetRsValidatorTranslation() };
            }
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(_configuration.GetValue<string>("APIHost").TrimEnd('/') + "/");
            if (disco.IsError)
            {
                return new LoginResponseModel { UserMessage = nameof(RsStrings.InvalidUser).GetRsValidatorTranslation() };
            }
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest { Address = disco.TokenEndpoint, UserName = request.UserName, Password = request.Password, ClientId ="Api" });

            if (tokenResponse.IsError)
            {
                return new LoginResponseModel { UserMessage = nameof(RsStrings.InvalidUser).GetRsValidatorTranslation() };
            }
            return new LoginResponseModel { Success=true, AccessToken = tokenResponse.AccessToken };
        }
    }
}
