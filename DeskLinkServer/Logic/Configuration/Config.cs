using Newtonsoft.Json;
using System.Collections.Generic;

namespace DeskLinkServer.Logic.Configuration
{
    public class Config
    {
        [JsonIgnore]
        public readonly string ServiceName = "DeskLink";
        [JsonIgnore]
        public readonly int ServicePort = 15507;

        public readonly List<Device> KnownDevices = new List<Device>();


    }
}
