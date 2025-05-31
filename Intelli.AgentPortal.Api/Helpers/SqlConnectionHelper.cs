using Intelli.AgentPortal.Domain;
using Intelli.AgentPortal.Shared.Dto;
using System.Data.SqlClient;

namespace Intelli.AgentPortal.Api.Helpers
{
    public static class SqlConnectionHelper
    {
        public static string ToConnectionString(SqlConnectionConfigurationDTO config)
        {
            // Create a new SqlConnectionStringBuilder and
            // initialize it with a few name/value pairs.
            var builder = new SqlConnectionStringBuilder();

            builder.Add("Data Source", config.Server);
            builder.Add("Initial Catalog", config.InitialCatalog);

            if (config.IntegratedSecurity)
            {
                builder.Add("Integrated Security", config.IntegratedSecurity);
            }
            else
            {
                builder.Add("User Id", config.User);
                var decryptedPassword = EncryptionHelper.DecryptString(config.Password);
                builder.Add("Password", decryptedPassword);
            }

            return builder.ConnectionString;
        }
    }
}
