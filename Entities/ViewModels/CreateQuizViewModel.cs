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
        [DisplayName("Anket Adı")]
        public string Name { get; set; }
        [Required]
        [DisplayName("İsim")]
        public string CreatedBy { get; set; }
        public IList<Question> Questions { get; set; }
    }
}
