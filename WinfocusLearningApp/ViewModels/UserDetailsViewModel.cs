﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WinfocusLearningApp.DataEntity;

namespace WinfocusLearningApp.ViewModels
{
    public class UserDetailsViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int RoleId { get; set; }
        public int IsActive { get; set; }
        public int IsDeleted { get; set; }
        public int DeletedBy { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public string UniqueID { get; set; }
        public string MobileNo { get; set; }
        public string ActivationCode { get; set; }
        public int EmailVerify { get; set; }
        public string Picture { get; set; }
        public string ResetPasswordCode { get; set; }
        public string RoleName { get; set; }    

        public List<UserDetailsViewModel> UserDetails { get; set; }

    }
}