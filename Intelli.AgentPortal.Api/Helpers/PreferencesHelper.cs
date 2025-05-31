using Intelli.AgentPortal.Domain.Core.Repository;
using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Services
{
    /// <summary>
    /// The column preferences helper class.
    /// Will be used to insert default preferences for a new user.
    /// </summary>
    public class PreferencesHelper
    {
        private readonly AgentPortalContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PreferencesHelper"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PreferencesHelper(AgentPortalContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Sets the default column preferences for newly added user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="userName">The current logged in user name.</param>
        /// <returns>A Task.</returns>
        public async Task SetDefaultAsync(int userId, string userName)
        {
            var columnPreferencesRepository = new GenericRepository<ColumnPreference>(_context);

            var query = _context.ScreenColumns.Where(x => x.DefaultVisibility == true)
                                                .OrderBy(x => x.ScreenId)
                                                .ThenBy(x => x.DefaultOrder);

            var list = await query.ToListAsync();
            foreach (var item in list)
            {
                columnPreferencesRepository.Insert(new ColumnPreference
                {
                    ScreenId = item.ScreenId,
                    ColumnName = item.ColumnName,
                    SystemUserId = userId
                });
            }
            columnPreferencesRepository.SaveChanges(userName, null);
        }
    }
}
