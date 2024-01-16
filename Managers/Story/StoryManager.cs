using Floppy.Models;
using Floppy.Models.StoryModels;
using Floppy.Models.UserModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Floppy.Managers.Stories
{
    public class StoryManager:IStoryManager
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;
        public StoryManager(UserManager<User> userManager, ApplicationContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task BuyStoryAsync(string username, int id)
        {
            var user = await _userManager.FindByNameAsync(username);
            var story = _context.UserStories.FirstOrDefault(s => s.UserId == user.Id && s.StoryId == id);
            var price = (await _context.Stories.FindAsync(id)).Price;
            if(user.Money >= price)
            {
                story.Purchared = true;
                user.Money -= price;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Story>> GetCurrentStoriesAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return _context.UserStories.Where(s => s.UserId == user.Id).Where(s => s.Purchared == false).Select(s => s.Story);
            
        }

        public async Task<IEnumerable<Story>> GetPurcharedStoriesAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return _context.UserStories.Where(s => s.UserId == user.Id).Where(s => s.Purchared == true).Select(s => s.Story);
        }
        public async Task<Story> GetStoryAsync(int id)
        {
            return await _context.Stories.FindAsync(id);
        }
    }
}
