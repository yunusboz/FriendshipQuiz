﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IUnitOfWork
    {
        IQuizRepository Quiz { get; }
        IQuestionRepository Question { get; }
        IQuizResultRepository QuizResult { get; }
        void Save();
    }
}
