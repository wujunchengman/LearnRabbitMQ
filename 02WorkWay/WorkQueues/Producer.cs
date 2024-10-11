using System.Text;
using RabbitMQ.Client;
using WorkWay.Utils;

namespace WorkWay.WorkQueues
{
    public class Producer
    {
        static readonly string QUEUE_NAME = "work_queue";
        public static void Send()
        {
            using var connection = ConnectionUtil.GetConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: QUEUE_NAME,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            for (int i = 0; i < 10; i++)
            {
                var message = i+"Hello RabbitMQ~~~";  
                channel.BasicPublish(exchange: string.Empty,
                    routingKey: QUEUE_NAME,
                    basicProperties: null,
                    body: Encoding.UTF8.GetBytes(message));
            }


        }
    }
}