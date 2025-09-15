using EventManagerDemo.Models;

namespace EventManagerDemo.Core.Services
{
    internal class Service2
    {
        private readonly EventManager _eventManager;
        private readonly CancellationToken _cancellationToken;
        private readonly Stack<Service1Data> _data = new Stack<Service1Data>();
        internal bool _disposed = false;
        public Service2(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            _eventManager = EventManager.GetEventManager();
            _eventManager.Service1DataProcessedEvent += Service1DataProcessedEvent; //Подписываемся на событие получения обработанных данных из службы №1
        }
        private void Service1DataProcessedEvent(object? sender, IEnumerable<Service1Data> e)
        {
            Console.WriteLine($"Service2 данные службы №1 успешно получены. Количество полученных строк: {e.Count()}.");
            foreach (var item in e)
                _data.Push(item);
        }
        internal async Task StartAsync()
        {
            Console.WriteLine("Service2 started");
            while (!_cancellationToken.IsCancellationRequested)
            {
                int dataProcessed = 0;
                while (_data.Any())
                {
                    dataProcessed++;
                    var item = _data.Pop();
                    try
                    {
                        Console.WriteLine($"Service2 Обрабатываем строку {item.Id}.");
                        await Task.Delay(1000);
                    }
                    catch (Exception ex)
                    {
                        _data.Push(item); //Возвращаем в стэк если обработка не удалась
                    }
                }
                if (dataProcessed > 0)
                    _eventManager.Service2DataProcessed(this);
            }
            Console.WriteLine("Service2 stopped");
            _disposed = true;
        }
    }
}
