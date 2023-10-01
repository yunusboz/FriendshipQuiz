﻿using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Authorization;
using Utility.Constants;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class QuestionController : Controller
    {
        private readonly IRepositoryManager _unitOfWork;

        public QuestionController(IRepositoryManager unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //var questions = _unitOfWork.Question.GetAll().ToList();
            var questions2 = _unitOfWork.Question.GetAll().Include(q => q.Quiz).ToList();
            return View(questions2);
        }

        public IActionResult GetQuestions(Guid id)
        {
            List<Question> questionsOfQuiz = _unitOfWork.Question
                .GetQuestionsById(q => q.QuizID == id)
                .ToList();
            return View(questionsOfQuiz);
        }

        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Create([FromForm] Question question)
        //{
        //    var id = TempData["quizId"].ToString();
        //    question.QuizID = int.Parse(id);
        //    _unitOfWork.Question.Add(question);
        //    _unitOfWork.Save();
        //    return RedirectToAction("Index", "Quiz");
        //}
    }
}
