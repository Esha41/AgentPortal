
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Shared.DTO;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Intelli.AgentPortal.Shared.Services.Abstract;

namespace Intelli.AgentPortal.Shared.Services.Implementations
{
    public class ForthNetActions : ICompanyActions
    {

        public List<CustomerData> AgentCustomerData(Batch batch)
        {
            List<CustomerData> customerInfo = new List<CustomerData>();
            var documentClasseFieldList = DocumentClasseFieldClass.GetAllDocumentClasseFields();
            var batchSourcesList = DocumentClasseFieldClass.GetAllBatchSources();

            var FirstName = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocFirstNameGr)?.Id)?.FieldValue;
            var LastName = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocLastNameGr)?.Id)?.FieldValue;
            var IdNumber = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocIDNumber)?.Id)?.FieldValue;

            customerInfo.Add(new CustomerData { Label = "First Name", Value = FirstName });
            customerInfo.Add(new CustomerData { Label = "Last Name", Value = LastName });
            customerInfo.Add(new CustomerData { Label = "Id Number", Value = IdNumber });

            if (batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.ForthNet1)?.Id)
            {
                var Phone = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocPhone)?.Id)?.FieldValue;
                var TaxNumber = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocTaxNumber)?.Id)?.FieldValue;

                customerInfo.Add(new CustomerData { Label = "Phone", Value = Phone });
                customerInfo.Add(new CustomerData { Label = "Birthdate", Value = TaxNumber });
            }
            else if (batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.ForthNet2)?.Id)
            {
                var Phone = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocPhone)?.Id)?.FieldValue;
                var TaxNumber = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocTaxNumber)?.Id)?.FieldValue;

                customerInfo.Add(new CustomerData { Label = "Phone", Value = Phone });
                customerInfo.Add(new CustomerData { Label = "Tax Number", Value = TaxNumber });
            }
            else if (batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.ForthNet3)?.Id)
            {
                var Phone = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocPhone)?.Id)?.FieldValue;
                var TaxNumber = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocTaxNumber)?.Id)?.FieldValue;
                var Address = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocAddress)?.Id)?.FieldValue;
                var AddressNo = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocAddressNo)?.Id)?.FieldValue;

                customerInfo.Add(new CustomerData { Label = "Phone", Value = Phone });
                customerInfo.Add(new CustomerData { Label = "Tax Number", Value = TaxNumber });
                customerInfo.Add(new CustomerData { Label = "Address", Value = Address });
                customerInfo.Add(new CustomerData { Label = "Address No", Value = AddressNo });
            }
            else if (batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.ForthNet4)?.Id)
            {
                var Phone = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocPhone)?.Id)?.FieldValue;
                var TaxNumber = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocTaxNumber)?.Id)?.FieldValue;
                var Address = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocAddress)?.Id)?.FieldValue;
                var AddressNo = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocAddressNo)?.Id)?.FieldValue;

                customerInfo.Add(new CustomerData { Label = "Phone", Value = Phone });
                customerInfo.Add(new CustomerData { Label = "Tax Number", Value = TaxNumber });
                customerInfo.Add(new CustomerData { Label = "Address", Value = Address });
                customerInfo.Add(new CustomerData { Label = "Address No", Value = AddressNo });
            }
            else if (batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.ForthNet5)?.Id)
            {
                var Address = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocAddress)?.Id)?.FieldValue;
                var AddressNo = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocAddressNo)?.Id)?.FieldValue;

                customerInfo.Add(new CustomerData { Label = "Address", Value = Address });
                customerInfo.Add(new CustomerData { Label = "Address No", Value = AddressNo });
            }
            else if (batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.ForthNet6)?.Id)
            {
                var TaxNumber = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocTaxNumber)?.Id)?.FieldValue;
                var Address = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocAddress)?.Id)?.FieldValue;
                var AddressNo = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocAddressNo)?.Id)?.FieldValue;

                customerInfo.Add(new CustomerData { Label = "Tax Number", Value = TaxNumber });
                customerInfo.Add(new CustomerData { Label = "Address", Value = Address });
                customerInfo.Add(new CustomerData { Label = "Address No", Value = AddressNo });
            }
            else if (batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.ForthNet8)?.Id)
            {
                var Phone = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocPhone)?.Id)?.FieldValue;
                var TaxNumber = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocTaxNumber)?.Id)?.FieldValue;
                var Address = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocAddress)?.Id)?.FieldValue;
                var AddressNo = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocAddressNo)?.Id)?.FieldValue;

                customerInfo.Add(new CustomerData { Label = "Phone", Value = Phone });
                customerInfo.Add(new CustomerData { Label = "Tax Number", Value = TaxNumber });
                customerInfo.Add(new CustomerData { Label = "Address", Value = Address });
                customerInfo.Add(new CustomerData { Label = "Address No", Value = AddressNo });
            }
            else if (batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.ForthNet9)?.Id)
            {
                var Address = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocAddress)?.Id)?.FieldValue;
                var AddressNo = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocAddressNo)?.Id)?.FieldValue;
                var SupplyNumber = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocSupplyNumber)?.Id)?.FieldValue;
                var CounterNumber = batch.BatchMeta.FirstOrDefault(x => x.DocumentClassFieldId == documentClasseFieldList.FirstOrDefault(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocCounterNumber)?.Id)?.FieldValue;

                customerInfo.Add(new CustomerData { Label = "Address", Value = Address });
                customerInfo.Add(new CustomerData { Label = "Address No", Value = AddressNo });
                customerInfo.Add(new CustomerData { Label = "Supply Number", Value = SupplyNumber });
                customerInfo.Add(new CustomerData { Label = "Counter Number", Value = CounterNumber });
            }

            return customerInfo;
        }

        public List<PradoCheck> PradoChecks(int? DocumentClassId)
        {
            List<PradoCheck> AlphaPradoCheckList = new List<PradoCheck>();

            var documentClasseFieldList = DocumentClasseFieldClass.GetAllDocumentClasseFields();

            AlphaPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoWatermark).FirstOrDefault()?.Id : null, UILabel = "Watermark" });
            AlphaPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoGuillochesFineLinePatterns).FirstOrDefault()?.Id : null, UILabel = "Guilloches / Fine Line Patterns" });
            AlphaPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoLaminate).FirstOrDefault()?.Id : null, UILabel = "Laminate" });
            AlphaPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoSubstrateSecurityThread).FirstOrDefault()?.Id : null, UILabel = "Substrate Security Thread" });
            AlphaPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoBiographicalData).FirstOrDefault()?.Id : null, UILabel = "Biographical Data" });

            return AlphaPradoCheckList;
        }
        public virtual Dictionary<string, string> GetDocumentsDownloadAccess(string token) => new Dictionary<string, string>();

    }
}
