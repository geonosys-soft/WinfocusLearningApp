using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WinfocusLearningApp.DataEntity;
using WinfocusLearningApp.ViewModels;

namespace WinfocusLearningApp.Controllers
{
    public class RegistrationController : Controller
    {
        Winfocus_CS db = new Winfocus_CS();
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Student() {
            string alpha = "WFE";
            Random random = new Random();
            int unique = random.Next(10000, 99999);
            int y = DateTime.Now.Year;
            int m = DateTime.Now.Month;
            var uniqueID = alpha +"-"+ y + m+"-"+ unique;
            StudentRegistrationModel info=new StudentRegistrationModel();
            info.RegistrationId = uniqueID;
            return View(info);
        }
        [HttpPost]
        public async Task<ActionResult> Student(StudentRegistrationModel info,HttpPostedFileBase studentPhoto, HttpPostedFileBase idProof, HttpPostedFileBase reportCard)
        {
            TblSTudentBasicDetail tblSTudentBasicDetail = new TblSTudentBasicDetail
            {
                AccademicBoard = info.board,
                ACID = info.academic_year,
                CurrentStudyClass = info.currentClass,
                Dream= info.dreamCareer,
                FullName = info.fullName,
                CLSchoolName = info.schoolName,
                CLSLocation = info.schoolLocation,
                DOB = info.dob,
                EmailID = info.studentEmail,
                CousreID = info.program,
                GradeID = info.grade,
                CreatedDt = DateTime.Now,
                Emirate = info.state,
                Gender=info.gender,
                IsActive = 0,
                IsDeleted = 0,
                LastPassClass = info.currentClass,
                MobileNumber = info.studentMobile,
                ProcessStage=1,
                RegId = info.RegistrationId,
                SyllabusID = info.syllabus,
                StreamID = info.program,
                TargetExam = info.targetExam,
                TargetYearExam = info.targetYear,
                WhatsApp = info.whatsappNumber,
                MteamID = "", // Assuming MteamID is the same as studentMobile
                Profile= studentPhoto != null ? convertFile(studentPhoto) : null,
            };
            db.TblSTudentBasicDetails.Add(tblSTudentBasicDetail);
            
            TblStudent_Parent_Basic tblStudent_Parent_Basic = new TblStudent_Parent_Basic
            {
                CompanyName = info.companyName,
                Dstrict= info.district,
                Email= info.parentEmail,
                ParentName = info.parentName,
                Emirates = info.state,
                HouseName = info.homeAddress,
                Location = info.parentLocation,
                MobileNumber = info.parentMobile,
                Occupation = info.parentOccupation,
                FatherSignature = idProof != null ? convertFile(idProof) : null,
                PIN = info.postOffice,
                PO= info.permanentLocation,
                RelationShip = info.relationship != null ? info.relationship : null,
                RegId = info.RegistrationId,
                State = info.state,
                WhatsAppNumber = info.parentWhatsapp != null ? info.parentWhatsapp : info.parentMobile,
                Place= info.permanentLocation,
                StudentSignature= reportCard != null ? convertFile(reportCard) : null
            };
            db.TblStudent_Parent_Basic.Add(tblStudent_Parent_Basic);

            var tblStudent_Mark_Scoreds = new List<TblStudent_Mark_Scored>
            {
                new TblStudent_Mark_Scored
                {
                    RegID = info.RegistrationId,
                    Subject = "Maths",
                    MaxMark = (decimal?)info.mathTotal,
                    MarkObtained = (decimal?)info.mathObtained,
                    IsActive = 1,
                    CreatedDt = DateTime.Now,
                    IsDeleted = 0
                },
                new TblStudent_Mark_Scored
                {
                    RegID = info.RegistrationId,
                    Subject = "Biology",
                    MaxMark = (decimal?)info.bioTotal,
                    MarkObtained = (decimal?)info.bioObtained,
                    IsActive = 1,
                    CreatedDt = DateTime.Now,
                    IsDeleted = 0
                },
                new TblStudent_Mark_Scored
                {
                    RegID = info.RegistrationId,
                    Subject = "Chemistry",
                    MaxMark = (decimal?)info.chemTotal,
                    MarkObtained = (decimal?)info.chemObtained,
                    IsActive = 1,
                    CreatedDt = DateTime.Now,
                    IsDeleted = 0
                },
                new TblStudent_Mark_Scored
                {
                    RegID = info.RegistrationId,
                    Subject = "Physics",
                    MaxMark = (decimal?)info.phyTotal,
                    MarkObtained = (decimal?)info.phyObtained,
                    IsActive = 1,
                    CreatedDt = DateTime.Now,
                    IsDeleted = 0
                }
            };

            db.TblStudent_Mark_Scored.AddRange(tblStudent_Mark_Scoreds);
            //db.TblSTudentBasicDetails.AddRange(tblSTudentBasicDetail);
            db.SaveChanges();
            return View();
        }
        public byte[] convertFile(HttpPostedFileBase file)
        {
            byte[] fileContent = new byte[file.ContentLength];
            var FileExt = Path.GetExtension(file.FileName);
            //fileContent = ConvertToByteArrayChunked(file.FileName);
            file.InputStream.Read(fileContent, 0, file.ContentLength);
             return fileContent;
        }
        
        public void SendMail(TblSTudentBasicDetail programmes)
        {
            var url = string.Format("/Account/login");
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);

            // var link = "https://winfocus.in/courses/login";

            //var fromEmail = new MailAddress("entrancecoachme@gmail.com", "Activation Account - winwintutor.com");
            var fromEmail = new MailAddress("entrancecoachme@gmail.com", "Account Registration - WINFOCUS");

            var toEmail = new MailAddress(programmes.EmailID);

            //  var fromEmailPassword = "DXB1234567890";mwesxhtcabdgzvhs
            //  var fromEmailPassword = "Enq@1234!!!";
            var fromEmailPassword = "dmzpwtbqmsnscbuc";
            string subject = "Account Registartion !";

            string body = "Name : " + programmes.FullName + "<br/>" + "Account Details" + "<br/>" + "Username : " + programmes.RegId + "<br/>" + "Yo will get the registration details after confirming admin Approval.";

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
        public ActionResult Student_payment()
        {
            return View();
        }
    }
}