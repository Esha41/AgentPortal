namespace Intelli.AgentPortal.Api.DTO
{
    public class ValidDocumentsDTO
    {
        public int? DocumentClassId { get; set; }
        public bool isValid { get; set; }
        public bool? IsBatchItem { get; set; }
        public bool? IncludeInOnboarding { get; set; }
        public string RecognitionMappedName { get; set; }
        public int DocumentGroupNameId { get; set; }
        public bool Mandatory { get; set; }
        public string Code { get; set; }
    }
}
