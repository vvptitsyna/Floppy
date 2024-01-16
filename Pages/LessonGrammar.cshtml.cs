using Floppy.Managers.Lessons;
using Floppy.Managers.Users;
using Floppy.Models.LessonModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Floppy.Pages
{
    public class LessonGrammarModel : PageModel
    {
        public IEnumerable<GrammarExample> Grammars { get; set; }
        public int LessonId { get; set; }
        public int Count { get; set; }
        private readonly ILessonManager _lessonManager;
        private readonly IUserManager _userManager;
        public LessonGrammarModel(ILessonManager lessonManager, IUserManager userManager)
        {
            _lessonManager = lessonManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnPost(int lessonId)
        {
            var progress = await _userManager.GetProgressAsync(User.Identity.Name);
            var current = await _userManager.GetCurrentLessonAsync(User.Identity.Name);

            if(lessonId==current)
                if (!progress.WordsComplete)
                    return RedirectToPage("Lessons");

            Grammars = await _lessonManager.GetGrammarsAsync(lessonId);
            LessonId = lessonId;
            Count = Grammars.Count();
            return Page();
        }

        public async Task<IActionResult> OnPostComplete(int lessonId)
        {
            var current = await _userManager.GetCurrentLessonAsync(User.Identity.Name);
            if (lessonId > current || lessonId < 1)
                return RedirectToPage("Lessons");
            if (lessonId == current)
                await _lessonManager.LearnGrammarAsync(User.Identity.Name);
            return RedirectToPage("LessonTask", new { id = lessonId });
        }
    }
}
