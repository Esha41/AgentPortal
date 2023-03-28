namespace Intelli.AgentPortal.EventBus.RabbitMQ.Receiver
{
    /// <summary>
    /// The event handler interface.
    /// </summary>
    public interface IEventHandler
    {
        /// <summary>
        /// Handlers the event.
        /// </summary>
        /// <param name="eventMsg">The event msg.</param>
        void HandlerEvent(string eventMsg);
    }
}
