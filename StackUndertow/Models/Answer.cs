using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackUndertow.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int Score { get; set; }
        public int QuestionId { get; set; }
        public string OwnerId { get; set; }
        public DateTime Created { get; set; }

        public virtual ApplicationUser Owner { get; set; }
        public virtual Question Question { get; set; }
    }
}