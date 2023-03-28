namespace Intelli.AgentPortal.Shared.Dto
{
    public class SqlConnectionConfigurationDTO
    {
        public string Server { get; set; }
        public string InitialCatalog { get; set; }
        public bool IntegratedSecurity { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
