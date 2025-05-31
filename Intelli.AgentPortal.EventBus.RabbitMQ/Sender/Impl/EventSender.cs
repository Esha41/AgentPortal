using Intelli.AgentPortal.EventBus.RabbitMQ.Config;
using Intelli.AgentPortal.EventBus.RabbitMQ.Event;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;

namespace Intelli.AgentPortal.EventBus.RabbitMQ.Sender.Impl
{
    /// <summary>
    /// The event sender implementation.
    /// </summary>
    public class EventSender : IEventSender
    {
        private readonly string _hostname;

        private readonly string _username;
        private readonly string _password;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSender"/> class.
        /// </summary>
        /// <param name="rabbitMqOptions">The rabbit mq configuration options.</param>
        public EventSender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
        }

        /// <summary>
        /// Sends the event.
        /// </summary>
        /// <param name="mqEvent">The mq event.</param>
        /// <returns>An EventSenderStatus enumeration value.</returns>
        public EventSenderStatus SendEvent<TModel>(MQEvent<TModel> mqEvent)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };

            try
            {
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: mqEvent.QueueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var json = JsonConvert.SerializeObject(mqEvent);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: string.Empty,
                    routingKey: mqEvent.QueueName,
                    basicProperties: null,
                    body: body);
            }
            catch (BrokerUnreachableException)
            {
                return EventSenderStatus.Error;
            }

            return EventSenderStatus.Success;
        }
    }
}
