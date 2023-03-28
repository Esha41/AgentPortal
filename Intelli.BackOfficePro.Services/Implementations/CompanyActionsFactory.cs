
using Intelli.Agentpotal.Shared.Services.Abstract;
using System;

namespace Intelli.Agentpotal.Shared.Services.Implementations
{
    public class CompanyActionsFactory
    {
        public ICompanyActions GetCompanyActions(string companyCode)
        {
            ICompanyActions companyActions = null;
            switch (companyCode)
            {
                case "EURBNK":
                    {
                        companyActions = new EurobankActions();
                        break;
                    }
                default:
                    throw new Exception($"Company { companyCode } has not been implemented yet");

            }
            return companyActions;
        }
    }
}
