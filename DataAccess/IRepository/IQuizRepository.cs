using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IQuizRepository : IRepository<Quiz>
    {
        void Update(Quiz quiz);
    }
}
