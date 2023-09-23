using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Quiz
    {
        public int QuizID { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; } = "Anonim";
        public DateTime CreatedDate { get; set; }
        public ICollection<Question> Questions { get; set; }

        public Quiz()
        {
            Questions = new HashSet<Question>();
            CreatedDate = DateTime.Now;
        }
    }
}
