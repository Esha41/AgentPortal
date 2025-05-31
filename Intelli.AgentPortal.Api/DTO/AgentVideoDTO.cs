namespace Intelli.AgentPortal.Api.DTO
{
    public class AgentVideoDTO
    {
        public string requestId { get; set; }
        public string AgentController { get; set; }
        public string AlertMessage { get; set; }
        public bool PendingBatchesView { get; set; }
        public bool AgentView { get; set; }
        public bool AgentVideoView { get; set; }
        public bool AgentVideoWithJumio { get; set; }
        public bool IndexOnlyDataView { get; set; }
    }
}
