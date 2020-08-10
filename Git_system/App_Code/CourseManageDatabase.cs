using System;
using System.Collections.Generic;
using System.Linq;

namespace Git_system.App_Code
{
    public class CourseManageDatabase
    {
        private static Git_system.Models.Database_MainEntities1 db = new Git_system.Models.Database_MainEntities1();

        public static void updateStatusCourse()
        {
            List<Git_system.Models.Course> courses = db.Courses.Where(c => c.DateStart <= DateTime.Now).ToList();
            foreach (Git_system.Models.Course course in courses)
            {
                Git_system.Models.Course savecourse = course;
                savecourse.Active = false;
                db.Entry(course).CurrentValues.SetValues(savecourse);
                db.SaveChanges();
            }
        }
    }
}