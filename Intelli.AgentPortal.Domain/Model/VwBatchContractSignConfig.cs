using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwBatchContractSignConfig
    {
        public int DocumentSignConfigId { get; set; }
        public int BatchId { get; set; }
        public string Pdfname { get; set; }
        public string DocumentTemplateName { get; set; }
        public string SignatureConfiguration { get; set; }
    }
}
