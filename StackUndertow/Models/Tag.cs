using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StackUndertow.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        
        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
        
    }
}