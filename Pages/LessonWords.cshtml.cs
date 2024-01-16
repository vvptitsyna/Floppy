using Floppy.Managers.Lessons;
using Floppy.Managers.Users;
using Floppy.Models.WordModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Floppy.Pages
{
    public class LessonWordsModel : PageModel
    {
        public IEnumerable<Word> Words { get; set; }
        public int LessonId { get; set; }
        public int Count { get; set; }
        private readonly ILessonManager _lessonManager;
        private readonly IUserManager _userManager;
        public LessonWordsModel(ILessonManager lessonManager, IUserManager userManager)
        {
            _lessonManager = lessonManager;
            _userManager = userManager;
        }
        public async Task OnPost(int lessonId)
        {
            Words = await _lessonManager.GetWordsAsync(lessonId);
            LessonId = lessonId;
            Count = Words.Count();
        }

        public async Task<IActionResult> OnPostComplete(int lessonId)
        {
            var current = await _userManager.GetCurrentLessonAsync(User.Identity.Name);
            if (lessonId >  current|| lessonId < 1)
                return RedirectToPage("Lessons");
            if (lessonId == current)
                await _lessonManager.LearnWordsAsync(User.Identity.Name,lessonId);
            return RedirectToPage("LessonTask", new {id=lessonId});
        }
    }
}
