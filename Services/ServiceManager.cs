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
        public ServiceManager(
            IQuizService quizService,
            IQuestionService questionService,
            IQuizResultService quizResultService)
        {
            _quizService = quizService;
            _questionService = questionService;
            _quizResultService = quizResultService;
        }

        public IQuizService QuizService => _quizService;

        public IQuestionService QuestionService => _questionService;

        public IQuizResultService QuizResultService => _quizResultService;
    }
}
