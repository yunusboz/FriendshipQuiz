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
    public class CreateQuizViewModel
    {

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [DisplayName("Quiz Adı")]
        public string Name { get; set; }
        [Required]
        [DisplayName("İsim")]
        public string CreatedBy { get; set; }
        public int questionId1 { get; set; }
        public int questionId2 { get; set; }
        public int questionId3 { get; set; }
        public int questionId4 { get; set; }
        public int answerId1 { get; set; }
        public int answerId2 { get; set; }
        public int answerId3 { get; set; }
        public int answerId4 { get; set; }
    }
}
