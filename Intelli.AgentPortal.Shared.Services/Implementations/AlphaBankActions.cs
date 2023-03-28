
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Shared.DTO;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Intelli.AgentPortal.Shared.Services.Abstract;

namespace Intelli.AgentPortal.Shared.Services.Implementations
{
    public class AlphaBankActions : ICompanyActions
    {
        public List<CustomerData> AgentCustomerData(Batch batch)
        {
            List<CustomerData> customerInfo = new List<CustomerData>();

            //calling static methods from static class
            var documentClasseFieldList = DocumentClasseFieldClass.GetAllDocumentClasseFields();
            var documentClassList = DocumentClasseFieldClass.GetAllDocumentClasses();

            var FirstName = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocFirstNameEn)?.Id)?.FieldValue;
            var LastName = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocLastNameEn)?.Id)?.FieldValue;
            var BirthDate = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocBirthDate)?.Id)?.FieldValue;

            customerInfo.Add(new CustomerData { Label = "First Name", Value = FirstName });
            customerInfo.Add(new CustomerData { Label = "Last Name", Value = LastName });

            if (batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.FirstOrDefault()?.DocumentClassId == documentClassList.FirstOrDefault(s => s.EnumValue == DocumentClassKeys.IdentiyId)?.Id)
            {
                var IdNumber = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocIDNumber)?.Id)?.FieldValue;
                var MothersName = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocMotherFirstNameGr)?.Id)?.FieldValue;
                var FahersName = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocFatherNameGr)?.Id)?.FieldValue;
                var BirthPlace = batch.BatchHistories.Where(x => x.IsLast = true).FirstOrDefault()?.BatchHistoryItems.Where(y => y.DocumentClassId == documentClassList.FirstOrDefault(s => s.EnumValue == DocumentClassKeys.IdentiyId)?.Id).FirstOrDefault()?.BatchHistoryItemFields.Where(z => z.DocumentClassFieldId == documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.IdentityIdBirthPlace).FirstOrDefault()?.Id).FirstOrDefault()?.RegisteredFieldValue;


                customerInfo.Add(new CustomerData { Label = "Id Number", Value = IdNumber });
                customerInfo.Add(new CustomerData { Label = "Birthdate", Value = BirthDate });
                customerInfo.Add(new CustomerData { Label = "Faher's Name", Value = FahersName });
                customerInfo.Add(new CustomerData { Label = "Mother's Name", Value = MothersName });
                customerInfo.Add(new CustomerData { Label = "Birth Place", Value = BirthPlace });
            }
            else if (batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.FirstOrDefault()?.DocumentClassId == documentClassList.FirstOrDefault(v => v.EnumValue == DocumentClassKeys.Passport)?.Id)
            {
                var PassportNumber = batch.BatchHistories.Where(x => x.IsLast = true).FirstOrDefault()?.BatchHistoryItems
                    .Where(y => y.DocumentClassId == documentClassList.FirstOrDefault(s => s.EnumValue == DocumentClassKeys.Passport)?.Id).FirstOrDefault()?.BatchHistoryItemFields
                    .Where(z => z.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.PassportPassportNo)?.Id).FirstOrDefault()?.RegisteredFieldValue;

                var BirthPlaceEn = batch.BatchHistories.Where(x => x.IsLast = true).FirstOrDefault()?.BatchHistoryItems
                    .Where(y => y.DocumentClassId == documentClassList.FirstOrDefault(v => v.EnumValue == DocumentClassKeys.Passport)?.Id).FirstOrDefault()?.BatchHistoryItemFields.Where(z => z.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.PassportBirthPlaceEn)?.Id).FirstOrDefault()?.RegisteredFieldValue;

                customerInfo.Add(new CustomerData { Label = "Passport Number", Value = PassportNumber });
                customerInfo.Add(new CustomerData { Label = "Birthdate", Value = BirthDate });
                customerInfo.Add(new CustomerData { Label = "Birth Place En", Value = BirthPlaceEn });
            }
            else if (batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.FirstOrDefault()?.DocumentClassId == documentClassList.FirstOrDefault(s => s.EnumValue == DocumentClassKeys.EUId)?.Id)
            {
                var euIdNumber = batch.BatchHistories.Where(x => x.IsLast = true).FirstOrDefault()?.BatchHistoryItems
                    .Where(y => y.DocumentClassId == documentClassList.FirstOrDefault(s => s.EnumValue == DocumentClassKeys.EUId)?.Id).FirstOrDefault()?.BatchHistoryItemFields
                    .Where(z => z.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.EUIdIDNumber)?.Id).FirstOrDefault()?.RegisteredFieldValue;

                customerInfo.Add(new CustomerData { Label = "Birthdate", Value = BirthDate });
                customerInfo.Add(new CustomerData { Label = "EUId Number", Value = euIdNumber });
            }
            return customerInfo;
        }

        public Dictionary<string, string> GetDocumentsDownloadAccess(string token)
        {
            return new Dictionary<string, string>();
        }

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
    }
}
