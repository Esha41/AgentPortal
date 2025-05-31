using AutoMapper;
using Intelli.AgentPortal.Api.Helpers;
using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Services
{
    /// <summary>
    /// The custom password validator.
    /// </summary>
    public class AgentPortalPasswordValidator : PasswordValidator<AspNetUser>
    {
        private readonly AgentPortalContext _context;
        private readonly int RestrictLastUsedPasswords;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentPortalPasswordValidator"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The auto mapper.</param>
        public AgentPortalPasswordValidator(AgentPortalContext context, IMapper mapper)
        {
            _context = context;

            var dto = ConfigurationHelper.Read(context, mapper);
            RestrictLastUsedPasswords = dto.RestrictLastUsedPasswords;
        }

        /// <summary>
        /// Validates the password.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns>A Task.</returns>
        public override async Task<IdentityResult> ValidateAsync(UserManager<AspNetUser> manager, AspNetUser user, string password)
        {
            if (RestrictLastUsedPasswords > 0)
            {
                // Cannot use last password(s)
                var query = _context.PasswordHistories.AsQueryable()
                                        .Where(x => x.SystemUserId == user.SystemUserId)
                                        .OrderByDescending(x => x.Id)
                                        .Take(RestrictLastUsedPasswords);

                var list = await query.ToListAsync();

                if (list.Count > 0)
                {
                    string passwordHash = await StringHasher.GetHashAsync(password);

                    if (list.Exists(x => x.PasswordHash == passwordHash))
                        return await Task.FromResult(IdentityResult.Failed(new IdentityError
                        {
                            Code = "OldPassword",
                            Description = $"You cannot use your last {RestrictLastUsedPasswords} password(s)"
                        }));
                }
            }

            // If all validations passed return success
            return await Task.FromResult(IdentityResult.Success);
        }
    }
}
