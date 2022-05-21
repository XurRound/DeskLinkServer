using DeskLinkServer.Logic.Helpers.StringEnum;

namespace DeskLinkServer.Framework.Components
{
    public enum Status
    {
        [StatusAttribute("checkIcon", false)]
        Success,
        [StatusAttribute("waitIcon", true)]
        Wait,
        [StatusAttribute("errorIcon", false)]
        Error,
        [StatusAttribute("deviceIcon", false)]
        MissingDevices
    }
}
