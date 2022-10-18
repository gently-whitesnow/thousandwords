using ATI.Services.Common.Behaviors.OperationBuilder.Extensions;
using ATI.Services.Common.Swagger;
using Microsoft.AspNetCore.Mvc;
using ThousandWords.Core.Models.DTO;
using ThousandWords.Core.Services.CompleteWord;
using ThousandWords.Core.Services.GetWords;
using ThousandWords.WebApi.Attributes;

namespace ThousandWords.WebApi.Controllers;

[ApiController]
[Route("words")]
public class WordsController : ControllerWithOpenApi
{
    private readonly GetWordsService _getWordsService;
    private readonly CompleteWordService _completeWordService;

    public WordsController(GetWordsService getWordsService, CompleteWordService completeWordService)
    {
        _getWordsService = getWordsService;
        _completeWordService = completeWordService;
    }

    [HttpGet]
    [CapAuthorize]
    public async Task<IActionResult> GetWords([FromQuery] int count)
    {
        var userKey = HttpContext.User.Identity.Name;
        return await _getWordsService.GetWordsAsync(userKey, count).AsActionResultAsync();
    }

    [HttpPost]
    [CapAuthorize]
    public async Task<IActionResult> PostWords([FromBody] WordsRequestDto requestDto)
    {
        var userKey = HttpContext.User.Identity.Name;
        return await _completeWordService.CompleteWordAndGetWordsAsync(userKey, requestDto).AsActionResultAsync();
    }
}