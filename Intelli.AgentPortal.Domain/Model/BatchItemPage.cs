using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BatchItemPage
    {
        public int Id { get; set; }
        public int BatchItemId { get; set; }
        public string FileName { get; set; }
        public int Number { get; set; }
        public string OriginalName { get; set; }

        public virtual BatchItem BatchItem { get; set; }
    }
}
