//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WinfocusLearningApp.DataEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblSTudentBasicDetail
    {
        public int SID { get; set; }
        public string RegId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string WhatsApp { get; set; }
        public string EmailID { get; set; }
        public string MteamID { get; set; }
        public byte[] Profile { get; set; }
        public Nullable<System.DateTime> CreatedDt { get; set; }
        public Nullable<int> IsActive { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<int> ProcessStage { get; set; }
        public string BOTIM { get; set; }
        public string VISA { get; set; }
        public Nullable<int> Gender { get; set; }
        public string DOB { get; set; }
    }
}
