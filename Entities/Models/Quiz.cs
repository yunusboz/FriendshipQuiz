using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Quiz
    {
        public Guid QuizID { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [DisplayName("Anket Adı")]
        public string Name { get; set; }

        [Required]
        [DisplayName("İsim")]
        public string CreatedBy { get; set; } = "Anonim";
        public DateTime CreatedDate { get; set; }
        public int VisitLimit { get; set; } = 5;
        [DisplayName("Sorular")]
        public ICollection<Question> Questions { get; set; }
        public ICollection<QuizResult> QuizResults { get; set; }

        [ValidateNever]
        public string? AppUserId { get; set; }
        [ValidateNever]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public Quiz()
        {
            QuizID = Guid.NewGuid();
            Questions = new HashSet<Question>();
            QuizResults = new HashSet<QuizResult>();
            CreatedDate = DateTime.Now;
        }
    }
}
