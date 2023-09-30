﻿using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.IRepository;
using Entities.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuizController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuizController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Quiz> quizzes = _unitOfWork.Quiz.GetAll().Include(q => q.Questions).ToList();
            return View(quizzes);
        }

        public IActionResult Create()
        {
            return View(new QuizViewModel());
        }

        [HttpPost]
        public IActionResult Create(QuizViewModel quizViewModel)
        {
            Quiz quiz = new Quiz()
            {
                Name = quizViewModel.Quiz.Name,
                CreatedBy = quizViewModel.Quiz.CreatedBy
            };
            quiz.Questions.Add(quizViewModel.Question);
            _unitOfWork.Quiz.Add(quiz);
            _unitOfWork.Save();
            return RedirectToAction("Index");
            /*TempData["quizId"] = quiz.QuizID;
            return RedirectToAction("Create", "Question");*/

            //return View("Question/Create", quiz.QuizID);
        }

    }
}
