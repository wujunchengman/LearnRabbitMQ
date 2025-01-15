using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WorkWay.Utils;

namespace WorkWay.Routing
{
    public class RoutingConsumer(string queueName)
    {
        private readonly string QUEUE_NAME = queueName;
        public void Read(string name)
        {
            using var connection = ConnectionUtil.GetConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: QUEUE_NAME,
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            // 接收消息  
            var consumer = new EventingBasicConsumer(channel);

            // 回调方法,当收到消息后，会自动执行该方法  
            // consumerTag：标识   
            // body：数据 
            consumer.Received += (model, ea) =>
            {
                Console.WriteLine($"{name} body：" + Encoding.UTF8.GetString(ea.Body.ToArray()));
            };
            // 参数1. queue：队列名称  
            // 参数2. autoAck：是否自动确认，类似咱们发短信，发送成功会收到一个确认消息  
            // 参数3. callback：回调对象  
            // 消费者类似一个监听程序，主要是用来监听消息  
            channel.BasicConsume(queue: QUEUE_NAME,
                                 autoAck: true,
                                 consumer: consumer);

            // 回调方法仅在连接的生命周期内触发，休眠一段时间，避免出现回调触发前就已退出的情况
            Thread.Sleep(TimeSpan.FromSeconds(3));
        }
    }
}