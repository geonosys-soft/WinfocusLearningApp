using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public string key = ConfigurationManager.AppSettings["RazorpayKey"];
        public string secret = ConfigurationManager.AppSettings["RazorpaySecret"];
        public string orderId;
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
        public ActionResult Student_payment(string RegId,string refferenceode)
        {
            TblSTudentBasicDetail studentBasicDetail = db.TblSTudentBasicDetails.FirstOrDefault(s => s.RegId == RegId && s.IsDeleted == 0);
            if (studentBasicDetail == null)
            {
                return HttpNotFound("Student not found.");
            }
           var tblstudentpaymentterms = db.TblStudent_Payment_Terms.FirstOrDefault(s => s.RegID == RegId && s.IsDeleted == 0);
            if (tblstudentpaymentterms != null)
            {
                return RedirectToAction("student_pymt_init_summary", new { RegID = RegId });
            }

            TblDicountCoupen tblDicountCoupen = db.TblDicountCoupens.FirstOrDefault(c => c.CoupenCode == refferenceode && c.IsActive == 1 && c.IsDeleted == 0);
                        
            if (tblDicountCoupen != null)
            {
                var validityDays = tblDicountCoupen.ValidityDays;
                var currentDate = DateTime.Now;
                if(tblDicountCoupen.FirstUsedDate != null)
                {
                    var firstUsedDate = tblDicountCoupen.FirstUsedDate.Value;
                    if ((currentDate - firstUsedDate).TotalDays > validityDays)
                    {
                       tblDicountCoupen = null; // Reset to null to avoid further processing
                    }
                }                                   
            }
            else
            {
                if (!string.IsNullOrEmpty(refferenceode))
                    ViewBag.ErrorMessage = "Inavlid Coupen";
            }
                var day1Discount = tblDicountCoupen?.Day1Dis ?? 0;
            var day2Discount = tblDicountCoupen?.Day2Dis ?? 0;
            var day3Discount = tblDicountCoupen?.Day3Dis ?? 0;
            StudentPaymentModel studentPaymentModel = new StudentPaymentModel();
            var feeDetails = (from fee in db.TblFeeDetails
                              join academicYear in db.TblAccademicYears on fee.ACID equals academicYear.Id
                              join syllabus in db.TblSyllabus on fee.SyllabusID equals syllabus.Id
                             join grade in db.TblGrades on fee.GradeId equals grade.Id
                             join stream in db.TblStreams on fee.StreamID equals stream.Id
                             join course in db.TblCourses on fee.CourseID equals course.Id
                             where fee.StreamID == studentBasicDetail.StreamID && fee.IsDeleted == 0
                             select new FeeTermDiscountModel
                             {
                                 Id = fee.Id,
                                 FeeAmount = fee.FeeAmount,
                                 RegistrationFee = fee.RegistrationFee,
                                 TotalFee = fee.TotalFee,
                                 DiscountPers = fee.DiscountPers,
                                 ACID = fee.ACID,
                                 SyllabusID = fee.SyllabusID,
                                 SyllabusName = syllabus.Name,
                                 GradeId = fee.GradeId,
                                 GradeName = grade.Name,
                                 StreamID = fee.StreamID,
                                 StreamName = stream.Name,
                                 CourseID = fee.CourseID,
                                 CourseName = course.Name,
                                 Term1 = fee.Term1,
                                 Term2 = fee.Term2,
                                 Term3 = fee.Term3,
                                 Term4 = fee.Term4,
                                 Term5 = fee.Term5,
                                 Description= fee.Description,
                                Day1=fee.TotalFee-((fee.TotalFee * day1Discount) / 100),
                                Day2=fee.TotalFee-((fee.TotalFee * day2Discount) / 100),
                                Day3=fee.TotalFee-(fee.TotalFee * day3Discount) / 100,
                                 CreatedBy = fee.CreatedBy,
                                 CreatedDate = fee.CreatedDate,
                                 IsDeleted = fee.IsDeleted,
                                 DeletedDate = fee.DeletedDate,
                                 ModifiedBy = fee.ModifiedBy,
                                 ModifiedTime = fee.ModifiedTime
                             }).ToList();

            studentPaymentModel.FeeTermDiscounts = feeDetails;
            studentPaymentModel.CourseSelected=studentBasicDetail.TargetExam;
            studentPaymentModel.RegID = studentBasicDetail.RegId;
            studentPaymentModel.ReferenceCode = refferenceode;

            return View(studentPaymentModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Student_Save_payment(StudentPaymentModel model, HttpPostedFileBase paymentProof)
        {
            if (paymentProof != null && paymentProof.ContentLength > 0)
            {
                model.UploadSlip =convertFile(paymentProof);
            }

            TblDicountCoupen tblDicountCoupen = db.TblDicountCoupens.FirstOrDefault(c => c.CoupenCode == model.ReferenceCode && c.IsActive == 1 && c.IsDeleted == 0);


            if (tblDicountCoupen != null)
            {
                var validityDays = tblDicountCoupen.ValidityDays;
                var currentDate = DateTime.Now;
                var DisPer = 0.00;
                if (tblDicountCoupen.FirstUsedDate != null)
                {
                    var firstUsedDate = tblDicountCoupen.FirstUsedDate.Value;
                    if ((currentDate - firstUsedDate).TotalDays > validityDays)
                    {
                        // If the coupon is expired, set it to inactive
                        tblDicountCoupen.IsActive = 0;
                        db.Entry(tblDicountCoupen).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        tblDicountCoupen = null; // Reset to null to avoid further processing
                    }
                }
                else
                {                     // If the coupon has never been used, set the first used date to current date
                    DisPer = (double)(tblDicountCoupen.Day1Dis ?? 0);
                    tblDicountCoupen.FirstUsedDate = DateTime.Now;
                    tblDicountCoupen.UsedBy = model.RegID;
                    tblDicountCoupen.IsActive = 1;
                    db.Entry(tblDicountCoupen).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

            }

            TblStudent_Payment_Terms tblStudent_Payment_Terms = new TblStudent_Payment_Terms
            {
                RegID = model.RegID,
                CourseSelected = model.CourseSelected,
                PaymentMode = model.PaymentMode,
                CourseID = model.CourseID,
                PaymentType = model.PaymentType,
                TransactionID = model.TransactionID,
                UploadSlip = model.UploadSlip,
                CreatedDt = DateTime.Now,
                IsDeleted = 0,
                ReferenceCode= model.ReferenceCode
            };
            db.TblStudent_Payment_Terms.Add(tblStudent_Payment_Terms);
            db.SaveChanges();
            return RedirectToAction("student_pymt_init_summary", new { RegID=model.RegID});
        }

        public ActionResult student_pymt_init_summary(string RegID)
       {
            if(string.IsNullOrEmpty(RegID))
            {
                return HttpNotFound("Registration ID is required.");
            }
            StudentPaymentPreviwModel studentPaymentPreviwModel = new StudentPaymentPreviwModel();
            var studentPaymentTerms = db.TblStudent_Payment_Terms.FirstOrDefault(s => s.RegID == RegID && s.IsDeleted == 0);
            var feeDetails = (from fee in db.TblFeeDetails
                              join academicYear in db.TblAccademicYears on fee.ACID equals academicYear.Id
                              join syllabus in db.TblSyllabus on fee.SyllabusID equals syllabus.Id
                              join grade in db.TblGrades on fee.GradeId equals grade.Id
                              join stream in db.TblStreams on fee.StreamID equals stream.Id
                              join course in db.TblCourses on fee.CourseID equals course.Id
                              where fee.Id ==studentPaymentTerms.CourseID  && fee.IsDeleted == 0
                              select new FeeTermDiscountModel
                              {
                                  Id = fee.Id,
                                  AcademicYear = academicYear.AccademicYear,
                                  FeeAmount = fee.FeeAmount,
                                  RegistrationFee = fee.RegistrationFee,
                                  TotalFee = fee.TotalFee,
                                  DiscountPers = fee.DiscountPers,
                                  ACID = fee.ACID,
                                  SyllabusID = fee.SyllabusID,
                                  SyllabusName = syllabus.Name,
                                  GradeId = fee.GradeId,
                                  GradeName = grade.Name,
                                  StreamID = fee.StreamID,
                                  StreamName = stream.Name,
                                  CourseID = fee.CourseID,
                                  CourseName = course.Name,
                                  Term1 = fee.Term1,
                                  Term2 = fee.Term2,
                                  Term3 = fee.Term3,
                                  Term4 = fee.Term4,
                                  Term5 = fee.Term5,
                                  Description = fee.Description,
                                  CreatedBy = fee.CreatedBy,
                                  CreatedDate = fee.CreatedDate,
                                  IsDeleted = fee.IsDeleted,
                                  DeletedDate = fee.DeletedDate,
                                  ModifiedBy = fee.ModifiedBy,
                                  ModifiedTime = fee.ModifiedTime
                              }).FirstOrDefault();

            var studentPreview=(from student in db.TblSTudentBasicDetails
                                join StudentPaymentModel in db.TblStudent_Payment_Terms on student.RegId equals StudentPaymentModel.RegID
                                where student.RegId == RegID && student.IsDeleted == 0 && StudentPaymentModel.IsDeleted == 0
                                select new StudentPaymentPreviwModel
                                {
                                    RegID = student.RegId,
                                    StudentName = student.FullName,
                                    Email = student.EmailID,
                                    Mobile = student.MobileNumber,
                                    DOB = student.DOB,
                                    ReferenceCode = studentPaymentTerms.ReferenceCode,
                                    RegistrationFee = feeDetails.RegistrationFee.ToString(),
                                    CourseFee = feeDetails.TotalFee.ToString(),
                                    TotalFee = feeDetails.TotalFee.ToString(),
                                    Discount = feeDetails.DiscountPers.ToString(),
                                    Term1 = feeDetails.Term1.ToString(),
                                    Term2 = feeDetails.Term2.ToString(),
                                    Term3 = feeDetails.Term3.ToString(),
                                    Term4 = feeDetails.Term4.ToString(),
                                    Term5 = feeDetails.Term5.ToString(),
                                    PaymentMethod = studentPaymentTerms.PaymentType,
                                    PaymentMode=studentPaymentTerms.PaymentMode
                                }).FirstOrDefault();
            
            TblDicountCoupen tblDicountCoupen = db.TblDicountCoupens.FirstOrDefault(c => c.CoupenCode == studentPaymentTerms.ReferenceCode && c.IsActive == 1 && c.IsDeleted == 0);
            var DisPer = 0.00; 
            if (tblDicountCoupen != null)
            {
                var validityDays = tblDicountCoupen.ValidityDays;
                var currentDate = DateTime.Now;
              
                if (tblDicountCoupen.FirstUsedDate != null)
                {
                    var firstUsedDate = tblDicountCoupen.FirstUsedDate.Value;
                    if ((currentDate - firstUsedDate).TotalDays > validityDays)
                    {
                        DisPer = 0.00;
                                             
                    }
                    else
                    {                     // If the coupon is still valid, calculate the discount percentage
                        var daysSinceFirstUsed = Math.Round((currentDate - firstUsedDate).TotalDays);
                        switch (daysSinceFirstUsed)
                        {
                            case 0:
                                DisPer = (double)(tblDicountCoupen.Day1Dis ?? 0);
                                break;
                            case 1:
                                DisPer = (double)(tblDicountCoupen.Day1Dis ?? 0);
                                break;
                            case 2:
                                DisPer = (double)(tblDicountCoupen.Day2Dis ?? 0);
                                break;
                            case 3:
                                DisPer = (double)(tblDicountCoupen.Day3Dis ?? 0);
                                break;
                            default:
                                DisPer = 0.00; // No discount if more than 3 days
                                break;
                        }
                    }
                }
                else
                {                     // If the coupon has never been used, set the first used date to current date
                    DisPer = (double)(tblDicountCoupen.Day1Dis ?? 0);
                }

            }
            var currentPayable = 0.00;
            studentPaymentPreviwModel = studentPreview;
            switch(studentPaymentPreviwModel.PaymentMode)
            {
                case "Full Payment":
                    currentPayable = Convert.ToDouble(studentPaymentPreviwModel.TotalFee) - ((Convert.ToDouble(studentPaymentPreviwModel.TotalFee) * DisPer) / 100);
                    break;
                case "Installments":
                   currentPayable = Convert.ToDouble(studentPaymentPreviwModel.Term1);
                    break;                
            }
            studentPaymentPreviwModel.CurrentPayable = currentPayable.ToString();
            studentPaymentPreviwModel.AcademicYear = feeDetails.AcademicYear;
            studentPaymentPreviwModel.Course = feeDetails.CourseName;
            studentPaymentPreviwModel.Syllabus = feeDetails.SyllabusName;
            studentPaymentPreviwModel.Grade = feeDetails.GradeName;
            studentPaymentPreviwModel.Program=feeDetails.Description;
            studentPaymentPreviwModel.CodeUsedDate =Convert.ToDateTime(tblDicountCoupen.FirstUsedDate);
            var feeDiscountAmount= (Convert.ToDouble(feeDetails.TotalFee)-currentPayable );
            studentPaymentPreviwModel.Discount = feeDiscountAmount.ToString();
           studentPaymentPreviwModel.DisPerc = DisPer;
            Random random = new Random();
            int unique = random.Next(10000, 99999);
            int y = DateTime.Now.Year;
            int m = DateTime.Now.Month;
            studentPaymentPreviwModel.MOrderID = "WFE" + "-" + y + m + "-" + unique; // Generating a unique merchant order ID
            // Here creating payment reference
            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount", Convert.ToDouble(studentPaymentPreviwModel.CurrentPayable)*100); // this amount should be same as transaction amount
            input.Add("currency", "INR");
            input.Add("receipt", studentPaymentPreviwModel.MOrderID);
            input.Add("payment_capture", 1);

            //string key = "rzp_test_z08RG1rzYUtKrc";
            //  string secret = "u6tPO7RYZ3Fp2zZsbMDTMIR9";

            RazorpayClient client = new RazorpayClient(key, secret);

            Razorpay.Api.Order order = client.Order.Create(input);
            orderId = order["id"].ToString();
            ViewBag.RazorpayKey = key; // ✅ Pass this to the view
            studentPaymentPreviwModel.razorpay_order_id = orderId;

            return View(studentPaymentPreviwModel);
        }

        [HttpPost]
        public async Task<ActionResult> student_pymt_init_summary(StudentPaymentPreviwModel model)
        {
           /* TblDicountCoupen tblDicountCoupen = db.TblDicountCoupens.FirstOrDefault(c => c.CoupenCode == model.ReferenceCode && c.IsActive == 1 && c.IsDeleted == 0);
           

            if (tblDicountCoupen != null)
            {
                var validityDays = tblDicountCoupen.ValidityDays;
                var currentDate = DateTime.Now;
                var DisPer = 0.00;
                if (tblDicountCoupen.FirstUsedDate != null)
                {
                    var firstUsedDate = tblDicountCoupen.FirstUsedDate.Value;
                    if ((currentDate - firstUsedDate).TotalDays > validityDays)
                    {
                        // If the coupon is expired, set it to inactive
                        tblDicountCoupen.IsActive = 0;
                        db.Entry(tblDicountCoupen).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        tblDicountCoupen = null; // Reset to null to avoid further processing
                    }
                    else
                    {                     // If the coupon is still valid, calculate the discount percentage
                        var daysSinceFirstUsed = (currentDate - firstUsedDate).TotalDays;
                        switch (daysSinceFirstUsed)
                        {
                            case 1:
                                DisPer = (double)(tblDicountCoupen.Day1Dis ?? 0);
                                break;
                            case 2:
                                DisPer = (double)(tblDicountCoupen.Day2Dis ?? 0);
                                break;
                            case 3:
                                DisPer = (double)(tblDicountCoupen.Day3Dis ?? 0);
                                break;
                            default:
                                DisPer = 0.00; // No discount if more than 3 days
                                break;
                        }
                    }
                }
                else
                {                     // If the coupon has never been used, set the first used date to current date
                    DisPer= (double)(tblDicountCoupen.Day1Dis ?? 0);
                    tblDicountCoupen.FirstUsedDate = DateTime.Now;
                    tblDicountCoupen.UsedBy = model.RegID;
                    tblDicountCoupen.IsActive = 1;
                    db.Entry(tblDicountCoupen).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

            }
            var day1Discount = tblDicountCoupen?.Day1Dis ?? 0;
            var day2Discount = tblDicountCoupen?.Day2Dis ?? 0;
            var day3Discount = tblDicountCoupen?.Day3Dis ?? 0;
            var feeDetails = (from fee in db.TblFeeDetails
                              join academicYear in db.TblAccademicYears on fee.ACID equals academicYear.Id
                              join syllabus in db.TblSyllabus on fee.SyllabusID equals syllabus.Id
                              join grade in db.TblGrades on fee.GradeId equals grade.Id
                              join stream in db.TblStreams on fee.StreamID equals stream.Id
                              join course in db.TblCourses on fee.CourseID equals course.Id
                              where fee.CourseID == model.CourseID && fee.IsDeleted == 0
                              select new FeeTermDiscountModel
                              {
                                  Id = fee.Id,
                                  FeeAmount = fee.FeeAmount,
                                  RegistrationFee = fee.RegistrationFee,
                                  TotalFee = fee.TotalFee,
                                  DiscountPers = fee.DiscountPers,
                                  ACID = fee.ACID,
                                  SyllabusID = fee.SyllabusID,
                                  SyllabusName = syllabus.Name,
                                  GradeId = fee.GradeId,
                                  GradeName = grade.Name,
                                  StreamID = fee.StreamID,
                                  StreamName = stream.Name,
                                  CourseID = fee.CourseID,
                                  CourseName = course.Name,
                                  Term1 = fee.Term1,
                                  Term2 = fee.Term2,
                                  Term3 = fee.Term3,
                                  Term4 = fee.Term4,
                                  Term5 = fee.Term5,
                                  Description=fee.Description,
                                  Day1=fee.TotalFee-((fee.TotalFee * day1Discount) / 100),
                                  Day2=fee.TotalFee-((fee.TotalFee * day2Discount) / 100),
                                  Day3=fee.TotalFee-(fee.TotalFee * day3Discount) / 100,
                                  CreatedBy = fee.CreatedBy,
                                  CreatedDate = fee.CreatedDate,
                                  IsDeleted = fee.IsDeleted,
                                  DeletedDate = fee.DeletedDate,
                                  ModifiedBy = fee.ModifiedBy,
                                  ModifiedTime = fee.ModifiedTime
                              }).ToList();
            TblStudent_Payment_Terms tblStudent_Payment_Terms = new TblStudent_Payment_Terms
            {
                RegID = model.RegID,
                CourseSelected = model.CourseSelected,
                PaymentMode = model.PaymentMode,
                CourseID = model.CourseID,
                PaymentType = model.PaymentType,
                TransactionID = model.TransactionID,
                UploadSlip = model.UploadSlip,
                CreatedDt = DateTime.Now,
                IsDeleted = 0
            };
            db.TblStudent_Payment_Terms.Add(tblStudent_Payment_Terms);
            db.SaveChanges();*/
            return View();
        }
    }

  
}