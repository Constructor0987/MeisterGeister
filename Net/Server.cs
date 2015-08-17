using MeisterGeister.Net.Web;
using Microsoft.Owin.Hosting;
using System;
using System.Reflection;
using System.Threading;
using MeisterGeister.Logic.General;

namespace MeisterGeister.Net
{
    public delegate void ServerStateChangeEventHandler(Server.States state);

    public class Server
    {
        public enum States { Stopped, Starting, Started, Stopping }

        #region Events

        public event ServerStateChangeEventHandler ServerStateChanged;

        private void OnServerStateChanged(States state)
        {
            if (ServerStateChanged != null)
                ServerStateChanged.Invoke(state);
        }

        #endregion

        #region Serverstatus

        private static AutoResetEvent _shutdownEvent = new AutoResetEvent(false);
        private static AutoResetEvent _startupEvent = new AutoResetEvent(false);


        private Thread _workerThread;
        private readonly Object _lock = new Object();
        private States _status = States.Stopped;

        public States Status
        {
            get { return _status; }
            private set
            {
                if (value != _status)
                {
                    _status = value;
                    OnServerStateChanged(value);
                }
            }
        }

        public int? Port { get; private set; }

        public void Start(int port)
        {
            lock (_lock)
            {
                if (Status != States.Stopped)
                {
                    return;
                }
                Logger.LogMsgToFile(String.Format("Starte Webserver an Port {0}", port));
                Port = port;
                Status = States.Starting;
                _workerThread = new Thread(ServerWorker);
                _workerThread.Start(port);
                _startupEvent.WaitOne();
            }
        }

        public void Stop()
        {
            lock (_lock)
            {
                if (Status != States.Started)
                {
                    return;
                }
                _shutdownEvent.Set();
                _workerThread.Join();
                Logger.LogMsgToFile("Webserver beendet");
                Port = null;
                Status = States.Stopped;
            }
        }

        private void ServerWorker(object port)
        {
            _startupEvent.Set();
            String serverUrl = String.Format("http://+:{0}", port);
            try
            {
                using (WebApp.Start<Starter>(serverUrl))
                {
                    Logger.LogMsgToFile(String.Format("Webserver gestartet an Port {0}", port));
                    Status = States.Started;
                    _shutdownEvent.WaitOne();
                    Logger.LogMsgToFile(String.Format("Beende Webserver an Port {0}", port));
                    Status = States.Stopping;
                }
            }
            catch (TargetInvocationException)
            {
                Logger.LogMsgToFile("Server konnte nicht gestartet werden. Wahrscheinlich ist die URL " + serverUrl +  " durch die Windows UAC gesperrt.");
            }
        }

        #endregion
    }
}
