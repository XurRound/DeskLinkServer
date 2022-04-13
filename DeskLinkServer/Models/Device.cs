namespace DeskLinkServer.Logic
{
    public class Device
    {
        public string Identifier { get; }
        public string Name { get; }
        public string LastIPAddress { get; set; }

        public Device(string id, string name)
        {
            Identifier = id;
            Name = name;
        }
    }
}
