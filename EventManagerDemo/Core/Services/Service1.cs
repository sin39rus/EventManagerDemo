using EventManagerDemo.Helpers;
using EventManagerDemo.Models;

namespace EventManagerDemo.Core.Services
{
    internal class Service1
    {
        private readonly EventManager _eventManager;
        private readonly CancellationToken _cancellationToken;
        internal bool _disposed = false;
        public Service1(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            _eventManager = EventManager.GetEventManager();
            _eventManager.Service2DataProcessedEvent += Service2DataProcessedEvent;
        }

        internal async Task StartAsync()
        {
            Console.WriteLine("Service1 started");
            while (!_cancellationToken.IsCancellationRequested)
            {
                try
                {
                    Console.WriteLine("Service1 обрабатываем данные.");
                    var data = await DataProcessingAsync();// Обработка займет 5 секунд.
                    Console.WriteLine($"Service1 данные успешно обработаны, получено {data.Count} строк.");
                    _eventManager.Service1DataProcessed(this, data); //Сообщаем менеджеру что мы обработали данные

                    await Task.Delay(TimeSpan.FromSeconds(10), _cancellationToken); //Отдыхаем что бы не грузить поток.
                }
                catch
                {
                }
            }
            Console.WriteLine("Service1 stopped");
            _disposed = true;
        }
        private Task<List<Service1Data>> DataProcessingAsync()
        {
            return Task.Run(() =>
            {
                return DataContext.GetService1Data();
            });
        }
        private void Service2DataProcessedEvent(object? sender, EventArgs e) =>
            Console.WriteLine("Service1 Служба №2 сообщает о завершении обработки документов.");
    }
}
