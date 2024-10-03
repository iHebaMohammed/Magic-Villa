﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
