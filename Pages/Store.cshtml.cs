using Floppy.Managers.Stories;
using Floppy.Managers.Users;
using Floppy.Managers.Words;
using Floppy.Models.StoryModels;
using Floppy.Models.WordModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Floppy.Pages
{
    public class StoreModel : PageModel
    {
        public IEnumerable<Story> Stories { get; set; }
        public IEnumerable<WordSet> Wordsets { get; set; }
        public int Balance { get; set; }
        public bool IsStories { get; set; }

        private readonly IStoryManager _storyManager;
        private readonly IUserManager _userManager;
        private readonly IWordManager _wordManager;

        public StoreModel(IStoryManager storyManager,IUserManager userManager, IWordManager wordManager)
        {
            _storyManager = storyManager;
            _userManager = userManager;
            _wordManager = wordManager;
        }

        public async Task OnGetStoriesAsync()
        {
          IsStories = true;
          Balance = await _userManager.GetBalanceAsync(User.Identity.Name);
          Stories = await _storyManager.GetCurrentStoriesAsync(User.Identity.Name);
        }

        public async Task OnGetWordsAsync()
        {
            IsStories = false;
            Balance = await _userManager.GetBalanceAsync(User.Identity.Name);
            Wordsets = await _wordManager.GetCurrentWordSetsAsync(User.Identity.Name);
        }

        public async Task<IActionResult> OnPostStoryAsync(int id)
        {
            await _storyManager.BuyStoryAsync(User.Identity.Name, id);
            HttpContext.Session.Remove("Money");
            HttpContext.Session.SetString("Money", (await _userManager.GetBalanceAsync(User.Identity.Name)).ToString());
            return RedirectToPage("Store","Stories",null);
        }

        public async Task<IActionResult> OnPostWordSetAsync(int id)
        {
            await _wordManager.BuyWordSetAsync(User.Identity.Name, id);
            HttpContext.Session.Remove("Money");
            HttpContext.Session.SetString("Money", (await _userManager.GetBalanceAsync(User.Identity.Name)).ToString());
            return RedirectToPage("Store", "Words", null);
        }
    }
}
