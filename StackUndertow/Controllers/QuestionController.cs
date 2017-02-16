using Microsoft.AspNet.Identity;
using StackUndertow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackUndertow.Controllers
{
    public class QuestionController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Question
        public ActionResult Index()
        {           
            return View(db.Questions
                .OrderByDescending(q => q.Created)
                .ToList());
        }

        // CREATE: Initial View
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // CREATE: Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Body,OwnerId")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.Created = DateTime.Now;
                question.OwnerId = User.Identity.GetUserId();
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }
    }
}