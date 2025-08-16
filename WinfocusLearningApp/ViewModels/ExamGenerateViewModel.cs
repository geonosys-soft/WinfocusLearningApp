using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WinfocusLearningApp.DataEntity;

namespace WinfocusLearningApp.ViewModels
{
    public class ExamGenerateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public string ExamTime { get; set; }
        public int TotalMark { get; set; }
        public int GroupId { get; set; }
        public int AcademicYearID { get; set; }
        public int SyllabusID { get; set; }
        public int ClassID { get; set; }
        public int StreamID { get; set; }
        public int SubjectId { get; set; }
        public Nullable<int> ChapterId { get; set; }
        public int QsAsFrom { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public int IsActive { get; set; }
        public int IsDeleted { get; set; }
        public int DeletedBy { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDateTime { get; set; }
        public System.DateTime DeletedDateTime { get; set; }
        public Nullable<int> ExamType { get; set; }

        public List<ExamQuestionSelectionModel> ExamQuestions { get; set; }
    }

}