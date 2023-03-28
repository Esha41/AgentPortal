
using Intelli.AgentPortal.Domain.Model;
using System.Collections.Generic;

namespace Intelli.AgentPortal.Shared.Mvc.DocumentClassFields
{
    /// <summary>
    /// Interface for Document Class Fields
    /// </summary>
    public interface IDocumentClassFields
    {
        /// <summary>
        /// Get Document Class Field Values.
        /// </summary>
        /// <param name="fieldId">The fieldId.</param>
        /// <param name="isLanguageActive">The isLanguageActive.</param>
        /// <returns>A List of int.</returns>
        List<int> GetDocumentClassFieldValues(List<int> fieldId,bool isLanguageActive);

        /// <summary>
        /// Get Rejection Reasons From Code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>A string.</returns>
        string GetRejectionReasonFromCode(string code);

        /// <summary>
        /// Get Document Class Fields By EnumValue.
        /// </summary>
        /// <param enumValue="enumValue">The code.</param>
        /// <returns>A DocumentClassField.</returns>
        DocumentClassField GetDocumentClasseFieldsByEnumValue(string enumValue);

        /// <summary>
        /// Get Document Class Fields list.
        /// </summary>
        /// <param name="documentClassId">The documentClassId.</param>
        /// <returns>A DocumentClassField List.</returns>
        List<DocumentClassField> GetDocumentClassFields(int documentClassId);

        /// <summary>
        /// Get Document Clas sBy EnumValue.
        /// </summary>
        /// <param name="enumValue">The enumValue.</param>
        /// <returns>A DocumentClasse.</returns>
        DocumentClasses GetDocumentClassByEnumValue(string enumValue);
    }
}
