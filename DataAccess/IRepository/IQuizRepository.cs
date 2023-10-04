using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IQuizRepository : IGenericRepository<Quiz>
    {
        IQueryable<Quiz> GetAllQuizzes(bool trackChanges, string includeProperties = "");
        Quiz? GetOneQuiz(Guid id, bool trackChanges, string includeProperties = "");
        void CreateOneQuiz(Quiz quiz);
        void DeleteOneQuiz(Quiz quiz);
        void UpdateOneQuiz(Quiz quiz);
    }
}
