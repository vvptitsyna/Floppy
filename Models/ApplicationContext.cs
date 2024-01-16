using Floppy.Models.LessonModels;
using Floppy.Models.StoryModels;
using Floppy.Models.UserModels;
using Floppy.Models.WordModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Floppy.Models
{
    public class ApplicationContext:IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Word> Words { get; set; }
        public DbSet<UserWord> UserWords { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<UserStory> UserStories { get; set; }
        public DbSet<WordSet> WordSets { get; set; }
        public DbSet<UserWordSet> UserWordSets { get; set; }

        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Grammar> Grammars { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
        }
       
    }
}
