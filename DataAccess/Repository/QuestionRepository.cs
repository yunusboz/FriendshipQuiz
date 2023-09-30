using DataAccess.Contexts;
using DataAccess.IRepository;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private BaseDbContext _dbContext;
        public QuestionRepository(BaseDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Question> GetQuestionsById(Expression<Func<Question, bool>> expression)
        {
            return _dbContext.Questions.Where(expression);
        }

        public void Update(Question question)
        {
            _dbContext.Questions.Update(question);
        }
    }
}
