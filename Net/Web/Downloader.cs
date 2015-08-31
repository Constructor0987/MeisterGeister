using MeisterGeister.Logic.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.Net.Web
{
    /// <summary>
    /// Bearbeitet einen Download nach dem nächsten.
    /// </summary>
    public class Downloader : IDisposable
    {
        private WebClient webClient;
        private Queue<DownloadInfo> queue;
        private bool isRunning = false;

        public bool IsRunning
        {
            get { return isRunning; }
        }

        public Downloader() 
        {
            webClient = new WebClient();
            webClient.DownloadFileCompleted += webClient_DownloadFileCompleted;
            webClient.DownloadProgressChanged += webClient_DownloadProgressChanged;
            queue = new Queue<DownloadInfo>();
        }

        /// <summary>
        /// Fügt einen Download mit Callback hinzu und startet den Downloader.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="filePath"></param>
        /// <param name="callBack"></param>
        public void AddDownload(string url, string filePath, Action<string, Exception> callBack = null)
        {
            queue.Enqueue(new DownloadInfo() {
                Url = url,
                FilePath = filePath,
                CallBack = callBack
            });
            if (!isRunning)
                StartDownload();
        }

        void StartDownload()
        {
            if(queue.Count == 0)
            {
                isRunning = false;
                return;
            }
            isRunning = true;
            var di = queue.Peek();
            Uri uri = new Uri(di.Url);
            try
            {
                webClient.DownloadFileAsync(uri, di.FilePath);
            }
            catch(Exception e)
            {
                //return error
                if (di.CallBack != null)
                    di.CallBack(di.FilePath, e);
                //continue
                queue.Dequeue();
                StartDownload();
            }
        }

        void webClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            var di = queue.Dequeue();
            if (di.CallBack != null)
                di.CallBack(di.FilePath, null);
            //start next download
            StartDownload();
        }

        void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            RaiseDownloadProgressChanged(sender, e);
        }

        #region Events
        private static WeakEvent<DownloadProgressChangedEventHandler> _downloadProgressChanged = new WeakEvent<DownloadProgressChangedEventHandler>();
        public static event DownloadProgressChangedEventHandler DownloadProgressChanged
        {
            add { _downloadProgressChanged.Add(value); }
            remove { _downloadProgressChanged.Remove(value); }
        }

        public static void RaiseDownloadProgressChanged(object sender = null, DownloadProgressChangedEventArgs args = null)
        {
            _downloadProgressChanged.Raise(sender, args ?? DownloadProgressChangedEventArgs.Empty);
        }
        #endregion

        private class DownloadInfo
        {
            public string Url;
            public string FilePath;
            public Action<string, Exception> CallBack;
        }

        #region IDisposable
        public void Dispose(bool itIsSafeToAlsoFreeManagedObjects)
        {
            if (itIsSafeToAlsoFreeManagedObjects)
            {
                //free managed objects
                if (webClient != null)
                {
                    webClient.CancelAsync();
                    webClient.Dispose();
                    webClient = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Downloader ()
        {
            Dispose(false);
        }
        #endregion
    }
}
