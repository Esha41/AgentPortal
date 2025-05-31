using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BatchSource
    {
        public BatchSource()
        {
            BatchSourceAppPages = new HashSet<BatchSourceAppPage>();
            BatchSourceUploadDocs = new HashSet<BatchSourceUploadDoc>();
            Batches = new HashSet<Batch>();
            CountriesPerCompanies = new HashSet<CountriesPerCompany>();
            InfoDocClassFields = new HashSet<InfoDocClassField>();
        }

        public int Id { get; set; }
        public string BatchSource1 { get; set; }
        public string EnumValue { get; set; }
        public string BatchSourceCode { get; set; }
        public string Comments { get; set; }
        public int? CompanyId { get; set; }
        public string ValidationApiUrl { get; set; }
        public string ValidationApiConfiguration { get; set; }
        public string RedirectLink { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<BatchSourceAppPage> BatchSourceAppPages { get; set; }
        public virtual ICollection<BatchSourceUploadDoc> BatchSourceUploadDocs { get; set; }
        public virtual ICollection<Batch> Batches { get; set; }
        public virtual ICollection<CountriesPerCompany> CountriesPerCompanies { get; set; }
        public virtual ICollection<InfoDocClassField> InfoDocClassFields { get; set; }
    }
}
