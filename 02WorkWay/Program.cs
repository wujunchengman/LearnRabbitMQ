using WorkWay.Fanout;
using WorkWay.WorkQueues;

while (true)
{
    Console.WriteLine("请选择运行示例程序程序：");
    Console.WriteLine("1. Work Queues");
    Console.WriteLine("2. Publish/Subscribe");
    var read = Console.ReadLine();

    if (int.TryParse(read, out var r))
    {
        switch (r)
        {
            case 1:
                {
                    // 通过Task同时启动两个接收程序
                    _ = Task.Run(() =>
                    {
                        Consumer.Read("Consumer1");
                    });
                    _ = Task.Run(() =>
                    {
                        Consumer.Read("Consumer2");
                    });

                    Producer.Send();
                }
                break;
            case 2:
                {
                    _ = Task.Run(() =>
                    {
                        var consumer1 = new FanoutConsumer(FanoutProducer.Queue1Name);
                        consumer1.Read(nameof(consumer1));
                    });
                    _ = Task.Run(() =>
                    {
                        var consumer2 = new FanoutConsumer(FanoutProducer.Queue2Name);
                        consumer2.Read(nameof(consumer2));
                    });
                    FanoutProducer.Send();
                    break;
                }
            default:
                Console.WriteLine("无效输入，请重新输入");
                break;
        }
    }
    else
    {
        Console.WriteLine("请输入数字！！！");
    }
}
