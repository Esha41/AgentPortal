using System;

namespace Intelli.AgentPortal.Api.DTO
{
    public class BatchItemVMDTO
    {
        public BatchItemVMDTO()
        { }

        public BatchItemVMDTO(BatchItemViewDTO batchItemViewModel, string requestId = "")
        {
            this.BatchItemId = batchItemViewModel.BatchItemId;
            this.BatchId = batchItemViewModel.BatchId;
            this.BatchSourceCode = batchItemViewModel.BatchSourceCode;
            this.DocumentGroupName = batchItemViewModel.DocumentGroupName;
            this.DocumentGroupId = batchItemViewModel.DocumentGroupId;
            this.FileName = batchItemViewModel.FileName;
            this.CreatedDate = batchItemViewModel.CreatedDate;
            this.DocumentClassId = batchItemViewModel.DocumentClassId;
            this.IsValid = batchItemViewModel.IsValid;
            this.RequestId = requestId;
        }
        public int BatchItemId { get; set; }
        public int BatchId { get; set; }
        public string BatchSourceCode { get; set; }
        public string DocumentGroupName { get; set; }
        public int DocumentGroupId { get; set; }
        public string FileName { get; set; }
        public string RequestId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int DocumentClassId { get; set; }
        public bool IsValid { get; set; }
    }
}
