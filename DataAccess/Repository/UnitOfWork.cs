using DataAccess.Contexts;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private BaseDbContext _dbContext;
        public IQuizRepository Quiz { get; private set; }

        public IQuestionRepository Question { get; private set; }

        public IQuizResultRepository QuizResult { get; private set; }

        public UnitOfWork(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
            Quiz = new QuizRepository(_dbContext);
            Question = new QuestionRepository(_dbContext);
            QuizResult = new QuizResultRepository(_dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
