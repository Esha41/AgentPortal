using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Domain.Model.Custom
{
    public class CustomIdentityDbContext<TUser> : IdentityDbContext<TUser, CustomIdentityRole, int> where TUser : IdentityUser
    {
        public CustomIdentityDbContext(DbContextOptions options) : base(options) { }

       
        protected CustomIdentityDbContext() { }
    }
}
