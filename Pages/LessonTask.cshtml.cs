using Floppy.Managers.Users;
using Floppy.Models.UserModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Floppy.Pages
{
    public class LessonTaskModel : PageModel
    {
        public Progress Progress { get; set; }
        public int Id { get; set; }
        public int Current { get; set; }
        private readonly IUserManager _userManager;
        public LessonTaskModel(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Current = await _userManager.GetCurrentLessonAsync(User.Identity.Name);
            if (id > Current || id<1)
                return RedirectToPage("Lessons");
            Progress = await _userManager.GetProgressAsync(User.Identity.Name);
            Id = id;
            return Page();
        }

    }
}
