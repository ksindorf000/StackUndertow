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
    }
}