using Intelli.AgentPortal.Domain;
using Intelli.AgentPortal.Domain.Model.Core;
using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    /// <summary>
    /// The asp net role.
    /// </summary>
    public partial class SystemRole : ICompanyEntity
    {
        public void ReduceResponseSize()
        {
            this.Company.SystemRoles = null;
            this.Company.DocumentsPerCompany = null;
            this.Company.UserCompanies = null;
            this.SystemUserRoles = null;
            this.Company.UsersPerCompany = EncryptionHelper.DecryptUsersPerCompany(this.Company.UsersPerCompany).ToString();
        }
    }

}
