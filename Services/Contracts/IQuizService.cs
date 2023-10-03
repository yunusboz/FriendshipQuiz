using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IQuizService
    {
        IEnumerable<Quiz> GetAllQuiz(bool trackChanges, string includeProperties = "");
        Quiz? GetOneQuiz(Guid id, bool trackChanges, string includeProperties = "");
        void CreateOneQuiz(Quiz quiz);
        void UpdateOneQuiz(Quiz quiz);
        void DeleteOneQuiz(Guid id);
        void DecreaseVisitLimitByOne(Guid id);
    }
}
