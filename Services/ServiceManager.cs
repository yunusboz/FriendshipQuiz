using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IQuizService _quizService;
        private readonly IQuestionService _questionService;
        private readonly IQuizResultService _quizResultService;
        private readonly IApplicationUserService _applicationUserService;
        public ServiceManager(
            IQuizService quizService,
            IQuestionService questionService,
            IQuizResultService quizResultService,
            IApplicationUserService applicationUserService)
        {
            _quizService = quizService;
            _questionService = questionService;
            _quizResultService = quizResultService;
            _applicationUserService = applicationUserService;
        }

        public IQuizService QuizService => _quizService;

        public IQuestionService QuestionService => _questionService;

        public IQuizResultService QuizResultService => _quizResultService;

        public IApplicationUserService ApplicationUserService => _applicationUserService;
    }
}
