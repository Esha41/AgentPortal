namespace Intelli.AgentPortal.Api.DTO
{
    public class VwRegisterPersonalInfoFieldDTO
    {
        public int? BatchId { get; set; }
        public int BatchSourceId { get; set; }
        public int? Id { get; set; }
        public string EnumValue { get; set; }
        public string FieldValue { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsMandatory { get; set; }
        public int? Ordering { get; set; }
    }
}
