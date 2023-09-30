using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IQuestionRepository : IRepository<Question>
    {
        void Update(Question question);
        IEnumerable<Question> GetQuestionsById(Expression<Func<Question,bool>> expression);
    }
}
