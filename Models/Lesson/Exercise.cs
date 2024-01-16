using System.Collections.Generic;

namespace Floppy.Models.LessonModels
{
    public class Exercise
    {
        public int Id { get; set; }
        public List<ExerciseExample> Examples { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
