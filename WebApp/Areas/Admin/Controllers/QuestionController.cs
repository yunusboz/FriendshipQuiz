using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Authorization;
using Utility.Constants;
using Services.Contracts;
using Entities.ViewModels;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class QuestionController : Controller
    {
        private readonly IServiceManager _manager;

        public QuestionController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            var questions = _manager.QuestionService
                .GetAllQuestions(trackChanges: false, includeProperties: "Quiz").ToList();
            return View(questions);
        }

        public IActionResult GetQuestions(Guid id)
        {
            List<Question> quizQuestions = _manager.QuestionService
                .GetAllQuestions(false)
                .Where(q => q.QuizID.Equals(id))
                .ToList();
            TempData["quizId"] = id;
            return View(quizQuestions);
        }

        public IActionResult Create([FromRoute] Guid id)
        {
            return View(new QuestionViewModel() { QuizId = id});
        }

        [HttpPost]
        public IActionResult Create([FromForm] QuestionViewModel qvm)
        {
            Question question = new Question()
            {
                QuizID = qvm.QuizId,
                QuestionText = qvm.QuestionText,
                OptionA = qvm.OptionA,
                OptionB = qvm.OptionB,
                OptionC = qvm.OptionC,
                OptionD = qvm.OptionD,
                OptionE = qvm.OptionE
            };
            _manager.QuestionService.CreateOneQuestion(question);
            return RedirectToAction(nameof(GetQuestions), new { id = question.QuizID });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var questionToBeDeleted = _manager.QuestionService.GetOneQuestion(id, false);
            if(questionToBeDeleted == null)
            {
                return Json(new { success = false, message = "Silerken bir hata oluştu. Tekrar deneyiniz." });
            }

            _manager.QuestionService.DeleteOneQuestion(id);

            return Json(new { success = true, message = "Silme işlemi başarılı." });
        }
    }
}
