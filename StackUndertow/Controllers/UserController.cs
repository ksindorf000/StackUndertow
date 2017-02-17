using Microsoft.AspNet.Identity;
using StackUndertow.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using static StackUndertow.Models.ImgUpload;

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

            var profilePic = db.ImgUploads
                .Where(i => i.TypeRef == "ProfilePic"
                && i.OwnerId == userId)
                .FirstOrDefault();

            ViewBag.PicPath = profilePic.FilePath;
            ViewBag.PicAlt = profilePic.Caption;

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
        [Route("u/{userName}")]
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

        [HttpPost]
        public ActionResult Upload(ImageUploadViewModel formData)
        {
            var uploadedFile = Request.Files[0];
            string filename = $"{DateTime.Now.Ticks}{uploadedFile.FileName}";
            var serverPath = Server.MapPath(@"~\Uploads");
            var fullPath = Path.Combine(serverPath, filename);
            uploadedFile.SaveAs(fullPath);

            // ---------------------

            WebImage img = new WebImage(uploadedFile.InputStream);
            if (img.Width > 1000)
                img.Resize(1000, 1000);           

            var uploadModel = new ImgUpload
            {
                Caption = formData.Caption,
                File = filename,
                OwnerId = User.Identity.GetUserId(),
                TypeRef = "ProfilePic",
            };

            db.ImgUploads.Add(uploadModel);
            db.SaveChanges();
            return Redirect("Index");
        }

    }
}
