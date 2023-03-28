using Intelli.AgentPortal.EventBus.RabbitMQ.Config;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.EventBus.RabbitMQ.Receiver.Impl
{
    /// <summary>
    /// The event listener that runs as background service.
    /// </summary>
    public class EventListener : BackgroundService
    {

        private IModel _channel;
        private IConnection _connection;

        private Dictionary<string, EventConsumer> _consumers;
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventListener"/> class.
        /// </summary>
        /// <param name="rabbitMqOptions">The rabbit mq options.</param>
        /// <param name="eventHandlerFactory">The event handler factory.</param>
        public EventListener(IOptions<RabbitMqConfiguration> rabbitMqOptions,
            IQueueHandlerMappingFactory eventHandlerFactory)
        {

            _hostname = rabbitMqOptions.Value.Hostname;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _consumers = new Dictionary<string, EventConsumer>();

            InitializeRabbitMqListener(eventHandlerFactory);
        }

        /// <summary>
        /// Executes the async.
        /// </summary>
        /// <param name="stoppingToken">The stopping token.</param>
        /// <returns>A Task.</returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Initializes the rabbit mq listener.
        /// </summary>
        /// <param name="eventHandlerFactory">The event handler factory.</param>
        private void InitializeRabbitMqListener(IQueueHandlerMappingFactory eventHandlerFactory)
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };
            _connection = factory.CreateConnection();
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _channel = _connection.CreateModel();

            Dictionary<string, IEventHandler> handlers = eventHandlerFactory.GetQueueToHandlerMapping();
            foreach (var kvp in handlers)
            {
                _channel.QueueDeclare(queue: kvp.Key,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                _consumers[kvp.Key] = new EventConsumer(kvp.Key, _channel, kvp.Value);
            }
        }

        /// <summary>
        /// RabbitMQ connection shutdown.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The shutdown event arguments.</param>
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

        /// <summary>
        /// Disposes the RabbitMQ after closing channel and connection.
        /// </summary>
        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
