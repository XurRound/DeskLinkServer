namespace DeskLinkServer.Logic
{
    public class Device
    {
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string LastIPAddress { get; set; }

        public Device(string id, string name)
        {
            Identifier = id;
            Name = name;
        }
    }
}
