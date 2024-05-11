using System.Text;
using System.Text.Json;
using PlatformService.Dtos;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _config;
        private readonly IConnection _connenction;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration config)
        {
            _config = config;
            var factory = new ConnectionFactory()
            {
                HostName = _config["RabbitMQHost"],
                Port = int.Parse(_config["RabbitMQPort"]),
            };
            try
            {
                _connenction = factory.CreateConnection();
                _channel = _connenction.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _connenction.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                Console.WriteLine("-->Connected to Rabbitmq");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"-->Could not connect to rabbitmq:{ex.Message}");
            }
        }
        public void PublishNewPlatform(PlatformPublishedDto dto)
        {
            var message = JsonSerializer.Serialize(dto);
            if (_connenction.IsOpen)
            {
                Console.WriteLine("--> bus open ,sending msgs");
                //for sending the message
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("--> bus closed");
            }
        }
        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "trigger",
              routingKey: "",
              basicProperties: null,
              body: body
            );
            Console.WriteLine($"-->msg send {message}");
        }
        private void Dispose()
        {
            Console.WriteLine("bus disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connenction.Close();
            }
        }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> Rabbitmq Connenction shutdown");
        }
    }
}