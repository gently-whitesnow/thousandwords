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

    public Task<OperationResult<WordsResponse>> GetWordsAsync(string userKey, int requiredCount) =>
        GetWordsAsync(userKey, requiredCount, new List<int>());

    private async Task<OperationResult<WordsResponse>> GetWordsAsync(string userKey, int requiredCount, IEnumerable<int> sessionWords)
    {
        var operation = await GetUserByKeyAsync(userKey);
        if (!operation.Success) 
            return new OperationResult<WordsResponse>(operation);
        
        return await GetWordsAsync(operation.Value, requiredCount, sessionWords.ToList());
    }

    /// <summary>
    /// Если не получается вернуть слова пользователю, то возвращаем слова, которые он уже знает
    /// Обрабатываем кейс, когда он почти все слова выучил
    /// </summary>
    /// <param name="user"></param>
    /// <param name="requiredCount"></param>
    /// <param name="sessionWords"></param>
    /// <returns></returns>
    public async Task<OperationResult<WordsResponse>> GetWordsAsync(User user, int requiredCount, List<int> sessionWords)
    {
        var getDictionaryInfoOperation = await GetDictionaryInfoAsync(user.LanguageDictionaryName);
        if (!getDictionaryInfoOperation.Success) 
            return new OperationResult<WordsResponse>(getDictionaryInfoOperation);
        
        var getWordsOperation = await GetResponseDtoAsync(user, getDictionaryInfoOperation.Value, sessionWords, requiredCount);
        if (!getWordsOperation.Success)
            return new OperationResult<WordsResponse>(getWordsOperation);
        if (getWordsOperation.Value.Words.Any())
            return getWordsOperation;
        // Представляем что пользователь ничего не выучил, чтобы дать ему доиграть
        user.CompletedPairs = new HashSet<int>();
        return await GetResponseDtoAsync(user, getDictionaryInfoOperation.Value, sessionWords, requiredCount);
    }

    private async Task<OperationResult<WordsResponse>> GetResponseDtoAsync(User user,
        LanguageDictionaryInfo dictionaryInfo, IEnumerable<int> excludedWords, int requiredCount)
    {
        var extractor = LanguagePairsExtractorBuilder
            .FromDbContext(_languagePairsDbContext)
            .ForUser(user)
            .WithExcludes(excludedWords)
            .SetLanguageDictionaryInfo(dictionaryInfo)
            .SetRequiredCount(requiredCount)
            .Build();
        var getWordsOperation = await extractor.ExtractAsync();
        if (!getWordsOperation.Success)
            return new OperationResult<WordsResponse>(getWordsOperation);
        
        return new OperationResult<WordsResponse>(new WordsResponse
        {
            UserLevel = user.CompletedPairs.Count,
            Words = getWordsOperation.Value.Select(MapPairToDto)
        });
    }

    private static LanguagePairDto MapPairToDto(LanguagePair pair)
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

    private Task<OperationResult<LanguageDictionaryInfo>> GetDictionaryInfoAsync(string languageDictionaryName)
    {
        return _dictionaryInfoDbContext.GetLanguageDictionaryByNameAsync(languageDictionaryName);
    }
}