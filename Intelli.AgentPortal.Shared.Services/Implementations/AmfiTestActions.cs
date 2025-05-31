
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Shared.DTO;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Intelli.AgentPortal.Shared.Services.Abstract;

namespace Intelli.AgentPortal.Shared.Services.Implementations
{
    public class AmfiTestActions : ICompanyActions
    {
        public List<CustomerData> AgentCustomerData(Batch batch)
        {
            var customerInfo = new List<CustomerData>();
            var documentClasseList = DocumentClasseFieldClass.GetAllDocumentClasses();
            var documentClasseFieldList = DocumentClasseFieldClass.GetAllDocumentClasseFields();
            var batchSourcesList = DocumentClasseFieldClass.GetAllBatchSources();

            if (batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.amfiTest)?.Id)
            {
                var identityFirstNameFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("FirstName")).Select(x => x.Id).ToList();
                var identityLastNameFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("LastName")).Select(x => x.Id).ToList();
                var identityIdNumberFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("IDNumber")).Select(x => x.Id).ToList();
                var identityBirthDateFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("BirthDate")).Select(x => x.Id).ToList();

                var firstName = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityFirstNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var lastName = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityLastNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var birthDate = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityBirthDateFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var idNumber = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityIdNumberFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;

                customerInfo.Add(new CustomerData { Label = "First Name", Value = firstName });
                customerInfo.Add(new CustomerData { Label = "Last Name", Value = lastName });
                customerInfo.Add(new CustomerData { Label = "Birth Date", Value = birthDate });
                customerInfo.Add(new CustomerData { Label = "Id Number", Value = idNumber });
            }
            else if (batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.eKYCIdentity)?.Id)
            {
                var identityFirstNameFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("FirstName")).Select(x => x.Id).ToList();
                var identityLastNameFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("LastName")).Select(x => x.Id).ToList();
                var identityIdNumberFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("IDNumber")).Select(x => x.Id).ToList();
                var identityBirthDateFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("BirthDate")).Select(x => x.Id).ToList();

                var firstName = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity /*&& (bool)y.IsValid*/)?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityFirstNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var lastName = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity /*&& (bool)y.IsValid*/)?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityLastNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var birthDate = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity /*&& (bool)y.IsValid*/)?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityBirthDateFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var idNumber = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity /*&& (bool)y.IsValid*/)?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityIdNumberFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;

                customerInfo.Add(new CustomerData { Label = "First Name", Value = firstName });
                customerInfo.Add(new CustomerData { Label = "Last Name", Value = lastName });
                customerInfo.Add(new CustomerData { Label = "Birth Date", Value = birthDate });
                customerInfo.Add(new CustomerData { Label = "Id Number", Value = idNumber });
            }
            else
            {
                customerInfo.Add(new CustomerData { Label = "Warning", Value = "Flow Has Not Been Implemented Yet" });
            }

            return customerInfo;
        }

        public List<PradoCheck> PradoChecks(int? DocumentClassId)
        {
            List<PradoCheck> VivaPradoCheckList = new List<PradoCheck>();  //calling static methods from static class
            var documentClasseFieldList = DocumentClasseFieldClass.GetAllDocumentClasseFields();
            var documentClassList = DocumentClasseFieldClass.GetAllDocumentClasses();

            VivaPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoWatermark).FirstOrDefault()?.Id : null, UILabel = "Watermark" });
            VivaPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoGuillochesFineLinePatterns).FirstOrDefault()?.Id : null, UILabel = "Guilloches / Fine Line Patterns" });
            VivaPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoLaminate).FirstOrDefault()?.Id : null, UILabel = "Laminate" });
            VivaPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoSubstrateSecurityThread).FirstOrDefault()?.Id : null, UILabel = "Substrate Security Thread" });
            VivaPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoBiographicalData).FirstOrDefault()?.Id : null, UILabel = "Biographical Data" });

            if (DocumentClassId != null && (DocumentClassId == documentClassList.Where(d => d.EnumValue == DocumentClassKeys.EUId).FirstOrDefault()?.Id || DocumentClassId == documentClassList.Where(d => d.EnumValue == DocumentClassKeys.Passport).FirstOrDefault()?.Id))
            {
                VivaPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoMRZZone).FirstOrDefault()?.Id : null, UILabel = "MRZ Zone " });
                VivaPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoPhotoAndSignature).FirstOrDefault()?.Id : null, UILabel = "Photo N Signature" });
                VivaPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoOpticalSecurityElements).FirstOrDefault()?.Id : null, UILabel = "Optical Security Elements" });
            }

            return VivaPradoCheckList;
        }
        public virtual Dictionary<string, string> GetDocumentsDownloadAccess(string token) => new Dictionary<string, string>();

    }
}
