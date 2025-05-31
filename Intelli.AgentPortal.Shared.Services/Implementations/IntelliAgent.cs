
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Shared.DTO;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Intelli.AgentPortal.Shared.Services.Abstract;

namespace Intelli.AgentPortal.Shared.Services.Implementations
{
    /// <summary>
    /// Implementations for Company Actions
    /// </summary>
    public class IntelliAgent : ICompanyActions
    {
        /// <summary>
        /// Get company specific customer data.
        /// </summary>
        /// <param name="Batch">The batch model.</param>
        /// <returns>CustomerData list.</returns>
        public List<CustomerData> AgentCustomerData(Batch batch)
        {
            List<CustomerData> customerInfo = new List<CustomerData>();

            //calling static methods from static class
            var documentClasseFieldList = DocumentClasseFieldClass.GetAllDocumentClasseFields();

            var identityFirstNameFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("FirstName")).Select(x => x.Id).ToList();
            var identityLastNameFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("LastName")).Select(x => x.Id).ToList();
            var identityIdNumberFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("IDNumber")).Select(x => x.Id).ToList();
            var identityBirthDateFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("BirthDate")).Select(x => x.Id).ToList();
            var identityFatherNameFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("FatherFirstName")).Select(x => x.Id).ToList();
            var identityMotherNameFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("MotherFirstName")).Select(x => x.Id).ToList();

            var firstName = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityFirstNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
            var lastName = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityLastNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
            var IdNumber = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityIdNumberFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
            var birthDate = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityBirthDateFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
            var fatherFirstName = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityFatherNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
            var motherFirstName = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityMotherNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;

            customerInfo.Add(new CustomerData { Label = "First Name", Value = firstName });
            customerInfo.Add(new CustomerData { Label = "Last Name", Value = lastName });
            customerInfo.Add(new CustomerData { Label = "Id Number", Value = IdNumber });
            customerInfo.Add(new CustomerData { Label = "Birth Date", Value = birthDate });
            customerInfo.Add(new CustomerData { Label = "Father's Name", Value = fatherFirstName });
            customerInfo.Add(new CustomerData { Label = "Mother's Name", Value = motherFirstName });

            return customerInfo;
        }

        /// <summary>
        /// Get company specific Prado Checks.
        /// </summary>
        /// <param name="DocumentClassId">TheDocumentClassId.</param>
        /// <returns>PradoCheck list.</returns>
        public List<PradoCheck> PradoChecks(int? DocumentClassId)
        {
            List<PradoCheck> AlphaPradoCheckList = new List<PradoCheck>();
            //calling static methods from static class
            var documentClasseFieldList = DocumentClasseFieldClass.GetAllDocumentClasseFields();
            var documentClassList = DocumentClasseFieldClass.GetAllDocumentClasses();

            AlphaPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoWatermark).FirstOrDefault()?.Id : null, UILabel = "Watermark" });
            AlphaPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoGuillochesFineLinePatterns).FirstOrDefault()?.Id : null, UILabel = "Guilloches / Fine Line Patterns" });
            AlphaPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoLaminate).FirstOrDefault()?.Id : null, UILabel = "Laminate" });
            AlphaPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoSubstrateSecurityThread).FirstOrDefault()?.Id : null, UILabel = "Substrate Security Thread" });
            AlphaPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoBiographicalData).FirstOrDefault()?.Id : null, UILabel = "Biographical Data" });

            if (DocumentClassId != null && (DocumentClassId == documentClassList.Where(d => d.EnumValue == DocumentClassKeys.EUId).FirstOrDefault()?.Id || DocumentClassId == documentClassList.Where(d => d.EnumValue == DocumentClassKeys.Passport).FirstOrDefault()?.Id))
            {
                AlphaPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoMRZZone).FirstOrDefault()?.Id : null, UILabel = "MRZ Zone " });
                AlphaPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoPhotoAndSignature).FirstOrDefault()?.Id : null, UILabel = "Photo N Signature" });
                AlphaPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoOpticalSecurityElements).FirstOrDefault()?.Id : null, UILabel = "Optical Security Elements" });
            }

            return AlphaPradoCheckList;
        }
        public virtual Dictionary<string, string> GetDocumentsDownloadAccess(string token) => new Dictionary<string, string>();

    }
}
