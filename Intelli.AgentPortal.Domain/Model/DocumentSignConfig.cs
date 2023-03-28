using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class DocumentSignConfig
    {
        public DocumentSignConfig()
        {
            BatchContractSignConfigs = new HashSet<BatchContractSignConfig>();
            BatchSourceAppPages = new HashSet<BatchSourceAppPage>();
        }

        public int Id { get; set; }
        public string DocumentTemplateName { get; set; }
        public string SignatureConfiguration { get; set; }

        public virtual ICollection<BatchContractSignConfig> BatchContractSignConfigs { get; set; }
        public virtual ICollection<BatchSourceAppPage> BatchSourceAppPages { get; set; }
    }
}
