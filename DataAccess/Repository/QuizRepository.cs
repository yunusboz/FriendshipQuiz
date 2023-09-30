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
    public class QuizRepository : Repository<Quiz>, IQuizRepository
    {
        private BaseDbContext _context;
        public QuizRepository(BaseDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public void Update(Quiz quiz)
        {
            _context.Quizzes.Update(quiz);
        }
    }
}
