using EventManagerDemo.Core.Services;

namespace EventManagerDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Console.WriteLine("Инициируем службы");
            Service1 service1 = new Service1(cancellationTokenSource.Token);
            Service2 service2 = new Service2(cancellationTokenSource.Token);

            Console.WriteLine("Запускаем службы");
            service1.StartAsync();
            service2.StartAsync();
            Console.ReadLine();//Ставим поток в ожидание
            cancellationTokenSource.Cancel();//Останавливаем службы
            while (service1._disposed && service2._disposed) //Ожидаем когда обе службы завершат свою работу.
            {

            }

        }
    }
}
