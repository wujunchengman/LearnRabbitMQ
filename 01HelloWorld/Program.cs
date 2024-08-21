// See https://aka.ms/new-console-template for more information
using HelloWorld;

while (true)
{
    Console.WriteLine("请选择运行生产者程序还是消费者程序：");
    Console.WriteLine("1. 生产者程序");
    Console.WriteLine("2. 消费者程序");
    var read = Console.ReadLine();

    if (int.TryParse(read, out var r))
    {
        switch (r)
        {
            case 1:
                Producer.Send();
                break;
            case 2:
                Consumer.Read();
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
