
namespace Intelli.AgentPortal.Shared.Mvc.Resources
{
    /// <summary>
    /// Message String Keys.
    /// </summary>
    public static class MsgKeys
    {
        /// <summary>
        /// "Invalid Input Parameters."
        /// </summary>
        public static string InvalidInputParameters => "invalid_input";
        public static string PathNotExist => "PathNotExist";
        public static string NOK_LockedByOtherAgent => "NOK_LockedByOtherAgent";
        public static string BATCHSET => "BATCHSET";
        public static string NoBatchInQueue => "NoPendingBatchInQueue";
        public static string AllBatchLocked => "AllBatchesAreLocked";
        public static string BatchLocked => "BatchLockedByAnotherAgent";
        public static string NoFileFound => "NoFileFound";

        /// <summary>
        /// "Updated Successfully."
        /// </summary>
        public static string UpdatedSuccessfully => "update_success";

        /// <summary>
        /// "Deleted Successfully."
        /// </summary>
        public static string DeletedSuccessfully => "delete_success";

        /// <summary>
        /// "Created Successfully."
        /// </summary>
        public static string CreatedSuccessfully => "insert_success";

        /// <summary>
        /// "Requested object does not exists in database."
        /// </summary>
        public static string ObjectNotExists => "object_not_exists";

        /// <summary>
        /// "Cannot login user."
        /// </summary>
        public static string UserLoginFailed => "user_login_failed";

        /// <summary>
        /// "Unable to register user."
        /// </summary>
        public static string UserRegistrationFailed => "user_registration_failed";

        /// <summary>
        /// "Login credentials are invalid."
        /// </summary>
        public static string InvalidLoginCredentials => "invalid_login";

        /// <summary>
        /// "Cannot update user."
        /// </summary>
        public static string UserUpdationFailed => "user_updation_failed";

        /// <summary>
        /// "Data you requested is not active."
        /// </summary>
        public static string ObjectNotActive => "not_active";

        /// <summary>
        /// "You are not allowed to perform this operation."
        /// </summary>
        public static string NotAllowed => "not_allowed";

        /// <summary>
        /// "Something went wrong."
        /// </summary>
        public static string SomethingWentWrong => "something_went_wrong";

        /// <summary>
        /// "Email confirmation link sent successfully."
        /// </summary>
        public static string EmailConfirmationLinkSuccess => "email_sent_success";

        /// <summary>
        /// "Unable to send email confirmation link."
        /// </summary>
        public static string EmailConfirmationLinkFailure => "email_sent_failure";

        /// <summary>
        /// "Username is incorrect."
        /// </summary>
        public static string UsernameIsIncorrect => "invalid_credentials";

        /// <summary>
        /// "Password is incorrect."
        /// </summary>
        public static string PasswordIsIncorrect => "invalid_credentials";

        /// <summary>
        /// "User has been deactivated."
        /// </summary>
        public static string UserDeactivated => "user_not_active";

        /// <summary>
        /// "Email address is not verified."
        /// </summary>
        public static string EmailNotVerified => "email_not_verified";

        /// <summary>
        /// "Token is not valid."
        /// </summary>
        public static string InvalidToken => "invalid_token";

        /// <summary>
        /// Gets the user already exists.
        /// </summary>
        public static string UserAlreadyExists => "user_already_exists";

        /// <summary>
        /// Gets the unknown failure.
        /// </summary>
        public static string UnknownFailure => "unknown_failure";

        /// <summary>
        /// Gets the email is taken.
        /// </summary>
        public static string EmailIsTaken => "email_is_taken";

        /// <summary>
        /// Gets the email is invalid.
        /// </summary>
        public static string EmailIsInvalid => "email_is_invalid";

        /// <summary>
        /// Gets the lockout not enabled.
        /// </summary>
        public static string LockoutNotEnabled => "lockout_not_enabled";

        /// <summary>
        /// Gets the optimistic concurrency failure.
        /// </summary>
        public static string OptimisticConcurrencyFailure => "optimistic_concurrency_failure";

        /// <summary>
        /// Gets the password too short.
        /// </summary>
        public static string PasswordTooShort => "password_too_short";

        /// <summary>
        /// Gets the password must have digit.
        /// </summary>
        public static string PasswordMustHaveDigit => "password_must_have_digit";

        /// <summary>
        /// Gets the password must have lowercase.
        /// </summary>
        public static string PasswordMustHaveLowercase => "password_must_have_lowercase";

        /// <summary>
        /// Gets the password must have non alphanumeric.
        /// </summary>
        public static string PasswordMustHaveNonAlphanumeric => "password_must_have_non_alphanumeric";

        /// <summary>
        /// Gets the password must have uppercase.
        /// </summary>
        public static string PasswordMustHaveUppercase => "password_must_have_uppercase";

        /// <summary>
        /// Gets the role name taken.
        /// </summary>
        public static string RoleNameTaken => "role_name_taken";

        /// <summary>
        /// Gets the role name invalid.
        /// </summary>
        public static string RoleNameInvalid => "role_name_invalid";

        /// <summary>
        /// Gets the user has a password set.
        /// </summary>
        public static string UserHasAPasswordSet => "user_has_a_password_set";

        /// <summary>
        /// Gets the user already in role.
        /// </summary>
        public static string UserAlreadyInRole => "user_already_in_role";

        /// <summary>
        /// Gets the user not in role.
        /// </summary>
        public static string UserNotInRole => "user_not_in_role";

        /// <summary>
        /// Gets the email not found.
        /// </summary>
        public static string EmailNotFound => "email_not_found";

        /// <summary>
        /// Password reset link sent successfully.
        /// </summary>
        public static string PasswordResetSent => "password_reset_sent";

        /// <summary>
        /// Gets the record with same name exists.
        /// </summary>
        public static string RecordWithSameNameExists => "with_same_name_exists";

        /// <summary>
        /// Gets the record with same name exists.
        /// </summary>
        public static string InActiveRecordWithSameNameExists => "inactive_with_same_name_exists";

        /// <summary>
        /// Child entity exists.
        /// </summary>
        public static string ChildEntityExists => "related_entity";

        /// <summary>
        /// Gets the already signed in.
        /// </summary>
        public static string AlreadySignedIn => "already_signed_in";

        /// <summary>
        /// Sign in limit exceeded.
        /// </summary>
        public static string SignInLimitExceeded => "sign_in_limit_exceeded";

        /// <summary>
        /// Gets the user signed out.
        /// </summary>
        public static string UserSignedOut => "user_signed_out";

        /// <summary>
        /// Gets the user duplicated.
        /// </summary>
        public static string UserDuplicated => "userDuplicated";

        /// <summary>
        /// Gets the role duplicated.
        /// </summary>
        public static string RoleDuplicated => "roleDuplicated";

        /// <summary>
        /// Gets the code duplicated.
        /// </summary>
        public static string CodeDuplicated => "codeDuplicated";

        /// <summary>
        /// Gets the user already signed in msg key.
        /// </summary>
        public static string UserAlreadySignedIn => "already_signed_in";
    }
}
