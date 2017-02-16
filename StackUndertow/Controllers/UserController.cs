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
        [Authorize]
        [Route("User/{userName}")]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
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

            //http://stackoverflow.com/questions/7707290/get-sum-from-a-datacolumn-values-in-c-sharp
            var scores = db.Answers.AsEnumerable();
            ViewBag.Score = scores.Sum(datarow => datarow.Score);

            return View(currentUser);
        }
    }
}
