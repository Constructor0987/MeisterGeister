using System.Diagnostics.CodeAnalysis;
using MeisterGeister.Net;
using MeisterGeister.ViewModel.Base;

// ReSharper disable once CheckNamespace

namespace MeisterGeister.ViewModel.Settings
{
    public class NetzwerkeinstellungenViewModel : ViewModelBase
    {
        private const int ServerPort = 50132;

        public NetzwerkeinstellungenViewModel()
        {
            OnToggleWebServerStatus = new CommandBase(ToggleWebServerStatus, CanToggleWebServerStatus);
        }

        public string WebServerStatusText
        {
            get
            {
                var status = Global.WebServer.Status;
                var port = Global.WebServer.Port;
                switch (status)
                {
                    case Server.States.Stopped:
                        return "Webserver inaktiv";
                    case Server.States.Starting:
                        return string.Format("Webserver wird auf Port {0} gestartet ...", port);
                    case Server.States.Started:
                        return string.Format("Webserver aktiv auf Port {0}", port);
                    case Server.States.Stopping:
                        return "Webserver wird angehalten";
                }
                return string.Format("Ups! Es gibt leider keinen Text für den Status {0}", status);
            }
        }

        public CommandBase OnToggleWebServerStatus { get; }

        private bool CanToggleWebServerStatus(object arg)
        {
            return Global.WebServer.Status == Server.States.Started ||
                   Global.WebServer.Status == Server.States.Stopped;
        }

        private void ToggleWebServerStatus(object obj)
        {
            switch (Global.WebServer.Status)
            {
                case Server.States.Started:
                    Global.WebServer.Stop();
                    break;
                case Server.States.Stopped:
                    Global.WebServer.Start(ServerPort);
                    break;
            }
        }

        public override void RegisterEvents()
        {
            base.RegisterEvents();
            Global.WebServer.ServerStateChanged += WebServer_ServerStateChanged;
        }

        public override void UnregisterEvents()
        {
            base.UnregisterEvents();
            Global.WebServer.ServerStateChanged -= WebServer_ServerStateChanged;
        }

        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        private void WebServer_ServerStateChanged(Server.States state)
        {
            OnToggleWebServerStatus.Invalidate();
            OnChanged("WebServerStatusText");
        }
    }
}