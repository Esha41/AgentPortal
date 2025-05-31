
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Shared.DTO;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Intelli.AgentPortal.Shared.Services.Abstract;

namespace Intelli.AgentPortal.Shared.Services.Implementations
{
    public class WindActions : ICompanyActions
    {
        public List<CustomerData> AgentCustomerData(Batch batch)
        {
            var customerInfo = new List<CustomerData>();
            var documentClasseFieldList = DocumentClasseFieldClass.GetAllDocumentClasseFields();
            var batchSourcesList = DocumentClasseFieldClass.GetAllBatchSources();

            if (batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.WindMVP)?.Id)
            {
                var identityFirstNameFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("FirstName")).Select(x => x.Id).ToList();
                var identityLastNameFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("LastName")).Select(x => x.Id).ToList();

                var email = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocEmail)?.Id)?.FieldValue;
                var mobile = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocMobile)?.Id)?.FieldValue;
                var firstName = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityFirstNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var lastName = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityLastNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;

                customerInfo.Add(new CustomerData { Label = "E-mail", Value = email });
                customerInfo.Add(new CustomerData { Label = "Mobile Phone", Value = mobile });
                customerInfo.Add(new CustomerData { Label = "First Name", Value = firstName });
                customerInfo.Add(new CustomerData { Label = "Last Name", Value = lastName });

            }
            else if (batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.WindPostPaid)?.Id)
            {
                var identityFirstNameFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("FirstName")).Select(x => x.Id).ToList();
                var identityLastNameFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("LastName")).Select(x => x.Id).ToList();

                var email = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocEmail)?.Id)?.FieldValue;
                var mobile = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocPhone)?.Id)?.FieldValue;
                var firstName = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityFirstNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var lastName = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityLastNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var delieryTimeslot = batch.BatchMeta.Where(bm => bm.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocDeliveryTimeslot)?.Id).Select(bm => bm.FieldValue).FirstOrDefault();

                var sourceURL = batch.BatchMeta.FirstOrDefault(b => b.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocSourceURL)?.Id)?.FieldValue;

                customerInfo.Add(new CustomerData { Label = "E-mail", Value = email });
                customerInfo.Add(new CustomerData { Label = "Mobile Phone", Value = mobile });
                customerInfo.Add(new CustomerData { Label = "First Name", Value = firstName });
                customerInfo.Add(new CustomerData { Label = "Last Name", Value = lastName });
                customerInfo.Add(new CustomerData { Label = "Source Url", Value = sourceURL });
                customerInfo.Add(new CustomerData { Label = "Delivery Timeslot", Value = delieryTimeslot });

                var showDiffAddressFields = Convert.ToBoolean(batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocHasDifferentAddress)?.Id)?.FieldValue.Equals("true", StringComparison.OrdinalIgnoreCase));

                if (showDiffAddressFields)
                {
                    var city = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocCity1)?.Id)?.FieldValue;
                    var address1 = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocAddress1)?.Id)?.FieldValue;
                    var addressNo1 = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocAddressNo1)?.Id)?.FieldValue;
                    var postalCode1 = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocPostal1)?.Id)?.FieldValue;

                    customerInfo.Add(new CustomerData { Label = "City", Value = city });
                    customerInfo.Add(new CustomerData { Label = "Address", Value = address1 });
                    customerInfo.Add(new CustomerData { Label = "Address number", Value = addressNo1 });
                    customerInfo.Add(new CustomerData { Label = "Postal Code", Value = postalCode1 });
                }
            }
            else
            {
                customerInfo.Add(new CustomerData { Label = "Warning", Value = "Flow Has Not Yet Been Implemented" });
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
