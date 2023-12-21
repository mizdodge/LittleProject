using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class UserModel
    {
        public Guid ID { get; set; }
        public String Username { get; set; }
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public String Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
