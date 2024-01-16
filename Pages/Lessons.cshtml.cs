using Floppy.Managers.Users;
using Floppy.Managers.Words;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Floppy.Pages.Lesson
{
    public class LessonsModel : PageModel
    {
        public int CurrentLesson { get; set; }
        public string LessonName { get; set; }
        public int WordCount { get; set; }

        private readonly IUserManager _userManager;
        public LessonsModel(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task OnGet([FromServices] IWordManager wordManager)
        {
            CurrentLesson =  await _userManager.GetCurrentLessonAsync(User.Identity.Name);
            LessonName = await _userManager.GetCurrentLessonNameAsync(User.Identity.Name);
            WordCount = await wordManager.GetCountWordsAsync();
        }
        public async Task<IActionResult> OnGetExitAsync()
        {
           await _userManager.SignOutAsync();
           return RedirectToPage("Index");
        }
    }
}
