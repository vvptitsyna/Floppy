using Floppy.Models.UserModels;
using Floppy.Models.WordModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Floppy.Managers.Users
{
    public interface IUserManager
    {
        Task<SignResult> RegisterAsync(RegisterViewModel model);
        Task<SignResult> SignInAsync(LoginViewModel model);
        Task<int> GetBalanceAsync(string username);
        Task<Progress> GetProgressAsync(string username);
        Task<int> GetCurrentLessonAsync(string username);
        Task<string> GetCurrentLessonNameAsync(string username);
        Task SignOutAsync();

    }
}
