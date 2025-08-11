using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WinfocusLearningApp.DataEntity;
using WinfocusLearningApp.ViewModels;

namespace WinfocusLearningApp.Controllers
{
    public class PaymentController : Controller
    {
        Winfocus_CS Winfocus_CS = new Winfocus_CS();
        // GET: Payment
        public string orderId;
        public string paymentId;
        public string signature;
        public string key = ConfigurationManager.AppSettings["RazorpayKey"];
        public string secret = ConfigurationManager.AppSettings["RazorpaySecret"];
        public ActionResult Payment()
        {
            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount", 100); // this amount should be same as transaction amount
            input.Add("currency", "INR");
            input.Add("receipt", "12121");
            input.Add("payment_capture", 1);

            //string key = "rzp_test_z08RG1rzYUtKrc";
            //  string secret = "u6tPO7RYZ3Fp2zZsbMDTMIR9";

            RazorpayClient client = new RazorpayClient(key, secret);

            Razorpay.Api.Order order = client.Order.Create(input);
            orderId = order["id"].ToString();
            ViewBag.RazorpayKey = key; // ✅ Pass this to the view
            RazorPayPaymentModel model = new RazorPayPaymentModel
            {
                razorpay_order_id = orderId
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Response_Payment(StudentPaymentPreviwModel model)
        {
           // string paymentId = Request.Form["razorpay_payment_id"];

            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount", model.TotalPayable); // this amount should be same as transaction amount

            //string key = "<Enter your Api Key here>";
            // string secret = "<Enter your Api Secret here>";

            RazorpayClient client = new RazorpayClient(key, secret);

            Dictionary<string, string> attributes = new Dictionary<string, string>();

            attributes.Add("razorpay_payment_id", model.razorpay_payment_id);
            attributes.Add("razorpay_order_id", model.razorpay_order_id);
            attributes.Add("razorpay_signature", model.razorpay_signature);

            Utils.verifyPaymentSignature(attributes);

            //          Please use below code to refund the payment   
            //          Refund refund = new Razorpay.Api.Payment((string) paymentId).Refund();


            string payload = model.razorpay_order_id + "|" + model.razorpay_payment_id;

            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
                var generatedSignature = BitConverter.ToString(hash).Replace("-", "").ToLower().Replace(" ", "");

                if (generatedSignature == model.razorpay_signature.ToLower())
                {
                    // ✅ Payment is successful and verified
                    // ⬇ Save to DB
                    //SavePayment(model.razorpay_payment_id, model.razorpay_order_id, "Success");
                        var studentFee=(from s in Winfocus_CS.TblFeeDetails
                                        join terms in Winfocus_CS.TblStudent_Payment_Terms on s.Id equals terms.CourseID
                                        where terms.RegID == model.RegID
                                         select new StudentPaymentPreviwModel
                                         {
                                                RegID = terms.RegID,
                                                MarchentOrderId = model.MarchentOrderId,
                                                ReferenceCode = model.ReferenceCode,
                                                RegistrationFee = s.RegistrationFee.ToString(),
                                                CourseFee = s.FeeAmount.ToString(),
                                                TotalFee = s.TotalFee.ToString(),
                                                Term1 = s.Term1.ToString(),
                                                Term2 = s.Term2.ToString(),
                                                Term3 = s.Term3.ToString(),
                                                Term4 = s.Term4.ToString(),
                                                Term5 = s.Term5.ToString(),
                                                PaymentMethod = terms.PaymentMode,
                                                PaymentStatus = "Success",                                                
                                                PaymentReference = model.razorpay_payment_id,
                                                PaymentMode = model.PaymentMode,
                                                PaymentResponseCode= "200",
                                                PaymentResponseMessage= "Payment Successful",                                                
                                                CurrentPayable=model.CurrentPayable.ToString(),
                                                razorpay_payment_id=model.razorpay_payment_id,
                                                razorpay_order_id=model.razorpay_order_id,
                                                razorpay_signature=model.razorpay_signature,
                                                Discount = model.Discount,
                                                DisPerc = model.DisPerc,
                                             Course = s.Id.ToString()

                                         }).FirstOrDefault();
                    if(model.PaymentMode.Equals("Full Payment"))
                    {
                        TblStudent_Fee std_fee_info = new TblStudent_Fee
                        {
                            Amount = Convert.ToDouble(studentFee.TotalFee),
                            RegID = studentFee.RegID,
                            FeePayTerm = "Full Payment",
                            DiscountPer =studentFee.DisPerc.ToString(),
                            DiscountAmount =Convert.ToDouble(studentFee.Discount),
                            PaymentStatus = 1, // 1 for Success,
                            FeeID = Convert.ToInt32(studentFee.Course), // Assuming 1 is the ID for the fee type
                            InstallmentMode = 0, // 0 for Full Payment
                            PaymentDt = DateTime.Now,
                            PaymentDueDt = null, // Assuming payment due date is 30 days from now
                            IsActive = 1, // 1 for Active
                            CreatedDt = DateTime.Now,
                            IsDeleted = 0, // 0 for Not Deleted
                            DeletedDt = null,
                            TaxAmount=model.GSTAmount != null ? Convert.ToDouble(model.GSTAmount) : 0,
                            TaxPer = model.GSTPerc != null ? Convert.ToDouble(model.GSTPerc) : 0

                        };
                        Winfocus_CS.TblStudent_Fee.Add(std_fee_info);
                        Winfocus_CS.SaveChanges();
                    }
                    else
                    {
                        TblStudent_Fee std_fee_info = new TblStudent_Fee
                        {
                            Amount = Convert.ToDouble(studentFee.TotalFee),
                            RegID = studentFee.RegID,
                            FeePayTerm = "Term 1",
                            DiscountPer = studentFee.DisPerc.ToString(),
                            DiscountAmount = Convert.ToDouble(studentFee.Discount),
                            //  DiscountAmount = (Convert.ToDouble(studentFee.TotalFee) * Convert.ToDouble(studentFee.Discount)) / 100,
                            PaymentStatus = 1, // 1 for Success,
                            FeeID = Convert.ToInt32(studentFee.Course), // Assuming 1 is the ID for the fee type
                            InstallmentMode = 1, // 1 for Installment Payment
                            PaymentDt = DateTime.Now,
                            PaymentDueDt = null, // Assuming payment due date is 30 days from now
                            IsActive = 1, // 1 for Active
                            CreatedDt = DateTime.Now,
                            IsDeleted = 0, // 0 for Not Deleted
                            DeletedDt = null,
                            TaxAmount = model.GSTAmount != null ? Convert.ToDouble(model.GSTAmount) : 0,
                            TaxPer = model.GSTPerc != null ? Convert.ToDouble(model.GSTPerc) : 0

                        };
                        List<TblStudent_Fee> lstStdFee = new List<TblStudent_Fee>();
                        lstStdFee.Add(std_fee_info);
                        if (studentFee.Term2 != "0")
                        {
                            TblStudent_Fee std_fee_info2 = new TblStudent_Fee
                            {
                                Amount = Convert.ToDouble(studentFee.Term2),
                                RegID = studentFee.RegID,
                                FeePayTerm = "Term 2",
                                DiscountPer = studentFee.DisPerc.ToString(),
                                DiscountAmount = Convert.ToDouble(studentFee.Discount),
                                //DiscountAmount = (Convert.ToDouble(studentFee.Term2) * Convert.ToDouble(studentFee.Discount)) / 100,
                                PaymentStatus = 0, // 0 for Pending
                                FeeID = Convert.ToInt32(studentFee.Course), // Assuming 1 is the ID for the fee type
                                InstallmentMode = 1, // 1 for Installment Payment
                                PaymentDt = null,
                                PaymentDueDt = DateTime.Now.AddDays(30), // Assuming payment due date is 30 days from now
                                IsActive = 1, // 1 for Active
                                CreatedDt = DateTime.Now,
                                IsDeleted = 0, // 0 for Not Deleted
                                DeletedDt = null,
                                
                            };
                            lstStdFee.Add(std_fee_info2);
                        }
                        if (studentFee.Term3 != "0")
                        {
                            TblStudent_Fee std_fee_info3 = new TblStudent_Fee
                            {
                                Amount = Convert.ToDouble(studentFee.Term3),
                                RegID = studentFee.RegID,
                                FeePayTerm = "Term 3",
                                DiscountPer = studentFee.DisPerc.ToString(),
                                DiscountAmount = Convert.ToDouble(studentFee.Discount),
                                //DiscountAmount = (Convert.ToDouble(studentFee.Term3) * Convert.ToDouble(studentFee.Discount)) / 100,
                                PaymentStatus = 0, // 0 for Pending
                                FeeID = Convert.ToInt32(studentFee.Course), // Assuming 1 is the ID for the fee type
                                InstallmentMode = 1, // 1 for Installment Payment
                                PaymentDt = null,
                                PaymentDueDt = DateTime.Now.AddDays(60), // Assuming payment due date is 60 days from now
                                IsActive = 1, // 1 for Active
                                CreatedDt = DateTime.Now,
                                IsDeleted = 0, // 0 for Not Deleted
                                DeletedDt = null,
                                
                            };
                            lstStdFee.Add(std_fee_info3);
                        }
                        if (studentFee.Term4 != "0")
                        {
                            TblStudent_Fee std_fee_info4 = new TblStudent_Fee
                            {
                                Amount = Convert.ToDouble(studentFee.Term4),
                                RegID = studentFee.RegID,
                                FeePayTerm = "Term 4",
                                DiscountPer = studentFee.DisPerc.ToString(),
                                DiscountAmount = Convert.ToDouble(studentFee.Discount),
                                //DiscountAmount = (Convert.ToDouble(studentFee.Term4) * Convert.ToDouble(studentFee.Discount)) / 100,
                                PaymentStatus = 0, // 0 for Pending
                                FeeID = Convert.ToInt32(studentFee.Course), // Assuming 1 is the ID for the fee type
                                InstallmentMode = 1, // 1 for Installment Payment
                                PaymentDt = null,
                                PaymentDueDt = DateTime.Now.AddDays(90), // Assuming payment due date is 90 days from now
                                IsActive = 1, // 1 for Active
                                CreatedDt = DateTime.Now,
                                IsDeleted = 0, // 0 for Not Deleted
                                DeletedDt = null,
                               
                            };
                            lstStdFee.Add(std_fee_info4);
                        }
                        if (studentFee.Term5 != "0")
                        {
                            TblStudent_Fee std_fee_info5 = new TblStudent_Fee
                            {
                                Amount = Convert.ToDouble(studentFee.Term5),
                                RegID = studentFee.RegID,
                                FeePayTerm = "Term 5",
                                DiscountPer = studentFee.DisPerc.ToString(),
                                DiscountAmount = Convert.ToDouble(studentFee.Discount),
                                //DiscountAmount = (Convert.ToDouble(studentFee.Term5) * Convert.ToDouble(studentFee.Discount)) / 100,
                                PaymentStatus = 0, // 0 for Pending
                                FeeID = Convert.ToInt32(studentFee.Course), // Assuming 1 is the ID for the fee type
                                InstallmentMode = 1, // 1 for Installment Payment
                                PaymentDt = null,
                                PaymentDueDt = DateTime.Now.AddDays(120), // Assuming payment due date is 120 days from now
                                IsActive = 1, // 1 for Active
                                CreatedDt = DateTime.Now,
                                IsDeleted = 0, // 0 for Not Deleted
                                DeletedDt = null,
                                
                            };
                            lstStdFee.Add(std_fee_info5);
                        }
                        Winfocus_CS.TblStudent_Fee.AddRange(lstStdFee);
                        Winfocus_CS.SaveChanges();

                    }
                    TblStudent_Payment_History tblStudent_Payment_History = new TblStudent_Payment_History
                    {
                        Amount= model.CurrentPayable,
                        RegID = model.RegID,
                        DiscountUsed = model.Discount,
                        FeeID = Convert.ToInt32(studentFee.Course), // Assuming 1 is the ID for the fee type
                        paymentMode = model.PaymentMode,
                        Status = "Success",
                        paymentType = "Online",
                        OrderID = model.razorpay_order_id,
                        PaymentID = model.razorpay_payment_id,
                        Signature = model.razorpay_signature,
                        TransactionDate = DateTime.Now,
                        TaxPer = model.GSTPerc != null ? Convert.ToDouble(model.GSTPerc) : 0,
                        TaxAmount = model.GSTAmount != null ? Convert.ToDouble(model.GSTAmount) : 0

                    };
                    Winfocus_CS.TblStudent_Payment_History.Add(tblStudent_Payment_History);
                    if (Winfocus_CS.SaveChanges() > 0)
                    {
                      TblStudent_Payment_Terms term_fee_details = Winfocus_CS.TblStudent_Payment_Terms.Where(x => x.RegID == model.RegID).FirstOrDefault();
                        term_fee_details.PaymentStatus = 1; // 1 for Success
                        Winfocus_CS.Entry(term_fee_details).State = System.Data.Entity.EntityState.Modified;
                       if (Winfocus_CS.SaveChanges() > 0)
                        {
                            var studedetails = Winfocus_CS.TblSTudentBasicDetails.Where(x => x.RegId == model.RegID).FirstOrDefault();
                            AddUser(studedetails);
                            SendMail(studedetails);
                        }
                        return RedirectToAction("PaymentSuccess");//payment success page
                    }
                    else
                    {
                        return RedirectToAction("PaymentFailed");//db save failed message
                    }

                        //=================

                        
                }
                else
                {
                    // ❌ Payment verification failed
                    // SavePayment(model.razorpay_payment_id, model.razorpay_order_id, "Failed");
                    var term_fee_details=Winfocus_CS.TblStudent_Payment_Terms.Where(x => x.RegID == model.RegID).FirstOrDefault();
                    TblStudent_Payment_History tblStudent_Payment_History = new TblStudent_Payment_History
                    {
                        Amount = model.TotalPayable,
                        RegID = model.RegID,
                        DiscountUsed = model.Discount,
                        FeeID = Convert.ToInt32(term_fee_details.CourseID), // Assuming 1 is the ID for the fee type
                        paymentMode = model.PaymentMode,
                        Status = "Success",
                        paymentType = "Online",
                        OrderID = model.razorpay_order_id,
                        PaymentID = model.razorpay_payment_id,
                        Signature = model.razorpay_signature,
                        TransactionDate = DateTime.Now

                    };
                    if (Winfocus_CS.SaveChanges() > 0)
                    {
                       
                        return RedirectToAction("Login","Account");//payment success page
                    }
                    else
                    {
                        return RedirectToAction("Login", "Account");//db save failed message
                    }
                   // return RedirectToAction("PaymentFailed");
                }
                //return View();
            }
        }

        public int AddUser(TblSTudentBasicDetail tblS)
        {
            
            try
            {
                User user = new User
                {
                    LastName= tblS.FullName,
                    Email = tblS.EmailID,
                    UserName= tblS.RegId,
                    Password = tblS.DOB, // Assuming DOB is used as a password, which is not recommended for security reasons
                    ActivationCode = Guid.NewGuid().ToString(),
                    IsActive = 1, // 1 for Active
                    IsDeleted = 0, // 0 for Not Deleted
                    CreatedDate= DateTime.Now,
                   RoleId=3, // Assuming 3 is the role ID for Student
                    DeletedBy = 0, // 0 for Not Deleted
                    DeletedDate = DateTime.Now,
                    UniqueID=tblS.RegId, // Assuming RegId is unique for each user
                    MobileNo= tblS.MobileNumber

                };
                Winfocus_CS.Users.Add(user);
                if (Winfocus_CS.SaveChanges() > 0)
                {
                    var findUser = Winfocus_CS.Users.Where(x => x.UniqueID == tblS.RegId).FirstOrDefault();
                    Winfocus_CS.SPinsertRole(findUser.RoleId,findUser.Id); // Assuming 3 is the role ID for Student

                    // Add student basic details
                    tblS.IsActive = 1; // 1 for Active
                    tblS.IsDeleted = 0; // 0 for Not Deleted
                    tblS.ProcessStage = 4;
                    tblS.CreatedDt = DateTime.Now;
                    Winfocus_CS.TblSTudentBasicDetails.Add(tblS);
                    if (Winfocus_CS.SaveChanges() > 0)
                    {
                      
                       
                        return 1; // Success
                    }
                    else
                    {
                        return 0; // Failed to save student details
                    }
                }
                else
                {
                    return 0; // Failed to save user details
                }
            }
            catch (Exception ex)
            {
                throw ex;
                return 0;
            }
        }

        public void SendMail(TblSTudentBasicDetail model)
        {
            var url = string.Format("/Account/login");
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);

            // var link = "https://winfocus.in/courses/login";

            //var fromEmail = new MailAddress("entrancecoachme@gmail.com", "Activation Account - winwintutor.com");
            var fromEmail = new MailAddress("entrancecoachme@gmail.com", "Account Registration - WINFOCUS");

            var toEmail = new MailAddress(model.EmailID);

            //  var fromEmailPassword = "DXB1234567890";mwesxhtcabdgzvhs
            //  var fromEmailPassword = "Enq@1234!!!";
            var fromEmailPassword = "dmzpwtbqmsnscbuc";
            string subject = "Account Registartion !";

            string body = $@"
            <p>Dear {model.FullName},</p>
            <p>Thank you for your payment. Your account is now active. Below are your login credentials:</p>
            <p><b>Username:</b> {model.RegId}<br/>
            <b>Password:</b> {model.DOB}</p>
            <p>You can log in here: <a href='{link}'>Login Now</a></p>
            <p>If you have any questions, feel free to contact us.</p>
            <p>Regards,<br/>Support Team</p>
        ";
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