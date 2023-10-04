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
            ViewBag.Answers = GetAnswers();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] CreateQuizViewModel quizVM)
        {
            Question question1 = _manager.QuestionService.GetOneQuestion(quizVM.questionId1,false);
            Question question2 = _manager.QuestionService.GetOneQuestion(quizVM.questionId2,false);
            Question question3 = _manager.QuestionService.GetOneQuestion(quizVM.questionId3,false);
            Question question4 = _manager.QuestionService.GetOneQuestion(quizVM.questionId4,false);
            question1.QuestionID = 0;
            question1.CorrectAnswer = quizVM.answerId1;
            question2.QuestionID = 0;
            question2.CorrectAnswer = quizVM.answerId2;
            question3.QuestionID = 0;
            question3.CorrectAnswer = quizVM.answerId3;
            question4.QuestionID = 0;
            question4.CorrectAnswer = quizVM.answerId4;
            List<Question> questions = new List<Question> { question1, question2, question3, question4 };
            if (ModelState.IsValid)
            {
                Quiz quiz = new Quiz()
                {
                    Name = quizVM.Name,
                    CreatedBy = quizVM.CreatedBy,
                    Questions = questions
                };
                _manager.QuizService.CreateOneQuiz(quiz);
                return RedirectToAction(nameof(ShowLink), new { id =  quiz.QuizID.ToString()});
            }
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

        private List<SelectListItem> GetAnswers(int questionId = 1)
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

            var defaultQuestion = new SelectListItem()
            {
                Value = "",
                Text = "Soru Seçin"
            };

            questionsList.Insert(0,defaultQuestion);

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
