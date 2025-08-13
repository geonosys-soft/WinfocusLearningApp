using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WinfocusLearningApp.DataEntity;

namespace WinfocusLearningApp.ViewModels
{
    public class DTPRegistrationModel
    {
        public int ID { get; set; }
        public string RegID { get; set; }
        public string Fullname { get; set; }
        public string DOB { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string Location { get; set; }
        public string Place { get; set; }
        public string Address { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDt { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDt { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDt { get; set; }
        public byte[] ProfilePic { get; set; }
        public byte[] AddressProof { get; set; }
        public string WhatsApp { get; set; }

       public List<TblDTPRegistration> _dtpRegistrationList;

    }
}