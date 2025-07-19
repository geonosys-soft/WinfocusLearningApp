using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WinfocusLearningApp.Models
{
    public class LoginModel
    {
        public string Username { get; set; }    
        public string Password { get; set; }
        public string UserId { get; set; }
    }

    public class CustomSerializeModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<string> RoleName { get; set; }

    }
}