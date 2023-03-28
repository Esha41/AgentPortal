using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.Domain.Model;

namespace Intelli.AgentPortal.Shared.Services.Implementations
{
    /// <summary>
    /// The Document Class Field static Class.
    /// </summary>
    public static class DocumentClasseFieldClass
    {
        private static List<DocumentClassField> documentClassFieldList = new List<DocumentClassField>();
        private static List<DocumentClasses> documentClassesList = new List<DocumentClasses>();
        private static List<BatchSource> batchSourcesList = new List<BatchSource>();
        static DocumentClasseFieldClass()
        {
            AgentPortalContext _context = new AgentPortalContext();
            documentClassFieldList.AddRange(_context.DocumentClassFields.ToList());
            documentClassesList.AddRange(_context.DocumentClasses.ToList());
            batchSourcesList.AddRange(_context.BatchSources.ToList());
        }

        /// <summary>
        /// Get All Document Fields.
        /// </summary>
        /// <returns>DocumentFields List.</returns>
        public static List<DocumentClassField> GetAllDocumentClasseFields()
        {
            //var result = _context.DocumentClassFields.ToList();
            //return result;
            return documentClassFieldList;
        }

        /// <summary>
        /// Get All Document Classes.
        /// </summary>
        /// <returns>DocumentClasses List.</returns>
        public static List<DocumentClasses> GetAllDocumentClasses()
        {
            //var result = _context.DocumentClasses.ToList();
            //return result;
            return documentClassesList;
        }

        /// <summary>
        /// Get All GetAllBatch Sources.
        /// </summary>
        /// <returns>GetAllBatchSources List.</returns>
        public static List<BatchSource> GetAllBatchSources()
        {
            return batchSourcesList;
        }
    }
}
