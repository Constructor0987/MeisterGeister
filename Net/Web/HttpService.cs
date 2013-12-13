using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;

namespace MeisterGeister.Net.Web
{
    public class HttpService : IDisposable
    {
        private readonly HttpListener _listener;
        private readonly Thread _listenerThread;
        private readonly Thread[] _workers;
        private readonly ManualResetEvent _stop, _ready;
        private Queue<HttpListenerContext> _queue;

        public HttpService(int maxThreads)
        {
            _workers = new Thread[maxThreads];
            _queue = new Queue<HttpListenerContext>();
            _stop = new ManualResetEvent(false);
            _ready = new ManualResetEvent(false);
            _listener = new HttpListener();
            _listenerThread = new Thread(HandleRequests);
        }

        public void Start(int port, string url = "")
        {
            _listener.Prefixes.Add(String.Format(@"http://localhost:{0}/{1}", port, url));
            try
            {
                _listener.Start(); //TODO JT: schmeisst httplistenerexception, wenn der Port bereits genutzt wird.
                _listenerThread.Start();

                for (int i = 0; i < _workers.Length; i++)
                {
                    _workers[i] = new Thread(Worker);
                    _workers[i].Start();
                }
            }
            catch { }
        }

        public void Dispose()
        { Stop(); }

        public void Stop()
        {
            try
            {
                _stop.Set();
                _listenerThread.Join();
                foreach (Thread worker in _workers)
                    worker.Join();
                _listener.Stop();
            }
            catch { } //TODO JT: sauber lösen
        }

        private void HandleRequests()
        {
            while (_listener.IsListening)
            {
                var context = _listener.BeginGetContext(ContextReady, null);

                if (0 == WaitHandle.WaitAny(new[] { _stop, context.AsyncWaitHandle }))
                    return;
            }
        }

        private void ContextReady(IAsyncResult ar)
        {
            try
            {
                lock (_queue)
                {
                    if (!_listener.IsListening)
                        return;
                    _queue.Enqueue(_listener.EndGetContext(ar));
                    _ready.Set();
                }
            }
            catch { return; }
        }

        private void Worker()
        {
            WaitHandle[] wait = new[] { _ready, _stop };
            while (0 == WaitHandle.WaitAny(wait))
            {
                HttpListenerContext context;
                lock (_queue)
                {
                    if (_queue.Count > 0)
                        context = _queue.Dequeue();
                    else
                    {
                        _ready.Reset();
                        continue;
                    }
                }

                try { ProcessRequest(context); }
                catch (Exception e) { Console.Error.WriteLine(e); }
            }
        }

        public event Action<HttpListenerContext> ProcessRequest;
    }
}
