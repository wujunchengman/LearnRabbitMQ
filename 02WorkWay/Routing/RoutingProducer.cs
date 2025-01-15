using System.Text;
using RabbitMQ.Client;
using WorkWay.Utils;

namespace WorkWay.Routing
{
    public class RoutingProducer
    {static readonly string ExchangeName = "test_direct";
        public const string Queue1Name = "test_direct_queue1";
        public const string Queue2Name = "test_direct_queue2";
        public static void Send()
        {
            // 获取连接
            using var connection = ConnectionUtil.GetConnection();

            // 创建频道
            using var channel = connection.CreateModel();

            // 参数1. exchange：交换机名称  
            // 参数2. type：交换机类型  
            //     DIRECT("direct")：定向  
            //     FANOUT("fanout")：扇形（广播），发送消息到每一个与之绑定队列。  
            //     TOPIC("topic")：通配符的方式  
            //     HEADERS("headers")：参数匹配  
            // 参数3. durable：是否持久化  
            // 参数4. autoDelete：自动删除  
            // 参数5. arguments：其它参数  

            // 创建交换机
            channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Direct);

            // 创建队列
            channel.QueueDeclare(queue: Queue1Name,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            channel.QueueDeclare(queue: Queue2Name,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            // 参数1. queue：队列名称  
            // 参数2. exchange：交换机名称  
            // 参数3. routingKey：路由键，绑定规则  
            //     如果交换机的类型为fanout，routingKey设置为""  

            // 绑定队列和交换机
            // 队列1绑定error
            channel.QueueBind(Queue1Name, ExchangeName, "error");

            // 队列2绑定info error warning
            channel.QueueBind(Queue2Name, ExchangeName, "info");
            channel.QueueBind(Queue2Name, ExchangeName, "error");
            channel.QueueBind(Queue2Name, ExchangeName, "warning");

            // 发送消息
            channel.BasicPublish(ExchangeName,"warning",null,Encoding.UTF8.GetBytes("日志信息：张三调用了delete方法...日志级别：warning..."));

            channel.BasicPublish(ExchangeName,"error",null,Encoding.UTF8.GetBytes("日志信息：张三调用了delete方法...日志级别：error..."));

            // 释放资源
            // using资源会在离开作用域时自动释放
        }
    }
}