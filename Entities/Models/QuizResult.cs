using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class QuizResult
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CorrectAnswer { get; set; }
        public int? WrongAnswer { get; set; }

        public Quiz Quiz { get; set; }
        public Guid? QuizId { get; set; }

    }
}
