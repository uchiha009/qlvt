using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quanlyvatut.Models.back_end
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int CodeID { get; set; }

        public virtual Role Role { get; set; }
    }
}