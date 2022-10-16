using ATI.Services.Common.Behaviors.OperationBuilder.Extensions;
using Microsoft.AspNetCore.Mvc;
using ThousandWords.Core.Interfaces.DbContexts;
using ThousandWords.Core.Models.DTO;
using ThousandWords.WebApi.Attributes;
using ThousandWords.WebApi.Services;

namespace ThousandWords.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IUsersDbContext _usersDbContext;
    private readonly AuthService _authService;

    public AuthController(IUsersDbContext usersDbContext, AuthService authService)
    {
        _usersDbContext = usersDbContext;
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
        return await _authService.AuthUser(HttpContext, userDto).AsActionResultAsync();
    }
}