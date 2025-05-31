using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Intelli.AgentPortal.EventBus.RabbitMQ.Receiver
{
    /// <summary>
    /// The event consumer.
    /// Subscriber of the event.
    /// </summary>
    public class EventConsumer : EventingBasicConsumer
    {
        private readonly IEventHandler _eventHandler;
        private readonly string _queueName;
        /// <summary>
        /// Initializes a new instance of the <see cref="EventConsumer"/> class.
        /// </summary>
        /// <param name="queueName">The queue name.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="eventHandler">The event handler.</param>
        public EventConsumer(string queueName, IModel channel, IEventHandler eventHandler) : base(channel)
        {
            _queueName = queueName;
            _eventHandler = eventHandler;

            Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                HandleMessage(content);
                channel.BasicAck(ea.DeliveryTag, false);
            };
            Shutdown += OnConsumerShutdown;
            Registered += OnConsumerRegistered;
            Unregistered += OnConsumerUnregistered;
            ConsumerCancelled += OnConsumerConsumerCancelled;

            channel.BasicConsume(_queueName, false, this);
        }

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="eventMsg">The event msg to be handled.</param>
        private void HandleMessage(string eventMsg)
        {
            _eventHandler.HandlerEvent(eventMsg);
        }

        /// <summary>
        /// Consumer cancelled event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The consumer event arguments.</param>
        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)
        {
        }

        /// <summary>
        /// Consumer unregistered event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The consumer event arguments.</param>
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
        }

        /// <summary>
        /// Consumer registered event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The consumer event arguments.</param>
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
        }

        /// <summary>
        /// Consumer shutdown event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The consumer event arguments.</param>
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
        }


    }
}
