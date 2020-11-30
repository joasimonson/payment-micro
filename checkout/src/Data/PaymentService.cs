using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Checkout.Data
{
    public class PaymentService
    {
        private readonly string RABBITMQ_DEFAULT_HOST;
        private readonly int RABBITMQ_DEFAULT_PORT;
        private readonly string RABBITMQ_DEFAULT_USER;
        private readonly string RABBITMQ_DEFAULT_PASS;
        private readonly string RABBITMQ_PUBLISHER_EXCHANGE;

        public PaymentService(IConfiguration configuration)
        {
            RABBITMQ_DEFAULT_HOST = configuration.GetValue<string>(nameof(RABBITMQ_DEFAULT_HOST));
            RABBITMQ_DEFAULT_PORT = configuration.GetValue<int>(nameof(RABBITMQ_DEFAULT_PORT));
            RABBITMQ_DEFAULT_USER = configuration.GetValue<string>(nameof(RABBITMQ_DEFAULT_USER));
            RABBITMQ_DEFAULT_PASS = configuration.GetValue<string>(nameof(RABBITMQ_DEFAULT_PASS));
            RABBITMQ_PUBLISHER_EXCHANGE = configuration.GetValue<string>(nameof(RABBITMQ_PUBLISHER_EXCHANGE));
        }

        public async Task SendToQueue(Payment payment)
        {
            var factory = new ConnectionFactory()
            {
                HostName = RABBITMQ_DEFAULT_HOST,
                Port = RABBITMQ_DEFAULT_PORT,
                UserName = RABBITMQ_DEFAULT_USER,
                Password = RABBITMQ_DEFAULT_PASS
            };

            await Task.Run(() =>
            {
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                var jsonMsg = JsonSerializer.Serialize(payment);
                var msg = Encoding.UTF8.GetBytes(jsonMsg);

                channel.BasicPublish(exchange: RABBITMQ_PUBLISHER_EXCHANGE,
                                     routingKey: "",
                                     basicProperties: null,
                                     body: msg);
            });
        }
    }
}
