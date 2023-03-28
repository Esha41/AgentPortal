
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Shared.DTO;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Intelli.AgentPortal.Shared.Services.Abstract;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Intelli.AgentPortal.Shared.Services.Implementations
{
    public class FlexfinActions : ICompanyActions
    {
        public List<CustomerData> AgentCustomerData(Batch batch)
        {
            List<CustomerData> customerInfo = new List<CustomerData>();
            var documentClasseList = DocumentClasseFieldClass.GetAllDocumentClasses();
            var documentClasseFieldList = DocumentClasseFieldClass.GetAllDocumentClasseFields();
            var batchSourcesList = DocumentClasseFieldClass.GetAllBatchSources();

            var identityFirstNameFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("FirstName")).Select(x => x.Id).ToList();
            var identityLastNameFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("LastName")).Select(x => x.Id).ToList();
            var identityIdNumberFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("IDNumber")).Select(x => x.Id).ToList();
            var identityBirthDateFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("BirthDate")).Select(x => x.Id).ToList();
            var identityFatherNameFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("FatherFirstName")).Select(x => x.Id).ToList();
            var identityMotherNameFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("MotherFirstName")).Select(x => x.Id).ToList();

            if (batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.FlexFin)?.Id ||
           batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.FlexFin_Sign)?.Id)
            {
                var firstName = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityFirstNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var lastName = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityLastNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var IdNumber = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityIdNumberFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var birthDate = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityBirthDateFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var fatherFirstName = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityFatherNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var motherFirstName = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityMotherNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;

                customerInfo.Add(new CustomerData { Label = "First Name", Value = firstName });
                customerInfo.Add(new CustomerData { Label = "Last Name", Value = lastName });
                customerInfo.Add(new CustomerData { Label = "Id Number", Value = IdNumber });
                customerInfo.Add(new CustomerData { Label = "Birth Date", Value = birthDate });
                customerInfo.Add(new CustomerData { Label = "Father's Name", Value = fatherFirstName });
                customerInfo.Add(new CustomerData { Label = "Mother's Name", Value = motherFirstName });
            }
            else if (batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.FlexfineKYC)?.Id)
            {
                var firstName = batch.BatchItems.Where(y => y.DocumentClassId == documentClasseList.FirstOrDefault(d => d.EnumValue == DocumentClassKeys.eKYCIdentity)?.Id)?.FirstOrDefault()?.BatchItemFields.FirstOrDefault(f => identityFirstNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var lastName = batch.BatchItems.Where(y => y.DocumentClassId == documentClasseList.FirstOrDefault(d => d.EnumValue == DocumentClassKeys.eKYCIdentity)?.Id)?.FirstOrDefault()?.BatchItemFields.FirstOrDefault(f => identityLastNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var IdNumber = batch.BatchItems.Where(y => y.DocumentClassId == documentClasseList.FirstOrDefault(d => d.EnumValue == DocumentClassKeys.eKYCIdentity)?.Id)?.FirstOrDefault()?.BatchItemFields.FirstOrDefault(f => identityIdNumberFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var birthDate = batch.BatchItems.Where(y => y.DocumentClassId == documentClasseList.FirstOrDefault(d => d.EnumValue == DocumentClassKeys.eKYCIdentity)?.Id)?.FirstOrDefault()?.BatchItemFields.FirstOrDefault(f => identityBirthDateFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var fatherFirstName = batch.BatchItems.Where(y => y.DocumentClassId == documentClasseList.FirstOrDefault(d => d.EnumValue == DocumentClassKeys.eKYCIdentity)?.Id)?.FirstOrDefault()?.BatchItemFields.FirstOrDefault(f => identityFatherNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var motherFirstName = batch.BatchItems.Where(y => y.DocumentClassId == documentClasseList.FirstOrDefault(d => d.EnumValue == DocumentClassKeys.eKYCIdentity)?.Id)?.FirstOrDefault()?.BatchItemFields.FirstOrDefault(f => identityMotherNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;

                customerInfo.Add(new CustomerData { Label = "First Name", Value = firstName });
                customerInfo.Add(new CustomerData { Label = "Last Name", Value = lastName });
                customerInfo.Add(new CustomerData { Label = "Id Number", Value = IdNumber });
                customerInfo.Add(new CustomerData { Label = "Birth Date", Value = birthDate });
                customerInfo.Add(new CustomerData { Label = "Father's Name", Value = fatherFirstName });
                customerInfo.Add(new CustomerData { Label = "Mother's Name", Value = motherFirstName });
            }
            else
            {
                customerInfo.Add(new CustomerData { Label = "Warning", Value = "Flow Has Not Been Implemented Yet" });
            }
            return customerInfo;
        }

        public List<PradoCheck> PradoChecks(int? DocumentClassId)
        {
            List<PradoCheck> FlexFinPradoCheckList = new List<PradoCheck>();
            //calling static methods from static class
            var documentClasseFieldList = DocumentClasseFieldClass.GetAllDocumentClasseFields();
            var documentClassList = DocumentClasseFieldClass.GetAllDocumentClasses();

            FlexFinPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoWatermark).FirstOrDefault()?.Id : null, UILabel = "Watermark" });
            FlexFinPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoGuillochesFineLinePatterns).FirstOrDefault()?.Id : null, UILabel = "Guilloches / Fine Line Patterns" });
            FlexFinPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoLaminate).FirstOrDefault()?.Id : null, UILabel = "Laminate" });
            FlexFinPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoSubstrateSecurityThread).FirstOrDefault()?.Id : null, UILabel = "Substrate Security Thread" });
            FlexFinPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoBiographicalData).FirstOrDefault()?.Id : null, UILabel = "Biographical Data" });

            if (DocumentClassId != null && (DocumentClassId == documentClassList.Where(d => d.EnumValue == DocumentClassKeys.EUId).FirstOrDefault()?.Id || DocumentClassId == documentClassList.Where(d => d.EnumValue == DocumentClassKeys.Passport).FirstOrDefault()?.Id))
            {
                FlexFinPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoMRZZone).FirstOrDefault()?.Id : null, UILabel = "MRZ Zone " });
                FlexFinPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoPhotoAndSignature).FirstOrDefault()?.Id : null, UILabel = "Photo N Signature" });
                FlexFinPradoCheckList.Add(new PradoCheck { PradoFieldId = documentClasseFieldList != null ? documentClasseFieldList.Where(d => d.EnumValue == DocumentClassFieldKeys.PradoOpticalSecurityElements).FirstOrDefault()?.Id : null, UILabel = "Optical Security Elements" });
            }
            return FlexFinPradoCheckList;
        }

        public Dictionary<string, string> GetDocumentsDownloadAccess(string token)
        {
            var factory = LoggerFactory.Create(b => b.AddConsole());
            var logger = factory.CreateLogger<FlexfinActions>();

            var company = CompanyStaticClass.GetCompanyByName(CompanyKeys.Flexfin);

            string accessEndpoint = company.DocumentsDownloadAccessEndpoint;

            HttpResponseMessage response = null;

            try
            {
                using (var client = new HttpClient() { Timeout = TimeSpan.FromSeconds(3) })
                {
                    client.DefaultRequestHeaders.Add("IntelliSessionToken", token);
                    response = client.PostAsync(accessEndpoint, new StringContent("")).Result;
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "{0}. ResuestId: {1}", "Error while communicating with Flexfin api", token);
                return new Dictionary<string, string>();
            }

            string responseContent = response.Content.ReadAsStringAsync().Result;
            logger.LogInformation("{0}. ResuestId: {1}", "GetPdfDownloadAccess content read= " + responseContent, token);

            if (response.IsSuccessStatusCode)
            {
                var responseDto = JsonConvert.DeserializeObject<FlexfinResponseDto>(responseContent);


                if (responseDto?.Error == null && responseDto?.Body?.Result == 0)//0 represents succes for Piraeus..
                {
                    try
                    {
                        string tokenConventionBasedFileUrl = company.DocumentsDownloadAccessFetchUrl.Replace("token", token);

                        using (var client = new HttpClient() { Timeout = TimeSpan.FromSeconds(3) })
                            response = client.GetAsync(tokenConventionBasedFileUrl).Result;

                        var contentAsBase64 = Convert.ToBase64String(response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult());

                        return new Dictionary<string, string>() { { "Document", $"data:application/pdf;base64,{contentAsBase64}" } };

                    }
                    catch (Exception e)
                    {
                        logger.LogError(e, "{0}. ResuestId: {1}", "Error while downloadind file from Flexfin api", token);
                    }
                }
            }
            return new Dictionary<string, string>();
        }
        public class FlexfinResponseDto
        {
            public Error? Error { get; set; }
            public bool Quarantine { get; set; }
            public Body? Body { get; set; }
        }
        public class Error
        {
            public bool IsInErrorState { get; set; }
            public string? ErrorCode { get; set; }
            public string? ErrorDescription { get; set; }
            public int ErrorType { get; set; }
        }
        public class Body
        {
            public string? ErrorMessage { get; set; }
            public int? Result { get; set; }
            public string? ValidFor { get; set; }
        }
    }
}
