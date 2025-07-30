using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WinfocusLearningApp.DataEntity;
using WinfocusLearningApp.ViewModels;

namespace WinfocusLearningApp.Controllers
{
    public class Admin_StudentController : Controller
    {
        Winfocus_CS db = new Winfocus_CS();

        // GET: Admin_Student
        public ActionResult RegistrationList()
        {
            StudentReglistModel studentList = new StudentReglistModel();
            var StudeReg = (from student in db.TblSTudentBasicDetails
                            join academicData in db.TblAccademicYears on student.ACID equals academicData.Id
                            join syllabusData in db.TblSyllabus on student.SyllabusID equals syllabusData.Id
                            join gradeData in db.TblGrades on student.GradeID equals gradeData.Id
                            join streamData in db.TblStreams on student.StreamID equals streamData.Id
                            where student.IsDeleted == 0
                            select new StudentReglistModel
                            {
                                SID = student.SID,
                                RegId = student.RegId,
                                FullName = student.FullName,
                                IsActive=student.IsActive,
                                AccademicBoard = student.AccademicBoard,
                                CurrentStudyClass = student.CurrentStudyClass,
                                ACID = student.ACID,
                                ACYear = academicData.AccademicYear,
                                SyllabusID = student.SyllabusID,
                                SyllabusName = syllabusData.Name,
                                CLSchoolName = student.CLSchoolName,
                                CLSLocation = student.CLSLocation,
                                CreatedDt=student.CreatedDt,
                                DOB = student.DOB,
                                EmailID = student.EmailID,
                                MobileNumber = student.MobileNumber,
                                WhatsApp = student.WhatsApp,
                                MteamID = student.MteamID,
                                Dream= student.Dream,
                                Gender = student.Gender,
                                GradeName = gradeData.Name,
                                GradeID = student.GradeID,
                                IsDeleted = student.IsDeleted,
                                StreamID = student.StreamID,
                                StreamName = streamData.Name,
                                Emirate = student.Emirate,
                                TargetExam = student.TargetExam,
                                TargetYearExam = student.TargetYearExam,
                                CousreID = student.CousreID,
                                ProcessStage = student.ProcessStage,
                                Profile = student.Profile
                            }).ToList();
         
            studentList.studentList = StudeReg;

            return View(studentList);
        }
        public ActionResult RegistrationListview(int Id)
        {
            var StudeReg = (from student in db.TblSTudentBasicDetails
                            join academicData in db.TblAccademicYears on student.ACID equals academicData.Id
                            join syllabusData in db.TblSyllabus on student.SyllabusID equals syllabusData.Id
                            join gradeData in db.TblGrades on student.GradeID equals gradeData.Id
                            join streamData in db.TblStreams on student.StreamID equals streamData.Id
                            join parentData in db.TblStudent_Parent_Basic on student.RegId equals parentData.RegId
                            where student.IsDeleted == 0 && student.SID==Id
                            select new StudentRegistrationCompleteModel
                            {
                                SID = student.SID,
                                RegId = student.RegId,
                                FullName = student.FullName,
                                IsActive = student.IsActive,
                                AccademicBoard = student.AccademicBoard,
                                CurrentStudyClass = student.CurrentStudyClass,
                                ACID = student.ACID,
                                ACYear = academicData.AccademicYear,
                                SyllabusID = student.SyllabusID,
                                SyllabusName = syllabusData.Name,
                                CLSchoolName = student.CLSchoolName,
                                CLSLocation = student.CLSLocation,
                                CreatedDt = student.CreatedDt,
                                DOB = student.DOB,
                                EmailID = student.EmailID,
                                MobileNumber = student.MobileNumber,
                                WhatsApp = student.WhatsApp,
                                MteamID = student.MteamID,
                                Dream = student.Dream,
                                Gender = student.Gender,
                                GradeName = gradeData.Name,
                                GradeID = student.GradeID,
                                IsDeleted = student.IsDeleted,
                                StreamID = student.StreamID,
                                StreamName = streamData.Name,
                                Emirate = student.Emirate,
                                TargetExam = student.TargetExam,
                                TargetYearExam = student.TargetYearExam,
                                CousreID = student.CousreID,
                                ProcessStage = student.ProcessStage,
                                Profile = student.Profile,
                                //parent details
                                ParentName = parentData.ParentName,
                                CompanyName = parentData.CompanyName,
                                RelationShip = parentData.RelationShip,
                                ParentMobileNumber = parentData.MobileNumber,
                                WhatsAppNumber = parentData.WhatsAppNumber,
                                Email = parentData.Email,
                                CourseName = "",
                                Dstrict = parentData.Dstrict,
                                Emirates = parentData.Emirates,
                                FatherSignature = parentData.FatherSignature,
                                HouseName = parentData.HouseName,
                                LastPassClass = student.LastPassClass,
                                Location = parentData.Location,
                                Occupation = parentData.Occupation,
                                PIN = parentData.PIN,
                                Place = parentData.Place,
                                PO = parentData.PO,
                                State = parentData.State,
                                StudentSignature = parentData.StudentSignature
                                                               
                            }).FirstOrDefault();
            StudentRegistrationCompleteModel studentRegistrationCompleteModel = new StudentRegistrationCompleteModel();
            
            studentRegistrationCompleteModel = StudeReg;
           
            
             studentRegistrationCompleteModel.ProfilePath = ConvertImage(StudeReg.Profile);
            studentRegistrationCompleteModel.FatherSignaturePath=ConvertPdf(StudeReg.FatherSignature);
            studentRegistrationCompleteModel.StudentSignaturePath=ConvertPdf(StudeReg.StudentSignature);

            return View(studentRegistrationCompleteModel);
        }
        public ActionResult Accept_Student_Regitration(int Id)
        {
            var student = db.TblSTudentBasicDetails.Where(x => x.SID == Id).FirstOrDefault();
            if (student != null)
            {
                student.IsActive = 1;
                student.ProcessStage = 2; // Process Stage 2 for Accepted
                                          // db.SaveChanges();
                SendPaymentMail(student);
            }
            return RedirectToAction("RegistrationList");
        }
        public ActionResult Reject_Student_Registration(int Id)
        {
            var student = db.TblSTudentBasicDetails.Where(x => x.SID == Id).FirstOrDefault();
            if (student != null)
            {
                student.IsActive = 0;
                student.ProcessStage = 3; // Process Stage 3 for Rejected
                db.SaveChanges();
            }
            return RedirectToAction("RegistrationList");
        }

