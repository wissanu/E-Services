using Git_system.Models;
using System;
using System.Data.Entity;

namespace Git_system.App_Code
{
    public class LogManageDatabase
    {
        public static void add_to_database(string username, string massage, Int16 status)
        {
            using (var context = new Database_MainEntities1())
            {
                Log log = new Log();
                log.Username = username;
                log.Massage = massage;
                log.Date = DateTime.Now;
                log.Status = status;

                context.Entry(log).State = log.Id == 0 ? EntityState.Added : EntityState.Modified;
                context.SaveChangesAsync();
            }
        }

        public static Log add_to_model_unsave(string username, string massage, Int16 status, Database_MainEntities1 dbCon)
        {
            Log log = new Log();
            log.Username = username;
            log.Massage = massage;
            log.Date = DateTime.Now;
            log.Status = status;

            dbCon.Entry(log).State = log.Id == 0 ? EntityState.Added : EntityState.Modified;
            return log;
        }
    }
}