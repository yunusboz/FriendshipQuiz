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
    public class QuizResultManager : IQuizResultService
    {
        private readonly IRepositoryManager _manager;

        public QuizResultManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public void CreateOneQuizResult(QuizResult quizResult)
        {
            _manager.QuizResult.CreateOneQuizResult(quizResult);
            _manager.Save();
        }

        public IEnumerable<QuizResult> GetAllQuizResults(bool trackChanges, string includeProperties = "")
        {
            return _manager.QuizResult.GetAllQuizResults(trackChanges, includeProperties);
        }

        public QuizResult? GetOneQuizResult(int id, bool trackChanges, string includeProperties = "")
        {
            QuizResult? quizResult = _manager.QuizResult.GetOneQuizResult(id, trackChanges, includeProperties);
            if (quizResult is null)
                throw new Exception($"Quiz Result with {id} could not found");

            return quizResult;
        }
    }
}
