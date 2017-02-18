using Microsoft.AspNet.Identity;
using StackUndertow.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
            ViewBag.controlName = "Question";
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
            }
            return View(question);
        }

        // POST: Image Upload
        [HttpPost]
        public ActionResult Upload(ImageUploadViewModel formData, int? id)
        {
            //Get File and Create Path
            var uploadedFile = Request.Files[0];
            string filename = $"{DateTime.Now.Ticks}{uploadedFile.FileName}";
            var serverPath = Server.MapPath(@"~\Uploads");
            var fullPath = Path.Combine(serverPath, filename);

            //Resize Image
            //WebImage img = new WebImage(uploadedFile.InputStream);
            //if (img.Width > 1000)
            //    img.Resize(1000, 1000);

            //Save Image
            uploadedFile.SaveAs(fullPath);

            var userId = User.Identity.GetUserId();
            int qId = 0;

            //Get Question Id if not provided            
            if (id == null)
            {
                qId = db.Questions
                    .Where(q => q.OwnerId == userId)
                    .OrderByDescending(q => q.Created)
                    .Select(q => q.Id)
                    .FirstOrDefault();
            }
            else
            {
                qId = (int)id;
            }

            //Create ImgUpload Entry
            var uploadModel = new ImgUpload
            {
                Caption = formData.Caption,
                File = filename,
                OwnerId = User.Identity.GetUserId(),
                RefId = qId,
                TypeRef = "QAttach",
            };

            db.ImgUploads.Add(uploadModel);
            db.SaveChanges();

            return RedirectToAction("Index");
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

            var attachment = db.ImgUploads
                .Where(i => i.TypeRef == "QAttach"
                && i.RefId == question.Id)
                .FirstOrDefault();

            if (attachment != null)
            {
                if (attachment.FilePath != null && attachment.FilePath != "")
                {
                    ViewBag.attachmentPath = attachment.FilePath;
                }
            }

            ViewBag.answerList = db.Answers
                .Where(a => a.QuestionId == id)
                .OrderByDescending(a => a.Score)
                .ToList();

            if (ViewBag.answerList.Count == 0)
            {
                ViewBag.canEdit = true;
            }

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