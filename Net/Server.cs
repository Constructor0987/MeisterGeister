using MeisterGeister.Net.Web;
using Microsoft.Owin.Hosting;
using System;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using MeisterGeister.Logic.General;

namespace MeisterGeister.Net
{
    public delegate void ServerStateChangeEventHandler(Server.States state);

    public class Server
    {
        public static bool AddURLACL(int port)
        {
            var pi = new System.Diagnostics.ProcessStartInfo();
            pi.Verb = "runas";
            pi.FileName = "netsh";
            pi.Arguments = String.Format("http add urlacl url=http://+:{0}/ sddl=D:(A;;GX;;;S-1-1-0)", port);
            pi.UseShellExecute = true;
            pi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            pi.CreateNoWindow = true;
            return System.Diagnostics.Process.Start(pi).WaitForExit(10000);
        }

        public enum States { Stopped, Starting, Started, Stopping }

        #region Events

        public event ServerStateChangeEventHandler ServerStateChanged;

        private void OnServerStateChanged(States state)
        {
            if (ServerStateChanged != null)
                ServerStateChanged(state);
        }

        #endregion

        #region Serverstatus

        private static readonly AutoResetEvent _shutdownEvent = new AutoResetEvent(false);
        private static readonly AutoResetEvent _startupEvent = new AutoResetEvent(false);


        private Thread _workerThread;
        private readonly Object _lock = new Object();
        private States _status = States.Stopped;

        private static bool IsMainThread()
        {
            return Application.Current.Dispatcher.Thread == Thread.CurrentThread;
        }

        public States Status
        {
            get { return _status; }
            private set
            {
                if (value != _status)
                {
                    _status = value;
                    if (IsMainThread())
                    {
                        OnServerStateChanged(value);
                    }
                    else
                    {
                        Application.Current.Dispatcher.InvokeAsync(new Action(() => OnServerStateChanged(value)));
                    }
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
            bool tryagain = true;
            while (tryagain)
            {
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
                    tryagain = false;
                }
                catch (TargetInvocationException)
                {
                    Logger.LogMsgToFile("Server konnte nicht gestartet werden. Wahrscheinlich ist die URL " + serverUrl + " durch die Windows UAC gesperrt.");
                    int p = (int?)port ?? 50132;
                    tryagain = Server.AddURLACL(p);
                }
            }
        }

        #endregion
    }
}
