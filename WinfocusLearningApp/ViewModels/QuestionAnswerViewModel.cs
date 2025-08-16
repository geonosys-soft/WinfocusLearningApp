using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WinfocusLearningApp.ViewModels
{
    public class QuestionAnswerViewModel
    {
        public int ID { get; set; }
        public string Question { get; set; }
        public byte[] QImage { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public Nullable<int> Answer { get; set; }
        public Nullable<int> Mark { get; set; }
        public Nullable<int> SubjectID { get; set; }
        public string Subject { get; set; }
        public Nullable<int> ChapterID { get; set; }
        public string Chapter { get; set; }
        public Nullable<int> QuestionType { get; set; }

        public Nullable<int> AccademicYearId { get; set; }
        public string AccademicYear { get; set; }
        public Nullable<int> SyllabusID { get; set; }
        public string Syllabus { get; set; }
        public Nullable<int> ClassID { get; set; }
        public string Class { get; set; }
        public Nullable<int> StreamID { get; set; }
        public string Stream { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> Week { get; set; }
        public Nullable<int> CompletionStatus { get; set; }
        public Nullable<System.DateTime> Completiondate { get; set; }
        public Nullable<int> IsActive { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> CreatedDt { get; set; }
        public Nullable<System.DateTime> DeletedDt { get; set; }

        public List<QuestionAnswerViewModel> questionAnswerViewModels { get; set; }
    }
}