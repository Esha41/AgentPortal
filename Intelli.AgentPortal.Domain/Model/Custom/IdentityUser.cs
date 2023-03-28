using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Domain.Model.Custom
{
    public class IdentityUser :  IdentityUser<int>
    {
        //
        // Summary:
        //     Initializes a new instance of Microsoft.AspNetCore.Identity.IdentityUser.
        //
        // Remarks:
        //     The Id property is initialized to form a new GUID string value.
        public IdentityUser()
        {
          
            SecurityStamp = Guid.NewGuid().ToString();
        }

        //
        // Summary:
        //     Initializes a new instance of Microsoft.AspNetCore.Identity.IdentityUser.
        //
        // Parameters:
        //   userName:
        //     The user name.
        //
        // Remarks:
        //     The Id property is initialized to form a new GUID string value.
        public IdentityUser(string userName)
            : this()
        {
            UserName = userName;
        }

    }
}
