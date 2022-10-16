using ATI.Services.Common.Behaviors;
using ThousandWords.Core.Interfaces.DbContexts;
using ThousandWords.Core.Models;
using ThousandWords.Core.Models.DTO;
using ThousandWords.Core.Services.GetWords;

namespace ThousandWords.Core.Services.CompleteWord;

public class CompleteWordService
{
    private readonly IUsersDbContext _usersDbContext;

    private readonly GetWordsService _getWordsService;

    public CompleteWordService(IUsersDbContext usersDbContext, GetWordsService getWordsService)
    {
        _usersDbContext = usersDbContext;
        _getWordsService = getWordsService;
    }

    public async Task<OperationResult<WordsResponseDto>> CompleteWordAndGetWordsAsync(string userKey, WordsRequestDto requestDto)
    {
        var userOperation = await _usersDbContext.GetUserByKeyAsync(userKey);
        if (userOperation.Success)
        {
            var user = userOperation.Value;
            var updateOperation = await UpdateUserCompletedPairsAsync(user, requestDto.CompletedWordId);
            if (updateOperation.Success)
            {
                return await _getWordsService.GetWordsAsync(user, requestDto.RequiredWordsCount,
                    requestDto.SessionWordsId);
            }

            return new OperationResult<WordsResponseDto>(updateOperation);
        }

        return new OperationResult<WordsResponseDto>(userOperation);
    }

    private async Task<OperationResult> UpdateUserCompletedPairsAsync(User user, int completedWordId)
    {
        if (user.CompletedPairs.Contains(completedWordId))
            return new OperationResult(ActionStatus.Ok);
        
        user.CompletedPairs.Add(completedWordId);
        var operation = await _usersDbContext.InsertAsync(user);
        if (operation.Success == false)
            user.CompletedPairs.Remove(completedWordId);
        return operation;
    }
}