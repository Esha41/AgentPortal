using Intelli.AgentPortal.Api.DTO;
using Intelli.AgentPortal.Auth.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Services
{

    /// <summary>
    /// Interface for authentication service
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="loginDTO">The login DTO.</param>
        /// <returns>User login result containing appropriate message if login failed.</returns>
        Task<UserLoginResult> ValidateUser(LoginDTO loginDTO, bool forceSignIn);

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="registrationDTO">The registration DTO.</param>
        /// <returns>A thread task to return the MS identity result.</returns>
        Task<UserRegistrationResult> RegisterUser(UserReadDTO registrationDTO);

        /// <summary>
        /// Verifies the email.
        /// </summary>
        /// <param name="dto">The verify email dto.</param>
        /// <returns>A Task of user login result.</returns>
        Task<UserLoginResult> VerifyEmail(VerifyEmailDTO dto);

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>A Task.</returns>
        Task<UserRegistrationResult> UpdateUser(UserReadDTO dto);

        /// <summary>
        /// Updates the user credentials.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>A Task.</returns>
        Task<UserRegistrationResult> UpdateUserCredentials(UserCredentialsDTO dto);

        /// <summary>
        /// Resends the email confirmation.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>True if email send successfully, else returns false.</returns>
        Task<bool> ResendEmailConfirmation(int userId);

        /// <summary>
        /// Sends the forgot password link to user's email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>Empty string if email sent successfully, else returns error message.</returns>
        Task<string> SendPasswordResetLink(string email);

        List<int> GetAssociatedCompaniesId(int userId);
    }
}
