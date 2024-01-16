using Floppy.Models.WordModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Floppy.Managers.Words
{
    public interface IWordManager
    {
        Task<IEnumerable<Word>> GetCurrentWordsAsync(string username);
        Task<IEnumerable<Word>> GetLearnedWordsAsync(string username);
        Task<IEnumerable<WordSet>> GetPurcharedWordSetsAsync(string username);
        Task<IEnumerable<WordSet>> GetCurrentWordSetsAsync(string username);
        Task LearnWordAsync(string username, int id);
        Task BuyWordSetAsync(string username, int id);
        Task<int> GetCountWordsAsync();
    }
}
