using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackUndertow.Models
{
    public class ScoreLog
    {
        public int Id { get; set; }
        public string TargetId { get; set; }
        public int Points { get; set; }
        public string Event { get; set; }
        public string EventOwnerId { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual ApplicationUser Target { get; set; }        
    }
}