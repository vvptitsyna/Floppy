using Floppy.Models;
using Floppy.Models.StoryModels;
using Floppy.Models.UserModels;
using Floppy.Models.WordModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Floppy.Managers.Users
{
    public class UserManager:IUserManager
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationContext _context;


        public UserManager(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<int> GetBalanceAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                user = await _userManager.FindByEmailAsync(username);
            return user.Money;
        }

        public async Task<int> GetCurrentLessonAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user.CurrentLesson;
        }

        public async Task<string> GetCurrentLessonNameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var lesson = _context.Lessons.Find(user.CurrentLesson);
            if (lesson == null) return "-";
            return lesson.Name;
        }

        public async Task<Progress> GetProgressAsync(string username)
        {
            var id = (await _userManager.FindByNameAsync(username)).Id;
            var user = _context.Users.Include(u=>u.Progress).FirstOrDefault(u=>u.Id==id);
            return user.Progress;
        }

        public async Task<SignResult> RegisterAsync(RegisterViewModel model)
        {
            User user = new User { Email = model.Email, UserName = model.Login,Money = 0,CurrentLesson=1 };
            var userResult = new SignResult();
            if ((await _userManager.FindByEmailAsync(model.Email)) != null)
            {
                userResult.Error = "Пользователь с таким email уже существует";
                userResult.Succeed = false;
            }
            else if ((await _userManager.FindByNameAsync(model.Login)) != null)
            {
                userResult.Error = "Пользователь с таким именем уже существует";
                userResult.Succeed = false;
            }
            else
            {
                userResult.Succeed = (await _userManager.CreateAsync(user, model.Password)).Succeeded;
                user = await _userManager.FindByNameAsync(model.Login);
                var progress = new Progress() { ExerciseComplete = false, GrammarComplete = false, WordsComplete = false, User = user, UserId = user.Id };
                user.Progress = progress;
                await _signInManager.SignInAsync(user, isPersistent: false);

                user = await _context.Users.Include(u => u.UserStories).ThenInclude(s=>s.Story).Include(u=>u.UserWordSets).FirstOrDefaultAsync(u => u.Id == user.Id);
                foreach(var story in _context.Stories)
                {
                    user.UserStories.Add(new UserStory { Purchared = false, Story = story, StoryId = story.Id, User = user, UserId = user.Id });
                }
                foreach (var wordset in _context.WordSets)
                {
                    if(wordset.LessonId==null)
                        user.UserWordSets.Add(new UserWordSet { Purchared = false, WordSet = wordset, WordSetId = wordset.Id, User = user, UserId = user.Id });
                }
                await _context.SaveChangesAsync();
            }
            return userResult;
        }

        public async Task<SignResult> SignInAsync(LoginViewModel model)
        {
            var userResult = new SignResult();
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == model.Login || u.Email == model.Login);

            if(user != null)
                userResult.Succeed = (await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false)).Succeeded;


            if (!userResult.Succeed)
                userResult.Error = "Неправильный логин или пароль";

            return userResult;
        }

        public async Task SignOutAsync()
        {
          await  _signInManager.SignOutAsync();
        }
    }
}
