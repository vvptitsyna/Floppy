using Floppy.Models.UserModels;
using System.Collections.Generic;

namespace Floppy.Models.StoryModels
{
    public class Story
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tale { get; set; }
        public string Translation { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public List<UserStory> UserStories { get; set; }
    }
}