        public string ConvertImage(byte[] imgFile)
        {
            string base64Image = Convert.ToBase64String(imgFile);
            string imageSrc = $"data:image/jpeg;base64,{base64Image}";
            return imageSrc;
        }
        public string ConvertPdf(byte[] pdfFile)
        {
            string base64Image = Convert.ToBase64String(pdfFile);
            string imageSrc = $"data:application/pdf;base64,{base64Image}";
            return imageSrc;
        }

        public void SendPaymentMail(TblSTudentBasicDetail info)
        {
            var url = string.Format("/Registration/Student_Payment?RegId="+info.RegId);
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);

            // var link = "https://winfocus.in/Account/login";

            //var fromEmail = new MailAddress("entrancecoachme@gmail.com", "Activation Account - winwintutor.com");
            var fromEmail = new MailAddress("entrancecoachme@gmail.com", "Account Registration - WINFOCUS");

            var toEmail = new MailAddress(info.EmailID);

            //  var fromEmailPassword = "DXB1234567890";mwesxhtcabdgzvhs
            //  var fromEmailPassword = "Enq@1234!!!";
            var fromEmailPassword = "dmzpwtbqmsnscbuc";
            string subject = "Registration Approved – Complete Payment to Activate Your Account";

            /*  string body = "Dear " + info.FullName + "<br/>" + "We are pleased to inform you that your registration for " +
                  "[Course/Program Name] has been approved by the administration.<br/>To proceed further, please complete the " +
                  "payment using the link provided below:<br/>🔗 Payment Link:" + link + "<br/><b>Important Notes:</b><br/><ul><li>Kindly ensure payment is completed " +
                  "within 4 days to secure your enrollment.</li><li>After successful payment, your login credentials will be sent to your " +
                  "registered email address.</li></ul> <br/>If you have any questions or face any issues during the payment process, please don’t hesitate to " +
                  "contact us at [Support Email/Phone Number].<br/>Welcome aboard, and we look forward to having you with us!<br/>Warm regards,<br/>[Your Full Name]" +
                  "<br/>Admin Office<br/>[Institution Name]<br/>[Contact Info]<br/>[Website URL]";*/
            string body = "<p>Dear <strong>"+info.FullName+"</strong>,</p><p>We are pleased to inform you that your registration for the <strong>[Course/Program Name]</strong> has been approved." +
                "</p><p>To proceed, please complete the payment using the link below:</p><p style=\"text-align: left; margin: 30px 0;\">" +
                " <a href=\""+link+"\" target=\"_blank\" style=\"background-color: #1abc9c; color: white; padding: 12px " +
                "24px; text-decoration: none; border-radius: 4px; font-weight: bold;\"> Complete Payment </a></p><p><strong>Note:</strong> " +
                "Once your payment is confirmed, we will send your login credentials to your registered email address.</p>" +
                "<p>If you have any questions, feel free to contact us at <a href=\"mailto:[Support Email]\">[Support Email]</a>.</p>" +
                "<p>Welcome aboard!</p><p>Best regards,<br /><strong>[Your Name]</strong><br />" +
                " Admin Office<br />[Institution Name]<br /> [Contact Info] | <a href=\"[Website URL]\" target=\"_blank\">[Website URL]</a></p>";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true

            })
                smtp.Send(message);
        }
    }
}