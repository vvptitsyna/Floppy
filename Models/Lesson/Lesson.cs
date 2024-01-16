using Floppy.Models.WordModels;

namespace Floppy.Models.LessonModels
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public WordSet WordSet { get; set; }
        public Exercise Exercise { get; set; }
        public Grammar Grammar { get; set; }
    }
}
