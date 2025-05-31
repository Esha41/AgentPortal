
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Shared.DTO;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Intelli.AgentPortal.Shared.Services.Abstract;

namespace Intelli.AgentPortal.Shared.Services.Implementations
{
    public class StoiximanCYActions : ICompanyActions
    {
        public List<CustomerData> AgentCustomerData(Batch batch)
        {
            List<CustomerData> customerInfo = new List<CustomerData>();

            var documentClasseFieldList = DocumentClasseFieldClass.GetAllDocumentClasseFields();
            var batchSourcesList = DocumentClasseFieldClass.GetAllBatchSources();

            if (batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.StoiximanCY)?.Id || batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.StoiximanCY_PoA)?.Id)
            {
                var FirstName = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocFirstNameGr)?.Id)?.FieldValue;
                var LastName = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocLastNameGr)?.Id)?.FieldValue;
                var Birthdate = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocBirthDate)?.Id)?.FieldValue;
                var CountryOfOrigin = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocCountryOfOrigin)?.Id)?.FieldValue;
                var Address = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocAddress)?.Id)?.FieldValue;
                var City = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocCity)?.Id)?.FieldValue;

                //customerInfo.Add(new CustomerData { Label = "First Name", Value = FirstName });
                //customerInfo.Add(new CustomerData { Label = "Last Name", Value = LastName });
                //customerInfo.Add(new CustomerData { Label = "Birthdate", Value = Birthdate });
                customerInfo.Add(new CustomerData { Label = "Country of Origin", Value = CountryOfOrigin });
                //customerInfo.Add(new CustomerData { Label = "Address", Value = Address });
                //customerInfo.Add(new CustomerData { Label = "City", Value = City });
            }
            else
            {
                customerInfo.Add(new CustomerData { Label = "Warning", Value = "Flow Has Not Been Implemented Yet" });
            }
            return customerInfo;
        }

        public List<PradoCheck> PradoChecks(int? DocumentClassId)
        {
            List<PradoCheck> AlphaPradoCheckList = new List<PradoCheck>();
            return AlphaPradoCheckList;
        }
        public virtual Dictionary<string, string> GetDocumentsDownloadAccess(string token) => new Dictionary<string, string>();

    }
}
