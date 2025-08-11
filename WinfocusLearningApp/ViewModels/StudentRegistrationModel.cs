using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using WinfocusLearningApp.DataEntity;

namespace WinfocusLearningApp.ViewModels
{
    public class StudentRegistrationModel
    {
        public string RegistrationId { get; set; }
        public string fullName { get; set; }
        public string dob { get; set; }
        public string studentEmail { get; set; }
        public string gender { get; set; }
        public string studentMobile { get; set; }
        public string whatsappNumber { get; set; }
        public string board { get; set; }
        public string currentClass { get; set; }
        public double mathObtained { get; set; }
        public double mathTotal { get; set; }
        public double bioObtained { get; set; }
        public double bioTotal { get; set; }
        public double chemObtained { get; set; }
        public double chemTotal { get; set; }
        public double phyObtained { get; set; }
        public double phyTotal { get; set; }
       
        public double totalScienceMark { get; set; }
        public double maxScienceMark { get; set; }

        public double phyObtainedJunior { get; set; }
        public double phyTotalJunior { get; set; }
        public double chemObtainedJunior { get; set; }
        public double chemTotalJunior { get; set; }
        public double bioObtainedJunior { get; set; }
        public double bioTotalJunior { get; set; }
        public double mathObtainedJunior { get; set; }
        public double mathTotalJunior { get; set; }
       

        public string  schoolName { get; set; }
        public string schoolLocation { get; set; }
        public string schoolArea { get; set; }

        public string targetExam { get; set; }
        public string targetYear { get; set; }
       public int academic_year { get; set; }
        public int syllabus { get; set; }
        public int grade { get; set; }
        public int program { get; set; }
        public string dreamCareer { get; set; }
        public string  parentName { get; set; }
        public string relationship { get; set; }
        public string parentMobile { get; set; }
        public string parentWhatsapp { get; set; }
        public string parentBotim { get; set; }
        public string parentEmail { get; set; }
        public string parentOccupation { get; set; }
        public string companyName { get; set; }
        public string parentLocation { get; set; }
        public string homeAddress { get; set; }
        public string permanentLocation { get; set; }
        public string postOffice { get; set; }
        public string district { get; set; }
        public string state { get; set; }




    }
}