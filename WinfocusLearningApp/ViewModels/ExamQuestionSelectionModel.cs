using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WinfocusLearningApp.ViewModels
{
    public class ExamQuestionSelectionModel
    {
        public int QuestionID { get; set; }
        public string Question { get; set; }

        public byte[] QuestionImage { get; set; }

        public int Mark { get; set; }

        public bool QuestionTickStatus { get; set; }

    }
}