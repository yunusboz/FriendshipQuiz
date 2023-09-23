using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Contexts;

namespace WebApp.Controllers
{
    public class QuizController : Controller
    {
        private readonly BaseDbContext _context;

        public QuizController(BaseDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Quiz> quizzes = _context.Quizzes.Include(q => q.Questions).ToList();
            return View(quizzes);
        }
    }
}
