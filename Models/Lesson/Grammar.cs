using System.Collections.Generic;

namespace Floppy.Models.LessonModels
{
    public class Grammar
    {
        public int Id { get; set; }
        public List<GrammarExample> Examples { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
