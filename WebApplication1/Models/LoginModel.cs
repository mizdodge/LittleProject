using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class LoginModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string rolename { get; set; }
        public string token { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public bool IsAdmin { get; set; }
    }
    public class ListLogin
    {
        public LoginModel data { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }
}
