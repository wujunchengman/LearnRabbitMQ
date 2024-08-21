using System.Text;
using RabbitMQ.Client;
namespace HelloWorld
{
    public class Producer
    {
        public static void Send()
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
            // 要发送的信息  
            const string message = "你好；小兔子！";
            // 参数1：交换机名称,如果没有指定则使用默认Default Exchange  
            // 参数2：路由key,简单模式可以传递队列名称  
            // 参数3：配置信息  
            // 参数4：消息内容  
            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "simple_queue_dotnet",
                                 basicProperties: null,
                                 body: Encoding.UTF8.GetBytes(message));
            Console.WriteLine($"已发送消息：{message}");

            // channel创建时使用了using，在超出作用域时会自动关闭释放
            // connection创建时使用了using，在超出作用域时会自动关闭释放

        }
    }
}