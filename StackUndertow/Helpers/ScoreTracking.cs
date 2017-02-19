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
            db.SaveChanges();

            LogEvent(targetUser.Id, targetUser.UserName, 5, "Asked Question");
        }

        /*****************************************************
        * AnsUpVoted()
        *      Adds 10 points when user's answer is upvoted
        ******************************************************/
        public static void AnsUpVoted(string targetUserId, string ownerUserName)
        {
            ApplicationUser targetUser = db.Users.Find(targetUserId);

            targetUser.TotalScore += 10;
            db.Entry(targetUser).State = EntityState.Modified;
            db.SaveChanges();

            LogEvent(targetUser.Id, ownerUserName, 10, "Answer UpVoted");
        }

        /***********************************************************
        * AnsDwnVoted()
        *      Subtracts 5 points when user's answer is downvoted
        ************************************************************/
        public static void AnsDwnVoted(string targetUserId, string ownerUserName)
        {
            ApplicationUser targetUser = db.Users.Find(targetUserId);

            targetUser.TotalScore -= 5;
            db.Entry(targetUser).State = EntityState.Modified;

            LogEvent(targetUser.Id, ownerUserName, -5, "Answer UpVoted");

            ApplicationUser eventOwner = db.Users
               .Where(u => u.UserName == ownerUserName)
               .FirstOrDefault();

            eventOwner.TotalScore -= 1;
            db.Entry(targetUser).State = EntityState.Modified;

            LogEvent(eventOwner.Id, ownerUserName, -1, "DownVoted an Answer");

            db.SaveChanges();

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