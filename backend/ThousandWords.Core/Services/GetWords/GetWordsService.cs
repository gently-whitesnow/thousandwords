using ATI.Services.Common.Behaviors;
using ThousandWords.Core.Interfaces.DbContexts;
using ThousandWords.Core.Models;
using ThousandWords.Core.Models.DTO;

namespace ThousandWords.Core.Services.GetWords;

public class GetWordsService
{
    private readonly IUsersDbContext _usersDbContext;
    private readonly ILanguageDictionaryInfoDbContext _dictionaryInfoDbContext;
    private readonly ILanguagePairsDbContext _languagePairsDbContext;

    public GetWordsService(
        IUsersDbContext usersDbContext, 
        ILanguageDictionaryInfoDbContext dictionaryInfoDbContext, 
        ILanguagePairsDbContext languagePairsDbContext)
    {
        _usersDbContext = usersDbContext;
        _dictionaryInfoDbContext = dictionaryInfoDbContext;
        _languagePairsDbContext = languagePairsDbContext;
    }

    public Task<OperationResult<WordsResponseDto>> GetWordsAsync(string userKey, int requiredCount) =>
        GetWordsAsync(userKey, requiredCount, new List<int>());

    public async Task<OperationResult<WordsResponseDto>> GetWordsAsync(string userKey, int requiredCount, IEnumerable<int> sessionWords)
    {
        var operation = await GetUserByKeyAsync(userKey);
        if (operation.Success)
        {
            var user = operation.Value;
            return await GetWordsAsync(user, requiredCount, sessionWords);
        }

        return new OperationResult<WordsResponseDto>(operation);
    }

    public async Task<OperationResult<WordsResponseDto>> GetWordsAsync(User user, int requiredCount, IEnumerable<int> sessionWords)
    {
        var dictionaryInfoOperation = await GetDictionaryInfoAsync(user);
        if (dictionaryInfoOperation.Success)
        {
            var dictionaryInfo = dictionaryInfoOperation.Value;
            return await GetResponseDtoAsync(user, dictionaryInfo, sessionWords, requiredCount);
        }

        return new OperationResult<WordsResponseDto>(dictionaryInfoOperation);
    }

    private async Task<OperationResult<WordsResponseDto>> GetResponseDtoAsync(User user,
        LanguageDictionaryInfo dictionaryInfo, IEnumerable<int> excludedWords, int requiredCount)
    {
        var extractor = LanguagePairsExtractorBuilder
            .FromDbContext(_languagePairsDbContext)
            .ForUser(user)
            .WithExcludes(excludedWords)
            .SetLanguageDictionaryInfo(dictionaryInfo)
            .SetRequiredCount(requiredCount)
            .Build();
        var operation = await extractor.ExtractAsync();
        if (operation.Success)
        {
            var pairs = operation.Value;
            var responseDto = new WordsResponseDto
            {
                UserLevel = user.CompletedPairs.Count,
                Words = pairs.Select(MapPairToDto)
            };
            return new OperationResult<WordsResponseDto>(responseDto);
        }

        return new OperationResult<WordsResponseDto>(operation);
    }

    private LanguagePairDto MapPairToDto(LanguagePair pair)
    {
        return new LanguagePairDto
        {
            Id = pair.Id,
            NativeWord = pair.NativeWord,
            ForeignWord = pair.ForeignWord
        };
    }

    private Task<OperationResult<User>> GetUserByKeyAsync(string userKey)
    {
        return _usersDbContext.GetUserByKeyAsync(userKey);
    }

    private Task<OperationResult<LanguageDictionaryInfo>> GetDictionaryInfoAsync(User user)
    {
        var dictionaryName = user.LanguageDictionaryName;
        return _dictionaryInfoDbContext.GetLanguageDictionaryByNameAsync(dictionaryName);
    }
}