using Microsoft.AspNet.Identity;
using StackUndertow.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackUndertow.Controllers
{
    [Authorize]
    public class AnswerController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();


        // ANSWER: Add
        // https://github.com/NicholasSaravia/StackUndertow
        [HttpPost]
        public ActionResult AddAnswer(string body, int id, string command)
        {
            var userid = User.Identity.GetUserId();

            if (command.Equals("Answer"))
            {
                Answer answer = new Answer()
                {
                    OwnerId = userid,
                    QuestionId = id,
                    Body = body,
                    Score = 0,
                    Created = DateTime.Now,
                };

                db.Answers.Add(answer);
                db.SaveChanges();

                return RedirectToAction("Detail", "Question");
            }

            return RedirectToAction("Detail", "Question");
        }

        // ANSWER: Voting
        [HttpPost]
        public ActionResult Vote(int id, int ansId, string command)
        {
            var userid = User.Identity.GetUserId();
            var answer = db.Answers.Find(ansId);

            if (answer == null)
            {
                return HttpNotFound();
            }

            if (command.Equals("UpVote"))
            {
                answer.Score += 1;
                db.Entry(answer).State = EntityState.Modified;
                db.SaveChanges();
            }
            else if (command.Equals("DownVote"))
            {
                answer.Score -= 1;
                db.Entry(answer).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Detail", "Question");

        }
    }
}