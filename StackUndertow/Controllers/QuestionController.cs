using Microsoft.AspNet.Identity;
using StackUndertow.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        // DETAIL
        [Route("Detail/{id}")]
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Question question = db.Questions.Find(id);

            if (question == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.answerList = db.Answers
                .Where(a => a.QuestionId == id)
                .OrderBy(a => a.Score)
                .ToList();
            
            return View(question);
        }

        // EDIT: Initial View
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string userId = User.Identity.GetUserId();
            Question question = db.Questions
                .Where(q => q.Id == id
                && q.OwnerId == userId)
                .FirstOrDefault();

            if (question == null)
            {
                return HttpNotFound();
            }

            return View(question);
        }

        // EDIT: Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,OwnerId")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.OwnerId = User.Identity.GetUserId();
                question.Created = question.Created;
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // DELETE: Initial View
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string userId = User.Identity.GetUserId();
            Question question = db.Questions
                .Where(q => q.Id == id
                && q.OwnerId == userId)
                .FirstOrDefault();
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // DELETE: Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}