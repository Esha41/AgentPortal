
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Shared.DTO;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Intelli.AgentPortal.Shared.Services.Abstract;

namespace Intelli.AgentPortal.Shared.Services.Implementations
{
    public class eIDASActions : ICompanyActions
    {
        public List<CustomerData> AgentCustomerData(Batch batch)
        {
            List<CustomerData> customerInfo = new List<CustomerData>();
            var documentClasseFieldList = DocumentClasseFieldClass.GetAllDocumentClasseFields();
            var batchSourcesList = DocumentClasseFieldClass.GetAllBatchSources();

            if (batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.eIDAS1)?.Id ||
          batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.eIDAS2)?.Id)
            {
                var identityFirstNameFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("FirstName")).Select(x => x.Id).ToList();
                var identityLastNameFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("LastName")).Select(x => x.Id).ToList();
                var firstName = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityFirstNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var lastName = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityLastNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                customerInfo.Add(new CustomerData { Label = "First Name", Value = firstName });
                customerInfo.Add(new CustomerData { Label = "Last Name", Value = lastName });
            }
            else
            {
                customerInfo.Add(new CustomerData { Label = "Warning", Value = "Flow Has Not Been Implemented Yet" });
            }
            return customerInfo;
        }

        public List<PradoCheck> PradoChecks(int? DocumentClassId)
        {
            return new List<PradoCheck>();
        }
        public virtual Dictionary<string, string> GetDocumentsDownloadAccess(string token) => new Dictionary<string, string>();

    }
}
