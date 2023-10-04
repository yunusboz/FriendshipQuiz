using DataAccess.Contexts;
using DataAccess.IRepository;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class QuizRepository : GenericRepository<Quiz>, IQuizRepository
    {
        public QuizRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }

        public void CreateOneQuiz(Quiz quiz) => Add(quiz);

        public void DeleteOneQuiz(Quiz quiz) => Remove(quiz);

        public IQueryable<Quiz> GetAllQuizzes(bool trackChanges, string includeProperties = "") 
            => GetAll(trackChanges, includeProperties);

        public Quiz? GetOneQuiz(Guid id, bool trackChanges, string includeProperties = "")
        {
            return Get(q => q.QuizID.Equals(id), trackChanges, includeProperties);
        }

        public void UpdateOneQuiz(Quiz quiz) => Update(quiz);
    }
}
