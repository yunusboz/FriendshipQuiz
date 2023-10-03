using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Entities.ViewModels
{
    public class ShowQuizViewModel
    {
        public Guid QuizId { get; set; }

        [ValidateNever]
        [DisplayName("Anket Adı")]
        public string SurveyName { get; set; }

        [ValidateNever]
        public string CreatedBy { get; set; }

        [Required(ErrorMessage = "{0} alanı zorunlu.")]
        [Display(Name = "Ad")]
        [StringLength(30, ErrorMessage = "{0} en az {2} karakter ve en fazla {1} karakter uzunluğunda olabilir", MinimumLength = 3)]
        public string Name { get; set; }

        [ValidateNever]
        public List<Question> Questions { get; set; }

        public List<int> SelectedOptions { get; set; } = new List<int>() { 0, 1, 2, 3 };
    }
}
