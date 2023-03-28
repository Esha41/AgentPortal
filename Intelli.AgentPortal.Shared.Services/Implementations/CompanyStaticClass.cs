using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.Domain.Model;

namespace Intelli.AgentPortal.Shared.Services.Implementations
{
    /// <summary>
    /// The Company Static Class.
    /// </summary>
    public static class CompanyStaticClass
    {
        private static List<Company> companyList = new List<Company>();
        static CompanyStaticClass()
        {
            AgentPortalContext _context = new AgentPortalContext();
            companyList.AddRange(_context.Companies.ToList());
        }

        /// <summary>
        ///  Get Company By Name.
        /// </summary>
        /// <param name="companyName">The companyName.</param>
        /// <returns>Company.</returns>
        public static Company GetCompanyByName(string companyName)
        {
            return companyList.Where(d=>d.Name== companyName).FirstOrDefault();
        }
    }
}
