using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IQuestionService
    {
        IEnumerable<Question> GetAllQuestions(bool trackChanges, string includeProperties = "");
        Question? GetOneQuestion(int id, bool trackChanges, string includeProperties = "");
        List<string> GetQuestionAnswersById(int questionId);
        void CreateOneQuestion(Question question);
        void UpdateOneQuestion(Question question);
        void DeleteOneQuestion(int id);
    }
}
