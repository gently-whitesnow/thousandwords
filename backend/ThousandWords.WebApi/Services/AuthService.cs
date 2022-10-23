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

    public async Task<OperationResult<ClaimsPrincipal>> AuthUser(UserDto userDto)
    {
        var userKeyOperation = await GetOrCreateUserKeyAsync(userDto.Email, userDto.LanguageDictionaryName);
        return userKeyOperation.Success
            ? new OperationResult<ClaimsPrincipal>(GetClaimsPrincipal(userKeyOperation.Value))
            : new OperationResult<ClaimsPrincipal>(userKeyOperation);
    }

    private async Task<OperationResult<string>> GetOrCreateUserKeyAsync(string email, string dictionary)
    {
        var userKey = User.GetKey(dictionary, email);
        var checkKeyOperation = await IsExistsUserKeyAsync(userKey);
        if (!checkKeyOperation.Success) 
            return new OperationResult<string>(checkKeyOperation);
        
        if (checkKeyOperation.Value)
            return new OperationResult<string>(userKey);

        var createUserOperation = await CreateUserKeyAsync(userKey, dictionary);
        return createUserOperation.Success
            ? new OperationResult<string>(createUserOperation.Value)
            : new OperationResult<string>(createUserOperation);
    }

    private async Task<OperationResult<string>> CreateUserKeyAsync(string userKey, string dictionary)
    {
        var user = new User
        {
            Key = userKey,
            LanguageDictionaryName = dictionary,
            CompletedPairs = new HashSet<int>()
        };

        var insertOperation = await _usersDbContext.InsertAsync(user);

        return insertOperation.Success
            ? new OperationResult<string>(user.GetKey())
            : new OperationResult<string>(insertOperation);
    }

    private Task<OperationResult<bool>> IsExistsUserKeyAsync(string userKey)
    {
        return _usersDbContext.KeyExistsAsync(userKey);
    }

    private static ClaimsPrincipal GetClaimsPrincipal(string userKey)
    {
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, userKey)
        };
        return new ClaimsPrincipal(new ClaimsIdentity(
            claims,
            "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType));
    }
}