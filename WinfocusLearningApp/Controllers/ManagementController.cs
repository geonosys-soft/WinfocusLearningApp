using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities.IO;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WinfocusLearningApp.Authentication;
using WinfocusLearningApp.DataEntity;
using WinfocusLearningApp.ViewModels;


namespace WinfocusLearningApp.Controllers
{
   // [CustomAuthorize(Roles = "Admin")]
    public class ManagementController : Controller
    {

        private readonly Winfocus_CS dbEntities = new Winfocus_CS();
        // GET: Admin/Management
        public ActionResult Index()
        {
            // You can redirect to the first item or a management dashboard
            return RedirectToAction("AllUsers");
        }

        public ActionResult AllUsers()
        {
            //code updated
            return View();
        }

        public ActionResult CreateUsers() { return View(); }
        public ActionResult AcademicYear(int? id)
        {

            TblAccademicYear inf = new TblAccademicYear();
            if (id != null)
            {
                inf = dbEntities.TblAccademicYears.Find(id);
            }
            else
            {

                inf.CreatedBy = 1; // Assuming 1 is the ID of the admin user
                inf.CreatedDate = DateTime.UtcNow;
                inf.IsDeleted = 0;
            }
            return View(inf);
        }


        public ActionResult ClassManagement() { return View(); }
        public ActionResult Stream() { return View(); }
        public ActionResult Course() { return View(); }
        public ActionResult Subject() { return View(); }
        public ActionResult Chapter() { return View(); }
        public ActionResult SubChapter() { return View(); }
        public ActionResult Module() { return View(); }
        public ActionResult NoteType() { return View(); }
        public ActionResult SchoolState() { return View(); }
        public ActionResult District() { return View(); }
        public ActionResult SchoolDetails() { return View(); }
        public ActionResult CreateGroup() { return View(); }
        public ActionResult ManageGroup() { return View(); }
        public ActionResult CourseFee() { return View(); }
        public ActionResult RequestCallBack() { return View(); }
        public ActionResult StudentReference() { return View(); }
        public ActionResult CreateLessonSummary() { return View(); }
        public ActionResult SyllabusCreation() { return View(); }
        public ActionResult AddMeeting() { return View(); }
        public ActionResult TimeTable() { return View(); }
        public ActionResult Blog() { return View(); }
        public ActionResult SupportTicket() { return View(); }
        public ActionResult Complaints() { return View(); }
        public ActionResult MaterialType() { return View(); }
        public ActionResult Batch() { return View(); }
        public ActionResult PreferredTimeSlot() { return View(); }
        public ActionResult St_Registration() { return View(); }
        public ActionResult BDE_Registration() { return View(); }
        public ActionResult FeesDetails() { return View(); }
        public ActionResult StudentTeacherGroup(int? Id) {
            GroupModelView model = new GroupModelView();
            if (Id == null)
            {
                ViewBag.AcademicYearID = new SelectList(Enumerable.Empty<SelectListItem>());
                ViewBag.SyllabusID = new SelectList(Enumerable.Empty<SelectListItem>());
                ViewBag.ClassID = new SelectList(Enumerable.Empty<SelectListItem>());
                ViewBag.StreamID = new SelectList(Enumerable.Empty<SelectListItem>());
                ViewBag.TeacherID= new SelectList(Enumerable.Empty<SelectListItem>());
                ViewBag.StudentID = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            else { 
            }
            if (TempData["Success"]!=null)
            {
                ViewBag.Success = TempData["Success"];
            }
            if (TempData["Error"]!=null)
            {
                ViewBag.Error = TempData["Error"];
            }

                return View(); 
        }
        [HttpPost]
        public async Task< ActionResult> StudentTeacherGroup(GroupModelView modelView)
        {
            GroupModelView model = new GroupModelView();

            if (modelView.Id != 0)
            {
            }
            else 
            { 
            }
                return View();
        }
        public ActionResult TargetyearExam(int? id)
        {
            TargetYearExamViewModel model = new TargetYearExamViewModel();
            if (id != null)
            {
                var targetYearExam = dbEntities.TblTargetYears.Find(id);
                ViewBag.TargetExamID = new SelectList(dbEntities.TblTargetExams.Where(p => p.IsDeleted == 0), "ID", "TargetExam", targetYearExam.TargetExamID);
                model.ID = targetYearExam.ID;
                model.TargetYear = targetYearExam.TargetYear;
            }
            else
            {
                ViewBag.TargetExamID = new SelectList(dbEntities.TblTargetExams.Where(p => p.IsDeleted == 0), "ID", "TargetExam");

            }
            var data = (from TYre in dbEntities.TblTargetYears
                        join TExm in dbEntities.TblTargetExams on TYre.TargetExamID equals TExm.ID
                        where TYre.IsDelete == 0
                        select new TargetYearExamViewModel
                        {
                            ID = TYre.ID,
                            TargetExam = TExm.TargetExam,
                            TargetExamID = TYre.TargetExamID,
                            TargetYear = TYre.TargetYear,
                            IsDelete = TYre.IsDelete,
                            CreatedDt = TYre.CreatedDt,
                            DeletedDt = TYre.DeletedDt,
                            IsActive = TYre.IsActive
                        }).ToList();
            model.Exam = data;
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.Success = TempData["SuccessMessage"];
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.Error = TempData["ErrorMessage"];
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TargetYearExam(TargetYearExamViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ID != 0)
                {
                    var targetyearExam = dbEntities.TblTargetYears.Find(model.ID);
                    targetyearExam.TargetYear = model.TargetYear;
                    targetyearExam.TargetExamID = model.TargetExamID;
                    dbEntities.Entry(targetyearExam).State = System.Data.Entity.EntityState.Modified;
                    if (dbEntities.SaveChanges() > 0)
                    {
                        TempData["SuccessMessage"] = "Target Year Exam updated successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Failed to update Target Year Exam.";
                    }
                }
                else
                {

                    TblTargetYear tblTargetYear = new TblTargetYear()
                    {
                        CreatedDt = DateTime.UtcNow,
                        IsActive = 1,
                        IsDelete = 0,
                        TargetYear = model.TargetYear,
                        TargetExamID = model.TargetExamID
                    };
                    dbEntities.TblTargetYears.Add(tblTargetYear);
                    if (dbEntities.SaveChanges() > 0)
                    {
                        TempData["SuccessMessage"] = "Target Year Exam created successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Failed to create Target Year Exam.";
                    }
                }

                return RedirectToAction("TargetyearExam", "Management");
            }
            ViewBag.TargetExamID = new SelectList(dbEntities.TblTargetExams.Where(p => p.IsDeleted == 0), "ID", "TargetExam");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult deleteTargetExamYear(int? deleteId)
        {
            var targetYearExam = dbEntities.TblTargetYears.Find(deleteId);
            if (targetYearExam != null)
            {
                targetYearExam.IsDelete = 1;
                targetYearExam.DeletedDt = DateTime.UtcNow;
                dbEntities.Entry(targetYearExam).State = System.Data.Entity.EntityState.Modified;
                if (dbEntities.SaveChanges() > 0)
                {
                    TempData["SuccessMessage"] = "Target Year Exam deleted successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to delete Target Year Exam.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Target Year Exam not found.";
            }
            return RedirectToAction("TargetyearExam", "Management");
        }
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult DTPRegistration(int? Id)
        {
            DTPRegistrationModel info = new DTPRegistrationModel();
            if (TempData["Success"] != null)
            {
                ViewBag.Success = TempData["Success"];
            }
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }
            string alpha = "DTP";
            Random random = new Random();
            int unique = random.Next(10000, 99999);
            int y = DateTime.Now.Year;
            int m = DateTime.Now.Month;
            var uniqueID = alpha + "-" + y + m + "-" + unique;
            if (Id != null)
            {
                var data = dbEntities.TblDTPRegistrations.Where(x => x.ID == Id).FirstOrDefault();
                if (data != null)
                {
                    info.ID = data.ID;
                    info.RegID = data.RegID;
                    info.Fullname = data.Fullname;
                    info.DOB = data.DOB;
                    info.MobileNumber = data.MobileNumber;
                    info.EmailId = data.EmailId;
                    info.Location = data.Location;
                    info.Place = data.Place;
                    info.Address = data.Address;
                    info.IsDeleted = data.IsDeleted;
                    info.DeletedBy = data.DeletedBy;
                    info.DeletedDt = data.DeletedDt;
                    info.CreatedBy = data.CreatedBy;
                    info.CreatedDt = data.CreatedDt;
                    info.ModifiedBy = data.ModifiedBy;
                    info.ModifiedDt = data.ModifiedDt;
                    info.ProfilePic = data.ProfilePic;
                    info.AddressProof = data.AddressProof;
                    info.WhatsApp = data.WhatsApp;
                }
            }
            else
            {
                info.RegID=uniqueID;
            }

            return View(info);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DTPRegistration(DTPRegistrationModel model,HttpPostedFileBase ProfilePicture,HttpPostedFileBase IdProof)
        {
            if (ModelState.IsValid)
            {
                if (model.ID != 0)
                {
                    // Update existing registration
                    var registration = dbEntities.TblDTPRegistrations.Find(model.ID);
                    if (registration != null)
                    {
                        registration.Fullname = model.Fullname;
                        registration.DOB = model.DOB;
                        registration.MobileNumber = model.MobileNumber;
                        registration.EmailId = model.EmailId;
                       registration.RegID = model.RegID;
                        registration.Address = model.Address;
                        registration.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                        registration.ModifiedDt = DateTime.UtcNow;
                        registration.ProfilePic = ProfilePicture==null? registration.ProfilePic: convertFile(ProfilePicture);
                        registration.AddressProof = model.AddressProof;
                        registration.WhatsApp = model.WhatsApp;
                        registration.AddressProof = IdProof == null ? registration.AddressProof : convertFile(IdProof);
                        dbEntities.Entry(registration).State = System.Data.Entity.EntityState.Modified;
                        if (dbEntities.SaveChanges() > 0)
                        {
                            TempData["Success"] = "DTP Registration updated successfully.";
                        }
                        else
                        {
                            TempData["Error"] = "Failed to update DTP Registration.";
                        }
                    }
                }
                else
                {
                    TblDTPRegistration registration = new TblDTPRegistration
                    {
                        Fullname = model.Fullname,
                        DOB = model.DOB,
                        MobileNumber = model.MobileNumber,
                        EmailId = model.EmailId,
                        Address = model.Address,
                        CreatedBy = 1, // Assuming 1 is the ID of the admin user
                        CreatedDt = DateTime.UtcNow,
                        IsDeleted = 0,
                        ProfilePic =ProfilePicture==null?null:convertFile(ProfilePicture),
                        AddressProof = IdProof==null?null:convertFile(IdProof),
                        WhatsApp = model.WhatsApp,
                        RegID = model.RegID,
                    };
                    dbEntities.TblDTPRegistrations.Add(registration);
                    if (dbEntities.SaveChanges() > 0)
                    {
                        TempData["Success"] = "DTP Registration created successfully.";
                    }
                    else
                    {
                        TempData["Error"] = "Failed to create DTP Registration.";
                    }
                }

            }
            return RedirectToAction("DTPRegistration", "Management");
        }

        public byte[] convertFile(HttpPostedFileBase file)
        {
            byte[] fileContent = new byte[file.ContentLength];
            var FileExt = Path.GetExtension(file.FileName);
            //fileContent = ConvertToByteArrayChunked(file.FileName);
            file.InputStream.Read(fileContent, 0, file.ContentLength);
            return fileContent;
        }

        public void SendLoginMail(User data)
        {
            var url = string.Format("/Account/login");
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);

            // var link = "https://winfocus.in/courses/login";

            //var fromEmail = new MailAddress("entrancecoachme@gmail.com", "Activation Account - winwintutor.com");
            var fromEmail = new MailAddress("entrancecoachme@gmail.com", "Account Registration - WINFOCUS");

            var toEmail = new MailAddress(data.Email);

            //  var fromEmailPassword = "DXB1234567890";mwesxhtcabdgzvhs
            //  var fromEmailPassword = "Enq@1234!!!";
            var fromEmailPassword = "dmzpwtbqmsnscbuc";
            string subject = "Account Registartion !";

            string body = " <h2>Welcome to Our Portal</h2> <p>Dear <strong>"+data.LastName+"</strong>,</p>" +
                "<p>Your account has been created successfully. Please find your login credentials below:</p>" +
                " <div class=\"credentials\"><p><strong>Username:</strong> "+data.UserName+"</p>" +
                "<p><strong>Password:</strong> "+data.Password+"</p></div><p>You can log in using the button below:</p>" +
                " <a href=\""+link+"\" class=\"btn\">Login to Your Account</a> <p class=\"footer\">Please keep your login " +
                "credentials confidential. If you did not request this account,  contact our support team immediately.</p>";

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
        public ActionResult DTPRegistrationList()
        {
            var registrations = dbEntities.TblDTPRegistrations.Where(x => x.IsDeleted == 0).ToList();
            DTPRegistrationModel model = new DTPRegistrationModel();
            if (TempData["Success"] != null)
            {
                ViewBag.Success = TempData["Success"];
            }
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }
            model._dtpRegistrationList = registrations;


            return View(model);
        }
        public ActionResult DeleteDTPRegistration(int? deleteId)
        {
            var registration = dbEntities.TblDTPRegistrations.Find(deleteId);
            if (registration != null)
            {
                registration.IsDeleted = 1;
                registration.DeletedDt = DateTime.UtcNow;
                dbEntities.Entry(registration).State = System.Data.Entity.EntityState.Modified;
                if (dbEntities.SaveChanges() > 0)
                {
                    TempData["Success"] = "DTP Registration deleted successfully.";
                }
                else
                {
                    TempData["Error"] = "Failed to delete DTP Registration.";
                }
            }
            else
            {
                TempData["Error"] = "DTP Registration not found.";
            }
            return RedirectToAction("DTPRegistrationList", "Management");
        }

        public ActionResult addDTPUser(int id) 
        {
            try
            {
                var dtpRegistration = dbEntities.TblDTPRegistrations.Find(id);
                var findUser = dbEntities.Users.FirstOrDefault(u => u.UniqueID == dtpRegistration.RegID);
                if (findUser != null)
                {
                    TempData["Error"] = "DTP User already exists for this registration.";
                    return RedirectToAction("DTPRegistrationList", "Management");
                }
                if (dtpRegistration == null)
                {
                    TempData["Error"] = "DTP Registration not found.";
                    return RedirectToAction("DTPRegistrationList", "Management");
                }
                else
                {
                    User newUser = new User
                    {
                        LastName = dtpRegistration.Fullname,
                        Email = dtpRegistration.EmailId,
                        UserName = dtpRegistration.RegID,
                        Password = dtpRegistration.DOB, // Assuming DOB is used as a password, which is not recommended for security reasons
                        ActivationCode = Guid.NewGuid().ToString(),
                        IsActive = 1, // 1 for Active
                        IsDeleted = 0, // 0 for Not Deleted
                        CreatedDate = DateTime.Now,
                        RoleId = 4, // Assuming 3 is the role ID for DTP
                        DeletedBy = 0, // 0 for Not Deleted
                        DeletedDate = DateTime.Now,
                        UniqueID = dtpRegistration.RegID, // Assuming RegId is unique for each user
                        MobileNo = dtpRegistration.MobileNumber
                    };
                    dbEntities.Users.Add(newUser);
                    if(dbEntities.SaveChanges() > 0)
                    {
                        // Send email notification
                        var userData = dbEntities.Users.FirstOrDefault(r => r.UniqueID == newUser.UniqueID);
                        SendLoginMail(userData);
                        dbEntities.SPinsertRole(userData.RoleId,userData.Id);
                        TempData["Success"] = "DTP User added successfully.";
                    }
                    else
                    {
                        TempData["Error"] = "Failed to add DTP User.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to add DTP User: " + ex.Message;
                return RedirectToAction("DTPRegistrationList", "Management");
            }

            return RedirectToAction("DTPRegistrationList", "Management");
        }


    }
}