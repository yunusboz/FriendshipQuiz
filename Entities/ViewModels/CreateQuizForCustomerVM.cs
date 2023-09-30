using Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class CreateQuizForCustomerVM
    {
        public Quiz Quiz { get; set; }
        public Question Question { get; set; }
        public List<SelectListItem> QuestionsSelectList { get; set; } = new List<SelectListItem>();
        public string SelectedQuestion { get; set; }
    }
}
