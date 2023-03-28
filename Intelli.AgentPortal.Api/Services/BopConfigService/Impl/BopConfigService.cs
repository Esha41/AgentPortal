using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Domain.Repository.Impl;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intelli.AgentPortal.Api.Services.BopConfigService.Impl
{
    /// <summary>
    /// The Bop Config Service.
    /// </summary>
    public class BopConfigService : IBopConfigService
    {
        private List<BopConfig> bopConfigList = new List<BopConfig>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BopConfigService"/> class.
        /// </summary>
        /// <param name="provider">The IServiceProvider.</param>
        public BopConfigService(IServiceProvider provider)
        {
            var scope = provider.CreateScope();
            var context = scope.ServiceProvider.GetService<AgentPortalContext>();
            var _repositoryBopConfig = new CustomRepository<BopConfig>(context);
            bopConfigList.AddRange(_repositoryBopConfig.Query().ToList());
        }

        /// <summary>
        /// Get BopConfig By EnumValue.
        /// </summary>
        /// <param name="enumValue">The enumValue.</param>
        /// <returns>string.</returns>\
        public string GetBopConfigByEnumValue(string enumValue)
        {
            return bopConfigList.Where(d => d.EnumValue == enumValue).FirstOrDefault().Value;
        }
    }
}
