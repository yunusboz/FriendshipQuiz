    using DataAccess.IRepository;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class QuizController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuizController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowLink(string id)
        {
            ViewData["URL"] = $"{this.Request.Scheme}://{this.Request.Host}/Customer/Quiz/{nameof(StartQuiz)}/{id}";
            return View();
        }

        public IActionResult StartQuiz(string id)
        {
            if(id != null && id != "")
            {
                var quizEntity = _unitOfWork.Quiz.Get(q => q.QuizID.Equals(Guid.Parse(id)), "Questions");
                if(quizEntity is not null && quizEntity.VisitLimit != 0)
                {
                    var questionsEntity = quizEntity.Questions.ToList();
                    List<QuestionViewModel> qvm = new List<QuestionViewModel>();

                    foreach(var question in questionsEntity)
                    {
                        QuestionViewModel questionViewModel = new QuestionViewModel(question);
                        qvm.Add(questionViewModel);
                    }
                    ShowQuizViewModel viewModel = new ShowQuizViewModel
                    {
                        CreatedBy = quizEntity.CreatedBy,
                        Name = quizEntity.Name,
                        Questions = qvm
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
        public IActionResult StartQuiz(ShowQuizViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Create(string name)
        {
            CreateQuizViewModel vm = new CreateQuizViewModel
            {
                CreatedBy = name,
                Questions = _unitOfWork.Question
                .GetAll()
                .Where(q => q.QuizID == Guid.Parse("ea8b887f-042d-4c56-a034-68845aa34099"))
                .ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create([FromForm] CreateQuizViewModel quizVM)
        {
            if (ModelState.IsValid)
            {
                Quiz quiz = new Quiz()
                {
                    Name = quizVM.Name,
                    CreatedBy = quizVM.CreatedBy,
                    Questions = quizVM.Questions
                };
                _unitOfWork.Quiz.Add(quiz);
                _unitOfWork.Save();
                string createdQuizId = quiz.QuizID.ToString();
                return RedirectToAction(nameof(ShowLink), new { id = createdQuizId });
            }
            return View();
        }

        //private List<SelectListItem> GetAnswers()
        //{
        //    var answerLists = new List<SelectListItem>();
        //    var questions = _unitOfWork.Question.GetAll()
        //        .Where(q => q.QuizID == 1).ToList();

        //    answerLists = questions.Select(q => new SelectListItem()
        //    {
        //        Value = q.QuestionID.ToString(),
        //        Text = q.
        //    });
        //}

        private List<SelectListItem> GetQuestions()
        {
            var questionsList = new List<SelectListItem>();
            List<Question> questions = _unitOfWork.Question.GetAll()
                .Where(q => q.QuizID == Guid.Parse("ea8b887f-042d-4c56-a034-68845aa34099")).ToList();

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
    }
}
