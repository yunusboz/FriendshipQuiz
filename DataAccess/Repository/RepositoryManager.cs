using DataAccess.Contexts;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private BaseDbContext _dbContext;
        private readonly IQuizRepository _quizRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuizResultRepository _quizResultRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;

        public RepositoryManager(
            BaseDbContext dbContext,
            IQuizRepository quizRepository,
            IQuestionRepository questionRepository,
            IQuizResultRepository quizResultRepository,
            IApplicationUserRepository applicationUserRepository)
        {
            _dbContext = dbContext;
            _quizRepository = quizRepository;
            _questionRepository = questionRepository;
            _quizResultRepository = quizResultRepository;
            _applicationUserRepository = applicationUserRepository;
        }

        public IQuizRepository Quiz => _quizRepository;
        public IQuestionRepository Question => _questionRepository;
        public IQuizResultRepository QuizResult => _quizResultRepository;
        public IApplicationUserRepository ApplicationUser => _applicationUserRepository;

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
