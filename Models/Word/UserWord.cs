using Floppy.Models.UserModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Floppy.Models.WordModels
{
    public class UserWord
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int WordId { get; set; }
        public Word Word { get; set; }
        public bool Learned { get; set; }       
    }
}
