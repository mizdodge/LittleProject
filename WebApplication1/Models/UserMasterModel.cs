using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterServices.Model
{
    public class UserMasterModel
    {
        public Guid ID { get; set; }
        public String Username { get; set; }
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public String Password { get; set; }
        public bool IsAdmin { get; set; }   
        public Guid UserDetailID{get;set; }
        public UserDetailMaster UserDetail { get; set; }
    }
    public class UserDetailMaster
    {
        public Guid ID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Postalcode { get; set; }
    }
    }
