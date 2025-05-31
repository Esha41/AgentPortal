using System;
using System.Collections.Generic;

namespace Intelli.AgentPortal.Api.DTO
{
    public class PendingBatchesDTO
    {
        public List<PendingBatchesVideoPriorityDTO> VideoPriority { get; set; } = new();
        /*  public List<PendingBatchesUserDTO> Users { get; set; } = new();
          public List<PendingBatchescCompanyDTO> Companies { get; set; } = new();*/
     
    }
}
