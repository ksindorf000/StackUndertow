using Microsoft.AspNet.Identity;
using StackUndertow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackUndertow.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        // GET: Current User
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            ViewBag.UserName = User.Identity.GetUserName();

            ApplicationUser currentUser = db.Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            ViewBag.questionList = db.Questions
                .Where(q => q.OwnerId == userId)
                .OrderByDescending(q => q.Created)
                .ToList();

            ViewBag.answerList = db.Answers
                .Where(a => a.OwnerId == userId)
                .OrderByDescending(a => a.Score)
                .ToList();
            
            ViewBag.Score = CalculateScore(ViewBag.answerList);

            return View(currentUser);
        }

        // GET: User in Route
        [Route("User/{userName}")]
        public ActionResult Index(string userName)
        {
            ApplicationUser targetUser = db.Users
                .Where(u => u.UserName == userName)
                .FirstOrDefault();

            string userId = targetUser.Id;

            ViewBag.questionList = db.Questions
                .Where(q => q.OwnerId == userId)
                .OrderByDescending(q => q.Created)
                .ToList();

            ViewBag.answerList = db.Answers
                .Where(a => a.OwnerId == userId)
                .OrderByDescending(a => a.Score)
                .ToList();
                       
            ViewBag.Score = CalculateScore(ViewBag.answerList);

            return View(targetUser);
        }

        // Calculate Score
        private int CalculateScore(List<Answer> answerList)
        {
            int score = 0;

            foreach (var answer in answerList)
            {
                score += answer.Score;
            }

            return score;
        }
    }
}
