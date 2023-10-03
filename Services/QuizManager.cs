using DataAccess.IRepository;
using Entities.Models;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class QuizManager : IQuizService
    {
        private readonly IRepositoryManager _manager;

        public QuizManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public void CreateOneQuiz(Quiz quiz)
        {
            _manager.Quiz.Add(quiz);
            _manager.Save();
        }

        public void DecreaseVisitLimitByOne(Guid id)
        {
            Quiz? quiz = _manager.Quiz.GetOneQuiz(id, true);
            if (quiz != null)
            {
                quiz.VisitLimit -= 1;
                _manager.Save();
            }
        }

        public void DeleteOneQuiz(Guid id)
        {
            Quiz? quiz = _manager.Quiz.GetOneQuiz(id, false);
            if (quiz is not null)
            {
                _manager.Quiz.Remove(quiz);
                _manager.Save();
            }
        }

        public IEnumerable<Quiz> GetAllQuiz(bool trackChanges, string includeProperties = "")
        {
            return _manager.Quiz.GetAllQuizzes(trackChanges, includeProperties);
        }

        public Quiz? GetOneQuiz(Guid id, bool trackChanges, string includeProperties = "")
        {
            Quiz? quiz = _manager.Quiz.GetOneQuiz(id, false,includeProperties);
            if (quiz is null)
                throw new Exception($"Quiz with {id} not found");

            return quiz;
        }

        public void UpdateOneQuiz(Quiz quiz)
        {
            _manager.Quiz.Update(quiz);
            _manager.Save();
        }
    }
}
