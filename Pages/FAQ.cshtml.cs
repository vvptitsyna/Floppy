using Floppy.Managers.Users;
using Floppy.Models.UserModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Floppy.Pages
{
    public class ProfileModel : PageModel
    {
       
        private readonly IUserManager _userManager;
        public ProfileModel(IUserManager userManager)
        {
            _userManager = userManager;
        }
        public void OnGet()
        {
        }
    }
}
