using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories.Contexts;

namespace WebApp.Controllers
{
    public class QuestionController : Controller
    {
        private readonly BaseDbContext _context;

        public QuestionController(BaseDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var questions = _context.Questions.Include(a => a.Quiz).ToList();
            return View(questions);
        }

        public IActionResult GetQuestions(int id)
        {
            List<Question> questionsOfQuiz = _context.Questions.Where(q => q.QuizID == id).ToList();
            return View(questionsOfQuiz);
        }
    }
}
