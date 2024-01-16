using Floppy.Managers.Stories;
using Floppy.Models.StoryModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Floppy.Pages
{
    public class ReadStoryModel : PageModel
    {
        public Story Story { get; set; }
        private readonly IStoryManager _storyManager;
        public ReadStoryModel(IStoryManager storyManager)
        {
            _storyManager = storyManager;
        }
        public async Task OnPost(int id)
        {
            Story = await _storyManager.GetStoryAsync(id);
            System.Console.WriteLine();
        }
    }
}
