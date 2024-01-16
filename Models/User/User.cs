using Floppy.Models.LessonModels;
using Floppy.Models.StoryModels;
using Floppy.Models.WordModels;
using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Floppy.Models.UserModels
{
    public class User:IdentityUser<int>
    {
        public int Money { get; set; }
        public int CurrentLesson { get; set; }
        public List<UserWord> UserWords { get; set; }
        public List<UserStory> UserStories { get; set; }
        public List<UserWordSet> UserWordSets { get; set; }
        public Progress Progress { get; set; }
    }
}
