using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WinfocusLearningApp.DataEntity;

namespace WinfocusLearningApp.ViewModels
{
    public class StudentPaymentModel
    {

        public int ID { get; set; }
        public string ReferenceCode { get; set; }
        public string RegID { get; set; }
        public string CourseSelected { get; set; }
        public string PaymentMode { get; set; }
        public Nullable<int> CourseID { get; set; }
        public string PaymentType { get; set; }
        public string TransactionID { get; set; }
        public byte[] UploadSlip { get; set; }
        public Nullable<int> PaymentStatus { get; set; }
        public Nullable<System.DateTime> CreatedDt { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<int> DeletedDt { get; set; }
        public List<FeeTermDiscountModel> FeeTermDiscounts { get; set; }

    }
}