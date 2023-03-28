using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class DocumentClassField
    {
        public DocumentClassField()
        {
            BatchHistoryItemFields = new HashSet<BatchHistoryItemField>();
            BatchHistoryMeta = new HashSet<BatchHistoryMetum>();
            BatchItemFields = new HashSet<BatchItemField>();
            InfoDocClassFields = new HashSet<InfoDocClassField>();
        }

        public int Id { get; set; }
        public int DocumentClassId { get; set; }
        public int DocumentClassFieldTypeId { get; set; }
        public string EnumValue { get; set; }
        public string Uilabel { get; set; }
        public bool? PublishEnabled { get; set; }
        public int? Uisort { get; set; }
        public int? DictionaryTypeId { get; set; }
        public bool? IsMandatory { get; set; }
        public string MappedName { get; set; }
        public string CorrectiveActionMappedName { get; set; }
        public virtual DocumentClasses DocumentClass { get; set; }
        public virtual ICollection<BatchHistoryItemField> BatchHistoryItemFields { get; set; }
        public virtual ICollection<BatchHistoryMetum> BatchHistoryMeta { get; set; }
        public virtual ICollection<BatchItemField> BatchItemFields { get; set; }
        public virtual ICollection<InfoDocClassField> InfoDocClassFields { get; set; }
    }
}
