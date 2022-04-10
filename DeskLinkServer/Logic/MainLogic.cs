using DeskLinkServer.Logic.Configuration;
using DeskLinkServer.Logic.Network.Discovery;

namespace DeskLinkServer.Logic
{
    public class MainLogic
    {
        public Config Configuration { get; }

        private readonly ServiceDispatcher serviceDispatcher;

        public MainLogic()
        {
            Configuration = ConfigManager.LoadConfig();

            serviceDispatcher = new ServiceDispatcher(Configuration.ServiceName, Configuration.ServicePort);
        }

        public void Start()
        {
            serviceDispatcher.Start();
        }

        public void Stop()
        {
            serviceDispatcher.Stop();
            ConfigManager.SaveConfig(Configuration);
        }
    }
}
