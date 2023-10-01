using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IQuizResultService
    {
        IEnumerable<QuizResult> GetAllQuizResults(bool trackChanges, string includeProperties = "");
        QuizResult? GetOneQuizResult(int id, bool trackChanges, string includeProperties = "");
        void CreateOneQuizResult(QuizResult quizResult);
    }
}
