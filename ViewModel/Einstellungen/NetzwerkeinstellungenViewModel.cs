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
                        return "Webserver wird auf Port {port} gestartet ...";
                    case Server.States.Started:
                        return "Webserver aktiv auf Port {port}";
                    case Server.States.Stopping:
                        return "Webserver wird angehalten";
                }
                return "Ups! Es gibt leider keinen Text für den Status {status}";
            }
        }

        public CommandBase OnToggleWebServerStatus { get; private set; }

        private bool CanToggleWebServerStatus(object arg)
        {
            return Global.WebServer.Status == Server.States.Started || Global.WebServer.Status == Server.States.Stopped;
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
            OnChanged("WebServerStatusText");
        }
    }
}