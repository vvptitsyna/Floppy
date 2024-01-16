using Floppy.Managers.Users;
using Floppy.Models;
using Floppy.Models.UserModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Floppy.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUserManager _userManager;
        public IndexModel(IUserManager userManager)
        {
           _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpContext.Session.Remove("Money");
                HttpContext.Session.SetString("Money", (await _userManager.GetBalanceAsync(User.Identity.Name)).ToString());
                return RedirectToPage("Lessons");
            }
            else
                return Page();
        }

        public async Task<IActionResult> OnPostRegisterAsync(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _userManager.RegisterAsync(model);
                if (!result.Succeed)
                    ModelState.AddModelError("", result.Error);
                else
                {
                    HttpContext.Session.Remove("Money");
                    HttpContext.Session.SetString("Money", (await _userManager.GetBalanceAsync(model.Login)).ToString());
                    return RedirectToPage("Lessons");
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostLoginAsync(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.SignInAsync(model);
                if (!result.Succeed)
                    ModelState.AddModelError("", result.Error);
                else
                {
                    var name = User.Identity.Name;
                    HttpContext.Session.Remove("Money");
                    HttpContext.Session.SetString("Money", (await _userManager.GetBalanceAsync(model.Login)).ToString());
                    return RedirectToPage("Lessons");
                }
            }
            return Page();
        }
    }
}
