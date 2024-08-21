using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace HelloWorld
{
    public class Consumer
    {
        public static void Read()
        {
            // 创建连接工厂 
            var factory = new ConnectionFactory();
            // 设置主机地址  
            factory.HostName = "localhost";
            // 设置连接端口号：默认为 5672
            factory.Port = 5672;
            // 虚拟主机名称：默认为 /
            factory.VirtualHost = "/";
            // 设置连接用户名；默认为guest  
            factory.UserName = "guest";
            // 设置连接密码；默认为guest  
            factory.Password = "123456";
            // 创建连接  
            using var connection = factory.CreateConnection();
            // 创建频道  
            using var channel = connection.CreateModel();

            // 声明（创建）队列  
            // queue      参数1：队列名称  
            // durable    参数2：是否定义持久化队列，当 MQ 重启之后还在  
            // exclusive  参数3：是否独占本次连接。若独占，只能有一个消费者监听这个队列且 Connection 关闭时删除这个队列  
            // autoDelete 参数4：是否在不使用的时候自动删除队列，也就是在没有Consumer时自动删除  
            // arguments  参数5：队列其它参数  
            channel.QueueDeclare(queue: "simple_queue_dotnet",
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
                Console.WriteLine("consumerTag：" + ea.ConsumerTag);
                Console.WriteLine("Exchange：" + ea.Exchange);
                Console.WriteLine("RoutingKey：" + ea.RoutingKey);
                Console.WriteLine("properties：" + ea.BasicProperties);
                Console.WriteLine("body：" + Encoding.UTF8.GetString(ea.Body.ToArray()));
            };
            // 参数1. queue：队列名称  
            // 参数2. autoAck：是否自动确认，类似咱们发短信，发送成功会收到一个确认消息  
            // 参数3. callback：回调对象  
            // 消费者类似一个监听程序，主要是用来监听消息  
            channel.BasicConsume(queue: "simple_queue_dotnet",
                                 autoAck: true,
                                 consumer: consumer);

            // 回调方法仅在连接的生命周期内触发，休眠一段时间，避免出现回调触发前就已退出的情况
            Thread.Sleep(TimeSpan.FromSeconds(3));

            // channel创建时使用了using，在超出作用域时会自动关闭释放
            // connection创建时使用了using，在超出作用域时会自动关闭释放
        }
    }
}