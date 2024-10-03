using Demo.DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Identity
{
    public class MagicVillaIdentityDbContext : IdentityDbContext<AppUser>
    {
        public MagicVillaIdentityDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
