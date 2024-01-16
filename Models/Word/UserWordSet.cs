using Floppy.Models.UserModels;
using Floppy.Models.WordModels;

namespace Floppy.Models.WordModels
{
    public class UserWordSet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int WordSetId { get; set; }
        public WordSet WordSet { get; set; }
        public bool Purchared { get; set; }
    }
}
