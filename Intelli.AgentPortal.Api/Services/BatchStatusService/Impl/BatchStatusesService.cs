using Intelli.AgentPortal.Domain.Model;
using System.Collections.Generic;
using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.Domain.Repository.Impl;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Intelli.AgentPortal.Api.Services.BatchStatusService.Impl
{
    /// <summary>
    /// The Batch Status Service.
    /// </summary>
    public class BatchStatusesService : IBatchStatusService
    {
        private List<BatchStatus> batchStatusesList = new List<BatchStatus>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchStatusesService"/> class.
        /// </summary>
        /// <param name="provider">The IServiceProvider.</param>
        public BatchStatusesService(IServiceProvider provider)
        {
            var scope = provider.CreateScope();
            var context = scope.ServiceProvider.GetService<AgentPortalContext>();
            var _repositoryBatchStatus = new CustomRepository<BatchStatus>(context);
            batchStatusesList.AddRange(_repositoryBatchStatus.Query().ToList());
        }

        /// <summary>
        /// Get Batch Status Id By EnumValue.
        /// </summary>
        /// <param name="enumValue">The enumValue.</param>
        /// <returns>int.</returns>\
        public int GetBatchStatusIdByEnumValue(string enumValue)
        {
            return batchStatusesList.Where(d => d.EnumValue == enumValue).FirstOrDefault().Id;
        }
    }
}
