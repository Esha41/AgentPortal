using System.Collections.Generic;

namespace Intelli.AgentPortal.EventBus.RabbitMQ.Receiver
{
    /// <summary>
    /// The queue handler mapping factory.
    /// </summary>
    public interface IQueueHandlerMappingFactory
    {
        /// <summary>
        /// Gets the queue to handler mapping.
        /// </summary>
        /// <returns>A Dictionary of event handlers.</returns>
        Dictionary<string, IEventHandler> GetQueueToHandlerMapping();
    }
}
