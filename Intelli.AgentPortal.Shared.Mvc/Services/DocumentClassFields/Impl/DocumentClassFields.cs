using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Domain.Repository;
using Intelli.AgentPortal.Domain.Repository.Impl;
using Intelli.AgentPortal.Shared.Mvc.DocumentClassFields;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intelli.AgentPortal.Shared.Mvc.Services.DocumentClassFields.Impl
{
    /// <summary>
    /// The DocumentClassFields Service.
    /// </summary>
    public class DocumentClassFields : IDocumentClassFields
    {
        private List<DocumentRejectionReason> documentRejectionReasonList = new List<DocumentRejectionReason>();
        private List<DocumentClassField> documentClassFieldList = new List<DocumentClassField>();
        private List<DocumentClasses> documentClassesList = new List<DocumentClasses>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentClassFieldsService"/> class.
        /// </summary>
        /// <param name="provider">The IServiceProvider.</param>
        public DocumentClassFields(IServiceProvider provider)
        {
            var scope = provider.CreateScope();
            var context = scope.ServiceProvider.GetService<AgentPortalContext>();
            var documentClassFieldRepository = new CustomRepository<DocumentClassField>(context);
            var documentClassRepository = new CustomRepository<DocumentClasses>(context);
            var documentRejectionReasonRepository = new CustomRepository<DocumentRejectionReason>(context);

            documentRejectionReasonList.AddRange(documentRejectionReasonRepository.Query().ToList());
            documentClassFieldList.AddRange(documentClassFieldRepository.Query().ToList());
            documentClassesList.AddRange(documentClassRepository.Query().ToList());
        }

        /// <summary>
        /// Get Document Class Fields By EnumValue.
        /// </summary>
        /// <param name="enumValue">The enumValue.</param>
        /// <returns>A DocumentClassField.</returns>
        public DocumentClassField GetDocumentClasseFieldsByEnumValue(string enumValue)
        {
            return documentClassFieldList.Where(s => s.EnumValue == enumValue).FirstOrDefault();
        }

        /// <summary>
        /// Get Document Clas sBy EnumValue.
        /// </summary>
        /// <param name="enumValue">The enumValue.</param>
        /// <returns>A DocumentClasse.</returns>
        public DocumentClasses GetDocumentClassByEnumValue(string enumValue)
        {
            return documentClassesList.Where(s => s.EnumValue == enumValue).FirstOrDefault();
        }

        /// <summary>
        /// Get Document Class Fields list.
        /// </summary>
        /// <param name="documentClassId">The documentClassId.</param>
        /// <returns>A DocumentClassField List.</returns>
        public List<DocumentClassField> GetDocumentClassFields(int documentClassId)
        {
            return documentClassFieldList.Where(s => s.DocumentClassId == documentClassId).ToList();
        }

        /// <summary>
        /// Get Document Class Field Values.
        /// </summary>
        /// <param name="fieldId">The fieldId.</param>
        /// <param name="isLanguageActive">The isLanguageActive.</param>
        /// <returns>A List of int.</returns>
        public List<int> GetDocumentClassFieldValues(List<int> fieldId, bool isLanguageActive)
        {
            //getting field Id only of specifc constants
            var result = documentClassFieldList.Where(s => fieldId.Contains(s.Id));
            List<DocumentClassField> fieldList = new List<DocumentClassField>();

            if (isLanguageActive == false)
                fieldList = result.Where(d => d.EnumValue == DocumentClassFieldKeys.RegisteredDocReseller || d.EnumValue == DocumentClassFieldKeys.RegisteredDocCountryOfOrigin).ToList();
            else
                fieldList = result.Where(d => d.EnumValue == null || d.EnumValue == DocumentClassFieldKeys.RegisteredDocCountryOfOrigin).ToList();

            return fieldList.Select(d => d.Id).ToList();
        }

        /// <summary>
        /// Get Rejection Reasons From Code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>A string.</returns>
        public string GetRejectionReasonFromCode(string code)
        {
            return documentRejectionReasonList.Where(s => s.Code == code).FirstOrDefault()?.Descr;
        }
    }
}
