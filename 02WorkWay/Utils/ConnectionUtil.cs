using RabbitMQ.Client;

namespace WorkWay.Utils
{
    public class ConnectionUtil
    {
        public static string HOST_ADDRESS = "localhost";  

        public static IConnection GetConnection(){
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
            return factory.CreateConnection();
        }
    }
}