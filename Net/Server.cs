using MeisterGeister.Net.Web;
using Microsoft.Owin.Hosting;
using System;
using System.Reflection;
using System.Threading;

namespace MeisterGeister.Net
{
    public class Server
    {
        private static AutoResetEvent _shutdownEvent = new AutoResetEvent(false);
        private static AutoResetEvent _startupEvent = new AutoResetEvent(false);

        private Thread _workerThread = null;
        private Object _lock = new Object();

        public bool IsStarted
        {
            get {
                return _workerThread != null && _workerThread.IsAlive;
            }
        }

        public void Start()
        {
            lock (_lock)
            {
                if (IsStarted)
                {
                    return;
                }
                _workerThread = new Thread(new ThreadStart(ServerWorker));
                _workerThread.Start();
                _startupEvent.WaitOne();
            }
        }

        public void Stop()
        {
            lock (_lock)
            {
                if (!IsStarted)
                {
                    return;
                }
                _shutdownEvent.Set();
                _workerThread.Join();
            }
        }

        private void ServerWorker()
        {
            _startupEvent.Set();
            try {
                using (WebApp.Start<Starter>("http://+:50132"))
                {
                    Console.Out.WriteLine("Server gestartet");
                    _shutdownEvent.WaitOne();
                    Console.Out.WriteLine("Server beendet");
                }
            }
            catch (TargetInvocationException)
            {
                Console.Out.WriteLine("Server konnte nicht gestartet werden. Wahrscheinlich ist die URL durch die Windows UAC gesperrt.");
            }
        }
    }
}
