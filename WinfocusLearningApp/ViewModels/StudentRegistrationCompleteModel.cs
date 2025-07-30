using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WinfocusLearningApp.DataEntity;

namespace WinfocusLearningApp.ViewModels
{
    public class StudentRegistrationCompleteModel
    {
        public int SID { get; set; }
        public string RegId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string MobileNumber { get; set; }
        public string WhatsApp { get; set; }
        public string EmailID { get; set; }
        public string MteamID { get; set; }
        public string AccademicBoard { get; set; }
        public string CurrentStudyClass { get; set; }
        public string LastPassClass { get; set; }
        public string CLSchoolName { get; set; }
        public string CLSLocation { get; set; }
        public string Emirate { get; set; }
        public string TargetExam { get; set; }
        public string TargetYearExam { get; set; }
        public Nullable<int> ACID { get; set; }
        public string ACYear { get; set; }
        public Nullable<int> SyllabusID { get; set; }
        public string SyllabusName { get; set; }
        public Nullable<int> GradeID { get; set; }
        public string GradeName { get; set; }
        public Nullable<int> StreamID { get; set; }
        public string StreamName { get; set; }
        public Nullable<int> CousreID { get; set; }
        public string CourseName { get; set; }
        public string Dream { get; set; }
        public Nullable<System.DateTime> CreatedDt { get; set; }
        public Nullable<int> IsActive { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<int> ProcessStage { get; set; }
        public byte[] Profile { get; set; }
        public string ProfilePath { get; set; }

        //Parent Details

        public string ParentName { get; set; }
        public string RelationShip { get; set; }
        public string ParentMobileNumber { get; set; }
        public string WhatsAppNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string Emirates { get; set; }
        public string Occupation { get; set; }
        public string CompanyName { get; set; }
        public string HouseName { get; set; }
        public string State { get; set; }
        public string Dstrict { get; set; }
        public string Place { get; set; }
        public string PO { get; set; }
        public string PIN { get; set; }
        public byte[] FatherSignature { get; set; }
        public string FatherSignaturePath { get; set; }
        public byte[] StudentSignature { get; set; }
        public string StudentSignaturePath { get; set; }
        public List<TblStudent_Mark_Scored> studentList { get; set; }

    }
}