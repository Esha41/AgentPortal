using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Shared.DTO;
using System.Collections.Generic;

namespace Intelli.AgentPortal.Api.DTO
{
    public class AgentViewDTO
    {
        public AgentViewDTO(string companyLogoName)
        {
            CompanyLogo = companyLogoName;
        }

        public AgentViewDTO()
        {
            List<DboVwDocumentGroupNamesForBatchHistory> BatchHistoryItems = new List<DboVwDocumentGroupNamesForBatchHistory>();
            List<VwGetBatchHistoryMeta> BatchHistoryMetas = new List<VwGetBatchHistoryMeta>();
            List<BatchItemVMDTO> BatchItems = new List<BatchItemVMDTO>();
            List<UploadedDocumentsDTO> SignedDocuments = new List<UploadedDocumentsDTO>();
            CustomerModel CustomerModel = new CustomerModel();
            Batch batch = new Batch();
            BatchHistoriesDTO batchHistoryLast = new BatchHistoriesDTO();
            List<string> VerificationRejectionReasons = new List<string>();
            List<BatchSourceUploadDocDTO> BatchSourceUploadDocs = new List<BatchSourceUploadDocDTO>();
         //   List<vw_InfoDocClassField> InfoDocClassFields = new List<vw_InfoDocClassField>();
            List<VwRegisterPersonalInfoField> RegisterPersonalInfoFields = new List<VwRegisterPersonalInfoField>();
            List<BM> inputs = new List<BM>();
            List<CustomerData> CustomerInfo = new List<CustomerData>();
            List<PradoCheck> PradoCheckList = new List<PradoCheck>();
            List<VideoValidationControlModel> vvcmList = new List<VideoValidationControlModel>();

            VideoValidity = false.ToString();
            VideoSessionResult = "User Abandoned";
            VideoVerificationStatus = "";
        }
        public int ClientConcentId { get; set; }
        public int FeeWillId { get; set; }
        public int LivenessId { get; set; }
        public int VerificationStatusId { get; set; }
        public int SimilarityByAgentId { get; set; }
        public int VideoSessionResultId { get; set; }
        public int VideoVerificationStatusId { get; set; }
        public int VideoRejectionReasonId { get; set; }
        public int VideoValidityId { get; set; }
        public int batchNeedDocuments { get; set; }
        public List<PradoCheck> PradoCheckList { get; set; }
        public string CompanyLogo { get; set; }
        public PendingBatchesDTO pendingBatches { get; set; } = new();
        public List<DboVwDocumentGroupNamesForBatchHistory> BatchHistoryItems { get; set; }
        public List<VwGetBatchHistoryMeta> BatchHistoryMetas { get; set; }
        public List<BatchItemVMDTO> BatchItems { get; set; }
        public List<UploadedDocumentsDTO> SignedDocuments { get; set; }
        public CustomerModel CustomerModel { get; set; }
        public string Token { get; set; }
        public FullBatchDto batch { get; set; }
        public BatchHistoriesDTO batchHistoryLast { get; set; }
        public List<string> VerificationRejectionReasons { get; set; }
        public string FaceMatching { get; set; }
        public string IsAlive { get; set; }
        public string VerificationStatus { get; set; }
        public string TokBoxApiKey { get; set; }
        public string TokBoxSessionId { get; set; }
        public string TokBoxSessionToken { get; set; }
        public string TokBoxSRID { get; set; }
        public List<BatchSourceUploadDocDTO> BatchSourceUploadDocs { get; set; }
        public List<VwRegisterPersonalInfoField> RegisterPersonalInfoFields { get; set; }
        public List<BM> inputs { get; set; }
        public List<CustomerData> CustomerInfo { get; set; }
        public bool AllUploadedDocumentsExist { get; set; }
        public string VideoSessionResult { get; set; }
        public string VideoValidity { get; set; }
        public string VideoVerificationStatus { get; set; }
        public string ClientConsents { get; set; }

        public List<VideoValidationControlModel> vvcmList { get; set; }
        public string SelfiePortraitSimilarity { get; set; }

    }

    public class BM
    {
        public string FriendlyName { get; set; }
        public string FieldValue { get; set; }
    }

    public class VideoValidationControlModel
    {
        public DocumentClassFieldDTO DocumentClassField { get; set; }
        public string FieldValue { get; set; }
    }
}
