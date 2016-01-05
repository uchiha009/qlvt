﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quanlyvatut.Models.back_end
{
    public class Role
    {
        public int ID { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}