using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WinfocusLearningApp.DataEntity;

namespace WinfocusLearningApp.ViewModels
{
    public class GroupModelView
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int AcademicYearID { get; set; }
        public int SyllabusID { get; set; }
        public int ClassID { get; set; }
        public int StreamID { get; set; }
        public Nullable<int> SubjectID { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDateTime { get; set; }
        public int DeletedBy { get; set; }
        public System.DateTime DeletedDateTime { get; set; }
        public int IsDeleted { get; set; }
        public string TeacherIdList{ get; set;}
        public string StudentIdList { get; set; }

        public string selectedStudentIds { get; set; }
        public string selectedTeacherIds { get; set; }

    }
    public class GroupListModel
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public List<UserDropdownListModel> TeacherList { get; set; }
        public List<UserDropdownListModel> StudentList { get; set; }
    }
    public class UserDropdownListModel { 
    public int Id { get; set; }
        public string Fullname { get; set; }
        public string UniqueID { get; set; }
    }
}