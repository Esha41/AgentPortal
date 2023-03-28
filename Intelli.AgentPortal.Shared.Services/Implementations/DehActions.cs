
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Shared.DTO;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Intelli.AgentPortal.Shared.Services.Abstract;

namespace Intelli.AgentPortal.Shared.Services.Implementations
{
    public class DehActions : ICompanyActions
    {

        public List<CustomerData> AgentCustomerData(Batch batch)
        {
            List<CustomerData> customerInfo = new List<CustomerData>();
            var documentClasseFieldList = DocumentClasseFieldClass.GetAllDocumentClasseFields();
            var batchSourcesList = DocumentClasseFieldClass.GetAllBatchSources();

            var Phone = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocPhone)?.Id)?.FieldValue;
            var Email = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocEmail)?.Id)?.FieldValue;

            customerInfo.Add(new CustomerData { Label = "Phone", Value = Phone });
            customerInfo.Add(new CustomerData { Label = "Email", Value = Email });

            if (batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.DEH_SupplierChangeF)?.Id || batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.DEH_StartF)?.Id ||
                batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.DEH_ReStartF)?.Id || batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.DEH_NameChangeF)?.Id)
            {
                var FirstName = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocFirstNameGr)?.Id)?.FieldValue;
                var LastName = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocLastNameGr)?.Id)?.FieldValue;

                customerInfo.Add(new CustomerData { Label = "First Name", Value = FirstName });
                customerInfo.Add(new CustomerData { Label = "Last Name", Value = LastName });

            }
            else if (batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.DEH_SupplierChangeN)?.Id || batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.DEH_NameChangeN)?.Id ||
                batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.DEH_StartN)?.Id || batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.DEH_ReStartN)?.Id)
            {
                var CompanyName = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocCompanyName)?.Id)?.FieldValue;
                var AdminLastName = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocCompanyAdminLastNameGR)?.Id)?.FieldValue;
                var AdminFirstName = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocCompanyAdminFirstNameGR)?.Id)?.FieldValue;

                customerInfo.Add(new CustomerData { Label = "Company Name", Value = CompanyName });
                customerInfo.Add(new CustomerData { Label = "Admin Last Name", Value = AdminLastName });
                customerInfo.Add(new CustomerData { Label = "Admin First Name", Value = AdminFirstName });
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
