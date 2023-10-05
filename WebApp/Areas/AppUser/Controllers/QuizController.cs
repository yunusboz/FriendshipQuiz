using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;
using System.Security.Claims;
using Utility.Constants;

namespace WebApp.Areas.AppUser.Controllers
{
    [Area("AppUser")]
    public class QuizController : Controller
    {
        private readonly IServiceManager _manager;
        

        public QuizController(IServiceManager manager)
        {
            _manager = manager;
        }

        [Authorize(Roles = Roles.User)]
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Quiz> userQuizzes = _manager.QuizService
                .GetAllQuiz(false,"ApplicationUser")
                .Where(q => q.AppUserId == userId)
                .ToList();
            return View(userQuizzes);
        }

        public IActionResult GetQuestionsById(Guid id)
        {
            List<Question> quizQuestions = _manager.QuestionService
                .GetAllQuestions(false)
                .Where(q => q.QuizID.Equals(id))
                .ToList();
            return View(quizQuestions);
        }

        [Authorize(Roles = Roles.User)]
        public IActionResult Create(string name)
        {
            CreateQuizForUserVM vm = new CreateQuizForUserVM()
            {
                Quiz = new Quiz { CreatedBy = name }
            };
            ViewBag.Questions = GetQuestions();
            ViewBag.Answers = GetAnswers(85);
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = Roles.User)]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateQuizForUserVM createQuizForUserVM)
        {                
                List<Question> questions = new List<Question>();

                foreach (var item in createQuizForUserVM.Questions)
                {
                    Question entity =
                        _manager.QuestionService
                        .GetOneQuestion(int.Parse(item.QuestionText), false);
                    Question question = new Question()
                    {
                        QuestionText = entity.QuestionText,
                        OptionA = entity.OptionA,
                        OptionB = entity.OptionB,
                        OptionC = entity.OptionC,
                        OptionD = entity.OptionD,
                        OptionE = entity.OptionE,
                        CorrectAnswer = item.CorrectAnswer
                    };
                    questions.Add(question);
                }
                Quiz quiz = new Quiz()
                {
                    Name = createQuizForUserVM.Quiz.Name,
                    CreatedBy = createQuizForUserVM.Quiz.CreatedBy,
                    Questions = questions,
                    AppUserId = GetAppUserId()
                };
                _manager.QuizService.CreateOneQuiz(quiz);
                return RedirectToAction(nameof(ShowLink), new { id = quiz.QuizID.ToString() });
        }

        [Authorize(Roles = Roles.User)]
        public IActionResult ShowLink(string id)
        {
            ViewData["URL"] = $"{this.Request.Scheme}://{this.Request.Host}/AppUser/Quiz/{nameof(StartQuiz)}/{id}";
            return View();
        }

        public IActionResult StartQuiz(string id)
        {
            if (id != null && id != "")
            {
                var quizEntity = _manager.QuizService.GetOneQuiz(Guid.Parse(id), false, "Questions");
                if (quizEntity is not null)
                {
                    ShowQuizViewModel viewModel = new ShowQuizViewModel
                    {
                        QuizId = Guid.Parse(id),
                        CreatedBy = quizEntity.CreatedBy,
                        SurveyName = quizEntity.Name,
                        Questions = quizEntity.Questions.ToList()
                    };

                    return View(viewModel);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StartQuiz(ShowQuizViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                List<Question> questions = _manager.QuestionService
                    .GetAllQuestions(false, "Quiz")
                    .Where(q => q.QuizID.Equals(viewModel.QuizId))
                    .ToList();
                int correctAnswer = 0;
                for (int i = 0; i < questions.Count; i++)
                {
                    if (questions[i].CorrectAnswer == viewModel.SelectedOptions[i])
                    {
                        correctAnswer++;
                    }
                }
                QuizResult quizResult = new QuizResult()
                {
                    Name = viewModel.Name,
                    CorrectAnswer = correctAnswer,
                    WrongAnswer = questions.Count - correctAnswer,
                    QuizId = viewModel.QuizId
                };
                _manager.QuizResultService.CreateOneQuizResult(quizResult);
                return RedirectToAction(nameof(ShowQuizResult), new { quizResultId = quizResult.Id });
            }
            return View();
        }

        public IActionResult ShowQuizResult(int quizResultId)
        {
            QuizResult? quizResult = _manager.QuizResultService.GetOneQuizResult(quizResultId, false);
            return View(quizResult);
        }

        public JsonResult GetAnswersByQuestion(int questionId)
        {
            List<SelectListItem> answers = GetAnswers(questionId);
            return Json(answers);
        }

        private List<SelectListItem> GetAnswers(int questionId)
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
                .Where(q => q.QuizID == Guid.Parse("9a3eb07f-0246-462b-8065-9c4b77c72363"))
                .ToList();
            return items;
        }

        private string GetAppUserId()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }
    }
}
