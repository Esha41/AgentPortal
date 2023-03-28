
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Shared.DTO;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Intelli.AgentPortal.Shared.Services.Abstract;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Intelli.AgentPortal.Shared.Services.Implementations
{
    public class PiraeusActions : ICompanyActions
    {
        public List<CustomerData> AgentCustomerData(Batch batch)
        {
            List<CustomerData> customerInfo = new List<CustomerData>();

            var documentClasseList = DocumentClasseFieldClass.GetAllDocumentClasses();
            var documentClasseFieldList = DocumentClasseFieldClass.GetAllDocumentClasseFields();
            var batchSourcesList = DocumentClasseFieldClass.GetAllBatchSources();

            var identityFirstNameFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("FirstName")).Select(x => x.Id).ToList();
            var identityLastNameFieldIDs = documentClasseFieldList.Where(d => d.MappedName.StartsWith("LastName")).Select(x => x.Id).ToList();

            if (batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.Piraeus)?.Id)
            {
                var firstName = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityFirstNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var lastName = batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault()?.BatchHistoryItems.Where(y => y.BatchSourceUploadDocId == (int)DocumentGroupNames.Identity && Convert.ToBoolean(y.IsValid))?.FirstOrDefault()?.BatchHistoryItemFields.FirstOrDefault(f => identityLastNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;

                customerInfo.Add(new CustomerData { Label = "First Name", Value = firstName });
                customerInfo.Add(new CustomerData { Label = "Last Name", Value = lastName });
            }
            else if (batch.BatchSourceId == batchSourcesList.FirstOrDefault(d => d.EnumValue == BatchSourcesKeys.PiraeusIdeKYC)?.Id)
            {
                var batchItemsFields = batch.BatchItems.SelectMany(bi => bi.BatchItemFields);

                var firstName = batch.BatchItems.Where(y => y.DocumentClassId == documentClasseList.FirstOrDefault(d => d.EnumValue == DocumentClassKeys.eKYCIdentity)?.Id)?.FirstOrDefault()?.BatchItemFields.FirstOrDefault(f => identityFirstNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;
                var lastName = batch.BatchItems.Where(y => y.DocumentClassId == documentClasseList.FirstOrDefault(d => d.EnumValue == DocumentClassKeys.eKYCIdentity)?.Id)?.FirstOrDefault()?.BatchItemFields.FirstOrDefault(f => identityLastNameFieldIDs.Contains(f.DocumentClassFieldId))?.RegisteredFieldValue;

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
            List<PradoCheck> AlphaPradoCheckList = new List<PradoCheck>();
            return AlphaPradoCheckList;
        }
        //public virtual Dictionary<string, string> GetDocumentsDownloadAccess(string token) => new Dictionary<string, string>();

        public Dictionary<string, string> GetDocumentsDownloadAccess(string token)
        {
            var factory = LoggerFactory.Create(b => b.AddConsole());
            var logger = factory.CreateLogger<PiraeusActions>();

            var company = CompanyStaticClass.GetCompanyByName(CompanyKeys.PiraeusBank);

            string accessEndpoint = company.DocumentsDownloadAccessEndpoint;

            HttpResponseMessage response = null;
            string responseContent = string.Empty;

            try
            {
                using (var client = new HttpClient() { Timeout = TimeSpan.FromSeconds(3) })
                {
                    client.DefaultRequestHeaders.Add("IntelliSessionToken", token);
                    //Execute request for download access
                    response = client.PostAsync(accessEndpoint, new StringContent("")).Result;
                    responseContent = response.Content.ReadAsStringAsync().Result;
                    //Log response

                    logger.LogInformation("{0}. ResuestId: {1}", "AccessTokenRequest response= " + responseContent, token);
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "{0}. ResuestId: {1}", "Error while communicating with Piraeus api", token);

                return new Dictionary<string, string>();
            }

            if (response.IsSuccessStatusCode)
            {
                var responseDto = JsonConvert.DeserializeObject<PiraeusResponseDto>(responseContent);
                if (responseDto?.Error == null && responseDto?.Body?.Result == 0)//0 represents success for Piraeus..
                {
                    try
                    {
                        string tokenConventionBasedFileUrl = company.DocumentsDownloadAccessFetchUrl.Replace("token", token);

                        //Fetch actual pdf
                        using (var client = new HttpClient() { Timeout = TimeSpan.FromSeconds(3) })
                            response = client.GetAsync(tokenConventionBasedFileUrl).Result;

                        //If success
                        if (response.IsSuccessStatusCode)
                        {
                            var contentAsBase64 = Convert.ToBase64String(response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult());
                            //log received content

                            logger.LogInformation("{0}. ResuestId: {1}", "Pdf response content= " + contentAsBase64, token);

                            //AND content is of type "pdf" (JVBER = pdf as base64 standard prefix)
                            if (contentAsBase64.StartsWith("JVBER"))
                                return new Dictionary<string, string>() { { "Document", $"data:application/pdf;base64,{contentAsBase64}" } };
                        }
                    }
                    catch (Exception e)
                    {
                        logger.LogError(e, "{0}. ResuestId: {1}", "Error while downloadind file from Piraeus api", token);
                    }
                }
            }
            return new Dictionary<string, string>();
        }
        public class PiraeusResponseDto
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
