using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        IQueryable<Question> GetAllQuestions(bool trackChanges, string includeProperties = "");
        Question? GetOneQuestion(int id, bool trackChanges, string includeProperties = "");
        void CreateOneQuestion(Question question);
        void DeleteOneQuestion(Question question);
        void UpdateOneQuestion(Question question);
    }
}
