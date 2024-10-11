using WorkWay.WorkQueues;

while (true)
{
    Console.WriteLine("请选择运行示例程序程序：");
    Console.WriteLine("1. Work Queues");
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
