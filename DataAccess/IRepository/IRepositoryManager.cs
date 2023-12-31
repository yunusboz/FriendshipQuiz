﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IRepositoryManager
    {
        IQuizRepository Quiz { get; }
        IQuestionRepository Question { get; }
        IQuizResultRepository QuizResult { get; }
        IApplicationUserRepository ApplicationUser { get; }
        void Save();
    }
}
