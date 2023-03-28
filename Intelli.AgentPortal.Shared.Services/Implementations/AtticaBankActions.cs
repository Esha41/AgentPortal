
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Shared.DTO;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Intelli.AgentPortal.Shared.Services.Abstract;

namespace Intelli.AgentPortal.Shared.Services.Implementations
{
    public class AtticaBankActions : ICompanyActions
    {
        public List<CustomerData> AgentCustomerData(Batch batch)
        {
            List<CustomerData> customerInfo = new List<CustomerData>();
            var documentClasseFieldList = DocumentClasseFieldClass.GetAllDocumentClasseFields();
            var fieldsOfInterest = new List<int?> { documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.IdentityIdFirstNameGr)?.Id,
                                                    documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.IdentityIdLastNameGr)?.Id,
                                                    documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.IdentityIdFatherNameGr)?.Id,
                                                    documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.IdentityIdMotherFirstNameGr)?.Id,
                                                   documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.IdentityIdMotherLastNameGr)?.Id,
                                                    documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.IdentityIdBirthDate)?.Id };

            var itemfields = batch.BatchHistories.Where(bh => bh.IsLast)
                .SelectMany(bh => bh.BatchHistoryItems)
                .SelectMany(bhi => bhi.BatchHistoryItemFields)
                .Where(bhif => fieldsOfInterest.Any(foi => foi == bhif.DocumentClassFieldId));

            foreach (var itemField in itemfields)
            {
                var relatedDocumentClassField = documentClasseFieldList.FirstOrDefault(dcf => dcf.Id == itemField.DocumentClassFieldId);
                customerInfo.Add(new CustomerData { Label = relatedDocumentClassField?.Uilabel, Value = itemField.RegisteredFieldValue });
            }

            return customerInfo;
        }

        public List<PradoCheck> PradoChecks(int? DocumentClassId) => new List<PradoCheck>();
        public virtual Dictionary<string, string> GetDocumentsDownloadAccess(string token) => new Dictionary<string, string>();

    }
}
