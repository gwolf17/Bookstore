﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class AppIdentityDBContext : IdentityDbContext<IdentityUser>  //Inherits from the IdentityDbContext contained in the .Identity file we downloaded
    {
        //Constructor
        public AppIdentityDBContext(DbContextOptions options) : base(options)
        {

        }
    }
}
