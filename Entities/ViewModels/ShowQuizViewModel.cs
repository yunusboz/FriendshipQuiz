using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class ShowQuizViewModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [DisplayName("Quiz Adı")]
        public string Name { get; set; }
        [Required]
        [DisplayName("İsim")]
        public string CreatedBy { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
        public QuizResult QuizResult { get; set; }
    }
}
