﻿using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.IRepository;
using Entities.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using Utility.Constants;
using Services.Contracts;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class QuizController : Controller
    {
        private readonly IServiceManager _manager;

        public QuizController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            List<Quiz> quizzes = _manager.QuizService
                .GetAllQuiz(trackChanges: false, includeProperties: "Questions")
                .ToList();
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
            _manager.QuizService.CreateOneQuiz(quiz);
            return RedirectToAction("Index");
            /*TempData["quizId"] = quiz.QuizID;
            return RedirectToAction("Create", "Question");*/

            //return View("Question/Create", quiz.QuizID);
        }

    }
}
