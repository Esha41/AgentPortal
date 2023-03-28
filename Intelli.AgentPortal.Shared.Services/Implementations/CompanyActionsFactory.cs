using Intelli.AgentPortal.Shared.Services.Abstract;

namespace Intelli.AgentPortal.Shared.Services.Implementations
{
    public class CompanyActionsFactory
    {
        public ICompanyActions GetCompanyActions(string companyCode)
        {
            ICompanyActions companyActions = null;
            switch (companyCode)
            {
                case "INTELLI":
                    {
                        companyActions = new IntelliAgent();
                        break;
                    }
                case "GovGr":
                    {
                        companyActions = new GovGrActions();
                        break;
                    }
                case "ALPHA":
                    {
                        companyActions = new AlphaBankActions();
                        break;
                    }
                case "ForthNet":
                    {
                        companyActions = new ForthNetActions();
                        break;
                    }
                case "WIND":
                    {
                        companyActions = new WindActions();
                        break;
                    }
                case "VIVAWALLET":
                    {
                        companyActions = new VivaWalletActions();
                        break;
                    }
                case "INTELLI2":
                    {
                        companyActions = new IntelliActions();
                        break;
                    }
                case "STOIXIOB":
                    {
                        companyActions = new StoiximanV2Actions();
                        break;
                    }
                case "EURBNK":
                    {
                        companyActions = new EUROBANKActions();
                        break;
                    }
                case "DEH":
                    {
                        companyActions = new DehActions();
                        break;
                    }
                case "VOLTON":
                    {
                        companyActions = new VoltonActions();
                        break;
                    }
                case "PIRAEUS":
                    {
                        companyActions = new PiraeusActions();
                        break;
                    }
                case "EBURY":
                    {
                        companyActions = new eBuryActions();
                        break;
                    }
                case "STOIXIBG":
                    {
                        companyActions = new StoiximanBGActions();
                        break;
                    }
                case "STOIXICY":
                    {
                        companyActions = new StoiximanCYActions();
                        break;
                    }
                case "FLEXFIN":
                    {
                        companyActions = new FlexfinActions();
                        break;
                    }
                case "eIDAS":
                    {
                        companyActions = new eIDASActions();
                        break;
                    }
                case "ATTICABNK":
                    {
                        companyActions = new AtticaBankActions();
                        break;
                    }
                case "EVERYPAY":
                    {
                        companyActions = new EveryPayActions();
                        break;
                    }
                case "THROOPAY":
                    {
                        companyActions = new ThrooPayActions();
                        break;
                    }
                case "AMFI":
                    {
                        companyActions = new AmfiTestActions();
                        break;
                    }
                case "WORLDBRIDG":
                    {
                        companyActions = new WorldBridgeActions();
                        break;
                    }
                case "IDBOXDEMO":
                    {
                        companyActions = new IdBoxDemoActions();
                        break;
                    }
                default:
                    throw new Exception($"Company { companyCode } has not been implemented yet");

            }
            return companyActions;
        }
    }
}
