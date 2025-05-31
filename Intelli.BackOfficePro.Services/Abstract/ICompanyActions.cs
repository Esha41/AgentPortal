using System.Collections.Generic;

namespace Intelli.Agentpotal.Shared.Services.Abstract
{
    public interface ICompanyActions
    {
        List<CustomerData> AgentCustomerData(Batch batch);
    }
}
