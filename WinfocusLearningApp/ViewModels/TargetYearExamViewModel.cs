using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WinfocusLearningApp.DataEntity;

namespace WinfocusLearningApp.ViewModels
{
    public class TargetYearExamViewModel
    {
       
        public int ID { get; set; }
        public string TargetYear { get; set; }
        public string TargetExam { get; set; }
        public Nullable<int> TargetExamID { get; set; }
        public Nullable<int> IsActive { get; set; }
        public Nullable<int> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDt { get; set; }
        public Nullable<System.DateTime> DeletedDt { get; set; }

        public List<TargetYearExamViewModel> Exam { get; set; }

    }
}