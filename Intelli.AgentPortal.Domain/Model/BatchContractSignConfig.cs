using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BatchContractSignConfig
    {
        public int Id { get; set; }
        public int DocumentSignConfigId { get; set; }
        public int BatchId { get; set; }
        public string Pdfname { get; set; }

        public virtual Batch Batch { get; set; }
        public virtual DocumentSignConfig DocumentSignConfig { get; set; }
    }
}
