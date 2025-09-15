using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerDemo.Core.Services
{
    internal class LoggerService
    {
        public LoggerService()
        {
            EventManager eventManager = EventManager.GetEventManager();
            eventManager.Service1DataProcessedEvent += EventManager_Service1DataProcessedEvent;
            eventManager.Service2DataProcessedEvent += EventManager_Service2DataProcessedEvent;
        }

        private void EventManager_Service2DataProcessedEvent(object? sender, EventArgs e)
        {
            Thread.Sleep(1000000);
        }

        private void EventManager_Service1DataProcessedEvent(object? sender, IEnumerable<Models.Service1Data> e)
        {
            Console.WriteLine("LoggerService_Service1DataProcessedEvent");
        }
    }
}
