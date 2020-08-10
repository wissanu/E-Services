using System;

namespace Git_system.Models.Form
{
    public class Backend_Course : Course
    {
        public Backend_Course()
        {
        }

        public Backend_Course(Course course)
        {
            this.Id = course.Id;

            this.TitleTH = course.TitleTH;
            this.TitleEN = course.TitleEN;

            this.Active = course.Active;
            this.ProductId = course.ProductId;
            this.ProductSKUActive = course.ProductSKUActive;

            this.Product = course.Product;
            this.PayItems = course.PayItems;

            this.DateStart = course.DateStart;
            this.DateEnd = course.DateEnd;

            this.DateStart_Date = course.DateStart;
            this.DateStart_Time = course.DateStart;

            this.DateEnd_Date = course.DateEnd;
            this.DateEnd_Time = course.DateEnd;
        }

        public Nullable<DateTime> DateStart_Date { get; set; }

        public Nullable<DateTime> DateStart_Time { get; set; }

        public Nullable<DateTime> DateEnd_Date { get; set; }

        public Nullable<DateTime> DateEnd_Time { get; set; }
    }
}