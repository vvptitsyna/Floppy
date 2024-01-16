using Floppy.Models.UserModels;

namespace Floppy.Models.StoryModels
{
    public class UserStory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int StoryId { get; set; }
        public Story Story { get; set; }
        public bool Purchared { get; set; }
    }
}
