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
    public class QuizResultRepository : Repository<QuizResult>, IQuizResultRepository
    {
        private BaseDbContext _context;
        public QuizResultRepository(BaseDbContext dbContext) : base(dbContext)
        {
            dbContext = _context;
        }

        public void Update(QuizResult quizResult)
        {
            _context.QuizResults.Update(quizResult);
        }
    }
}
