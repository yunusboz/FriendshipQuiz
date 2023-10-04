﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        IQuizService QuizService { get; }
        IQuestionService QuestionService { get; }
        IQuizResultService QuizResultService { get; }
        IApplicationUserService ApplicationUserService { get; }
    }
}
