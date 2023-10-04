﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class CreateQuizForUserVM
    {
        public Quiz Quiz { get; set; }
        public IList<Question> Questions { get; set; }
    }
}
