using ATI.Services.Common.Behaviors.OperationBuilder.Extensions;
using ATI.Services.Common.Swagger;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ThousandWords.Core.Interfaces.DbContexts;
using ThousandWords.Core.Models.DTO;
using ThousandWords.WebApi.Attributes;
using ThousandWords.WebApi.Services;

namespace ThousandWords.WebApi.Controllers;

[Route("auth")]
public class AuthController : ControllerWithOpenApi
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    [CapAuthorize]
    public IActionResult GetAuth()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AuthUser([FromBody] UserDto userDto)
    {
        var authOperation = await _authService.AuthUser(userDto);
        if (!authOperation.Success)
            return authOperation.AsActionResult();
        
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, authOperation.Value);
        return Ok();
    }
}