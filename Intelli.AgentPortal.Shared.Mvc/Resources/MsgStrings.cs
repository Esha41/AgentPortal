
namespace Intelli.AgentPortal.Shared.Mvc.Resources
{
    /// <summary>
    /// Message Strings.
    /// </summary>
    public static class MsgStrings
    {
        /// <summary>
        /// Gets the msg string from key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A msg string.</returns>
        public static string GetMsg(string key)
        {
            return key switch
            {
                "user_not_active" => "User is not active.",
                "sign_in_limit_exceeded" => "Maximum limit of users allowed to sign in for your company reached.",
                "already_signed_in" => "Your account is already signed in.",
                _ => key,
            };
        }
    }
}
