using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Security.Claims;
using Utility.Constants;

namespace WebApp.Areas.AppUser.Controllers
{
    [Area("AppUser")]
    [Authorize(Roles = Roles.User)]
    public class UserController : Controller
    {
        private readonly IServiceManager _manager;

        public UserController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ApplicationUser appUser = _manager.ApplicationUserService.GetOneAppUsers(userId, false);
            TempData["Name"] = appUser.FirstName + " " +appUser.LastName;
            return View();
        }

        public IActionResult ScoreTable(Guid id)
        {
            List<QuizResult> quizResult = _manager.QuizService.GetOneQuiz(id, false, "QuizResults").QuizResults.ToList();
            return View(quizResult);
        }
    }
}
