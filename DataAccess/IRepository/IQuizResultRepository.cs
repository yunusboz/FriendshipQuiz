using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IQuizResultRepository : IGenericRepository<QuizResult>
    {
        IQueryable<QuizResult> GetAllQuizResults(bool trackChanges, string includeProperties = "");
        QuizResult? GetOneQuizResult(int id, bool trackChanges, string includeProperties = "");
        void CreateOneQuizResult(QuizResult quizResult);
    }
}
