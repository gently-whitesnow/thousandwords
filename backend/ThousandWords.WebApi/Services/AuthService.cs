using System.Security.Claims;
using ATI.Services.Common.Behaviors;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ThousandWords.Core.Interfaces.DbContexts;
using ThousandWords.Core.Models;
using ThousandWords.Core.Models.DTO;

namespace ThousandWords.WebApi.Services;

public class AuthService
{
    private readonly IUsersDbContext _usersDbContext;

    public AuthService(IUsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }

    public async Task<OperationResult> AuthUser(HttpContext httpContext, UserDto userDto)
    {
        var userKeyOperation = await GetOrCreateUserKeyAsync(userDto.Email, userDto.LanguageDictionaryName);
        if (userKeyOperation.Success)
        {
            var userKey = userKeyOperation.Value;
            var claimsPrincipal = GetClaimsPrincipal(userKey);
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
            return new OperationResult(ActionStatus.Ok);
        }

        return new OperationResult(userKeyOperation);
    }

    private async Task<OperationResult<string>> GetOrCreateUserKeyAsync(string email, string dictionary)
    {
        var userKey = User.GetKey(email, dictionary);
        var checkKeyOperation = await IsExistsUserKeyAsync(userKey);
        if (checkKeyOperation.Success)
        {
            if (checkKeyOperation.Value)
                return new OperationResult<string>(userKey);

            var createUserOperation = await CreateUserKeyAsync(userKey, dictionary);
            if (createUserOperation.Success)
                return new OperationResult<string>(createUserOperation.Value);
            return new OperationResult<string>(createUserOperation);
        }

        return new OperationResult<string>(checkKeyOperation);
    }

    private async Task<OperationResult<string>> CreateUserKeyAsync(string userKey, string dictionary)
    {
        var user = new User
        {
            Key = userKey,
            LanguageDictionaryName = dictionary,
            CompletedPairs = new HashSet<int>()
        };

        var operation = await _usersDbContext.InsertAsync(user);
        if (operation.Success)
            return new OperationResult<string>(user.GetKey());
        return new OperationResult<string>(operation);
    }

    private Task<OperationResult<bool>> IsExistsUserKeyAsync(string userKey)
    {
        return _usersDbContext.KeyExistsAsync(userKey);
    }

    private ClaimsPrincipal GetClaimsPrincipal(string userKey)
    {
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, userKey)
        };
        var claimsIdentity = new ClaimsIdentity(
            claims, 
            "ApplicationCookie", 
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        return claimsPrincipal;
    }
}