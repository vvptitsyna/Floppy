using Floppy.Models.LessonModels;
using Floppy.Models.WordModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Floppy.Managers.Lessons
{
    public interface ILessonManager
    {
        Task<IEnumerable<Word>> GetWordsAsync(int lessonId);
        Task<IEnumerable<GrammarExample>> GetGrammarsAsync(int lessonId);
        Task<IEnumerable<ExerciseExample>> GetExercisesAsync(int lessonId);
        Task LearnWordsAsync(string username,int lessonid);
        Task LearnGrammarAsync(string username);
        Task AddMoneyAsync(string username,int correctAnswers);
    }
}
