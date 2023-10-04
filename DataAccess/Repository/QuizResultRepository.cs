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
    public class QuizResultRepository : GenericRepository<QuizResult>, IQuizResultRepository
    {
        public QuizResultRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }

        public void CreateOneQuizResult(QuizResult quizResult) => Add(quizResult);

        public IQueryable<QuizResult> GetAllQuizResults(bool trackChanges, string includeProperties = "") 
            => GetAll(trackChanges, includeProperties);

        public QuizResult? GetOneQuizResult(int id, bool trackChanges, string includeProperties = "")
        {
            return Get(q => q.Id.Equals(id),trackChanges,includeProperties);
        }
    }
}
