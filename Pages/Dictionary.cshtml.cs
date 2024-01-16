using Floppy.Managers.Words;
using Floppy.Models.WordModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Floppy.Pages
{
    public class DictionaryModel : PageModel
    {

        public IEnumerable<Word> Words { get; set; }
        private readonly IWordManager _wordManager;
        public DictionaryModel(IWordManager wordManager)
        {
            _wordManager = wordManager;
        }

        public async Task OnGetCurrentAsync()
        {
            ViewData["Learned"] = false;
            ViewData["LearnCount"] = (await _wordManager.GetLearnedWordsAsync(User.Identity.Name)).Count();
            Words = await _wordManager.GetCurrentWordsAsync(User.Identity.Name);
            ViewData["Count"] = Words.Count();
        }

        public async Task OnGetLearnedAsync()
        {
            ViewData["Learned"] = true;
            ViewData["Count"] =  (await _wordManager.GetCurrentWordsAsync(User.Identity.Name)).Count();
            Words = await _wordManager.GetLearnedWordsAsync(User.Identity.Name);
            ViewData["LearnCount"] = Words.Count();
        }

        public async Task<IActionResult> OnPost(int wordId)
        {
            await _wordManager.LearnWordAsync(User.Identity.Name, wordId);
            return RedirectToPage("Dictionary","Current",null);
        }
    }
}
