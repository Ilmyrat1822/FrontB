using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FrontB.Classes
{
    public class UserClass
    {
       public string? Name { get; set; }
       public string? Surname { get; set; }
       public string? Username { get; set; }
       public string? Status { get; set; }
       public string? Granted { get; set; }

    }
    public class LoginResponse
    {
        public Model_Login data { get; set; }
    }
    public class Model_Login
    {        
        string _token = "";
        string _refresh_token = "";
        public string token { get { return _token; } set { if (value == null) _token = " "; else _token = value; } }
        public string refresh_token { get { return _refresh_token; } set { if (value == null) _refresh_token = " "; else _refresh_token = value; } }
        public Admin admin { get; set; }    
    }
    public class Admin
    {   
        string _id = "";
        string _name = "";
        string _surname = "";
        string _username = "";
        string _granted = "";
        
        public string? id { get { return _id; } set { if (value == null) _id = " "; else _id = value; } }
        public string? name { get { return _name; } set { if (value == null) _name = " "; else _name = value; } }
        public string? surname { get { return _surname; } set { if (value == null) _surname = " "; else _surname = value; } }
        public int? status { get; set; }
        public string? username { get { return _username; } set { if (value == null) _username = " "; else _username = value; } }
        public string? granted { get { return _granted; } set { if (value == null) _granted = " "; else _granted = value; } }
    }

}