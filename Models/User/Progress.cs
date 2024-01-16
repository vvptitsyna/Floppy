namespace Floppy.Models.UserModels
{
    public class Progress
    {
        public int Id { get; set; }
        public bool WordsComplete { get; set; }=false;
        public bool GrammarComplete { get; set; }=false;
        public bool ExerciseComplete { get; set; }=false;
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
