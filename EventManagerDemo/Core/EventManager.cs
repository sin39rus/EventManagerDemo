using EventManagerDemo.Core.Services;
using EventManagerDemo.Models;

namespace EventManagerDemo.Core
{
    internal class EventManager
    {
        private static readonly object _lock = new object();
        private static EventManager? _instance;
        private EventManager() { }
        internal static EventManager GetEventManager()
        {
            lock (_lock)
            {
                if (_instance is null)
                    _instance = new EventManager();
                return _instance;
            }
        }


        #region События для службы 1
        internal event EventHandler<IEnumerable<Service1Data>> Service1DataProcessedEvent;
        internal void Service1DataProcessed(Service1 sender, IEnumerable<Service1Data> data)
        {
            Task.Run(() => { Service1DataProcessedEvent?.Invoke(sender, data); });
        }
        #endregion

        #region События для службы 2
        internal event EventHandler Service2DataProcessedEvent;
        internal void Service2DataProcessed(Service2 sender)
        {
            Task.Run(() => { Service2DataProcessedEvent?.Invoke(sender, EventArgs.Empty); });
        }
        #endregion
    }
}
