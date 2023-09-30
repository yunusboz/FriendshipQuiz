using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IQuizResultRepository : IRepository<QuizResult>
    {
        void Update(QuizResult quizResult);
    }
}
