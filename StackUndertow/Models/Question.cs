using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackUndertow.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string OwnerId { get; set; }
        public DateTime Created { get; set; }
        
        public virtual ApplicationUser Owner { get; set; }

    }
}