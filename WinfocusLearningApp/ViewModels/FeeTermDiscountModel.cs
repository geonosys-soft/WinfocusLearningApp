using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WinfocusLearningApp.DataEntity;

namespace WinfocusLearningApp.ViewModels
{
    public class FeeTermDiscountModel
    {
        public int Id { get; set; }
        public Nullable<decimal> FeeAmount { get; set; }
        public Nullable<decimal> RegistrationFee { get; set; }
        public Nullable<decimal> TotalFee { get; set; }
        public Nullable<decimal> DiscountPers { get; set; }
        public Nullable<int> ACID { get; set; }
        public string AcademicYear { get; set; }
        public Nullable<int> SyllabusID { get; set; }
        public string SyllabusName { get; set; }
        public Nullable<int> GradeId { get; set; }
        public string GradeName { get; set; }
        public Nullable<int> StreamID { get; set; }
        public string StreamName { get; set; }
        public Nullable<int> CourseID { get; set; }
        public string CourseName { get; set; }
        public Nullable<decimal> Term1 { get; set; }
        public Nullable<decimal> Term2 { get; set; }
        public Nullable<decimal> Term3 { get; set; }
        public Nullable<decimal> Term4 { get; set; }
        public Nullable<decimal> Term5 { get; set; }

        public Nullable<decimal> Day1 { get; set; }
        public Nullable<decimal> Day2 { get; set; }
        public Nullable<decimal> Day3 { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedTime { get; set; }
        public string Description { get; set; }

    }
}