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
    public class QuestionManager : IQuestionService
    {
        private readonly IRepositoryManager _manager;

        public QuestionManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public void CreateOneQuestion(Question question)
        {
            _manager.Question.Add(question);
            _manager.Save();
        }

        public void DeleteOneQuestion(int id)
        {
            Question? question = _manager.Question.GetOneQuestion(id, false);
            if(question is not null)
            {
                _manager.Question.Remove(question);
                _manager.Save();
            }
        }

        public IEnumerable<Question> GetAllQuestions(bool trackChanges, string includeProperties = "")
        {
            return _manager.Question.GetAllQuestions(trackChanges, includeProperties);
        }

        public Question? GetOneQuestion(int id, bool trackChanges, string includeProperties = "")
        {
            Question? question = _manager.Question.GetOneQuestion(id, false);
            if (question is null)
                throw new Exception($"Question with {id} not found");

            return question;
        }

        public void UpdateOneQuestion(Question question)
        {
            _manager.Question.Update(question);
            _manager.Save();
        }
    }
}
