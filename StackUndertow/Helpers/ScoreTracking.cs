using StackUndertow.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StackUndertow.Helpers
{
    public class ScoreTracking
    {
        public static ApplicationDbContext db = new ApplicationDbContext();

        /********************************************
         * QCreate()
         *      Adds 5 points for asking a question
         ********************************************/
        public static void QCreate(string userId)
        {
            ApplicationUser targetUser = db.Users.Find(userId);

            targetUser.TotalScore += 5;
            db.Entry(targetUser).State = EntityState.Modified;

            LogEvent(targetUser.Id, targetUser.UserName, 5, "Asked Question");
        }

        /***********************************************
         * LogEvent()
         *    Logs all events that affect user's score
         ***********************************************/
        private static void LogEvent(string targetId, string eventOwner, int points, string eventName)
        {
            ScoreLog newEvent = new ScoreLog
            {
                TargetId = targetId,
                EventOwnerName = eventOwner,
                Event = eventName,
                Points = points,
                Timestamp = DateTime.Now
            };

            db.ScoreLogs.Add(newEvent);
            db.SaveChanges();
        }

    }
}