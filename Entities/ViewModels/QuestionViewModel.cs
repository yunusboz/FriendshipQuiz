using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class QuestionViewModel
    {
        public Guid QuizId { get; set; }

        [Required]
        [DisplayName("Soru")]
        public string QuestionText { get; set; }
        [Required]
        [DisplayName("Seçenek A")]
        public string OptionA { get; set; }
        [Required]
        [DisplayName("Seçenek B")]
        public string OptionB { get; set; }
        [Required]
        [DisplayName("Seçenek C")]
        public string OptionC { get; set; }
        [Required]
        [DisplayName("Seçenek D")]
        public string OptionD { get; set; }
        [Required]
        [DisplayName("Seçenek E")]
        public string OptionE { get; set; }
        public int CorrectAnswer { get; set; }
        public int SelectedAnswer { get; set; }

        public QuestionViewModel()
        {
            
        }

        public QuestionViewModel(Question entity)
        {
            QuestionText = entity.QuestionText;
            OptionA = entity.OptionA;
            OptionB = entity.OptionB;
            OptionC = entity.OptionC;
            OptionD = entity.OptionD;
            OptionE = entity.OptionE;
            CorrectAnswer = entity.CorrectAnswer;
        }

    }
}
