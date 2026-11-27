#if NET
using MeisterGeister.Logic.General;
using MeisterGeister.Net.Web;
using Nancy;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Windows;

namespace MeisterGeister.Net
{
    public delegate void ServerStateChangeEventHandler(Server.States state);

    public class Server
    {
        public enum States { Stopped, Starting, Started, Stopping }

        private readonly object _lock = new object();
        private readonly string _host;
        private volatile States _status = States.Stopped;
        private HttpListener _listener;
        private Thread _workerThread;

        public event ServerStateChangeEventHandler ServerStateChanged;
        public States Status { get { return _status; } }
        public int? Port { get; private set; }

        public Server() : this("+") { }

        internal Server(string host)
        {
            _host = host;
        }

        public static bool AddURLACL(int port)
        {
            var startInfo = new ProcessStartInfo
            {
                Verb = "runas",
                FileName = "netsh",
                Arguments = string.Format("http add urlacl url=http://+:{0}/ sddl=D:(A;;GX;;;S-1-1-0)", port),
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true
            };
            using (var process = Process.Start(startInfo))
                return process != null && process.WaitForExit(10000) && process.ExitCode == 0;
        }

        private void SetStatus(States value)
        {
            if (_status == value)
                return;
            _status = value;
            var dispatcher = Application.Current?.Dispatcher;
            if (dispatcher != null && !dispatcher.CheckAccess())
                dispatcher.InvokeAsync(new Action(() => ServerStateChanged?.Invoke(value)));
            else
                ServerStateChanged?.Invoke(value);
        }

        public void Start(int port)
        {
            if (port < 1 || port > 65535)
                throw new ArgumentOutOfRangeException(nameof(port));
            lock (_lock)
            {
                if (_status != States.Stopped)
                    return;
                Port = port;
                _listener = new HttpListener();
                _listener.Prefixes.Add(string.Format("http://{0}:{1}/", _host, port));
                SetStatus(States.Starting);
                _workerThread = new Thread(ServerWorker) { IsBackground = true };
                _workerThread.Start(_listener);
            }
        }

        public void Stop()
        {
            Thread worker;
            lock (_lock)
            {
                if (_status == States.Stopped || _status == States.Stopping)
                    return;
                SetStatus(States.Stopping);
                _listener.Close();
                worker = _workerThread;
            }
            if (worker != Thread.CurrentThread)
                worker.Join();
        }

        private void ServerWorker(object state)
        {
            var listener = (HttpListener)state;
            try
            {
                try
                {
                    listener.Start();
                }
                catch (HttpListenerException ex) when (ex.ErrorCode == 5 && _status != States.Stopping)
                {
                    Logger.LogMsgToFile("Server konnte nicht gestartet werden. Die URL-Reservierung wird angefordert.");
                    if (!AddURLACL(Port.Value))
                        throw;
                    listener.Start();
                }

                if (_status == States.Stopping)
                    return;
                var rest = new RestModule();
                var dereGlobus = new DereGlobusModule();
                Logger.LogMsgToFile(string.Format("Webserver gestartet an Port {0}", Port));
                SetStatus(States.Started);
                while (listener.IsListening)
                {
                    HttpListenerContext context;
                    try { context = listener.GetContext(); }
                    catch (HttpListenerException) when (_status == States.Stopping) { break; }
                    catch (ObjectDisposedException) when (_status == States.Stopping) { break; }
                    HandleRequest(context, rest, dereGlobus);
                }
            }
            catch (Exception ex)
            {
                if (_status != States.Stopping)
                    Logger.LogMsgToFile("Webserver-Fehler: " + ex);
            }
            finally
            {
                listener.Close();
                lock (_lock)
                {
                    Port = null;
                    _listener = null;
                    SetStatus(States.Stopped);
                }
                Logger.LogMsgToFile("Webserver beendet");
            }
        }

        private static void HandleRequest(HttpListenerContext context, NancyModule rest, NancyModule dereGlobus)
        {
            try
            {
                if (!string.Equals(context.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                {
                    context.Response.StatusCode = 405;
                    return;
                }
                string path = context.Request.Url.AbsolutePath;
                Response result = rest.Dispatch(path, context.Request.QueryString) ??
                    dereGlobus.Dispatch(path, context.Request.QueryString);
                if (result == null)
                {
                    context.Response.StatusCode = 404;
                    return;
                }
                context.Response.ContentType = result.ContentType;
                using (result.Body)
                    result.Body.CopyTo(context.Response.OutputStream);
            }
            catch (Exception ex)
            {
                Logger.LogMsgToFile("Webserver-Anfrage fehlgeschlagen: " + ex);
                context.Response.StatusCode = 500;
            }
            finally
            {
                context.Response.Close();
            }
        }
    }
}
#else
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
#endif
