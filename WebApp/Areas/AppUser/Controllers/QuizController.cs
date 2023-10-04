using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;
using Utility.Constants;

namespace WebApp.Areas.AppUser.Controllers
{
    [Area("AppUser")]
    [Authorize(Roles = Roles.User)]
    public class QuizController : Controller
    {
        private readonly IServiceManager _manager;

        public QuizController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(string name)
        {
            CreateQuizForUserVM vm = new CreateQuizForUserVM()
            {
                Quiz = new Quiz { CreatedBy = name }
            };
            ViewBag.Questions = GetQuestions();
            ViewBag.Answers = GetAnswers();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(CreateQuizForUserVM createQuizForUserVM)
        {
            return View();
        }

        public JsonResult GetAnswersByQuestion(int questionId)
        {
            List<SelectListItem> answers = GetAnswers(questionId);
            return Json(answers);
        }

        private List<SelectListItem> GetAnswers(int questionId = 1)
        {

            var question = _manager.QuestionService.GetOneQuestion(questionId, false);
            var answerLists = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "1", Text =  question.OptionA },
                new SelectListItem() { Value = "2", Text =  question.OptionB },
                new SelectListItem() { Value = "3", Text =  question.OptionC },
                new SelectListItem() { Value = "4", Text =  question.OptionD },
                new SelectListItem() { Value = "5", Text =  question.OptionE }
            };
            return answerLists;
        }

        private List<SelectListItem> GetQuestions()
        {
            var questionsList = new List<SelectListItem>();
            List<Question> questions = new();
            questions = GetQuestionPool();

            questionsList = questions.Select(q => new SelectListItem()
            {
                Value = q.QuestionID.ToString(),
                Text = q.QuestionText
            }).ToList();
            return questionsList;
        }

        private List<Question> GetQuestionPool()
        {
            var items = _manager.QuestionService.GetAllQuestions(false, "Quiz")
                .Where(q => q.QuizID == Guid.Parse("EA8B887F-042D-4C56-A034-68845AA34099"))
                .ToList();
            return items;
        }
    }
}
