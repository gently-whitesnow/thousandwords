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

    public async Task<OperationResult<WordsResponse>> CompleteWordAndGetWordsAsync(string userKey,
        WordsRequest request)
    {
        var getUserOperation = await _usersDbContext.GetUserByKeyAsync(userKey);
        if (!getUserOperation.Success)
        {
            return new OperationResult<WordsResponse>(getUserOperation);
        }
        
        var user = getUserOperation.Value;
        var updateOperation = await UpdateUserCompletedPairsAsync(user, request.CompletedWordId);
        if (!updateOperation.Success)
        {
            return new OperationResult<WordsResponse>(updateOperation);
        }

        return await _getWordsService.GetWordsAsync(user, request.RequiredWordsCount,
            request.SessionWordsId);
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