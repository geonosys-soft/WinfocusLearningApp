using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WinfocusLearningApp.ViewModels
{
    public class StudentPaymentPreviwModel
    {
        public string RegID { get; set; }
        public string StudentName { get; set; }
        public string DOB { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string MarchentOrderId { get; set; }
        public string AcademicYear { get; set; }
        public string Syllabus { get; set; }
        public string Grade { get; set; }
        public string Program { get; set; }
        public string Course { get; set; }
        public string ReferenceCode { get; set; }
        public DateTime CodeUsedDate { get; set; }
        public string RegistrationFee { get; set; }
        public string CourseFee { get; set; }
        public string TotalFee { get; set; }
        public string Discount { get; set; }
        public double DisPerc { get; set; }

         public string Term1 { get; set; }
        public string Term2 { get; set; }
        public string Term3 { get; set; }
        public string Term4 { get; set; }
        public string Term5 { get; set; }
        public string Day1 { get; set; }
        public string Day2 { get; set; }
        public string Day3 { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentReference { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentResponse { get;set; }
        public string PaymentResponseCode { get; set; }
        public string PaymentResponseMessage { get; set; }
        public string PaymentResponseDate { get; set; }
        public string PaymentResponseTime { get; set; }
        public string CurrentPayable { get; set; }
        public string GSTPerc { get; set; }
        public string GSTAmount { get; set; }
        public string TotalPayable { get; set; }

        public string razorpay_payment_id { get; set; }
        public string razorpay_order_id { get; set; }
        public string razorpay_signature { get; set; }

        public string MOrderID { get; set; }

    }
}