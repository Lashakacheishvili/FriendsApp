using Domain.Models;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Common.Enums.Enums;
using static IdentityModel.OidcConstants;

namespace FriendsApi.OAuth
{
    public class AppResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public AppResourceOwnerPasswordValidator(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _userManager.Users.Include(s => s.Roles).FirstOrDefaultAsync(s => s.UserName == context.UserName && !s.DeleteDate.HasValue && s.UserStatus== UserStatus.Active);
            if (user == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest);
                return;
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, context.Password, false);
            if (result.Succeeded)
            {
                context.Result = new GrantValidationResult(user.Id.ToString(), AuthenticationMethods.Password);
                return;
            }
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "Access Denied");
        }
    }
}
