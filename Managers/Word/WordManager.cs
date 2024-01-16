using Floppy.Models;
using Floppy.Models.UserModels;
using Floppy.Models.WordModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Floppy.Managers.Words
{
    public class WordManager:IWordManager
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;
        public WordManager(UserManager<User> userManager, ApplicationContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IEnumerable<Word>> GetCurrentWordsAsync(string username)
        {
           var user = await _userManager.FindByNameAsync(username);
           return _context.UserWords.Where(w=>w.UserId==user.Id).Where(w=>w.Learned==false).Select(w=>w.Word);
        }

        public async Task<IEnumerable<Word>> GetLearnedWordsAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return _context.UserWords.Where(w => w.UserId == user.Id).Where(w => w.Learned == true).Select(w => w.Word);
        }

        public async Task<IEnumerable<WordSet>> GetPurcharedWordSetsAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return _context.UserWordSets.Where(w => w.UserId == user.Id).Where(w => w.Purchared == true).Select(w => w.WordSet);
        }

        public async Task<IEnumerable<WordSet>> GetCurrentWordSetsAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return _context.UserWordSets.Where(w => w.UserId == user.Id).Where(w => w.Purchared == false).Select(w => w.WordSet);
        }

        public async Task LearnWordAsync(string username, int id)
        {
            var user = await _userManager.FindByNameAsync(username);
            var word = _context.UserWords.FirstOrDefault(w => w.UserId == user.Id && w.WordId == id);
            word.Learned = true;
            await _context.SaveChangesAsync();
        }

        public async Task BuyWordSetAsync(string username, int id)
        {
            var userId = (await _userManager.FindByNameAsync(username)).Id;
            var user = _context.Users.Include(u=>u.UserWords).FirstOrDefault(u => u.Id == userId);
            var price = (await _context.WordSets.FindAsync(id)).Price;
            if(user.Money>=price)
            {
                _context.UserWordSets.FirstOrDefault(w => w.UserId == user.Id && w.WordSetId == id).Purchared = true;
                var wordset = await _context.WordSets.Include(w => w.Words).FirstOrDefaultAsync(w => w.Id == id);
                var words = wordset.Words;
                user.Money -= price.Value;

                foreach (var word in words)
                {
                    user.UserWords.Add(new UserWord {User = user,UserId=user.Id,Word=word,WordId=word.Id,Learned=false});
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetCountWordsAsync()
        {
            return await _context.Words.CountAsync();
        }
    }
}
