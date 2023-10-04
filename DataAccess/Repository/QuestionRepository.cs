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
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }

        public void CreateOneQuestion(Question question) => Add(question);

        public void DeleteOneQuestion(Question question) => Remove(question);

        public IQueryable<Question> GetAllQuestions(bool trackChanges, string includeProperties = "") 
            => GetAll(trackChanges, includeProperties);

        public Question? GetOneQuestion(int id, bool trackChanges, string includeProperties = "")
        {
            return Get(q => q.QuestionID == id, trackChanges, includeProperties);
        }

        public void UpdateOneQuestion(Question question) => Update(question);
    }
}
