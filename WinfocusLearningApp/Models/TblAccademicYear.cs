using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WinfocusLearningApp.Models
{
    public class TblAccademicYear
    {
        public int Id { get; set; }
        public string AccademicYear { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int IsDeleted { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedTime { get; set; }
    }
}