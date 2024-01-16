using Floppy.Managers.Stories;
using Floppy.Models.StoryModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Floppy.Pages
{
    public class StoriesModel : PageModel
    {
        public IEnumerable<Story> Stories { get; set; }
        private readonly IStoryManager _storyManager;
        public StoriesModel(IStoryManager storyManager)
        {
            _storyManager = storyManager;
        }
        public async Task OnGetAsync()
        {
            Stories = await _storyManager.GetPurcharedStoriesAsync(User.Identity.Name);
        }
    }
}
