using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPTestAPI.Models
{
    public class LoginModel
    {
        public String UserName { get; set; }
        public String Password { get; set; }
        public String UserType { get; set; }
    }

    public class LoginReturnModel
    {
        public bool status { get; set; }
        public string msg { get; set; }
        public string token { get; set; }
    }
}
