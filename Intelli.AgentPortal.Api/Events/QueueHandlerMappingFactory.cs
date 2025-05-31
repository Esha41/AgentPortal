using Intelli.AgentPortal.Api.Events.Handlers;
using Intelli.AgentPortal.Domain.Core.Repository;
using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.EventBus.RabbitMQ.Receiver;
using Intelli.AgentPortal.Shared.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Intelli.AgentPortal.Api.Events
{
    /// <summary>
    /// The queue handler mapping factory.
    /// </summary>
    public class QueueHandlerMappingFactory : IQueueHandlerMappingFactory
    {
        private readonly IRepository<Audit> _auditRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueHandlerMappingFactory"/> class.
        /// </summary>
        /// <param name="provider">The service provider.</param>
        public QueueHandlerMappingFactory(IServiceProvider provider)
        {
            var scope = provider.CreateScope();
            var context = scope.ServiceProvider.GetService<AgentPortalAuditContext>();
            _auditRepository = new GenericRepository<Audit>(context);
        }

        /// <summary>
        /// Gets the queue to handler mapping.
        /// </summary>
        /// <returns>A Dictionary of event handlers.</returns>
        public Dictionary<string, IEventHandler> GetQueueToHandlerMapping()
        {
            Dictionary<string, IEventHandler> result = new()
            {
                [BaseController.AUDIT_EVENT_NAME] = new AuditEventHandler(_auditRepository)
            };
            return result;
        }
    }
}
