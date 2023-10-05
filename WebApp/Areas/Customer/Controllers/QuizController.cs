    using DataAccess.IRepository;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Services.Contracts;
using System.Text.Json.Nodes;

namespace WebApp.Areas.Customer.Controllers
{
    [Area("Customer")]
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

        public IActionResult ShowLink(string id)
        {
            ViewData["URL"] = $"{this.Request.Scheme}://{this.Request.Host}/Customer/Quiz/{nameof(StartQuiz)}/{id}";
            Quiz quiz = _manager.QuizService.GetOneQuiz(Guid.Parse(id), false, "QuizResults");
            List<QuizResult> quizResult = quiz.QuizResults.ToList();
            return View(quizResult);
        }

        public IActionResult StartQuiz(string id)
        {
            if(id != null && id != "")
            {
                var quizEntity = _manager.QuizService.GetOneQuiz(Guid.Parse(id), false, "Questions");
                if(quizEntity is not null && quizEntity.VisitLimit != 0)
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
                    return RedirectToAction(nameof(QuatoExceed));
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
                for(int i = 0; i < questions.Count; i++)
                {
                    if (questions.Select(q => q.CorrectAnswer).ElementAt(i) == viewModel.SelectedOptions[i])
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
                _manager.QuizService.DecreaseVisitLimitByOne(viewModel.QuizId);
                return RedirectToAction(nameof(ShowQuizResult), new {quizResultId = quizResult.Id});
            }
            return View();
        }

        public IActionResult ShowQuizResult(int quizResultId)
        {
            QuizResult? quizResult = _manager.QuizResultService.GetOneQuizResult(quizResultId, false);
            return View(quizResult);
        }

        [HttpGet]
        public IActionResult Create(string name)
        {
            CreateQuizViewModel vm = new CreateQuizViewModel
            {
                CreatedBy = name,
                Name = ""
            };
            ViewBag.Questions = GetQuestions();
            ViewBag.Answers = GetAnswers(85);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] CreateQuizViewModel quizVM)
        {
            List<Question> questions = new List<Question>();
            foreach (var item in quizVM.Questions)
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
                    Name = quizVM.Name,
                    CreatedBy = quizVM.CreatedBy,
                    Questions = questions
                };
                _manager.QuizService.CreateOneQuiz(quiz);
                return RedirectToAction(nameof(ShowLink), new { id =  quiz.QuizID.ToString()});
            
            return View();
        }

        public IActionResult QuatoExceed()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAnswersByQuestion(int questionId)
        {
            List<SelectListItem> answers = GetAnswers(questionId);
            return Json(answers);
        }

        private List<SelectListItem> GetAnswers(int questionId)
        {

            var question = _manager.QuestionService.GetOneQuestion(questionId, false);

            //SelectListItem item = new SelectListItem() { Value = "1", Text = question.OptionA };
            var answerLists = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "1", Text =  question.OptionA },
                new SelectListItem() { Value = "2", Text =  question.OptionB },
                new SelectListItem() { Value = "3", Text =  question.OptionC },
                new SelectListItem() { Value = "4", Text =  question.OptionD },
                new SelectListItem() { Value = "5", Text =  question.OptionE }
            };

            //var answerLists = new List<SelectListItem>();
            //answerLists.Add(item);


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
    }
}
