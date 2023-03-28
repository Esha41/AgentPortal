using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Shared.DTO;
using Intelli.AgentPortal.Shared.Mvc.DocumentClassFields;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Intelli.AgentPortal.Shared.Mvc.Services.CustomerDetailsManager;
using System.Linq;

namespace Intelli.AgentPortal.Shared
{
    /// <summary>
    /// The CustomerDetailsManager implementation.
    /// </summary>
    public class CustomerDetailsManager : ICustomerDetailsManager
    {
        public IDocumentClassFields _documentClassFieldService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerDetailsManager"/> class.
        /// </summary>
        /// <param name="documentClassFieldService">The IDocumentClassFields.</param>
        public CustomerDetailsManager(IDocumentClassFields documentClassFieldService)
        {
            _documentClassFieldService = documentClassFieldService;
        }

        /// <summary>
        /// Get Customer Model.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <returns>A CustomerModel.</returns>
        public CustomerModel GetCustomerModel(Batch batch)
        {
                string firstName = batch.BatchMeta.Where(x => x.DocumentClassFieldId == _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.RegisteredDocFirstNameGr)?.Id).FirstOrDefault()?.FieldValue;
                string lastName = batch.BatchMeta.Where(x => x.DocumentClassFieldId == _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.RegisteredDocLastNameGr)?.Id).FirstOrDefault()?.FieldValue;
                string mobile = batch.BatchMeta.Where(x => x.DocumentClassFieldId == _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.RegisteredDocMobile)?.Id).FirstOrDefault()?.FieldValue;
                string taxCode = batch.BatchMeta.Where(x => x.DocumentClassFieldId == _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.RegisteredDocTaxNumber)?.Id).FirstOrDefault()?.FieldValue;
                string idNumber = batch.BatchMeta.Where(x => x.DocumentClassFieldId == _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.RegisteredDocIDNumber)?.Id).FirstOrDefault()?.FieldValue;
                string fathersName = batch.BatchMeta.Where(x => x.DocumentClassFieldId == _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.RegisteredDocFatherNameGr)?.Id).FirstOrDefault()?.FieldValue;
                string mothersName = batch.BatchMeta.Where(x => x.DocumentClassFieldId == _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.RegisteredDocMotherFirstNameGr)?.Id).FirstOrDefault()?.FieldValue;
                string birthDate = batch.BatchMeta.Where(x => x.DocumentClassFieldId == _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.RegisteredDocBirthDate)?.Id).FirstOrDefault()?.FieldValue;

                return new CustomerModel() { FirstName = firstName, LastName = lastName, Mobile = mobile, TaxCode = taxCode, IDNumber = idNumber, FathersName = fathersName, MothersName = mothersName, BirthDate = birthDate };
        }
    }
}
